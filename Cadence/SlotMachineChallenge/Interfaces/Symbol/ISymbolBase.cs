using Cadence.Models.Slot;

namespace Cadence.Interfaces.Symbol;

public interface ISymbolBase
{
    public List<SlotCoordinate> SlotCoordinates { get; set; }
    public bool IsSpecialSymbol => false;
    public bool IsPayingSymbol => false;
}