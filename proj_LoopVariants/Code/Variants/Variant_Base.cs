using RoR2;

namespace LoopVariants
{
    public class Variant_Base
    {

        public static bool ShouldAddLoopEnemies(DirectorCardCategorySelection dccs)
        {
            if (dccs == null)
            {
                return false;
            }
            if (RunArtifactManager.instance && RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.mixEnemyArtifactDef))
            {
                return false;
            }
            if (dccs && dccs is FamilyDirectorCardCategorySelection)
            {
                return false;
            }
            return true;
        }

    }
}