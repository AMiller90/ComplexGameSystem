using UnityEngine;
using System.Collections;
using UnityEditor;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform muzzleTransform;

    private float speed = 5;

	// Use this for initialization
	void Start ()
	{
    }
	
	// Update is called once per frame
	void Update ()
	{
        CheckBounds();
    
        Move();

	    Shoot();
	}

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.up * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.left * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.position += Vector3.down * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void Shoot()
    {
        Vector3 displacement = (Input.mousePosition - transform.position);
        displacement.Normalize();

        Debug.DrawLine(muzzleTransform.position, Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject gbullet = Instantiate(bullet, muzzleTransform.position, muzzleTransform.rotation) as GameObject;
            gbullet.GetComponent<Bullet>().Speed = 10;
            gbullet.GetComponent<Bullet>().Direction = displacement;
        }
       
    }

    private void CheckBounds()
    {
        if (transform.position.x <= -18.5)
            transform.position = new Vector3(-18.5f,transform.position.y,transform.position.z);
        if (transform.position.x >= 18.5)
            transform.position = new Vector3(18.5f, transform.position.y, transform.position.z);
        if (transform.position.y <= 1.5f)
            transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        if (transform.position.y >= 28.0f)
            transform.position = new Vector3(transform.position.x, 28.0f, transform.position.z);
    }
}
