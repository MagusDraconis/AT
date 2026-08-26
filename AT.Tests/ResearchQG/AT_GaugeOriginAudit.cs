using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;

/// <summary>Hostile audit: why SU(3)×SU(2)×U(1)?</summary>
public class AT_GaugeOriginAudit : ResearchTestBase
{
    public AT_GaugeOriginAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void GaugeOrigin_HostileAudit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("Gauge Origin — Hostile Audit (why SU(3)×SU(2)×U(1)?)");

        S(sb, "Section A — Existing derivations mapped"); sb.AppendLine(SectionA());
        S(sb, "Section B — First non-derived node"); sb.AppendLine(SectionB());
        S(sb, "Section C — Four derivation routes"); sb.AppendLine(SectionC());
        S(sb, "Section D — Six rejected fallacies"); sb.AppendLine(SectionD());
        S(sb, "Section E — Subgroup classification"); sb.AppendLine(SectionE());
        S(sb, "Section F — Outputs (value, success, no-go, path)"); sb.AppendLine(SectionF());

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  U(1): DERIVED   SU(2): EMERGENT   SU(3): CONTINGENT");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "GaugeOrigin_Report.txt"), sb.ToString());

        Assert.True(GaugeOriginAnalyzer.Subgroups().Length == 3);
        Assert.True(GaugeOriginAnalyzer.Classification().Length == 3);
        Assert.True(GaugeOriginAnalyzer.RejectedFallacies().Length == 6);
        Assert.True(GaugeOriginAnalyzer.DerivationRoutes().Length == 4);
    }

    // ---------------------------------------------------------------------

    private static string SectionA()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Existing derivations (from QG-038, X048–X056, X060e):");
        sb.AppendLine();
        sb.AppendLine("  U(1):  Phase θ lives on S¹ (R complex ⇒ phase, X036). S¹'s isometry group IS U(1).");
        sb.AppendLine("         ∮∇θ·dl = 2πn ⇒ integer topological charge ⇒ U(1) gauge. Photon = n=0 phase wave.");
        sb.AppendLine("         CLEANEST RESULT — not chosen, it is the symmetry of phase itself.");
        sb.AppendLine();
        sb.AppendLine("  SU(2): Binary winding n↔−n = Z₂; spinor double-cover of SO(3) = SU(2). Weak doublets");
        sb.AppendLine("         map to binary winding pairs. BUT the lift from discrete Z₂ to continuous SU(2)");
        sb.AppendLine("         was left as PARTIAL (QG-038).");
        sb.AppendLine();
        sb.AppendLine("  SU(3): Tri-winding confinement (n=3). Color triplets ↔ 3 bound vortex substructures.");
        sb.AppendLine("         The number 3 matches; the full SU(3) algebra (8 gluons) NOT derived; confinement");
        sb.AppendLine("         borrowed from QCD.");
        sb.AppendLine();
        sb.AppendLine("  Pattern: rank(U(1))=1, rank(SU(2))=2, rank(SU(3))=3 matches winding sectors n=1,2,3 —");
        sb.AppendLine("         'intriguing but possibly numerology' (no mechanism links them).");
        return sb.ToString();
    }

    private static string SectionB()
    {
        return
            "FIRST NON-DERIVED NODE: the DEFECT COUNT per sector (n=1, 2, 3).\n" +
            "\n" +
            "The group STRUCTURE of each factor is derivable/emergent from the defect-moduli\n" +
            "automorphism (Aut of the n-defect moduli space ⊇ SU(n)): U(1)=Aut(S¹), SU(2)=double\n" +
            "cover of SO(3), SU(3)⊂Aut(C³/S₃). But the COUNT n (why 1 EM + 2 weak + 3 strong) is NOT\n" +
            "fixed by anything in the chain. Topology gives π₁(S¹)=ℤ (INFINITE winding, no specific n);\n" +
            "the attractor landscape content is contingent (flavor audit); S_n only permutes (doesn't\n" +
            "fix n); persistence gives no preference. The '1-2-3' count is the SAME underived '3' that\n" +
            "appears as 3 generations, 3 spatial dims, dim(G)=3 (QG-067: SELECTED, not derived).";
    }

    private static string SectionC()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Four searches (each evaluated against the no-new-primitives constraint):");
        sb.AppendLine();
        foreach (var r in GaugeOriginAnalyzer.DerivationRoutes())
        {
            sb.AppendLine($"  {r.Route}: {r.Result}");
            sb.AppendLine($"      → {r.Verdict}");
            sb.AppendLine();
        }
        sb.AppendLine("RESULT: only the defect-moduli route derives the group STRUCTURE; NO route fixes the");
        sb.AppendLine("defect COUNT n. The count is the residual irreducible-contingent input.");
        return sb.ToString();
    }

    private static string SectionD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Rejected (per the audit rules):");
        sb.AppendLine();
        foreach (var f in GaugeOriginAnalyzer.RejectedFallacies())
            sb.AppendLine("  " + f);
        return sb.ToString();
    }

    private static string SectionE()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Classification of each subgroup (Derived / Selected / Emergent / Contingent / Assumed):");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,-8} {1,-14} {2,-6}", "group", "classification", "success"));
        foreach (var c in GaugeOriginAnalyzer.Classification())
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-8} {1,-14} {2,6:P0}", c.Group, c.Classification, c.Success));
        sb.AppendLine();
        sb.AppendLine("  U(1)   : DERIVED   — a theorem (Aut(S¹)=U(1), π₁(S¹)=ℤ). No assumption.");
        sb.AppendLine("  SU(2)  : EMERGENT  — binary doublet {n=±1} (derived minimal winding pair) + complex");
        sb.AppendLine("           Hilbert space ⇒ Bloch sphere ⇒ SO(3)=SU(2)/Z₂ ⇒ spinor SU(2). Structure");
        sb.AppendLine("           emergent; the '2' is the minimal winding pair (near-derived).");
        sb.AppendLine("  SU(3)  : CONTINGENT — the '3' is the underived count (same 3 as generations/dims);");
        sb.AppendLine("           the full 8-gluon algebra is borrowed, not derived. Weakest link.");
        return sb.ToString();
    }

    private static string SectionF()
    {
        return
            "THEORETICAL VALUE (what each subgroup is, in terms of the chain):\n" +
            "  U(1)  = Aut(S¹)                         [value: derived, success 1.0]\n" +
            "  SU(2) = double-cover of SO(3) ≅ Aut(S²)  [value: emergent, success ~0.7]\n" +
            "  SU(3) = Aut(C³/S₃) ⊇ SU(3)              [value: contingent, success ~0.1]\n" +
            "\n" +
            "STRONGEST NO-GO THEOREM:\n" +
            "  The defect-moduli route derives the group STRUCTURE Aut(moduli of n defects) ⊇ SU(n),\n" +
            "  but the defect COUNT n is not fixed by topology (π₁(S¹)=ℤ, infinite), attractor content\n" +
            "  (contingent), symmetry (S_n permutes, doesn't fix n), or persistence (all classical\n" +
            "  groups stable). Therefore the SM gauge group's non-abelian factors reduce to the underived\n" +
            "  counts n=2 (weak) and n=3 (strong) — the same '1-2-3' that recurs as 3 generations and\n" +
            "  3+1 dimensions. With no new primitives and anthropic/numerology/hidden-dims rejected,\n" +
            "  SU(3)'s '3' is irreducible-CONTINGENT.\n" +
            "\n" +
            "STRONGEST REMAINING DERIVATION PATH:\n" +
            "  Derive the defect-count pattern (1,2,3) from a single principle. This is IDENTICAL to the\n" +
            "  open 'why 3' question (QG-067): the unique intersection of the CP-violation lower bound\n" +
            "  (N≥3) and the empirical Z-width/Higgs upper bound (N≤3). If ANY one of the three '3's\n" +
            "  (generations, color, spatial dims) were derived, the others might follow — but QG-067\n" +
            "  already showed this is SELECTION, not derivation. Hence SU(3) remains contingent.";
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
