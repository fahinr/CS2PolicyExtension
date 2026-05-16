using System.Collections.Generic;
using Colossal.Localization;
using Game.SceneFlow;

namespace CS2PolicyExtension
{
    internal static class Localization
    {
        private static readonly MemorySource Source = new MemorySource(new Dictionary<string, string>
        {
            { "Policy.TITLE[CS2PolicyExtension.PublicWiFi]", "Public Wi-Fi" },
            { "Policy.DESCRIPTION[CS2PolicyExtension.PublicWiFi]", "Provides free public Wi-Fi access in the district, slightly improving citizen wellbeing." }
        });

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
