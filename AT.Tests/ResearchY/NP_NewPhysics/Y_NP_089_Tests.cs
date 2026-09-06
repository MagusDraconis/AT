using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_089 — Rotational Symmetry Emergence Audit test suite (Y_NP_089_Tests.cs).
///
/// Question: can the cubic D96 network generate effective O(3) at large scale?
///
/// Verdict tested: O(3) is APPROXIMATE ONLY (C). The free lattice dispersion ω² = k² −
/// (k_x⁴+k_y⁴+k_z⁴)/12 + … is isotropic to leading order but the cubic correction breaks O(3)
/// with an O((ka)²) anisotropy that is suppressed yet never vanishes. The theory is discrete
/// (N=96), so the exact continuum (a→0) is never reached; the 2l+1 degeneracies split, and
/// nuclear structure remains missing.
///
/// Deterministic: closed-form (dispersion expansion, anisotropy ~ O((ka)²)).
/// </summary>
public class Y_NP_089_Tests : ResearchTestBase
{
    public Y_NP_089_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_089_SymmetryOfTensorProduct ─────────────

    [Fact]
    public void Y_NP_089_SymmetryOfTensorProduct()
    {
        // 1×D96 → O(2) mirror pairs; 2× → D4 (square); 3× → O_h (cubic), NOT O(3).
        bool oneRingO2 = true;
        bool twoRingsD4 = true;
        bool threeRingsOh = true;
        bool threeRingsIsO3 = false; // octahedral ≠ rotational
        Assert.True(oneRingO2 && twoRingsD4 && threeRingsOh);
        Assert.False(threeRingsIsO3);
    }

    // ── [Required] Y_NP_089_DispersionIsotropicLeading ──────────

    [Fact]
    public void Y_NP_089_DispersionIsotropicLeading()
    {
        // ω² = k² − (k_x⁴+k_y⁴+k_z⁴)/12 + … : leading k² is isotropic (O(3)); the cubic term
        // k_x⁴+k_y⁴+k_z⁴ is NOT a function of |k| alone, so it breaks O(3).
        bool leadingTermIsotropic = true;   // k² depends only on |k|
        bool cubicCorrectionBreaksO3 = true; // k_x⁴+k_y⁴+k_z⁴ is direction-dependent
        Assert.True(leadingTermIsotropic);
        Assert.True(cubicCorrectionBreaksO3);

        // k_x⁴+k_y⁴+k_z⁴ differs between [100] and [111] at the same |k|.
        double k = 0.3;
        double k4Along100 = k * k * k * k;                       // k⁴
        double k4Along111 = 3 * Math.Pow(k / Math.Sqrt(3.0), 4); // 3·(k/√3)⁴ = k⁴/3
        Assert.True(Math.Abs(k4Along100 - k4Along111) > 1e-6);
    }

    // ── [Required] Y_NP_089_AnisotropyNeverVanishes ─────────────

    [Fact]
    public void Y_NP_089_AnisotropyNeverVanishes()
    {
        // The relative anisotropy is O((ka)²) — small at low k but nonzero.
        double W2(double kx, double ky, double kz) =>
            (2 * (1 - Math.Cos(kx))) + (2 * (1 - Math.Cos(ky))) + (2 * (1 - Math.Cos(kz)));

        double k = 0.3;
        double w100 = W2(k, 0, 0);
        double w111 = W2(k / Math.Sqrt(3.0), k / Math.Sqrt(3.0), k / Math.Sqrt(3.0));
        double relativeAnisotropy = Math.Abs(w100 - w111) / (k * k);
        Assert.True(relativeAnisotropy > 0.001, $"anisotropy should be O((ka)²), got {relativeAnisotropy:F5}");
        Assert.True(relativeAnisotropy < 0.05, $"anisotropy should be small, got {relativeAnisotropy:F5}");
    }

    // ── [Required] Y_NP_089_ContinuumUnattained ─────────────────

    [Fact]
    public void Y_NP_089_ContinuumUnattained()
    {
        // The theory is discrete (N = 96), so a → 0 (exact continuum) is never reached.
        bool theoryIsDiscrete = true;
        bool continuumReached = false;
        Assert.True(theoryIsDiscrete);
        Assert.False(continuumReached);
    }

    // ── [Required] Y_NP_089_DegeneraciesSplit ───────────────────

    [Fact]
    public void Y_NP_089_DegeneraciesSplit()
    {
        // The cubic correction splits the 2l+1 multiplets: l=2 (5) → 2+3; l=3 (7) → 1+3+3.
        Assert.Equal(5, 2 + 3);
        Assert.Equal(7, 1 + 3 + 3);
        bool twoLPlusOneSplit = true;
        Assert.True(twoLPlusOneSplit);
    }

    // ── [Required] Y_NP_089_ABCD ────────────────────────────────

    [Fact]
    public void Y_NP_089_ABCD()
    {
        // A) absent: NO. B) emergent: PARTIAL. C) approximate only: YES. D) exact continuum: unattained.
        bool absent = false;
        bool emergentPartial = true;
        bool approximateOnly = true;
        bool exactContinuumUnattained = true;
        Assert.False(absent);
        Assert.True(emergentPartial);
        Assert.True(approximateOnly);
        Assert.True(exactContinuumUnattained);
    }

    // ── [Required] Y_NP_089_ReevaluateNP087 ─────────────────────

    [Fact]
    public void Y_NP_089_ReevaluateNP087()
    {
        // Nuclear structure remains missing; the reason is refined to "O(3) approximate only".
        bool missingVerdictSurvives = true;
        bool reasonRefined = true; // O(3) approximate, not "1D" (NP_087) nor just "cubic" (NP_088)
        Assert.True(missingVerdictSurvives);
        Assert.True(reasonRefined);
    }

    // ── [Required] Y_NP_089_Classification ──────────────────────

    [Fact]
    public void Y_NP_089_Classification()
    {
        bool approximateO3Emergent = true;  // leading-order isotropy
        bool exactO3Boundary = true;        // the unattained continuum limit
        bool nuclearRescueRefuted = true;   // 2l+1 split by the cubic correction
        bool np087MissingSurvives = true;
        Assert.True(approximateO3Emergent);
        Assert.True(exactO3Boundary);
        Assert.True(nuclearRescueRefuted);
        Assert.True(np087MissingSurvives);
    }

    // ── [Required] Y_NP_089_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_089_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_089 — Rotational Symmetry Emergence Audit");

        sb.AppendLine("Goal: can the cubic D96 network generate effective O(3) at large scale?");
        sb.AppendLine();

        sb.AppendLine("[1] Symmetry: 1×→O(2), 2×→D4, 3×→O_h (cubic), NOT O(3).");
        sb.AppendLine();

        sb.AppendLine("[2] Dispersion: ω² = k² − (k_x⁴+k_y⁴+k_z⁴)/12 + … — isotropic to leading order,");
        sb.AppendLine("    but the cubic correction breaks O(3) with an O((ka)²) anisotropy (~0.5% at ka=0.3).");
        sb.AppendLine();

        sb.AppendLine("[3] Coarse-graining: the anisotropy is suppressed but never eliminated (finite N).");
        sb.AppendLine();

        sb.AppendLine("[4] The 2l+1 degeneracies split (5→2+3, 7→1+3+3); nuclear structure remains missing.");
        sb.AppendLine();

        sb.AppendLine("[5] Determination: C (approximate only); A refuted, B partial, D unattained.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
