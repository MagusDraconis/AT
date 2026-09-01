using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_128_AutonomousCollectiveWaveField : ResearchTestBase
{
    public AT_128_AutonomousCollectiveWaveField(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_128_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-128 Autonomous Collective Wave Field");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Θ(x,t) = Σ A_c·exp(iθ_c) is the collective field (AT-126/127).");
        sb.AppendLine("  2. At high density, Θ may become autonomous.");
        sb.AppendLine("  3. Closure test: predict Θ(t+Δt) from Θ(t) alone vs from all θ_c.");
        sb.AppendLine("  4. Assume particle description is sufficient until disproven.");
        sb.AppendLine();

        Sec(sb, "1. AT-127 Recap & Field Autonomy Theory");
        sb.AppendLine(CollectiveWaveFieldAnalyzer.FieldTheory());
        sb.AppendLine();

        Sec(sb, "2. Autonomy Experiments");

        double[] K_values = { 5.0 };
        double[] lambda_values = { 0.10 };
        int[] targetQ_values = { 2, 5, 10, 20, 50, 100 };
        string[] layouts = { "random", "lattice" };
        int seeds = 2;

        sb.AppendLine($"  Scan: {targetQ_values.Length} densities × {layouts.Length} layouts × {seeds} seeds = {targetQ_values.Length * layouts.Length * seeds} runs");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
        sb.Clear();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = CollectiveWaveFieldAnalyzer.Analyze(
            K_values, lambda_values, targetQ_values, layouts, N: 300, seedsPerPoint: seeds);
        sw.Stop();

        Sec(sb, "3. Closure Test Results");
        sb.AppendLine($"  Completed in {sw.Elapsed.TotalSeconds:F0}s. {report.Runs.Count} runs.");
        sb.AppendLine($"  Field autonomy: {(report.FieldAutonomyFound ? "YES" : "NO")}");
        sb.AppendLine($"  Best equation: {report.BestFieldEquation}");
        sb.AppendLine($"  Critical density: {report.CriticalDensityForAutonomy:F2}");
        sb.AppendLine();

        sb.AppendLine("  Closure ratio (field_err / particle_err) vs density:");
        sb.AppendLine("  ρ_Q   │ Field Err │ Part Err │ Ratio │ Autonomous? │ Regime");
        sb.AppendLine("  " + new string('─', 70));
        foreach (var c in report.ClosureTests)
            sb.AppendLine($"  {c.Density,5:F2} │ {c.FieldRMSError,9:F4} │ {c.ParticleRMSError,8:F4} │ {c.ClosureRatio,5:F2} │ {(c.FieldOutperforms ? "YES" : "NO"),-10} │ {(c.ClosureRatio < 0.8 ? "Field" : c.ClosureRatio < 1.2 ? "Mixed" : "Particle")}");
        sb.AppendLine();

        Sec(sb, "4. Field Equation Comparison");
        sb.AppendLine($"  {report.Predictions.Count} predictions across {report.Predictions.Select(p => p.ModelType).Distinct().Count()} models");
        sb.AppendLine("  Model               │ RMSE   │ R²     │ Accurate?");
        sb.AppendLine("  " + new string('─', 50));
        var bestModels = report.Predictions.GroupBy(p => p.ModelType)
            .Select(g => new { Model = g.Key, AvgR2 = g.Average(p => p.R2Score), AvgRMSE = g.Average(p => p.RMSError) })
            .OrderByDescending(x => x.AvgR2);
        foreach (var m in bestModels)
            sb.AppendLine($"  {m.Model,-20} │ {m.AvgRMSE,6:F4} │ {m.AvgR2,6:F3} │ {(m.AvgR2 > 0.5 ? "YES" : "NO")}");
        sb.AppendLine();

        Sec(sb, "5. Particle-Field Phase Diagram");
        var pd = report.PhaseDiagram;
        sb.AppendLine("  Closure ratio (density × coupling):");
        sb.AppendLine("    < 1: Field model better (autonomous).");
        sb.AppendLine("    > 1: Particle model better (micro needed).");
        sb.AppendLine();
        sb.AppendLine("  K →");
        for (int d = pd.DensityAxis.Length - 1; d >= 0; d--)
        {
            sb.Append("    ");
            for (int c = 0; c < pd.CouplingAxis.Length; c++)
                sb.Append($" {pd.ClosureRatioGrid[d, c]:F2}");
            sb.AppendLine($"  ρ={pd.DensityAxis[d]:F2}");
        }
        sb.AppendLine();
        sb.AppendLine(pd.Description);
        sb.AppendLine();

        Sec(sb, "6. Information-Loss Analysis");
        sb.AppendLine("  At low density: individual charge phases contain unique information.");
        sb.AppendLine("    → Field model loses this → high prediction error.");
        sb.AppendLine("  At high density: phases become correlated (R_Q → 1).");
        sb.AppendLine("    → Θ captures most information → field model performs well.");
        sb.AppendLine("  Information retention = R² of best field model:");
        foreach (var c in report.ClosureTests)
            sb.AppendLine($"    ρ_Q={c.Density:F2}: retention={c.InformationRetention:F1}%, best={c.BestModel}");
        sb.AppendLine();

        Sec(sb, "7. Hostile Review");
        sb.AppendLine("  ATTEMPT 1: Is the 'closure' just a consequence of R_Q→1?");
        sb.AppendLine("    → PARTIALLY. R_Q→1 means all phases equal → Θ trivial to predict.");
        sb.AppendLine("    → But closure means Θ is a COMPLETE description — not just trivial.");
        sb.AppendLine("    → A single global phase IS an autonomous field variable.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 2: Does the field equation have predictive power beyond persistence?");
        sb.AppendLine("    → The null model (persistence: Θ(t+Δt)=Θ(t)) is the baseline.");
        sb.AppendLine("    → If damped wave beats persistence: genuine dynamics captured.");
        sb.AppendLine("    → If persistence wins: Θ changes slowly — still autonomous but trivial.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 3: Is the autonomy threshold a finite-size artifact?");
        sb.AppendLine("    → At small N: fluctuations dominate → particle model always better.");
        sb.AppendLine("    → At N→∞: law of large numbers → field model exact.");
        sb.AppendLine($"    → ρ_c({report.CriticalDensityForAutonomy:F2}) at N=300 may shift with N.");
        sb.AppendLine();

        sb.AppendLine("  ATTEMPT 4: Can we falsify by showing Θ predicts Q poorly?");
        sb.AppendLine("    → Θ describes PHASE dynamics, not CHARGE dynamics.");
        sb.AppendLine("    → Q = β₀({R>0.5}) is a separate conserved quantity.");
        sb.AppendLine("    → Θ autonomy ≠ complete theory. Q + Θ together = complete.");
        sb.AppendLine();

        Sec(sb, "8. Research Questions");
        sb.AppendLine(CollectiveWaveFieldAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        Sec(sb, "9. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-128 completed.  Runtime: {sw.Elapsed.TotalSeconds:F0}s.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Θ autonomy: {(report.FieldAutonomyFound ? "ESTABLISHED" : "NOT ESTABLISHED")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
