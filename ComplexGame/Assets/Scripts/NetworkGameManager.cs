
namespace Assets.Scripts
{
    using System.Collections.Generic;

    using UnityEngine;

    /// <summary>
    /// The network game manager.
    /// </summary>
    public sealed class NetworkGameManager
    {
        /// <summary>
        /// The players.
        /// </summary>
        private static readonly List<NetworkPlayer> Players = new List<NetworkPlayer>();

        /// <summary>
        /// The add to list function.
        /// </summary>
        /// <param name="player">
        /// The player.
        /// </param>
        public static void AddToList(NetworkPlayer player)
        {
            Players.Add(player);
        }

        /// <summary>
        /// The remove and check for win function.
        /// </summary>
        /// <param name="player">
        /// The player.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool RemoveAndCheckForWin(NetworkPlayer player)
        {
            player.GetComponent<Commands>().CmdChangeName(player.GetComponentsInChildren<TextMesh>()[0].text + " Loses!");
            Players.Remove(player);

            if (Players.Count == 1)
            {
                string name = Players[0].GetComponentsInChildren<TextMesh>()[0].text;
                Players[0].GetComponent<Commands>().CmdChangeName(name + " Wins!");
                Players.Clear();
                return true;
            }
            return false;
        }

        /// <summary>
        /// The players get count function.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int PlayersGetCount()
        {
            return Players.Count;
        }
    }
}
