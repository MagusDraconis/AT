using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 259 — Observable Origin Audit. Determine whether observables were selected because they
/// matched a D96 formula (post-hoc) or because D96 naturally points to them.
/// </summary>
public class ATQG_Phase259_ObservableOriginAuditTests : ResearchTestBase
{
    public ATQG_Phase259_ObservableOriginAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2590_ObservableRegister()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2590: the observable register (QG140-258)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - OBSERVABLE audit only: no formula complexity, no uniqueness (that is QG253);");
        sb.AppendLine("  - NATURAL = D96 structure alone leads to it (identity / octave-forced class /");
        sb.AppendLine("    frozen-or-hidden value before measurement);");
        sb.AppendLine("  - SECONDARY = catalog value known at derivation, but D96 class-consistent;");
        sb.AppendLine("  - POST-HOC = entered the register because a formula matched it (QG239/250 flags).");
        sb.AppendLine();

        foreach (var g in ObservableOriginAudit.Observables().GroupBy(o => o.Cat))
        {
            sb.AppendLine($"── {g.Key} ──");
            foreach (var o in g)
                sb.AppendLine($"  [{o.Origin,-17}] {o.Name} ({o.Phase})");
        }
        sb.AppendLine();
        sb.AppendLine($"By category: {string.Join(", ", ObservableOriginAudit.CategoryCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");
        sb.AppendLine($"By origin: {string.Join(", ", ObservableOriginAudit.Counts().Select(kv => $"{kv.Key}={kv.Value}"))}");
        sb.AppendLine($"Total observables: {ObservableOriginAudit.Total()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(29, ObservableOriginAudit.Total());
        var c = ObservableOriginAudit.Counts();
        Assert.Equal(7, c[ObservableOriginAudit.Origin.NaturalTarget]);
        Assert.Equal(19, c[ObservableOriginAudit.Origin.SecondaryTarget]);
        Assert.Equal(3, c[ObservableOriginAudit.Origin.PostHocTarget]);
    }

    [Fact]
    public void ATQG2591_OriginEvidence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2591: the origin evidence for every observable");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Structural pointer + temporal independence => NATURAL;");
        sb.AppendLine("  - Structural pointer, catalog value known => SECONDARY;");
        sb.AppendLine("  - Explicit retro-selection / asserted dictionary flag => POST-HOC.");
        sb.AppendLine();

        foreach (var o in ObservableOriginAudit.Observables())
            sb.AppendLine($"  [{o.Origin,-17}] {o.Name}: {o.Evidence}");

        Output.WriteLine(sb.ToString());

        // Key spot-checks of the classification.
        var reg = ObservableOriginAudit.Observables();
        Assert.Equal(ObservableOriginAudit.Origin.NaturalTarget,
            reg.Single(o => o.Name == "Family count = 3").Origin);
        Assert.Equal(ObservableOriginAudit.Origin.PostHocTarget,
            reg.Single(o => o.Name == "Spectral index n_s").Origin);
        Assert.Equal(ObservableOriginAudit.Origin.PostHocTarget,
            reg.Single(o => o.Name == "1/α_em = 137").Origin);
        Assert.Equal(ObservableOriginAudit.Origin.NaturalTarget,
            reg.Single(o => o.Name == "P1 — 106 GeV resonance").Origin);
        // The honest Bekenstein failure is NOT retro-selection — it is a catalog miss.
        Assert.Equal(ObservableOriginAudit.Origin.SecondaryTarget,
            reg.Single(o => o.Name == "Bekenstein S = A/4").Origin);
    }

    [Fact]
    public void ATQG2592_SelectionRisk()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2592: the observable-selection risk");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - risk score = (0.5·secondary + 1.0·post-hoc)/total (natural contributes 0);");
        sb.AppendLine("  - LOW < 0.25, MEDIUM < 0.60, HIGH ≥ 0.60.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {ObservableOriginAudit.Summary()}");
        sb.AppendLine($"Risk score: {ObservableOriginAudit.RiskScore():F3}");
        sb.AppendLine($"Natural fraction: {ObservableOriginAudit.NaturalFraction():P1}");
        sb.AppendLine($"CLASSIFICATION = {ObservableOriginAudit.ClassifyRisk()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - The register is predominantly CATALOG-DRIVEN: most observables are SECONDARY");
        sb.AppendLine("    (D96 class-consistent, value known at derivation). This is the expected");
        sb.AppendLine("    situation for a theory that reproduces the measured SM/GR/cosmology catalog.");
        sb.AppendLine("  - A genuine NATURAL core exists and is temporally independent: octave ratios,");
        sb.AppendLine("    family count, θ_QCD = 0, the PRE-REGISTERED ladder (P1/P2/P3), and the blind");
        sb.AppendLine("    Higgs reconstruction (QG176).");
        sb.AppendLine("  - A small POST-HOC minority is explicitly flagged: n_s + acoustic peaks");
        sb.AppendLine("    (QG239 retro-selection) and the 1/α_em dictionary (QG250).");
        sb.AppendLine("  - The honest Bekenstein FAILURE (QG185/196) is anti-retro evidence: the catalog");
        sb.AppendLine("    contains a target D96 cannot match, so selection is not pure fitting.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("MEDIUM", ObservableOriginAudit.ClassifyRisk());
        Assert.True(ObservableOriginAudit.RiskScore() >= 0.25 && ObservableOriginAudit.RiskScore() < 0.60);
        Assert.Contains("MEDIUM", ObservableOriginAudit.Summary());
    }
}
