using Cadence.Enumerators.Machine;
using Cadence.Factories.Machine;
using Cadence.Models.Config;
using Cadence.Models.Machine;
using Cadence.Models.Round;
using Cadence.Models.Slot;
using Cadence.Models.Symbols;

AnticipatorConfig anticipatorConfig = new AnticipatorConfig
{
    ColumnSize = 5,
    MinToAnticipate = 2,
    MaxToAnticipate = 3,
    AnticipateCadence = 2,
    DefaultCadence = 0.25f
};

SlotMachineCadence machineCadence = (SlotMachineCadence) MachineFactory.GetMachine(Machine.SlotMachine, anticipatorConfig);

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

machineCadence.AddRounds(gameRounds);
RoundsCadences cadences = machineCadence.HandleCadences();
machineCadence.CleanRounds();

Console.WriteLine($"CADENCES \n{cadences}");
