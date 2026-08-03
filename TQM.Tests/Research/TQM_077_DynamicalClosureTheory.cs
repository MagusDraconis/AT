using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_077_DynamicalClosureTheory : ResearchTestBase
{
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int NPerGroup = 50;
    private const int BaseSeed = 770325819;
    private static readonly double[] RTargets = { 0.3, 0.5, 0.7, 0.9 };
    private static readonly string[] Laws = { "cos", "cos²", "exp(-|x|)", "1/(1+|x|)" };
    private const int SeedsPerPoint = 2;

    public TQM_077_DynamicalClosureTheory(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_077_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-077 Dynamical Closure Theory");

        sb.AppendLine("TQM-077: Do Dynamical Evolution Terms Close the Trajectory Theory?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  TQM-074: F_net = Alignment × ⟨f⟩.");
        sb.AppendLine("  TQM-075: Alignment ≈ R².");
        sb.AppendLine("  TQM-076: Static initial conditions predict always-positive");
        sb.AppendLine("    laws but fail for sign-changing laws (cos).");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: Dynamical evolution terms (dR/dt, dA/dt,");
        sb.AppendLine("  dF/dt) close the prediction gap. Including how the");
        sb.AppendLine("  state CHANGES enables accurate trajectory forecasting.");
        sb.AppendLine();

        // ── Section 2: Experimental Setup ────────────────────────────
        Sec(sb, "2. Experimental Setup");
        sb.AppendLine($"  R targets: [{string.Join(", ", RTargets)}]");
        sb.AppendLine($"  Laws: {string.Join(", ", Laws)}");
        sb.AppendLine($"  Seeds: {SeedsPerPoint}, N={NPerGroup * 2},");
        sb.AppendLine($"  Trajectory length: 100 steps, recorded every 2");
        sb.AppendLine();
        sb.AppendLine("  Models (increasing complexity):");
        sb.AppendLine("    A: Static (A·⟨f⟩)     — initial state only");
        sb.AppendLine("    B: Static + R         — add coherence");
        sb.AppendLine("    C: + dR/dt            — add coherence evolution");
        sb.AppendLine("    D: + dA/dt            — add alignment evolution");
        sb.AppendLine("    E: Full Dynamic       — + dF/dt (all derivatives)");
        sb.AppendLine();

        // ── Run ──────────────────────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var reports = DynamicalClosureAnalyzer.RunClosureAnalysis(
            RTargets, Laws, K, Lambda, NPerGroup, SeedsPerPoint, BaseSeed);
        sw.Stop();

        sb.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        // ── Section 3: Dynamic Variables ─────────────────────────────
        Sec(sb, "3. Dynamic Variable Evolution (cos, R=0.5 sample)");

        // Show a sample trajectory.
        sb.AppendLine("  Step │ R      │ dR/dt   │ A       │ dA/dt   │ F       │ dF/dt");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var report in reports.Values)
        {
            var bestModel = report.Models[0];
            sb.AppendLine();
            sb.AppendLine($"  Law: {report.LawName}");
            sb.AppendLine($"  Model │ R²      │ RMSE    │ ΔR²      │ Description");
            sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

            // Show models in order.
            foreach (var m in report.Models.OrderBy(m => m.Name.Length))
            {
                double delta = m.R2 - report.StaticR2;
                string ds = delta >= 0 ? $"+{delta:F4}" : $"{delta:F4}";
                string star = m == bestModel ? " \u2605" : "  ";
                sb.AppendLine($"  {m.Name,-24}{star} │ {m.R2,6:F4} │ {m.RMSE,6:F4} │ {ds,7} │ {m.Description}");
            }
            sb.AppendLine();
        }

        // ── Section 5: Closure Analysis ──────────────────────────────
        Sec(sb, "5. Dynamical Closure Summary");

        sb.AppendLine("  Law          │ Static R²│ Best R²  │ Gain     │ Best Model       │ Class");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        foreach (var (law, report) in reports)
        {
            sb.AppendLine($"  {law,-11} │ {report.StaticR2,8:F4} │ {report.BestR2,7:F4} │ {report.DynamicalGain,9:+0.0000} │ {report.Models[0].Name,-16} │ {report.Classification}");
        }
        sb.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        // Focus on cos — the sign-changing law that failed in TQM-076.
        var cosReport = reports["cos"];
        var cosStatic = cosReport.Models.First(m => m.Name == "Static (A·⟨f⟩)");
        var cosDR = cosReport.Models.First(m => m.Name == "Static + dR/dt");
        var cosFull = cosReport.Models.First(m => m.Name == "Full Dynamic");

        sb.AppendLine("  Q1: Does dR/dt improve trajectory prediction?");
        sb.AppendLine($"    cos: static R²={cosStatic.R2:F4}, +dR/dt R²={cosDR.R2:F4}");
        sb.AppendLine($"    ΔR² = {cosDR.R2 - cosStatic.R2:+0.0000}");
        sb.AppendLine();

        sb.AppendLine("  Q2: Does dA/dt improve trajectory prediction?");
        var cosDA = cosReport.Models.First(m => m.Name == "Static + dR/dt + dA/dt");
        sb.AppendLine($"    cos: +dR R²={cosDR.R2:F4}, +dR+dA R²={cosDA.R2:F4}");
        sb.AppendLine($"    ΔR² = {cosDA.R2 - cosDR.R2:+0.0000}");
        sb.AppendLine();

        sb.AppendLine("  Q3: Can sign-changing laws be predicted accurately?");
        sb.AppendLine($"    cos full dynamic R² = {cosFull.R2:F4}");
        sb.AppendLine($"    {(cosFull.R2 > 0.5 ? "YES — Dynamical closure works for sign-changing laws" : cosFull.R2 > cosStatic.R2 + 0.05 ? "PARTIALLY — Improvement but not full closure" : "NO — Even dynamical terms insufficient for sign-changing laws")}");
        sb.AppendLine();

        sb.AppendLine("  Q4: Does a closed dynamical description exist?");
        double maxGain = reports.Values.Max(r => r.DynamicalGain);
        sb.AppendLine($"    Max dynamical gain across laws: {maxGain:+.F4}");
        sb.AppendLine($"    {(maxGain > 0.10 ? "YES — Dynamical closure achieved" : maxGain > 0.03 ? "PARTIALLY — Modest closure" : "NO — Full closure not achieved")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: How much predictive power comes from derivatives vs state?");
        sb.AppendLine($"    Best static R² (any law): {reports.Values.Max(r => r.StaticR2):F4}");
        sb.AppendLine($"    Best dynamic R² (any law): {reports.Values.Max(r => r.BestR2):F4}");
        sb.AppendLine();

        // ── Section 6: Interpretation ────────────────────────────────
        Sec(sb, "6. Interpretation");

        string bestClass = reports.Values
            .OrderByDescending(r => r.DynamicalGain).First().Classification;
        sb.AppendLine($"  Best classification: {bestClass}");
        sb.AppendLine();

        if (cosReport.DynamicalGain > 0.03)
        {
            sb.AppendLine("  Dynamical terms IMPROVE trajectory prediction for");
            sb.AppendLine("  sign-changing coupling laws. The derivatives dR/dt");
            sb.AppendLine("  and dA/dt capture the direction in which phase");
            sb.AppendLine("  alignment is evolving, enabling the model to");
            sb.AppendLine("  anticipate force sign changes.");
            sb.AppendLine();
            sb.AppendLine("  This CLOSES the theory: the full force-emergence");
            sb.AppendLine("  framework now includes both static structure");
            sb.AppendLine("  (P(θ) → A → F) and dynamical evolution (dR/dt,");
            sb.AppendLine("  dA/dt → ΔF → Δsep).");
        }
        else
        {
            sb.AppendLine("  Dynamical terms do NOT significantly improve");
            sb.AppendLine("  trajectory prediction for sign-changing laws.");
            sb.AppendLine("  The phase evolution during motion is too chaotic");
            sb.AppendLine("  to be captured by local derivatives — the system");
            sb.AppendLine("  may require a fully coupled phase-position");
            sb.AppendLine("  integration for accurate prediction.");
        }
        sb.AppendLine();

        // ── Section 7: Conclusion ────────────────────────────────────
        Sec(sb, "7. Conclusion");
        sb.AppendLine($"  C1. Best classification: {bestClass}");
        sb.AppendLine($"  C2. cos static R²: {cosStatic.R2:F4}");
        sb.AppendLine($"  C3. cos dynamic R²: {cosFull.R2:F4}");
        sb.AppendLine($"  C4. Dynamical gain (cos): {cosReport.DynamicalGain:+.F4}");
        sb.AppendLine($"  C5. Max gain (any law): {maxGain:+.F4}");
        sb.AppendLine();

        sb.AppendLine("  Model progression (cos):");
        foreach (var m in cosReport.Models.OrderBy(m => m.Name.Length))
            sb.AppendLine($"    {m.Name}: R²={m.R2:F4}, RMSE={m.RMSE:F4}");
        sb.AppendLine();

        sb.AppendLine($"  C6. {cosReport.Interpretation}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment TQM-077 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
