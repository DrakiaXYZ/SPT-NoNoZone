
using System;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using Newtonsoft.Json;
using EFT;
using System.IO;
using System.Reflection;
using Comfort.Common;
using EFT.UI.Ragfair;
using System.Collections.Generic;
using dvize.NoNoZone;

namespace NoNoZone
{

    [BepInPlugin("com.dvize.NoNoZone", "dvize.NoNoZone", "1.0.0")]
    class NoNoZonePlugin : BaseUnityPlugin
    {

        public static ConfigEntry<float> DistanceClearBotSpawn;
        private void Awake()
        {

            DistanceClearBotSpawn = Config.Bind(
                "Main Settings",
                "DistanceClearBotSpawn",
                20.0f,
                "Distance to Keep Clear of Bot Spawns");

            new BotClassSpawnerPatch().Enable();
        }


    }
}
