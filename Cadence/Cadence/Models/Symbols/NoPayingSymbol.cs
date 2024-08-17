using Cadence.Models.Slot;

namespace Cadence.Models.Symbols;

public class NoPayingSymbol(List<SlotCoordinate> slotCoordinates) : SymbolBase(slotCoordinates)
{
    public bool IsSpecialSymbol => false;
    public bool IsPayingSymbol => false;
}