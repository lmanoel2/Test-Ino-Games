using Cadence.Enumerators.Machine;
using Cadence.Factories.Machine;
using Cadence.Interfaces.Machine;
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
    DefaultCadence = 0.25
};

IMachineBase machineCadence = MachineFactory.GetMachine(Machine.SlotMachine, anticipatorConfig);

RoundsSymbols gameRounds = new RoundsSymbols
{
    RoundOne = new SpecialSymbol()
    {
        SlotCoordinates = new List<SlotCoordinate>
        {
            new SlotCoordinate { Column = 0, Row = 2 },
            new SlotCoordinate { Column = 1, Row = 3 },
            new SlotCoordinate { Column = 3, Row = 4 }
        }
    },
    RoundTwo = new SpecialSymbol
    {
        SlotCoordinates = new List<SlotCoordinate>
        {
            new SlotCoordinate { Column = 0, Row = 2 },
            new SlotCoordinate { Column = 0, Row = 3 }
        }
    },
    RoundThree = new SpecialSymbol
    {
        SlotCoordinates = new List<SlotCoordinate>
        {
            new SlotCoordinate { Column = 4, Row = 2 },
            new SlotCoordinate { Column = 4, Row = 3 }
        }
    }
};

RoundsCadences slotMachineCadences = new RoundsCadences
{
    RoundOne = new SlotCadence(),
    RoundTwo = new SlotCadence(),
    RoundThree = new SlotCadence()
};


Console.WriteLine($"CADENCES {HandleCadences(slotMachineCadences, gameRounds)}");


static RoundsCadences HandleCadences(RoundsCadences slotMachineCadences, RoundsSymbols rounds)
{
    slotMachineCadences.RoundOne = SlotCadence(rounds.RoundOne.SlotCoordinates);
    slotMachineCadences.RoundTwo = SlotCadence(rounds.RoundTwo.SlotCoordinates);
    slotMachineCadences.RoundThree = SlotCadence(rounds.RoundThree.SlotCoordinates);

    return slotMachineCadences;
}

static SlotCadence SlotCadence(List<SlotCoordinate> slotCoordinates)
{
    return new SlotCadence();
}
