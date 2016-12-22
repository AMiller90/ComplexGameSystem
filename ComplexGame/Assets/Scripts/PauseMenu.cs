
namespace Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.Networking.Match;

    /// <summary>
    /// The pause menu.
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        /// <summary>
        /// The is on.
        /// </summary>
        private static bool isOn;

        /// <summary>
        /// The instance.
        /// </summary>
        private static PauseMenu instance;

        /// <summary>
        /// The network manager.
        /// </summary>
        private NetworkManager networkManager;

        /// <summary>
        /// The pause menu.
        /// </summary>
        [SerializeField]
        private GameObject pauseMenu;

        /// <summary>
        /// Gets or sets a value indicating whether is on.
        /// </summary>
        public static bool IsOn
        {
            get
            {
                return isOn;
            }

            set
            {
                isOn = value;
            }
        }

        /// <summary>
        /// Gets the self.
        /// </summary>
        public static PauseMenu Self
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// The toggle pause menu.
        /// </summary>
        public void TogglePauseMenu()
        {
            this.pauseMenu.SetActive(!this.pauseMenu.activeSelf);
            isOn = this.pauseMenu.activeSelf;
        }

        /// <summary>
        /// The leave room.
        /// </summary>
        public void LeaveRoom()
        {
            MatchInfo matchInfo = this.networkManager.matchInfo;
            this.networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, this.networkManager.OnDropConnection);
            this.networkManager.StopHost();
        }

        /// <summary>
        /// The start function.
        /// </summary>
        private void Start()
        {
            isOn = false;
            instance = this;
            this.networkManager = NetworkManager.singleton;
        }
    }
}
