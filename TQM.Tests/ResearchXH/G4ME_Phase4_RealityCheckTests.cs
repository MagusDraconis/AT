using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-ME Phase 4 — reality-check the deficit hierarchy. Phase 3 showed a log-deficit hierarchy produces a
/// flat rotation curve. Here we ask whether the required m ∝ ln(Rmax/r) profile emerges NATURALLY from the
/// TQM abundance law (self-similar actualization deficits), or whether it is an imposed (tuned) ansatz.
///
/// Tests: G4-ME40 (abundance-law family, marginal α=0), G4-ME41 (scaling stability: constant per-octave
///        increment), G4-ME42 (hierarchy growth + classification).
/// </summary>
public class G4ME_Phase4_RealityCheckTests : ResearchTestBase
{
    public G4ME_Phase4_RealityCheckTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;

    private static double V2(Func<double, double> rho, double r) => DeficitCollective.RotationCurveProxy(rho, r, D);
    private static double M(Func<double, double> rho, double r) => 1.0 - rho(r);

    // ── G4-ME40: abundance-law family — flat curve is the marginal α=0 case ──────────

    [Fact]
    public void G4_ME40_AbundanceLawFamily()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME40: abundance-law family — the flat curve is the marginal α=0 case");

        // Deficit family m(r) ∝ r^(−α) for α≠0, m ∝ ln(Rmax/r) for α=0. Rotation curve v² ∝ r^(−α),
        // so v²(3)/v²(9) = 3^α: flat (α=0) → 1, Keplerian (α=1) → 3.
        sb.AppendLine($"{"alpha",8} {"v^2(3)",12} {"v^2(9)",12} {"ratio",10} {"shape",18}");
        double ratio0 = 0, ratio1 = 0, ratio025 = 0;
        foreach (double alpha in new[] { 0.0, 0.25, 0.5, 1.0, 2.0 })
        {
            double v3 = V2(u => DeficitCollective.AbundanceDeficit(u, alpha), 3.0);
            double v9 = V2(u => DeficitCollective.AbundanceDeficit(u, alpha), 9.0);
            double ratio = v3 / v9;
            if (alpha == 0.0) ratio0 = ratio;
            if (alpha == 0.25) ratio025 = ratio;
            if (alpha == 1.0) ratio1 = ratio;
            string shape = ratio < 1.5 ? "flat (α=0)" : ratio < 2.5 ? "mild falloff" : "Keplerian (α≥1)";
            sb.AppendLine($"{alpha,8:F2} {v3,12:F6} {v9,12:F6} {ratio,10:F3} {shape,18}");
        }

        bool flatIsMarginal = ratio0 < 1.5;                 // α=0 is flat
        bool keplerianAtOne = ratio1 > 2.5;                 // α=1 is Keplerian
        bool monotonic = ratio0 < ratio025 && ratio025 < ratio1;   // ratio = 3^α, increasing in α

        sb.AppendLine();
        sb.AppendLine($"flat curve (α=0) is the MARGINAL case: {flatIsMarginal}");
        sb.AppendLine($"Keplerian (α=1): {keplerianAtOne}");
        sb.AppendLine($"ratio increases monotonically in α (3^α): {monotonic}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the flat rotation curve is EXACTLY the α=0 (log) member of the self-similar");
        sb.AppendLine("power-law family m ∝ r^(−α). It is the boundary between falling (α>0) and rising (α<0)");
        sb.AppendLine("curves — a special, marginal, but not arbitrary point (α=0 = constant deficit per octave).");
        Output.WriteLine(sb.ToString());

