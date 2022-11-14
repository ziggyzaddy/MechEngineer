﻿using System.Collections.Generic;
using CustomComponents;

namespace MechEngineer.Features.CustomCapacities.Aliases;

[CustomComponent("CarryLeftOverCapacity")]
public class CarryLeftOverCapacityCustom : SimpleCustomComponent, IValueComponent<float>, IAfterLoad
{
    private float Value;

    public void LoadValue(float value)
    {
        Value = value;
    }

    public void OnLoaded(Dictionary<string, object> values)
    {
        Def.AddComponent(new CapacityModCustom
            {
                Collection = CustomCapacitiesFeature.CarryLeftOverCollectionId,
                Quantity = Value
            }
        );
    }
}
