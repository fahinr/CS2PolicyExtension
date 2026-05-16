using Colossal.Mathematics;
using Game.Areas;
using Game.Prefabs;

namespace CS2PolicyExtension.Policies
{
    internal static class PublicWiFiPolicy
    {
        public const string PrefabName = "CS2PolicyExtension.PublicWiFi";

        public static bool Register(PrefabSystem prefabSystem)
        {
            var prefabId = new PrefabID(nameof(PolicyTogglePrefab), PrefabName);
            if (prefabSystem.TryGetPrefab(prefabId, out PrefabBase _))
            {
                Mod.log.Info($"{PrefabName} already registered");
                return false;
            }

            var prefab = PrefabBase.Create<PolicyTogglePrefab>(PrefabName);
            prefab.m_Category = PolicyCategory.Services;
            prefab.m_Visibility = PolicyVisibility.Default;

            var uiObject = prefab.AddComponent<UIObject>();
            uiObject.m_Icon = "Media/Game/Icons/Wellbeing.svg";
            uiObject.m_Priority = 100;

            var modifiers = prefab.AddComponent<DistrictModifiers>();
            modifiers.m_Modifiers = new[]
            {
                new DistrictModifierInfo
                {
                    m_Type = DistrictModifierType.Wellbeing,
                    m_Mode = ModifierValueMode.Relative,
                    m_Range = new Bounds1(0.01f, 0.01f)
                }
            };

            prefabSystem.AddOrUpdatePrefab(prefab);
            Mod.log.Info($"Registered district policy prefab {PrefabName}");
            return true;
        }
    }
}
