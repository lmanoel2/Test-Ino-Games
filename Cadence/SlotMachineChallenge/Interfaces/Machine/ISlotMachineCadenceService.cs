using SlotMachineChallenge.Models.Results;
using SlotMachineChallenge.Models.Round;

namespace SlotMachineChallenge.Interfaces.Machine;

public interface ISlotMachineCadenceService
{
    public RoundsCadences HandleCadences();
    public List<WinningCombinationsResult> CalculateLineWinningCombinations(int[] lineSymbols);
}