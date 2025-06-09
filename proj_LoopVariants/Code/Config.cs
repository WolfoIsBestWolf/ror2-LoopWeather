using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using UnityEngine;

namespace LoopVariants
{
    public static class WConfig
    {
        public static ConfigFile ConfigFileSTAGES = new ConfigFile(Paths.ConfigPath + "\\Wolfo.LoopVariants.cfg", true);

        //public static ConfigEntry<AddContent> cfgGameplayChanges;
        public static ConfigEntry<bool> Monster_Additions;
        public static ConfigEntry<bool> Name_Changes;
        public static ConfigEntry<bool> LegacyVariants;
         

        public enum EnumEnemyAdds
        {
            Never,
            LittleGameplayTweaks,
            Always
        }
        public enum AddContent
        {
            Never,
            AutoDetect,
            Always
        }
        public enum TarRiver
        {
            Off,
            VisualOnly,
            Inflicts
        }
        public static void InitConfig()
        {
            /*cfgGameplayChanges = ConfigFileSTAGES.Bind(
                "Main",
                "Gameplay Changes",
                AddContent.AutoDetect,
                "Should there be mild gameplay changes during loop variants.\nDoes not include new gameobjects that might block a path.\nSome changes will not activate if you are already on the stage.\nIf you plan on playing with people that do not have this mod it's better to disable this.\n\nAquaduct : Slowing Tar\nSulfur Pools : Weaker but Lethal Helfire Pods\nSundered Grove : Healing Fruits"
            );*/
            Monster_Additions = ConfigFileSTAGES.Bind(
               "Main",
               "Add monsters to weather variants",
                true,
               "Add additional monsters to the spawn pool of variants.\n\nWill only add if it detects all users have mod."
           );            
            LegacyVariants = ConfigFileSTAGES.Bind(
               "Main",
               "Legacy Variants",
                true,
               "I may disable variants i'm not entirely happy with after some time. This enables all the config for those."
           );
            LegacyVariants.SettingChanged += LegacyVariants_SettingChanged;


            Name_Changes = ConfigFileSTAGES.Bind(
               "Main",
               "Name Changes",
               true,
               "Should variants display a different name like official variants."
            );
     

            WIP = ConfigFileSTAGES.Bind(
                "Testing",
                "Wip",
                false,
                "Not much to see"
            );
            InitConfigStages();
        }

        private static void LegacyVariants_SettingChanged(object sender, System.EventArgs e)
        {
            S_2_LemurianTemple_Legacy = LegacyVariants;
            S_4_DampAbyss_Legacy = LegacyVariants;
            S_5_Helminth_Legacy = LegacyVariants;

        }

