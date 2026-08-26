using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 1 — derive ρ directly from microscopic Q-event actualization dynamics. Models actualization as
/// a Galton–Watson branching process over logarithmic layers; the per-octave deficit counts A_k = A₀·μ^k map to
/// the abundance exponent via μ = λ^(−α), so criticality (μ=1) ⟺ α=0 (the log-deficit attractor).
///
/// Tests: ATQG10 (branching → α; critical μ=1 → α=0 → log deficit), ATQG11 (branching-generated ρ reproduces
///        the gravity requirements), ATQG12 (criticality = unique scale-free point + classification).
/// </summary>
public class ATQG_Phase1_MicroscopicOriginOfRhoTests : ResearchTestBase
{
    public ATQG_Phase1_MicroscopicOriginOfRhoTests(ITestOutputHelper o) : base(o) { }

    private const double LAMBDA = 1.5;
    private const int K = 16;

    // ── ATQG10: branching ratio → α; critical μ=1 → α=0 → log deficit ──────────────

    [Fact]
    public void ATQG10_BranchingToAlpha()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG10: Q-event branching → α; criticality (μ=1) → α=0 → log deficit");

        // μ = λ^(−α): α = −ln μ / ln λ. Critical branching (μ=1) gives α=0.
        sb.AppendLine($"{"α",7} {"μ=λ^(−α)",12} {"α_back",10}");
        bool roundTrip = true;
        foreach (double alpha in new[] { 0.0, 0.5, 1.0 })
        {
            double mu = QEventBranching.MuFromAlpha(alpha, LAMBDA);
            double ab = QEventBranching.AlphaFromMu(mu, LAMBDA);
            if (Math.Abs(ab - alpha) > 1e-9) roundTrip = false;
            sb.AppendLine($"{alpha,7:F1} {mu,12:F4} {ab,10:F4}");
        }

        // Critical μ=1 → uniform per-octave counts → cumulative deficit = log deficit.
        double m0 = 0.4, r0 = 0.5, Rmax = r0 * Math.Pow(LAMBDA, K);
        var counts = QEventBranching.DeficitCounts(m0 / K, 1.0, K);
        bool logDeficit = true;
        sb.AppendLine();
        sb.AppendLine($"{"k",4} {"R_k",9} {"m_cumul",11} {"m_log",11}");
        for (int k = 0; k <= K; k += 4)
        {
            double Rk = r0 * Math.Pow(LAMBDA, k);
            double mCumul = k == K ? 0.0 : QEventBranching.CumulativeDeficit(counts, k);
            double mLog = m0 * Math.Log(Rmax / Rk) / Math.Log(Rmax / r0);
            if (Math.Abs(mCumul - mLog) > 1e-9) logDeficit = false;
            sb.AppendLine($"{k,4} {Rk,9:F2} {mCumul,11:F5} {mLog,11:F5}");
        }

        sb.AppendLine();
        sb.AppendLine($"μ ↔ α round-trip exact: {roundTrip}");
        sb.AppendLine($"critical μ=1 → cumulative deficit = log deficit (exact): {logDeficit}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the microscopic branching ratio μ maps bijectively to the abundance exponent α;");
        sb.AppendLine("criticality (μ=1) is exactly α=0, whose cumulative deficit is the log-deficit density.");
        Output.WriteLine(sb.ToString());

