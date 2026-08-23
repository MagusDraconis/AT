using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 258 — Blind Formula Tournament. Test whether the QG254/QG255 D96 selection rules have
/// predictive power when the target is revealed only after the selection is locked.
/// </summary>
public class TQMQG_Phase258_BlindFormulaTournamentTests : ResearchTestBase
{
    public TQMQG_Phase258_BlindFormulaTournamentTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2580_BlindSelection()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2580: the blind selection (locked before reveal)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Input: D96 quantities only; no observable or target value enters selection;");
        sb.AppendLine("  - Pool: all expressions up to complexity 6, restricted to ratio-form (all seven");
        sb.AppendLine("    observables are dimensionless ratios);");
        sb.AppendLine("  - Rules: QG254 octave preservation, then QG255 moment-closure MDL.");
        sb.AppendLine();

        var pool = BlindFormulaTournament.Pool();
        string top = BlindFormulaTournament.SelectTopFormula(pool);
        sb.AppendLine($"Pool size (complexity ≤ {BlindFormulaTournament.MaxComplexity}): {pool.Length}");
        sb.AppendLine($"Blind top formula (locked): {top}");
        sb.AppendLine();

        sb.AppendLine("THE OBSERVABLES (revealed after locking):");
        foreach (var o in BlindFormulaTournament.Observables())
            sb.AppendLine($"  {o.Name,-10} target {o.Target,-8} published [{o.PublishedFormula}] (c={o.PublishedComplexity})");

        Output.WriteLine(sb.ToString());

        Assert.True(pool.Length > 10_000, "the pool must be large");
        Assert.Equal(7, BlindFormulaTournament.Observables().Length);
        // The blind selection is a single locked formula (the same for every observable).
        Assert.NotEmpty(top);
    }

    [Fact]
    public void TQMQG2581_TournamentRun()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2581: the locked-then-revealed tournament");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Selection is locked with no target; targets are revealed only afterward;");
        sb.AppendLine("  - Success = locked formula reproduces the target within 1%.");
        sb.AppendLine();

        foreach (var r in BlindFormulaTournament.RunAll())
            sb.AppendLine($"  {r.Observable,-10} selected [{r.Selected}] = {r.SelectedValue:F6} | target {r.Target:F4} | dev {r.Deviation * 100:F2}% | {(r.Success ? "HIT" : "MISS")}");

        Output.WriteLine(sb.ToString());

        var results = BlindFormulaTournament.RunAll();
        Assert.Equal(7, results.Length);
        // The selection is degenerate: the same formula is selected for every observable.
        var selected = results.Select(r => r.Selected).Distinct().ToList();
        Assert.True(selected.Count == 1, "the target-free rule chain selects the same formula for every observable");
    }

    [Fact]
    public void TQMQG2582_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2582: the honest verdict — WEAK");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - WEAK < 30%, MODERATE 30-59%, STRONG 60-84%, PREDICTIVE ≥ 85%.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {BlindFormulaTournament.Summary()}");
        sb.AppendLine($"Success rate: {BlindFormulaTournament.SuccessRate():P0}");
        sb.AppendLine($"CLASSIFICATION = {BlindFormulaTournament.Classify()}");
        sb.AppendLine();
        sb.AppendLine("VERDICT:");
        sb.AppendLine("  - The target-free rule chain selects the SAME formula (the globally minimal-");
        sb.AppendLine("    complexity octave-preserving ratio) for every observable;");
        sb.AppendLine("  - that locked formula matches NONE of the seven revealed targets (0/7);");
        sb.AppendLine("  - the QG254/QG255 rules have NO blind predictive power — a formula cannot be");
        sb.AppendLine("    selected for a specific observable without reference to what it is;");
        sb.AppendLine("  - this confirms QG256/QG257: the rules are a heuristic narrowing that only");
        sb.AppendLine("    'works' when the pool is pre-restricted by the target (as in QG253), not a");
        sb.AppendLine("    predictive selection principle.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("WEAK", BlindFormulaTournament.Classify());
        Assert.True(BlindFormulaTournament.SuccessRate() < 0.30, "the blind success rate is below 30%");
        Assert.Contains("WEAK", BlindFormulaTournament.Summary());
    }
}