        public static void RiskConfig()
        {
           ModSettingsManager.SetModIcon(Assets.LoadAssetAsync<Sprite>("Assets/LoopVariants/Icon.png"));

            ModSettingsManager.SetModDescription("Loop Weather Variants for more stages.");


            CheckBoxConfig overwriteName = new CheckBoxConfig
            {
                name = "Gameplay Changes",
            };

            //ModSettingsManager.AddOption(new ChoiceOption(cfgGameplayChanges));
            ModSettingsManager.AddOption(new CheckBoxOption(Monster_Additions));
            ModSettingsManager.AddOption(new CheckBoxOption(Name_Changes));
            ModSettingsManager.AddOption(new CheckBoxOption(LegacyVariants));
           
            ModSettingsManager.AddOption(new CheckBoxOption(S_1_Golem));
            //ModSettingsManager.AddOption(new CheckBoxOption(S_1_Roost));
            ModSettingsManager.AddOption(new CheckBoxOption(S_1_Snow));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_1_Snow));

 
            ModSettingsManager.AddOption(new CheckBoxOption(S_2_Goolake));
            ModSettingsManager.AddOption(new CheckBoxOption(S_2_Goolake_River));
            ModSettingsManager.AddOption(new CheckBoxOption(S_2_FoggySwamp));
            ModSettingsManager.AddOption(new CheckBoxOption(S_2_Ancient));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_2_Ancient));
            ModSettingsManager.AddOption(new CheckBoxOption(S_2_LemurianTemple_Legacy));


            ModSettingsManager.AddOption(new CheckBoxOption(S_3_Wisp));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_3_Wisp));
            ModSettingsManager.AddOption(new CheckBoxOption(S_3_Sulfur));
            //ModSettingsManager.AddOption(new CheckBoxOption(S_3_Sulfur_Hellfire));
            ModSettingsManager.AddOption(new CheckBoxOption(S_3_Sulfur_ExtraLights));

            ModSettingsManager.AddOption(new CheckBoxOption(S_4_DampAbyss_Legacy));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_4_Damp_Abyss_Legacy));

            ModSettingsManager.AddOption(new CheckBoxOption(S_4_Root_Jungle));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_4_Root_Jungle));
            //ModSettingsManager.AddOption(new CheckBoxOption(S_4_Root_Jungle_Fruit));

            ModSettingsManager.AddOption(new CheckBoxOption(S_5_Helminth_Legacy));
             
            ModSettingsManager.AddOption(new CheckBoxOption(S_6_Meridian));

        }


        public static ConfigEntry<bool> S_1_Golem;
        public static ConfigEntry<bool> S_1_Roost;
        public static ConfigEntry<bool> S_1_Snow;
        public static ConfigEntry<bool> S_1_SnowIceCream;

        public static ConfigEntry<bool> S_2_Goolake;
        public static ConfigEntry<bool> S_2_Goolake_River;
        public static ConfigEntry<bool> S_2_FoggySwamp;
        public static ConfigEntry<bool> S_2_Ancient;
        public static ConfigEntry<bool> S_2_LemurianTemple_Legacy;
        public static ConfigEntry<bool> S_2_LemurianTemple_HabitatFall;

        public static ConfigEntry<bool> S_3_Frozen;
        public static ConfigEntry<bool> S_3_Wisp;
        public static ConfigEntry<bool> S_3_Sulfur;
        //public static ConfigEntry<bool> S_3_Sulfur_Hellfire;
        public static ConfigEntry<bool> S_3_Sulfur_ExtraLights;

        public static ConfigEntry<bool> S_4_DampAbyss_Legacy;
        public static ConfigEntry<bool> S_4_Ship;
        public static ConfigEntry<bool> S_4_Root_Jungle;
        //public static ConfigEntry<bool> S_4_Root_Jungle_Fruit;

        public static ConfigEntry<bool> S_5_Sky;
        public static ConfigEntry<bool> S_5_Helminth_Legacy;

        public static ConfigEntry<bool> S_6_Commencement;
        public static ConfigEntry<bool> S_6_Meridian;
        public static ConfigEntry<bool> WIP;


        public static ConfigEntry<bool> Enemy_1_Golem;
        public static ConfigEntry<bool> Enemy_1_Roost;
        public static ConfigEntry<bool> Enemy_1_Snow;
        public static ConfigEntry<bool> Enemy_1_Lakes;
        public static ConfigEntry<bool> Enemy_1_Village;

        public static ConfigEntry<bool> Enemy_2_Goolake;
        public static ConfigEntry<bool> Enemy_2_Swamp;
        public static ConfigEntry<bool> Enemy_2_Ancient;
        public static ConfigEntry<bool> Enemy_2_Temple;

        public static ConfigEntry<bool> Enemy_3_Frozen;
        public static ConfigEntry<bool> Enemy_3_Wisp;
        public static ConfigEntry<bool> Enemy_3_Sulfur;

        public static ConfigEntry<bool> Enemy_4_Damp_Abyss_Legacy;
        public static ConfigEntry<bool> Enemy_4_Ship;
        public static ConfigEntry<bool> Enemy_4_Root_Jungle;

        public static ConfigEntry<bool> Enemy_5_Sky;
        
        public static ConfigEntry<bool> Enemy_6_Commencement;
        public static ConfigEntry<bool> Enemy_6_Meridian;



        public static void InitConfigStages()
        {

            S_1_Golem = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Titanic Plains",
                true,
                "Enable alt weather for this stage. Sunset Plains"
            );
            S_1_Roost = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Distant Roost",
                true,
                "Enable alt weather for this stage. Not implemented"
            );
            S_1_Snow = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Siphoned Forest",
                true,
                "Enable alt weather for this stage. Night Time Aurora Borealis"
            );
            Enemy_1_Snow = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Siphoned Forest | Monsters",
                true,
                "Add mobs to Variant : Greater Wisps"
            );

            S_2_Goolake = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Abandoned Aquaduct",
                true,
                "Enable alt weather for this stage. Tar Filled, Green sick-ish feeling"
            );
            S_2_Goolake_River = ConfigFileSTAGES.Bind(
                 "Stage 2",
                 "Abandoned Aquaduct - River of Tar",
                 true,
                 "Enable the Tar River in the alt of this stage"
             );
            S_2_FoggySwamp = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Wetland Aspect",
                true,
                "Enable alt weather for this stage. Foggy and Rainy"
            );
            S_2_Ancient = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Aphelian Sanctuary",
                true,
                "Enable alt weather for this stage. Night Time/Eclipsed Sun"
            );
            Enemy_2_Ancient = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Aphelian Sanctuary | Monsters",
                false,
                "Add mobs to Variant : Lunar Exploders always, Lunar Golem and Wisp on loops."
            );
            S_2_LemurianTemple_Legacy = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Reformed Altar | Legacy",
                false,
                "Enable alt weather for this stage : Golden with Dieback leaves"
            );
            S_3_Frozen = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Rallypoint Delta",
                true,
                "Enable alt weather for this stage Not Implemented"
            );
            S_3_Wisp = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Scorched Acres",
                true,
                "Enable alt weather for this stage : Dusk"
            );
            Enemy_3_Wisp = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Scorched Acres | Monsters",
                true,
                "Add mobs to Variant : Child"
            );
            S_3_Sulfur = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Sulfur Pool",
                true,
                "Enable alt weather for this stage: Blue Lava"
            );
            /*S_3_Sulfur_Hellfire = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Sulfur Pool : Helfire",
                false,
                "Should Sulfur Pods in alt weather do less overall damage but also add lethal helfire."
            );*/
            S_3_Sulfur_ExtraLights = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Sulfur Pool : Reduce Lights",
                false,
                "Reduce Light amount on this stage. This might help optimization"
            );

            S_4_DampAbyss_Legacy = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Abyssal Depths | Legacy",
                false,
                "Enable alt weather for this stage : More Red, vaguely Imp themed"
            );
            Enemy_4_Damp_Abyss_Legacy = ConfigFileSTAGES.Bind(
               "Stage 4",
               "Abyssal Depths | Legacy | Monsters",
               false,
               "Add mobs to Variant : Void Reavers/Barnacles/Imps always, Void Jailer/Devestator/Imp Overlords post-loop"
           );
            S_4_Ship = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sirens Call",
                true,
                "Enable alt weather for this stage / Not Implemented"
            );
            S_4_Root_Jungle = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sundered Grove",
                true,
                "Enable alt weather for this stage"
            );
            Enemy_4_Root_Jungle = ConfigFileSTAGES.Bind(
               "Stage 4",
               "Sundered Grove | Monsters",
               true,
               "Add mobs to Variant : Geep & Gip"
           );
            /*S_4_Root_Jungle_Fruit = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sundered Grove - Healing Fruit",
                true,
                "Spawn 30-40 Healing Fruits like the Healing Fruit in Treeborn or Eggs in Sirens Call"
            );*/
            S_5_Sky = ConfigFileSTAGES.Bind(
                "Stage 5",
                "Sky Meadow",
                true,
                "Enable alt weather for this stage. Not Implemented"
            );
            S_5_Helminth_Legacy = ConfigFileSTAGES.Bind(
                "Stage 5",
                "Helminth Hatchery | Legacy",
                false,
                "Enable alt weather for this stage"
            );
    
            /*S_6_Commencement = ConfigFileSTAGES.Bind(
                "Stage Final",
                "Commencement",
                true,
                "Enable alt weather for this stage"
            );*/
            S_6_Meridian = ConfigFileSTAGES.Bind(
                "Stage Final",
                "Prime Meridian",
                true,
                "Change trees to Golden Dieback coloration on loops"
            );

        }

    }
}