﻿using System;
using BattleTech;
using MechEngineer.Features.HardpointFix.Public;

namespace MechEngineer.Features.HardpointFix.Patches;

[HarmonyPatch(typeof(MechHardpointRules), nameof(MechHardpointRules.GetComponentPrefabName))]
public static class MechHardpointRules_GetComponentPrefabName_Patch
{
    [HarmonyPriority(Priority.High)]
    [HarmonyPrefix]
    public static bool Prefix(BaseComponentRef componentRef, ref string? __result)
    {
        try
        {
            if (CalculatorSetup.SharedCalculator != null)
            {
                __result = CalculatorSetup.SharedCalculator.GetPrefabName(componentRef);
                return false;
            }
        }
        catch (Exception e)
        {
            Log.Main.Error?.Log(e);
        }
        return true;
    }

    [HarmonyPostfix]
    public static void Postfix(BaseComponentRef componentRef, ref string __result)
    {
        try
        {
            Log.Main.Trace?.Log($"GetComponentPrefabName prefabName={__result} ComponentDefID={componentRef.ComponentDefID} PrefabIdentifier={componentRef.Def.PrefabIdentifier}");
        }
        catch (Exception e)
        {
            Log.Main.Error?.Log(e);
        }
    }
}
