﻿using BattleTech;

namespace MechEngineer.Features.CustomCapacities;

public class CustomCapacitiesSettings : ISettings
{
    public bool Enabled { get; set; } = true;
    public string EnabledDescription => "Enables some carry rules.";

    public string CarryHandErrorOverweight = "OVERWEIGHT: 'Mechs handheld carry weight exceeds maximum.";
    public string CarryHandErrorOneFreeHand = "OVERWEIGHT: 'Mechs handheld carry weight requires one free hand.";

    public CustomCapacity CarryWeight = new()
    {
        Description = new()
        {
            Id = "carry_weight",
            Name = "Carry Weight",
            Details = "Carry weight represents the total carry capacity of a mech on top of the normal chassis weight internal capacity." +
                      " Each hand actuator allows to carry an equivalent of up to 5% chassis maximum tonnage." +
                      " If a melee weapon is too heavy for a single arm, it can be held two-handed by combining both hands carry capacities.",
            Icon = "UixSvgIcon_specialEquip_Melee"
        },
        Format = "{0:0.#} / {1:0.#}",
        ErrorOverweight = "OVERWEIGHT: 'Mechs total carry weight exceeds maximum.",
        HideIfNoUsageAndCapacity = true
    };

    public CustomCapacity[] Capacities =
    {
        new()
        {
            Description = new()
            {
                Id = "special",
                Name = "e.g. Special",
                Details = "This is just an example on how you can define custom capacities on anything and use it up on anything." +
                          " Useful if you want some more knapsack problem solving gameplay.",
                Icon = "uixSvgIcon_ability_angelofdeath"
            },
            Format = "{0:0} / {1:0}",
            ErrorOverweight = "OVERUSE: 'Mechs special points exceeds maximum.",
            HideIfNoUsageAndCapacity = true
        }
    };

    public class CustomCapacity
    {
        public BaseDescriptionDef Description { get; set; } = null!;
        public string DescriptionDescription => "The description has the id that is referenced from the CapacityMod custom";

        public string Format { get; set; } = null!;
        public string ErrorOverweight { get; set; } = null!;

        public bool HideIfNoUsageAndCapacity { get; set; }
        public string HideIfNoUsageAndCapacityDescription => "Hides the capacity if usage and capacity amounts are 0.";
    }
}
