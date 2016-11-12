using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnpoints;

    // Use this for initialization
    void Start ()
    {
        spawnpoints = new List<Transform>();

        for (int i = 1; i < 5; i++)
        {
            spawnpoints.Add(gameObject.GetComponentsInChildren<Transform>()[i]);
        }

        foreach (Transform t in spawnpoints)
        {
            t.gameObject.gameObject.AddComponent<NetworkStartPosition>();
        }
    }
}
