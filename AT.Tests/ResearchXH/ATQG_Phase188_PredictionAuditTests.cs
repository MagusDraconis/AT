using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 188 — Prediction Audit. Lists all remaining falsifiable predictions (from the physics-coverage
/// single source of truth), classifies each as testable now / soon / inaccessible, and ranks them by scientific
/// impact, feasibility and falsifiability. Deterministic audit — no new physics.
/// </summary>
public class ATQG_Phase188_PredictionAuditTests : ResearchTestBase
{
    public ATQG_Phase188_PredictionAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1880_FullPredictionCatalog()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1880: complete remaining-prediction catalog");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Source of truth: Docs/ATQG_PhysicsCoverage.json (predictions, observables, open questions).");
        sb.AppendLine("  - The catalog mirrors the JSON entries P1-P10 (all remaining falsifiable predictions).");
        sb.AppendLine();

        var all = PredictionAudit.AllPredictions();
        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        foreach (var p in all)
            sb.AppendLine($"  {p.Id}  {p.Name,-46} {p.Phase,-7} {p.Status,-34} {p.Horizon}");
        sb.AppendLine();
        sb.AppendLine($"  total predictions in catalog: {all.Length}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(10, PredictionAudit.Count());
        Assert.True(PredictionAudit.CatalogComplete(), "catalog must be complete (10, all horizons, valid top-10)");
        Assert.Contains(all, p => p.Id == "P1" && p.Name.Contains("106 GeV"));
        Assert.Contains(all, p => p.Id == "P3" && p.Name.Contains("0νββ"));
        Assert.Contains(all, p => p.Id == "P5" && p.Name.Contains("Poisson"));
    }

    [Fact]
    public void ATQG1881_HorizonClassificationAndRanking()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1881: horizon classification (now / soon / inaccessible) and composite ranking");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - NOW: existing data or currently-running experiments (LHC Run 3).");
        sb.AppendLine("  - SOON: next-generation experiment within ~1-2 decades (nEXO, LEGEND-1000, DUNE, FCC-hh).");
        sb.AppendLine("  - INACCESSIBLE: no plausible experiment in the foreseeable future.");
        sb.AppendLine("  - Composite score = impact·3 + feasibility·2 + falsifiability·2 (documented weights).");
        sb.AppendLine();

        var now = PredictionAudit.TestableNow();
        var soon = PredictionAudit.TestableSoon();
        var inacc = PredictionAudit.CurrentlyInaccessible();
        var ranked = PredictionAudit.Ranked();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Testable NOW:         {now.Length}  → " + string.Join(", ", now.Select(p => p.Id)));
        sb.AppendLine($"  Testable SOON:        {soon.Length}  → " + string.Join(", ", soon.Select(p => p.Id)));
        sb.AppendLine($"  Currently INACCESSIBLE: {inacc.Length}  → " + string.Join(", ", inacc.Select(p => p.Id)));
        sb.AppendLine();
        sb.AppendLine("  RANKED (score descending):");
        foreach (var p in ranked)
            sb.AppendLine($"    {p.Score,5:F1}  {p.Id}  {p.Name,-46} (I{p.Impact} F{p.Feasibility} X{p.Falsifiability})");

        Output.WriteLine(sb.ToString());

        Assert.True(PredictionAudit.AllHorizonsPresent(), "all three horizon classes must be non-empty");
        Assert.True(PredictionAudit.Top10Valid(), "Top-10 must be sorted descending by score");
        Assert.True(now.Length >= 2, "at least 2 predictions testable now (P1, P2)");
        Assert.True(inacc.Length >= 3, "at least 3 predictions currently inaccessible");
    }

    [Fact]
    public void ATQG1882_Top10AndRecommendedTarget()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1882: Top-10 predictions and recommended next target");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The recommended next target is the highest-ranked prediction by composite score.");
        sb.AppendLine("  - Scientific impact, feasibility, and falsifiability are scored 1-5 and weighted.");
        sb.AppendLine();

        var top = PredictionAudit.Top10();
        var target = PredictionAudit.RecommendedNextTarget();

        sb.AppendLine("TOP 10 PREDICTIONS:");
        for (int i = 0; i < top.Length; i++)
            sb.AppendLine($"  {i + 1,2}. {top[i].Score,5:F1}  {top[i].Id}  {top[i].Name,-46} {top[i].Horizon}");
        sb.AppendLine();
        sb.AppendLine("RECOMMENDED NEXT TARGET:");
        sb.AppendLine($"  {target.Id} — {target.Name}");
        sb.AppendLine($"  Phase: {target.Phase}   Score: {target.Score:F1}   Horizon: {target.Horizon}");
        sb.AppendLine($"  Note: {target.Note}");
        sb.AppendLine();

        sb.AppendLine("FINAL CONCLUSION:");
        sb.AppendLine("  The 106 GeV resonance (QG132) is the top-ranked remaining falsifiable prediction:");
        sb.AppendLine("  highest impact (new scalar sector), feasible with existing LHC Run 3 data, and sharply");
        sb.AppendLine("  falsifiable (search window 99-114 GeV, 9 ladder rungs, 15.2/20.3 GeV decay quanta).");
        sb.AppendLine("  The 0νββ rate (m_ββ = 2.02e-3 eV, QG179) is the top SOON target (nEXO/LEGEND-1000).");

        Output.WriteLine(sb.ToString());

        Assert.Equal(10, top.Length);
        Assert.True(PredictionAudit.RecommendedIs106GeV(),
            "the 106 GeV resonance must be the recommended next target");
        Assert.Equal("P1", target.Id);
    }
}
