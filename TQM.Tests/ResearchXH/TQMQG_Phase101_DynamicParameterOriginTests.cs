using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 101 — Parameter origin from network dynamics. Determines whether masses/couplings/mixing angles can
/// emerge from stable dynamic activity patterns. Classify: NO RELATION / PARTIAL RELATION / DYNAMIC ORIGIN.
///
/// Tests: TQMQG1010 (rate patterns + attractors), TQMQG1011 (oscillatory + metastable + families), TQMQG1012 (classification).
/// </summary>
public class TQMQG_Phase101_DynamicParameterOriginTests : ResearchTestBase
{
    public TQMQG_Phase101_DynamicParameterOriginTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG1010: actualization-rate patterns, dynamic attractors ─────────────────

    [Fact]
    public void TQMQG1010_RatesAndAttractors()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1010: do actualization patterns and attractors exist?");

        bool rates = DynamicParameterOrigin.ActualizationRatePatternsExist();
        bool attractors = DynamicParameterOrigin.DynamicAttractorsExist();

        sb.AppendLine($"actualization-rate patterns exist (Q-event activity): {rates}");
        sb.AppendLine($"dynamic RG attractors native: {attractors}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the network has genuine dynamics — actualization activity and RG attractors — providing a");
        sb.AppendLine("dynamic substrate for a parameter origin (vs static geometry).");
        Output.WriteLine(sb.ToString());

        Assert.True(rates, "rate patterns exist");
        Assert.True(attractors, "attractors exist");
    }

    // ── TQMQG1011: oscillatory states, metastable configurations, parameter families ─

    [Fact]
    public void TQMQG1011_OscillationMetastableFamilies()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1011: oscillatory states, metastable, parameter families");

        bool oscillatory = DynamicParameterOrigin.OscillatoryLinkStatesExist();
        bool metastable = DynamicParameterOrigin.MetastableConfigurationsExist();
        bool families = DynamicParameterOrigin.ParameterFamiliesFromDynamics();
        bool selects = DynamicParameterOrigin.DynamicsSelectsValues();

        sb.AppendLine($"oscillatory link states exist: {oscillatory}");
        sb.AppendLine($"metastable configurations exist: {metastable}");
        sb.AppendLine($"dynamics can organize parameters into families: {families}");
        sb.AppendLine($"native dynamics SELECTS specific SM values: {selects}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: dynamics provides an organizing structure (frequencies, attractor families), but no native");
        sb.AppendLine("dynamics selects the specific SM parameter values.");
        Output.WriteLine(sb.ToString());

        Assert.True(oscillatory, "oscillatory states exist");
        Assert.True(metastable, "metastable states exist");
        Assert.True(families, "families organizable");
        Assert.False(selects, "dynamics does not select values");
    }

    // ── TQMQG1012: classification ──────────────────────────────────────────────────

    [Fact]
    public void TQMQG1012_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1012: NO RELATION / PARTIAL RELATION / DYNAMIC ORIGIN?");

        sb.AppendLine($"CLASSIFICATION: {DynamicParameterOrigin.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO RELATION: actualization dynamics, attractors, and oscillations are real network structure.");
        sb.AppendLine("  • NOT DYNAMIC ORIGIN: no native dynamics is identified whose activity pattern equals the SM parameters.");
        sb.AppendLine("  • PARTIAL RELATION: real dynamics + organizing structure, without value selection.");
        sb.AppendLine();
        sb.AppendLine("So network dynamics gives a PARTIAL RELATION to parameters (organizing structure, not dynamic origin).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL RELATION", DynamicParameterOrigin.Classify());
        Assert.True(DynamicParameterOrigin.ActualizationRatePatternsExist());
        Assert.False(DynamicParameterOrigin.DynamicsSelectsValues());
    }
}
