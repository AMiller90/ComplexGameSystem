
namespace Assets.Scripts
{
    using System.Runtime.InteropServices;

    using UnityEngine;
    using UnityEngine.Networking;

    /// <summary>
    /// The network audio manager.
    /// </summary>
    public class NetworkAudioManager : NetworkBehaviour
    {
        /// <summary>
        /// The source.
        /// </summary>
        private AudioSource source;

        /// <summary>
        /// The shoot clip.
        /// </summary>
        public AudioClip[] ShootClip;

        /// <summary>
        /// The start.
        /// </summary>
        private void Start()
        {
            this.source = this.GetComponent<AudioSource>();
        }

        /// <summary>
        /// The update.
        /// </summary>
        private void Update()
        {
            if (this.isLocalPlayer)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    this.CmdSendClipToServer(0);
                }
            }
        }

        /// <summary>
        /// The function to send clip to server.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        [Command]
        private void CmdSendClipToServer(int id)
        {
            this.RpcSendSoundToClients(id);
        }

        /// <summary>
        /// The function to send sound to clients.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        [ClientRpc]
        private void RpcSendSoundToClients(int id)
        {
            this.source.PlayOneShot(this.ShootClip[id]);
        }
    }
}
