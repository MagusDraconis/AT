using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 285 — Psi Reinterpretation Audit. What role does ψ play in the reduced hierarchy
/// (Difference → Actualization → Resonance → Measurement → Physics)? D96 only, no observables.
/// </summary>
public class ATQG_Phase285_PsiReinterpretationAuditTests : ResearchTestBase
{
    public ATQG_Phase285_PsiReinterpretationAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2850_PsiFacts()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2850: the established ψ facts");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - ψ is fundamental (cannot emerge from scalar Q-events, QG52);");
        sb.AppendLine("  - ψ is the Weyl (non-conformal) content of the connectivity (QG54).");
        sb.AppendLine();

        sb.AppendLine($"ψ fundamental (cannot emerge from scalars): {PsiReinterpretationAudit.PsiFundamental()}");
        sb.AppendLine($"ψ is the Weyl content: {PsiReinterpretationAudit.PsiIsWeylContent()}");
        sb.AppendLine($"ψ contingent (not forced by internal consistency): {PsiReinterpretationAudit.PsiContingent()}");
        sb.AppendLine($"spin-2 polarizations: {PsiReinterpretationAudit.Spin2Polarizations()}");

        Output.WriteLine(sb.ToString());

        Assert.True(PsiReinterpretationAudit.PsiFundamental(), "ψ cannot emerge from scalar Q-events");
        Assert.True(PsiReinterpretationAudit.PsiIsWeylContent(), "ψ is the non-conformal content");
        Assert.True(PsiReinterpretationAudit.PsiContingent(), "ψ is a contingent postulate, not forced");
        Assert.Equal(2, PsiReinterpretationAudit.Spin2Polarizations());
    }

    [Fact]
    public void ATQG2851_ReinterpretationLinks()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2851: the reinterpretation links in the reduced hierarchy");

        sb.AppendLine("HYPOTHESIS: ψ is reinterpretable as difference, actualization, resonance,");
        sb.AppendLine("orientation, and information — but remains fundamental.");
        sb.AppendLine();

        foreach (var (name, link, note) in PsiReinterpretationAudit.Reinterpretations())
            sb.AppendLine($"  [{name,-12}] link={link,-5} {note}");
        sb.AppendLine();
        sb.AppendLine($"links that hold: {PsiReinterpretationAudit.LinkCount()}/5");
        sb.AppendLine($"fully reinterpreted: {PsiReinterpretationAudit.FullyReinterpreted()}");

        Output.WriteLine(sb.ToString());

        Assert.True(PsiReinterpretationAudit.FullyReinterpreted(),
            "ψ is reinterpreted in all five reduced concepts");
        Assert.Equal(5, PsiReinterpretationAudit.LinkCount());
    }

    [Fact]
    public void ATQG2852_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2852: the ψ determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no target values):");
        sb.AppendLine("  - NO LINK (score ≤ 2), PARTIAL LINK (3-4), PSI REINTERPRETATION (5-6);");
        sb.AppendLine("  - the question: what role does ψ play in the reduced hierarchy?");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {PsiReinterpretationAudit.Summary()}");
        sb.AppendLine($"Reinterpretation score: {PsiReinterpretationAudit.ReinterpretationScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {PsiReinterpretationAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - ψ AS DIFFERENCE: the Weyl tensor IS the difference from conformal flatness —");
        sb.AppendLine("    the non-conformal content of the metric (strong link to Difference, QG270/278).");
        sb.AppendLine("  - ψ AS ACTUALIZATION: the anisotropic (traceless) stress — the scalar sector is");
        sb.AppendLine("    the trace/density ρ, ψ is the anisotropy.");
        sb.AppendLine("  - ψ AS RESONANCE: the spin-2 TT modes of the connectivity (2 polarizations, QG54).");
        sb.AppendLine("  - ψ AS ORIENTATION: the + and × GW polarization modes.");
        sb.AppendLine("  - ψ AS INFORMATION: the anisotropic information ρ = |ψ|² lacks.");
        sb.AppendLine("  - BUT ψ REMAINS FUNDAMENTAL (QG52: cannot emerge from scalars) — the");
        sb.AppendLine("    reinterpretation LOCATES ψ in the hierarchy without ELIMINATING it. ψ is the");
        sb.AppendLine("    tensor (anisotropic) face of the same Difference the scalar sector reads as");
        sb.AppendLine("    density.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PSI REINTERPRETATION", PsiReinterpretationAudit.Classify());
        Assert.True(PsiReinterpretationAudit.ReinterpretationScore() >= 5);
        Assert.Contains("PSI REINTERPRETATION", PsiReinterpretationAudit.Summary());
        Assert.Contains("anisotropic", PsiReinterpretationAudit.Summary());
    }
}
