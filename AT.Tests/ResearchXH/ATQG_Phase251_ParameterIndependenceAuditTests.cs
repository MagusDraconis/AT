using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 251 — Parameter Independence Audit. Determine the effective number of independent D96
/// parameters and whether over-parameterization can reasonably be claimed.
/// </summary>
public class ATQG_Phase251_ParameterIndependenceAuditTests : ResearchTestBase
{
    public ATQG_Phase251_ParameterIndependenceAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2510_ParameterClassifications()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2510: the nine D96 parameters");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The eight spectral quantities (Σm, #d, #g, span, λ₂, Σ√m, occ, occMom) all descend");
        sb.AppendLine("    from ONE object: the D96 network spectrum (multiplicity multiset + octave bands);");
        sb.AppendLine("  - me is the single free empirical input.");
        sb.AppendLine();

        sb.AppendLine("THE MULTIPLICITY MULTISET (the generating object):");
        var ms = ParameterIndependenceAudit.MultiplicityMultiset();
        sb.AppendLine($"  [{string.Join(",", ms)}]  (#g = {ms.Length}, Σm = {ms.Sum()}, #d = {ms.Count(m => m == 2)})");
        sb.AppendLine($"Octave occupancies: [{string.Join(",", ParameterIndependenceAudit.OctaveOccupancies())}]");
        sb.AppendLine();

        sb.AppendLine("THE NINE PARAMETERS:");
        foreach (var p in ParameterIndependenceAudit.Parameters())
        {
            string val = p.Name == "occ (octave occupancies)" ? "[4,4,87]" : p.Value.ToString("F6", CultureInfo.InvariantCulture);
            sb.AppendLine($"  {p.Name,-32} = {val,-10} [{p.Status}]");
            sb.AppendLine($"      {p.Source}");
        }
        sb.AppendLine();
        sb.AppendLine($"By status: {string.Join(", ", ParameterIndependenceAudit.StatusCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");

        Output.WriteLine(sb.ToString());

        var sc = ParameterIndependenceAudit.StatusCounts();
        Assert.Equal(1, sc[ParameterIndependenceAudit.Status.Independent]);  // me only
        Assert.Equal(1, sc[ParameterIndependenceAudit.Status.Derived]);      // occMom
        Assert.Equal(7, sc[ParameterIndependenceAudit.Status.Dependent]);    // the rest
        Assert.Equal(0, ParameterIndependenceAudit.MutuallyIndependentSpectralCount());
    }

    [Fact]
    public void ATQG2511_EffectiveCountAndRatio()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2511: the effective independent parameter count");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Effective count = the listed independent inputs + the single structural source");
        sb.AppendLine("    (the D96 network) that fixes every dependent quantity.");
        sb.AppendLine();

        sb.AppendLine($"Independent listed parameters (me): {ParameterIndependenceAudit.Parameters().Count(p => p.Status == ParameterIndependenceAudit.Status.Independent)}");
        sb.AppendLine($"+ D96 structural selection (the network): 1");
        sb.AppendLine($"EFFECTIVE INDEPENDENT PARAMETER COUNT = {ParameterIndependenceAudit.EffectiveParameterCount()}");
        sb.AppendLine($"Derived physical targets (observable register): {ParameterIndependenceAudit.DerivedTargetCount()}");
        sb.AppendLine($"Targets : free-input ratio ≈ {ParameterIndependenceAudit.TargetRatio():F0}:1");
        sb.AppendLine($"Are the eight spectral quantities eight independent knobs? {ParameterIndependenceAudit.EightIndependentKnobs()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(2, ParameterIndependenceAudit.EffectiveParameterCount());
        Assert.True(ParameterIndependenceAudit.TargetRatio() >= 10, "the ratio must be ≥ 10:1 for LOW risk");
        Assert.False(ParameterIndependenceAudit.EightIndependentKnobs(), "the eight quantities collapse to one spectrum");
    }

    [Fact]
    public void ATQG2512_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2512: the parameter-leakage risk");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - LOW: effective free inputs ≤ 3 AND target:input ratio ≥ 10:1;");
        sb.AppendLine("  - The QG250 F1 attack claimed eight independent knobs — this audit tests that premise.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {ParameterIndependenceAudit.Summary()}");
        sb.AppendLine($"CLASSIFICATION = {ParameterIndependenceAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("RESIDUAL (separate) RISK: formula selection — which combination of the LOCKED");
        sb.AppendLine("quantities was chosen post-hoc (QG239, QG250 #6) — a distinct claim not adjudicated here.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("LOW", ParameterIndependenceAudit.Classify());
        Assert.Contains("LOW", ParameterIndependenceAudit.Summary());
        Assert.Equal(2, ParameterIndependenceAudit.EffectiveParameterCount());
    }
}
