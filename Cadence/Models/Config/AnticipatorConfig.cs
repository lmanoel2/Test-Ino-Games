namespace Cadence.Models.Config;

public class AnticipatorConfig : ConfigBase
{
    public int ColumnSize { get; set; }
    public int MinToAnticipate { get; set; }
    public int MaxToAnticipate { get; set; }
    public double AnticipateCadence { get; set; }
    public double DefaultCadence { get; set; }
}