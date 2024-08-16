using Cadence.Models.Config;

namespace Cadence.Models.Machine;

public class SlotMachineCadence(AnticipatorConfig config) : MachineBase
{
    public AnticipatorConfig Config { get; init; } = config;
}

public class SlotCadence : List<double> { }

