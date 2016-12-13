
namespace Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.Networking;

    /// <summary>
    /// The commands.
    /// </summary>
    public class Commands : NetworkBehaviour
    {
        /// <summary>
        /// The p max health.
        /// </summary>
        [SyncVar]
        private readonly float pmaxhealth = 100;

        /// <summary>
        /// The sync arm rotation.
        /// </summary>
        [SyncVar]
        private Quaternion syncarmRotation;

        /// <summary>
        /// The flip vector.
        /// </summary>
        [SyncVar]
        private Vector3 flipVector = new Vector3(1, 1, 1);

        /// <summary>
        /// The p name.
        /// </summary>
        [SyncVar]
        private string pname = string.Empty;

        /// <summary>
        /// The p health.
        /// </summary>
        [SyncVar]
        private float phealth = 100;

        /// <summary>
        /// The command change name function.
        /// </summary>
        /// <param name="newName">
        /// The new name.
        /// </param>
        [Command]
        public void CmdChangeName(string newName)
        {
            this.pname = newName;
        }

        /// <summary>
        /// The command health update function.
        /// </summary>
        /// <param name="newHealth">
        /// The new health.
        /// </param>
        [Command]
        public void CmdHealthUpdate(float newHealth)
        {
            this.phealth = newHealth;
        }

        /// <summary>
        /// The command arm rotation function.
        /// </summary>
        /// <param name="rot">
        /// The rot.
        /// </param>
        [Command]
        public void CmdArmRotation(Quaternion rot)
        {
            this.syncarmRotation = rot;
        }

        /// <summary>
        /// The command flip body function.
        /// </summary>
        /// <param name="flip">
        /// The flip.
        /// </param>
        [Command]
        public void CmdFlipBody(Vector3 flip)
        {
            this.flipVector = flip;
        }

        /// <summary>
        /// The command spawn bullet function.
        /// </summary>
        /// <param name="bull">
        /// The bull.
        /// </param>
        [Command]
        public void CmdSpawnBullet(GameObject bull)
        {
            NetworkServer.Spawn(bull);
        }

        /// <summary>
        /// The command shoot function.
        /// </summary>
        /// <param name="bulletDirection">
        /// The bullet direction.
        /// </param>
        [Command]
        public void CmdShoot(Vector3 bulletDirection)
        {
            GameObject gbullet = Instantiate(this.GetComponent<NetworkPlayer>().Bullet, this.GetComponentsInChildren<Transform>()[4].position, this.GetComponentsInChildren<Transform>()[4].rotation) as GameObject;

            if (gbullet != null && !gbullet.GetComponent<Bullet>())
            {
                gbullet.AddComponent<Bullet>();
            }

            if (gbullet != null)
            {
                gbullet.GetComponent<Bullet>().Direction = bulletDirection;

                NetworkServer.Spawn(gbullet);
            }
        }

        /// <summary>
        /// The start.
        /// </summary>
        private void Start()
        {
            if (this.isServer)
            {
                NetworkGameManager.AddToList(this.gameObject.GetComponent<NetworkPlayer>());
            }

            if (this.isLocalPlayer)
            {
                this.GetComponent<NetworkAnimator>().SetParameterAutoSend(0,true);
                this.syncarmRotation = Quaternion.identity;
            }

        }

        /// <summary>
        /// The update.
        /// </summary>
        private void Update()
        {
            if (this.gameObject == null || PauseMenu.IsOn)
            {
                return;
            }

            this.GetComponentsInChildren<TextMesh>()[0].text = this.pname;

            this.GetComponentsInChildren<TextMesh>()[1].text = this.phealth + "/" + this.pmaxhealth;
        
            if (!this.isLocalPlayer)
            {
                this.GetComponentsInChildren<Transform>()[1].localScale = this.flipVector;
                this.GetComponentsInChildren<Transform>()[2].rotation = this.syncarmRotation;
            }
       
        }

        /// <summary>
        /// The on gui.
        /// </summary>
        private void OnGUI()
        {
            if (this.isLocalPlayer)
            {
                GUI.Box(new Rect(10, Screen.height - 100, 80, 30), "Enter Name");
                this.pname = GUI.TextField(new Rect(10, Screen.height - 70, 80, 30), this.pname);

                if (!this.isServer)
                {
                    if (GUI.Button(new Rect(10, Screen.height - 40, 80, 30), "Change"))
                    {
                        this.CmdChangeName(this.pname);
                    }
                }

            }
        }
    }
}