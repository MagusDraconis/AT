using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-ME Phase 2 — search for the origin of long-range gravity. The single deficit gives the correct
/// attractive sign but the WRONG range (localized ∝ ∇m). Here we test whether collective deficit
/// structures (networks, multi-scale/nested voids, abundance-law distributions) can produce an
/// approximate 1/r² field. All quantities derived from ρ only.
///
/// Tests: G4-ME20 (deficit network baseline — still localized), G4-ME21 (nested self-similar structure
///        → 1/r² emergence), G4-ME22 (abundance-law continuum limit → exact 1/r² + classification).
/// </summary>
public class G4ME_Phase2_LongRangeGravityTests : ResearchTestBase
{
    public G4ME_Phase2_LongRangeGravityTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;

    private static double A3D(Func<double, double> rho, double r) => DeficitCollective.AtAcceleration3D(rho, r, D);

    // ── G4-ME20: deficit network baseline — still localized, no 1/r² ─────────────────

    [Fact]
    public void G4_ME20_DeficitNetworkBaseline()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME20: deficit network — is a collection of localized voids long-range?");

        // (a) Single Gaussian void: field ∝ ∇m = (2r/σ²)A e^(−r²/σ²) — vanishes exponentially.
        sb.AppendLine($"{"r",6} {"a_Gauss",10} {"1/r^2",8} {"a/(1/r^2)",12}");
        double aNear = 0, aFar = 0;
        foreach (double r in new[] { 0.5, 1.0, 1.5, 2.0, 3.0 })
        {
            double a = A3D(u => DeficitCollective.GaussianVoid(u), r);
            if (r == 0.5) aNear = a;
            if (r == 2.0) aFar = a;
            sb.AppendLine($"{r,6:F1} {a,10:F6} {1.0 / (r * r),8:F4} {a / (1.0 / (r * r)),12:F4}");
        }

        // (b) Network of three concentric void shells at 0.5, 1.0, 1.5 — superposition stays localized.
        double aNet = A3D(u => 1.0
            - 0.4 * Math.Exp(-Math.Pow(u - 0.5, 2) / 0.09)
            - 0.4 * Math.Exp(-Math.Pow(u - 1.0, 2) / 0.09)
            - 0.4 * Math.Exp(-Math.Pow(u - 1.5, 2) / 0.09), 4.0);
        sb.AppendLine();
        sb.AppendLine($"network of 3 void shells: a(4.0) = {aNet:E2} (1/r² at r=4 = {1.0 / 16.0:F4})");
        sb.AppendLine($"compact void: a(1.5) = {A3D(u => DeficitCollective.CompactVoid(u), 1.5):E2} (no exterior field)");

        bool gaussLocalized = Math.Abs(aFar) < 1e-4;                     // field has vanished by r=2
        bool networkLocalized = Math.Abs(aNet) < 1e-3;                   // sum of localized = localized
        bool attractive = aNear < 0;

        sb.AppendLine();
        sb.AppendLine($"single Gaussian void attractive (a<0) at small r: {attractive}");
        sb.AppendLine($"Gaussian field localized (vanishes by r=2): {gaussLocalized}");
        sb.AppendLine($"network superposition localized (no 1/r²): {networkLocalized}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a NETWORK of localized deficits is still LOCALIZED — superposition of");
        sb.AppendLine("exponentially-decaying fields cannot produce a 1/r² tail. Long-range gravity is NOT");
        sb.AppendLine("achieved by simply having many voids; a specific SCALE-FREE arrangement is required.");
        Output.WriteLine(sb.ToString());

