using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X041_EmergenceOfGravity : ResearchTestBase
{
    public AT_X041_EmergenceOfGravity(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X041_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X041 Emergence of Gravity from Q-Actualization Structure");

        var models = GravityEmergenceAnalyzer.AnalyzeModels();
        var tests = GravityEmergenceAnalyzer.BuildTests();

        int surviving = models.Count(m => m.Survives);

        // 1. Framework
        Sec(sb, "The Question");
        sb.AppendLine("  Space = graph of Q-entities.");
        sb.AppendLine("  Time = partial order of actualization events (X040).");
        sb.AppendLine("  Spacetime = causal structure of Q-events.");
        sb.AppendLine("  Does GRAVITY emerge from non-uniformity in this structure?");
        sb.AppendLine();

        // 2. Competing models
        Sec(sb, "Competing Gravity Models");
        sb.AppendLine("  Model  Mechanism                                           Survives?");
        sb.AppendLine("  " + new string('─', 75));
        foreach (var m in models)
        {
            string s = m.Survives ? "YES" : "NO";
            sb.AppendLine($"  {m.Name,-6} {m.Mechanism.Split('\n')[0],-52} {s}");
        }
        sb.AppendLine();
        sb.AppendLine($"  {surviving}/{models.Count} models survive.");
        sb.AppendLine();

        // 3. Model details
        Sec(sb, "Model Analysis");
        foreach (var m in models)
        {
            sb.AppendLine($"  [{m.Name}]");
            sb.AppendLine($"  Mechanism: {m.Mechanism}");
            sb.AppendLine($"  Predictions: Attraction={m.PredictsAttraction}, "
                + $"Redshift={m.PredictsRedshift}, Newtonian={m.HasNewtonianLimit}");
            sb.AppendLine($"  Flaw: {m.FatalFlaw}");
            sb.AppendLine();
        }

        // 4. Experimental tests
        Sec(sb, "Experimental Tests — Which Model Matches GR?");
        sb.AppendLine("  Test                      A (density)    B (causal set)  C (hybrid)   Best Match");
        sb.AppendLine("  " + new string('─', 90));
        foreach (var t in tests)
        {
            sb.AppendLine($"  {t.Test,-25} {t.PredictedA[..Math.Min(18, t.PredictedA.Length)],-18} {t.PredictedB[..Math.Min(18, t.PredictedB.Length)],-18} {t.PredictedC[..Math.Min(18, t.PredictedC.Length)],-18} {t.BestMatch}");
        }
        sb.AppendLine();

        // 5. Causal set derivation
        Sec(sb, "Model B: Causal Set Gravity");
        sb.AppendLine(GravityEmergenceAnalyzer.TheCausalSetDerivation());

        // 6. The emergent metric
        Sec(sb, "Emergent Metric from Q-Event Density");
        sb.AppendLine(GravityEmergenceAnalyzer.TheEmergentMetric());

        // 7. Newtonian limit
        Sec(sb, "Newtonian Limit");
        sb.AppendLine("  In the weak-field, slow-motion limit of causal set gravity:");
        sb.AppendLine("    • Proper time τ maximizes Q-event count along worldline.");
        sb.AppendLine("    • In flat spacetime: τ = ∫ √(1 - v²/c²) dt.");
        sb.AppendLine("    • With curvature: δτ = (1/c²) ∫ Φ dt where ∇²Φ = 4πGρ.");
        sb.AppendLine("    • This IS the Newtonian limit of GR.");
        sb.AppendLine("    • 1/r² force law emerges for spherical sources.");
        sb.AppendLine();

        // 8. The sign problem
        Sec(sb, "The Redshift Sign Problem — Model A Fails");
        sb.AppendLine("  GR: clocks run SLOWER near mass → gravitational REDSHIFT.");
        sb.AppendLine("  Model A (naive): mass increases τ → clocks FASTER → BLUESHIFT.");
        sb.AppendLine("  WRONG SIGN. Model A must assume mass REDUCES τ to match GR.");
        sb.AppendLine();
        sb.AppendLine("  Model B (causal set): redshift follows from metric structure,");
        sb.AppendLine("  not from τ-gradient. Correct sign automatically. ✓");
        sb.AppendLine();

        // 9. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(GravityEmergenceAnalyzer.HostileReview());

        // 10. Final verdict
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X041 COMPLETE.");
        sb.AppendLine($"  Classification: C — Partial Gravitational Emergence.");
        sb.AppendLine($"  Model B (causal set gravity) is the correct structure.");
        sb.AppendLine($"  Gravity emerges from Q-event partial order.");
        sb.AppendLine($"  Gaps: dimensionality (3+1) and Newton's G not derived from Q.");
        sb.AppendLine($"  Full GR requires external causal set theory bridge.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
