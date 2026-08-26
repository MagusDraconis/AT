using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 296 — Reconstruction Audit. Reconstruct all major QG results (QG223-295) from only
/// the minimal theory Difference → Actualization → Inevitable Spectrum → Physics, across QM / Gravity /
/// Matter / SM / Cosmology. Classified DIRECT / INDIRECT / REQUIRES EXTRA ASSUMPTION. Output the
/// minimal dependency tree.
/// </summary>
public class ATQG_Phase296_ReconstructionAuditTests : ResearchTestBase
{
    public ATQG_Phase296_ReconstructionAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2960_DirectResults()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2960: the DIRECT results use only the minimal hierarchy");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the QM results (Born rule, duality, ψ) are DIRECT (pure Difference reads);");
        sb.AppendLine("  - the Matter/Cosmology spectral results (count, access counts, Ω_Λ, Ω_m, n_s,");
        sb.AppendLine("    peak ratios) are DIRECT (pure spectral reads).");
        sb.AppendLine();

        sb.AppendLine($"minimal theory intact: {ReconstructionAudit.MinimalTheoryIntact()}");
        sb.AppendLine($"minimal theory: {string.Join(" → ", ReconstructionAudit.MinimalTheory())}");
        sb.AppendLine();
        sb.AppendLine("DIRECT RESULTS:");
        foreach (var r in ReconstructionAudit.Results().Where(r => r.Class == ReconstructionAudit.Reconstruct.Direct))
        {
            sb.AppendLine($"  [{r.Category.PadRight(9)}] {r.Name} ({r.QgPhase}) — {r.MinimalPath}");
        }
        sb.AppendLine();
        sb.AppendLine($"direct count: {ReconstructionAudit.DirectCount()}");

        Output.WriteLine(sb.ToString());

        Assert.True(ReconstructionAudit.MinimalTheoryIntact(),
            "the minimal theory must be intact");
        Assert.True(ReconstructionAudit.Results().Where(r => r.Category == "QM")
            .All(r => r.Class == ReconstructionAudit.Reconstruct.Direct),
            "all QM results must be DIRECT");
        Assert.True(ReconstructionAudit.DirectCount() >= 8,
            "at least 8 results must be DIRECT");
    }

    [Fact]
    public void ATQG2961_IndirectResults()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2961: the INDIRECT results need only the ONE scale + derived intermediates");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - masses, couplings, mixings, gravity, predictions are INDIRECT — the structure");
        sb.AppendLine("    is chain-derived, needing only the ONE calibration scale (me/MZ) and derived");
        sb.AppendLine("    intermediates (d≥3, δ, recombination/R);");
        sb.AppendLine("  - no INDIRECT result needs a free constant.");
        sb.AppendLine();

        sb.AppendLine("INDIRECT RESULTS:");
        foreach (var r in ReconstructionAudit.Results().Where(r => r.Class == ReconstructionAudit.Reconstruct.Indirect))
        {
            sb.AppendLine($"  [{r.Category.PadRight(9)}] {r.Name} ({r.QgPhase}) — {r.MinimalPath}");
        }
        sb.AppendLine();
        sb.AppendLine($"indirect count: {ReconstructionAudit.IndirectCount()}");

        Output.WriteLine(sb.ToString());

        Assert.True(ReconstructionAudit.IndirectCount() >= 10,
            "at least 10 results must be INDIRECT");
        Assert.True(ReconstructionAudit.Results().Where(r => r.Category == "SM")
            .Any(r => r.Class == ReconstructionAudit.Reconstruct.Indirect),
            "the SM predictions must be INDIRECT (structure + calibration)");
    }

    [Fact]
    public void ATQG2962_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2962: the reconstruction determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - COMPLETE RECONSTRUCTION: all results DIRECT or INDIRECT, with only the");
        sb.AppendLine("    documented boundaries (Bekenstein 2π, 5/4) needing extra assumptions;");
        sb.AppendLine("  - MISSING LINK: a major result cannot be reconstructed from the minimal theory.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {ReconstructionAudit.Summary()}");
        sb.AppendLine($"Reconstruction score: {ReconstructionAudit.ReconstructionScore()}/5");
        sb.AppendLine($"direct={ReconstructionAudit.DirectCount()} indirect={ReconstructionAudit.IndirectCount()} extra assumption={ReconstructionAudit.ExtraAssumptionCount()}");
        sb.AppendLine($"reconstruction complete: {ReconstructionAudit.ReconstructionComplete()}");
        sb.AppendLine($"CLASSIFICATION = {ReconstructionAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("THE MINIMAL DEPENDENCY TREE:");
        sb.AppendLine("  Difference → Actualization → (N=96 fixed point) → Inevitable Spectrum");
        sb.AppendLine("    (spectral constants Σm, #d, #g, occMom, λ₂, span, occupancies)");
        sb.AppendLine("      ├→ QM (Born rule, duality, ψ) — DIRECT");
        sb.AppendLine("      ├→ Matter (count, access counts) — DIRECT; (masses) — INDIRECT (+me)");
        sb.AppendLine("      ├→ SM (couplings, mixings, predictions) — INDIRECT (+me/MZ/δ)");
        sb.AppendLine("      ├→ Gravity (Einstein, M∝R, Λ) — INDIRECT (+d≥3/R)");
        sb.AppendLine("      └→ Cosmology (Ω_Λ, Ω_m, n_s, peak ratios) — DIRECT; (positions, Λ value) — INDIRECT");
        sb.AppendLine("  EXTRA ASSUMPTIONS: Bekenstein 2π (QG185), 5/4 (QG238) — the documented boundaries.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("COMPLETE RECONSTRUCTION", ReconstructionAudit.Classify());
        Assert.True(ReconstructionAudit.ReconstructionScore() >= 5);
        Assert.True(ReconstructionAudit.ReconstructionComplete());
        Assert.Contains("COMPLETE RECONSTRUCTION", ReconstructionAudit.Summary());
    }
}
