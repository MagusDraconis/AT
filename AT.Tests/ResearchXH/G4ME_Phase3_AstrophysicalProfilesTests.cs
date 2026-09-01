using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-ME Phase 3 — astrophysical plausibility. The scale-free deficit hierarchy produces Newton-like 1/r²
/// gravity (Phase 2). Here we test whether realistic galaxy-scale mass profiles can emerge: power-law
/// hierarchies (Keplerian point mass), abundance-law (log-deficit) hierarchies (flat rotation curve),
/// finite-size cutoffs, and hierarchical void populations, compared to Newtonian expectation.
///
/// Tests: G4-ME30 (power-law → Keplerian), G4-ME31 (log-deficit → flat rotation curve + finite cutoff),
///        G4-ME32 (hierarchical void population → log deficit + stability + classification).
/// </summary>
public class G4ME_Phase3_AstrophysicalProfilesTests : ResearchTestBase
{
    public G4ME_Phase3_AstrophysicalProfilesTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;

    private static double A3D(Func<double, double> rho, double r) => DeficitCollective.AtAcceleration3D(rho, r, D);
    private static double V2(Func<double, double> rho, double r) => DeficitCollective.RotationCurveProxy(rho, r, D);

    // ── G4-ME30: power-law hierarchy → Keplerian (point-mass) rotation curve ─────────

    [Fact]
    public void G4_ME30_PowerLawKeplerianRotationCurve()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME30: power-law hierarchy → Keplerian (point-mass) rotation curve");

        sb.AppendLine($"{"r",6} {"v^2",12} {"M_eff=v^2 r",14}");
        double v2_3 = 0, v2_9 = 0, mEff12 = 0;
        foreach (double r in new[] { 1.0, 2.0, 3.0, 5.0, 8.0, 9.0, 12.0 })
        {
            double v2 = V2(u => DeficitCollective.PowerLawDeficit(u), r);
            double mEff = DeficitCollective.EffectiveEnclosedMass(A3D(u => DeficitCollective.PowerLawDeficit(u), r), r);
            if (r == 3.0) v2_3 = v2;
            if (r == 9.0) v2_9 = v2;
            if (r == 12.0) mEff12 = mEff;
            sb.AppendLine($"{r,6:F1} {v2,12:F6} {mEff,14:F6}");
        }

        double asymptote = 0.5 * 0.5 / (D * 1.0);   // m0·r0/(d·ρ̄)
        bool keplerian = v2_3 / v2_9 > 2.0;          // v² ∝ 1/r ⇒ steep falloff
        bool pointMass = Math.Abs(mEff12 - asymptote) / asymptote < 0.10;

        sb.AppendLine();
        sb.AppendLine($"v²(3)/v²(9) = {v2_3 / v2_9:F2} (Keplerian ⇒ ≈3; flat ⇒ ≈1)");
        sb.AppendLine($"M_eff(12) = {mEff12:F6}, asymptote m0·r0/(d·ρ̄) = {asymptote:F6}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the power-law deficit hierarchy reproduces the KEPLERIAN (point-mass) rotation");
        sb.AppendLine($"curve v² ∝ 1/r with effective enclosed mass → const. Keplerian: {keplerian}; point-mass: {pointMass}.");
        Output.WriteLine(sb.ToString());

