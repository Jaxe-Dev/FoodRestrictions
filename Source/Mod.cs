using HarmonyLib;
using Verse;

namespace FoodRestrictions
{
  [StaticConstructorOnStartup]
  internal static class Mod
  {
    public const string Id = "FoodRestrictions";
    public const string Name = "Food Restrictions";
    public const string Version = "1.3";

    static Mod()
    {
      new Harmony(Id).PatchAll();
      Log("Initialized");
    }

    public static void Log(string message) => Verse.Log.Message(PrefixMessage(message));
    private static string PrefixMessage(string message) => $"[{Name} v{Version}] {message}";
  }
}
