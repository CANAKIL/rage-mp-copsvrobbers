using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerSide
{
    public class Main : Script
    {
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
                robberCount = 0;
                copCount = 0;
            }
            if (copCount > 0 && robberCount > 0) 
            {
                copCount = 0;
                robberCount = 0;
                return 1;
            }
            else return 0;
        }

        private bool chaseStarted = false;
        public void StartChase(Player player)
        {
            string side = player.GetData<string>(pickedSide);
            if (side == "Cops")
            {
                Vehicle veh = NAPI.Vehicle.CreateVehicle(VehicleHash.Police2, new Vector3(-1141.0807, -851.121, 13.528971), 39.3838f, 131, 131);
                player.Position = new Vector3(-1136.67, -856.95844, 13.252587);
                NAPI.Task.Run(() =>
                {
                    player.SetIntoVehicle(veh, (int)VehicleSeat.Driver);
                }, 500);
            }
            Vehicle _veh = NAPI.Vehicle.CreateVehicle(VehicleHash.Kuruma, new Vector3(-1148.4805, -842.65814, 14.196873), 39.3838f, 131, 131);
            /* else if (side == "Robbers")
            {
                Vehicle veh = NAPI.Vehicle.CreateVehicle(VehicleHash.Police2, new Vector3(x: -1148.4805, y: -842.65814, z: 14.196873), 39.3838f, 131, 131);
                player.Position = new Vector3(-1136.67, -856.95844, 13.252587);
                NAPI.Player.SetPlayerIntoVehicle(player, veh, 0);
            } */
            NAPI.Task.Run(() =>
            {
                player.TriggerEvent("toggleDisableCarControls");
            }, 200);

            // Countdown until chase starts
            NAPI.Task.Run(() =>
            {
                chaseStarted = true;
                NAPI.Notification.SendNotificationToPlayer(player, "Go!", true);
            }, 5000);
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

        [Command("pos")]
        public void debugPosition(Player player)
        {
            Vector3 playerPos = player.Position;
            NAPI.Util.ConsoleOutput("X: " + playerPos.X + ", Y: " + playerPos.Y + ", Z: " + playerPos.Z + ", rot: " + player.Vehicle.Rotation);
            NAPI.Notification.SendNotificationToPlayer(player, "Printed pos to server console.", true);
        }
    }
}
