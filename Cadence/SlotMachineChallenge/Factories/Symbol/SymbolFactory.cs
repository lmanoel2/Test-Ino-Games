using Cadence.Models.Slot;
using Cadence.Models.Symbols;

namespace Cadence.Factories.Symbol;

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