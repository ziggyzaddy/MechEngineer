﻿using System;
using BattleTech;

namespace MechEngineer.Features.OverrideStatTooltips.Patches;

[HarmonyPatch(typeof(StatTooltipData), nameof(StatTooltipData.SetHeatData))]
public static class StatTooltipData_SetHeatData_Patch
{
    [HarmonyPostfix]
    public static void Postfix(StatTooltipData __instance, MechDef def)
    {
        try
        {
            OverrideStatTooltipsFeature.HeatEfficiencyStat.SetupTooltip(__instance, def);
        }
        catch (Exception e)
        {
            Log.Main.Error?.Log(e);
        }
    }
}