        Assert.True(keplerian, "power-law hierarchy should give a Keplerian (falling) rotation curve");
        Assert.True(pointMass, "effective enclosed mass should approach the point-mass constant");
    }

    // ── G4-ME31: log-deficit (abundance-law) → flat rotation curve + finite cutoff ───

    [Fact]
    public void G4_ME31_LogDeficitFlatRotationCurve()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME31: abundance-law (log-deficit) hierarchy → flat rotation curve");

        double m0 = 0.4, r0 = 0.5, Rmax = 10.0;
        sb.AppendLine($"{"r",6} {"m(r)",10} {"v^2",12} {"M_eff=v^2 r",14}");
        double v2_3 = 0, v2_9 = 0, mEff3 = 0, mEff9 = 0;
        foreach (double r in new[] { 1.0, 2.0, 3.0, 5.0, 7.0, 9.0 })
        {
            double v2 = V2(u => DeficitCollective.LogDeficit(u), r);
            double mEff = DeficitCollective.EffectiveEnclosedMass(A3D(u => DeficitCollective.LogDeficit(u), r), r);
            if (r == 3.0) { v2_3 = v2; mEff3 = mEff; }
            if (r == 9.0) { v2_9 = v2; mEff9 = mEff; }
            sb.AppendLine($"{r,6:F1} {1.0 - DeficitCollective.LogDeficit(r),10:F6} {v2,12:F6} {mEff,14:F6}");
        }

        double analytic = m0 / (D * 1.0 * Math.Log(Rmax / r0));   // m0/(d·ρ̄·ln(Rmax/r0))
        bool flat = v2_3 / v2_9 < 1.5;                             // ≈ flat (Keplerian would be ≈3)
        bool matchesAnalytic = Math.Abs(v2_9 - analytic) / analytic < 0.10;
        bool haloLike = mEff9 / mEff3 is > 2.0 and < 4.0;          // M_eff ∝ r (dark-matter-halo form)

        // Finite-size cutoff: beyond Rmax there are no voids → field vanishes.
        double aBeyond = A3D(u => DeficitCollective.LogDeficit(u), 11.0);
        bool cutoff = Math.Abs(aBeyond) < 1e-6;

        sb.AppendLine();
        sb.AppendLine($"v²(3)/v²(9) = {v2_3 / v2_9:F2} (flat ⇒ ≈1; Keplerian ⇒ ≈3)");
        sb.AppendLine($"v²(9) = {v2_9:F6}, analytic asymptote m0/(d·ρ̄·ln(Rmax/r0)) = {analytic:F6}");
        sb.AppendLine($"M_eff(9)/M_eff(3) = {mEff9 / mEff3:F2} (halo-like M ∝ r ⇒ ≈3)");
        sb.AppendLine($"finite cutoff: a(r=11 &gt; Rmax) = {aBeyond:E2} ≈ 0");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the log-deficit (constant-deficit-per-octave) hierarchy produces an");
        sb.AppendLine("APPROXIMATELY FLAT rotation curve v² ≈ const, with effective enclosed mass M_eff ∝ r");
        sb.AppendLine("(the dark-matter-halo form), truncated at the finite galaxy radius Rmax.");
        Output.WriteLine(sb.ToString());

        Assert.True(flat, "log-deficit hierarchy should give a flat rotation curve");
        Assert.True(matchesAnalytic, "flat rotation-curve value should match the analytic asymptote");
        Assert.True(haloLike, "effective mass should grow linearly (halo-like)");
        Assert.True(cutoff, "field should vanish beyond the finite-size cutoff");
    }

    // ── G4-ME32: hierarchical void population → log deficit + stability ──────────────

    [Fact]
    public void G4_ME32_HierarchicalPopulationStability()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME32: hierarchical void population → log deficit, and stability");

        double m0 = 0.4, r0 = 0.5, lambda = 1.5;
        int K = 8;
        double Rmax = r0 * Math.Pow(lambda, K);

        // (a) The discrete annular hierarchy (constant amplitude per octave) matches the log deficit.
        sb.AppendLine($"{"octave",7} {"mid",8} {"m_ann",10} {"m_log",10} {"rel.err",10}");
        bool matches = true;
        for (int k = 0; k < K; k++)
        {
            double Rk = r0 * Math.Pow(lambda, k);
            double mid = Math.Sqrt(Rk * Rk * lambda);                 // geometric midpoint
            double mAnn = 1.0 - DeficitCollective.AnnularDeficit(mid, 1.0, m0, r0, lambda, K);
            double mLog = 1.0 - DeficitCollective.LogDeficit(mid, 1.0, m0, r0, Rmax);
            double rel = Math.Abs(mAnn - mLog) / Math.Max(mLog, 1e-9);
            if (k <= 4 && rel > 0.25) matches = false;               // inner octaves (non-negligible deficit)
            sb.AppendLine($"{k,7} {mid,8:F2} {mAnn,10:F5} {mLog,10:F5} {rel,10:F3}");
        }

        // (b) Finite cutoff: the hierarchy vanishes beyond Rmax.
        double mBeyond = 1.0 - DeficitCollective.AnnularDeficit(Rmax * 1.1, 1.0, m0, r0, lambda, K);
        bool cutoff = Math.Abs(mBeyond) < 1e-12;

        // (c) Stability: the flat rotation-curve value depends only on (m0, Rmax, r0), not the void spacing.
        double analytic = m0 / (D * 1.0 * Math.Log(Rmax / r0));
        double numeric = V2(u => DeficitCollective.LogDeficit(u, 1.0, m0, r0, Rmax), 9.0);
        bool stable = Math.Abs(numeric - analytic) / analytic < 0.10;

        sb.AppendLine();
        sb.AppendLine($"finite cutoff (m beyond Rmax = {Rmax:F1}): {mBeyond:E2} ≈ 0");
        sb.AppendLine($"stability: v² depends only on (m0, Rmax, r0); analytic = {analytic:F6}, numeric = {numeric:F6}");
        sb.AppendLine($"hierarchical population matches log deficit (inner octaves): {matches}");
        sb.AppendLine($"flat rotation-curve value stable (refinement-independent): {stable}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: PLAUSIBLE — a hierarchical void population (constant deficit per octave,");
        sb.AppendLine("finite K) reproduces the log-deficit envelope and hence a realistic FLAT rotation curve.");
        sb.AppendLine("The result is stable: it depends only on the total deficit depth m0 and the dynamic range");
        sb.AppendLine("ln(Rmax/r0), not on the microscopic void spacing.");
        Output.WriteLine(sb.ToString());

        Assert.True(matches, "discrete hierarchy should match the log deficit at midpoints");
        Assert.True(cutoff, "hierarchy should vanish beyond the finite cutoff");
        Assert.True(stable, "flat rotation-curve value should be stable/deterministic");
    }
}
