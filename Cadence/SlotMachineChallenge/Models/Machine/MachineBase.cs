namespace SlotMachineChallenge.Models.Machine;

public abstract class MachineBase(Enumerators.Machine.Machine machineType)
{
    public Enumerators.Machine.Machine MachineType { get; init; } = machineType;
}