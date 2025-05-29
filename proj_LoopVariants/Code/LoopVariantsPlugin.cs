using BepInEx;
using R2API.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;
//using LoopVariantConfig;


namespace LoopVariants
{
    [BepInDependency("com.bepis.r2api")]
    [BepInDependency("Wolfo.LoopVariantConfig")]
    [BepInPlugin("Wolfo.WLoopVariants", "WolfosLoopVariants", "1.5.0")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]


    public class WLoopMain : BaseUnityPlugin
    {

        //public static List<string> ExistingVariants = new List<string>() { "wispgraveyard", "golemplains", "goolake", "dampcavesimple", "snowyforest", "helminthroost", "foggyswamp", "rootjungle", "sulfurpools", "lemuriantemple", "ancientloft" };
        public static List<string> DisabledVariants = new List<string>();

        public static bool AddMonsters
        {
            get
            {
                return WConfig.Monster_Additions.Value && ShouldAddContent;
            }
        }
        public static bool ShouldAddContent
        {
            get
            {
                if (WConfig.cfgGameplayChanges.Value == WConfig.AddContent.Always)
                {
                    return true;
                }
                if (WConfig.cfgGameplayChanges.Value == WConfig.AddContent.Never)
                {
                    return false;
                }
                return peopleWithMod == PlayerCharacterMasterController.instances.Count;
            }
        }
        public static int peopleWithMod = 0;

        public void Start()
        {
            WConfig.RiskConfig();
        }
        public void Awake()
        {
            Assets.Init(Info);
            WConfig.InitConfig();

            Main_Variants.Start();

            RemoveVariantNames();
            On.RoR2.UI.AssignStageToken.Start += ApplyLoopNameChanges;
 
            LoopVariantConfig.VariantConfig.applyWeatherDCCS += VariantDCCS;
            LoopVariantConfig.VariantConfig.applyWeatherVisuals += VariantConfig_applyWeatherGlobal;

            ChatMessageBase.chatMessageTypeToIndex.Add(typeof(ClientPing), (byte)ChatMessageBase.chatMessageIndexToType.Count);
            ChatMessageBase.chatMessageIndexToType.Add(typeof(ClientPing));
            On.RoR2.Run.PreStartClient += Run_PreStartClient;
            Run.onRunDestroyGlobal += Run_onRunDestroyGlobal;
        }
        private void Run_PreStartClient(On.RoR2.Run.orig_PreStartClient orig, Run self)
        {
            orig(self);
            Chat.SendBroadcastChat(new ClientPing());
        }
        private void Run_onRunDestroyGlobal(Run obj)
        {
            peopleWithMod = 0;
        }

        

