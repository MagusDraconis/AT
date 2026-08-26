using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-A Phase 0 — audit the metric ansatz g = ρ^(2/d)η. Why exactly ρ^(2/d)? Tests five candidate
/// requirements (scale invariance, volume-element consistency, counting-measure preservation, conformal
/// covariance, uniqueness) to determine whether k = 2/d is UNIQUE, PREFERRED, or ASSUMED.
///
/// Tests: G4-A00 (volume-element/counting-measure uniquely selects k=2/d), G4-A01 (scale invariance +
///        conformal covariance are k-independent), G4-A02 (conformal flatness is an assumption + classification).
/// </summary>
public class G4A_Phase0_MetricAnsatzAuditTests : ResearchTestBase
{
    public G4A_Phase0_MetricAnsatzAuditTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;
    private const double X = 0.4;

    // ── G4-A00: volume-element / counting-measure consistency uniquely selects k=2/d ──

    [Fact]
    public void G4_A00_VolumeElementUniqueness()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-A00: volume-element / counting-measure consistency uniquely selects k = 2/d");

        // For g = ρ^k η, det g = ρ^(kd) det η, so √(−g) = ρ^(kd/2). Counting-measure preservation demands
        // √(−g) = ρ (the invariant volume element equals the counting measure), i.e. ρ^(kd/2) = ρ ⇒ k = 2/d.
        sb.AppendLine($"profile ρ = 1 + x², x = {X}, d = {D}, ρ = {MetricAnsatzAudit.Profile(X):F6}");
        sb.AppendLine($"{"k",8} {"√(−g)",12} {"ρ",12} {"|√(−g)−ρ|/ρ",14}");
        double errAt2d = 0;
        foreach (double k in new[] { 1.0 / D, 2.0 / D, 3.0 / D, 1.0, 4.0 / D })
        {
            double vol = MetricAnsatzAudit.VolumeElement(X, k, D);
            double err = MetricAnsatzAudit.VolumeError(X, k, D);
            if (Math.Abs(k - 2.0 / D) < 1e-9) errAt2d = err;
            sb.AppendLine($"{k,8:F4} {vol,12:F6} {MetricAnsatzAudit.Profile(X),12:F6} {err,14:E2}");
        }

        bool zeroAt2d = errAt2d < 1e-12;                                        // exactly zero at k=2/d
        bool nonzeroElsewhere = true;
        foreach (double k in new[] { 1.0 / D, 3.0 / D, 1.0, 4.0 / D })
            if (MetricAnsatzAudit.VolumeError(X, k, D) < 1e-3) nonzeroElsewhere = false;

        sb.AppendLine();
        sb.AppendLine($"√(−g) = ρ exactly at k = 2/d: {zeroAt2d}");
        sb.AppendLine($"√(−g) ≠ ρ for all other k: {nonzeroElsewhere}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the counting-measure / volume-element requirement √(−g) = ρ has the UNIQUE");
        sb.AppendLine("solution k = 2/d (kd/2 = 1). The exponent is uniquely derived, not assumed.");
        Output.WriteLine(sb.ToString());

