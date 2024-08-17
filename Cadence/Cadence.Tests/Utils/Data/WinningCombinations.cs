using Cadence.Models.Results;

namespace Cadence.Tests.Utils.Data;

public static class WinningCombinations
{
   public static IEnumerable<object[]> GetWinningCombinationTestData()
    {
        yield return new object[]
        {
            new int[] { 1, 6, 6, 7, 2, 3 },
            new List<WinningCombinationsResult>()
        };
        
        yield return new object[]
        {
            new int[] { 1, 6, 6, 7, 2, 2 },
            new List<WinningCombinationsResult>()
        };
        
        yield return new object[]
        {
            new int[] { 1, 2, 6, 6, 6 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(6, new List<int> { 2, 3, 4 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 3, 3, 3, 8, 6, 3 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(3, new List<int> { 0, 1, 2 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 3, 3, 3, 8, 8, 8 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(3, new List<int> { 0, 1, 2 }),
                new WinningCombinationsResult(8, new List<int> { 3, 4, 5 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 3, 4, 3, 3, 3, 3 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(3, new List<int> { 2, 3, 4, 5 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 9, 9, 5, 9, 9 },
            new List<WinningCombinationsResult>()
        };
        
        yield return new object[]
        {
            new int[] { 9, 5, 5, 9, 9 },
            new List<WinningCombinationsResult>()
        };
        
        yield return new object[]
        {
            new int[] { 9, 5, 9, 5, 9 },
            new List<WinningCombinationsResult>()
        };
        
        yield return new object[]
        {
            new int[] { 5, 9, 5, 9, 5 },
            new List<WinningCombinationsResult>()
        };
        
        yield return new object[]
        {
            new int[] { 6, 6, 3, 0, 6 },
            new List<WinningCombinationsResult>()
        };
        
        yield return new object[]
        {
            new int[] { 1, 2, 0, 0, 3, 3 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(2, new List<int> { 1, 2, 3 }),
                new WinningCombinationsResult(3, new List<int> { 4, 5 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 1, 1, 0, 0, 3, 3 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(1, new List<int> { 0, 1, 2, 3 }),
                new WinningCombinationsResult(3, new List<int> { 4, 5 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 0, 0, 0, 3, 3, 3 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(3, new List<int> { 0, 1, 2, 3, 4, 5 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 0, 0, 2, 3, 3, 3 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(2, new List<int> { 0, 1, 2 }),
                new WinningCombinationsResult(3, new List<int> { 3, 4, 5 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 2, 0, 0, 3, 3, 3 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(2, new List<int> { 0, 1, 2 }),
                new WinningCombinationsResult(3, new List<int> { 1, 2, 3, 4, 5 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 4, 4, 6, 0, 2, 2 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(2, new List<int> { 3, 4, 5 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 3, 5, 8, 5, 5, 0 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(5, new List<int> { 3, 4, 5 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 3, 0, 3, 4, 4, 0 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(3, new List<int> { 0, 1, 2 }),
                new WinningCombinationsResult(4, new List<int> { 3, 4, 5 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 0, 8, 6, 8, 8 },
            new List<WinningCombinationsResult>()
        };
        
        yield return new object[]
        {
            new int[] { 8, 8, 6, 8, 0 },
            new List<WinningCombinationsResult>()
        };

        yield return new object[]
        {
            new int[] { 0, 0, 0, 0, 8 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(8, new List<int> { 0, 1, 2, 3, 4 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 8, 0, 0, 0, 0 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(8, new List<int> { 0, 1, 2, 3, 4 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 0, 0, 0, 0, 0 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(0, new List<int> { 0, 1, 2, 3, 4 })
            }
        };
        
        yield return new object[]
        {
            new int[] { 1, 1, 2, 0, 0 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(2, new List<int> { 2, 3, 4 })
            }
        };

        yield return new object[]
        {
            new int[] { 11, 0, 0, 7, 4 },
            new List<WinningCombinationsResult>
            {
                new WinningCombinationsResult(7, new List<int> { 1, 2, 3 })
            }
        };
    }
}