        Assert.True(attractive, "Gaussian void should be attractive at small r");
        Assert.True(gaussLocalized, "Gaussian field should vanish (localized)");
        Assert.True(networkLocalized, "network superposition should remain localized");
    }

    // ── G4-ME21: nested self-similar structure → 1/r² emergence ──────────────────────

    [Fact]
    public void G4_ME21_NestedSelfSimilarEmergence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME21: nested self-similar void hierarchy → 1/r² field");

        // Geometric radii R_k = r0·λ^k, amplitudes A_k = A0·λ^(−k), self-similar widths σ_k = σ0·λ^k.
        // One void per logarithmic octave ⇒ cumulative deficit m(r) ∝ 1/r ⇒ a ∝ 1/r².
        double[] rr = { 1.0, 1.5, 2.0, 3.0, 4.5, 6.5, 9.0, 12.0, 16.0, 22.0 };
        var m = new List<double>();
        var amag = new List<double>();
        var aSign = new List<double>();
        sb.AppendLine($"{"r",6} {"m(r)",10} {"a(r)",12} {"M_eff=-a r^2",14}");
        bool allAttractive = true;
        foreach (double r in rr)
        {
            double rho = DeficitCollective.NestedVoidField(r);
            double mm = 1.0 - rho;
            double a = A3D(u => DeficitCollective.NestedVoidField(u), r);
            m.Add(mm);
            amag.Add(Math.Abs(a));
            aSign.Add(a);
            if (!(a < 0)) allAttractive = false;
            sb.AppendLine($"{r,6:F1} {mm,10:F6} {a,12:F6} {DeficitCollective.EffectiveEnclosedMass(a, r),14:F6}");
        }

        var (mSlope, _) = DeficitCollective.LogLogFit(rr, m.ToArray());
        var (aSlope, _) = DeficitCollective.LogLogFit(rr, amag.ToArray());

        sb.AppendLine();
        sb.AppendLine($"m(r)  power-law slope (log-log) = {mSlope:F3}   (target −1: 1/r deficit)");
        sb.AppendLine($"|a(r)| power-law slope (log-log) = {aSlope:F3}   (target −2: 1/r² field)");
        sb.AppendLine($"attractive (a<0) everywhere: {allAttractive}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a SELF-SIMILAR (geometric) hierarchy of voids — one per octave, amplitudes");
        sb.AppendLine("falling as 1/R — makes the cumulative deficit m ∝ 1/r, whose gradient is the 1/r² field.");
        sb.AppendLine("This is the mechanism by which long-range (Newtonian-form) attraction EMERGES natively.");
        Output.WriteLine(sb.ToString());

        Assert.True(allAttractive, "nested field should be attractive everywhere");
        Assert.InRange(mSlope, -1.5, -0.6);
        Assert.InRange(aSlope, -2.4, -1.6);
    }

    // ── G4-ME22: abundance-law continuum limit → exact 1/r² + classification ──────────

    [Fact]
    public void G4_ME22_AbundanceLawContinuumLimit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME22: abundance-law distribution — exact 1/r² and effective enclosed mass");

        // Continuum limit of the scale-free abundance law n(R) ∝ 1/R (equal deficit per octave) is the
        // smooth power-law deficit ρ = ρ̄ − m0/(1 + r/r0). Its AT field is a = −(1/d) m′/ρ ∝ −1/r².
        double m0 = 0.5, r0 = 0.5;
        double asymptote = m0 * r0 / (D * 1.0);   // M_eff → m0·r0/(d·ρ̄) as r → ∞

        sb.AppendLine($"{"r",6} {"a_AT",12} {"M_eff=-a r^2",14} {"a_Newton(M_eff)",16}");
        var rs = new[] { 1.0, 2.0, 3.0, 5.0, 8.0, 12.0 };
        foreach (double r in rs)
        {
            double a = A3D(u => DeficitCollective.PowerLawDeficit(u), r);
            double mEff = DeficitCollective.EffectiveEnclosedMass(a, r);
            double aNewt = DeficitCollective.NewtonianPointMass(mEff, r);
            sb.AppendLine($"{r,6:F1} {a,12:F6} {mEff,14:F6} {aNewt,16:F6}");
        }

        double aLast = A3D(u => DeficitCollective.PowerLawDeficit(u), 12.0);
        double mEffLast = DeficitCollective.EffectiveEnclosedMass(aLast, 12.0);
        double relErr = Math.Abs(mEffLast - asymptote) / asymptote;

        sb.AppendLine();
        sb.AppendLine($"asymptotic effective enclosed mass M_eff → m0·r0/(d·ρ̄) = {asymptote:F6}");
        sb.AppendLine($"M_eff(12) = {mEffLast:F6}  (relative deviation {relErr:P1})");
        sb.AppendLine($"a_AT(12) = {aLast:F6}, Newtonian point-mass M_eff/r² = {DeficitCollective.NewtonianPointMass(mEffLast, 12.0):F6}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: MECHANISM IDENTIFIED. The abundance-law (scale-free, n(R) ∝ 1/R) deficit");
        sb.AppendLine("hierarchy generates a deficit m ∝ 1/r — the conformal tail of a point-like source ρ ≈ 1 − d·M/r —");
        sb.AppendLine("whose gradient is EXACTLY the Newtonian 1/r² field. The effective enclosed mass asymptotes to a");
        sb.AppendLine("constant (point-mass form). Long-range gravity therefore emerges from the CONFORMAL 1/r tail of a");
        sb.AppendLine("scale-free deficit hierarchy, not from a single localized void.");
        Output.WriteLine(sb.ToString());

        Assert.True(aLast < 0, "power-law deficit field should be attractive");
        Assert.True(relErr < 0.10, "M_eff should approach its asymptotic constant (point-mass form)");
        Assert.True(mEffLast > 0, "effective enclosed mass should be positive");
    }
}
