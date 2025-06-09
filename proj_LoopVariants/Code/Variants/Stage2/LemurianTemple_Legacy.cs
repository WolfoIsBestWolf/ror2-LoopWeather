using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace LoopVariants
{
    public class Variants_2_LemurianTemple_Legacy : Variant_Base
    {
 
        public static Material Gold_matLTCrystal;
        public static Material CrystalForAltar;


        public static PostProcessProfile PP_AncientLoft;
        public static bool setupComplete = false;
        public static new void Setup()
        {
            if (setupComplete)
            {
                return;
            }
            setupComplete = true;
            //Gold instead of Green Crystals?
            //More Orange homey fog?
            //Leaves from Golden Dieback cuz ofc
            //Color LeafColor = new Color(1.8f, 0.8f, 2.3f, 2f) * 0.9f;
            Color LeafColor = new Color(0.6604f, 0.3208f, 0.529f, 1) * 1.1f;


 
            //matLTCrystal = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/lemuriantemple/Assets/matLTCrystal.mat").WaitForCompletion());
            Gold_matLTCrystal = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/matHalcyoniteShrineGold.mat").WaitForCompletion());
            //matLTCrystal = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/ShrineGoldshoresAccess/matShrineChanceGolden.mat").WaitForCompletion());

            //Texture2D texHalcyoniteShrineFlow = Files.Bundle.LoadAsset<Texture2D>("Assets/LoopVariants/LemurianTemple/texHalcyoniteShrineFlow.png");

            //matLTCrystal.SetTexture("_FlowHeightmap", texHalcyoniteShrineFlow);
            Gold_matLTCrystal.color *= 1.3f;
            Gold_matLTCrystal.SetFloat("_SliceHeight", 500);
            //_SliceHeight 9.66
            CrystalForAltar = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/matShrineColossusAccess.mat").WaitForCompletion());
            CrystalForAltar.color = new Color(3.5f, 1f, 0.5f);

            PP_AncientLoft = Object.Instantiate(Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/DLC1/ancientloft/ppSceneAncientLoft.asset").WaitForCompletion());
            PostProcessProfile ppSceneGoolake = Addressables.LoadAssetAsync<PostProcessProfile>(key: "RoR2/Base/title/PostProcessing/ppSceneGoolake.asset").WaitForCompletion();

            RampFog rampFog = (RampFog)Object.Instantiate(PP_AncientLoft.settings[0]);
            RampFog rampFogG = (RampFog)ppSceneGoolake.settings[0];

            //rampFog.fogColorMid.value = new Color(0.67 0.435 0.35 0.44);//
            //rampFog.fogColorEnd.value = (rampFog.fogColorEnd.value*2 + rampFogG.fogColorMid.value) / 3;
            //rampFog.fogColorMid.value = (rampFog.fogColorMid.value + rampFogG.fogColorMid.value) / 2;
            //rampFog.fogColorStart.value = (rampFog.fogColorStart.value + rampFogG.fogColorStart.value) / 2;
            rampFog.fogColorEnd.value = new Color(0.6f, 0.5f, 0.34f, 1);
            rampFog.fogColorMid.value = new Color(0.67f, 0.5f, 0.32f, 0.44f);

            PP_AncientLoft.settings[0] = rampFog;

        }

        public static void LoopWeather()
        {
            #region Lighting
            GameObject Weather = GameObject.Find("/Weather, LemurianTemple");

            Light Sun = Weather.transform.GetChild(1).GetComponent<Light>();
            Sun.color = new Color(1f, 0.9176f, 0.749f, 1);//0.749 0.9176 1 1
            Sun.intensity = 0.35f; //0.7

            PostProcessVolume PP = Weather.transform.GetChild(2).GetComponent<PostProcessVolume>();
            PP.profile = PP_AncientLoft;

            SetAmbientLight Ambient = Weather.transform.GetChild(2).GetComponent<SetAmbientLight>();
            Ambient.ambientSkyColor = new Color(1, 1, 0.8f, 1f);//0.9216 1 1 1
            Ambient.ambientIntensity = 0.285f; //0.38
            Ambient.ApplyLighting();


            Color GodRayColor = new Color(1.3f, 0.9759f, 0.6706f, 1);//1 0.9759 0.6706 1
            Light[] lightsList = Weather.transform.GetChild(6).GetComponentsInChildren<Light>();
            foreach (Light light in lightsList)
            {
                light.color = GodRayColor;
            }
            #endregion
  
            #region Crystals
            GameObject Prop = GameObject.Find("/HOLDER: Prop");

            Transform GoldDecals = Prop.transform.Find("GoldDecal");
            for (int i = 0; i < GoldDecals.childCount; i++)
            {
                GoldDecals.GetChild(i).gameObject.SetActive(true);
            }

            Transform Crystals = Prop.transform.GetChild(4);
            Crystals.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().materials = new Material[]
            {
                 Crystals.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material,
                Gold_matLTCrystal
            };


            //1 0.8789 0.2877 1
            //Color CrystalColor = new Color(0.42f, 0.42f, 0.12f, 1f); //0.1176 0.4745 0.3608 1
            Color CrystalColor = new Color(1f, 0.8789f, 0.2877f, 1); //0.1176 0.4745 0.3608 1
            lightsList = Crystals.GetChild(1).GetComponentsInChildren<Light>();
            foreach (Light light in lightsList)
            {
                light.color = CrystalColor;
            }

            GameObject AltarCrystal = GameObject.Find("/HOLDER:Terrain/LTAltar/meshLTAltarCrystal");
            AltarCrystal.GetComponent<MeshRenderer>().material = CrystalForAltar;


            #endregion
        }


    }
}