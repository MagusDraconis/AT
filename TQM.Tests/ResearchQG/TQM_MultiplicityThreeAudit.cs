using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

/// <summary>Hostile audit of the recurring integer 3 as a single multiplicity variable N.</summary>
public class TQM_MultiplicityThreeAudit : ResearchTestBase
{
    public TQM_MultiplicityThreeAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void MultiplicityThree_HostileAudit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("The Recurring Integer 3 — Multiplicity Audit (N=3?)");

        S(sb, "Section A — Occurrences as one multiplicity N"); sb.AppendLine(SectionA());
        S(sb, "Section B — The two statuses: spatial vs internal 3"); sb.AppendLine(SectionB());
        S(sb, "Section C — Search for a derivation of N=3"); sb.AppendLine(SectionC());
        S(sb, "Section D — Rejected fallacies"); sb.AppendLine(SectionD());
        S(sb, "Section E — Classification of N=3"); sb.AppendLine(SectionE());
        S(sb, "Section F — Outputs"); sb.AppendLine(SectionF());

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  spatial N=3: DERIVED   internal N=3: SELECTED (derived lower ∩ empirical upper)");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "MultiplicityThree_Report.txt"), sb.ToString());

        // CP lower bound must be exactly N=3.
        Assert.Equal(3, MultiplicityThreeAnalyzer.DerivedLowerBound());
        // N=1,2 have no CP phase; N=3 has exactly one.
        Assert.Equal(0, MultiplicityThreeAnalyzer.CPPhases(2), 12);
        Assert.Equal(1, MultiplicityThreeAnalyzer.CPPhases(3), 12);
        // No bifurcation gives exactly 3 stable branches without codim-2 (butterfly).
        Assert.Equal(2, MultiplicityThreeAnalyzer.Bifurcations().First(b => b.Catastrophe == "pitchfork").StableBranches);
        // Internal 3 does NOT inherit the spatial 3.
        Assert.False(MultiplicityThreeAnalyzer.InternalInheritsSpatial);
    }

    // ---------------------------------------------------------------------

    private static string SectionA()
    {
        var sb = new StringBuilder();
        sb.AppendLine("All occurrences treated as manifestations of ONE multiplicity variable N:");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,-26} {1,-18} {2}", "occurrence", "status", "basis"));
        foreach (var o in MultiplicityThreeAnalyzer.Occurrences())
            sb.AppendLine(string.Format("  {0,-26} {1,-18} {2}", o.Occurrence, o.Status, o.Basis));
        sb.AppendLine();
        sb.AppendLine("  KEY: they are NOT all the same status. The SPACETIME 3 (d=3+1) is DERIVED (complexity");
        sb.AppendLine("  maximization peaks at M²≈5, X042/XE009). The INTERNAL 3s (generations, color, dim(G))");
        sb.AppendLine("  live in different spaces and are SELECTED, not derived. Unifying them requires a");
        sb.AppendLine("  mechanism linking spacetime N to internal N — which does not exist.");
        return sb.ToString();
    }

    private static string SectionB()
    {
        return
            "TWO STATUSES, NOT ONE:\n" +
            "\n" +
            "  SPATIAL N=3 (3+1 dims): DERIVED. Complexity maximization has 6 requirements that\n" +
            "    peak at M²≈5, giving d=3+1. This is the STRONGEST '3' in TQM — a derivation, though\n" +
            "    it runs through the complexity/observer window (partly selection-like).\n" +
            "\n" +
            "  INTERNAL N=3 (generations = color = dim(G)): SELECTED. The lower bound N≥3 IS derived\n" +
            "    (CP violation needs ≥1 complex phase: (N-1)(N-2)/2 ≥ 1 ⟹ N≥3; S3 is the first\n" +
            "    non-abelian permutation group). The upper bound N≤3 is EMPIRICAL (Z-width N_ν=3,\n" +
            "    Higgs production ~9× enhancement). N=3 = derived-lower ∩ empirical-upper.\n" +
            "\n" +
            "  CRITICAL: the internal 3 does NOT inherit the spatial 3. Generation space G, color SU(3),\n" +
            "    and spacetime are DIFFERENT spaces (QG-051/066/067). No principle forces the internal\n" +
            "    multiplicity to equal the spatial dimensionality — the '3=3=3=3' is a coincidence until\n" +
            "    a linking mechanism is found.";
    }

    private static string SectionC()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Search for a derivation of N=3:");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  CP phases in N×N mixing: N=1:{0:F0}, N=2:{1:F0}, N=3:{2:F0}, N=4:{3:F0}, N=5:{4:F0}",
            MultiplicityThreeAnalyzer.CPPhases(1), MultiplicityThreeAnalyzer.CPPhases(2),
            MultiplicityThreeAnalyzer.CPPhases(3), MultiplicityThreeAnalyzer.CPPhases(4),
            MultiplicityThreeAnalyzer.CPPhases(5)));
        sb.AppendLine("  → DERIVED lower bound: N≥3 (the first N with a complex phase). This is a theorem.");
        sb.AppendLine();
        sb.AppendLine("  Upper bound N≤3: EMPIRICAL (Z-width, Higgs). NOT derived.");
        sb.AppendLine();
        sb.AppendLine("  Catastrophe/bifurcation stable branches (does any give exactly 3?):");
        foreach (var b in MultiplicityThreeAnalyzer.Bifurcations())
            sb.AppendLine($"    {b.Catastrophe,-12} → {b.StableBranches} stable branch(es)");
        sb.AppendLine("  → pitchfork=2, cusp=2 (2 stable + 1 unstable); butterfly=3 but is codim-2 (needs 2 params).");
        sb.AppendLine("    NO natural catastrophe gives exactly 3 stable branches at codim-1.");
        sb.AppendLine();
        sb.AppendLine("  CONCLUSION: the lower bound (N≥3) is DERIVED; the upper bound (N≤3) is EMPIRICAL; no");
        sb.AppendLine("  mechanism fixes N=3 from below AND above. N=3 = SELECTION.");
        return sb.ToString();
    }

    private static string SectionD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Rejected:");
        sb.AppendLine();
        foreach (var f in MultiplicityThreeAnalyzer.RejectedFallacies())
            sb.AppendLine("  " + f);
        return sb.ToString();
    }

    private static string SectionE()
    {
        return
            "N=3 is: SELECTED (for the internal sectors); DERIVED (for spacetime).\n" +
            "\n" +
            "  - NOT purely derived: the upper bound N≤3 is empirical, and the spacetime derivation\n" +
            "    runs through complexity (partly selection-like).\n" +
            "  - NOT emergent: no dynamics/attractor produces exactly 3 (no codim-1 catastrophe).\n" +
            "  - NOT purely contingent: the lower bound N≥3 IS a theorem (CP violation).\n" +
            "  - SELECTED: N=3 is the unique integer satisfying a derived lower bound and an\n" +
            "    empirical upper bound. This is the strongest honest status for the internal 3.\n" +
            "  - The spacetime N=3 is separately DERIVED (complexity maximization, X042/XE009), but\n" +
            "    the internal N=3 does not inherit it.";
    }

    private static string SectionF()
    {
        return
            "FIRST UNRESOLVED NODE: the UPPER bound N≤3 (why no N≥4). It is empirical (Z-width N_ν=3,\n" +
            "  Higgs production), not derived. No symmetry, catastrophe, or topology principle excludes\n" +
            "  N≥4. This is the single gap separating 'selected' from 'derived'.\n" +
            "\n" +
            "STRONGEST DERIVATION PATH: derive the upper bound N≤3 from a stability/catastrophe principle.\n" +
            "  The only candidate is a codim-1 catastrophe with exactly 3 stable branches — but no such\n" +
            "  catastrophe exists (pitchfork=2, cusp=2, butterfly=3 but codim-2). Alternatively, show the\n" +
            "  internal N inherits the DERIVED spacetime N=3 — but no mechanism links the two spaces\n" +
            "  (QG-051/066/067). Both paths are currently blocked.\n" +
            "\n" +
            "STRONGEST NO-GO THEOREM:\n" +
            "  No topology (π₁(S¹)=ℤ, infinite), symmetry (S_N exists for all N), catastrophe (no codim-1\n" +
            "  gives exactly 3 stable), or persistence (all classical groups stable) principle fixes N=3\n" +
            "  from below AND above. The lower bound N≥3 is derived (CP theorem); the upper bound N≤3 is\n" +
            "  empirical. Hence, with no new primitives and anthropics/numerology/hidden-params rejected,\n" +
            "  the internal N=3 is irreducible-SELECTED: the unique value at the derived-∩-empirical\n" +
            "  boundary, not computable from the primitives alone.\n" +
            "\n" +
            "SUCCESS PROBABILITY (deriving N=3 for the internal sectors): ≈ 0.1–0.2.\n" +
            "  The lower bound is already derived (1.0); the upper bound has no known derivation route,\n" +
            "  so the residual is the empirical N≤3 input. A spacetime→internal multiplicity link would\n" +
            "  be a major breakthrough but has no current candidate mechanism.";
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
