using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LoopVariants
{
    public class Variants_6_Meridian : Variant_Base
    {

        public static void LoopWeather()
        {
            Material matBHFallPlatformSimple = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallPlatformSimple.mat").WaitForCompletion();
            Material matBHFallTerrainVines = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallTerrainVines.mat").WaitForCompletion();
            Material matBHFallDomeTrim = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDomeTrim.mat").WaitForCompletion();

            GameObject Terrain = GameObject.Find("/HOLDER: Art/Terrain");

            Transform BHFoliage = Terrain.transform.GetChild(5);
            Material matBHDistantTree_Billboard = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDistantTreeBillboard.mat").WaitForCompletion());
            Material matBHDistantTree = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC2/habitatfall/Assets/matBHFallDistantTree.mat").WaitForCompletion();


            BillboardRenderer[] billboardList = BHFoliage.GetComponentsInChildren<BillboardRenderer>();
            foreach (BillboardRenderer renderer in billboardList)
            {
                renderer.billboard.material = matBHDistantTree_Billboard;
            }
            //Why does this one work lol
            MeshRenderer[] rendererList = BHFoliage.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in rendererList)
            {
                switch (renderer.material.name)
                {
                    case "matBHDistantTree (Instance)":
                        renderer.material = matBHDistantTree;
                        break;
                }
            }

            Transform T3Objects = Terrain.transform.GetChild(6);
            rendererList = T3Objects.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in rendererList)
            {
                switch (renderer.material.name)
                {
                    case "matBHPlatformSimple (Instance)":
                        renderer.material = matBHFallPlatformSimple;
                        break;
                    case "matBHFallTerrainVines (Instance)":
                        renderer.material = matBHFallTerrainVines;
                        break;
                    case "matBHDomeTrim (Instance)":
                        renderer.material = matBHFallDomeTrim;
                        break;
                }
            }
        }

    }
}