using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class NetworkGameManager
{
    private static List<NetworkPlayer> players = new List<NetworkPlayer>();

    public static void AddToList(NetworkPlayer player)
    {
        players.Add(player);
    }

    public static bool RemoveAndCheckForWin(NetworkPlayer player)
    {
        players.Remove(player);

        
        if (players.Count == 1)
        {
            string name = players[0].GetComponentsInChildren<TextMesh>()[0].text;
            players[0].GetComponent<Commands>().CmdChangeName(name + " Wins!");
            return true;
        }
        return false;
    }

    public static int PlayersGetCount()
    {
        return players.Count;
    }
}
