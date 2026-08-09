namespace TQM.Core.ResearchXB.Models;

/// <summary>
/// Universe ensemble model: fix identity, vary history.
/// ResearchXB-001
/// </summary>
public static class UniverseEnsembleModel
{
    public sealed record Universe(
        int Id, double Xi, double Alpha,
        double M2, double OmegaDM, double OmegaB);

    public static List<Universe> GenerateEnsemble(int count)
    {
        var rng = new Random(42);
        var universes = new List<Universe>();

        for (int i = 0; i < count; i++)
        {
            // Scale-invariant distribution for mass scale
            double xi = Math.Pow(10, 20 + 4 * rng.NextDouble()); // log-uniform 10^20-10^24

            // Stability window for alpha
            double alpha = Math.Pow(10, -4 + 3 * rng.NextDouble()); // 10^-4 to 10^-1

            // M^2 peaked at O(1-10)
            double m2 = Math.Pow(10, rng.NextDouble()); // 1 to 10

            // Omega from freezeout (correlated)
            double omegaBase = Math.Pow(10, -1 + rng.NextDouble()); // 0.1 to 1
            double omegaDM = omegaBase * 0.8;
            double omegaB = omegaBase * 0.2;

            universes.Add(new Universe(i, xi, alpha, m2, omegaDM, omegaB));
        }

        return universes;
    }

    public static string EnsembleStatistics(List<Universe> universes)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"UNIVERSE ENSEMBLE: {universes.Count} UNIVERSES");
        sb.AppendLine("(Same identity, different histories)");
        sb.AppendLine();
        sb.AppendLine("  Parameter    Min         Max         Mean        Our Universe");
        sb.AppendLine("  " + new string('-', 65));

        double xiMin = universes.Min(u => u.Xi);
        double xiMax = universes.Max(u => u.Xi);
        double xiMean = universes.Average(u => u.Xi);
        double logXiMean = Math.Pow(10, universes.Average(u => Math.Log10(u.Xi)));

        sb.AppendLine($"  xi/l_P     10^{Math.Log10(xiMin):F0}     10^{Math.Log10(xiMax):F0}    10^{Math.Log10(logXiMean):F0}      10^22");

        double alphaMin = universes.Min(u => u.Alpha);
        double alphaMax = universes.Max(u => u.Alpha);
        double alphaMean = universes.Average(u => u.Alpha);
        sb.AppendLine($"  alpha       {alphaMin:F4}     {alphaMax:F4}      {alphaMean:F4}      1/137");

        double omegaMin = universes.Min(u => u.OmegaDM);
        double omegaMax = universes.Max(u => u.OmegaDM);
        double omegaMean = universes.Average(u => u.OmegaDM);
        sb.AppendLine($"  Omega_DM    {omegaMin:F3}      {omegaMax:F3}       {omegaMean:F3}       0.27");

        int viableCount = universes.Count(u =>
            u.Alpha > 0.001 && u.Alpha < 0.1 &&
            u.OmegaDM > 0.1 && u.OmegaDM < 0.5);
        sb.AppendLine();
        sb.AppendLine($"  VIABLE UNIVERSES (atoms + galaxies): {viableCount}/{universes.Count} ({100.0 * viableCount / universes.Count:F0}%)");
        sb.AppendLine("  Our universe: inside the viable subset.");

        return sb.ToString();
    }
}
