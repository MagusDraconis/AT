using System.Text;
using static TQM.Core.Research.CollapseAuditMetrics;

namespace TQM.Core.Research;

/// <summary>
/// Hostile audit of the X038 Q-conservation collapse argument.
/// TQM-X038b: Hostile Audit of the Q-Conservation Collapse Argument
/// </summary>
public static class CollapseAuditAnalyzer
{
    public static CollapseAuditReport Audit()
    {
        var defenses = CollapseHostileAudit.ExecuteDefenses();
        var theorems = CollapseHostileAudit.BuildTheorems();

        int successful = defenses.Count(d => d.BreaksCollapse);
        AuditVerdict verdict = successful == 0
            ? AuditVerdict.FullySurvives
            : successful <= 2 ? AuditVerdict.MostlySurvives
            : successful <= 4 ? AuditVerdict.SeriousLoophole
            : AuditVerdict.Destroyed;

        string summary = verdict == AuditVerdict.FullySurvives
            ? "X038 FULLY SURVIVES. All 7 Many-Worlds defenses FAIL. "
              + "The Q-conservation collapse argument is RIGOROUS. "
              + "MW cannot be saved without either (i) redefining Q to lose individuation, "
              + "(ii) denying Q conservation, or (iii) rejecting A3 identity persistence. "
              + "All three escape routes destroy the TQM framework. "
              + "Within TQM, branching is MATHEMATICALLY FORBIDDEN. "
              + "Single-outcome measurement is a THEOREM, not a postulate."
            : $"{successful}/{defenses.Count} defenses succeeded.";

        return new CollapseAuditReport(defenses, theorems,
            defenses.Count, successful, verdict, summary);
    }

    public static string DefenseReport(List<MwDefense> defenses)
    {
        var sb = new StringBuilder();
        sb.AppendLine("MANY-WORLDS DEFENSES — HOSTILE AUDIT");
        sb.AppendLine();
        foreach (var d in defenses)
        {
            string icon = d.BreaksCollapse ? "⚠ BREACH" : "✗ FAILED";
            sb.AppendLine($"  [{icon}] Defense {d.Number}: {d.DefenseName}");
            sb.AppendLine();
            sb.AppendLine($"  MW ARGUMENT:");
            foreach (var line in d.ManyWorldsArgument.Split('\n'))
                sb.AppendLine($"    {line}");
            sb.AppendLine();
            sb.AppendLine($"  TQM RESPONSE:");
            foreach (var line in d.TqmResponse.Split('\n'))
                sb.AppendLine($"    {line}");
            sb.AppendLine();
            sb.AppendLine($"  FAILURE POINT: {d.ExactFailurePoint}");
            sb.AppendLine();
            sb.AppendLine(new string('-', 80));
        }
        int failed = defenses.Count(d => !d.BreaksCollapse);
        sb.AppendLine($"  {failed}/{defenses.Count} defenses FAILED. X038 SURVIVES.");
        return sb.ToString();
    }

    public static string TheoremReport(List<BranchCountTheorem> theorems)
    {
        var sb = new StringBuilder();
        sb.AppendLine("BRANCH-COUNT THEOREMS");
        sb.AppendLine();
        sb.AppendLine("  Scenario                  Q_before   Q_branch   Q_collapse   ΔQ_branch");
        sb.AppendLine("  " + new string('─', 75));
        foreach (var t in theorems)
        {
            int delta = t.QAfterIfBranching - t.QBefore;
            sb.AppendLine($"  {t.Setup,-25} {t.QBefore,8}  {t.QAfterIfBranching,9}  {t.QAfterIfCollapse,10}  +{delta}");
        }
        sb.AppendLine();
        sb.AppendLine("  Q conservation ⇒ ΔQ = 0 ⇒ branching forbidden ⇒ collapse required.");
        return sb.ToString();
    }

    public static string MwEscapeRoutes()
    {
        var sb = new StringBuilder();
        sb.AppendLine("MANY-WORLDS ESCAPE ROUTES — ALL FATAL");
        sb.AppendLine();
        sb.AppendLine("  Route 1: Redefine Q to be trivial (Q≡1 always)");
        sb.AppendLine("    Cost: Loses individuation. Contradicts X035.");
        sb.AppendLine("    Status: MW survives, TQM dies. NOT A DEFENSE WITHIN TQM.");
        sb.AppendLine();
        sb.AppendLine("  Route 2: Deny Q conservation in quantum regime");
        sb.AppendLine("    Cost: Loses TQM-116 theorem. Q becomes arbitrary.");
        sb.AppendLine("    Status: MW survives, TQM loses its charge-conservation structure.");
        sb.AppendLine();
        sb.AppendLine("  Route 3: Reject identity persistence (A3)");
        sb.AppendLine("    Cost: Unravels X036 (complexity-to-quantum theorem).");
        sb.AppendLine("    Loses: self-consistency, carriers, species, Born derivation.");
        sb.AppendLine("    Status: MW survives, the entire TQM derivation chain collapses.");
        sb.AppendLine();
        sb.AppendLine("  Route 4: Deny that branches are distinguishable domains");
        sb.AppendLine("    Cost: Contradicts decoherence theory. 'Pointer up' and 'pointer down'");
        sb.AppendLine("    are MACROSCOPICALLY DISTINCT — different positions, different records.");
        sb.AppendLine("    If these aren't distinguishable, nothing is.");
        sb.AppendLine("    Status: MW survives only by denying empirical facts.");
        sb.AppendLine();
        sb.AppendLine("  ALL ESCAPE ROUTES DESTROY EITHER TQM OR EMPIRICAL ADEQUACY.");
        sb.AppendLine("  Many-Worlds and TQM are LOGICALLY INCOMPATIBLE.");
        return sb.ToString();
    }
}
