
using BepInEx;
using BepInEx.Configuration;

namespace NoNoZone
{

    [BepInPlugin("com.dvize.NoNoZone", "dvize.NoNoZone", "1.5.0")]
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
        }

        private void Start()
        {
            new ZonePatches.AddFromListPatch().Enable();
            new ZonePatches.AddPatch().Enable();
            new ZonePatches.AddPlayerPatch().Enable();
        }

        public static float GetDistance(string location)
        {
            float distanceClearValue;

            switch (location)
            {
                case "factory4_day":
                case "factory4_night":
                    distanceClearValue = NoNoZonePlugin.factoryDistance.Value;
                    break;
                case "bigmap":
                    distanceClearValue = NoNoZonePlugin.customsDistance.Value;
                    break;
                case "interchange":
                    distanceClearValue = NoNoZonePlugin.interchangeDistance.Value;
                    break;
                case "reservbase":
                    distanceClearValue = NoNoZonePlugin.reserveDistance.Value;
                    break;
                case "laboratory":
                    distanceClearValue = NoNoZonePlugin.laboratoryDistance.Value;
                    break;
                case "lighthouse":
                    distanceClearValue = NoNoZonePlugin.lighthouseDistance.Value;
                    break;
                case "shoreline":
                    distanceClearValue = NoNoZonePlugin.shorelineDistance.Value;
                    break;
                case "woods":
                    distanceClearValue = NoNoZonePlugin.woodsDistance.Value;
                    break;
                case "tarkovstreets":
                    distanceClearValue = NoNoZonePlugin.tarkovstreetsDistance.Value;
                    break;
                default:
                    distanceClearValue = 25.0f;
                    break;
            }

            return distanceClearValue;
        }
    }
}
