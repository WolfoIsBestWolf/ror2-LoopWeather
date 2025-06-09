using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace VariantConfig
{
    public static class WConfig
    {
        public static ConfigFile ConfigFileSTAGES = new ConfigFile(Paths.ConfigPath + "\\Wolfo.LoopVariantConfig.cfg", true);

        public static ConfigEntry<bool> Preset;

        public static ConfigEntry<bool> LakesNightSpawnPool; 
        public static ConfigEntry<bool> VillageNight_Credits;
 
        public static ConfigEntry<float> Chance_PreLoop;
        public static ConfigEntry<float> Chance_Loop;
        public static ConfigEntry<float> Chance_Loop_2;
        public static ConfigEntry<bool> Alternate_Chances;
 

        public static void InitConfig()
        {

            Preset = ConfigFileSTAGES.Bind(
                "Preset",
                "Simple 50/50 pre/post loop",
                false,
                "Sets chance config values to 50/50 or 0/100"
            );
            Preset.SettingChanged += Preset_SettingChanged;
            Chance_PreLoop = ConfigFileSTAGES.Bind(
                "Config",
                "Chance pre-loop",
                0f,
                "% Chance for a loop weather to happen pre-loop\n(Stages 1 - 5)"
            );
            Chance_Loop = ConfigFileSTAGES.Bind(
                "Config",
                "Chance post-loop",
                100f,
                "% Chance for a loop weather to happen loop 1\n(Stages 6 - 10)"
            );
            Chance_Loop_2 = ConfigFileSTAGES.Bind(
                "Config",
                "Chance consecutive-loops",
                100f,
                "% Chance for a loop weather to happen loop 2+\n(Stage 11+)\n-1 will just use loop 1 Chance instead"
            );
            Alternate_Chances = ConfigFileSTAGES.Bind(
                "Config",
                "Alternate Chances between loops",
                false,
                "Weather sets alternate between loops.\nGo standard, loop, standard, loop.\nIgnores consecutive loop setting."
            );
 
            LakesNightSpawnPool = ConfigFileSTAGES.Bind(
                "Stage 1 Loop Variants",
                "Friendlier spawnpools",
                true,
                "Change Viscious Falls & Disturbed Impact to be more Stage 1 friendly.\n\nViscious Fall content now Loop Exclusive\nImp Overlord\nGrovetender\nHalcyonite\nVoid Reaver\nElder Lemurian\nVoid Seed\n\nDisturbed Impact content now Loop Exclusive\nGrovetender"
            );
            VillageNight_Credits = ConfigFileSTAGES.Bind(
                "Stage 1 Loop Variants",
                "Credits Nerfs",
                true,
                "Nerf Disturbed Impact Credits to 280 - 30(flat), if it's Stage 1\nDisturbed Impact has 310 Credits and a guaranteed Large Chest.\n\nThis would make it the best stage 1 by far if not changed."
            );


          
        }

        private static void Preset_SettingChanged(object sender, System.EventArgs e)
        {
            if (Preset.Value == true)
            {
                Chance_PreLoop.Value = 50;
                Chance_Loop.Value = 50;
                Chance_Loop_2.Value = -1;
            }
            if (Preset.Value == false)
            {
                Chance_PreLoop.Value = 0;
                Chance_Loop.Value = 100;
                Chance_Loop_2.Value = -1;
            }
        }

        public static void RiskConfig()
        {
            ModSettingsManager.SetModDescription("Config for when Loop Variants happen");
            Texture2D icon = Addressables.LoadAssetAsync<Texture2D>(key: "RoR2/DLC2/lakesnight/texLakesNightPreview.png").WaitForCompletion();
            ModSettingsManager.SetModIcon(Sprite.Create(icon, new Rect(0, 0, 128, 128), new Vector2(-0.5f, -0.5f)));



            var entries = ConfigFileSTAGES.GetConfigEntries();

            ModSettingsManager.AddOption(new CheckBoxOption(Preset));
            ModSettingsManager.AddOption(new FloatFieldOption(Chance_PreLoop));
            ModSettingsManager.AddOption(new FloatFieldOption(Chance_Loop));
            ModSettingsManager.AddOption(new FloatFieldOption(Chance_Loop_2));
            ModSettingsManager.AddOption(new CheckBoxOption(Alternate_Chances));
            ModSettingsManager.AddOption(new CheckBoxOption(LakesNightSpawnPool,true));
            ModSettingsManager.AddOption(new CheckBoxOption(VillageNight_Credits));

 
        }
         

 
    }
}