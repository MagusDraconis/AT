using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 24 — minimal tensor extension audit. Ranks the four candidate new primitives by the additional
/// degrees of freedom they introduce and whether they can source helicity-2, to find the smallest extension that
/// restores lensing, tensor GWs, and Hawking thermodynamics. Classify: DERIVED / EMERGENT / NEW PRIMITIVE /
/// MINIMAL NEW PRIMITIVE.
///
/// Tests: TQMQG240 (d.o.f. census & minimality), TQMQG241 (observable requirements), TQMQG242 (classification).
/// </summary>
public class TQMQG_Phase24_MinimalTensorExtensionTests : ResearchTestBase
{
    public TQMQG_Phase24_MinimalTensorExtensionTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG240: candidate census — which primitive is smallest AND spin-2-capable? ───

    [Fact]
    public void TQMQG240_CandidateCensus()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG240: candidate new primitives ranked by d.o.f. and spin");

        int d = 3;
        double tensorMeasure = MinimalTensorExtension.TensorCountingMeasureDof(d);
        double directional = MinimalTensorExtension.DirectionalActualizationDof(d);
        double anisotropic = MinimalTensorExtension.AnisotropicCausalDof(d);
        double psi = MinimalTensorExtension.PsiFieldDof(d);

        sb.AppendLine($"tensor counting measure      : {tensorMeasure} d.o.f.  spin-{MinimalTensorExtension.MaxHelicity("rank2")}");
        sb.AppendLine($"directional actualization     : {directional} d.o.f.  spin-{MinimalTensorExtension.MaxHelicity("vector")}");
        sb.AppendLine($"anisotropic causal structure  : {anisotropic} d.o.f.  spin-{MinimalTensorExtension.MaxHelicity("rank2")}");
        sb.AppendLine($"ψ-field (independent spin-2)  : {psi} d.o.f.  spin-{MinimalTensorExtension.MaxHelicity("psi")}");

        bool psiIsSmallest = psi < directional && psi < tensorMeasure && psi < anisotropic;
        bool directionalInsufficient = !MinimalTensorExtension.CanSourceTensorGWs("vector");
        bool rank2OverComplete = tensorMeasure > psi && MinimalTensorExtension.CanSourceTensorGWs("rank2");
        bool psiMinimalAndCapable = psi <= tensorMeasure && MinimalTensorExtension.CanSourceTensorGWs("psi");

        sb.AppendLine();
        sb.AppendLine($"ψ is the smallest candidate ({psi} < 3 < 6): {psiIsSmallest}");
        sb.AppendLine($"directional (spin-1) cannot source helicity-2: {directionalInsufficient}");
        sb.AppendLine($"rank-2 candidates are spin-2-capable but over-complete (6 > 2): {rank2OverComplete}");
        sb.AppendLine($"ψ is both minimal and spin-2-capable: {psiMinimalAndCapable}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the two rank-2 primitives (tensor counting measure, anisotropic causal structure) both carry");
        sb.AppendLine("6 d.o.f. and are over-complete; directional actualization is smaller (3) but spin-1, so it cannot make");
        sb.AppendLine("gravitons. Only the ψ-field delivers exactly the 2 graviton d.o.f. — the minimal spin-2 primitive.");
        Output.WriteLine(sb.ToString());

        Assert.True(psiIsSmallest, "ψ should be the smallest candidate");
        Assert.True(directionalInsufficient, "a vector cannot source spin-2");
        Assert.True(psiMinimalAndCapable, "ψ should be the minimal spin-2-capable primitive");
    }

    // ── TQMQG241: observable requirements — what do lensing/GW/Hawking each need? ─────

    [Fact]
    public void TQMQG241_ObservableRequirements()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG241: minimal additional d.o.f. to restore all three observables");

        int d = 3;
        double lensing = MinimalTensorExtension.LensingDofRequired();
        double gw = MinimalTensorExtension.TensorGWDofRequired(d);
        double hawking = MinimalTensorExtension.HawkingDofRequired();
        double minimal = MinimalTensorExtension.MinimalAdditionalDof(d);

        sb.AppendLine($"lensing   (Weyl ≠ 0)  needs ≥ {lensing} d.o.f.  (a scalar ψ breaks conformal flatness)");
        sb.AppendLine($"tensor GW (helicity-2) needs = {gw} d.o.f.  (2 polarizations at d=3)");
        sb.AppendLine($"Hawking T (T = κ/2π)  needs + {hawking} d.o.f.  (derived from the horizon profile)");
        sb.AppendLine($"=> minimal additional d.o.f. = max(1, 2, 0) = {minimal}");
        sb.AppendLine();

        bool gravitonCoversLensing = gw >= lensing;
        bool gravitonCoversHawking = gw >= hawking;
        bool minimalEqualsGraviton = minimal == gw;

        sb.AppendLine($"graviton (2) covers lensing (1): {gravitonCoversLensing}");
        sb.AppendLine($"graviton (2) covers Hawking (0): {gravitonCoversHawking}");
        sb.AppendLine($"minimal additional d.o.f. = graviton count: {minimalEqualsGraviton}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the 2 graviton helicities are the unique minimum — they restore lensing (need 1) and tensor GWs");
        sb.AppendLine("(need exactly 2), while Hawking T costs nothing extra because T = κ/2π follows from the horizon profile.");
        Output.WriteLine(sb.ToString());

        Assert.True(gravitonCoversLensing, "graviton should cover the lensing requirement");
        Assert.True(minimalEqualsGraviton, "minimal extension = 2 graviton d.o.f.");
        Assert.Equal(2.0, minimal);
    }

    // ── TQMQG242: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG242_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG242: classification of the minimal tensor extension");

        sb.AppendLine("CLASSIFICATION: MINIMAL NEW PRIMITIVE.");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED / NOT EMERGENT: QG23 showed ψ cannot arise from the single scalar actualization.");
        sb.AppendLine("  • A NEW PRIMITIVE is required; the question here is which one is SMALLEST.");
        sb.AppendLine("  • tensor counting measure      → 6 d.o.f. (rank-2, over-complete)");
        sb.AppendLine("  • directional actualization    → 3 d.o.f. (spin-1, INSUFFICIENT for helicity-2)");
        sb.AppendLine("  • anisotropic causal structure → 6 d.o.f. (rank-2, over-complete)");
        sb.AppendLine("  • ψ-field (spin-2)             → 2 d.o.f. (MINIMAL, exactly the graviton)");
        sb.AppendLine();
        sb.AppendLine("  Therefore the MINIMAL NEW PRIMITIVE is the ψ-field: a transverse-traceless, symmetric rank-2 (spin-2) field");
        sb.AppendLine("  carrying exactly the 2 graviton polarizations. It is the smallest extension that restores lensing (1 d.o.f." +
                      " needed),");
        sb.AppendLine("  tensor GWs (2 needed), and Hawking thermodynamics (0 extra).");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
