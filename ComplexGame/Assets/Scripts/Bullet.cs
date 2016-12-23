
namespace Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.Networking;

    /// <summary>
    /// The bullet class.
    /// </summary>
    public class Bullet : NetworkBehaviour
    {
        /// <summary>
        /// The speed.
        /// </summary>
        private float speed;

        /// <summary>
        /// The direction.
        /// </summary>
        private Vector3 direction;

        /// <summary>
        /// The damage.
        /// </summary>
        private float damage;

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        public float Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public Vector3 Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public float Damage
        {
            get { return this.damage; }
            set { this.damage = value; }
        }

        /// <summary>
        /// The start function.
        /// </summary>
        private void Start()
        {
            this.damage = 5;
            this.speed = 10;
        }

        /// <summary>
        /// The update function.
        /// </summary>
        private void Update()
        {
            this.transform.position += this.direction * (Time.deltaTime * this.speed);
        }

        /// <summary>
        /// The on trigger enter function.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        private void OnTriggerEnter(Collider other)
        {
            if (!this.isServer) 
            {
                return;
            }

            NetworkPlayer p = other.gameObject.GetComponent<NetworkPlayer>();

            if (p)
            {
                p.TakeDamage(this.damage);
            }

            Destroy(this.gameObject);
        }
    }
}