        private void VariantConfig_applyWeatherGlobal()
        {
            bool hadVariant = false;
            try
            {
                switch (SceneInfo.instance.sceneDef.baseSceneName)
                {
                    case "golemplains":
                        if (WConfig.Stage_1_Golem.Value)
                        {
                            hadVariant = true;
                            Variants_1_GolemPlains.LoopWeather();
                        }
                        break;
                    case "blackbeach":
                        if (WConfig.WIP.Value)
                        {
                            hadVariant = true;
                            Variants_1_BlackBeach.LoopWeather();
                        }
                        break;
                    case "snowyforest":
                        if (WConfig.Stage_1_Snow.Value)
                        {
                            hadVariant = true;
                            Variants_1_SnowyForest.LoopWeather();
                        }
                        break;
                    case "goolake":
                        if (WConfig.Stage_2_Goolake.Value)
                        {
                            hadVariant = true;
                            Variants_2_Goolake.LoopWeather();
                        }
                        break;
                    case "foggyswamp":
                        if (WConfig.Stage_2_Swamp.Value)
                        {
                            hadVariant = true;
                            Variants_2_FoggySwamp.LoopWeather();
                        }
                        break;
                    case "ancientloft":
                        if (WConfig.Stage_2_Ancient.Value)
                        {
                            hadVariant = true;
                            Variants_2_AncientLoft.LoopWeather();
                        }
                        break;
                    case "lemuriantemple":
                        if (WConfig.Stage_2_Temple.Value)
                        {
                            hadVariant = true;
                            Variants_2_LemurianTemple.LoopWeather();
                        }
                        break;
                    case "frozenwall":
                        if (WConfig.WIP.Value)
                        {
                            hadVariant = true;
                            Variants_3_FrozenWall.LoopWeather();
                        }
                        break;
                    case "wispgraveyard":
                        if (WConfig.Stage_3_Wisp.Value)
                        {
                            hadVariant = true;
                            Variants_3_WispGraveyard.LoopWeather();
                        }
                        break;
                    case "sulfurpools":
                        if (WConfig.Stage_3_Sulfur.Value)
                        {
                            hadVariant = true;
                            Variants_3_Sulfur.LoopWeather();
                        }
                        break;
                    case "dampcavesimple":
                        if (WConfig.Stage_4_Damp_Abyss.Value)
                        {
                            hadVariant = true;
                            Variants_4_DampCaveSimpleAbyss.LoopWeather();
                        }
                        break;
                    case "shipgraveyard":
                        if (WConfig.WIP.Value)
                        {
                            hadVariant = true;
                            Variants_4_ShipGraveyard.LoopWeather();
                        }
                        break;
                    case "rootjungle":
                        if (WConfig.Stage_4_Root_Jungle.Value)
                        {
                            hadVariant = true;
                            Variants_4_RootJungle.LoopWeather();
                        }
                        break;
                    case "skymeadow":
                        if (WConfig.WIP.Value)
                        {
                            hadVariant = true;
                            Variants_5_SkyMeadow.LoopWeather();
                        }
                        break;
                    case "helminthroost":
                        if (WConfig.Stage_5_Helminth.Value)
                        {
                            hadVariant = true;
                            Variants_5_HelminthRoost.LoopWeather();
                        }
                        break;
                    case "moon2":
                        if (WConfig.WIP.Value)
                        {
                            hadVariant = true;
                            Variants_6_Moon.LoopWeather();
                        }
                        break;
                    case "meridian":
                        if (WConfig.Stage_6_Meridian.Value)
                        {
                            //hadVariant = true;
                            Variants_6_Meridian.LoopWeather();
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("LoopVariants Error: " + e);
            }

            if (hadVariant)
            {
                Debug.Log("Applying Weather to " + SceneInfo.instance.sceneDef.baseSceneName);
                LoopVariantConfig.SyncLoopWeather.instance.AppliedToCurrentStage = true;
            }
        }

        private void VariantDCCS(DirectorCardCategorySelection dccs)
        {
            if (!AddMonsters)
            {
                return;
            }
            try
            {
                switch (SceneInfo.instance.sceneDef.baseSceneName)
                {
                    case "snowyforest":
                        if (WConfig.Enemy_1_Snow.Value && WConfig.Stage_1_Snow.Value)
                        {
                            Variants_1_SnowyForest.AddVariantMonsters(dccs);
                        }
                        break;
                    case "ancientloft":
                        if (WConfig.Enemy_2_Ancient.Value && WConfig.Stage_2_Ancient.Value)
                        {
                            Variants_2_AncientLoft.AddVariantMonsters(dccs);
                        }
                        break;
                    case "wispgraveyard":
                        if (WConfig.Enemy_3_Wisp.Value && WConfig.Stage_3_Wisp.Value)
                        {
                            Variants_3_WispGraveyard.AddVariantMonsters(dccs);
                        }
                        break;
                    case "dampcavesimple":
                        if (WConfig.Enemy_4_Damp_Abyss.Value && WConfig.Stage_4_Damp_Abyss.Value)
                        {
                            Variants_4_DampCaveSimpleAbyss.AddVariantMonsters(dccs);
                        }
                        break;
                    case "rootjungle":
                        if (WConfig.Enemy_4_Root_Jungle.Value && WConfig.Stage_4_Root_Jungle.Value)
                        {
                            Variants_4_RootJungle.AddVariantMonsters(dccs);
                        }
                        break;
                    case "helminthroost":
                        if (WConfig.Enemy_5_Helminth.Value && WConfig.Stage_5_Helminth.Value)
                        {
                            Variants_5_HelminthRoost.AddVariantMonsters(dccs);
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("LoopVariants Error: " + e);
            }
        }

        public static void RemoveVariantNames()
        {

            if (!WConfig.Stage_1_Golem.Value)
            {
                DisabledVariants.Add("golemplains");
            }
            if (WConfig.Stage_1_Roost != null && !WConfig.Stage_1_Roost.Value)
            {
                DisabledVariants.Add("blackbeach");
            }
            if (!WConfig.Stage_1_Snow.Value)
            {
                DisabledVariants.Add("snowyforest");
            }
            //
            if (!WConfig.Stage_2_Goolake.Value)
            {
                DisabledVariants.Add("goolake");
            }
            if (!WConfig.Stage_2_Swamp.Value)
            {
                DisabledVariants.Add("foggyswamp");
            }
            if (!WConfig.Stage_2_Ancient.Value)
            {
                DisabledVariants.Add("ancientloft");
            }
            if (!WConfig.Stage_2_Temple.Value)
            {
                DisabledVariants.Add("lemuriantemple");
            }
            //
            if (WConfig.Stage_3_Frozen != null && !WConfig.Stage_3_Frozen.Value)
            {
                DisabledVariants.Add("frozenwall");
            }
            if (!WConfig.Stage_3_Wisp.Value)
            {
                DisabledVariants.Add("wispgraveyard");
            }
            if (!WConfig.Stage_3_Sulfur.Value)
            {
                DisabledVariants.Add("sulfurpools");
            }
            //
            if (!WConfig.Stage_4_Damp_Abyss.Value)
            {
                DisabledVariants.Add("dampcavesimple");
            }
            if (WConfig.Stage_4_Ship != null && !WConfig.Stage_4_Ship.Value)
            {
                DisabledVariants.Add("shipgraveyard");
            }
            if (!WConfig.Stage_4_Root_Jungle.Value)
            {
                DisabledVariants.Add("rootjungle");
            }
            //
            if (WConfig.Stage_5_Sky != null && !WConfig.Stage_5_Sky.Value)
            {
                DisabledVariants.Add("skymeadow");
            }
            if (!WConfig.Stage_5_Helminth.Value)
            {
                DisabledVariants.Add("helminthroost");
            }
            //
            if (WConfig.Stage_6_Commencement != null && !WConfig.Stage_6_Commencement.Value)
            {
                DisabledVariants.Add("moon2");
            }
            if (!WConfig.Stage_6_Meridian.Value)
            {
                DisabledVariants.Add("meridian");
            }
        }

        private static void ApplyLoopNameChanges(On.RoR2.UI.AssignStageToken.orig_Start orig, RoR2.UI.AssignStageToken self)
        {
            orig(self);
            if (WConfig.Name_Changes.Value)
            {
                if (LoopVariantConfig.SyncLoopWeather.instance.CurrentStage_LoopVariant)
                {
                    SceneDef mostRecentSceneDef = SceneCatalog.mostRecentSceneDef;
                    //If token exists
                    Language language2 = Language.FindLanguageByName("en");
                    if (language2.stringsByToken.ContainsKey(mostRecentSceneDef.nameToken + "_LOOP"))
                    {
                        if (!DisabledVariants.Contains(mostRecentSceneDef.baseSceneName))
                        {
                            self.titleText.SetText(Language.GetString(mostRecentSceneDef.nameToken + "_LOOP"), true);
                            self.subtitleText.SetText(Language.GetString(mostRecentSceneDef.subtitleToken + "_LOOP"), true);
                        }
                    }
                }
            }
        }

        public class ClientPing : ChatMessageBase
        {
            public override string ConstructChatString()
            {
                peopleWithMod++;
                return null;
            }
        }
    }

}