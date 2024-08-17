using SlotMachineChallenge.Interfaces.Machine;
using SlotMachineChallenge.Models.Machine;
using SlotMachineChallenge.Services.Machine;

namespace SlotMachineChallenge.Factories.Machine;

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