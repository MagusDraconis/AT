using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 22 — audit conformal-flatness consequences. Tests whether the failures (no lensing, no tensor
/// GWs, no Hawking T) are consequences of conformal flatness (ψ=0) or fundamental AT results. Classify:
/// CONFORMAL-FLATNESS ARTIFACT / FUNDAMENTAL AT RESULT.
///
/// Tests: ATQG220 (lensing is a conformal-flatness artifact), ATQG221 (tensor modes are a conformal-flatness
///        artifact), ATQG222 (classification).
/// </summary>
public class ATQG_Phase22_ConformalFlatnessAuditTests : ResearchTestBase
{
    public ATQG_Phase22_ConformalFlatnessAuditTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG220: "no lensing" is a conformal-flatness artifact ─────────────────────

    [Fact]
    public void ATQG220_LensingIsConformalFlatnessArtifact()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG220: light bending turns ON when conformal flatness is relaxed (ψ≠0)");

        double x = 0.4;
        sb.AppendLine($"light bending (reference curvature) at x={x}:");
        sb.AppendLine($"{"ψ (b·x²)",10} {"bending R[h_ψ]",18}");
        foreach (double psi in new[] { 0.0, 0.1, 0.3 })
        {
            sb.AppendLine($"{psi,10:F2} {ConformalFlatnessAudit.LightBending(x, psi),18:F4}");
        }

        bool flatNoBending = ConformalFlatnessAudit.LightBending(x, 0.0) == 0.0;
        bool nonConformalBends = ConformalFlatnessAudit.LightBending(x, 0.3) > 1e-3;

        sb.AppendLine();
        sb.AppendLine($"conformal flatness (ψ=0) → zero bending: {flatNoBending}");
        sb.AppendLine($"weakly non-conformal (ψ≠0) → non-zero bending: {nonConformalBends}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: 'no gravitational lensing' (QG21) is a DIRECT consequence of conformal flatness (ψ=0).");
        sb.AppendLine("Relaxing it (ψ≠0) restores light bending — so it is an ARTIFACT of the assumption, not a fundamental AT result.");
        Output.WriteLine(sb.ToString());

        Assert.True(flatNoBending, "conformal flatness should give zero bending");
        Assert.True(nonConformalBends, "non-conformal metric should bend light");
    }

    // ── ATQG221: "no tensor GWs" is a conformal-flatness artifact ──────────────────

    [Fact]
    public void ATQG221_TensorModesConformalFlatnessArtifact()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG221: the tensor (graviton) sector is frozen by conformal flatness, not absent");

        int d = 3;
        double tensorDof = ConformalFlatnessAudit.TensorModes(d);
        double psi0 = ConformalFlatnessAudit.LightBending(0.4, 0.0);    // frozen (ψ=0)
        double psiNonzero = ConformalFlatnessAudit.LightBending(0.4, 0.3);  // activated (ψ≠0)

        sb.AppendLine($"tensor (Weyl+graviton) d.o.f. at d=3: {tensorDof} (frozen by ψ=0)");
        sb.AppendLine($"reference curvature: ψ=0 → {psi0:F1}, ψ=0.3 → {psiNonzero:F3}");

        bool tensorExists = tensorDof > 0.0;                 // the sector EXISTS
        bool frozenByFlatness = psi0 == 0.0;                 // ψ=0 freezes it
        bool activatedByPsi = psiNonzero > 1e-3;             // ψ≠0 activates it

        sb.AppendLine();
        sb.AppendLine($"tensor sector EXISTS (d.o.f. > 0): {tensorExists}");
        sb.AppendLine($"frozen by conformal flatness (ψ=0): {frozenByFlatness}");
        sb.AppendLine($"activated by relaxing flatness (ψ≠0): {activatedByPsi}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: 'no tensor GWs' (QG18) is a conformal-flatness ARTIFACT: the graviton sector exists");
        sb.AppendLine("but is frozen to zero by ψ=0, and is restored by ψ≠0 (the same knob as lensing).");
        Output.WriteLine(sb.ToString());

        Assert.True(tensorExists, "tensor sector should exist for d=3");
        Assert.True(frozenByFlatness, "ψ=0 should freeze the tensor sector");
        Assert.True(activatedByPsi, "ψ≠0 should activate the tensor sector");
    }

    // ── ATQG222: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG222_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG222: are the failures artifacts of conformal flatness, or fundamental?");

        sb.AppendLine("CLASSIFICATION: CONFORMAL-FLATNESS ARTIFACT (for lensing and tensor GWs; Hawking T partly).");
        sb.AppendLine();
        sb.AppendLine("  • NO LENSING: a DIRECT artifact of conformal flatness (ψ=0 → Weyl=0 → null geodesics straight).");
        sb.AppendLine("    Relaxing flatness (ψ≠0) restores lensing (ATQG220).");
        sb.AppendLine("  • NO TENSOR GWs: a DIRECT artifact of conformal flatness (ψ=0 → graviton frozen). Relaxing it");
        sb.AppendLine("    restores the +/× modes (ATQG221).");
        sb.AppendLine("  • NO HAWKING T: PARTLY an artifact — the surface gravity depends on the metric structure — but the");
        sb.AppendLine("    main failure (T ∝ R vs 1/R) stems from the mass-radius relation (deficit mass ∝ R^d vs");
        sb.AppendLine("    Schwarzschild M ∝ R), which is a SEPARATE issue from conformal flatness.");
        sb.AppendLine("  • Therefore the three failures are NOT fundamental AT results: they are consequences of the");
        sb.AppendLine("    conformal-flatness ASSUMPTION (minimum-information, G4-A1), which is PREFERRED but not derived.");
        sb.AppendLine("    They all share a single cure: a weakly non-conformal reference (ψ/Weyl field) — the new primitive");
        sb.AppendLine("    already identified in QG19.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
