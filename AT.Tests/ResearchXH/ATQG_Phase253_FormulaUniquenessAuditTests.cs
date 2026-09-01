using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 253 — Formula Uniqueness Audit. Replace empirical formula choice with a derivation-choice
/// rule: generate all dimensionless D96 combinations, search the minimal-complexity expression per
/// observable, and determine whether the published formula is the simplest. Methodology only.
/// </summary>
public class ATQG_Phase253_FormulaUniquenessAuditTests : ResearchTestBase
{
    public ATQG_Phase253_FormulaUniquenessAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2530_GenerateAndMatch()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2530: the candidate pool and the search");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - D96 quantities: Σm, #d, #g, span, λ₂, occ₀, occ₁, occ₃, occMom, Σ√m;");
        sb.AppendLine("  - Candidate forms: q, q², q³, √q, 1/q, ln q, affine differences, products/ratios,");
        sb.AppendLine("    triples, 1/(affine), with a small constant multiplier set;");
        sb.AppendLine("  - Complexity = distinct quantities + operators + (1 if a non-trivial constant).");
        sb.AppendLine();

        sb.AppendLine($"Candidate pool size: {FormulaUniquenessAudit.Pool.Length}");
        sb.AppendLine($"Observables audited: {FormulaUniquenessAudit.Observables().Length}");
        sb.AppendLine();
        sb.AppendLine($"SUMMARY: {FormulaUniquenessAudit.Summary()}");
        sb.AppendLine($"Any UNIQUE? {FormulaUniquenessAudit.AnyUnique()}");
        sb.AppendLine($"By classification: {string.Join(", ", FormulaUniquenessAudit.ClassificationCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");

        Output.WriteLine(sb.ToString());

        Assert.True(FormulaUniquenessAudit.Pool.Length > 100_000, "the candidate pool must be large");
        Assert.Equal(7, FormulaUniquenessAudit.Observables().Length);
        Assert.True(FormulaUniquenessAudit.AnyUnique(), "r₃₁ = span/√3 is the unique minimal-complexity expression");
        Assert.Equal(1, FormulaUniquenessAudit.ClassificationCounts()["UNIQUE"]);
    }

    [Fact]
    public void ATQG2531_PerObservableAnalyses()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2531: per-observable uniqueness analysis");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - UNIQUE: published is the only match at its complexity (no simpler, no tie);");
        sb.AppendLine("  - NON-UNIQUE: published ties at the same complexity (no simpler match);");
        sb.AppendLine("  - MULTIPLE MATCHES: a strictly simpler expression reproduces the target.");
        sb.AppendLine();

        foreach (var a in FormulaUniquenessAudit.AllAnalyses())
        {
            sb.AppendLine($"  {a.Obs.Name,-8} target {a.Obs.Target,-8} | published [{a.Obs.PublishedFormula}] (c={a.Obs.PublishedComplexity}, dev {a.Obs.PublishedDeviation * 100:F3}%)");
            sb.AppendLine($"      min complexity {a.MinComplexity} with {a.MatchesAtMin} match(es); simpler exists: {a.SimplerExists}");
            sb.AppendLine($"      CLASSIFICATION: {a.Classification}");
            foreach (var (n, d, c) in a.TopMatches.Take(3))
                sb.AppendLine($"        c={c} dev={d * 100:F3}%  {n}");
            sb.AppendLine();
        }

        Output.WriteLine(sb.ToString());

        var analyses = FormulaUniquenessAudit.AllAnalyses();
        Assert.All(analyses, a => Assert.NotEqual("NO MATCH", a.Classification));
        Assert.Contains(analyses, a => a.Classification == "UNIQUE");     // r₃₁ = span/√3
        Assert.Contains(analyses, a => a.Classification == "NON-UNIQUE"); // m_μ/me, m_τ/m_μ
        Assert.Contains(analyses, a => a.Classification == "MULTIPLE MATCHES"); // 1−n_s, r₂₁, m₂/m₃, y_t/y_b
    }

    [Fact]
    public void ATQG2532_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2532: the honest verdict");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - A blind minimal-complexity search over ALL dimensionless D96 combinations is the");
        sb.AppendLine("    derivation-choice rule that replaces empirical formula choice.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {FormulaUniquenessAudit.Summary()}");
        sb.AppendLine($"Classification counts: {string.Join(", ", FormulaUniquenessAudit.ClassificationCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");
        sb.AppendLine();
        sb.AppendLine("VERDICT:");
        sb.AppendLine("  - Only ONE audited formula is UNIQUE (r₃₁ = span/√3 — the sole minimal-complexity match);");
        sb.AppendLine("  - TWO are NON-UNIQUE (m_μ/me, m_τ/m_μ — the published form ties with alternatives at the");
        sb.AppendLine("    same complexity: #g²/√occ₃ for m_μ/me, √3·√Σm / √#d/λ₂ for m_τ/m_μ);");
        sb.AppendLine("  - FOUR are MULTIPLE MATCHES (1−n_s, r₂₁, m₂/m₃, y_t/y_b — a STRICTLY SIMPLER expression");
        sb.AppendLine("    reproduces the target: 1/(span·ln occ₃) for 1−n_s, √Σm/occ₀ for r₂₁, 1/(occ₀√2) for");
        sb.AppendLine("    m₂/m₃, occ₀²/λ₂ for y_t/y_b);");
        sb.AppendLine("  - The published formulas are therefore mostly NOT forced by a minimal-complexity");
        sb.AppendLine("    derivation-choice rule — the choice was target-informed, confirming QG239/QG250's");
        sb.AppendLine("    RETRO-SELECTION RISK for all but r₃₁.");

        Output.WriteLine(sb.ToString());

        Assert.True(FormulaUniquenessAudit.AnyUnique(), "r₃₁ must be UNIQUE");
        var counts = FormulaUniquenessAudit.ClassificationCounts();
        Assert.Equal(1, counts["UNIQUE"]);
        Assert.Equal(2, counts["NON-UNIQUE"]);
        Assert.Equal(4, counts["MULTIPLE MATCHES"]);
    }
}
