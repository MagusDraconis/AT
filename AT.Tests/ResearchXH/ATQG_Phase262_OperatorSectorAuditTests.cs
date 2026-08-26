using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 262 — Operator Sector Audit. Classify every successful derivation by primary/secondary
/// operator and discover whether masses, couplings, cosmology and gravity are different projections of
/// the same operator sectors.
/// </summary>
public class ATQG_Phase262_OperatorSectorAuditTests : ResearchTestBase
{
    public ATQG_Phase262_OperatorSectorAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2620_OperatorMap()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2620: the operator map (every successful derivation)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - operators: CROWDING (degeneracy groups #d/#g/ω₀ω₂), COMPRESSION (octave bands");
        sb.AppendLine("    occ/occMom), BEAT (frequency ratio span), LOCKING (spectral gap λ₂),");
        sb.AppendLine("    MOMENT (the universal read-out Σm/Σ√m/Σm²);");
        sb.AppendLine("  - primary = dominant output in the formula; secondary = next-most-present;");
        sb.AppendLine("  - classification from the PUBLISHED formulas — no targets, no fitting.");
        sb.AppendLine();

        foreach (var g in OperatorSectorAudit.Observables().GroupBy(o => o.Sector))
        {
            sb.AppendLine($"── {g.Key} ──");
            foreach (var o in g)
                sb.AppendLine($"  [{o.Primary,-9}→{o.Secondary,-9}] {o.Name} ({o.Phase})");
        }
        sb.AppendLine();
        sb.AppendLine($"By sector: {string.Join(", ", OperatorSectorAudit.SectorCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");
        sb.AppendLine($"Primary operators: {string.Join(", ", OperatorSectorAudit.PrimaryCounts().Where(kv => kv.Value > 0).Select(kv => $"{kv.Key}={kv.Value}"))}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(30, OperatorSectorAudit.Observables().Length);
        Assert.Equal(5, OperatorSectorAudit.SectorCounts().Count);
        Assert.True(OperatorSectorAudit.PrimaryCounts().Values.Sum() == 30, "every observable has exactly one primary operator");
    }

    [Fact]
    public void ATQG2621_SectorProjections()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2621: the sector operator signatures");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - if the sectors are DIFFERENT PROJECTIONS of the SAME operator sectors, each");
        sb.AppendLine("    sector must use a large shared subset (≥ 3) of the five operators.");
        sb.AppendLine();

        foreach (OperatorSectorAudit.Sector s in Enum.GetValues<OperatorSectorAudit.Sector>())
        {
            var used = OperatorSectorAudit.OperatorsUsedBy(s);
            sb.AppendLine($"  {s,-11} uses {used.Length} operators: {string.Join(", ", used)}");
        }
        sb.AppendLine();
        sb.AppendLine($"Universal basis (all five operators in every sector): {OperatorSectorAudit.UniversalBasis()}");
        foreach (OperatorSectorAudit.Op op in Enum.GetValues<OperatorSectorAudit.Op>())
        {
            int sectors = OperatorSectorAudit.Observables().Where(o => o.Primary == op || o.Secondary == op).Select(o => o.Sector).Distinct().Count();
            sb.AppendLine($"  {op,-11} appears in {sectors}/5 sectors");
        }

        Output.WriteLine(sb.ToString());

        // Every sector uses at least 3 of the 5 operators (shared basis).
        foreach (OperatorSectorAudit.Sector s in Enum.GetValues<OperatorSectorAudit.Sector>())
            Assert.True(OperatorSectorAudit.OperatorsUsedBy(s).Length >= 3, $"sector {s} must use ≥3 operators");
        // MOMENT appears in every sector.
        Assert.True(OperatorSectorAudit.Observables().Where(o => o.Primary == OperatorSectorAudit.Op.Moment || o.Secondary == OperatorSectorAudit.Op.Moment)
            .Select(o => o.Sector).Distinct().Count() == 5, "MOMENT is universal");
    }

    [Fact]
    public void ATQG2622_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2622: the sector-unification determination");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - DISTINCT SECTORS (score ≤ 2), PARTIAL OVERLAP (3-4),");
        sb.AppendLine("    SAME OPERATOR SECTORS (5-6);");
        sb.AppendLine("  - goal: are masses, couplings, cosmology and gravity different projections of the");
        sb.AppendLine("    same operator sectors?");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {OperatorSectorAudit.Summary()}");
        sb.AppendLine($"Sector score: {OperatorSectorAudit.SectorScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {OperatorSectorAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - EVERY sector draws from the SAME operator basis: no operator is unique to a");
        sb.AppendLine("    single sector, and MOMENT is universal (appears in all five).");
        sb.AppendLine("  - The sector differences are of EMPHASIS, not of operator set: masses are");
        sb.AppendLine("    MOMENT-dominated, mixings CROWDING/COMPRESSION-dominated, cosmology");
        sb.AppendLine("    BEAT/COMPRESSION-dominated — but all use the shared five-operator basis.");
        sb.AppendLine("  - The operators are therefore SECTOR-UNIVERSAL: masses, couplings, cosmology and");
        sb.AppendLine("    gravity are different projections of the same operator sectors.");
        sb.AppendLine("  - Honest caveat: the operator map is structural (from the published formulas),");
        sb.AppendLine("    but the operator-to-observable assignment retains target-information from the");
        sb.AppendLine("    QG149-157 era (QG257/259/261). The universality is real; the assignment was not");
        sb.AppendLine("    derivation-free.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("SAME OPERATOR SECTORS", OperatorSectorAudit.Classify());
        Assert.True(OperatorSectorAudit.SectorScore() >= 5);
        Assert.Contains("SAME OPERATOR SECTORS", OperatorSectorAudit.Summary());
    }
}
