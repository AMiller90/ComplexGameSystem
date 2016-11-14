using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Player>())
            other.gameObject.GetComponent<Player>().CanJump = true;
    }
}
