
namespace Assets.Scripts
{
    using System.Collections;

    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.Networking.Match;

    /// <summary>
    /// The network player.
    /// </summary>
    public class NetworkPlayer : NetworkBehaviour
    {
        /// <summary>
        /// The facing right.
        /// </summary>
        private bool facingRight;

        /// <summary>
        /// The bullet.
        /// </summary>
        [SerializeField]
        private GameObject bullet;

        /// <summary>
        /// The arm transform.
        /// </summary>
        [SerializeField]
        private Transform armTransform;

        /// <summary>
        /// The body transform.
        /// </summary>
        [SerializeField]
        private Transform bodyTransform;

        /// <summary>
        /// The fire point transform.
        /// </summary>
        [SerializeField]
        private Transform firePointTransform;

        /// <summary>
        /// The animator.
        /// </summary>
        [SerializeField]
        private Animator anim;

        /// <summary>
        /// The can jump.
        /// </summary>
        private bool canJump;

        /// <summary>
        /// The health.
        /// </summary>
        private float health;

        /// <summary>
        /// The max health.
        /// </summary>
        private float maxHealth;

        /// <summary>
        /// Gets or sets a value indicating whether jump prop.
        /// </summary>
        public bool JumpProp
        {
            get { return this.canJump; }
            set { this.canJump = value; }
        }

        /// <summary>
        /// Gets or sets the health.
        /// </summary>
        public float Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        /// <summary>
        /// Gets the max health.
        /// </summary>
        public float MaxHealth
        {
            get { return this.maxHealth; }
        }

        /// <summary>
        /// Gets the bullet.
        /// </summary>
        public GameObject Bullet
        {
            get { return this.bullet; }
        }

        /// <summary>
        /// The take damage.
        /// </summary>
        /// <param name="amount">
        /// The amount.
        /// </param>
        public void TakeDamage(float amount)
        {
            if (!this.isServer || this.health <= 0)
            {
                return;
            }

            if (NetworkGameManager.PlayersGetCount() == 1)
            {
                return;
            }

            this.health -= amount;
            this.GetComponent<Commands>().CmdHealthUpdate(this.health);

            if (this.health <= 0)
            {
                this.health = 0;

                if (NetworkGameManager.RemoveAndCheckForWin(this))
                {
                    this.StartCoroutine(this.GoToLobby());
                }
            }

        }

        /// <summary>
        /// The awake function.
        /// </summary>
        private void Awake()
        {
            if (this.armTransform == null)
            {
                this.armTransform = this.GetComponentsInChildren<Transform>()[2];
            }

            if (this.bodyTransform == null)
            {
                this.bodyTransform = this.GetComponentsInChildren<Transform>()[1];
            }

            if (this.firePointTransform == null)
            {
                this.firePointTransform = this.GetComponentsInChildren<Transform>()[4];
            }

            if (this.anim == null)
            {
                this.anim = this.GetComponent<Animator>();
            }
        }

        /// <summary>
        /// The start function.
        /// </summary>
        private void Start()
        {
            this.maxHealth = 100;
            this.health = this.maxHealth;
            this.canJump = true;
        }

        /// <summary>
        /// The update function.
        /// </summary>
        private void Update()
        {
            if (!this.isLocalPlayer || PauseMenu.IsOn || this.health <= 0)
            {
                return;
            }

            this.Move();
            this.Jump();
            Vector3 dir = this.Aim();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                NetworkAudioManager.Self.CmdSendClipToServer(0);
                this.GetComponent<Commands>().CmdShoot(dir);
            }
        }

        /// <summary>
        /// The move function.
        /// </summary>
        private void Move()
        {
            this.anim.SetFloat("Speed", 0);


            if (Input.GetKeyDown(KeyCode.A) && !this.facingRight)
            {
                this.GetComponent<Commands>().CmdFlipBody(this.Flip());
            }

            if (Input.GetKeyDown(KeyCode.D) && this.facingRight)
            {
                this.GetComponent<Commands>().CmdFlipBody(this.Flip());
            }

            if (Input.GetKey(KeyCode.A))
            {
                this.anim.SetFloat("Speed", 5);
                this.transform.position += Vector3.left * 5 * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.D))
            {
                this.anim.SetFloat("Speed", 5);
                this.transform.position += Vector3.right * 5 * Time.deltaTime;
            }
        }

        /// <summary>
        /// The aim function.
        /// </summary>
        /// <returns>
        /// The <see cref="Vector3"/>.
        /// </returns>
        private Vector3 Aim()
        {

            // Get mouse position
            Vector3 mousePosition = Input.mousePosition;

            // Set the z position to offset with the camera
            mousePosition.z = Camera.main.transform.position.z * -1;

            // Get the displacement between mouse an muzzle position
            Vector3 displacement = Camera.main.ScreenToWorldPoint(mousePosition) - this.armTransform.position;
            
            // Get the direction
            Vector3 bulletdirection = displacement.normalized;

            // Calculate the rotation on Z
            float rotZ = Mathf.Atan2(bulletdirection.x, bulletdirection.y) * Mathf.Rad2Deg;

            // Rotate the z axis as the mouse moves
            this.armTransform.rotation = Quaternion.Euler(0f, 0f, -rotZ + 90);
            this.GetComponent<Commands>().CmdArmRotation(Quaternion.Euler(0f, 0f, -rotZ + 90));

            return bulletdirection;
        }

        /// <summary>
        /// The jump function.
        /// </summary>
        private void Jump()
        {
            this.anim.SetBool("IsGrounded", this.canJump);
            if (Input.GetKeyDown(KeyCode.Space) && this.canJump)
            {
                this.canJump = false;
                this.GetComponent<Rigidbody>().velocity = new Vector3(this.GetComponent<Rigidbody>().velocity.x, 10, 0);
            }
        }

        /// <summary>
        /// The flip function.
        /// </summary>
        /// <returns>
        /// The <see cref="Vector3"/>.
        /// </returns>
        private Vector3 Flip()
        {
            this.facingRight = !this.facingRight;
            Vector3 theScale = this.bodyTransform.localScale;
            theScale.x *= -1;
            return this.bodyTransform.localScale = theScale;
        }

        /// <summary>
        /// The go to lobby function.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        private IEnumerator GoToLobby()
        {
            NetworkManager networkManager = NetworkManager.singleton;
            MatchInfo matchInfo = networkManager.matchInfo;
            yield return new WaitForSeconds(3);

            networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
            networkManager.StopHost();
        }
    }
}
