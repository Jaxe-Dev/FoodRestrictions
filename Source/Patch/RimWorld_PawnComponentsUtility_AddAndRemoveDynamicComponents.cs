using Harmony;
using RimWorld;
using Verse;

namespace FoodRestrictions.Patch
{
    [HarmonyPatch(typeof(PawnComponentsUtility), "AddAndRemoveDynamicComponents")]
    internal static class RimWorld_PawnComponentsUtility_AddAndRemoveDynamicComponents
    {
        private static void Postfix(Pawn pawn)
        {
            if (pawn.RaceProps.Humanlike || ((!pawn.Faction?.IsPlayer ?? true) && (!pawn.HostFaction?.IsPlayer ?? true)) || (pawn.foodRestriction != null)) { return; }

            pawn.foodRestriction = new Pawn_FoodRestrictionTracker(pawn);
        }
    }
}
