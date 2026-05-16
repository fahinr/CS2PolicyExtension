using Colossal.Logging;
using CS2PolicyExtension.Policies;
using CS2PolicyExtension.Systems;
using Game;
using Game.Modding;
using Game.Prefabs;
using Game.SceneFlow;

namespace CS2PolicyExtension
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(CS2PolicyExtension)}.{nameof(Mod)}")
            .SetShowsErrorsInUI(false);

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            try
            {
                Localization.Register();

                var prefabSystem = updateSystem.World.GetOrCreateSystemManaged<PrefabSystem>();
                PublicWiFiPolicy.Register(prefabSystem);

                if (PublicWiFiDebugSystem.DebugLoggingEnabled)
                {
                    updateSystem.UpdateAt<PublicWiFiDebugSystem>(SystemUpdatePhase.GameSimulation);
                    log.Info("Enabled Public Wi-Fi debug system");
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex, "Failed to register policy prefabs");
            }
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
        }
    }
}
