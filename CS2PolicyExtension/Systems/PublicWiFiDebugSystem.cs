using CS2PolicyExtension.Policies;
using Game;
using Game.Areas;
using Game.Policies;
using Game.Prefabs;
using Unity.Collections;
using Unity.Entities;

namespace CS2PolicyExtension.Systems
{
    internal partial class PublicWiFiDebugSystem : GameSystemBase
    {
        public static readonly bool DebugLoggingEnabled = true;

        private EntityQuery _districtQuery;
        private PrefabSystem _prefabSystem;
        private bool _loggedMissingPrefab;
        private int _lastActiveDistrictCount = -1;
        private float _lastWellbeingRelativeDelta = float.NaN;

        protected override void OnCreate()
        {
            base.OnCreate();

            _prefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            _districtQuery = GetEntityQuery(
                ComponentType.ReadOnly<District>(),
                ComponentType.ReadOnly<Policy>(),
                ComponentType.ReadOnly<DistrictModifier>());
        }

        protected override void OnUpdate()
        {
            if (!DebugLoggingEnabled)
            {
                return;
            }

            var prefabId = new PrefabID(nameof(PolicyTogglePrefab), PublicWiFiPolicy.PrefabName);
            if (!_prefabSystem.TryGetPrefab(prefabId, out PrefabBase prefab))
            {
                if (!_loggedMissingPrefab)
                {
                    Mod.log.Info("Public Wi-Fi debug: policy prefab not registered yet");
                    _loggedMissingPrefab = true;
                }

                return;
            }

            var policyEntity = _prefabSystem.GetEntity(prefab);
            var districtEntities = _districtQuery.ToEntityArray(Allocator.Temp);

            try
            {
                var activeDistrictCount = 0;
                var wellbeingRelativeDelta = 0f;

                for (var i = 0; i < districtEntities.Length; i++)
                {
                    var districtEntity = districtEntities[i];
                    var policies = EntityManager.GetBuffer<Policy>(districtEntity, true);
                    var hasPublicWiFi = false;

                    for (var j = 0; j < policies.Length; j++)
                    {
                        if (policies[j].m_Policy == policyEntity &&
                            (policies[j].m_Flags & PolicyFlags.Active) != 0)
                        {
                            hasPublicWiFi = true;
                            break;
                        }
                    }

                    if (!hasPublicWiFi)
                    {
                        continue;
                    }

                    activeDistrictCount++;

                    var modifiers = EntityManager.GetBuffer<DistrictModifier>(districtEntity, true);
                    var wellbeingIndex = (int)DistrictModifierType.Wellbeing;
                    if (modifiers.Length > wellbeingIndex)
                    {
                        wellbeingRelativeDelta += modifiers[wellbeingIndex].m_Delta.y;
                    }
                }

                if (activeDistrictCount != _lastActiveDistrictCount ||
                    wellbeingRelativeDelta != _lastWellbeingRelativeDelta)
                {
                    Mod.log.Info(
                        $"Public Wi-Fi debug: activeDistricts={activeDistrictCount}, combinedWellbeingRelativeDelta={wellbeingRelativeDelta}");

                    _lastActiveDistrictCount = activeDistrictCount;
                    _lastWellbeingRelativeDelta = wellbeingRelativeDelta;
                }
            }
            finally
            {
                districtEntities.Dispose();
            }
        }
    }
}
