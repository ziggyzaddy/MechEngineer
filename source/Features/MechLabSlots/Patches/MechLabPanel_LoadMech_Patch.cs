﻿using System;
using BattleTech.UI;

namespace MechEngineer.Features.MechLabSlots.Patches;

[HarmonyPatch(typeof(MechLabPanel), nameof(MechLabPanel.LoadMech))]
public static class MechLabPanel_LoadMech_Patch
{
    [HarmonyPostfix]
    public static void Postfix(MechLabPanel __instance)
    {
        try
        {
            MechLabAutoZoom.LoadMech(__instance);
        }
        catch (Exception e)
        {
            Log.Main.Error?.Log(e);
        }
    }
}
