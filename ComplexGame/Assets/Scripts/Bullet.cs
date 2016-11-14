using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Bullet : NetworkBehaviour
{
    private float speed;
    private Vector3 direction;
    private float damage;

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

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    void Start()
    {
        damage = 5;
        speed = 10;
    }

	// Update is called once per frame
	void Update ()
	{
	    transform.position += direction * (Time.deltaTime *speed);
	}

    void OnTriggerEnter(Collider other)
    {
        Player p = other.gameObject.GetComponent<Player>();

        if (p)
        {
            p.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
