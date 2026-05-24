using Game.Prefabs;

namespace CS2PolicyExtension.Policies
{
    internal sealed class DistrictPolicyDefinition
    {
        public string PrefabName;
        public string Title;
        public string Description;
        public PolicyCategory Category;
        public string Icon;
        public int Priority;
        public DistrictModifierInfo[] Modifiers;
    }
}
