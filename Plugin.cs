
using System;
using System.Diagnostics;
using BepInEx;
using BepInEx.Configuration;
using ZonePatches;
using VersionChecker;
using Aki.Reflection.Patching;

namespace NoNoZone
{

    [BepInPlugin("com.dvize.NoNoZone", "dvize.NoNoZone", "1.4.1")]
    internal class NoNoZonePlugin : BaseUnityPlugin
    {
        public static ConfigEntry<float> factoryDistance;
        public static ConfigEntry<float> interchangeDistance;
        public static ConfigEntry<float> laboratoryDistance;
        public static ConfigEntry<float> lighthouseDistance;
        public static ConfigEntry<float> reserveDistance;
        public static ConfigEntry<float> shorelineDistance;
        public static ConfigEntry<float> woodsDistance;
        public static ConfigEntry<float> customsDistance;
        public static ConfigEntry<float> tarkovstreetsDistance;

        internal void Awake()
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
           
            CheckEftVersion();
        }

        private void Start()
        {
            new ZonePatches.AddFromListPatch().Enable();
            new ZonePatches.AddPatch().Enable();
            new ZonePatches.AddPlayerPatch().Enable();
        }
        private void CheckEftVersion()
        {
            // Make sure the version of EFT being run is the correct version
            int currentVersion = FileVersionInfo.GetVersionInfo(BepInEx.Paths.ExecutablePath).FilePrivatePart;
            int buildVersion = TarkovVersion.BuildVersion;
            if (currentVersion != buildVersion)
            {
                Logger.LogError($"ERROR: This version of {Info.Metadata.Name} v{Info.Metadata.Version} was built for Tarkov {buildVersion}, but you are running {currentVersion}. Please download the correct plugin version.");
                EFT.UI.ConsoleScreen.LogError($"ERROR: This version of {Info.Metadata.Name} v{Info.Metadata.Version} was built for Tarkov {buildVersion}, but you are running {currentVersion}. Please download the correct plugin version.");
                throw new Exception($"Invalid EFT Version ({currentVersion} != {buildVersion})");
            }
        }
    }
}
