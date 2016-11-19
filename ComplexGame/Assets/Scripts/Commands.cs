using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;

//public class SyncListPlayers : SyncList<GameObject>
//{
//    protected override void SerializeItem(NetworkWriter writer, GameObject item)
//    {
//        writer.Write(item);
//    }

//    protected override GameObject DeserializeItem(NetworkReader reader)
//    {
//        return reader.ReadGameObject();
//    }
//}

public class Commands : NetworkBehaviour
{
    //public SyncListPlayers playersInGame = new SyncListPlayers();

    [SyncVar]
    private Quaternion syncarmRotation;

    [SyncVar]
    private Vector3 flipVector = new Vector3(1, 1, 1);

    [SyncVar]
    private string pname = "Player";

    [SyncVar]
    private float phealth = 100;

    [SyncVar]
    private float pmaxhealth = 100;

    [Command]
    public void CmdChangeName(string newName)
    {
        pname = newName;

    }

    [Command]
    public void CmdHealthUpdate(float newHealth)
    {
        phealth = newHealth;
    }

    [Command]
    public void CmdArmRotation(Quaternion rot)
    {
        syncarmRotation = rot;
    }

    [Command]
    public void CmdFlipBody(Vector3 flip)
    {
        flipVector = flip;
    }

    [Command]
    public void CmdSpawnBullet(GameObject bull)
    {
        NetworkServer.Spawn(bull);
    }

    [Command]
    public void CmdShoot(Vector3 bulletDirection)
    {
        //Instantiate a bullet
        GameObject gbullet = Instantiate(GetComponent<NetworkPlayer>().Bullet, GetComponentsInChildren<Transform>()[4].position, GetComponentsInChildren<Transform>()[4].rotation) as GameObject;

        //Add the bullet component if it doesnt have one
        if (!gbullet.GetComponent<Bullet>())
            gbullet.AddComponent<Bullet>();

        //Give it a direction
        gbullet.GetComponent<Bullet>().Direction = bulletDirection;

        NetworkServer.Spawn(gbullet);
    }

    void Start()
    {
        //playersInGame.Add(gameObject);
        //CmdPlayerCount(playersInGame.Count);

        NetworkGameManager.AddToList(gameObject.GetComponent<NetworkPlayer>());

        if (isLocalPlayer)
        {
            GetComponent<NetworkAnimator>().SetParameterAutoSend(0,true);
            syncarmRotation = Quaternion.identity;
        }
    }

    void Update()
    {
        if (gameObject == null)
            return;

        this.GetComponentsInChildren<TextMesh>()[0].text = pname;
        this.GetComponentsInChildren<TextMesh>()[1].text = phealth + "/" + pmaxhealth;

        if (!isLocalPlayer)
        {
            this.GetComponentsInChildren<Transform>()[1].localScale = flipVector;
            this.GetComponentsInChildren<Transform>()[2].rotation = syncarmRotation;
        }
       
    }

    void OnGUI()
    {
        if (isLocalPlayer)
        {
            GUI.Box(new Rect(10, Screen.height - 100, 80, 30), "Enter Name");
            pname = GUI.TextField(new Rect(10, Screen.height - 70, 80, 30), pname);
            this.GetComponentsInChildren<TextMesh>()[0].text = pname;
            if (GUI.Button(new Rect(10, Screen.height - 40, 80, 30), "Change"))
            {
                CmdChangeName(pname);
            }
        }
    }
}