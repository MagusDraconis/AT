namespace AT.Core.ResearchQG;

/// <summary>QG-090 information-change: change as information update ("It from Bit"). The
/// amount of change C(t) is the number of distinct states/events; H, entropy production and
/// information growth are manifestations of d ln C/dt.</summary>
public static class InformationChangeModel
{
    /// <summary>Amount of change C(t) = number of distinct states/events (causal set cardinality N).</summary>
    public static string AmountOfChange => "C(t) = N(t) = distinct states/events";

    /// <summary>C ∝ a³ (4-volume), so d ln C/dt = 3H (consistent with QG-087/088).</summary>
    public static double ChangeRateInUnitsOfH => 3.0;
}
