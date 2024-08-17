using Cadence.Factories.Machine;
using Cadence.Interfaces.Machine;
using Cadence.Models.Config;
using Cadence.Models.Machine;
using Cadence.Models.Results;
using Cadence.Models.Round;
using Cadence.Models.Slot;
using Cadence.Models.Symbols;
using Cadence.Tests.Utils.Data;
using FluentAssertions;
using Xunit;

namespace Cadence.Tests.Services.Machine;

public class SlotMachineCadenceServiceTest
{
    [Fact]
    public void Handle_Cadences_Without_Special_Symbol()
    {
        //Arrange
        AnticipatorConfig anticipatorConfig = new AnticipatorConfig
        {
            ColumnSize = 5,
            MinToAnticipate = 2,
            MaxToAnticipate = 3,
            AnticipateCadence = 2,
            DefaultCadence = 1
        };
        SimpleSymbol roundOne = new SimpleSymbol(new List<SlotCoordinate> { });
        SimpleSymbol roundTwo = new SimpleSymbol(new List<SlotCoordinate> { });
        SimpleSymbol roundThree = new SimpleSymbol(new List<SlotCoordinate> { });
        RoundsSymbols gameRounds = new RoundsSymbols(roundOne, roundTwo, roundThree);
        
        SlotMachineCadence machineCadence = new SlotMachineCadence(anticipatorConfig);
        machineCadence.AddRounds(gameRounds);
        
        ISlotMachineCadenceService machineService = MachineFactory.GetMachineService(machineCadence);

        //Act
        RoundsCadences cadences = machineService.HandleCadences();
        
        //Assert
        cadences.RoundOne.Should().BeEquivalentTo(new float[] { 0, 1, 2, 3, 4 });
        cadences.RoundTwo.Should().BeEquivalentTo(new float[] { 0, 1, 2, 3, 4 });
        cadences.RoundThree.Should().BeEquivalentTo(new float[] { 0, 1, 2, 3, 4 });
    }
    
    [Fact]
    public void Handle_Cadences_With_One_Special_Symbol_To_Start_Anticipate_Cadence()
    {
        //Arrange
        AnticipatorConfig anticipatorConfig = new AnticipatorConfig
        {
            ColumnSize = 5,
            MinToAnticipate = 1,
            MaxToAnticipate = 3,
            AnticipateCadence = 3,
            DefaultCadence = 1
        };
        SpecialSymbol roundOne = new SpecialSymbol(new List<SlotCoordinate> { new SlotCoordinate { Column = 0, Row = 2 } });
        SpecialSymbol roundTwo = new SpecialSymbol(new List<SlotCoordinate> { new SlotCoordinate { Column = 2, Row = 2 } });
        SpecialSymbol roundThree = new SpecialSymbol(new List<SlotCoordinate> { new SlotCoordinate { Column = 4, Row = 2 } });
        RoundsSymbols gameRounds = new RoundsSymbols(roundOne, roundTwo, roundThree);
        
        SlotMachineCadence machineCadence = new SlotMachineCadence(anticipatorConfig);
        machineCadence.AddRounds(gameRounds);
        
        ISlotMachineCadenceService machineService = MachineFactory.GetMachineService(machineCadence);

        //Act
        RoundsCadences cadences = machineService.HandleCadences();
        
        //Assert
        cadences.RoundOne.Should().BeEquivalentTo(new float[] { 0, 3, 6, 9, 12 });
        cadences.RoundTwo.Should().BeEquivalentTo(new float[] { 0, 1, 2, 5, 8 });
        cadences.RoundThree.Should().BeEquivalentTo(new float[] { 0, 1, 2, 3, 4 });
    }
    
