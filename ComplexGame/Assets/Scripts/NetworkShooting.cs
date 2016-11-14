using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkShooting : NetworkBehaviour {

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform armTransform;

    [SerializeField]
    private Transform firePointTransform;

	void Update ()
    {
        if (!isLocalPlayer)
            return;

            Shoot();
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
        float rotZ = Mathf.Atan2(bulletdirection.x, bulletdirection.y) * Mathf.Rad2Deg;

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
            gbullet.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

            GetComponent<SetUpLocalPlayer>().CmdSpawnBullet(gbullet);
        }
    }
}
