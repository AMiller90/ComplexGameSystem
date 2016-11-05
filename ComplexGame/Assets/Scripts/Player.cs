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
	    if (muzzleTransform == null)
	        muzzleTransform = gameObject.GetComponentInChildren<Transform>();
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
        //Get mouse position
        Vector3 mousePosition = Input.mousePosition;
        //Set the z position to 25 to offset with the camera
        mousePosition.z = 25;

        //Get the displacement between mouse an muzzle position
        Vector3 displacement = Camera.main.ScreenToWorldPoint(mousePosition) - muzzleTransform.position;
        //Get the direction
        Vector3 bulletdirection = displacement.normalized;

        Debug.DrawLine(muzzleTransform.position, Camera.main.ScreenToWorldPoint(mousePosition));

        //If left click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {//Instantiate a bullet
            GameObject gbullet = Instantiate(bullet, muzzleTransform.position, muzzleTransform.rotation) as GameObject;
            //Give it a speed
            gbullet.GetComponent<Bullet>().Speed = 10;
            //Give it a direction
            gbullet.GetComponent<Bullet>().Direction = bulletdirection;
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
