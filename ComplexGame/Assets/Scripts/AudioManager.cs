
namespace Assets.Scripts
{
    using UnityEngine;

    /// <summary>
    /// The audio manager.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static AudioManager instance;

        /// <summary>
        /// The volume.
        /// </summary>
        private static float volume = 1;

        /// <summary>
        /// The source.
        /// </summary>
        private AudioSource source;

        /// <summary>
        /// Gets the self.
        /// </summary>
        public static AudioManager Self
        {
            get
            {
                if (instance == null)
                {
                    instance = new AudioManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        public static float Volume
        {
            get
            {
                return volume;
            }

            set
            {
                volume = value;
            }
        }

        /// <summary>
        /// Gets the source.
        /// </summary>
        public AudioSource Source
        {
            get
            {
                return this.source;
            }
        }

        /// <summary>
        /// The play shot sound.
        /// </summary>
        public void PlayShotSound()
        {
            this.source.Play();
        }

        /// <summary>
        /// The start.
        /// </summary>
        private void Start()
        {
            instance = this;
            this.source = this.GetComponent<AudioSource>();
        }
    }
}
