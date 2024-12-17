using System.Reflection;
using DuckGame;
using DGRFix.PatchSystem;

namespace DGRFix
{

    /// <summary>
    /// The very core class, the actual mod class. Handles some internal functionality and basic calls to load the rest of the mod.
    /// </summary>
    public class TheMod : ClientMod
    {

        /// <summary>
        /// The duck-game generated mod config. This holds useful information and settings about the mod.
        /// </summary>
        public static ModConfiguration Config;

        public static string ReplaceData
        {
            get
            {
                var result = !Config.isWorkshop ? "LOCAL" : SteamIdField.GetValue(Config, new object[0]).ToString();
                return result;
            }
        }

        public static bool Disabled
        {
            get => (bool)DisabledField.GetValue(Config, new object[0]);
            set => DisabledField.SetValue(Config, value, new object[0]);
        }

        private static readonly PropertyInfo SteamIdField =
            typeof(ModConfiguration).GetProperty("workshopID", BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly PropertyInfo DisabledField =
            typeof(ModConfiguration).GetProperty("disabled", BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        /// Called by Duck Game while this mod is being loaded.
        /// </summary>
        protected override void OnPreInitialize()
        {
            Config = configuration;
            DependencyResolver.ResolveDependencies();
        }

        /// <summary>
        /// Called by Duck Game after all mods have been loaded.
        /// </summary>
        protected override void OnPostInitialize()
        {
            AutoPatchHandler.Patch();
        }
    }
}
