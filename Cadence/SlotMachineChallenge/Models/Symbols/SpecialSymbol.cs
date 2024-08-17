using Cadence.Models.Slot;

namespace Cadence.Models.Symbols;

public class SpecialSymbol(List<SlotCoordinate> slotCoordinates) : SymbolBase(slotCoordinates)
{
    public override bool IsSpecialSymbol => true;
    public override bool IsPayingSymbol => true;
}