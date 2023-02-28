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
                var desiredMethod = desiredType.GetMethod("Add", flags);
                return desiredMethod;
            }
            catch
            {
                Logger.LogError("NoNoZone: Failed to get target method");
            }

            return null;
        }

        [PatchPrefix]

        public static void Prefix(BotsClass __instance, BotOwner bot)
        {
            
            var player = Singleton<GameWorld>.Instance.MainPlayer;
            Logger.LogInfo($"NoNoZone: Bot Spawned");
            if (bot != null && player != null)
            {
                var botPosition = bot.Transform.position;
                var playerPosition = player.Transform.position;
                var distance = Vector3.Distance(botPosition, playerPosition);
                
                if (distance < NoNoZonePlugin.DistanceClearBotSpawn.Value)
                {
                    Logger.LogInfo("NoNoZone: Bot Spawned Too Close");
                    try
                    {
                        __instance.Remove(bot);
                    }
                    catch(Exception f)
                    {
                        Logger.LogError($"NoNoZone: Failed to remove bot {f}");
                    }

                    try
                    {
                        bot.Deactivate();
                        bot.Dispose();
                    }
                    catch (Exception e)
                    {
                        Logger.LogError($"NoNoZone: Failed to Deactivate botOwner: {e}");
                    }

                    //to re-add bot i need to pre-activate with another bot.. not worth hastle.
                   /* try
                    {
                        __instance.Add(bot);
                    }
                    catch (Exception g)
                    {
                        Logger.LogError($"NoNoZone: Failed to re-add bot: {g}");
                    }*/
                    
                    return;
                }
            }

            return;
        }
        
    }






}