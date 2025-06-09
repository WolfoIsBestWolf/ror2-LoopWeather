using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace LoopVariants
{
    public class Variants_2_LemurianTemple : Variant_Base
    {
        public static Material matBHFallEnvfxLeaves;
        public static Material matBHDistantTree_Billboard;
        public static Material matBHDistantTree;
        public static Material Cealing_Leaves_0_LOD0;

        public static Material LTFallenLeaf;

        public static Material matBHFallTerrainVines;
 
 
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


            matBHFallEnvfxLeaves = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallEnvfxLeaves.mat").WaitForCompletion());
            matBHDistantTree_Billboard = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDistantTreeBillboard.mat").WaitForCompletion();
            matBHDistantTree = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDistantTree.mat").WaitForCompletion();
            LTFallenLeaf = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/lemuriantemple/Assets/LTFallenLeaf.spm").WaitForCompletion());


            Texture2D BHDistantTreeP_Atlas = Assets.LoadAssetAsync<Texture2D>("Assets/LoopVariants/LemurianTemple/BHDistantTreeP_Atlas.png");
            Texture2D LTFallenLeaf_Atlas = Assets.LoadAssetAsync<Texture2D>("Assets/LoopVariants/LemurianTemple/LTFallenLeaf_Atlas.png");

            LTFallenLeaf.color = LeafColor;
            LTFallenLeaf.mainTexture = LTFallenLeaf_Atlas;
            LTFallenLeaf.SetColor("_HueVariation", new Color(1, 0.5f, 0.1f, 0.5f));
            matBHFallEnvfxLeaves.color = LeafColor;
            matBHFallEnvfxLeaves.mainTexture = BHDistantTreeP_Atlas;

            matBHFallTerrainVines = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallTerrainVines.mat").WaitForCompletion());
            matBHFallTerrainVines.SetFloat("_SnowBias", 0);

            
        }

        public static void LoopWeather()
        {
            
            #region Roots

            GameObject meshLTCeilingRoots = GameObject.Find("/HOLDER:Terrain/LTTerrain/meshLTCeilingRoots");
            meshLTCeilingRoots.GetComponent<MeshRenderer>().material = matBHFallTerrainVines; //matAncientLoft_BoulderInfiniteTower 


            GameObject LTColumnTGCVine = GameObject.Find("/HOLDER: ToggleGroups/HOLDER: TG FrontC/LTColumnTGCVine");
            if (LTColumnTGCVine)
            {
                LTColumnTGCVine.GetComponent<MeshRenderer>().material = matBHFallTerrainVines; //matAncientLoft_BoulderInfiniteTower 
            }


            #endregion


            #region Foliage
            GameObject Foliage = GameObject.Find("/HOLDER: Foliage");


            GameObject Weather = GameObject.Find("/Weather, LemurianTemple");

            Transform LeafParticles = Weather.transform.GetChild(5);
            ParticleSystemRenderer[] particleList = LeafParticles.GetComponentsInChildren<ParticleSystemRenderer>();
            foreach (ParticleSystemRenderer particle in particleList)
            {
                particle.material = matBHFallEnvfxLeaves;
            }
            //
            //
            Transform Leaves = Foliage.transform.GetChild(1);
            BillboardRenderer[] billboardList = Leaves.GetComponentsInChildren<BillboardRenderer>();
            foreach (BillboardRenderer renderer in billboardList)
            {
                renderer.billboard.material = matBHDistantTree_Billboard;
            }
            MeshRenderer[] rendererList = Leaves.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in rendererList)
            {
                switch (renderer.material.name)
                {
                    case "Leaves_0_LOD0 (Instance)":
                        renderer.material = matBHDistantTree;
                        break;
                    case "matBHDistantTree (Instance)":
                        renderer.material = matBHDistantTree;
                        break;
                }
            }
            //
            //



            //There's more fallen leaves in other holders


            Transform LeafPiles = Foliage.transform.GetChild(4);
            rendererList = LeafPiles.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in rendererList)
            {
                switch (renderer.material.name)
                {
                    case "Leaves_0_LOD0 (Instance)":
                        renderer.material = LTFallenLeaf; //Different Lod0 leaves
                        break;
                    case "Leaves_0_LOD1 (Instance)":
                        renderer.material = LTFallenLeaf;
                        break;
                }
            }


            GameObject Foliage2 = GameObject.Find("/HOLDER: ToggleGroups/HOLDER: TG BackA/TGBackB/LTFallenLeaf");
            GameObject Foliage3 = GameObject.Find("/HOLDER: ToggleGroups/HOLDER: TG FrontA");
            GameObject Foliage4 = GameObject.Find("/HOLDER: ToggleGroups/HOLDER: TG FrontC/LTFallenLeaf (9)");
            if (Foliage2)
            {
                Foliage2.transform.GetChild(0).GetComponent<MeshRenderer>().material = LTFallenLeaf;
                Foliage2.transform.GetChild(1).GetComponent<MeshRenderer>().material = LTFallenLeaf;
            }
            if (Foliage3)
            {
                Foliage3.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = LTFallenLeaf;
                Foliage3.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material = LTFallenLeaf;
                Foliage3.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material = LTFallenLeaf;
                Foliage3.transform.GetChild(1).GetChild(1).GetComponent<MeshRenderer>().material = LTFallenLeaf;
            }
            if (Foliage4)
            {
                Foliage4.transform.GetChild(0).GetComponent<MeshRenderer>().material = LTFallenLeaf;
                Foliage4.transform.GetChild(1).GetComponent<MeshRenderer>().material = LTFallenLeaf;
            }
            #endregion
 
        }


    }
}