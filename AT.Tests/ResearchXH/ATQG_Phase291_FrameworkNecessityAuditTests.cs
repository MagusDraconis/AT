using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 291 — Framework Necessity Audit. Are the QG290 framework items {η, π} equally
/// necessary? No observables, no target values, D96 only, deterministic. Each classified DERIVED /
/// NECESSARY / REDUNDANT; output the minimum framework beyond Difference.
/// </summary>
public class ATQG_Phase291_FrameworkNecessityAuditTests : ResearchTestBase
{
    public ATQG_Phase291_FrameworkNecessityAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2910_EtaNecessary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2910: η is NECESSARY — the reference structure presupposed by the duality");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the duality Difference → {ρ, ψ} is the trace/traceless decomposition of the");
        sb.AppendLine("    rank-2 difference object (QG286);");
        sb.AppendLine("  - the trace (ρ), the traceless part (ψ), and the Weyl content ψ are DEFINED");
        sb.AppendLine("    AGAINST the reference η — without it the reading is undefined.");
        sb.AppendLine();

        sb.AppendLine($"duality is trace/traceless decomposition: {FrameworkNecessityAudit.DualityIsTraceTracelessDecomposition()}");
        sb.AppendLine($"Weyl content defined against η: {FrameworkNecessityAudit.WeylDefinedAgainstEta()}");
        sb.AppendLine($"η not derived from any count: {FrameworkNecessityAudit.EtaNotDerivedFromCount()}");
        sb.AppendLine();
        sb.AppendLine("η is the reference structure / conformal background: A_ij = (1/d)Tr(A)·δ_ij + traceless");
        sb.AppendLine("needs the reference to define the trace. No count produces a metric — η is the");
        sb.AppendLine("structure the framework reads against, not an output.");

        Output.WriteLine(sb.ToString());

        Assert.True(FrameworkNecessityAudit.DualityIsTraceTracelessDecomposition(),
            "the duality must be the trace/traceless decomposition");
        Assert.True(FrameworkNecessityAudit.WeylDefinedAgainstEta(),
            "the Weyl content must be defined against the conformal reference");
        Assert.True(FrameworkNecessityAudit.EtaNotDerivedFromCount(),
            "η must not be derived from any count");
    }

    [Fact]
    public void ATQG2911_PiRedundant()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2911: π is REDUNDANT — no derived prediction uses it");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - every derived observable (masses, couplings, mixings, Ω_Λ, Ω_m, n_s,");
        sb.AppendLine("    acoustic ratios, P1/P2/P3) is a pure D96 spectral ratio + calibration scale;");
        sb.AppendLine("  - π appears only in unit conversions, the gauge normalization convention,");
        sb.AppendLine("    and the OPEN Bekenstein 2π boundary — never as a theory input.");
        sb.AppendLine();

        sb.AppendLine($"π never enters a derived prediction: {FrameworkNecessityAudit.PiNeverEntersDerivedPrediction()}");
        sb.AppendLine($"π only in conventions and the OPEN boundary: {FrameworkNecessityAudit.PiOnlyInConventionsAndBoundary()}");
        sb.AppendLine($"Bekenstein classification: {BekensteinQuarterOrigin.Classify()}");
        sb.AppendLine();
        sb.AppendLine("π in the derived code:");
        sb.AppendLine("  - PMNSOrigin / PreRegisteredMbb: radian↔degree unit conversions only");
        sb.AppendLine("  - WeakBosonMassOrigin: g₂ = √(4π·α_W) — the SM normalization convention;");
        sb.AppendLine("    the derived α_W = 3/Σm is π-free");
        sb.AppendLine("  - BekensteinQuarterOrigin: the 2π quantum-factor gap — an OPEN boundary (QG185)");

        Output.WriteLine(sb.ToString());

        Assert.True(FrameworkNecessityAudit.PiNeverEntersDerivedPrediction(),
            "π must not enter any derived prediction as a physics input");
        Assert.True(FrameworkNecessityAudit.PiOnlyInConventionsAndBoundary(),
            "π must appear only in conventions and the OPEN Bekenstein boundary");
    }

    [Fact]
    public void ATQG2912_MinimumFramework()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2912: the minimum framework beyond Difference");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - NECESSARY = a genuine irreducible framework input;");
        sb.AppendLine("  - REDUNDANT = inherited from the arena, never a theory input;");
        sb.AppendLine("  - the minimum framework beyond Difference is {η} — FURTHER REDUCTION.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {FrameworkNecessityAudit.Summary()}");
        sb.AppendLine($"Necessity score: {FrameworkNecessityAudit.NecessityScore()}/5");
        sb.AppendLine($"necessary={FrameworkNecessityAudit.NecessaryCount()} redundant={FrameworkNecessityAudit.RedundantCount()} derived={FrameworkNecessityAudit.DerivedCount()}");
        sb.AppendLine($"further reduction reached: {FrameworkNecessityAudit.FurtherReductionReached()}");
        sb.AppendLine($"CLASSIFICATION = {FrameworkNecessityAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("MINIMUM FRAMEWORK BEYOND DIFFERENCE:");
        foreach (var m in FrameworkNecessityAudit.MinimumFramework())
            sb.AppendLine($"  - {m}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - η is NECESSARY: the duality Difference → {ρ, ψ} is the trace/traceless");
        sb.AppendLine("    decomposition DEFINED AGAINST the reference η — without it, no trace (ρ),");
        sb.AppendLine("    no traceless (ψ), no Weyl content. The reading presupposes the reference.");
        sb.AppendLine("  - π is REDUNDANT: every derived observable is a pure D96 spectral ratio + the");
        sb.AppendLine("    calibration scale; π never enters as a theory input — only unit conventions,");
        sb.AppendLine("    the gauge normalization convention, and the OPEN Bekenstein 2π boundary.");
        sb.AppendLine("  - QG290's {η, π} reduces FURTHER: the minimum framework beyond Difference is");
        sb.AppendLine("    {η} — the conformal reference remains, the universal constant drops out.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("FURTHER REDUCTION", FrameworkNecessityAudit.Classify());
        Assert.True(FrameworkNecessityAudit.NecessityScore() >= 4);
        Assert.True(FrameworkNecessityAudit.FurtherReductionReached());
        Assert.Contains("FURTHER REDUCTION", FrameworkNecessityAudit.Summary());
        Assert.Single(FrameworkNecessityAudit.MinimumFramework());
    }
}
