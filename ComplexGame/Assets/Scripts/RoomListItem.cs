
//namespace Assets.Scripts
//{
//    using UnityEngine;
//    using UnityEngine.Networking.Match;
//    using UnityEngine.UI;

//    /// <summary>
//    /// The room list item.
//    /// </summary>
//    public class RoomListItem : MonoBehaviour
//    {
//        /// <summary>
//        /// The join room callback instance of the delegate.
//        /// </summary>
//        private JoinRoomDelegate joinRoomCallback;

//        /// <summary>
//        /// The room name text.
//        /// </summary>
//        [SerializeField]
//        private Text roomNameText;

//        /// <summary>
//        /// Instance of a match.
//        /// </summary>
//        private MatchInfoSnapshot match;

//        /// <summary>
//        /// The join room delegate for storing functions.
//        /// </summary>
//        /// <param name="match">
//        /// The match.
//        /// </param>
//        public delegate void JoinRoomDelegate(MatchInfoSnapshot match);

//        /// <summary>
//        /// Setup the match.
//        /// </summary>
//        /// <param name="aMatch">
//        /// The _match.
//        /// </param>
//        /// <param name="aJoinRoomCallback">
//        /// The _join room callback.
//        /// </param>
//        public void Setup(MatchInfoSnapshot aMatch, JoinRoomDelegate aJoinRoomCallback)
//        {// Set the match
//            this.match = aMatch;

//            // Set the call back function
//            this.joinRoomCallback = aJoinRoomCallback;

//            // Set the text of the join room button
//            this.roomNameText.text = this.match.name + " (" + this.match.currentSize + "/" + this.match.maxSize + ")";
//        }

//        /// <summary>
//        /// The join room function.
//        /// </summary>
//        public void JoinRoom()
//        {
//            // Call the function
//            this.joinRoomCallback.Invoke(this.match);
//        }
//    }
//}



namespace Assets.Scripts
{
    using System.Runtime.CompilerServices;

    using UnityEngine;
    using UnityEngine.Networking.Match;
    using UnityEngine.UI;

    /// <summary>
    /// The room list item.
    /// </summary>
    public class RoomListItem : MonoBehaviour
    {
        /// <summary>
        /// The join room callback instance of the delegate.
        /// </summary>
        private JoinRoomDelegate joinRoomCallback;

        /// <summary>
        /// The room name text.
        /// </summary>
        [SerializeField]
        private Text roomNameText;

        /// <summary>
        /// The instance of a match.
        /// </summary>
        private MatchInfoSnapshot match;

        /// <summary>
        /// The join room delegate for storing functions.
        /// </summary>
        /// <param name="match">
        /// The match.
        /// </param>
        public delegate void JoinRoomDelegate(MatchInfoSnapshot match);

        /// <summary>
        /// Setup the match.
        /// </summary>
        /// <param name="aMatch">
        /// The _match to setup.
        /// </param>
        /// <param name="aJoinRoomCallback">
        /// The _join room callback to use.
        /// </param>
        public void Setup(MatchInfoSnapshot aMatch, JoinRoomDelegate aJoinRoomCallback)
        { // Set the match
            this.match = aMatch;

            // Set the call back function
            this.joinRoomCallback = aJoinRoomCallback;

            // Set the text of the join room button
            this.roomNameText.text = this.match.name + " (" + this.match.currentSize + "/" + this.match.maxSize + ")";
        }

        /// <summary>
        /// The join room.
        /// </summary>
        public void JoinRoom()
        {
            // Call the function
            this.joinRoomCallback.Invoke(this.match);
        }
    }
}