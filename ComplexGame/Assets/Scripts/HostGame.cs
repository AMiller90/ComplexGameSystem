using System;

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class HostGame : MonoBehaviour{

    private uint roomSize = 4;

    private string roomName = string.Empty;

    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton;

        if (networkManager.matchMaker == null)
            networkManager.StartMatchMaker();
    }

    public void SetRoomName(string name)
    {
        roomName = name;
    } 

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(this.roomName))
        {
            Debug.Log("Creating a room: " + roomName + " with room for " + roomSize + " players.");
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, string.Empty, string.Empty,string.Empty, 0, 0, networkManager.OnMatchCreate);
        }
            
    }
}
