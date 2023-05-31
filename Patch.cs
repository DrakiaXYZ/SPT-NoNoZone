using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aki.Reflection.Patching;
using Comfort.Common;
using EFT;
using HarmonyLib;
using NoNoZone;
using UnityEngine;

namespace ZonePatches
{
    public class AddFromListPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod() => AccessTools.Method(typeof(BotsClass), "AddFromList");

        [PatchPrefix]
        private static void Prefix_AddFromList(BotsClass __instance)
        {
            GameWorld game = Singleton<GameWorld>.Instance;
            Player player = game.MainPlayer;
            Vector3 playerPosition = player.Transform.position;
            float distanceClearValue = 0f;
            string location = player.Location;
            var list_0 = (List<BotOwner>)AccessTools.Field(typeof(BotsClass), "list_0").GetValue(__instance);
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


            if (list_0.Count > 0)
            {
                foreach (BotOwner botOwner in list_0)
                {
                    if (botOwner.GetPlayer != player)
                    {
                        var botPosition = botOwner.Transform.position;
                        var distance = Vector3.Distance(botPosition, playerPosition);

                        if (distance < distanceClearValue)
                        {
                            var tempplayer = game.RegisteredPlayers.FirstOrDefault(p => p == botOwner.GetPlayer);
                            if (tempplayer != null)
                            {
                                /* Logger.LogDebug($"Location Value is: {location} and the distance Cap is ({distanceClearValue}(m)");
                                 Logger.LogDebug($"NoNoZone: Disabled Player: {tempplayer.Profile.Nickname}, {tempplayer.Profile.Info.Settings.Role} ({distance}m)");*/

                                game.UnregisterPlayer(botOwner.GetPlayer);
                                __instance.RemovePlayer(botOwner.GetPlayer);
                                __instance.Remove(botOwner);
                            }
                        }
                    }

                }

            }

        }

    }

    public class AddPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod() => AccessTools.Method(typeof(BotsClass), "Add");

        [PatchPrefix]
        private static void Prefix_Add(BotsClass __instance, BotOwner bot)
        {
            GameWorld game = Singleton<GameWorld>.Instance;
            Player player = game.MainPlayer;
            Vector3 playerPosition = player.Transform.position;
            float distanceClearValue = 0f;
            string location = player.Location;

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


            var botPosition = bot.Transform.position;
            var distance = Vector3.Distance(botPosition, playerPosition);

            if (distance < distanceClearValue)
            {
                var tempplayer = game.RegisteredPlayers.FirstOrDefault(p => p == bot.GetPlayer);
                if (tempplayer != null)
                {
                    /*Logger.LogDebug($"Location Value is: {location} and the distance Cap is ({distanceClearValue}(m)");
                    Logger.LogDebug($"NoNoZone: Disabled Player: {tempplayer.Profile.Nickname}, {tempplayer.Profile.Info.Settings.Role} ({distance}m)");*/

                    game.UnregisterPlayer(bot.GetPlayer);
                    __instance.RemovePlayer(bot.GetPlayer);
                    __instance.Remove(bot);
                }
            }

        }

    }

    public class AddPlayerPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod() => AccessTools.Method(typeof(BotsClass), "AddPlayer");

        [PatchPrefix]
        private static void Prefix_AddPlayer(BotsClass __instance, Player player)
        {
            if (player.IsAI)
            {
                var BotPlayer = player;
                GameWorld game = Singleton<GameWorld>.Instance;
                Player mainplayer = game.MainPlayer;
                Vector3 playerPosition = mainplayer.Transform.position;
                float distanceClearValue = 0f;
                string location = mainplayer.Location;

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


                var botPosition = BotPlayer.Transform.position;
                var distance = Vector3.Distance(botPosition, playerPosition);

                if (distance < distanceClearValue)
                {
                    if (BotPlayer != null)
                    {
                        /*Logger.LogDebug($"Location Value is: {location} and the distance Cap is ({distanceClearValue}(m)");
                        Logger.LogDebug($"NoNoZone: Disabled Player: {BotPlayer.Profile.Nickname}, {BotPlayer.Profile.Info.Settings.Role} ({distance}m)");*/

                        game.UnregisterPlayer(BotPlayer);
                        __instance.RemovePlayer(BotPlayer);
                        __instance.Remove(BotPlayer.AIData.BotOwner);
                    }
                }
            }
        }

    }
}