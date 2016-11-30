using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class RoomListItem : MonoBehaviour {

    //Delegate for storing functions
    public delegate void JoinRoomDelegate(MatchInfoSnapshot match);

    //Create instance of the delegate
    private JoinRoomDelegate joinRoomCallback;

    [SerializeField]
    private Text RoomNameText;

    //Instance of a match
    private MatchInfoSnapshot match;

    //Setup the match
    public void Setup(MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallback)
    {//Set the match
        match = _match;
        //Set the call back funciton
        joinRoomCallback = _joinRoomCallback;
        //Set the text of the join room button
        RoomNameText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
    }

    public void JoinRoom()
    {
        //Call the function
        joinRoomCallback.Invoke(match);
    }
}
