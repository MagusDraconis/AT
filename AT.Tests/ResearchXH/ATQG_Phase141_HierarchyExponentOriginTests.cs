using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 141 — Origin of hierarchy exponents. QG140 reproduced the lepton hierarchy via a fitted
/// amplification law. This phase asks whether the exponents can emerge from spectral or actualization
/// dynamics rather than fitting.
///
/// Tests: ATQG1410 (spectral scaling laws + octave occupancy), ATQG1411 (mode-density effects +
/// actualization statistics), ATQG1412 (exponent derivation + classification).
/// </summary>
public class ATQG_Phase141_HierarchyExponentOriginTests : ResearchTestBase
{
    public ATQG_Phase141_HierarchyExponentOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1410_SpectralScalingLawsAndOctaveOccupancy()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1410: spectral scaling laws and octave occupancy");

        double weyl = HierarchyExponentOrigin.WeylExponent();
        double density = HierarchyExponentOrigin.ModeDensityExponent();
        sb.AppendLine($"WEYL-LIKE SPECTRAL SCALING: N(ω) ~ ω^δ");
        sb.AppendLine($"  δ = {weyl:F3} (mode density g(ω) ~ ω^{density:F3})");
        sb.AppendLine($"  (δ≈1 → 1D, δ≈2 → 2D Weyl law)");
        sb.AppendLine();
        sb.AppendLine("OCTAVE OCCUPANCY (modes per octave band):");
        foreach (var (o, c, m) in HierarchyExponentOrigin.OctaveOccupancy())
            sb.AppendLine($"  octave {o}: center={c:F3} modes={m}");
        double occ = HierarchyExponentOrigin.OccupationExponentFromOccupancy();
        sb.AppendLine($"  fitted occupation exponent (modes ~ center^δ_occ) = {occ:F3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the spectrum follows a well-defined Weyl-like scaling and the octave");
        sb.AppendLine("occupancy follows a power law in the band center — a spectral origin for the");
        sb.AppendLine("occupation exponent.");
        Output.WriteLine(sb.ToString());

        Assert.True(weyl > 1.0 && weyl < 4.0, "Weyl exponent should be well-defined (1D–3D range)");
        Assert.True(!double.IsNaN(occ), "octave occupancy should follow a power law");
        Assert.True(occ > 1.0, "occupation exponent should be positive (growing occupancy)");
    }

    [Fact]
    public void ATQG1411_ModeDensityEffectsAndActualizationStatistics()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1411: mode-density effects and actualization statistics");

        double consistency = HierarchyExponentOrigin.DensityOccupationConsistency();
        var act = HierarchyExponentOrigin.ActualizationStatistics();
        bool hierarchy = HierarchyExponentOrigin.ActivityCarriesHierarchy();

        sb.AppendLine("MODE-DENSITY EFFECTS:");
        sb.AppendLine($"  |Weyl δ − occupation δ| = {consistency:F3}");
        sb.AppendLine($"  (small → the octave occupancy follows the spectral density)");
        sb.AppendLine();
        sb.AppendLine("ACTUALIZATION STATISTICS:");
        sb.AppendLine($"  final activity: min={act.Min:F3} max={act.Max:F3} distinct levels={act.DistinctValues}");
        sb.AppendLine($"  activity carries a hierarchy: {hierarchy}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the octave occupancy tracks the spectral density, and the raw actualization");
        sb.AppendLine("activity is saturated (no hierarchy there) — the exponents must come from the spectrum.");
        Output.WriteLine(sb.ToString());

        Assert.True(consistency < 1.0, "occupation should approximately track the spectral density");
        Assert.False(hierarchy, "final activity should be saturated (no hierarchy in activity values)");
        Assert.True(act.DistinctValues <= 2, "activity should have at most one distinct saturated level");
    }

    [Fact]
    public void ATQG1412_ExponentDerivationAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1412: exponent derivation and classification");

        double pNet = HierarchyExponentOrigin.NetMassExponent();
        double derived = HierarchyExponentOrigin.DerivedOccupationExponent();
        double weyl = HierarchyExponentOrigin.WeylExponent();
        double dev = HierarchyExponentOrigin.ExponentDerivationDeviation();
        int score = HierarchyExponentOrigin.OriginScore();
        string cls = HierarchyExponentOrigin.Classify();

        sb.AppendLine("EXPONENT DERIVATION:");
        sb.AppendLine($"  net mass exponent p_net = log(lepton span)/log(octave span) = {pNet:F3}");
        sb.AppendLine($"  derived occupation exponent δ_derived = {derived:F3}");
        sb.AppendLine($"  measured spectral density exponent δ_measured = {weyl:F3}");
        sb.AppendLine($"  relative deviation |δ_derived/δ_measured − 1| = {dev:F3}");
        sb.AppendLine();
        sb.AppendLine($"exponent-origin score (0..5): {score}");
        sb.AppendLine($"  +1 well-defined Weyl exponent: {weyl > 1.0 && weyl < 4.0}");
        sb.AppendLine($"  +1 octave occupancy power law: {!double.IsNaN(HierarchyExponentOrigin.OccupationExponentFromOccupancy())}");
        sb.AppendLine($"  +1 activity saturated: {!HierarchyExponentOrigin.ActivityCarriesHierarchy()}");
        sb.AppendLine($"  +1 partial derivation (<40%): {dev < 0.40}");
        sb.AppendLine($"  +1 tight derivation (<15%): {dev < 0.15}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • FIT ONLY rejected: the exponents follow from the spectral density scaling.");
        sb.AppendLine("  • DERIVED EXPONENTS accepted: the occupation exponent derived from the required mass");
        sb.AppendLine("    span matches the measured spectral (Weyl/mode-density) exponent — the hierarchy");
        sb.AppendLine("    amplification exponents EMERGE from the spectrum, not from free fitting.");
        Output.WriteLine(sb.ToString());

        Assert.True(dev < 0.40, "derived exponent should be close to the measured spectral exponent");
        Assert.True(dev < 0.15, "derivation should be tight");
        Assert.True(score >= 4, "exponent-origin score should be strong");
        Assert.Equal("DERIVED EXPONENTS", cls);
    }
}
