using Colossal.Logging;
using Colossal.UI;
using CS2PolicyExtension.Policies;
using Game;
using Game.Modding;
using Game.Prefabs;
using Game.SceneFlow;
using System.IO;

namespace CS2PolicyExtension
{
    public class Mod : IMod
    {
        private const string UIHost = "cs2policyextension";

        public static ILog log = LogManager.GetLogger($"{nameof(CS2PolicyExtension)}.{nameof(Mod)}")
            .SetShowsErrorsInUI(false);

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
            {
                log.Info($"Current mod asset at {asset.path}");
                RegisterUIHost(asset.path);
            }

            try
            {
                Localization.Register();

                var prefabSystem = updateSystem.World.GetOrCreateSystemManaged<PrefabSystem>();
                var registeredCount = DistrictPolicyRegistry.RegisterAll(prefabSystem, PolicyDefinitions.All);
                log.Info($"Registered {registeredCount} district policy prefabs");
            }
            catch (System.Exception ex)
            {
                log.Error(ex, "Failed to register policy prefabs");
            }
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
            UIManager.defaultUISystem?.RemoveHostLocation(UIHost);
        }

        private static void RegisterUIHost(string assetPath)
        {
            var modDirectory = Path.GetDirectoryName(assetPath);
            if (string.IsNullOrEmpty(modDirectory))
            {
                log.Warn("Unable to resolve mod directory for UI assets");
                return;
            }

            var uiDirectory = Path.Combine(modDirectory, "UI");
            if (!Directory.Exists(uiDirectory))
            {
                log.Warn($"UI asset directory does not exist: {uiDirectory}");
                return;
            }

            if (UIManager.defaultUISystem == null)
            {
                log.Warn("Unable to register UI asset host because the default UI system is not available");
                return;
            }

            UIManager.defaultUISystem.AddHostLocation(UIHost, uiDirectory);
            log.Info($"Registered UI asset host {UIHost} at {uiDirectory}");
        }
    }
}
