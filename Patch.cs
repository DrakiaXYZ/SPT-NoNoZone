using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using System.Linq;
using System.Reflection;
using EFT;
using Comfort.Common;
using UnityEngine;
using NoNoZone;
using System.Collections.Generic;
using System.Threading;
using System;
using EFT.UI.Matchmaker;

namespace dvize.NoNoZone
{
    public class BotClassSpawnerPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            try
            {
                return typeof(BotsClass).GetMethod("AddFromList", BindingFlags.Instance | BindingFlags.Public);
            }
            catch
            {
                Logger.LogInfo("NoNoZone: Failed to get target method");
            }

            return null;
        }

        [PatchPrefix]
        public static bool Prefix(BotsClass __instance, ref List<BotOwner> ___list_0)
        {
            var game = Singleton<GameWorld>.Instance;
            var player = game.MainPlayer;
            var playerPosition = player.Transform.position;
            float distanceClearValue = 0f;
            string location;


            location = player.Location;


            switch (location)
            {
                case "factory4_day":
                    distanceClearValue = NoNoZonePlugin.factoryDistance.Value;
                    break;
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
                    distanceClearValue = 250.0f;
                    break;
            }


            for (int i = 0; i < ___list_0.Count; i++)
            {
                var botOwner = ___list_0[i];
                var botPosition = botOwner.Transform.position;
                var distance = Vector3.Distance(botPosition, playerPosition);

                if (distance < distanceClearValue)
                {
                    var tempplayer = game.RegisteredPlayers.FirstOrDefault(p => p == botOwner.GetPlayer);
                    if (tempplayer != null)
                    {
                        Logger.LogDebug($"Location Value is: {location} and the distance Cap is ({distanceClearValue}(m)");
                        Logger.LogDebug($"NoNoZone: Disabled Player: {tempplayer.Profile.Nickname}, {tempplayer.Profile.Info.Settings.Role} ({distance}m)");

                        game.UnregisterPlayer(botOwner.GetPlayer);
                        __instance.RemovePlayer(botOwner.GetPlayer);
                        __instance.Remove(botOwner);
                    }
                }
            }

            return true;
        }
    }
}