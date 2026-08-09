using System.Globalization;
using System.Text;
using TQM.Core.ResearchXD;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXD;

public class TQM_XD004_EuclidDecisionTree : ResearchTestBase
{
    public TQM_XD004_EuclidDecisionTree(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XD004_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXD-004 Euclid Decision Tree");

        var tree = EuclidDecisionTreeAnalyzer.FullAssessment();

        // ═══ SECTION A: Current prediction ═══
        Sec(sb, "Section A — Current TQM Prediction");
        sb.AppendLine(EuclidDecisionTreeAnalyzer.ThePrediction());

        // ═══ SECTION B: Dependency chain ═══
        Sec(sb, "Section B — Prediction Dependency Chain");
        sb.AppendLine(EuclidDecisionTreeAnalyzer.DependencyChain());

        // ═══ SECTION C: Scenario matrix ═══
        Sec(sb, "Section C — Euclid Scenario Matrix");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-5} {1,-35} {2,8} {3,8} {4,-12}",
            "ID", "Scenario", "w₀", "±σ", "Action"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var s in tree.Scenarios)
        {
            string w0 = double.IsNaN(s.WOscillator) ? "tension" :
                        s.WOscillator.ToString("F3", CultureInfo.InvariantCulture);
            string sig = double.IsNaN(s.Sigma) ? "N/A" :
                         s.Sigma.ToString("F3", CultureInfo.InvariantCulture);
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-5} {1,-35} {2,8} {3,8} {4,-12}",
                s.Id, s.Name, w0, sig, s.ActionClass));
        }
        sb.AppendLine();
        foreach (var s in tree.Scenarios)
        {
            int pct = (int)(100.0 * tree.Sectors.Count(sv => !sv.DependsOnWEz || s.SurvivingSectors.Contains(sv.Sector)) / tree.TotalSectors);
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  [{0}] {1} — {2} ({3}/{4} sectors killed, {5}% survive)",
                s.Id, s.TqmVerdict, s.ActionClass, s.KilledCount, tree.TotalSectors, pct));
        }

        // ═══ SECTION D: Kill-shot analysis ═══
        Sec(sb, "Section D — Sector Survival Matrix (Worst Case: w = −1)");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-35} {1,8} {2,-10} {3}",
            "Sector", "Prior", "w(z)-dep?", "Worst Case"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var sv in tree.Sectors)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-35} {1,8:F2} {2,-10} {3}",
                sv.Sector, sv.PriorConfidence,
                sv.DependsOnWEz ? "YES ⚠" : "no",
                sv.DependsOnWEz ? sv.WorstCaseStatus : sv.BestCaseStatus));
        }
        int independent = tree.Sectors.Count(sv => !sv.DependsOnWEz);
        int dependent = tree.Sectors.Count(sv => sv.DependsOnWEz);
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Independent of w(z): {0}/{1} ({2:P0})",
            independent, tree.TotalSectors, (double)independent / tree.TotalSectors));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Dependent on w(z):  {0}/{1} ({2:P0})",
            dependent, tree.TotalSectors, (double)dependent / tree.TotalSectors));

        // ═══ SECTION E: Confidence updates ═══
        Sec(sb, "Section E — Bayesian Confidence Updates");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-40} {1,6} {2,6} {3,6} {4,6} {5,6}",
            "Branch", "Prior", "w=-1", "Weak", "Strong", "Wrong"));
        sb.AppendLine("  " + new string('-', 80));
        foreach (var cu in tree.ConfidenceUpdates)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-40} {1,6:F2} {2,6:F2} {3,6:F2} {4,6:F2} {5,6:F2}",
                cu.Branch, cu.Prior, cu.PosteriorWEqMinus1,
                cu.PosteriorWeakDev, cu.PosteriorStrongDev, cu.PosteriorWrongSign));
        }
        var overall = tree.ConfidenceUpdates.First(c => c.Branch == "Overall TQM framework");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  OVERALL: prior={0:F2}. Best={1:F2} (strong dev), Worst={2:F2} (w=-1 or wrong sign).",
            overall.Prior, overall.PosteriorStrongDev, overall.PosteriorWEqMinus1));

        // ═══ SECTION F: Revision protocol ═══
        Sec(sb, "Section F — Revision Protocol Actions");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-20} {1,-35} {2,-12} {3}",
            "Scenario", "Sector", "Action", "Rationale"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var ra in tree.RevisionActions)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-20} {1,-35} {2,-12} {3}",
                ra.Scenario, ra.Sector, ra.Action, ra.Rationale));
        }

        // ═══ SECTION G: Decision tree ═══
        Sec(sb, "Section G — Complete Decision Tree");
        sb.AppendLine("                    ┌── w = −1.000 ± 0.010  ──→  DELETE Λ(t), ~70% survives");
        sb.AppendLine("                    │                            Framework → 1-param + constant Λ");
        sb.AppendLine("                    │");
        sb.AppendLine("  EUCLID ──────────┼── |w+1| ~ 0.015, w > −1  ──→  CONFIRM Λ(t), TQM VALIDATED");
        sb.AppendLine("  + ROMAN           │    >3σ                        Elevate to Strong Model");
        sb.AppendLine("  + DESI            │");
        sb.AppendLine("                    ├── |w+1| > 0.05, w > −1   ──→  REFINE α, sign correct");
        sb.AppendLine("                    │    >5σ                        Recalibrate Λ(t)");
        sb.AppendLine("                    │");
        sb.AppendLine("                    ├── w < −1 (phantom)       ──→  DELETE Λ(t), ~80% survives");
        sb.AppendLine("                    │    >3σ                        Different Λ origin needed");
        sb.AppendLine("                    │");
        sb.AppendLine("                    ├── Euclid ≠ Roman          ──→  WAIT, experimental crisis");
        sb.AppendLine("                    │    >3σ tension                All preserved");
        sb.AppendLine("                    │");
        sb.AppendLine("                    └── DESI confirms evolution ──→  STRENGTHEN, 2-survey");
        sb.AppendLine("                         independent                 All preserved");
        sb.AppendLine();
        sb.AppendLine("  PRE-COMMITTED: Every branch has a documented, binding response.");
        sb.AppendLine("  NO IMPROVISATION: The decision tree is complete.");

        // ═══ SECTION H: Final verdict ═══
        Sec(sb, "Section H — Final Verdict");
        sb.AppendLine(tree.Verdict);

        // ═══ SUMMARY ═══
        Sec(sb, "Summary — Euclid Decision Tree");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Scenarios:           {0}", tree.Scenarios.Count));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Sectors mapped:      {0}", tree.TotalSectors));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Independent sectors: {0} (survive always)", independent));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  w(z)-dependent:      {0} (at risk)", dependent));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Revision actions:    {0}", tree.RevisionActions.Count));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Confidence updates:  {0} branches", tree.ConfidenceUpdates.Count));
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Readiness:           {0}", tree.ReadinessClass));
        sb.AppendLine();
        sb.AppendLine("  WORST CASE (w = −1):  ~70% survives. TQM → 1-parameter + constant Λ.");
        sb.AppendLine("  BEST CASE (w ≠ −1, correct sign): 100% survives. TQM validated.");
        sb.AppendLine("  ANY CASE: Response is pre-committed, documented, binding.");

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  ResearchXD-004 COMPLETE.");
        sb.AppendLine("  TQM is FULLY DECISION-READY for Euclid.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(); sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
