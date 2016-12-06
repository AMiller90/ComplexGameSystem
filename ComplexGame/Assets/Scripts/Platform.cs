
namespace Assets.Scripts
{
    using UnityEngine;

    /// <summary>
    /// The platform.
    /// </summary>
    public class Platform : MonoBehaviour
    {
        /// <summary>
        /// The on collision enter function.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<NetworkPlayer>())
            {
                other.gameObject.GetComponent<NetworkPlayer>().JumpProp = true;
            }
        }
    }
}
