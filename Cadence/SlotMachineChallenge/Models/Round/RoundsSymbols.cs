using SlotMachineChallenge.Models.Symbols;

namespace SlotMachineChallenge.Models.Round;

public class RoundsSymbols(SymbolBase roundOne, SymbolBase roundTwo, SymbolBase roundThree)
{
    public SymbolBase RoundOne { get; set; } = roundOne;
    public SymbolBase RoundTwo { get; set; } = roundTwo;
    public SymbolBase RoundThree { get; set; } = roundThree;
}