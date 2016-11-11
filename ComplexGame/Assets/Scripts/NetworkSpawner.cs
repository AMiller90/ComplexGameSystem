using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkSpawner : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        float X = Random.Range(-18, 18);
        float Y = Random.Range(2, 28);
        transform.position = new Vector3(X, Y, 0);
        gameObject.AddComponent<NetworkStartPosition>();
    }
}
