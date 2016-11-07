using UnityEngine;
using System.Collections;
using UnityEditor;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private Transform muzzleTransform;

    private bool canJump;

    public bool CanJump
    {
        get { return canJump; }
        set { canJump = value; }
    }

    private Rigidbody playerBody;

    private float health;
    private float speed;

	// Use this for initialization
	void Start ()
	{
	    health = 100;
	    speed = 5;

	    if (playerBody == null)
	        playerBody = GetComponent<Rigidbody>();

        if (muzzleTransform == null)
            muzzleTransform = GetComponentsInChildren<Transform>()[1];
    }
	
	// Update is called once per frame
	void Update ()
	{ 
        Move();
	    Jump();
	    Shoot();

	}

    private void Move()
    {//Move to the left
        if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.left * speed * Time.deltaTime;
        //Move to the right
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * speed * Time.deltaTime;

    }

    private void Shoot()
    {
        //Get mouse position
        Vector3 mousePosition = Input.mousePosition;
        //Set the z position to offset with the camera
        mousePosition.z = Camera.main.transform.position.z * -1;

        //Get the displacement between mouse an muzzle position
        Vector3 displacement = Camera.main.ScreenToWorldPoint(mousePosition) - muzzleTransform.position;
        //Get the direction
        Vector3 bulletdirection = displacement.normalized;

        //Calculate the rotation on Z
        float rotZ = Mathf.Atan2(bulletdirection.x, bulletdirection.y)*Mathf.Rad2Deg;

        //Rotate the z axis as the mouse moves
        transform.rotation = Quaternion.Euler(0f, 0f, -rotZ);

        Debug.DrawLine(muzzleTransform.position, Camera.main.ScreenToWorldPoint(mousePosition));

        //If left click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {//Instantiate a bullet
            GameObject gbullet = Instantiate(bullet, muzzleTransform.position, muzzleTransform.rotation) as GameObject;

            //Add the bullet component if it doesnt have one
            if (!gbullet.GetComponent<Bullet>())
                gbullet.AddComponent<Bullet>();


            //Give it a direction
            gbullet.GetComponent<Bullet>().Direction = bulletdirection;
            //Have the bullet point in the direction it is shooting 
            gbullet.transform.rotation = Quaternion.Euler(0f,0f,rotZ);
            
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
           canJump = false;
           playerBody.velocity = new Vector3(playerBody.velocity.x, 10, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Bullet>())
            Debug.Log("Boop");
    }
}
