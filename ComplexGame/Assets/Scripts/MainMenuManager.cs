
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
        /// The options button.
        /// </summary>
        [SerializeField]
        private Button optionsbutton;

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
        /// The instructionpanel.
        /// </summary>
        [SerializeField]
        private GameObject optionpanel;

        /// <summary>
        /// The volume slider.
        /// </summary>
        [SerializeField]
        private Slider volumeslider;

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
            AudioManager.Self.PlayShotSound();
            SceneManager.LoadScene(1);
        }

        /// <summary>
        /// The instructions.
        /// </summary>
        public void Instructions()
        {
            AudioManager.Self.PlayShotSound();
            this.instructionpanel.SetActive(true);
        }

        /// <summary>
        /// The options.
        /// </summary>
        public void Options()
        {
            AudioManager.Self.PlayShotSound();
            this.optionpanel.SetActive(true);
        }

        /// <summary>
        /// The exit.
        /// </summary>
        public void Exit()
        {
            AudioManager.Self.PlayShotSound();
            Application.Quit();
        }

        /// <summary>
        /// The close instructions menu.
        /// </summary>
        public void CloseInstructionsMenu()
        {
            AudioManager.Self.PlayShotSound();
            this.instructionpanel.SetActive(false);
        }

        /// <summary>
        /// The close options menu.
        /// </summary>
        public void CloseOptionsMenu()
        {
            AudioManager.Self.PlayShotSound();
            this.optionpanel.SetActive(false);
        }

        /// <summary>
        /// The volume slider.
        /// </summary>
        public void VolumeSlider()
        {
            AudioManager.Self.Source.volume = this.volumeslider.value;
            AudioManager.Volume = this.volumeslider.value;
        }
    }
}
