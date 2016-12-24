
namespace Assets.Scripts
{
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.Networking.Match;
    using UnityEngine.UI;

    /// <summary>
    /// The join game.
    /// </summary>
    public class JoinGame : MonoBehaviour
    {
        /// <summary>
        /// The room list.
        /// </summary>
        private readonly List<GameObject> roomList = new List<GameObject>();

        /// <summary>
        /// The status text variable.
        /// </summary>
        [SerializeField]
        private Text status;

        /// <summary>
        /// The room list item prefab.
        /// </summary>
        [SerializeField]
        private GameObject roomListItemPrefab;

        /// <summary>
        /// The room list parent.
        /// </summary>
        [SerializeField]
        private Transform roomListParent;

        /// <summary>
        /// The network manager.
        /// </summary>
        private NetworkManager networkManager;

        /// <summary>
        /// The refresh room list.
        /// </summary>
        public void RefreshRoomList()
        {
            AudioManager.Self.PlayShotSound();
            this.ClearRoomList();
            this.networkManager.matchMaker.ListMatches(0, 20, string.Empty, true, 0, 0, this.OnMatchList);
            this.status.text = "Loading...";
        }

        /// <summary>
        /// The on match list unity function.
        /// </summary>
        /// <param name="success">
        /// The success factor.
        /// </param>
        /// <param name="extendedInfo">
        /// The extended info string.
        /// </param>
        /// <param name="matches">
        /// The list of matches.
        /// </param>
        public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
        {
            this.status.text = string.Empty;

            if (!success || matches == null)
            {
                this.status.text = "Couldn't get room list.";
                return;
            }

            foreach (MatchInfoSnapshot match in matches)
            {
                GameObject roomListItemGo = Instantiate(this.roomListItemPrefab);
                roomListItemGo.transform.SetParent(this.roomListParent);

                RoomListItem roomListItem = roomListItemGo.GetComponent<RoomListItem>();

                if (roomListItem != null)
                {
                    roomListItem.Setup(match, this.JoinRoom);
                }

                this.roomList.Add(roomListItemGo);

            }

            if (this.roomList.Count == 0)
            {
                this.status.text = "No Rooms At The Moment";
            }
        }

        /// <summary>
        /// The join room function.
        /// </summary>
        /// <param name="match">
        /// The match.
        /// </param>
        public void JoinRoom(MatchInfoSnapshot match)
        {
            AudioManager.Self.PlayShotSound();
            this.networkManager.matchMaker.JoinMatch(match.networkId, string.Empty, string.Empty, string.Empty, 0, 0, this.networkManager.OnMatchJoined);
            this.ClearRoomList();
            this.status.text = "Joining...";
        }

        /// <summary>
        /// The start function.
        /// </summary>
        private void Start()
        {
            this.networkManager = NetworkManager.singleton;

            if (this.networkManager.matchMaker == null)
            {
                this.networkManager.StartMatchMaker();
            }

            this.RefreshRoomList();
        }

        /// <summary>
        /// The clear room list function.
        /// </summary>
        private void ClearRoomList()
        {
            for (int i = 0; i < this.roomList.Count; i++)
            {
                Destroy(this.roomList[i]);
            }

            this.roomList.Clear();
        }
    }
}
