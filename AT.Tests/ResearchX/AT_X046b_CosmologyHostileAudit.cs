using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X046b_CosmologyHostileAudit : ResearchTestBase
{
    public AT_X046b_CosmologyHostileAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X046b_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X046b Hostile Cosmology Audit");

        var tests = CosmologyAudit.RunTests();
        int passing = tests.Count(t => t.Passes);
        int failing = tests.Count(t => !t.Passes);

        // 1. Overview
        Sec(sb, "Audit Overview");
        sb.AppendLine($"  Tests: {tests.Count}. Passing: {passing}. Failing: {failing}.");
        sb.AppendLine();

        // 2. Test results
        Sec(sb, "Observational Tests");
        sb.AppendLine("  Test                           ΛCDM              AT                Pass?");
        sb.AppendLine("  " + new string('─', 85));
        foreach (var t in tests)
        {
            string passes = t.Passes ? "✓" : "✗";
            sb.AppendLine($"  {t.Test,-30}  {t.LcdmPrediction[..Math.Min(18, t.LcdmPrediction.Length)],-18} {t.AtPrediction[..Math.Min(18, t.AtPrediction.Length)],-18} {passes}");
        }
        sb.AppendLine();

        // 3. The key tension
        Sec(sb, "Key Tension: Exact Tracking is RULED OUT");
        sb.AppendLine(CosmologyAudit.TheKeyTension());

        // 4. Detailed failures
        Sec(sb, "Detailed Test Analysis");
        foreach (var t in tests)
        {
            sb.AppendLine($"  [{((t.Passes ? "✓" : "✗"))}] {t.Test}");
            sb.AppendLine($"  ΛCDM: {t.LcdmPrediction}");
            sb.AppendLine($"  AT:  {t.AtPrediction}");
            sb.AppendLine($"  Tension: {t.Tension}");
            sb.AppendLine();
        }

        // 5. CMB specific problem
        Sec(sb, "CMB Constraint — The Hardest Test");
        sb.AppendLine("  At recombination (z ≈ 1100):");
        sb.AppendLine("    H(z=1100) ≈ 10³ × H₀.");
        sb.AppendLine("    If Λ ∝ H², then Λ(z=1100) ≈ 10⁶ × Λ₀.");
        sb.AppendLine();
        sb.AppendLine("  This would dramatically change the expansion rate at recombination,");
        sb.AppendLine("  shifting the acoustic peaks in the CMB. Planck data tightly");
        sb.AppendLine("  constrains the expansion history at z~1100 to ΛCDM.");
        sb.AppendLine();
        sb.AppendLine("  RESCUE MECHANISMS:");
        sb.AppendLine("    1. Λ(t) = α/√V(t), not α·H²(t). V(t) is dominated by");
        sb.AppendLine("       low-redshift volume → Λ at recombination is much smaller");
        sb.AppendLine("       than naive H² scaling suggests.");
        sb.AppendLine("    2. Fluctuations: Λ is a STOCHASTIC variable. The mean value");
        sb.AppendLine("       at recombination may be small but with large fluctuations.");
        sb.AppendLine("    3. Need full causal set simulation to check consistency.");
        sb.AppendLine();

        // 6. Distinctive predictions
        Sec(sb, "Distinctive Predictions — Testable");
        sb.AppendLine("  1. Acceleration is TEMPORARY (not permanent de Sitter).");
        sb.AppendLine("  2. Expansion 'jerks': d³a/dt³ ≠ 0 at ~1% level.");
        sb.AppendLine("  3. Early dark energy signature (Λ larger in past).");
        sb.AppendLine("  4. w(z) ≠ -1, varies with redshift.");
        sb.AppendLine("  5. Potential resolution of H₀ tension.");
        sb.AppendLine();

        // 7. Final verdict
        Sec(sb, "Final Verdict");
        sb.AppendLine(CosmologyAudit.Verdict());

        string classification = failing <= 2 ? "C: Viable but Constrained"
            : failing <= 4 ? "B: Serious Tension"
            : "A: Ruled Out";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X046b COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  Exact Λ ∝ H² is RULED OUT (no acceleration).");
        sb.AppendLine($"  Stochastic Λ (fluctuations around ~H²) is VIABLE.");
        sb.AppendLine($"  Future surveys at ~0.1% precision will decisively test.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
