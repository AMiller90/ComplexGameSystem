using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<NetworkPlayer>())
            other.gameObject.GetComponent<NetworkPlayer>().JumpProp = true;
    }
}
