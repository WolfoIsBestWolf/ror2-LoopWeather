using LoopVariantConfig;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LoopVariants
{
    public class Stage1_Changes
    {
        public static void EditDccs()
        {
            if (!WConfig.LakesNightSpawnPool.Value)
            {
                return;
            }
            DirectorCardCategorySelection dccsLakesnightMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsLakesnightInteractables_DLC1 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/lakesnight/dccsLakesnightInteractables_DLC1.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsVillageNightMonsters_Additional = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/villagenight/dccsVillageNightMonsters_Additional.asset").WaitForCompletion();

            //Grandparent, BeetleQueen, Vagrant allowed
            dccsLakesnightMonsters.categories[0].cards[2].minimumStageCompletions = 2; //Grave
            dccsLakesnightMonsters.categories[0].cards[4].minimumStageCompletions = 2; //Imp
            dccsLakesnightMonsters.categories[0].cards[5].minimumStageCompletions = 2; //Halc

            dccsLakesnightMonsters.categories[1].cards[2].minimumStageCompletions = 2; //Elder Lemurian
            dccsLakesnightMonsters.categories[1].cards[3].minimumStageCompletions = 4; //Void Reaver

            dccsLakesnightInteractables_DLC1.categories[0].cards[0].minimumStageCompletions = 1; //Void Camp

            dccsVillageNightMonsters_Additional.categories[0].cards[1].minimumStageCompletions = 1; //Grave
 
        }


        public static void LakesNight_AddStage1FriendlyMonsters(DirectorCardCategorySelection dccs)
        {
            if (dccs == null)
            {
                return;
            }
            //Technically, Beetle Queen,
 
        }
 

    }
}