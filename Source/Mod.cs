using HarmonyLib;
using Verse;

namespace FoodRestrictions
{
    [StaticConstructorOnStartup]
    internal static class Mod
    {
        public const string Id = "FoodRestrictions";
        public const string Name = "Food Restrictions";
        public const string Version = "1.1";

        static Mod()
        {
            new Harmony(Id).PatchAll();
            Log("Initialized");
        }

        public static void Log(string message) => Verse.Log.Message(PrefixMessage(message));
        private static string PrefixMessage(string message) => $"[{Name} v{Version}] {message}";

        public class Exception : System.Exception
        {
            public Exception(string message) : base($"[{Name} : EXCEPTION] {message}")
            { }
        }
    }
}
