using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 88 — Origin of parameter values. Determines whether dynamical selection principles within the
/// network can determine preferred parameter values. Classify: NO CONSTRAINT / PARTIAL CONSTRAINT / VALUE SELECTION.
///
/// Tests: ATQG880 (entropy extremization + stability), ATQG881 (information minimization + criticality + attractors),
/// ATQG882 (classification).
/// </summary>
public class ATQG_Phase88_ParameterValueSelectionTests : ResearchTestBase
{
    public ATQG_Phase88_ParameterValueSelectionTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG880: entropy extremization, stability criteria ────────────────────────

    [Fact]
    public void ATQG880_EntropyAndStability()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG880: entropy extremization vs stability criteria");

        bool entropyNative = ParameterValueSelection.EntropyExtremizationNative();
        bool stability = ParameterValueSelection.StabilityConstrainsValues();

        sb.AppendLine($"entropy extremization NATIVE value-selection principle: {entropyNative}");
        sb.AppendLine($"stability criteria bound parameter RANGES: {stability}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: entropy extremization is NOT native (an additional postulate). Stability IS native and");
        sb.AppendLine("bounds parameter ranges (vacuum stability λ > 0, positive mass-squared) — a partial constraint.");
        Output.WriteLine(sb.ToString());

        Assert.False(entropyNative, "entropy extremization is not native");
        Assert.True(stability, "stability constrains ranges");
    }

    // ── ATQG881: information minimization, criticality, attractors ────────────────

    [Fact]
    public void ATQG881_MinimizationCriticalityAttractors()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG881: information minimization, criticality, RG attractors");

        bool infoNative = ParameterValueSelection.InformationMinimizationNative();
        bool criticality = ParameterValueSelection.NetworkCriticalityNative();
        bool rg = ParameterValueSelection.RgAttractorsConstrain();
        bool fullSelection = ParameterValueSelection.FullValueSelectionAchieved();

        sb.AppendLine($"information minimization NATIVE: {infoNative}");
        sb.AppendLine($"network criticality NATIVE (selects values): {criticality}");
        sb.AppendLine($"RG attractors (asymptotic freedom) constrain/relate values: {rg}");
        sb.AppendLine($"full VALUE SELECTION of the 19 numbers achieved: {fullSelection}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: information minimization and criticality are NOT native. RG attractors ARE native and relate/");
        sb.AppendLine("constrain couplings (e.g. SU(3) asymptotic freedom), but no principle fully selects the specific values.");
        Output.WriteLine(sb.ToString());

        Assert.False(infoNative, "information minimization not native");
        Assert.False(criticality, "criticality not native");
        Assert.True(rg, "RG attractors constrain");
        Assert.False(fullSelection, "no full value selection");
    }

    // ── ATQG882: classification ───────────────────────────────────────────────────

    [Fact]
    public void ATQG882_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG882: NO CONSTRAINT / PARTIAL CONSTRAINT / VALUE SELECTION?");

        sb.AppendLine($"CLASSIFICATION: {ParameterValueSelection.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO CONSTRAINT: stability and RG flow DO bound/relate values.");
        sb.AppendLine("  • NOT VALUE SELECTION: no native principle determines the specific 19 numbers.");
        sb.AppendLine("  • PARTIAL CONSTRAINT: stability bounds ranges; RG attractors relate couplings; the specific values stay");
        sb.AppendLine("    free.");
        sb.AppendLine();
        sb.AppendLine("So the network PARTIALLY constrains parameter values (bounds + relations), but does not select them.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL CONSTRAINT", ParameterValueSelection.Classify());
        Assert.True(ParameterValueSelection.StabilityConstrainsValues());
        Assert.False(ParameterValueSelection.FullValueSelectionAchieved());
    }
}
