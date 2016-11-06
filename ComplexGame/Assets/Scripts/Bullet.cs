using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private float speed;
    private Vector3 direction;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

	// Update is called once per frame
	void Update ()
	{
	    transform.position += direction * (Time.deltaTime *speed);
	}
}
