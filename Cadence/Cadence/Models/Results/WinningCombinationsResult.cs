namespace Cadence.Models.Results;

public class WinningCombinationsResult
{
    public int Number { get; set; }
    public List<int> Indices { get; set; }

    public WinningCombinationsResult()
    {
    }

    public WinningCombinationsResult(int number, List<int> indices)
    {
        Number = number;
        Indices = indices;
    }
}