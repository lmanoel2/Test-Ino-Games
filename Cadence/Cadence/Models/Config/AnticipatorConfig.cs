using Cadence.Interfaces.Config;

namespace Cadence.Models.Config;

public class AnticipatorConfig : IConfigBase
{
    public int ColumnSize { get; set; }
    public int MinToAnticipate { get; set; }
    public int MaxToAnticipate { get; set; }
    public int AnticipateCadence { get; set; }
    public float DefaultCadence { get; set; }
}