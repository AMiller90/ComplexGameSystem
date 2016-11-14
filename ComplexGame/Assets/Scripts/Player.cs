﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    private bool facingRight;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform armTransform;
    [SerializeField]
    private Transform bodyTransform;
    [SerializeField]
    private Transform firePointTransform;


    [SerializeField]
    private Animator anim;

    private bool canJump;

    public bool CanJump
    {
        get { return canJump; }
        set { canJump = value; }
    }

    private Rigidbody playerBody;

    private float health;
    private float maxHealth;
    private float speed;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
    }

    void Awake()
    {
        if (playerBody == null)
            playerBody = GetComponent<Rigidbody>();

        if (armTransform == null)
            armTransform = GetComponentsInChildren<Transform>()[2];

        if (bodyTransform == null)
            bodyTransform = GetComponentsInChildren<Transform>()[1];

        if (firePointTransform == null)
            firePointTransform = GetComponentsInChildren<Transform>()[4];

        if (anim == null)
            anim = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start ()
	{
	    maxHealth = 100;
	    health = maxHealth;

        speed = 0;
        canJump = true;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (!isLocalPlayer)
	        return;

	    if (gameObject == null)
	        return;

        Move();
	    Jump();
	    Shoot();
	}

    private void Move()
    {
        anim.SetFloat("Speed", 0);


        if (Input.GetKeyDown(KeyCode.A) && !facingRight)
        {
            GetComponent<SetUpLocalPlayer>().CmdFlipBody(Flip());
        }

        if (Input.GetKeyDown(KeyCode.D) && facingRight)
        {
            GetComponent<SetUpLocalPlayer>().CmdFlipBody(Flip());
        }
        //Move to the left
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetFloat("Speed", 5);
            transform.position += Vector3.left * 5 * Time.deltaTime;
        }
            
        //Move to the right
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetFloat("Speed", 5);
            transform.position += Vector3.right * 5 * Time.deltaTime;
        }
    }

    private void Shoot()
    {
        //Get mouse position
        Vector3 mousePosition = Input.mousePosition;
        //Set the z position to offset with the camera
        mousePosition.z = Camera.main.transform.position.z * -1;

        //Get the displacement between mouse an muzzle position
        Vector3 displacement = Camera.main.ScreenToWorldPoint(mousePosition) - armTransform.position;
        //Get the direction
        Vector3 bulletdirection = displacement.normalized;

        //Calculate the rotation on Z
        float rotZ = Mathf.Atan2(bulletdirection.x, bulletdirection.y)*Mathf.Rad2Deg;

        //Rotate the z axis as the mouse moves
        armTransform.rotation = Quaternion.Euler(0f, 0f, -rotZ + 90);
        GetComponent<SetUpLocalPlayer>().CmdArmRotation(Quaternion.Euler(0f, 0f, -rotZ + 90));

        Debug.DrawLine(armTransform.position, Camera.main.ScreenToWorldPoint(mousePosition));

        //If left click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {//Instantiate a bullet
            GameObject gbullet = Instantiate(bullet, firePointTransform.position, firePointTransform.rotation) as GameObject;

            //Add the bullet component if it doesnt have one
            if (!gbullet.GetComponent<Bullet>())
                gbullet.AddComponent<Bullet>();

            //Give it a direction
            gbullet.GetComponent<Bullet>().Direction = bulletdirection;
            //Have the bullet point in the direction it is shooting 
            gbullet.transform.rotation = Quaternion.Euler(0f,0f,rotZ);

            GetComponent<SetUpLocalPlayer>().CmdSpawnBullet(gbullet);
        }
    }

    private void Jump()
    {
        anim.SetBool("IsGrounded", canJump);
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
           canJump = false;
           playerBody.velocity = new Vector3(playerBody.velocity.x, 10, 0);
        }
    }

    private Vector3 Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = bodyTransform.localScale;
        theScale.x *= -1;
        return bodyTransform.localScale = theScale;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        GetComponent<SetUpLocalPlayer>().CmdHealthUpdate(health);

        if(health <= 0)
            Destroy(gameObject);
    }
}
