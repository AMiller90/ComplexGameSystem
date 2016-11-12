﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SetUpLocalPlayer : NetworkBehaviour
{
    [SyncVar]
    private Quaternion syncarmRotation;

    [SyncVar]
    private Vector3 flipVector;

    [SyncVar]
    private string pname = "player";

    [SyncVar]
    private float phealth = 100;
    [SyncVar]
    private float pmaxhealth = 100;

    void OnGUI()
    {
        if (isLocalPlayer)
        {
            GUI.Box(new Rect(10, Screen.height - 100, 80, 30), "Enter Name");
            pname = GUI.TextField(new Rect(10, Screen.height - 70, 80, 30), pname);
            if (GUI.Button(new Rect(10, Screen.height - 40, 80, 30), "Change"))
            {
                CmdChangeName(pname);
            }
        }
    }

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

    void Awake()
    {
        flipVector = new Vector3(1, 1, 1);
    }
    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            GetComponent<Player>().enabled = true;
            GetComponent<NetworkAnimator>().SetParameterAutoSend(0,true);
            syncarmRotation = Quaternion.identity;
        }
           
    }

    void Update()
    {
        this.GetComponentsInChildren<TextMesh>()[0].text = pname;
        this.GetComponentsInChildren<TextMesh>()[1].text = phealth + "/" + pmaxhealth;
        this.GetComponentsInChildren<Transform>()[1].localScale = flipVector;

        if (!isLocalPlayer)
            this.GetComponentsInChildren<Transform>()[2].rotation = syncarmRotation;
    }

    public override void PreStartClient()
    {
        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (!GetComponent<Player>().enabled && other.GetComponent<Bullet>())
    //    {
    //        Debug.Log("Someone else shot you!");
    //        //health -= other.GetComponent<Bullet>().Damage;
    //        //GetComponent<SetUpLocalPlayer>().CmdHealthUpdate(health);
    //        ////////if(health <= 0)
    //        ////////    Destroy(gameObject);
    //    }

    //}

}
