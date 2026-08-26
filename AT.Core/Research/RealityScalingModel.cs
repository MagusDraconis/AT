namespace AT.Core.Research;

/// <summary>
/// Analyzes scaling of innovation capacity with system size N.
/// AT-X027: Finite vs Infinite Reality Principle
/// </summary>
public static class RealityScalingModel
{
    public static List<InfiniteLimitMetrics.ScalingResult> AnalyzeScaling()
    {
        int[] sizes = { 10, 100, 1000, 10000, 100000 };
        var results = new List<InfiniteLimitMetrics.ScalingResult>();

        foreach (int N in sizes)
        {
            // For a 1D chain of N nodes:
            // - Orthogonal eigenmodes = N (finite)
            // - Species capacity = N (each eigenvalue = potential species)
            // - Operator families = limited by graph topology types
            // - Innovation saturates at species ≈ N

            double maxSpecies = N;
            double satTime = Math.Log(N) * 100; // logarithmic scaling of saturation time

            string regime = N < 1000 ? "Small — rapid saturation"
                          : N < 100000 ? "Medium — extended innovation" : "Large — very slow saturation";

            results.Add(new InfiniteLimitMetrics.ScalingResult(
                N, maxSpecies, satTime, true, regime));
        }

        return results;
    }
}
