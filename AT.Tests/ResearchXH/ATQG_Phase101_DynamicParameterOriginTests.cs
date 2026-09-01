using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 101 — Parameter origin from network dynamics. Determines whether masses/couplings/mixing angles can
/// emerge from stable dynamic activity patterns. Classify: NO RELATION / PARTIAL RELATION / DYNAMIC ORIGIN.
///
/// Tests: ATQG1010 (rate patterns + attractors), ATQG1011 (oscillatory + metastable + families), ATQG1012 (classification).
/// </summary>
public class ATQG_Phase101_DynamicParameterOriginTests : ResearchTestBase
{
    public ATQG_Phase101_DynamicParameterOriginTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1010: actualization-rate patterns, dynamic attractors ─────────────────

    [Fact]
    public void ATQG1010_RatesAndAttractors()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1010: do actualization patterns and attractors exist?");
        var evidence = DynamicParameterOrigin.ComputeEvidence();

        bool rates = DynamicParameterOrigin.ActualizationRatePatternsExist(evidence);
        bool attractors = DynamicParameterOrigin.DynamicAttractorsExist(evidence);

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  A1: Evidence scores are normalized to [0,1].");
        sb.AppendLine($"  A2: Presence threshold = {DynamicParameterOrigin.PresenceThreshold:F2}.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  rate-pattern score = {evidence.ActualizationRatePatternScore:F3}");
        sb.AppendLine($"  attractor score = {evidence.DynamicAttractorScore:F3}");
        sb.AppendLine($"  rate-pattern pass? {evidence.ActualizationRatePatternScore:F3} >= {DynamicParameterOrigin.PresenceThreshold:F2} => {rates}");
        sb.AppendLine($"  attractor pass? {evidence.DynamicAttractorScore:F3} >= {DynamicParameterOrigin.PresenceThreshold:F2} => {attractors}");
        sb.AppendLine();

        sb.AppendLine("FINAL CONCLUSION:");
        sb.AppendLine($"  actualization-rate patterns exist: {rates}");
        sb.AppendLine($"  dynamic RG attractors exist: {attractors}");
        sb.AppendLine("  Therefore, dynamic substrate is present (not static-only structure).");
        Output.WriteLine(sb.ToString());

        Assert.True(rates, "rate patterns exist");
        Assert.True(attractors, "attractors exist");
        Assert.True(evidence.ActualizationRatePatternScore >= DynamicParameterOrigin.PresenceThreshold);
        Assert.True(evidence.DynamicAttractorScore >= DynamicParameterOrigin.PresenceThreshold);
    }

    // ── ATQG1011: oscillatory states, metastable configurations, parameter families ─

    [Fact]
    public void ATQG1011_OscillationMetastableFamilies()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1011: oscillatory states, metastable, parameter families");
        var evidence = DynamicParameterOrigin.ComputeEvidence();

        bool oscillatory = DynamicParameterOrigin.OscillatoryLinkStatesExist(evidence);
        bool metastable = DynamicParameterOrigin.MetastableConfigurationsExist(evidence);
        bool families = DynamicParameterOrigin.ParameterFamiliesFromDynamics(evidence);
        bool selects = DynamicParameterOrigin.DynamicsSelectsValues(evidence);

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine($"  A1: Presence threshold = {DynamicParameterOrigin.PresenceThreshold:F2} for structure signals.");
        sb.AppendLine($"  A2: Selection threshold = {DynamicParameterOrigin.SelectionThreshold:F2} for value-fixing claim.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  oscillatory score = {evidence.OscillatoryStateScore:F3}");
        sb.AppendLine($"  metastable score = {evidence.MetastableConfigurationScore:F3}");
        sb.AppendLine($"  family-organization score = {evidence.ParameterFamilyOrganizationScore:F3}");
        sb.AppendLine($"  value-selection score = {evidence.ValueSelectionScore:F3}");
        sb.AppendLine($"  oscillatory pass? {evidence.OscillatoryStateScore:F3} >= {DynamicParameterOrigin.PresenceThreshold:F2} => {oscillatory}");
        sb.AppendLine($"  metastable pass? {evidence.MetastableConfigurationScore:F3} >= {DynamicParameterOrigin.PresenceThreshold:F2} => {metastable}");
        sb.AppendLine($"  families pass? {evidence.ParameterFamilyOrganizationScore:F3} >= {DynamicParameterOrigin.PresenceThreshold:F2} => {families}");
        sb.AppendLine($"  selects values? {evidence.ValueSelectionScore:F3} >= {DynamicParameterOrigin.SelectionThreshold:F2} => {selects}");
        sb.AppendLine();

        sb.AppendLine("FINAL CONCLUSION:");
        sb.AppendLine($"  oscillatory link states exist: {oscillatory}");
        sb.AppendLine($"  metastable configurations exist: {metastable}");
        sb.AppendLine($"  dynamics organizes parameter families: {families}");
        sb.AppendLine($"  dynamics selects specific SM values: {selects}");
        sb.AppendLine("  Therefore, dynamics provides organization without value selection.");
        Output.WriteLine(sb.ToString());

        Assert.True(oscillatory, "oscillatory states exist");
        Assert.True(metastable, "metastable states exist");
        Assert.True(families, "families organizable");
        Assert.False(selects, "dynamics does not select values");
        Assert.True(evidence.ParameterFamilyOrganizationScore >= DynamicParameterOrigin.PresenceThreshold);
        Assert.True(evidence.ValueSelectionScore < DynamicParameterOrigin.SelectionThreshold);
    }

    // ── ATQG1012: classification ──────────────────────────────────────────────────

    [Fact]
    public void ATQG1012_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1012: NO RELATION / PARTIAL RELATION / DYNAMIC ORIGIN?");
        var evidence = DynamicParameterOrigin.ComputeEvidence();
        string classification = DynamicParameterOrigin.Classify(evidence);

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  A1: Classification is derived from thresholded evidence flags.");
        sb.AppendLine("  A2: DYNAMIC ORIGIN requires all structure flags true and value-selection true.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  structure flags: rates={DynamicParameterOrigin.ActualizationRatePatternsExist(evidence)}, attractors={DynamicParameterOrigin.DynamicAttractorsExist(evidence)}, oscillatory={DynamicParameterOrigin.OscillatoryLinkStatesExist(evidence)}, metastable={DynamicParameterOrigin.MetastableConfigurationsExist(evidence)}, families={DynamicParameterOrigin.ParameterFamiliesFromDynamics(evidence)}");
        sb.AppendLine($"  value-selection flag: {DynamicParameterOrigin.DynamicsSelectsValues(evidence)}");
        sb.AppendLine($"  computed classification: {classification}");
        sb.AppendLine();

        sb.AppendLine("FINAL CONCLUSION:");
        sb.AppendLine("  • NOT NO RELATION: dynamic structure evidence passes threshold.");
        sb.AppendLine("  • NOT DYNAMIC ORIGIN: value-selection evidence remains below threshold.");
        sb.AppendLine("  • PARTIAL RELATION: organization exists without selecting SM values.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL RELATION", classification);
        Assert.True(DynamicParameterOrigin.ActualizationRatePatternsExist(evidence));
        Assert.False(DynamicParameterOrigin.DynamicsSelectsValues(evidence));
    }
}
