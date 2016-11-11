using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SetUpLocalPlayer : NetworkBehaviour {

    [SyncVar]
    private string name = "player";

    private Player player;
    private TextMesh NameMesh;
    private TextMesh HealthMesh;

    // Use this for initialization
    void Start ()
    {
        if (isLocalPlayer)
            GetComponent<Player>().enabled = true;

        player = GetComponent<Player>();

        NameMesh = GetComponentsInChildren<TextMesh>()[0];
        HealthMesh = GetComponentsInChildren<TextMesh>()[1];
    }

    // Update is called once per frame
    void Update ()
    {
        if (isLocalPlayer && player != null)
        {
            NameMesh.text = name;
            HealthMesh.text = player.Health + "/" + player.MaxHealth;
        }
	}

    void OnGUI()
    {
        if (isLocalPlayer)
        {
            GUI.Box(new Rect(25, Screen.height - 100, 100, 30), "Enter Name");

            name = GUI.TextField(new Rect(25, Screen.height - 70, 100, 30), name);
            
            if (GUI.Button(new Rect(25, Screen.height - 40, 100, 30), "Change"))
            {
                CmdChangeName(name);
            }
        }
        
    }

    [Command]
    public void CmdChangeName(string s)
    {
        name = s;
    }
}
