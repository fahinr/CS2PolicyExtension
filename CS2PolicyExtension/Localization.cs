using System.Collections.Generic;
using Colossal.Localization;
using CS2PolicyExtension.Policies;
using Game.SceneFlow;

namespace CS2PolicyExtension
{
    internal static class Localization
    {
        private static readonly MemorySource Source = new MemorySource(CreateEntries());

        private static Dictionary<string, string> CreateEntries()
        {
            var entries = new Dictionary<string, string>();

            foreach (var policy in PolicyDefinitions.All)
            {
                entries.Add($"Policy.TITLE[{policy.PrefabName}]", policy.Title);
                entries.Add($"Policy.DESCRIPTION[{policy.PrefabName}]", policy.Description);
            }

            return entries;
        }

        public static void Register()
        {
            var localizationManager = GameManager.instance.localizationManager;
            localizationManager.AddSource(localizationManager.activeLocaleId, Source);

            if (localizationManager.fallbackLocaleId != localizationManager.activeLocaleId)
            {
                localizationManager.AddSource(localizationManager.fallbackLocaleId, Source);
            }

            Mod.log.Info("Registered localization entries");
        }
    }
}