        Assert.True(roundTrip, "μ ↔ α should round-trip exactly");
        Assert.True(logDeficit, "critical branching should reproduce the log deficit");
    }

    // ── ATQG11: branching-generated ρ reproduces the gravity-required density ───────

    [Fact]
    public void ATQG11_ReproducesGravityDensity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG11: branching-generated ρ equals the gravity-required density exactly");

        sb.AppendLine($"{"α",7} {"ρ_branch",12} {"ρ_gravity",12} {"match",8}");
        bool allMatch = true;
        foreach (double alpha in new[] { 0.0, 0.5, 1.0 })
        {
            double rhoB = QEventBranching.Density(3.0, 0.4, alpha, 0.5, 10.0);
            double rhoG = DeficitCollective.AbundanceDeficit(3.0, alpha, 1.0, 0.4, 0.5, 10.0);
            bool match = Math.Abs(rhoB - rhoG) < 1e-12;
            if (!match) allMatch = false;
            sb.AppendLine($"{alpha,7:F1} {rhoB,12:F6} {rhoG,12:F6} {match,8}");
        }

        // The four gravity requirements (at α=0).
        double rho = QEventBranching.Density(3.0, 0.4, 0.0, 0.5, 10.0);
        double m = QEventBranching.DeficitDensity(3.0, 0.4, 0.0, 0.5, 10.0);
        double g11 = ActualizationGravity.Einstein11FromDensity(3.0, 3, 0.4, 0.5, 10.0);
        double gii = ActualizationGravity.EinsteinOtherFromDensity(3.0, 3, 0.4, 0.5, 10.0);
        double ratio = ActualizationGravity.RotationCurveRatio(3.0, 9.0, 3, 0.4, 0.5, 10.0);

        bool metricOrigin = rho > 0.0;
        bool deficitMatter = m > 0.0;
        bool einstein = Math.Abs(g11) > 1e-6 && Math.Abs(gii) > 1e-6;
        bool flat = ratio < 1.5;

        sb.AppendLine();
        sb.AppendLine($"branching-generated ρ = gravity-required ρ (all α): {allMatch}");
        sb.AppendLine($"ρ>0 (metric origin): {metricOrigin};  m>0 (deficit matter): {deficitMatter}");
        sb.AppendLine($"G non-trivial (Einstein): {einstein};  flat rotation (v²(3)/v²(9)={ratio:F2}): {flat}");
        Output.WriteLine(sb.ToString());

        Assert.True(allMatch, "branching density should equal the gravity density exactly");
        Assert.True(metricOrigin && deficitMatter, "ρ should reproduce metric origin + deficit matter");
        Assert.True(einstein && flat, "ρ should reproduce Einstein structure + flat rotation");
    }

    // ── ATQG12: criticality = unique scale-free point + classification ──────────────

    [Fact]
    public void ATQG12_CriticalityClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG12: criticality (μ=1) is the unique scale-free branching point");

        sb.AppendLine($"{"μ",7} {"L = 1/|ln μ|",16} {"scale-free?",14}");
        foreach (double mu in new[] { 0.7, 0.9, 1.0, 1.1, 1.5 })
        {
            double L = QEventBranching.BranchingScaleLength(mu);
            string sf = double.IsPositiveInfinity(L) ? "yes" : "no";
            sb.AppendLine($"{mu,7:F1} {L,16:F4} {sf,14}");
        }

        bool onlyCriticalScaleFree = double.IsPositiveInfinity(QEventBranching.BranchingScaleLength(1.0))
            && !double.IsPositiveInfinity(QEventBranching.BranchingScaleLength(0.9))
            && !double.IsPositiveInfinity(QEventBranching.BranchingScaleLength(1.1));

        sb.AppendLine();
        sb.AppendLine($"only μ=1 has infinite scale length (no preferred scale): {onlyCriticalScaleFree}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: FULL MATCH (conditional on scale-freeness = criticality).");
        sb.AppendLine("  • A sub/supercritical branching process has an intrinsic e-folding scale L = 1/|ln μ|; only the");
        sb.AppendLine("    CRITICAL process (μ=1) is scale-free (L → ∞).");
        sb.AppendLine("  • Scale-freeness (AT-F1: renormalization invariance) therefore selects μ=1, i.e. α=0, uniquely.");
        sb.AppendLine("  • The microscopic actualization rule — critical Q-event branching — generates EXACTLY the ρ the");
        sb.AppendLine("    gravity program requires (ATQG10/11).");
        sb.AppendLine("  • The single remaining input is scale-freeness itself (the criticality of the branching), which is");
        sb.AppendLine("    the renormalization-invariance requirement already reduced in AT-F1.");
        Output.WriteLine(sb.ToString());

        Assert.True(onlyCriticalScaleFree, "only critical branching should be scale-free");
    }
}
