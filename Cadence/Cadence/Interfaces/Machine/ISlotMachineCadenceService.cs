using Cadence.Models.Results;
using Cadence.Models.Round;

namespace Cadence.Interfaces.Machine;

public interface ISlotMachineCadenceService
{
    public RoundsCadences HandleCadences();
    public List<WinningCombinationsResult> CalculateLineWinningCombinations(int[] lineSymbols);
}