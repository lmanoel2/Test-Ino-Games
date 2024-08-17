using SlotMachineChallenge.Models.Config;
using SlotMachineChallenge.Models.Round;

namespace SlotMachineChallenge.Models.Machine;

public class SlotMachineCadence(AnticipatorConfig config) : MachineBase(Enumerators.Machine.Machine.SlotMachine)
{
    public AnticipatorConfig Config { get; init; } = config;

    public RoundsSymbols? RoundsSymbols { get; set; } 
    
    public void AddRounds(RoundsSymbols roundsSymbols)
    {
        RoundsSymbols = roundsSymbols;
    }

    public void CleanRounds()
    {
        RoundsSymbols = null;
    }
}