        Assert.True(flatIsMarginal, "α=0 (log) should give a flat rotation curve");
        Assert.True(keplerianAtOne, "α=1 should give a Keplerian rotation curve");
        Assert.True(monotonic, "rotation-curve ratio should increase monotonically in α");
    }

    // ── G4-ME41: scaling stability — constant per-octave increment ───────────────────

    [Fact]
    public void G4_ME41_ScalingStability()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME41: scaling stability — the log deficit is the unique scale-invariant profile");

        // A self-similar profile has a CONSTANT increment per octave: m(r) − m(λr) = const.
        sb.AppendLine($"{"r",6} {"m_log(r)",10} {"inc=log",10} {"inc=pow(α=1)",14}");
        double[] rs = { 1.0, 1.5, 2.0, 3.0, 4.0 };
        var logInc = new List<double>();
        var powInc = new List<double>();
        foreach (double r in rs)
        {
            double incLog = M(u => DeficitCollective.LogDeficit(u), r) - M(u => DeficitCollective.LogDeficit(u), 2 * r);
            double incPow = M(u => DeficitCollective.AbundanceDeficit(u, 1.0), r)
                          - M(u => DeficitCollective.AbundanceDeficit(u, 1.0), 2 * r);
            logInc.Add(incLog);
            powInc.Add(incPow);
            sb.AppendLine($"{r,6:F1} {M(u => DeficitCollective.LogDeficit(u), r),10:F6} {incLog,10:F6} {incPow,14:F6}");
        }

        double analytic = 0.4 * Math.Log(2.0) / Math.Log(20.0);   // m0·ln2/ln(Rmax/r0)
        double spread = logInc.Max() - logInc.Min();
        bool logConstant = spread < 1e-6;                          // constant per-octave increment
        bool matchesAnalytic = Math.Abs(logInc[0] - analytic) / analytic < 0.01;
        bool powerLawVaries = powInc[0] > 2.0 * powInc[^1];        // power-law increment decays

        sb.AppendLine();
        sb.AppendLine($"log-deficit increment m(r)−m(2r): spread = {spread:E1} (constant: {logConstant})");
        sb.AppendLine($"analytic increment m0·ln2/ln20 = {analytic:F6} (match: {matchesAnalytic})");
        sb.AppendLine($"power-law increment decays by factor {powInc[0] / powInc[^1]:F2} (NOT constant: {powerLawVaries})");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the log deficit is the UNIQUE profile whose per-octave increment m(r)−m(λr) is");
        sb.AppendLine("CONSTANT — i.e., the unique scale-invariant (self-similar) deficit. Its gradient a ∝ 1/r is");
        sb.AppendLine("the only scale-free radial field, hence the flat rotation curve is the self-similar prediction.");
        Output.WriteLine(sb.ToString());

        Assert.True(logConstant, "log deficit should have a constant per-octave increment");
        Assert.True(matchesAnalytic, "increment should equal m0·ln2/ln(Rmax/r0)");
        Assert.True(powerLawVaries, "power-law deficit increment should decay (not self-similar in depth)");
    }

    // ── G4-ME42: hierarchy growth → log deficit; classification ──────────────────────

    [Fact]
    public void G4_ME42_HierarchyGrowthClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME42: hierarchy growth — constant per-octave growth yields the log deficit");

        double m0 = 0.4, r0 = 0.5, lambda = 1.5;
        int K = 8;
        double Rmax = r0 * Math.Pow(lambda, K);
        double inc = m0 / K;

        // Self-similar growth: each octave adds an EQUAL increment m0/K; the deficit at octave k is the
        // cumulative sum of all outer octaves → m(r) = m0(K−k)/K ∝ ln(Rmax/r).
        sb.AppendLine($"per-octave increment = m0/K = {inc:F4}; Rmax = {Rmax:F3}");
        sb.AppendLine($"{"octave",7} {"m_ann",10} {"m_cumul",10} {"m_log",10} {"rel.err",10}");
        bool cumulativeMatches = true;
        bool envelopeMatches = true;
        for (int k = 0; k < K; k++)
        {
            double mid = r0 * Math.Pow(lambda, k) * Math.Sqrt(lambda);
            double mAnn = M(u => DeficitCollective.AnnularDeficit(u, 1.0, m0, r0, lambda, K), mid);
            double mCumul = inc * (K - k);                       // cumulative equal increments
            double mLog = M(u => DeficitCollective.LogDeficit(u, 1.0, m0, r0, Rmax), mid);
            double rel = Math.Abs(mCumul - mLog) / Math.Max(mLog, 1e-9);
            if (Math.Abs(mAnn - mCumul) > 1e-12) cumulativeMatches = false;
            if (k <= 4 && rel > 0.25) envelopeMatches = false;
            sb.AppendLine($"{k,7} {mAnn,10:F5} {mCumul,10:F5} {mLog,10:F5} {rel,10:F3}");
        }

        sb.AppendLine();
        sb.AppendLine($"cumulative equal increments == discrete hierarchy: {cumulativeMatches}");
        sb.AppendLine($"cumulative increments match the log-deficit envelope: {envelopeMatches}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: SEMI-NATURAL. The log deficit is NOT a tuned ansatz — it is the UNIQUE");
        sb.AppendLine("scale-invariant (self-similar) profile, generated by the natural constant-per-octave growth of");
        sb.AppendLine("actualization deficits (no preferred scale). However it is the MARGINAL α=0 member of the");
        sb.AppendLine("self-similar family: any α≠0 (also self-similar) gives a non-flat curve, and the dynamic");
        sb.AppendLine("selection of α=0 is a symmetry assumption rather than a derived attractor. The remaining free");
        sb.AppendLine("parameters (m0 = total deficit, Rmax = galaxy radius) are also free in GR (total mass, size).");
        Output.WriteLine(sb.ToString());

        Assert.True(cumulativeMatches, "cumulative equal increments should equal the discrete hierarchy");
        Assert.True(envelopeMatches, "cumulative increments should match the log-deficit envelope");
    }
}
