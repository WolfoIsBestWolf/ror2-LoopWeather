using BepInEx;
using LoopVariants;
using MonoMod.Cil;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
using RoR2.Stats;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace LoopVariantConfig
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("Wolfo.LoopVariantConfig", "LoopVariantConfig", "1.5.0")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
 
    public class VariantConfig : BaseUnityPlugin
    {
        public static bool HostHasMod_ = false;
        public static bool HostHasMod
        {
            get
            {
                return HostHasMod_ || NetworkServer.active;
            }
            set
            {
                HostHasMod_ = value;
            }
        }

        public static event Action<DirectorCardCategorySelection> applyWeatherDCCS;
        public static event Action applyWeatherVisuals;

        public void Start()
        {
            WConfig.RiskConfig();
        }
        public void Awake()
        {
            WConfig.InitConfig();
            OfficialVariant.Awake();

            //Run -> Next
            //Dccs take Next
            //Official variants take Next
            //Start
            //Roll
            //Take Current

            //Add component and roll for first stage
            On.RoR2.Run.Awake += AskHostIfHasMod;
            On.RoR2.Run.Start += AddRoll_StartOfRun;
            On.RoR2.Stage.PreStartClient += Global_RollForNextStage;
         

            IL.RoR2.ClassicStageInfo.RebuildCards += ApplyVariantDCCSChanges;


            ChatMessageBase.chatMessageTypeToIndex.Add(typeof(SendSyncLoopWeather), (byte)ChatMessageBase.chatMessageIndexToType.Count);
            ChatMessageBase.chatMessageIndexToType.Add(typeof(SendSyncLoopWeather));
            ChatMessageBase.chatMessageTypeToIndex.Add(typeof(HostHasModAlert), (byte)ChatMessageBase.chatMessageIndexToType.Count);
            ChatMessageBase.chatMessageIndexToType.Add(typeof(HostHasModAlert));
 
            applyWeatherDCCS += ApplyLoopDCCS;
            Stage1_Changes.EditDccs();
        }

  
        private void AskHostIfHasMod(On.RoR2.Run.orig_Awake orig, Run self)
        {
            orig(self);
            //Debug.LogWarning("On.RoR2.Run.orig_Awake");
            if (NetworkServer.active)
            {
                Chat.SendBroadcastChat(new HostHasModAlert());
            }
        }
        private void Global_RollForNextStage(On.RoR2.Stage.orig_PreStartClient orig, Stage self)
        {
            orig(self);
            SyncLoopWeather.instance.AppliedToCurrentStage = false;
            SyncLoopWeather.instance.RollForLoopWeather(false);
            //If Client && HostNoMod or Host
            if (!HostHasMod || NetworkServer.active)
            {
                if (SyncLoopWeather.instance.CurrentStage_LoopVariant)
                {
                    Action action = applyWeatherVisuals;
                    action();
                }
            }
        }
        private void AddRoll_StartOfRun(On.RoR2.Run.orig_Start orig, Run self)
        {
            Debug.LogWarning("On.RoR2.Run.orig_Start");
            SyncLoopWeather.instance.RollForLoopWeather(true);
            orig(self);
        }

        private void ApplyVariantDCCSChanges(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.Before,
            x => x.MatchStfld("RoR2.ClassicStageInfo", "modifiableMonsterCategories")))
            {
                c.EmitDelegate<Func<DirectorCardCategorySelection, DirectorCardCategorySelection>>((dccs) =>
                {
                    Debug.LogWarning("IL.RoR2.ClassicStageInfo.RebuildCards");
                    //Runs before Stage.PreStart so should take Next
                    if (SyncLoopWeather.instance.NextStage_LoopVariant)
                    {
                        Action<DirectorCardCategorySelection> action = applyWeatherDCCS;
                        action(dccs);
                    }
                    return dccs;
                });
            }
            else
            {
                Debug.LogWarning("IL Failed: AddVariantExclusiveMonsters");
            }
        }

        private static void ApplyLoopDCCS(DirectorCardCategorySelection dccs)
        {
            Debug.Log("Loop Variant DCCS : " + SceneCatalog.mostRecentSceneDef.baseSceneName);
            switch (SceneInfo.instance.sceneDef.baseSceneName)
            {
                case "lakesnight":
                    if (WConfig.LakesNightSpawnPool.Value)
                    {
                        Stage1_Changes.LakesNight_AddStage1FriendlyMonsters(dccs);
                    }
                    break;
                case "villagenight":
                    if (WConfig.VillageNight_Credits.Value)
                    {
                        if (Run.instance.stageClearCount == 0)
                        {
                            ClassicStageInfo.instance.sceneDirectorInteractibleCredits = 280;
                            HG.ArrayUtils.ArrayAppend(ref ClassicStageInfo.instance.bonusInteractibleCreditObjects,
                                new ClassicStageInfo.BonusInteractibleCreditObject
                                {
                                    points = -25, //Large Chest flat reduction
                                    objectThatGrantsPointsIfEnabled = RoR2.Run.instance.gameObject
                                });
                        }
                    }
                    break;
            }
        }
 
        
 
        public class HostHasModAlert : ChatMessageBase
        {
            public override string ConstructChatString()
            {
                if (HostHasMod == false)
                {
                    Debug.Log("LoopWeather | Host has mod");
                }
                HostHasMod = true;
                //Debug.Log("Will sync Custom Loop Variants with Host");
                return null;
            }
        }

        public class SendSyncLoopWeather : ChatMessageBase
        {
            public event Action<bool> onHostLoopChoice;
            public override string ConstructChatString()
            {
                if (!NetworkServer.active)
                {
                    SyncLoopWeather.instance.Next_Host = NEXT;
                    SyncLoopWeather.instance.Current_Host = CURRENT;
                    Debug.Log("Current Stage Loop Variant : " + SyncLoopWeather.instance);
                    Debug.Log("Next Stage Loop Variant : " + SyncLoopWeather.instance);
                    if (SyncLoopWeather.instance.CurrentStage_LoopVariant)
                    {
                        Action action = applyWeatherVisuals;
                        action();
                    }
                }
                return null;
            }

            public bool CURRENT;
            public bool NEXT;
            public override void Serialize(NetworkWriter writer)
            {
                base.Serialize(writer);
                writer.Write(CURRENT);
                writer.Write(NEXT);
            }
            public override void Deserialize(NetworkReader reader)
            {
                base.Deserialize(reader);
                CURRENT = reader.ReadBoolean();
                NEXT = reader.ReadBoolean();
            }

        }

        
    }
    public class SyncLoopWeather : MonoBehaviour
    {
        public static int loopClearCountPlusOne
        {
            get
            {
                if (!Run.instance)
                {
                    return 0;
                }
                return (Run.instance.stageClearCount + 1) / Run.stagesPerLoop;
            }
        }

        public void RollForLoopWeather(bool runStart)
        {
            bool useLoopChance = false;
            bool useLoop2Chance = false;
            if (RandomStageOrder)
            {
                useLoopChance = Util.CheckRoll(50, null);
            }
            else
            {
                if (WConfig.Alternate_Chances.Value)
                {
                    if (loopClearCountPlusOne % 2 == 1)
                    {
                        useLoopChance = true;
                    }
                }
                else if (loopClearCountPlusOne > 1 && WConfig.Chance_Loop_2.Value != -1)
                {
                    useLoop2Chance = true;
                }
                else
                {
                    useLoopChance = loopClearCountPlusOne > 0;
                }
            }
            if (useLoop2Chance)
            {
                NextStage_LoopVariant = Util.CheckRoll(WConfig.Chance_Loop_2.Value, null);
            }
            else if (useLoopChance)
            {
                NextStage_LoopVariant = Util.CheckRoll(WConfig.Chance_Loop.Value, null);
            }
            else
            {
                NextStage_LoopVariant = Util.CheckRoll(WConfig.Chance_PreLoop.Value, null);
            }
            if (NetworkServer.active)
            {
                Chat.SendBroadcastChat(new VariantConfig.SendSyncLoopWeather
                {
                    NEXT = Next_Host,
                    CURRENT = Current_Host,
                });
                if (runStart)
                {
                    Debug.Log("Stage 1        | Loop Variant : " + NextStage_LoopVariant);
                }
                else
                {
                    Debug.Log("Current Stage | Loop Variant : " + CurrentStage_LoopVariant);
                    Debug.Log("   Next Stage  | Loop Variant : " + NextStage_LoopVariant);
                }

            }


        }

        public static SyncLoopWeather instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Run.instance.gameObject.AddComponent<SyncLoopWeather>();
                }
                return _instance;
            }
        }
        public static SyncLoopWeather _instance = null;
        public static bool RandomStageOrder
        {
            get
            {
                if (!Run.instance)
                {
                    return false;
                }
                return Run.instance.ruleBook.stageOrder == StageOrder.Random;
            }

        }
        public void Awake()
        {
            _instance = this;
        }

        public bool AppliedToCurrentStage;

        public bool Next_Client;
        public bool Next_Host;
        public bool Current_Client;
        public bool Current_Host;

        public bool CurrentStage_LoopVariant
        {
            get
            {
                if (VariantConfig.HostHasMod)
                {
                    return Current_Host;
                }
                return Current_Client;
            }
        }
        public bool NextStage_LoopVariant
        {
            get
            {
                if (VariantConfig.HostHasMod)
                {
                    return Next_Host;
                }
                return Next_Client;
            }
            set
            {
                if (NetworkServer.active)
                {
                    Current_Host = Next_Host;
                    Next_Host = value;
                }
                Current_Client = Next_Client; 
                Next_Client = value;
            }
        }
        
    }
}