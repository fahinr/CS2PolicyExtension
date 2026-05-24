using Colossal.Mathematics;
using Game.Areas;
using Game.Prefabs;

namespace CS2PolicyExtension.Policies
{
    internal static class PolicyDefinitions
    {
        public static readonly DistrictPolicyDefinition[] All =
        {
            new DistrictPolicyDefinition
            {
                PrefabName = "CS2PolicyExtension.PublicWiFi",
                Title = "Public Wi-Fi",
                Description = "Provides free public Wi-Fi access, slightly improving wellbeing while increasing building upkeep.",
                Category = PolicyCategory.Services,
                Icon = "coui://cs2policyextension/Media/CS2PolicyExtension/Icons/PublicWiFi.svg",
                Priority = 100,
                Modifiers = new[]
                {
                    Modifier(DistrictModifierType.Wellbeing, 0.01f),
                    Modifier(DistrictModifierType.BuildingUpkeep, 0.02f)
                }
            },
            new DistrictPolicyDefinition
            {
                PrefabName = "CS2PolicyExtension.NightlifeDistrict",
                Title = "Nightlife District",
                Description = "Encourages late-night activity and local spending, but increases garbage and crime pressure.",
                Category = PolicyCategory.Culture,
                Icon = "coui://cs2policyextension/Media/CS2PolicyExtension/Icons/NightlifeDistrict.svg",
                Priority = 110,
                Modifiers = new[]
                {
                    Modifier(DistrictModifierType.Wellbeing, 0.015f),
                    Modifier(DistrictModifierType.ProductConsumption, 0.05f),
                    Modifier(DistrictModifierType.GarbageProduction, 0.05f),
                    Modifier(DistrictModifierType.CrimeAccumulation, 0.03f)
                }
            },
            new DistrictPolicyDefinition
            {
                PrefabName = "CS2PolicyExtension.NeighborhoodWatch",
                Title = "Neighborhood Watch",
                Description = "Improves local safety through community patrols, reducing crime with a small upkeep cost.",
                Category = PolicyCategory.Services,
                Icon = "coui://cs2policyextension/Media/CS2PolicyExtension/Icons/NeighborhoodWatch.svg",
                Priority = 120,
                Modifiers = new[]
                {
                    Modifier(DistrictModifierType.CrimeAccumulation, -0.08f),
                    Modifier(DistrictModifierType.Wellbeing, 0.005f),
                    Modifier(DistrictModifierType.BuildingUpkeep, 0.01f)
                }
            },
            new DistrictPolicyDefinition
            {
                PrefabName = "CS2PolicyExtension.GreenBuildingStandards",
                Title = "Green Building Standards",
                Description = "Promotes efficient buildings and waste reduction, increasing maintenance and compliance costs.",
                Category = PolicyCategory.CityPlanning,
                Icon = "coui://cs2policyextension/Media/CS2PolicyExtension/Icons/GreenBuildingStandards.svg",
                Priority = 130,
                Modifiers = new[]
                {
                    Modifier(DistrictModifierType.EnergyConsumptionAwareness, 0.08f),
                    Modifier(DistrictModifierType.GarbageProduction, -0.04f),
                    Modifier(DistrictModifierType.BuildingUpkeep, 0.02f)
                }
            },
            new DistrictPolicyDefinition
            {
                PrefabName = "CS2PolicyExtension.PedestrianPriorityZone",
                Title = "Pedestrian Priority Zone",
                Description = "Slows streets and improves traffic safety while encouraging walking and cycling over car use.",
                Category = PolicyCategory.Traffic,
                Icon = "coui://cs2policyextension/Media/CS2PolicyExtension/Icons/PedestrianPriorityZone.svg",
                Priority = 140,
                Modifiers = new[]
                {
                    Modifier(DistrictModifierType.StreetSpeedLimit, -0.15f),
                    Modifier(DistrictModifierType.StreetTrafficSafety, 0.08f),
                    Modifier(DistrictModifierType.CarReserveProbability, -0.05f),
                    Modifier(DistrictModifierType.BikeProbability, 0.05f)
                }
            },
            new DistrictPolicyDefinition
            {
                PrefabName = "CS2PolicyExtension.BusinessImprovementDistrict",
                Title = "Business Improvement District",
                Description = "Boosts commercial spending with district improvements, increasing upkeep and garbage output.",
                Category = PolicyCategory.Budget,
                Icon = "coui://cs2policyextension/Media/CS2PolicyExtension/Icons/BusinessImprovementDistrict.svg",
                Priority = 150,
                Modifiers = new[]
                {
                    Modifier(DistrictModifierType.ProductConsumption, 0.06f),
                    Modifier(DistrictModifierType.LowCommercialTax, 0.03f),
                    Modifier(DistrictModifierType.BuildingUpkeep, 0.015f),
                    Modifier(DistrictModifierType.GarbageProduction, 0.025f)
                }
            },
            new DistrictPolicyDefinition
            {
                PrefabName = "CS2PolicyExtension.LowEmissionZone",
                Title = "Low Emission Zone",
                Description = "Discourages car trips and encourages cycling with slower streets and modest compliance costs.",
                Category = PolicyCategory.Traffic,
                Icon = "coui://cs2policyextension/Media/CS2PolicyExtension/Icons/LowEmissionZone.svg",
                Priority = 160,
                Modifiers = new[]
                {
                    Modifier(DistrictModifierType.CarReserveProbability, -0.08f),
                    Modifier(DistrictModifierType.BikeProbability, 0.06f),
                    Modifier(DistrictModifierType.StreetSpeedLimit, -0.08f),
                    Modifier(DistrictModifierType.BuildingUpkeep, 0.01f)
                }
            },
            new DistrictPolicyDefinition
            {
                PrefabName = "CS2PolicyExtension.ForestryFirebreaks",
                Title = "Forestry Firebreaks",
                Description = "Requires defensible space and vegetation management, reducing fire hazard with added upkeep.",
                Category = PolicyCategory.Services,
                Icon = "coui://cs2policyextension/Media/CS2PolicyExtension/Icons/ForestryFirebreaks.svg",
                Priority = 170,
                Modifiers = new[]
                {
                    Modifier(DistrictModifierType.BuildingFireHazard, -0.08f),
                    Modifier(DistrictModifierType.BuildingUpkeep, 0.015f)
                }
            },
            new DistrictPolicyDefinition
            {
                PrefabName = "CS2PolicyExtension.VolunteerFireBrigade",
                Title = "Volunteer Fire Brigade",
                Description = "Organizes local emergency volunteers, improving fire response and lowering fire risk at a small cost.",
                Category = PolicyCategory.Services,
                Icon = "coui://cs2policyextension/Media/CS2PolicyExtension/Icons/VolunteerFireBrigade.svg",
                Priority = 180,
                Modifiers = new[]
                {
                    Modifier(DistrictModifierType.BuildingFireResponseTime, -0.1f),
                    Modifier(DistrictModifierType.BuildingFireHazard, -0.04f),
                    Modifier(DistrictModifierType.BuildingUpkeep, 0.01f)
                }
            },
            new DistrictPolicyDefinition
            {
                PrefabName = "CS2PolicyExtension.RuralRoadSafety",
                Title = "Rural Road Safety",
                Description = "Adds calmer rural road rules, reducing speeds and improving traffic safety with minor upkeep.",
                Category = PolicyCategory.Traffic,
                Icon = "coui://cs2policyextension/Media/CS2PolicyExtension/Icons/RuralRoadSafety.svg",
                Priority = 190,
                Modifiers = new[]
                {
                    Modifier(DistrictModifierType.StreetSpeedLimit, -0.1f),
                    Modifier(DistrictModifierType.StreetTrafficSafety, 0.1f),
                    Modifier(DistrictModifierType.BuildingUpkeep, 0.005f)
                }
            },
            new DistrictPolicyDefinition
            {
                PrefabName = "CS2PolicyExtension.FarmWasteComposting",
                Title = "Farm Waste Composting",
                Description = "Encourages composting and local waste handling, reducing garbage output with modest upkeep.",
                Category = PolicyCategory.CityPlanning,
                Icon = "coui://cs2policyextension/Media/CS2PolicyExtension/Icons/FarmWasteComposting.svg",
                Priority = 200,
                Modifiers = new[]
                {
                    Modifier(DistrictModifierType.GarbageProduction, -0.06f),
                    Modifier(DistrictModifierType.BuildingUpkeep, 0.01f)
                }
            },
            new DistrictPolicyDefinition
            {
                PrefabName = "CS2PolicyExtension.CountryLivingPromotion",
                Title = "Country Living Promotion",
                Description = "Promotes quiet rural living, improving wellbeing while increasing reliance on private cars.",
                Category = PolicyCategory.Culture,
                Icon = "coui://cs2policyextension/Media/CS2PolicyExtension/Icons/CountryLivingPromotion.svg",
                Priority = 210,
                Modifiers = new[]
                {
                    Modifier(DistrictModifierType.Wellbeing, 0.012f),
                    Modifier(DistrictModifierType.CarReserveProbability, 0.04f),
                    Modifier(DistrictModifierType.ProductConsumption, -0.02f)
                }
            },
            new DistrictPolicyDefinition
            {
                PrefabName = "CS2PolicyExtension.LocalMarketDays",
                Title = "Local Market Days",
                Description = "Supports periodic rural markets, increasing local spending and wellbeing with extra cleanup needs.",
                Category = PolicyCategory.Culture,
                Icon = "coui://cs2policyextension/Media/CS2PolicyExtension/Icons/LocalMarketDays.svg",
                Priority = 220,
                Modifiers = new[]
                {
                    Modifier(DistrictModifierType.ProductConsumption, 0.04f),
                    Modifier(DistrictModifierType.Wellbeing, 0.006f),
                    Modifier(DistrictModifierType.GarbageProduction, 0.025f),
                    Modifier(DistrictModifierType.StreetTrafficSafety, -0.02f)
                }
            }
        };

        private static DistrictModifierInfo Modifier(DistrictModifierType type, float relativeValue)
        {
            return new DistrictModifierInfo
            {
                m_Type = type,
                m_Mode = ModifierValueMode.Relative,
                m_Range = new Bounds1(relativeValue, relativeValue)
            };
        }
    }
}
