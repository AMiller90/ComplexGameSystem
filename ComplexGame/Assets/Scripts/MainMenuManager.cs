
namespace Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// The main menu manager.
    /// </summary>
    public class MainMenuManager : MonoBehaviour
    {
        /// <summary>
        /// The play button.
        /// </summary>
        [SerializeField]
        private Button playbutton;

        /// <summary>
        /// The instructions button.
        /// </summary>
        [SerializeField]
        private Button instructionsbutton;

        /// <summary>
        /// The exit button.
        /// </summary>
        [SerializeField]
        private Button exitbutton;

        /// <summary>
        /// The instructionpanel.
        /// </summary>
        [SerializeField]
        private GameObject instructionpanel;

        /// <summary>
        /// The xbutton.
        /// </summary>
        [SerializeField]
        private Button xbutton;

        /// <summary>
        /// The play function.
        /// </summary>
        public void Play()
        {
            SceneManager.LoadScene(1);
        }

        /// <summary>
        /// The instructions.
        /// </summary>
        public void Instructions()
        {
            this.instructionpanel.SetActive(true);

        }

        /// <summary>
        /// The exit.
        /// </summary>
        public void Exit()
        {
            Application.Quit();
        }

        /// <summary>
        /// The close instructions menu.
        /// </summary>
        public void CloseInstructionsMenu()
        {
            this.instructionpanel.SetActive(false);
        }
    }
}
