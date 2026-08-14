namespace TQM.Core.ResearchQG;

/// <summary>QG-096 causal-set g†-origin: does causal discreteness generate g† the same way it
/// generates Λ? The causal-depth growth rate is d ln(depth)/dt = H (QG-091), giving a_eff = cH —
/// the RIGHT magnitude but WITHOUT the 2π. Counting/order gives cH; the 2π is an angular-frequency
/// (radians→cycles) factor that causal sets do not fix. So causal discreteness predicts the cH SCALE,
/// not the 1/(2π).</summary>
public static class CausalGdaggerOrigin
{
    /// <summary>a_eff from causal-depth growth: a_eff = c·(d ln depth/dt) = cH.</summary>
    public static double AEffFromCausalDepth() => CausalSetAccelerationModel.CH;

    /// <summary>Does causal discreteness generate the 2π? NO.</summary>
    public static bool GeneratesTwoPi => false;

    public static string Reason =>
        "counting/order gives cH; the 2π is an angular-frequency factor (radians→cycles) not fixed by causal sets";

    /// <summary>Is g† = cH/2π predicted or inserted? The cH scale is predicted; the 1/(2π) is INSERTED.</summary>
    public static string PredictionStatus => "the cH scale is predicted; the 1/(2π) is inserted";
}
