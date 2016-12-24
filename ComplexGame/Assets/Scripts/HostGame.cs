
namespace Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Networking;

    /// <summary>
    /// The host game function.
    /// </summary>
    public class HostGame : MonoBehaviour
    {
        /// <summary>
        /// The room size.
        /// </summary>
        private readonly uint roomSize = 4;

        /// <summary>
        /// The room name.
        /// </summary>
        private string roomName = "";

        /// <summary>
        /// The network manager.
        /// </summary>
        private NetworkManager networkManager;

        /// <summary>
        /// The set room name function.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public void SetRoomName(InputField name)
        {
            this.roomName = name.text;
        }

        /// <summary>
        /// The create room function.
        /// </summary>
        public void CreateRoom()
        {
            if (this.roomName != "" && RegisterPlayer.Self.PlayerName != "")
            {
                AudioManager.Self.PlayShotSound();
                this.networkManager.matchMaker.CreateMatch(this.roomName, this.roomSize, true, string.Empty, string.Empty, string.Empty, 0, 0, this.networkManager.OnMatchCreate);
            }
        }

        /// <summary>
        /// The start function.
        /// </summary>
        private void Start()
        {
            AudioManager.Self.Source.volume = AudioManager.Volume;
            this.networkManager = NetworkManager.singleton;

            if (this.networkManager.matchMaker == null)
            {
                this.networkManager.StartMatchMaker();
            }
        }
    }
}
