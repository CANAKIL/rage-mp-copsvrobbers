using GTANetworkAPI;

namespace ServerSide
{
    public class Commands : Script
    {
        [Command("vspawn")]
        public void spawnVeh(Player player, string model)
        {
            VehicleHash vhash = NAPI.Util.VehicleNameToModel(model);
            NAPI.Vehicle.CreateVehicle(vhash, player.Position, player.Heading, 131, 131);
        }
    }
}
