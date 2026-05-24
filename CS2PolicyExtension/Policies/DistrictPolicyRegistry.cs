using Game.Prefabs;

namespace CS2PolicyExtension.Policies
{
    internal static class DistrictPolicyRegistry
    {
        public static int RegisterAll(PrefabSystem prefabSystem, DistrictPolicyDefinition[] policies)
        {
            var registeredCount = 0;

            foreach (var policy in policies)
            {
                if (Register(prefabSystem, policy))
                {
                    registeredCount++;
                }
            }

            return registeredCount;
        }

        private static bool Register(PrefabSystem prefabSystem, DistrictPolicyDefinition policy)
        {
            var prefabId = new PrefabID(nameof(PolicyTogglePrefab), policy.PrefabName);
            if (prefabSystem.TryGetPrefab(prefabId, out PrefabBase _))
            {
                Mod.log.Info($"{policy.PrefabName} already registered");
                return false;
            }

            var prefab = PrefabBase.Create<PolicyTogglePrefab>(policy.PrefabName);
            prefab.m_Category = policy.Category;
            prefab.m_Visibility = PolicyVisibility.Default;

            var uiObject = prefab.AddComponent<UIObject>();
            uiObject.m_Icon = policy.Icon;
            uiObject.m_Priority = policy.Priority;

            var modifiers = prefab.AddComponent<DistrictModifiers>();
            modifiers.m_Modifiers = policy.Modifiers;

            prefabSystem.AddOrUpdatePrefab(prefab);
            Mod.log.Info($"Registered district policy prefab {policy.PrefabName}");
            return true;
        }
    }
}
