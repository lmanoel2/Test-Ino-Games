using SlotMachineChallenge.Models.Slot;

namespace SlotMachineChallenge.Interfaces.Symbol;

public interface ISymbolBase
{
    public List<SlotCoordinate> SlotCoordinates { get; set; }
    public bool IsSpecialSymbol => false;
    public bool IsPayingSymbol => false;
}