        Assert.True(zeroAt2d, "k=2/d should give √(−g)=ρ exactly");
        Assert.True(nonzeroElsewhere, "all other k should violate the counting-measure requirement");
    }

    // ── G4-A01: scale invariance + conformal covariance are k-independent ────────────

    [Fact]
    public void G4_A01_ScaleInvarianceNonSelective()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-A01: scale invariance and conformal covariance do NOT select k");

        // Scale invariance: a = −(k/2)∇lnρ is invariant under ρ → cρ (∇ln(cρ) = ∇lnρ) for EVERY k.
        double dlnRho = MetricAnsatzAudit.LogDerivativeOf(u => MetricAnsatzAudit.Profile(u), X);
        double dln2Rho = MetricAnsatzAudit.LogDerivativeOf(u => 2.0 * MetricAnsatzAudit.Profile(u), X);
        bool scaleInvariant = Math.Abs(dlnRho - dln2Rho) < 1e-9;

        // The acceleration coefficient is −k/2: a(k)/k = −(1/2)∇lnρ is k-independent, so k is a free
        // magnitude NOT fixed by scale invariance.
        sb.AppendLine($"∇lnρ = {dlnRho:F6};  ∇ln(2ρ) = {dln2Rho:F6}  → scale-invariant: {scaleInvariant}");
        sb.AppendLine($"{"k",8} {"a=−(k/2)∇lnρ",16} {"a/k",12}");
        double aOverK = 0;
        foreach (double k in new[] { 1.0 / D, 2.0 / D, 3.0 / D, 1.0 })
        {
            double a = MetricAnsatzAudit.Acceleration(X, k);
            aOverK = a / k;
            sb.AppendLine($"{k,8:F4} {a,16:F6} {aOverK,12:F6}");
        }
        bool proportional = Math.Abs(aOverK - (-0.5 * dlnRho)) < 1e-9;   // a = −(k/2)∇lnρ

        sb.AppendLine();
        sb.AppendLine($"a ∝ k with coefficient −(1/2)∇lnρ (k-independent): {proportional}");
        sb.AppendLine($"scale invariance holds for EVERY k (only ∇lnρ, not k, is the invariant): {scaleInvariant}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: scale invariance and conformal covariance are satisfied by any k (they constrain");
        sb.AppendLine("only ∇lnρ, the conformal structure). They are CONSISTENT with k=2/d but do NOT select it.");
        Output.WriteLine(sb.ToString());

        Assert.True(scaleInvariant, "acceleration should be invariant under ρ → cρ");
        Assert.True(proportional, "a should equal −(k/2)∇lnρ (k enters only as a free magnitude)");
    }

    // ── G4-A02: conformal flatness is an assumption → classification ──────────────────

    [Fact]
    public void G4_A02_ConformalFlatnessAssumption()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-A02: conformal flatness is an assumption, not a derived consequence");

        // √(−g)=ρ fixes only the determinant. A ψ-perturbed metric with the SAME determinant gives a
        // DIFFERENT acceleration, so the conformally-flat form ρ^(2/d)η is an additional assumption.
        double aFlat = MetricAnsatzAudit.Acceleration(X, 2.0 / D);
        double aPert = MetricAnsatzAudit.PerturbedAcceleration(X, D);
        double volFlat = MetricAnsatzAudit.VolumeElement(X, 2.0 / D, D);
        double volPert = MetricAnsatzAudit.PerturbedVolumeElement(X, D);

        sb.AppendLine($"conformally-flat g = ρ^(2/d)η :  a = {aFlat:F6}, √(−g) = {volFlat:F6}");
        sb.AppendLine($"ψ-perturbed (non-flat) metric  :  a = {aPert:F6}, √(−g) = {volPert:F6}");
        sb.AppendLine($"  (g_00=−ρ^(2/d)e^{{2ψ}}, g_11=ρ^(2/d)e^{{−2ψ/(d−1)}}, ψ=b·x — det unchanged)");

        bool sameVolume = Math.Abs(volFlat - volPert) < 1e-12;
        bool differentAcceleration = Math.Abs(aPert / aFlat) > 2.0;

        sb.AppendLine();
        sb.AppendLine($"same √(−g) = ρ for both metrics: {sameVolume}");
        sb.AppendLine($"different acceleration (|a_pert/a_flat| = {Math.Abs(aPert / aFlat):F2} > 2): {differentAcceleration}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: PREFERRED (not UNIQUE, not merely ASSUMED).");
        sb.AppendLine("  • The EXPONENT k = 2/d is UNIQUE — uniquely selected by √(−g) = ρ (G4-A00).");
        sb.AppendLine("  • CONFORMAL FLATNESS (η) is ASSUMED — √(−g)=ρ fixes only det g, not the full metric;");
        sb.AppendLine("    a non-flat metric with the same √(−g)=ρ is physically distinct.");
        sb.AppendLine("  • It is PREFERRED because ρ is the only scalar available (minimality): no ψ field exists in");
        sb.AppendLine("    AT, so the metric built from ρ alone is the conformal factor times the vacuum η.");
        Output.WriteLine(sb.ToString());

        Assert.True(sameVolume, "ψ-perturbed metric should preserve √(−g)=ρ");
        Assert.True(differentAcceleration, "conformal flatness should be a physically distinct assumption");
    }
}
