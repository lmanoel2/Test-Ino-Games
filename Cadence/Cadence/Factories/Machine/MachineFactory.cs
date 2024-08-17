using Cadence.Interfaces.Machine;
using Cadence.Models.Machine;
using Cadence.Services.Machine;

namespace Cadence.Factories.Machine;

public static class MachineFactory
{
    public static ISlotMachineCadenceService GetMachineService(MachineBase machineBase)
    {
        return machineBase.MachineType switch
        {
            Enumerators.Machine.Machine.SlotMachine => new SlotMachineCadenceService((SlotMachineCadence) machineBase),
            _ => throw new InvalidOperationException($"Not found machine type {machineBase.MachineType}")
        };
    }
}