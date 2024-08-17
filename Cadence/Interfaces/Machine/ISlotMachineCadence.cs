using Cadence.Models.Round;

namespace Cadence.Interfaces.Machine;

public interface ISlotMachineCadence
{
    public void AddRounds(RoundsSymbols roundsSymbols);
    public void CleanRounds();
    public RoundsCadences HandleCadences();
}