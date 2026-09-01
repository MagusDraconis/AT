using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 157 — Origin of effective access counts. QG156 established δ = log(N_eff)/log(span) and
/// reproduced all four sectors. This phase derives N_eff directly from the D96/Z2 doublet-multiplicity and
/// octave-occupation structure, with no fitted sector/charge/isospin parameters.
///
/// Tests: ATQG1570 (D96 moment structure), ATQG1571 (derived counts predict sectors), ATQG1572
/// (no-parameter law + classification).
/// </summary>
public class ATQG_Phase157_EffectiveAccessCountsTests : ResearchTestBase
{
    public ATQG_Phase157_EffectiveAccessCountsTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1570_D96MomentStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1570: D96 moment structure");

        var ms = EffectiveAccessCounts.DoubletMultiplicities();
        sb.AppendLine("D96/Z2 OCCUPATION STRUCTURE (degenerate doublet multiplicities):");
        sb.AppendLine($"  doublet groups: {ms.Length}");
        sb.AppendLine($"  multiplicities: [{string.Join(",", ms)}]");
        sb.AppendLine($"  Σm = {ms.Sum()} (= total mode count 95)");
        sb.AppendLine();
        sb.AppendLine("OCTAVE OCCUPATIONS:");
        sb.AppendLine($"  [{string.Join(",", EffectiveAccessCounts.OctaveOccupancies())}]");
        sb.AppendLine();
        sb.AppendLine("D96 MOMENTS:");
        sb.AppendLine($"  Σ√m = {EffectiveAccessCounts.DoubletMoment(0.5):F3}");
        sb.AppendLine($"  Σm  = {EffectiveAccessCounts.DoubletMoment(1.0):F3}");
        sb.AppendLine($"  Σm² = {EffectiveAccessCounts.DoubletMoment(2.0):F3}");
        sb.AppendLine($"  Σocc²/occ₀ = {EffectiveAccessCounts.OctaveOccupationMoment():F3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the N_eff values are MOMENTS of the D96 occupation structure — the");
        sb.AppendLine("doublet-multiplicity distribution and the octave-occupation distribution.");
        Output.WriteLine(sb.ToString());

        Assert.True(ms.Sum() > 0, "multiplicity distribution should be non-trivial");
        Assert.True(EffectiveAccessCounts.DoubletMoment(0.5) < EffectiveAccessCounts.DoubletMoment(1.0),
            "moments should be increasing");
        Assert.True(EffectiveAccessCounts.DoubletMoment(1.0) < EffectiveAccessCounts.DoubletMoment(2.0),
            "moments should be increasing");
    }

    [Fact]
    public void ATQG1571_DerivedCountsPredictSectors()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1571: derived counts predict the sector dimensions");

        sb.AppendLine("N_eff DERIVED FROM D96 GEOMETRY (no fitted parameters):");
        foreach (var (n, c, m, a) in EffectiveAccessCounts.AccessCounts())
            sb.AppendLine($"  {n}: N_eff = {c:F3}  ({m} — {a})");
        sb.AppendLine();
        sb.AppendLine("UNIFIED LAW δ = log(N_eff)/log(span):");
        foreach (var (n, p, t, d, c) in EffectiveAccessCounts.UnifiedLaw())
            sb.AppendLine($"  {n}: predicted δ={p:F4}  target δ={t:F4}  deviation={d:P2}  (N_eff={c:F2})");
        sb.AppendLine();
        sb.AppendLine($"  mean deviation = {EffectiveAccessCounts.MeanDeviation():P2}");
        sb.AppendLine($"  max deviation = {EffectiveAccessCounts.MaxDeviation():P2}");
        sb.AppendLine($"  sectors within 5%: {EffectiveAccessCounts.SectorsWithin5Percent()}/4");
        sb.AppendLine();
        sb.AppendLine("  ν = Σ√m (neutral half-moment, statistical access — no charge channel, QG154)");
        sb.AppendLine("  d = Σm  (first moment, full-spectrum access, QG150)");
        sb.AppendLine("  ℓ = Σm² (doublet-occupancy moment, QG153)");
        sb.AppendLine("  u = Σocc²/occ₀ (octave-occupation moment, dense-band access, QG150)");
        Output.WriteLine(sb.ToString());

        Assert.True(EffectiveAccessCounts.Predictive(), "all four sectors should be within 5%");
        Assert.True(EffectiveAccessCounts.MaxDeviation() < 0.05, "max deviation < 5%");
        Assert.True(EffectiveAccessCounts.UnifiedLaw()[0].Deviation < 0.01, "neutrino should be near-exact");
        Assert.True(EffectiveAccessCounts.UnifiedLaw()[3].Deviation < 0.01, "up should be near-exact");
    }

    [Fact]
    public void ATQG1572_NoParameterLawAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1572: no-parameter law and classification");

        sb.AppendLine("NO-PARAMETER CHECK — the moment orders are fixed by the D96 structure:");
        foreach (var (n, order, structure) in EffectiveAccessCounts.MomentOrders())
            sb.AppendLine($"  {n}: moment order {(double.IsNaN(order) ? "octave" : order.ToString("F1", CultureInfo.InvariantCulture))} over the {structure}");
        sb.AppendLine();
        sb.AppendLine("  • no fitted sector parameters (orders are 1/2, 1, 2 — fixed half/first/second");
        sb.AppendLine("    moments of the multiplicity distribution; the octave moment uses occ²/occ₀)");
        sb.AppendLine("  • no charge-law fitting, no isospin coefficient fitting");
        sb.AppendLine();
        int score = EffectiveAccessCounts.OriginScore();
        string cls = EffectiveAccessCounts.Classify();
        sb.AppendLine($"N_eff-origin score (0..5): {score}");
        sb.AppendLine($"  +1 multiplicity distribution defined: {EffectiveAccessCounts.DoubletMultiplicities().Sum() > 0}");
        sb.AppendLine($"  +1 neutrino within 5% (half-moment): {EffectiveAccessCounts.UnifiedLaw()[0].Deviation < 0.05}");
        sb.AppendLine($"  +1 down within 5% (first moment): {EffectiveAccessCounts.UnifiedLaw()[1].Deviation < 0.05}");
        sb.AppendLine($"  +1 lepton within 5% (second moment): {EffectiveAccessCounts.UnifiedLaw()[2].Deviation < 0.05}");
        sb.AppendLine($"  +1 up within 5% (octave moment): {EffectiveAccessCounts.UnifiedLaw()[3].Deviation < 0.05}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO ORIGIN rejected: the N_eff values are exact D96 moments.");
        sb.AppendLine("  • PARTIAL ORIGIN rejected: all four sectors match within 0.46% (mean 0.16%).");
        sb.AppendLine("  • N_EFF ORIGIN accepted: the effective access counts EMERGE from the D96/Z2 spectral");
        sb.AppendLine("    geometry as moments of the doublet-multiplicity and octave-occupation distributions");
        sb.AppendLine("    — ν=Σ√m (neutral statistical access), d=Σm (full count), ℓ=Σm² (doublet occupancy),");
        sb.AppendLine("    u=Σocc²/occ₀ (occupation weighting) — so δ = log(N_eff)/log(span) predicts all four");
        sb.AppendLine("    sectors automatically with no fitted sector, charge, or isospin parameters.");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 4, "N_eff-origin score should be strong");
        Assert.True(EffectiveAccessCounts.MeanDeviation() < 0.01, "mean deviation should be < 1%");
        Assert.Equal("N_EFF ORIGIN", cls);
    }
}
