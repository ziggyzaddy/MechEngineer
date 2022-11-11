﻿using System;
using BattleTech;
using Harmony;
using MechEngineer.Features.OverrideStatTooltips.Helper;

namespace MechEngineer.Features.OverrideStatTooltips.Patches;

[HarmonyPatch(typeof(MechStatisticsRules), nameof(MechStatisticsRules.CalculateChassisMovementStat))]
public static class MechStatisticsRules_CalculateChassisMovementStat_Patch
{
    [HarmonyPrefix]
    public static bool Prefix(ref float currentValue, ref float maxValue)
    {
        try
        {
            MechStatUtils.SetStatValues(0, ref currentValue, ref maxValue);
            return false;
        }
        catch (Exception e)
        {
            Logging.Error?.Log(e);
        }
        return true;
    }
}