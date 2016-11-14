using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SetUpLocalPlayer : NetworkBehaviour
{
    [SyncVar]
    private Quaternion syncarmRotation;

    [SyncVar]
    private Vector3 flipVector = new Vector3(1, 1, 1);

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
            this.GetComponentsInChildren<TextMesh>()[0].text = pname;
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

    [Command]
    public void CmdSpawnBullet(GameObject bull)
    {
        NetworkServer.Spawn(bull);
    }

    // Use this for initialization
    void Start()
    {
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

    public override void PreStartClient()
    {
        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
    }

}
