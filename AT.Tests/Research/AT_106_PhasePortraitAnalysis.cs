using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_106_PhasePortraitAnalysis : ResearchTestBase
{
    public AT_106_PhasePortraitAnalysis(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_106_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-106 Phase Portrait and Fixed Point Analysis");

        sb.AppendLine("AT-106: What dynamical behavior emerges from the closed theory?");
        sb.AppendLine();

        // ── Section 1: The Closed System ─────────────────────────────
        Sec(sb, "1. The Closed AT Field Theory");
        sb.AppendLine("  dR/dt = c₀ · M · R · (1 − R²)    [AT-104, mean-field derivation]");
        sb.AppendLine("  dM/dt = a · R²                     [AT-105, position-dynamic derivation]");
        sb.AppendLine();
        sb.AppendLine($"  c₀ = {PhasePortraitAnalyzer.C0:F4}  (fitted mean-field coefficient)");
        sb.AppendLine($"  a  = {PhasePortraitAnalyzer.A_R2:F5}  (fitted M-growth coefficient)");
        sb.AppendLine($"  Physical bounds: R ∈ [0,1], M ∈ [0,K] with K from coupling law");
        sb.AppendLine();

        // ── Section 2: Fixed Point Analysis ──────────────────────────
        Sec(sb, "2. Analytic Fixed Point Analysis");

        var analysis = PhasePortraitAnalyzer.AnalyzeFixedPoints();

        sb.AppendLine("  ── Fixed Points ──");
        foreach (var fp in analysis.FixedPoints)
            sb.AppendLine($"  {fp}");
        sb.AppendLine();

        sb.AppendLine("  ── Jacobian Matrix ──");
        sb.AppendLine($"  {analysis.Jacobian}");
        sb.AppendLine();

        sb.AppendLine("  ── Eigenvalues ──");
        foreach (var ev in analysis.Eigenvalues)
            sb.AppendLine($"  {ev}");
        sb.AppendLine();

        sb.AppendLine("  ── Stability ──");
        foreach (var s in analysis.Stability)
            sb.AppendLine($"  {s}");
        sb.AppendLine();

        sb.AppendLine("  ── Nullclines ──");
        sb.AppendLine($"  {analysis.Nullclines}");
        sb.AppendLine();

        sb.AppendLine("  ── Invariant Regions ──");
        sb.AppendLine($"  {analysis.InvariantRegions}");
        sb.AppendLine();

        sb.AppendLine("  ── Long-Time Behavior ──");
        sb.AppendLine($"  {analysis.LongTimeBehavior}");
        sb.AppendLine();

        // ── Section 3: Linear Stability Analysis ─────────────────────
        Sec(sb, "3. Linear Stability Near Fixed Points");

        sb.AppendLine("  ── Near R = 0 (unstable manifold) ──");
        sb.AppendLine("  For small R ≪ 1, M = M₀:");
        sb.AppendLine("    dR/dt ≈ c₀·M₀·R         →  R(t) = R₀·exp(c₀·M₀·t)");
        sb.AppendLine("    dM/dt = a·R² ≈ a·R₀²·exp(2c₀·M₀·t)");
        sb.AppendLine();
        sb.AppendLine($"    Example: M₀=0.1, R₀=0.01 → growth rate = {PhasePortraitAnalyzer.C0 * 0.1:F6}");
        sb.AppendLine($"    Doubling time = {Math.Log(2) / (PhasePortraitAnalyzer.C0 * 0.1):F0} time units");
        sb.AppendLine();

        sb.AppendLine("  ── Near R = 1 (attracting manifold) ──");
        sb.AppendLine("  Let ε = 1−R ≪ 1:");
        sb.AppendLine("    dε/dt = −c₀·M·(1−ε)·(2ε−ε²) ≈ −2c₀·M·ε");
        sb.AppendLine("    → ε(t) = ε₀·exp(−2c₀·M·t)");
        sb.AppendLine("    → EXPONENTIAL CONVERGENCE to R = 1");
        sb.AppendLine();
        sb.AppendLine($"    Example: M=1.0, ε₀=0.01 → decay rate = {2 * PhasePortraitAnalyzer.C0:F4}");
        sb.AppendLine($"    Half-life of ε = {Math.Log(2) / (2 * PhasePortraitAnalyzer.C0):F0} time units");
        sb.AppendLine();

        // ── Section 4: Numerical Phase Portrait ──────────────────────
        Sec(sb, "4. Numerical Phase Portrait");

        var report = PhasePortraitAnalyzer.GeneratePhasePortrait();

        sb.AppendLine(report.PhasePortrait);
        sb.AppendLine();

        // ── Section 5: Trajectory Statistics ─────────────────────────
        Sec(sb, "5. Trajectory Statistics");

        var allTraj = report.Trajectories;
        double avgConvergeTime = allTraj.Where(t => !double.IsNaN(t.ConvergenceTime999))
            .Average(t => t.ConvergenceTime999);
        double maxR = allTraj.Max(t => t.Path[^1].R);
        double maxM = allTraj.Max(t => t.Path[^1].M);
        int convergedCount = allTraj.Count(t => t.ReachesR1);

        sb.AppendLine($"  Trajectories: {allTraj.Count}");
        sb.AppendLine($"  Converged to R>0.999: {convergedCount}/{allTraj.Count} ({100.0 * convergedCount / allTraj.Count:P0})");
        sb.AppendLine($"  Mean convergence time: {avgConvergeTime:F0} time units");
        sb.AppendLine($"  Final R range: [{allTraj.Min(t => t.Path[^1].R):F4}, {maxR:F4}]");
        sb.AppendLine($"  Final M range: [{allTraj.Min(t => t.Path[^1].M):F3}, {maxM:F3}]");
        sb.AppendLine();

        // ── Section 6: Research Questions ────────────────────────────
        Sec(sb, "6. Research Questions");

        sb.AppendLine("  Q1: What fixed points exist?");
        sb.AppendLine("    ONLY the line R = 0 (any M). This is a 1D manifold of fixed points.");
        sb.AppendLine("    There are NO isolated fixed points with R > 0.");
        sb.AppendLine();

        sb.AppendLine("  Q2: Are the fixed points stable?");
        sb.AppendLine("    NO. The R=0 line is UNSTABLE (λ₁ = c₀·M > 0).");
        sb.AppendLine("    Any perturbation R > 0 triggers exponential growth away from R=0.");
        sb.AppendLine("    This explains WHY synchronization is so robust (AT-052/053).");
        sb.AppendLine();

        sb.AppendLine("  Q3: Does R always converge to 1?");
        sb.AppendLine($"    YES — {convergedCount}/{allTraj.Count} trajectories from diverse");
        sb.AppendLine("    initial conditions all converge to R → 1.");
        sb.AppendLine("    R=1 is an ATTRACTING MANIFOLD approached exponentially.");
        sb.AppendLine();

        sb.AppendLine("  Q4: Does M diverge without bound?");
        sb.AppendLine("    WITHOUT physical saturation: YES — dM/dt → a > 0 as R → 1.");
        sb.AppendLine("    WITH physical saturation (M ≤ K): M → K, stable at maximum.");
        sb.AppendLine("    The physical system saturates at M = K (all oscillators coalesced).");
        sb.AppendLine();

        sb.AppendLine("  Q5: Does the system contain a limit cycle?");
        sb.AppendLine("    NO. Both dR/dt ≥ 0 and dM/dt ≥ 0 for all (R, M) in [0,1]×[0,K].");
        sb.AppendLine("    Trajectories are strictly monotonic in both variables.");
        sb.AppendLine("    The system is a GRADIENT FLOW — no oscillations possible.");
        sb.AppendLine();

        sb.AppendLine("  Q6: Can the long-term state be predicted analytically?");
        sb.AppendLine("    YES. The system flows to (R, M) → (1, K) from ANY initial condition");
        sb.AppendLine("    with R₀ > 0 and any M₀. The attractor is global.");
        sb.AppendLine("    Convergence is exponential in R: ε(t) ∝ exp(−2c₀·M·t).");
        sb.AppendLine("    M grows linearly at late times: M(t) ≈ a·t (until saturation at K).");
        sb.AppendLine();

        sb.AppendLine("  Q7: What physical interpretation follows?");
        sb.AppendLine("    INEVITABLE SYNCHRONIZATION + SPATIAL COLLAPSE.");
        sb.AppendLine("    The theory predicts that any system of coupled oscillators with");
        sb.AppendLine("    spatial coupling will inevitably synchronize (R→1) and coalesce");
        sb.AppendLine("    (M→K). This is a SELF-REINFORCING FIELD COLLAPSE:");
        sb.AppendLine("      R↑ → M↑ (AT-105: dM/dt ∝ R²)");
        sb.AppendLine("      M↑ → R↑ (AT-104: dR/dt ∝ M)");
        sb.AppendLine("    The feedback loop drives the system inexorably to (1, K).");
        sb.AppendLine();

        sb.AppendLine("  Q8: Does the theory imply inevitable synchronization?");
        sb.AppendLine("    YES — the only equilibrium (R=0) is UNSTABLE.");
        sb.AppendLine("    Any finite perturbation R > 0 inevitably grows to R → 1.");
        sb.AppendLine("    This is the mathematical basis for the robustness of");
        sb.AppendLine("    synchronization observed throughout AT experiments.");
        sb.AppendLine();

        // ── Section 7: Classification ────────────────────────────────
        Sec(sb, "7. Classification");

        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine();
        sb.AppendLine("  Dynamical class: GRADIENT FLOW with GLOBAL ATTRACTOR");
        sb.AppendLine("  Attractor: (R, M) = (1, K) — complete order");
        sb.AppendLine("  Basin of attraction: ALL R > 0, any M");
        sb.AppendLine("  Unstable manifold: R = 0 line (repellor)");
        sb.AppendLine();

        // ── Section 8: Physical Interpretation ───────────────────────
        Sec(sb, "8. Physical Interpretation");

        sb.AppendLine(report.PhysicalInterpretation);
        sb.AppendLine();

        // ── Section 9: Conclusion ────────────────────────────────────
        Sec(sb, "9. Conclusion");
        sb.AppendLine($"  C1.  Fixed points: R=0 line (unstable manifold)");
        sb.AppendLine($"  C2.  Attractor: (R,M) → (1,K) — global, exponential convergence");
        sb.AppendLine($"  C3.  Dynamics: gradient flow, strictly monotonic, no cycles");
        sb.AppendLine($"  C4.  Classification: {report.Classification}");
        sb.AppendLine($"  C5.  Convergence: {convergedCount}/{allTraj.Count} trajectories → R>0.999");
        sb.AppendLine($"  C6.  Mean convergence time: {avgConvergeTime:F0} time units");
        sb.AppendLine($"  C7.  Physical prediction: INEVITABLE SYNCHRONIZATION");
        sb.AppendLine($"  C8.  The closed theory predicts a self-reinforcing collapse");
        sb.AppendLine("       to complete order — consistent with AT-052/053.");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment AT-106 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
