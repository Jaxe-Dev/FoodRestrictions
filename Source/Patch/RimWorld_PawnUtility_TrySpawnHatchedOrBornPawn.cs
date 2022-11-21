using HarmonyLib;
using RimWorld;
using Verse;

namespace FoodRestrictions.Patch
{
    [HarmonyPatch(typeof(PawnUtility), nameof(PawnUtility.TrySpawnHatchedOrBornPawn))]
    internal static class RimWorld_PawnUtility_TrySpawnHatchedOrBornPawn
    {
        
        private static void Postfix(Pawn pawn,
            Thing motherOrEgg,
            IntVec3? positionOverride = null)
        {
            // if TrySpawnHatchedOrBornPawn is called by mammal animal then motherOrEgg is <Pawn mother>
            // if TrySpawnHatchedOrBornPawn is called by hatched egg then motherOrEgg is <ThingWithComps egg>
            // if TrySpawnHatchedOrBornPawn is called by humanlike then.. it could be several <Thing>s - we don't care
            if (pawn.RaceProps.Humanlike ||
                ((!pawn.Faction?.IsPlayer ?? true) && (!pawn.HostFaction?.IsPlayer ?? true)))
                return;

            switch (motherOrEgg)
            {
                case Pawn mother when mother.foodRestriction != null:
                    pawn.foodRestriction.CurrentFoodRestriction = mother.foodRestriction.CurrentFoodRestriction;
                    break;
                case ThingWithComps egg:
                {
                    var compHatcher = egg.TryGetComp<CompHatcher>();
                    if (compHatcher?.hatcheeParent.foodRestriction != null)
                    {
                        pawn.foodRestriction.CurrentFoodRestriction = compHatcher.hatcheeParent.foodRestriction.CurrentFoodRestriction;    
                    }

                    break;
                }
            }
        }
    }
}