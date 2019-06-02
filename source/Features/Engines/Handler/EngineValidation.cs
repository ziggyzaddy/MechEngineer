﻿using System.Collections.Generic;
using System.Linq;
using BattleTech;
using CustomComponents;
using MechEngineer.Features.Engines.Helper;

namespace MechEngineer.Features.Engines.Handler
{
    internal class EngineValidation : IValidateMech
    {
        internal static EngineValidation Shared = new EngineValidation();
        internal CCValidationAdapter CCValidation;

        internal EngineValidation()
        {
            CCValidation = new CCValidationAdapter(this);
        }

        public void ValidateMech(MechDef mechDef, Errors errors)
        {
            var engine = mechDef.GetEngine();
            if (engine == null)
            {
                return;
            }

            {
                var count = mechDef.Inventory.Count(c => c.ComponentDefType == ComponentType.JumpJet);
                var max = engine.CoreDef.GetMovement(mechDef.Chassis.Tonnage).JumpJetCount;

                if (count > max)
                {
                    if (errors.Add(MechValidationType.InvalidJumpjets, $"JUMP JETS: This Mech mounts too many jumpjets ({count} / {max})"))
                    {
                        return;
                    }
                }
            }

            if (EngineFeature.settings.MinimumHeatSinksOnMech > 0)
            {
                var externalCount = mechDef.Inventory.Count(c => c.Is<EngineHeatSinkDef>());
                var internalCount = engine.CoreDef.InternalHeatSinks;
                var count = internalCount + externalCount;

                var min = EngineFeature.settings.MinimumHeatSinksOnMech;

                if (count < min)
                {
                    if (errors.Add(MechValidationType.InvalidInventorySlots, $"HEAT SINKS: This Mech has too few heat sinks ({count} / {min})"))
                    {
                        return;
                    }
                }
            }

            if (!EngineFeature.settings.AllowMixingHeatSinkTypes)
            {
                var types = new HashSet<string>();

                var inventoryHeatSinkTypes = mechDef.Inventory.Select(r => r.GetComponent<EngineHeatSinkDef>()).Where(hs => hs != null).ToList();
                inventoryHeatSinkTypes.Add(engine.GetInternalEngineHeatSinkTypes());
                foreach (var hs in inventoryHeatSinkTypes)
                {
                    types.Add(hs.HSCategory);
                    if (types.Count <= 1)
                    {
                        continue;
                    }

                    if (errors.Add(MechValidationType.InvalidInventorySlots, "HEAT SINKS: Heat Sink types cannot be mixed"))
                    {
                        return;
                    }
                }
            }

            if (EngineFeature.settings.EnforceRulesForAdditionalInternalHeatSinks)
            {
                var count = engine.HeatBlockDef.HeatSinkCount;
                var max = engine.CoreDef.InternalHeatSinkAdditionalMaxCount;
                if (count > max)
                {
                    if (errors.Add(MechValidationType.InvalidInventorySlots, $"HEAT SINKS: This Mech has too many internal heat sinks ({count} / {max})"))
                    {
                        return;
                    }
                }
            }
        }
    }
}