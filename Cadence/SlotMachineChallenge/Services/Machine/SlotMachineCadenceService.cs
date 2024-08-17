using SlotMachineChallenge.Factories.Symbol;
using SlotMachineChallenge.Interfaces.Machine;
using SlotMachineChallenge.Interfaces.Symbol;
using SlotMachineChallenge.Models.Machine;
using SlotMachineChallenge.Models.Results;
using SlotMachineChallenge.Models.Round;

namespace SlotMachineChallenge.Services.Machine;

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
        List<WinningCombinationsResult> winningCombinationsResults = new List<WinningCombinationsResult>();
        
        for (int positionActualSymbol = 0; positionActualSymbol < lineSymbols.Length; positionActualSymbol++)
        {
            List<int> indicesCombinnation = new List<int>() {positionActualSymbol};
            int idSymbolActual = lineSymbols[positionActualSymbol];
            int idSymbolPrincipal = idSymbolActual;
            
            ISymbolBase symbolActual = SymbolFactory.GetSymbol(idSymbolActual);
            
            if (!symbolActual.IsPayingSymbol)
                continue;
            
            TryToLinkWithNextSymbols(lineSymbols, positionActualSymbol, symbolActual, idSymbolActual, idSymbolPrincipal, indicesCombinnation, winningCombinationsResults);
        }
        
        return winningCombinationsResults;
    }

    private static void TryToLinkWithNextSymbols(int[] lineSymbols, int positionActualSymbol, ISymbolBase symbolActual,
        int idSymbolActual, int idSymbolPrincipal, List<int> indicesCombinnation, List<WinningCombinationsResult> winningCombinationsResults)
    {
        for (int positionNextSymbol = positionActualSymbol+1; positionNextSymbol < lineSymbols.Length; positionNextSymbol++)
        {
            if (positionNextSymbol > lineSymbols.Length) 
                break;
                
            int nextIdSymbol = lineSymbols[positionNextSymbol];
            ISymbolBase nextSymbol = SymbolFactory.GetSymbol(nextIdSymbol);

            if (!CheckIsAllowedToLinkNextSymbol(nextSymbol, symbolActual, idSymbolActual, nextIdSymbol, idSymbolPrincipal)) 
                break;
                
            indicesCombinnation.Add(positionNextSymbol);
                
            idSymbolPrincipal = symbolActual.IsSpecialSymbol ? nextIdSymbol : idSymbolActual;

            if(!CheckIsAllowedPopulateWinningCombinationsResult(lineSymbols, indicesCombinnation, symbolActual, nextSymbol))
                continue;
                
            PopulateWinningCombinationsResults(winningCombinationsResults, idSymbolPrincipal, indicesCombinnation);
        }
    }

    private static bool CheckIsAllowedToLinkNextSymbol(ISymbolBase nextSymbol, ISymbolBase symbolActual, int idSymbolActual, int nextIdSymbol, int idSymbolPrincipal)
    {
        return nextSymbol.IsPayingSymbol && 
               (symbolActual.IsSpecialSymbol || idSymbolActual == nextIdSymbol || nextSymbol.IsSpecialSymbol) &&
               (idSymbolActual == idSymbolPrincipal || idSymbolPrincipal == nextIdSymbol);
    }

    private static bool CheckIsAllowedPopulateWinningCombinationsResult(int[] lineSymbols, List<int> indicesCombinnation, ISymbolBase symbolActual, ISymbolBase nextSymbol)
    {
        return indicesCombinnation.Count >= 3 &&
               (!symbolActual.IsSpecialSymbol || !nextSymbol.IsSpecialSymbol || indicesCombinnation.Count >= lineSymbols.Length);
    }
    
    private static void PopulateWinningCombinationsResults(List<WinningCombinationsResult> winningCombinationsResults, int idSymbolPrincipal, List<int> indicesCombinnation)
    {
        WinningCombinationsResult? winningCombinationsResult  = winningCombinationsResults.Find(w => w.Number == idSymbolPrincipal);

        if(winningCombinationsResult is null)
            winningCombinationsResults.Add(new WinningCombinationsResult(idSymbolPrincipal, indicesCombinnation));
        else if(winningCombinationsResult.Indices.Count < indicesCombinnation.Count)
            winningCombinationsResult.Indices = indicesCombinnation;
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