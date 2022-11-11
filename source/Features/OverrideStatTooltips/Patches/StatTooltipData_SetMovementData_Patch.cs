﻿using BattleTech;
using Harmony;
using System;

namespace MechEngineer.Features.OverrideStatTooltips.Patches;

[HarmonyPatch(typeof(StatTooltipData), nameof(StatTooltipData.SetMovementData))]
public static class StatTooltipData_SetMovementData_Patch
{
    [HarmonyPostfix]
    public static void Postfix(StatTooltipData __instance, MechDef def)
    {
        try
        {
            OverrideStatTooltipsFeature.MovementStat.SetupTooltip(__instance, def);
        }
        catch (Exception e)
        {
            Logging.Error?.Log(e);
        }
    }
}