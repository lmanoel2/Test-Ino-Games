using SlotMachineChallenge.Interfaces.Symbol;
using SlotMachineChallenge.Models.Slot;

namespace SlotMachineChallenge.Models.Symbols;

public abstract class SymbolBase(List<SlotCoordinate> slotCoordinates) : ISymbolBase
{
    public List<SlotCoordinate> SlotCoordinates { get; set; } = slotCoordinates;
    public virtual bool IsSpecialSymbol => false;
    public virtual bool IsPayingSymbol => false;
}