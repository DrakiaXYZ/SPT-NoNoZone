using System;
using BepInEx;
using BepInEx.Configuration;
using Comfort.Common;
using UnityEngine;
using Newtonsoft.Json;
using EFT;
using System.IO;
using System.Reflection;


namespace NoNoZone
{

    [BepInPlugin("com.dvize.NoNoZone", "dvize.NoNoZone", "1.0.0")]
    class NoNoZonePlugin : BaseUnityPlugin
    {
        public static ConfigEntry<float> DistanceToPreventSpawn
        {
            get;
            private set;
        }

        public static BotSpawnerClass botspawn;
        private void Awake()
        {
            DistanceToPreventSpawn = Config.Bind(
                "Main Settings",
                "Percentage Chance They Do Not Run Away from Grenade",
                35f,
                "Set the percentage chance here");

            botspawn = Singleton<BotSpawnerClass>.Instance;
            botspawn.OnBotCreated += CheckBotDistance();
        }

        private Action<BotOwner> CheckBotDistance()
        {
            try
            {
                foreach (Player bot in Singleton<GameWorld>.Instance.RegisteredPlayers)
                {
                    if (!bot.IsYourPlayer)
                    {
                        if (Vector3.Distance(bot.Position, Singleton<LocalPlayer>.Instance.Position) < DistanceToPreventSpawn.Value)
                        {
                            botspawn.DeletePlayer(bot);
                        }
                    }
                }
               
            }
            catch (Exception e)
            {
                Logger.LogInfo("NoNoZone: " + e);
            }

            return null;
        }
    
    }
}
