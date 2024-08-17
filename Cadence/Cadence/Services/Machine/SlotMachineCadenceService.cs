using Cadence.Interfaces.Machine;
using Cadence.Models.Machine;
using Cadence.Models.Results;
using Cadence.Models.Round;
using Cadence.Models.Symbols;

namespace Cadence.Services.Machine;

public class SlotMachineCadenceService(SlotMachineCadence slotMachineCadence) : ISlotMachineCadenceService
{
    public RoundsCadences HandleCadences()
    {
        if (slotMachineCadence.RoundsSymbols is null)
            throw new ArgumentNullException("Not found rounds to handle cadences");
        
        RoundsCadences roundsCadences = new RoundsCadences();

        roundsCadences.RoundOne = PopulateCandence(slotMachineCadence.RoundsSymbols.RoundOne);
        roundsCadences.RoundTwo = PopulateCandence(slotMachineCadence.RoundsSymbols.RoundTwo);
        roundsCadences.RoundThree = PopulateCandence(slotMachineCadence.RoundsSymbols.RoundThree);

        return roundsCadences;
    }

    public List<WinningCombinationsResult> CalculateLineWinningCombinations(int[] lineSymbols)
    {
        foreach (var lineSymbol in lineSymbols)
        {
            
        }
        
        return new List<WinningCombinationsResult>();
    }

    private float[] PopulateCandence(SymbolBase symbol)
    {
        int[] columnsWithSpecialSymbol = symbol.SlotCoordinates
            .Select(sc => sc.Column)
            .OrderBy(c => c)
            .ToArray();
        
        if (!symbol.IsSpecialSymbol ||
            columnsWithSpecialSymbol.Length == 0 ||
            columnsWithSpecialSymbol.Length < slotMachineCadence.Config.MinToAnticipate)
            return GetCadences();

        int columnToStartAnticipate = GetColumnToStartAnticipate(columnsWithSpecialSymbol);
        int columnToStopAnticipate = GetColumnToStopAnticipate(columnsWithSpecialSymbol);

        return columnToStartAnticipate > slotMachineCadence.Config.ColumnSize ? 
            GetCadences() : 
            GetCadences(columnToStartAnticipate, columnToStopAnticipate);
    }

    private int GetColumnToStartAnticipate(int[] columnsWithSpecialSymbol)
    {
        int columnToStartAnticipate = -1;
        int positionToGetMinAnticipate = slotMachineCadence.Config.MinToAnticipate - 1;

        if (positionToGetMinAnticipate < 0)
            positionToGetMinAnticipate = 0;
        
        columnToStartAnticipate = columnsWithSpecialSymbol[positionToGetMinAnticipate] + 1;
        return columnToStartAnticipate;
    }

    private int GetColumnToStopAnticipate(int[] columnsWithSpecialSymbol)
    {
        int columnToStopAnticipate = 999;

        int positionToGetMaxAnticipate = slotMachineCadence.Config.MaxToAnticipate - 1;
        
        if (positionToGetMaxAnticipate >= 0 && columnsWithSpecialSymbol.Length >= slotMachineCadence.Config.MaxToAnticipate)
            columnToStopAnticipate = columnsWithSpecialSymbol[positionToGetMaxAnticipate] + 1;
        
        return columnToStopAnticipate;
    }
    
    private float[] GetCadences(int columnToStartAnticipate = 999, int columnToStopAnticipate = -1)
    {
        float[] cadences = new float[slotMachineCadence.Config.ColumnSize];
        
        cadences[0] = 0;

        for (int i = 1; i < slotMachineCadence.Config.ColumnSize; i++)
        {
            if(i >= columnToStartAnticipate && i  < columnToStopAnticipate)
                cadences[i] = slotMachineCadence.Config.AnticipateCadence + cadences[i - 1];
            else
                cadences[i] = slotMachineCadence.Config.DefaultCadence + cadences[i - 1];
        }
        
        return cadences;
    }
}