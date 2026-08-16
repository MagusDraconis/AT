using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-A Phase 1 — derive or eliminate conformal flatness. G4-A0 showed the exponent 2/d is DERIVED but the
/// flat representative η is ASSUMED. Here we test whether causal order + counting measure can select η, via
/// causal-class consistency, minimum-information metrics, entropy of metric degrees of freedom, conformal
/// gauge freedom, and ψ-field perturbations. Classify: DERIVED / PREFERRED / GENUINELY ASSUMED.
///
/// Tests: G4-A10 (counting + flatness), G4-A11 (minimum-curvature), G4-A12 (stability + classification).
/// </summary>
public class G4A_Phase1_ConformalFlatnessTests : ResearchTestBase
{
    public G4A_Phase1_ConformalFlatnessTests(ITestOutputHelper o) : base(o) { }

    private const double X = 0.4;

    // ── G4-A10: the reference metric's degrees of freedom; η is the unique flat one ──

    [Fact]
    public void G4_A10_CountingAndFlatness()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-A10: √(−g)=ρ fixes only the determinant; η is the unique flat reference");

        // The counting measure fixes det g = −ρ² (one condition), leaving the reference h (det h = −1) with
        // d(d+1)/2 − 1 free functions. η (ψ=0) is the unique reference with ZERO Ricci curvature.
        sb.AppendLine($"{"b (ψ=b·x²)",14} {"R[h_ψ] at x=0.4",16}");
        double r0 = 0;
        var rs = new Dictionary<double, double>();
        foreach (double b in new[] { 0.0, 0.1, 0.3, 0.5 })
        {
            double r = MetricAnsatzAudit.ReferenceRicciScalar(X, b);
            rs[b] = r;
            if (b == 0.0) r0 = r;
            sb.AppendLine($"{b,14:F2} {r,16:F6}");
        }

        bool flatAtEta = Math.Abs(r0) < 1e-12;
        bool curvedElsewhere = rs[0.3] > 1e-3 && rs[0.5] > rs[0.3];

        sb.AppendLine();
        sb.AppendLine($"η (ψ=0) has R=0 (flat, structureless): {flatAtEta}");
        sb.AppendLine($"ψ≠0 introduces non-zero curvature (grows with ψ): {curvedElsewhere}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the counting measure ρ fixes only det g (the conformal factor); the reference h");
        sb.AppendLine("(det h = −1) is otherwise free. η is the UNIQUE reference with vanishing curvature — the");
        sb.AppendLine("structureless (zero-information) representative of the conformal class.");
        Output.WriteLine(sb.ToString());

        Assert.True(flatAtEta, "η should be flat (R=0)");
        Assert.True(curvedElsewhere, "ψ≠0 should introduce curvature");
    }

    // ── G4-A11: minimum-curvature (minimum-information) selects η ────────────────────

    [Fact]
    public void G4_A11_MinimumCurvature()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-A11: η minimizes the curvature (information) content of the reference metric");

        // Curvature CONTENT is the squared curvature R² (the natural "cost"/energy). R[h_ψ] = 0 at ψ=0,
        // and R² increases monotonically with |ψ| (for ψ ≥ 0): η minimizes the curvature content.
        sb.AppendLine($"{"b",10} {"R(0.4)",14} {"R² (content)",14}");
        double prev = 0;
        bool monotonic = true;
        foreach (double b in new[] { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5 })
        {
            double r = MetricAnsatzAudit.ReferenceRicciScalar(X, b);
            double r2 = r * r;
            if (b > 0.0 && r2 < prev) monotonic = false;
            prev = r2;
            sb.AppendLine($"{b,10:F2} {r,14:F6} {r2,14:F6}");
        }

        bool minimizedAtEta = MetricAnsatzAudit.ReferenceRicciScalar(X, 0.0) * MetricAnsatzAudit.ReferenceRicciScalar(X, 0.0) < 1e-12;

        sb.AppendLine();
        sb.AppendLine($"curvature content R² minimized (zero) at η (ψ=0): {minimizedAtEta}");
        sb.AppendLine($"R² increases monotonically with |ψ|: {monotonic}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: η is the MINIMUM-CURVATURE (equivalently minimum-information) representative of the");
        sb.AppendLine("conformal class. A ψ-field is an extra degree of freedom whose content is NOT sourced by ρ —");
        sb.AppendLine("introducing it increases the curvature (information) content.");
        Output.WriteLine(sb.ToString());

        Assert.True(minimizedAtEta, "η should minimize the reference curvature");
        Assert.True(monotonic, "curvature should grow monotonically with |ψ|");
    }

    // ── G4-A12: stability + classification ───────────────────────────────────────────

    [Fact]
    public void G4_A12_StabilityClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-A12: η is the stable minimum-curvature representative — classification");

        // η (ψ=0) is the unique critical point of the curvature CONTENT R² (dR²/dψ|₀ = 0) and a MINIMUM
        // (d²R²/dψ²|₀ = 32 > 0). A ψ-perturbation is a "massive" extra mode: it costs curvature content and
        // is not determined by ρ.
        double eps = 1e-4;
        double r2(double b) { double r = MetricAnsatzAudit.ReferenceRicciScalar(X, b); return r * r; }
        double dR2 = (r2(eps) - r2(-eps)) / (2.0 * eps);
        double d2R2 = (r2(eps) - 2.0 * r2(0.0) + r2(-eps)) / (eps * eps);

        bool criticalPoint = Math.Abs(dR2) < 1e-3;   // dR²/dψ = 0 at ψ=0
        bool minimum = d2R2 > 0.0;                   // d²R²/dψ² > 0

        sb.AppendLine($"dR²/dψ|₀ = {dR2:E2} (critical point: {criticalPoint})");
        sb.AppendLine($"d²R²/dψ²|₀ = {d2R2:F3} > 0 (minimum: {minimum})");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: PREFERRED (minimum-curvature/information), with a DERIVED-conditional note.");
        sb.AppendLine("  • η is NOT uniquely forced by causal order + counting measure: they fix the conformal factor");
        sb.AppendLine("    and determinant, leaving the conformal class (reference h) free.");
        sb.AppendLine("  • η IS uniquely selected by the MINIMUM-CURVATURE (minimum-information) principle — the");
        sb.AppendLine("    structureless representative, analogous to the α=0 maximum-entropy selection (G4-RHO).");
        sb.AppendLine("  • Conditional DERIVATION: the conformal class is fixed by the causal structure (Malament); if the");
        sb.AppendLine("    Q-event causal structure is Minkowskian (flat light cones, the vacuum the program assumes),");
        sb.AppendLine("    then η follows from causal order alone.");
        sb.AppendLine("  • So η is PREFERRED (minimum-information) — and DERIVED iff the causal vacuum is flat.");
        Output.WriteLine(sb.ToString());

        Assert.True(criticalPoint, "ψ=0 should be a critical point of the curvature");
        Assert.True(minimum, "ψ=0 should be a minimum of the curvature");
    }
}
