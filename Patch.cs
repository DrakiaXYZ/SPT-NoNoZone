using Aki.Reflection.Patching;
using Aki.Reflection.Utils;
using System.Linq;
using System.Reflection;
using EFT;
using Comfort.Common;
using UnityEngine;
using NoNoZone;
using EFT.AssetsManager;
using System;
using EFT.Bots;
using System.Collections.Generic;
using System.Threading;

namespace dvize.NoNoZone
{
    public class BotClassSpawnerPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            try
            {
                var desiredType = PatchConstants.EftTypes.Single(x => x.Name == "BotsClass");
                const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
                var desiredMethod = desiredType.GetMethod("AddFromList", flags);
                return desiredMethod;
            }
            catch
            {
                Logger.LogError("NoNoZone: Failed to get target method");
            }

            return null;
        }

        [PatchPrefix]

        public static bool Prefix(BotsClass __instance, ref List<BotOwner> ___list_0)
        {
            var player = Singleton<GameWorld>.Instance.MainPlayer;
            var tempList = new List<BotOwner>(___list_0);
            var playerPosition = player.Transform.position;
            var game = Singleton<GameWorld>.Instance;

            if (___list_0.Count > 0)
            {
                foreach (var botOwner in tempList)
                {
                    var botPosition = botOwner.Transform.position;
                    var distance = Vector3.Distance(botPosition, playerPosition);

                    if (distance < NoNoZonePlugin.DistanceClearBotSpawn.Value)
                    {
                        Logger.LogInfo($"NoNoZone: {botOwner.Profile.Info.Settings.Role} Spawned Too Close");

                        try
                        {
                            botOwner.Disable(); 
                        }
                        catch (Exception f)
                        {
                            Logger.LogError($"NoNoZone: Failed to disable bot: {f}");
                        }

                        try
                        {
                            game.RegisteredPlayers.Remove(botOwner.GetPlayer);
                        }
                        catch (Exception testc)
                        {
                            Logger.LogError($"NoNoZone: Failed to Remove Registered Player: {testc}");
                        }
                    }
                }

                return true;
            }

            return true;
        }
    }


}
