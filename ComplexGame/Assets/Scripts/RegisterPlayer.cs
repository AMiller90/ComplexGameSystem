
namespace Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// The register player.
    /// </summary>
    public class RegisterPlayer : MonoBehaviour
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static RegisterPlayer instance;

        /// <summary>
        /// The player name.
        /// </summary>
        private string playername;

        /// <summary>
        /// Gets the self.
        /// </summary>
        public static RegisterPlayer Self
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Gets the player name.
        /// </summary>
        public string PlayerName
        {
            get
            {
                return playername;
            }
        }

        /// <summary>
        /// The register name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public void RegisterName(InputField name)
        {
            this.playername = name.text;
        }

        /// <summary>
        /// The start.
        /// </summary>
        private void Start()
        {
            instance = this;
        }

    }
}
