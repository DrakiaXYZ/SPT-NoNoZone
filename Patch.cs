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
        private static FieldInfo _list0Field;
        protected override MethodBase GetTargetMethod()
        {
            _list0Field = AccessTools.Field(typeof(BotsClass), "list_0");

            return AccessTools.Method(typeof(BotsClass), "AddFromList");
        }

        [PatchPrefix]
        private static void Prefix_AddFromList(BotsClass __instance)
        {
            GameWorld game = Singleton<GameWorld>.Instance;
            Player player = game.MainPlayer;
            Vector3 playerPosition = player.Transform.position;
            string location = player.Location;
            float distanceClearValue = NoNoZonePlugin.GetDistance(location);
            var list_0 = (List<BotOwner>)_list0Field.GetValue(__instance);

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

    public class AddPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod() => AccessTools.Method(typeof(BotsClass), "Add");

        [PatchPrefix]
        private static void Prefix_Add(BotsClass __instance, BotOwner bot)
        {
            GameWorld game = Singleton<GameWorld>.Instance;
            Player player = game.MainPlayer;
            Vector3 playerPosition = player.Transform.position;
            string location = player.Location;
            float distanceClearValue = NoNoZonePlugin.GetDistance(location);


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
                string location = mainplayer.Location;
                float distanceClearValue = NoNoZonePlugin.GetDistance(location);

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