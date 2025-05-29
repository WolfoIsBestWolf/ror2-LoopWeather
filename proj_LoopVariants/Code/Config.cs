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

        public static ConfigEntry<AddContent> cfgGameplayChanges;
        public static ConfigEntry<bool> Monster_Additions;
        public static ConfigEntry<bool> Name_Changes;

  
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

        public static void InitConfig()
        {
            cfgGameplayChanges = ConfigFileSTAGES.Bind(
                "Main",
                "Gameplay Changes",
                AddContent.AutoDetect,
                "Should there be mild gameplay changes during loop variants.\nDoes not include new gameobjects that might block a path.\nSome changes will not activate if you are already on the stage.\nIf you plan on playing with people that do not have this mod it's better to disable this.\n\nAquaduct : Slowing Tar\nSulfur Pools : Weaker but Lethal Helfire Pods\nSundered Grove : Healing Fruits"
            );
            Monster_Additions = ConfigFileSTAGES.Bind(
               "Main",
               "Add monsters to weather variants",
                true,
               "Add additional monsters to the spawn pool of variants.\n\nWill only add if it detects all users have mod."
           );
 
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

      
        public static void RiskConfig()
        {
           ModSettingsManager.SetModIcon(Assets.Bundle.LoadAsset<Sprite>("Assets/LoopVariants/Icon.png"));

            ModSettingsManager.SetModDescription("Loop Weather Variants for more stages.");


            CheckBoxConfig overwriteName = new CheckBoxConfig
            {
                name = "Gameplay Changes",
            };

            ModSettingsManager.AddOption(new ChoiceOption(cfgGameplayChanges));
            ModSettingsManager.AddOption(new CheckBoxOption(Monster_Additions));
            ModSettingsManager.AddOption(new CheckBoxOption(Name_Changes));
           
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_1_Golem));
            //ModSettingsManager.AddOption(new CheckBoxOption(Stage_1_Roost));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_1_Snow));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_1_Snow));

 
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Goolake));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Goolake_River));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Swamp));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Ancient));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_2_Ancient));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_2_Temple));


            ModSettingsManager.AddOption(new CheckBoxOption(Stage_3_Wisp));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_3_Wisp));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_3_Sulfur));
            //ModSettingsManager.AddOption(new CheckBoxOption(Stage_3_Sulfur_Hellfire));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_3_Sulfur_ExtraLights));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_4_Damp_Abyss));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_4_Damp_Abyss));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_4_Root_Jungle));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_4_Root_Jungle));
            ModSettingsManager.AddOption(new CheckBoxOption(Stage_4_Root_Jungle_Fruit));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_5_Helminth));
            ModSettingsManager.AddOption(new CheckBoxOption(Enemy_5_Helminth));

            ModSettingsManager.AddOption(new CheckBoxOption(Stage_6_Meridian));

        }


        public static ConfigEntry<bool> Stage_1_Golem;
        public static ConfigEntry<bool> Stage_1_Roost;
        public static ConfigEntry<bool> Stage_1_Snow;

        public static ConfigEntry<bool> Stage_2_Goolake;
        public static ConfigEntry<bool> Stage_2_Goolake_River;
        public static ConfigEntry<bool> Stage_2_Goolake_Elders;
        public static ConfigEntry<bool> Stage_2_Swamp;
        public static ConfigEntry<bool> Stage_2_Ancient;
        public static ConfigEntry<bool> Stage_2_Temple;

        public static ConfigEntry<bool> Stage_3_Frozen;
        public static ConfigEntry<bool> Stage_3_Wisp;
        public static ConfigEntry<bool> Stage_3_Sulfur;
        //public static ConfigEntry<bool> Stage_3_Sulfur_Hellfire;
        public static ConfigEntry<bool> Stage_3_Sulfur_ExtraLights;

        public static ConfigEntry<bool> Stage_4_Damp_Abyss;
        public static ConfigEntry<bool> Stage_4_Ship;
        public static ConfigEntry<bool> Stage_4_Root_Jungle;
        public static ConfigEntry<bool> Stage_4_Root_Jungle_Fruit;

        public static ConfigEntry<bool> Stage_5_Sky;
        public static ConfigEntry<bool> Stage_5_Helminth;

        public static ConfigEntry<bool> Stage_6_Commencement;
        public static ConfigEntry<bool> Stage_6_Meridian;
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

        public static ConfigEntry<bool> Enemy_4_Damp_Abyss;
        public static ConfigEntry<bool> Enemy_4_Ship;
        public static ConfigEntry<bool> Enemy_4_Root_Jungle;

        public static ConfigEntry<bool> Enemy_5_Sky;
        public static ConfigEntry<bool> Enemy_5_Helminth;

        public static ConfigEntry<bool> Enemy_6_Commencement;
        public static ConfigEntry<bool> Enemy_6_Meridian;



        public static void InitConfigStages()
        {

            Stage_1_Golem = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Titanic Plains",
                true,
                "Enable alt weather for this stage. Sunset Plains"
            );
            Stage_1_Roost = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Distant Roost",
                true,
                "Enable alt weather for this stage. Not implemented"
            );
            Stage_1_Snow = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Siphoned Forest",
                true,
                "Enable alt weather for this stage. Night Time Aurora Borealis"
            );
            Enemy_1_Snow = ConfigFileSTAGES.Bind(
                "Stage 1",
                "Siphoned Forest : Monsters",
                true,
                "Add mobs to Variant : Greater Wisps"
            );

            Stage_2_Goolake = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Abandoned Aquaduct",
                true,
                "Enable alt weather for this stage. Tar Filled, Green sick-ish feeling"
            );
            Stage_2_Goolake_River = ConfigFileSTAGES.Bind(
                 "Stage 2",
                 "Abandoned Aquaduct - River of Tar",
                 true,
                 "Enable the Tar River in the alt of this stage"
             );
            Stage_2_Swamp = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Wetland Aspect",
                true,
                "Enable alt weather for this stage. Foggy and Rainy"
            );
            Stage_2_Ancient = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Aphelian Sanctuary",
                true,
                "Enable alt weather for this stage. Night Time/Eclipsed Sun"
            );
            Enemy_2_Ancient = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Aphelian Sanctuary : Monsters",
                true,
                "Add mobs to Variant : Lunar Exploders always, Lunar Golem and Wisp on loops."
            );
            Stage_2_Temple = ConfigFileSTAGES.Bind(
                "Stage 2",
                "Reformed Altar",
                true,
                "Enable alt weather for this stage : Golden with Dieback leaves"
            );
            Stage_3_Frozen = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Rallypoint Delta",
                true,
                "Enable alt weather for this stage Not Implemented"
            );
            Stage_3_Wisp = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Scorched Acres",
                true,
                "Enable alt weather for this stage : Dusk"
            );
            Enemy_3_Wisp = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Scorched Acres : Monsters",
                true,
                "Add mobs to Variant : Child"
            );
            Stage_3_Sulfur = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Sulfur Pool",
                true,
                "Enable alt weather for this stage: Blue Lava"
            );
            /*Stage_3_Sulfur_Hellfire = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Sulfur Pool : Helfire",
                false,
                "Should Sulfur Pods in alt weather do less overall damage but also add lethal helfire."
            );*/
            Stage_3_Sulfur_ExtraLights = ConfigFileSTAGES.Bind(
                "Stage 3",
                "Sulfur Pool : Reduce Lights",
                false,
                "Reduce Light amount on this stage. This might help optimization"
            );

            Stage_4_Damp_Abyss = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Abyssal Depths",
                true,
                "Enable alt weather for this stage : More Red, vaguely Imp themed"
            );
            Enemy_4_Damp_Abyss = ConfigFileSTAGES.Bind(
               "Stage 4",
               "Abyssal Depths : Monsters",
               true,
               "Add mobs to Variant : Void Reavers/Barnacles/Imps always, Void Jailer/Devestator/Imp Overlords post-loop"
           );
            Stage_4_Ship = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sirens Call",
                true,
                "Enable alt weather for this stage / Not Implemented"
            );
            Stage_4_Root_Jungle = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sundered Grove",
                true,
                "Enable alt weather for this stage"
            );
            Enemy_4_Root_Jungle = ConfigFileSTAGES.Bind(
               "Stage 4",
               "Sundered Grove : Monsters",
               true,
               "Add mobs to Variant : Geep & Gip"
           );
            Stage_4_Root_Jungle_Fruit = ConfigFileSTAGES.Bind(
                "Stage 4",
                "Sundered Grove - Healing Fruit",
                true,
                "Spawn 30-40 Healing Fruits like the Healing Fruit in Treeborn or Eggs in Sirens Call"
            );
            Stage_5_Sky = ConfigFileSTAGES.Bind(
                "Stage 5",
                "Sky Meadow",
                true,
                "Enable alt weather for this stage. Not Implemented"
            );
            Stage_5_Helminth = ConfigFileSTAGES.Bind(
                "Stage 5",
                "Helminth Hatchery",
                true,
                "Enable alt weather for this stage"
            );
            Enemy_5_Helminth = ConfigFileSTAGES.Bind(
              "Stage 5",
              "Helminth Hatchery : Monsters",
              true,
              "Add mobs to Variant : Halcyonite"
          );
            /*Stage_6_Commencement = ConfigFileSTAGES.Bind(
                "Stage Final",
                "Commencement",
                true,
                "Enable alt weather for this stage"
            );*/
            Stage_6_Meridian = ConfigFileSTAGES.Bind(
                "Stage Final",
                "Prime Meridian",
                true,
                "Change trees to Golden Dieback coloration on loops"
            );

        }

    }
}