namespace SlotMachineChallenge.Models.Round;

public class RoundsCadences
{
    public float[] RoundOne { get; set; } = new float[] { };
    public float[] RoundTwo { get; set; } = new float[] { };
    public float[] RoundThree { get; set; } = new float[] { };

    public override string ToString()
    {
        return $"Round One: [{string.Join(", ", RoundOne.Select(x => x.ToString("0.##").Replace(",", ".")))}]\n" +
               $"Round Two: [{string.Join(", ", RoundTwo.Select(x => x.ToString("0.##").Replace(",", ".")))}]\n" +
               $"Round Three: [{string.Join(", ", RoundThree.Select(x => x.ToString("0.##").Replace(",", ".")))}]";
    }
}