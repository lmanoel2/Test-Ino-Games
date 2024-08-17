using SlotMachineChallenge.Models.Slot;

namespace SlotMachineChallenge.Models.Symbols;

public class SimpleSymbol(List<SlotCoordinate> slotCoordinates) : SymbolBase(slotCoordinates)
{
    public override bool IsSpecialSymbol => false;
    public override bool IsPayingSymbol => true;
}