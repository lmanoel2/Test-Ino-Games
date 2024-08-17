using Cadence.Models.Round;

namespace Cadence.Interfaces.Machine;

public interface ISlotMachineCadenceService
{
    public RoundsCadences HandleCadences();
}