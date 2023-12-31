﻿using GTANetworkAPI;
using System.Diagnostics.Tracing;

namespace ServerSide
{
    public class Weather : Script
    {

        [RemoteEvent("sysChangeWeather")]
        public void changeWeather(Player player, string weather)
        {
            string weatherA = "CLEAR";
            if (weather == "Extrasunny")
            {
                weatherA = "EXTRASUNNY";
            }
            else if (weather == "Clear")
            {
                weatherA = "CLEAR";
            }
            else if (weather == "Clouds")
            {
                weatherA = "CLOUDS";
            }
            else if (weather == "Smog")
            {
                weatherA = "SMOG";
            }
            else if (weather == "Foggy")
            {
                weatherA = "FOGGY";
            }
            else if (weather == "Overcast")
            {
                weatherA = "OVERCAST";
            }
            else if (weather == "Rain")
            {
                weatherA = "RAIN";
            }
            else if (weather == "Thunder")
            {
                weatherA = "THUNDER";
            }
            else if (weather == "Clearing")
            {
                weatherA = "CLEARING";
            }
            else if (weather == "Neutral")
            {
                weatherA = "NEUTRAL";
            }
            else if (weather == "Snow")
            {
                weatherA = "SNOW";
            }
            else if (weather == "Blizzard")
            {
                weatherA = "BLIZZARD";
            }
            else if (weather == "Snowlight")
            {
                weatherA = "SNOWLIGHT";
            }
            else if (weather == "XMas")
            {
                weatherA = "XMAS";
            }
            else if (weather == "Halloween")
            {
                weatherA = "HALLOWEEN";
            }

            NAPI.World.SetWeather(weatherA);
        }

        [RemoteEvent("sysChangeTime")]
        public void changeTime(Player player, string time)
        {
            int settime = 0;
            if (time == "day")
            {
                settime = 12;
            }
            else if (time == "Night")
            {
                settime = 0;
            }
            else if (time == "Sunrise")
            {
                settime = 6;
            }
            else if (time == "Sunset")
            {
                settime = 21;
            }
            else
            {
                settime = 12;
            }
            NAPI.World.SetTime((int)settime, 0, 0);
        }
    }
}
