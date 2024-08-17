using Cadence.Factories.Symbol;
using Cadence.Interfaces.Machine;
using Cadence.Interfaces.Symbol;
using Cadence.Models.Machine;
using Cadence.Models.Results;
using Cadence.Models.Round;

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
        // 0, 0, 0, 0, 8
        List<WinningCombinationsResult> winningCombinationsResults = new List<WinningCombinationsResult>();
        
        for (int i = 0; i < lineSymbols.Length; i++)
        {
            int idSymbol = lineSymbols[i];
            int idSymbolPrincipal = idSymbol;
            List<int> indicesCombinnation = new List<int>() {i};
            ISymbolBase symbol = SymbolFactory.GetSymbol(idSymbol);
            
            if (!symbol.IsPayingSymbol)
                continue;
            
            for (int j = i+1; j < lineSymbols.Length; j++)
            {
                if (j > lineSymbols.Length) break;
                
                int nextIdSymbol = lineSymbols[j];
                ISymbolBase nextSymbol = SymbolFactory.GetSymbol(nextIdSymbol);

                if (!nextSymbol.IsPayingSymbol || 
                    (!symbol.IsSpecialSymbol && idSymbol != nextIdSymbol && !nextSymbol.IsSpecialSymbol) ||
                    idSymbol != idSymbolPrincipal &&  idSymbolPrincipal != nextIdSymbol) 
                    break;
                
                indicesCombinnation.Add(j);
                
                idSymbolPrincipal = symbol.IsSpecialSymbol ? nextIdSymbol : idSymbol;

                if(indicesCombinnation.Count < 3 ||
                   symbol.IsSpecialSymbol && nextSymbol.IsSpecialSymbol && indicesCombinnation.Count < lineSymbols.Length)
                    continue;
                
                WinningCombinationsResult? winningCombinationsResult  = winningCombinationsResults.Find(w => w.Number == idSymbolPrincipal);
                
                if(winningCombinationsResult is null)
                    winningCombinationsResults.Add(new WinningCombinationsResult(idSymbolPrincipal, indicesCombinnation));
                else if(winningCombinationsResult.Indices.Count < indicesCombinnation.Count)
                    winningCombinationsResult.Indices = indicesCombinnation;
            }
        }
        
        return winningCombinationsResults;
    }

    private float[] PopulateCandence(ISymbolBase symbol)
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