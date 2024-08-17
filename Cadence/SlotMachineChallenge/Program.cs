using SlotMachineChallenge.Factories.Machine;
using SlotMachineChallenge.Interfaces.Machine;
using SlotMachineChallenge.Models.Config;
using SlotMachineChallenge.Models.Machine;
using SlotMachineChallenge.Models.Round;
using SlotMachineChallenge.Models.Slot;
using SlotMachineChallenge.Models.Symbols;

AnticipatorConfig anticipatorConfig = new AnticipatorConfig
{
    ColumnSize = 5,
    MinToAnticipate = 2,
    MaxToAnticipate = 3,
    AnticipateCadence = 2,
    DefaultCadence = 0.25f
};

SpecialSymbol roundOne = new SpecialSymbol(new List<SlotCoordinate>
{
    new SlotCoordinate { Column = 0, Row = 2 },
    new SlotCoordinate { Column = 1, Row = 3 },
    new SlotCoordinate { Column = 3, Row = 4 }
});

SpecialSymbol roundTwo = new SpecialSymbol(new List<SlotCoordinate>
{
    new SlotCoordinate { Column = 0, Row = 2 },
    new SlotCoordinate { Column = 0, Row = 3 }
});

SpecialSymbol roundThree = new SpecialSymbol(new List<SlotCoordinate>
{
    new SlotCoordinate { Column = 4, Row = 2 },
    new SlotCoordinate { Column = 4, Row = 3 }
});

RoundsSymbols gameRounds = new RoundsSymbols(roundOne, roundTwo,roundThree);

SlotMachineCadence machineCadence = new SlotMachineCadence(anticipatorConfig);
machineCadence.AddRounds(gameRounds);

ISlotMachineCadenceService machineService = MachineFactory.GetMachineService(machineCadence);

RoundsCadences cadences = machineService.HandleCadences();
machineCadence.CleanRounds();

Console.WriteLine($"CADENCES \n{cadences}");