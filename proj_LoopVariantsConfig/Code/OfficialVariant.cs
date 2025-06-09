using RoR2;
using UnityEngine;
using UnityEngine.Networking;
 
namespace VariantConfig
{
    public class OfficialVariant
    {
        public static void Awake()
        {
            On.RoR2.Run.PickNextStageScene += Teleporter_PickLoopVariant;
            On.RoR2.SceneExitController.GetLoopedStageOrDefault += GreenPortal_PickLoopVariant;
            On.RoR2.BazaarController.IsUnlockedBeforeLooping += SeerStationVariants;
        }

        public static SceneDef VariantToPreLoop(SceneDef loopedScene, WeightedSelection<SceneDef> lookIn)
        {
            if (lookIn == null)
            {

            }
            for (int i = 0; i < lookIn.Count; i++) 
            {
                if (lookIn.choices[i].value.loopedSceneDef == loopedScene)
                {
                    return lookIn.choices[i].value;
                }
            }
            return null;
        }

        public static bool SeerStationVariants(On.RoR2.BazaarController.orig_IsUnlockedBeforeLooping orig, BazaarController self, SceneDef sceneDef)
        {
            //PreLoop with Variant
            if (sceneDef.loopedSceneDef != null)
            {
                return !SyncLoopWeather.instance.NextStage_LoopVariant;
            }
            //Loop Variant
            if (sceneDef.isLockedBeforeLooping)
            {
                return SyncLoopWeather.instance.NextStage_LoopVariant;
            }
            return orig(self, sceneDef);
        }


        public static void Teleporter_PickLoopVariant(On.RoR2.Run.orig_PickNextStageScene orig, Run self, WeightedSelection<SceneDef> choices)
        {
            orig(self, choices);
            if (SyncLoopWeather.instance.NextStage_LoopVariant)
            {
                if (self.nextStageScene.loopedSceneDef != null && self.CanPickStage(self.nextStageScene.loopedSceneDef))
                {
                    self.nextStageScene = self.nextStageScene.loopedSceneDef;
                }
            }
            else if (self.nextStageScene.isLockedBeforeLooping)
            {
                SceneDef preLoopVariant = VariantToPreLoop(self.nextStageScene, choices);
                if (preLoopVariant != null)
                {
                    self.nextStageScene = preLoopVariant;
                }
            }
 
        }

        public static SceneDef GreenPortal_PickLoopVariant(On.RoR2.SceneExitController.orig_GetLoopedStageOrDefault orig, SceneExitController self, SceneDef sceneDef)
        {
            if (SyncLoopWeather.instance.NextStage_LoopVariant)
            {
                return sceneDef.loopedSceneDef;
            }
            /*else if (sceneDef.isLockedBeforeLooping)
            {
                SceneDef pre = VariantToPreLoop(sceneDef);
                return sceneDef;
            }*/
            return sceneDef;
        }


    }
}