using Cadence.Models.Slot;

namespace Cadence.Models.Symbols;

public abstract class SymbolBase(List<SlotCoordinate> slotCoordinates)
{
    public List<SlotCoordinate> SlotCoordinates { get; set; } = slotCoordinates;

    public virtual bool IsSpecialSymbol => false;
}