    [Fact]
    public void Handle_Cadences_With_Special_Symbol_To_Start_Anticipate_And_Stop_Anticipate_Cadence()
    {
        //Arrange
        AnticipatorConfig anticipatorConfig = new AnticipatorConfig
        {
            ColumnSize = 6,
            MinToAnticipate = 2,
            MaxToAnticipate = 3,
            AnticipateCadence = 3,
            DefaultCadence = 1
        };
        SpecialSymbol roundOne = new SpecialSymbol(new List<SlotCoordinate> { new SlotCoordinate { Column = 0, Row = 2 }, new SlotCoordinate { Column = 1, Row = 2 }, new SlotCoordinate { Column = 3, Row = 2 } });
        SpecialSymbol roundTwo = new SpecialSymbol(new List<SlotCoordinate> { new SlotCoordinate { Column = 2, Row = 2 },  new SlotCoordinate { Column = 2, Row = 3 },  new SlotCoordinate { Column = 2, Row = 4 } });
        SpecialSymbol roundThree = new SpecialSymbol(new List<SlotCoordinate> { new SlotCoordinate { Column = 1, Row = 2 }, new SlotCoordinate { Column = 1, Row = 3 }, new SlotCoordinate { Column = 5, Row = 3 } });
        RoundsSymbols gameRounds = new RoundsSymbols(roundOne, roundTwo, roundThree);
        
        SlotMachineCadence machineCadence = new SlotMachineCadence(anticipatorConfig);
        machineCadence.AddRounds(gameRounds);
        
        ISlotMachineCadenceService machineService = MachineFactory.GetMachineService(machineCadence);

        //Act
        RoundsCadences cadences = machineService.HandleCadences();
        
        //Assert
        cadences.RoundOne.Should().BeEquivalentTo(new float[] { 0, 1, 4, 7, 8, 9 });
        cadences.RoundTwo.Should().BeEquivalentTo(new float[] { 0, 1, 2, 3, 4, 5 });
        cadences.RoundThree.Should().BeEquivalentTo(new float[] { 0, 1, 4, 7, 10, 13 });
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public void Handle_Quantity_Cadences_By_Column_Size(int columnSize)
    {
        //Arrange
        AnticipatorConfig anticipatorConfig = new AnticipatorConfig
        {
            ColumnSize = columnSize,
            MinToAnticipate = 2,
            MaxToAnticipate = 3,
            AnticipateCadence = 3,
            DefaultCadence = 1
        };
        SimpleSymbol roundOne = new SimpleSymbol(new List<SlotCoordinate> { });
        SimpleSymbol roundTwo = new SimpleSymbol(new List<SlotCoordinate> { });
        SimpleSymbol roundThree = new SimpleSymbol(new List<SlotCoordinate> { });
        RoundsSymbols gameRounds = new RoundsSymbols(roundOne, roundTwo, roundThree);
        
        SlotMachineCadence machineCadence = new SlotMachineCadence(anticipatorConfig);
        machineCadence.AddRounds(gameRounds);
        
        ISlotMachineCadenceService machineService = MachineFactory.GetMachineService(machineCadence);

        //Act
        RoundsCadences cadences = machineService.HandleCadences();
        
        //Assert
        cadences.RoundOne.Length.Should().Be(columnSize);
        cadences.RoundTwo.Length.Should().Be(columnSize);
        cadences.RoundThree.Length.Should().Be(columnSize);
    }
    
    
    
    
    [Theory]
    [MemberData(nameof(WinningCombinations.GetWinningCombinationTestData), MemberType = typeof(WinningCombinations))]
    public void Calculate_Winning_Combinations(int[] input, List<WinningCombinationsResult> outputExpected )
    {
        //Arrange
        AnticipatorConfig anticipatorConfig = new AnticipatorConfig { ColumnSize = 4, MinToAnticipate = 2, MaxToAnticipate = 3, AnticipateCadence = 3, DefaultCadence = 1 };
        SimpleSymbol roundOne = new SimpleSymbol(new List<SlotCoordinate> { });
        SimpleSymbol roundTwo = new SimpleSymbol(new List<SlotCoordinate> { });
        SimpleSymbol roundThree = new SimpleSymbol(new List<SlotCoordinate> { });
        RoundsSymbols gameRounds = new RoundsSymbols(roundOne, roundTwo, roundThree);
        
        SlotMachineCadence machineCadence = new SlotMachineCadence(anticipatorConfig);
        machineCadence.AddRounds(gameRounds);
        
        ISlotMachineCadenceService machineService = MachineFactory.GetMachineService(machineCadence);
        
        //Act
        List<WinningCombinationsResult> winningCombinations = machineService.CalculateLineWinningCombinations(input);
        
        //Assert
        winningCombinations.Should().BeEquivalentTo(outputExpected, options => options.WithoutStrictOrdering());
    }
}