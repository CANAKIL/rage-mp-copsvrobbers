using GTANetworkAPI;
using System;
using System.Collections.Generic;

namespace ServerSide
{
    public class Main : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            NAPI.Util.ConsoleOutput("Server started!");
        }
        List<Player> players = null;
        public int checkPlayerCount()
        {
            int playerCount;
            players = NAPI.Pools.GetAllPlayers();
            playerCount = players.Count;

            if (playerCount < 0)
            {
                NAPI.Notification.SendNotificationToAll("There is not enough people to start the game yet. Feel free to spawn a vehicle from F4 and wait for more players.", true);
                return 0;
            }
            else return 1;
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnect(Player player)
        {
            if (checkPlayerCount() > 0)
            {
                foreach (Player p in players)
                {
                    NAPI.Notification.SendNotificationToAll("Pick your desired side! 15 seconds left.", true);
                    p.TriggerEvent("startRaceProcedure", player);
                }
            }
        }
        
        private static string pickedSide = nameof(pickedSide);

        private int copCount = 0;
        private int robberCount = 0;
        public int validateEnoughPlayers()
        {
            List<Player> list = NAPI.Pools.GetAllPlayers();
            foreach (Player p in list)
            {
                string pSide = p.GetData<string>(pickedSide);
                if (pSide == "Cops") copCount++;
                else robberCount++;
            }
            if (copCount > 0 && robberCount > 0) return 1;
            else return 0;
        }

        public void StartChase(Player player)
        {
            string side = player.GetData<string>(pickedSide);
            // Spawn a car each for every player that picked a side on the chase.
            // Put the players in those vehicles.
            player.TriggerEvent("toggleFreezeClient");
            // Countdown until chase starts
            if (side == "Robber")
            {
                // Start after 5 seconds
            }
            else
            {
                // Start after 10 seconds
            }
        }
        [RemoteEvent("pickedSide")]
        public void PickedSide(Player player, string side)
        {
            NAPI.Util.ConsoleOutput("Player selected a side: " + side);
            player.SetData(pickedSide, side);
            if (validateEnoughPlayers() == 1)
            {
                NAPI.Notification.SendNotificationToPlayer(player, "Not enough players. Restarting the procedure.", true);
            }
            else
            {
                StartChase(player);
            }
        }
    }
}
