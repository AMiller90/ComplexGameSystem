using UnityEngine;

public class Border : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Bullet>())
            Destroy(other.gameObject);
    }
}
