using Cadence.Models.Slot;

namespace Cadence.Models.Symbols;

public class SimpleSymbol(List<SlotCoordinate> slotCoordinates) : SymbolBase(slotCoordinates)
{
    public override bool IsSpecialSymbol => false;
    public override bool IsPayingSymbol => true;
}