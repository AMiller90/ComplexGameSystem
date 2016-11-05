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
        BulletLifeTime();
	}

    private void BulletLifeTime()
    {
        if (transform.position.x <= -18.5 || transform.position.x >= 18.5)
            Destroy(gameObject);
        if (transform.position.y <= 1.5f || transform.position.y >= 27.5f)
            Destroy(gameObject);
    }
}
