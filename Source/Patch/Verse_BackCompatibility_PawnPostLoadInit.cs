using Harmony;
using RimWorld;
using Verse;

namespace FoodRestrictions.Patch
{
    [HarmonyPatch(typeof(BackCompatibility), "PawnPostLoadInit")]
    internal static class Verse_BackCompatibility_PawnPostLoadInit
    {
        private static bool Prefix(Pawn p)
        {
            if (p.Spawned && (p.rotationTracker == null)) { p.rotationTracker = new Pawn_RotationTracker(p); }
            if (!p.Destroyed && !p.Dead && (p.needs == null))
            {
                Log.Error(p.ToStringSafe() + " has null needs tracker even though he's not dead. Fixing...");
                p.needs = new Pawn_NeedsTracker(p);
                p.needs.SetInitialLevels();
            }
            if ((p.foodRestriction == null) && (((p.Faction != null) && p.Faction.IsPlayer) || ((p.HostFaction != null) && p.HostFaction.IsPlayer))) { p.foodRestriction = new Pawn_FoodRestrictionTracker(p); }

            return false;
        }
    }
}
