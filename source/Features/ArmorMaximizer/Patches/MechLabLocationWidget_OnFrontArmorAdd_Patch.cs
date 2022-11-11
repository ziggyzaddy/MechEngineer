﻿using System;
using BattleTech.UI;
using Harmony;

namespace MechEngineer.Features.ArmorMaximizer.Patches;

[HarmonyPatch(typeof(MechLabLocationWidget), nameof(MechLabLocationWidget.OnFrontArmorAdd))]
public static class MechLabLocationWidget_OnFrontArmorAdd_Patch
{
    [HarmonyPrefix]
    public static bool Prefix(MechLabLocationWidget __instance)
    {
        try
        {
            ArmorMaximizerHandler.OnArmorAddOrSubtract(__instance, false, +1f);
            return false;
        }
        catch (Exception e)
        {
            Logging.Error?.Log(e);
        }
        return true;
    }
}
