using Cadence.Interfaces.Symbol;
using Cadence.Models.Slot;

namespace Cadence.Models.Symbols;

public abstract class SymbolBase(List<SlotCoordinate> slotCoordinates) : ISymbolBase
{
    public List<SlotCoordinate> SlotCoordinates { get; set; } = slotCoordinates;
    public virtual bool IsSpecialSymbol => false;
    public virtual bool IsPayingSymbol => false;
}