using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_082_MeanCouplingFieldEquation : ResearchTestBase
{
    private const int N = 100;
    private const int BaseSeed = 820479163;
    private const int TotalSteps = 500;
    private const int SnapshotInterval = 10;
    private const double PositionStep = 0.001;

    // Parameter grid.
    private static readonly string[] Topologies =
        { "uniform", "clustered", "linear", "circular", "dense-sparse", "random-clusters" };
    private static readonly double[] KValues = { 1.0, 2.0, 5.0 };
    private static readonly double[] Lambdas = { 0.05, 0.10 };
    private const int SeedsPerCombo = 2;

    public AT_082_MeanCouplingFieldEquation(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_082_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-082 Mean Coupling Field Equation");

        sb.AppendLine("AT-082: Does MeanCoupling Obey a Simple Dynamical Field Equation?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  AT-081: MeanCoupling compresses 97.7% of topology info.");
        sb.AppendLine("  Network structure reduces to a single scalar M = ⟨K_ij⟩.");
        sb.AppendLine();
        sb.AppendLine("  This experiment asks: is M merely a compressed descriptor,");
        sb.AppendLine("  or does it behave as a genuine dynamical field variable");
        sb.AppendLine("  with its own equation of motion dM/dt = f(M, R)?");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: M evolves according to a simple deterministic");
        sb.AppendLine("  law coupling M and the coherence order parameter R.");
        sb.AppendLine();

        // ── Section 2: Experimental Setup ────────────────────────────
        Sec(sb, "2. Experimental Setup");
        int totalProfiles = Topologies.Length * KValues.Length * Lambdas.Length * SeedsPerCombo;
        sb.AppendLine($"  {totalProfiles} simulation profiles:");
        sb.AppendLine($"    {Topologies.Length} topology types × {KValues.Length} K values");
        sb.AppendLine($"    × {Lambdas.Length} λ values × {SeedsPerCombo} seeds");
        sb.AppendLine($"  N = {N}, {TotalSteps} steps, snapshot every {SnapshotInterval}");
        sb.AppendLine($"  Position dynamics: γ = {PositionStep} (coupling-energy gradient)");
        sb.AppendLine();
        sb.AppendLine("  Both phase (Kuramoto) and position evolution tracked.");
        sb.AppendLine("  Coupling matrix recomputed each step from evolving positions.");
        sb.AppendLine();
        sb.AppendLine("  6 candidate field equations dM/dt = f(M, R):");
        sb.AppendLine("    A: a₀ + a₁·M");
        sb.AppendLine("    B: a₀ + a₁·M + a₂·M²");
        sb.AppendLine("    C: a₀ + a₁·R");
        sb.AppendLine("    D: a₀ + a₁·M + a₂·R");
        sb.AppendLine("    E: a₀ + a₁·M·R");
        sb.AppendLine("    F: a₀ + a₁·M + a₂·R + a₃·M² + a₄·R² + a₅·M·R");
        sb.AppendLine();

        // ── Run simulations ──────────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();

        var profiles = new List<MeanCouplingFieldAnalyzer.MeanCouplingProfile>();
        int seedCounter = 0;

        var combos = new List<(string topo, double k, double lam)>();
        foreach (var topo in Topologies)
            foreach (var k in KValues)
                foreach (var lam in Lambdas)
                    combos.Add((topo, k, lam));

        // Parallel simulation for efficiency.
        var lockObj = new object();
        Parallel.ForEach(combos, combo =>
        {
            for (int s = 0; s < SeedsPerCombo; s++)
            {
                int seed = BaseSeed + Interlocked.Increment(ref seedCounter) * 7919;
                var profile = MeanCouplingFieldAnalyzer.SimulateProfile(
                    combo.topo, combo.k, combo.lam, N, seed,
                    TotalSteps, SnapshotInterval, PositionStep);
                lock (lockObj) profiles.Add(profile);
            }
        });

        sw.Stop();
        sb.AppendLine($"  Simulations completed in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        // ── Analyze ──────────────────────────────────────────────────
        var report = MeanCouplingFieldAnalyzer.Analyze(profiles);

        // ── Section 3: Mean Coupling Dynamics ────────────────────────
        Sec(sb, "3. Mean Coupling Dynamics");

        sb.AppendLine("  M(t) evolution by topology type:");
        sb.AppendLine("  Topology       │ K    │ λ    │ M(0)     │ M(end)   │ ΔM       │ ΔM/M(0)");
        sb.AppendLine("  " + new string('─', 88));

        var byParams = profiles.GroupBy(p => (p.TopologyType, p.K, p.Lambda)).ToList();
        foreach (var g in byParams)
        {
            double m0 = g.Average(p => p.M[0]);
            double mEnd = g.Average(p => p.M[^1]);
            double delta = mEnd - m0;
            double frac = m0 > 1e-10 ? delta / m0 : 0;
            sb.AppendLine($"  {g.Key.TopologyType,-14} │ {g.Key.K,4:F1} │ {g.Key.Lambda,4:F2} │ {m0,8:F5} │ {mEnd,8:F5} │ {delta,11:F5} │ {frac,9:P0}");
        }
        sb.AppendLine();

        sb.AppendLine($"  M range across all data: [{M_min(profiles):F6}, {M_max(profiles):F6}]");
        sb.AppendLine($"  ΔM range (max change):   [{dM_min(profiles):F6}, {dM_max(profiles):F6}]");
        sb.AppendLine($"  R range:                 [{R_min(profiles):F4}, {R_max(profiles):F4}]");

        double fracChanging = profiles.Count(p =>
            Math.Abs(p.M[^1] - p.M[0]) / Math.Max(p.M[0], 1e-10) > 0.01);
        sb.AppendLine($"  Profiles with >1% M change: {fracChanging}/{profiles.Count} ({fracChanging / profiles.Count:P0})");
        sb.AppendLine();

        // ── Section 4: Candidate Equations ───────────────────────────
        Sec(sb, "4. Candidate Field Equations");

        sb.AppendLine("  Model │ Equation                              │ P │    R²   │ Adj R²  │    AIC");
        sb.AppendLine("  " + new string('─', 100));
        foreach (var fit in report.Fits)
            sb.AppendLine($"  {fit.ModelLabel,-5} │ {fit.Equation,-38} │ {fit.NumParams,1} │ {fit.R2,6:F4} │ {fit.AdjustedR2,7:F4} │ {fit.AIC,7:F1}");
        sb.AppendLine();

        // Show parameters for top models.
        sb.AppendLine("  ── Best Model Coefficients ──");
        var best = report.Fits[0];
        for (int i = 0; i < best.Parameters.Length && i < best.ParamNames.Length; i++)
            sb.AppendLine($"    {best.ParamNames[i],-12} = {best.Parameters[i],18:E8}");
        sb.AppendLine();

        // ── Model comparison ─────────────────────────────────────────
        sb.AppendLine("  ── Model Comparison ──");
        sb.AppendLine("  Model │ ΔAdjR² (vs A) │ ΔAIC (vs best) │ Interpretation");
        sb.AppendLine("  " + new string('─', 75));
        foreach (var fit in report.Fits)
        {
            double daic = fit.AIC - best.AIC;
            string interp = daic < 2 ? "≈ best (ΔAIC < 2)" :
                            daic < 7 ? "plausible" :
                            daic < 10 ? "weak support" : "significantly worse";
            sb.AppendLine($"  {fit.ModelLabel,-5} │ {fit.AdjustedR2 - report.Fits[^1].AdjustedR2,+12:F4} │ {daic,+15:F1} │ {interp}");
        }
        sb.AppendLine();

        // ── Section 5: Per-Parameter Analysis ────────────────────────
        Sec(sb, "5. Per-Parameter Breakdown");

        sb.AppendLine("  Parameter       │ Topology   │ K   │ λ    │ Mean ΔM   │ Mean ΔR   │ |ΔM|/M₀");
        sb.AppendLine("  " + new string('─', 92));

        foreach (var g in byParams)
        {
            double meanDM = g.Average(p => p.M[^1] - p.M[0]);
            double meanDR = g.Average(p => p.R[^1] - p.R[0]);
            double m0 = g.Average(p => p.M[0]);
            double frac = m0 > 1e-10 ? Math.Abs(meanDM) / m0 : 0;
            sb.AppendLine($"  {g.Key.TopologyType,-14} │ {g.Key.K,4:F1} │ {g.Key.Lambda,4:F2} │ {meanDM,11:F5} │ {meanDR,11:F4} │ {frac,8:P1}");
        }
        sb.AppendLine();

        // ── Section 6: Field Interpretation ──────────────────────────
        Sec(sb, "6. Field Interpretation");

        // Check if M is essentially static.
        double meanAbsFracChange = profiles.Average(p =>
        {
            double m0 = Math.Max(p.M[0], 1e-10);
            return Math.Abs(p.M[^1] - p.M[0]) / m0;
        });

        sb.AppendLine($"  Mean |ΔM/M₀| across all profiles: {meanAbsFracChange:P1}");
        sb.AppendLine($"  M range: {report.MRange:F6}    dM/dt range: {report.dMdtRange:F6}");
        sb.AppendLine($"  R range: {report.RRange:F4}");
        sb.AppendLine();

        sb.AppendLine($"  Best model: {report.BestModel} (R² = {report.BestR2:F4})");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine();

        // ── Section 7: Research Questions ────────────────────────────
        Sec(sb, "7. Research Questions");

        sb.AppendLine("  Q1: Can dM/dt be predicted?");
        sb.AppendLine($"    Best Adj R² = {best.AdjustedR2:F4} ({best.Equation})");
        if (best.AdjustedR2 >= 0.3)
            sb.AppendLine("    YES — M evolution follows a predictable dynamical law.");
        else if (best.AdjustedR2 >= 0.1)
            sb.AppendLine("    PARTIALLY — M evolution shows weak predictability.");
        else
            sb.AppendLine("    NO — M evolution is dominated by noise, not deterministic dynamics.");
        sb.AppendLine();

        sb.AppendLine("  Q2: Is MeanCoupling a dynamical variable?");
        if (meanAbsFracChange > 0.05)
            sb.AppendLine($"    YES — M changes by {meanAbsFracChange:P0} on average (significant).");
        else if (meanAbsFracChange > 0.01)
            sb.AppendLine($"    WEAKLY — M changes by {meanAbsFracChange:P0} (marginal dynamics).");
        else
            sb.AppendLine($"    NO — M changes by {meanAbsFracChange:P0} (essentially static).");
        sb.AppendLine();

        sb.AppendLine("  Q3: Does a field equation exist?");
        sb.AppendLine($"    Classification: {report.Classification}");
        sb.AppendLine($"    {report.Interpretation}");
        sb.AppendLine();

        sb.AppendLine("  Q4: Can topology be completely replaced by M?");
        sb.AppendLine("    AT-081 showed M captures 97.7% of topology info for dR/dt.");
        if (best.AdjustedR2 >= 0.3)
            sb.AppendLine("    AT-082 shows M itself follows a predictable law → CLOSED THEORY.");
        else if (best.AdjustedR2 >= 0.1)
            sb.AppendLine("    AT-082 shows M has weak self-dynamics → partially closed.");
        else
            sb.AppendLine("    AT-082 shows M is static → topology replacement is complete but M needs no equation.");
        sb.AppendLine();

        sb.AppendLine("  Q5: Does M represent a true effective field?");
        sb.AppendLine($"    {report.Classification}");
        if (report.Classification.StartsWith("D"))
            sb.AppendLine("    YES — M is a genuine dynamical field with deterministic evolution.");
        else if (report.Classification.StartsWith("C"))
            sb.AppendLine("    PARTIALLY — M acts as an effective field with statistical predictability.");
        else if (report.Classification.StartsWith("B"))
            sb.AppendLine("    WEAKLY — M shows marginal dynamics, mostly a compressed descriptor.");
        else
            sb.AppendLine("    NO — M is purely a compressed static descriptor, not a field.");
        sb.AppendLine();

        // ── Additional: dR/dt coupling to M ──────────────────────────
        Sec(sb, "7b. Two-Way Coupling Analysis");
        sb.AppendLine("  AT-081: dR/dt = f(R, M)  →  R² = 0.758");
        sb.AppendLine($"  AT-082: dM/dt = f(M, R)  →  Adj R² = {best.AdjustedR2:F4}");

        if (best.AdjustedR2 >= 0.3)
            sb.AppendLine("  → TWO-WAY COUPLING: M ↔ R form a closed dynamical system.");
        else if (best.AdjustedR2 >= 0.1)
            sb.AppendLine("  → ASYMMETRIC: R depends on M, but M evolves slowly with weak predictability.");
        else
            sb.AppendLine("  → ONE-WAY: R depends on M (AT-081), but M is static → M is a parameter, not a variable.");
        sb.AppendLine();

        // ── Section 8: Interpretation ────────────────────────────────
        Sec(sb, "8. Interpretation");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Interpretation}");
        sb.AppendLine();

        // ── Section 9: Conclusion ────────────────────────────────────
        Sec(sb, "9. Conclusion");
        sb.AppendLine($"  C1. Classification: {report.Classification}");
        sb.AppendLine($"  C2. Best model: {best.ModelLabel} ({best.Equation})");
        sb.AppendLine($"  C3. Best Adj R²: {best.AdjustedR2:F4}");
        sb.AppendLine($"  C4. Best AIC: {best.AIC:F1}");
        sb.AppendLine($"  C5. M range: {report.MRange:F6}");
        sb.AppendLine($"  C6. dM/dt range: {report.dMdtRange:F6}");
        sb.AppendLine($"  C7. Mean |ΔM/M₀|: {meanAbsFracChange:P1}");
        sb.AppendLine($"  C8. Profiles: {profiles.Count} ({profiles.Count * TotalSteps / 1000}K total steps)");
        sb.AppendLine($"  C9. Data points pooled: {profiles.Sum(p => p.M.Length - 1)}");
        sb.AppendLine();
        sb.AppendLine($"  C10.{report.Interpretation}");

        // Check model quality trend.
        double r2Diff = report.Fits[0].R2 - report.Fits[^1].R2;
        if (r2Diff < 0.005)
            sb.AppendLine($"  C11.All models nearly equivalent (ΔR² = {r2Diff:F4}) — M has no dynamics.");
        else
            sb.AppendLine($"  C11.Model spread: ΔR² = {r2Diff:F4} — meaningful model discrimination.");

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment AT-082 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    // ══════════════════════════════════════════════════════════════════
    // Range helpers
    // ══════════════════════════════════════════════════════════════════

    private static double M_min(List<MeanCouplingFieldAnalyzer.MeanCouplingProfile> ps) =>
        ps.Min(p => p.M.Min());
    private static double M_max(List<MeanCouplingFieldAnalyzer.MeanCouplingProfile> ps) =>
        ps.Max(p => p.M.Max());
    private static double dM_min(List<MeanCouplingFieldAnalyzer.MeanCouplingProfile> ps) =>
        ps.Min(p => p.dMdt.Min());
    private static double dM_max(List<MeanCouplingFieldAnalyzer.MeanCouplingProfile> ps) =>
        ps.Max(p => p.dMdt.Max());
    private static double R_min(List<MeanCouplingFieldAnalyzer.MeanCouplingProfile> ps) =>
        ps.Min(p => p.R.Min());
    private static double R_max(List<MeanCouplingFieldAnalyzer.MeanCouplingProfile> ps) =>
        ps.Max(p => p.R.Max());

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
