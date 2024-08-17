using SlotMachineChallenge.Models.Slot;
using SlotMachineChallenge.Models.Symbols;

namespace SlotMachineChallenge.Factories.Symbol;

public static class SymbolFactory
{
    public static SymbolBase GetSymbol(int id)
    {
        return id switch
        {
            0 => new SpecialSymbol(new List<SlotCoordinate>()),
            >= 10 and <= 15 => new NoPayingSymbol(new List<SlotCoordinate>()),
            _ => new SimpleSymbol(new List<SlotCoordinate>())
        };
    }
}