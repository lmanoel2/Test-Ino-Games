using Cadence.Models.Round;

namespace Cadence.Interfaces.Machine;

public interface ISlotMachineCadenceService : IMachineService
{
    public RoundsCadences HandleCadences();
}