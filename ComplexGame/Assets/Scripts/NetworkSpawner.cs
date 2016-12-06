
namespace Assets.Scripts
{
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.Networking;

    /// <summary>
    /// The network class used for spawn points.
    /// </summary>
    public class NetworkSpawner : MonoBehaviour
    {
        /// <summary>
        /// The spawn points list.
        /// </summary>
        [SerializeField]
        private List<Transform> spawnpoints;

        /// <summary>
        /// The awake.
        /// </summary>
        private void Awake()
        {
            this.spawnpoints = new List<Transform>();

            for (int i = 1; i < 5; i++)
            {
                this.spawnpoints.Add(this.gameObject.GetComponentsInChildren<Transform>()[i]);
            }

            foreach (Transform t in this.spawnpoints)
            {
                t.gameObject.gameObject.AddComponent<NetworkStartPosition>();
            }
        }
    }
}
