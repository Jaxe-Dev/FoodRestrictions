using HarmonyLib;
using RimWorld;

namespace FoodRestrictions.Patch
{
    [HarmonyPatch(typeof(Pawn_FoodRestrictionTracker), "Configurable", MethodType.Getter)]
    internal static class RimWorld_Pawn_FoodRestrictionTracker_Configurable
    {
        private static bool Prefix(ref Pawn_FoodRestrictionTracker __instance, ref bool __result)
        {
            __result = !__instance.pawn.Destroyed && ((__instance.pawn.Faction == Faction.OfPlayer) || (__instance.pawn.HostFaction == Faction.OfPlayer));
            return false;
        }
    }
}
