using Cadence.Interfaces.Config;
using Cadence.Interfaces.Machine;
using Cadence.Models.Config;
using Cadence.Models.Machine;

namespace Cadence.Factories.Machine;

public static class MachineFactory
{
    public static IMachineBase GetMachine(Enumerators.Machine.Machine machine, IConfigBase config)
    {
        return machine switch
        {
            Enumerators.Machine.Machine.SlotMachine => new SlotMachineCadence((AnticipatorConfig) config),
            _ => throw new InvalidOperationException("Not found machine")
        };
    }
}