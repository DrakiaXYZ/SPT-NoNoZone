
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

    [BepInPlugin("com.dvize.NoNoZone", "dvize.NoNoZone", "1.3.0")]
    class NoNoZonePlugin : BaseUnityPlugin
    {
        //create configentry for each map.  factory, interchange, laboratory, lighthouse, reserve, shoreline, woods, customs, tarkovstreets
        
        public static ConfigEntry<float> factoryDistance;
        public static ConfigEntry<float> interchangeDistance;
        public static ConfigEntry<float> laboratoryDistance;
        public static ConfigEntry<float> lighthouseDistance;
        public static ConfigEntry<float> reserveDistance;
        public static ConfigEntry<float> shorelineDistance;
        public static ConfigEntry<float> woodsDistance;
        public static ConfigEntry<float> customsDistance;
        public static ConfigEntry<float> tarkovstreetsDistance;
        
        private void Awake()
        {
            
            factoryDistance = Config.Bind(
                "Main Settings",
                "factory",
                20.0f,
                "Distance to Keep Clear of Bot Spawns");

            customsDistance = Config.Bind(
                "Main Settings",
                "customs",
                20.0f,
                "Distance to Keep Clear of Bot Spawns");

            interchangeDistance = Config.Bind(
                "Main Settings",
                "interchange",
                20.0f,
                "Distance to Keep Clear of Bot Spawns");

            laboratoryDistance = Config.Bind(
                "Main Settings",
                "labs",
                20.0f,
                "Distance to Keep Clear of Bot Spawns");

            lighthouseDistance = Config.Bind(
                "Main Settings",
                "lighthouse",
                20.0f,
                "Distance to Keep Clear of Bot Spawns");

            reserveDistance = Config.Bind(
                "Main Settings",
                "reserve",
                20.0f,
                "Distance to Keep Clear of Bot Spawns");

            shorelineDistance = Config.Bind(
                "Main Settings",
                "shoreline",
                20.0f,
                "Distance to Keep Clear of Bot Spawns");

            woodsDistance = Config.Bind(
                "Main Settings",
                "woods",
                20.0f,
                "Distance to Keep Clear of Bot Spawns");

            tarkovstreetsDistance = Config.Bind(
                "Main Settings",
                "streets",
                20.0f,
                "Distance to Keep Clear of Bot Spawns");


            new BotClassSpawnerPatch().Enable();

        }


    }
}
