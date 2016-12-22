
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
        /// The shoot clip.
        /// </summary>
        public AudioClip[] ShootClip;

        /// <summary>
        /// The instance.
        /// </summary>
        private static NetworkAudioManager instance;

        /// <summary>
        /// The source.
        /// </summary>
        private AudioSource source;

        /// <summary>
        /// Gets the self.
        /// </summary>
        public static NetworkAudioManager Self
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// The function to send clip to server.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        [Command]
        public void CmdSendClipToServer(int id)
        {
            this.RpcSendSoundToClients(id);
        }

        /// <summary>
        /// The start.
        /// </summary>
        private void Start()
        {
            instance = this;
            this.source = this.GetComponent<AudioSource>();
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
