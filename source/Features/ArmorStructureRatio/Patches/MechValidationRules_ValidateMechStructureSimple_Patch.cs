﻿using System;
using BattleTech;

namespace MechEngineer.Features.ArmorStructureRatio.Patches;

[HarmonyPatch(typeof(MechValidationRules), nameof(MechValidationRules.ValidateMechStructureSimple))]
public static class MechValidationRules_ValidateMechStructureSimple_Patch
{
    [HarmonyPostfix]
    public static void Postfix(MechDef mechDef)
    {
        try
        {
            ArmorStructureRatioFeature.ValidateMechArmorStructureRatio(mechDef);
        }
        catch (Exception e)
        {
            Log.Main.Error?.Log(e);
        }
    }
}
