using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class NetworkPlayer : NetworkBehaviour
{
    private bool facingRight;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform armTransform;
    [SerializeField]
    private Transform bodyTransform;
    [SerializeField]
    private Transform firePointTransform;


    [SerializeField]
    private Animator anim;

    private bool canJump;

    public bool JumpProp
    {
        get { return canJump; }
        set { canJump = value; }
    }

    private float health;
    private float maxHealth;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
    }

    public GameObject Bullet
    {
        get { return bullet; }
    }

    void Awake()
    {
        if (armTransform == null)
            armTransform = GetComponentsInChildren<Transform>()[2];

        if (bodyTransform == null)
            bodyTransform = GetComponentsInChildren<Transform>()[1];

        if (firePointTransform == null)
            firePointTransform = GetComponentsInChildren<Transform>()[4];

        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Start()
    {
        maxHealth = 100;
        health = maxHealth;
        canJump = true;
    }

    void Update()
    {
        if (!isLocalPlayer || PauseMenu.isOn)
            return;

        Move();
        Jump();
        Vector3 dir = Aim();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            GetComponent<Commands>().CmdShoot(dir);
    }

    private void Move()
    {
        anim.SetFloat("Speed", 0);


        if (Input.GetKeyDown(KeyCode.A) && !facingRight)
        {
            GetComponent<Commands>().CmdFlipBody(Flip());
        }

        if (Input.GetKeyDown(KeyCode.D) && facingRight)
        {
            GetComponent<Commands>().CmdFlipBody(Flip());
        }

        //Move to the left
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetFloat("Speed", 5);
            transform.position += Vector3.left * 5 * Time.deltaTime;
        }

        //Move to the right
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetFloat("Speed", 5);
            transform.position += Vector3.right * 5 * Time.deltaTime;
        }
    }

    private Vector3 Aim()
    {

        //Get mouse position
        Vector3 mousePosition = Input.mousePosition;
        //Set the z position to offset with the camera
        mousePosition.z = Camera.main.transform.position.z * -1;

        //Get the displacement between mouse an muzzle position
        Vector3 displacement = Camera.main.ScreenToWorldPoint(mousePosition) - armTransform.position;
        //Get the direction
        Vector3 bulletdirection = displacement.normalized;

        //Calculate the rotation on Z
        float rotZ = Mathf.Atan2(bulletdirection.x, bulletdirection.y) * Mathf.Rad2Deg;

        //Rotate the z axis as the mouse moves
        armTransform.rotation = Quaternion.Euler(0f, 0f, -rotZ + 90);
        GetComponent<Commands>().CmdArmRotation(Quaternion.Euler(0f, 0f, -rotZ + 90));

        return bulletdirection;
    }

    private void Jump()
    {
        anim.SetBool("IsGrounded", canJump);
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 10, 0);
        }
    }

    private Vector3 Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = bodyTransform.localScale;
        theScale.x *= -1;
        return bodyTransform.localScale = theScale;
    }

    public void TakeDamage(float amount)
    {
        if (!isServer)
            return;

        if (NetworkGameManager.PlayersGetCount() == 1)
            return;

        health -= amount;
        GetComponent<Commands>().CmdHealthUpdate(health);

        if (health <= 0)
        {
            if (NetworkGameManager.RemoveAndCheckForWin(this))
            {
                StartCoroutine(GoToLobby());
            }
        }
            
    }

    IEnumerator GoToLobby()
    {
        NetworkManager networkManager = NetworkManager.singleton;
        MatchInfo matchInfo = networkManager.matchInfo;
        yield return new WaitForSeconds(3);

        networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
    }

    void BackToLobby()
    {
        FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();
    }
}
