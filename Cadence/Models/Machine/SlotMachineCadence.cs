using Cadence.Interfaces.Machine;
using Cadence.Models.Config;
using Cadence.Models.Round;
using Cadence.Models.Symbols;

namespace Cadence.Models.Machine;

public class SlotMachineCadence(AnticipatorConfig config) : MachineBase, ISlotMachineCadence
{
    public AnticipatorConfig Config { get; init; } = config;

    public RoundsSymbols? RoundsSymbols { get; private set; } 
    
    public void AddRounds(RoundsSymbols roundsSymbols)
    {
        RoundsSymbols = roundsSymbols;
    }

    public void CleanRounds()
    {
        RoundsSymbols = null;
    }

    public RoundsCadences HandleCadences()
    {
        if (RoundsSymbols is null)
            throw new ArgumentNullException("RoundsSymbols is null");
        
        RoundsCadences roundsCadences = new RoundsCadences();

        roundsCadences.RoundOne = PopulateCandence(RoundsSymbols.RoundOne);
        roundsCadences.RoundTwo = PopulateCandence(RoundsSymbols.RoundTwo);
        roundsCadences.RoundThree = PopulateCandence(RoundsSymbols.RoundThree);

        return roundsCadences;
    }

    private float[] PopulateCandence(SymbolBase symbol)
    {
        int[] columnsWithSpecialSymbol = symbol.SlotCoordinates
            .Select(sc => sc.Column)
            .OrderBy(c => c)
            .ToArray();
        
        if (!symbol.IsSpecialSymbol ||
            columnsWithSpecialSymbol.Length == 0 ||
            columnsWithSpecialSymbol.Length < Config.MinToAnticipate)
            return GetCadences();

        int columnToStartAnticipate = GetColumnToStartAnticipate(columnsWithSpecialSymbol);
        int columnToStopAnticipate = GetColumnToStopAnticipate(columnsWithSpecialSymbol);

        return columnToStartAnticipate > Config.ColumnSize ? 
            GetCadences() : 
            GetCadences(columnToStartAnticipate, columnToStopAnticipate);
    }

    private int GetColumnToStopAnticipate(int[] columnsWithSpecialSymbol)
    {
        int columnToStopAnticipate = 999;

        int positionToGetMaxAnticipate = Config.MaxToAnticipate - 1;
        
        if (positionToGetMaxAnticipate >= 0 && columnsWithSpecialSymbol.Length >= Config.MaxToAnticipate)
            columnToStopAnticipate = columnsWithSpecialSymbol[positionToGetMaxAnticipate] + 1;
        
        return columnToStopAnticipate;
    }

    private int GetColumnToStartAnticipate(int[] columnsWithSpecialSymbol)
    {
        int columnToStartAnticipate = -1;
        int positionToGetMinAnticipate = Config.MinToAnticipate - 1;

        if (positionToGetMinAnticipate < 0)
            positionToGetMinAnticipate = 0;
        
        columnToStartAnticipate = columnsWithSpecialSymbol[positionToGetMinAnticipate] + 1;
        return columnToStartAnticipate;
    }

    private float[] GetCadences(int columnToStartAnticipate = -1, int columnToStopAnticipate = 999)
    {
        float[] cadences = new float[Config.ColumnSize];
        
        cadences[0] = Config.DefaultCadence;

        for (int i = 1; i < Config.ColumnSize; i++)
        {
            if(i >= columnToStartAnticipate && i  < columnToStopAnticipate)
                cadences[i] = Config.AnticipateCadence + cadences[i - 1];
            else
                cadences[i] = Config.DefaultCadence + cadences[i - 1];
        }
        
        return cadences;
    }
}
