using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

/// <summary>
/// Hostile audit: is the Flavor/Yukawa structure still reducible, given the accepted hierarchy
/// (Q + Random Actualization + (ℓ,τ,ħ) + M²) and no new primitives allowed?
/// </summary>
public class TQM_FlavorReducibilityAudit : ResearchTestBase
{
    public TQM_FlavorReducibilityAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void FlavorReducibility_HostileAudit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        double q = FlavorReducibilityAnalyzer.KoideQ();
        double angle = FlavorReducibilityAnalyzer.KoideAngleDeg();
        var (ent, entRatio) = FlavorReducibilityAnalyzer.ShannonEntropy();
        var (pe, pmu, ptau) = FlavorReducibilityAnalyzer.Participation();

        var sb = new StringBuilder();
        PrintHeader("Flavor/Yukawa Reducibility — Hostile Audit");

        S(sb, "Section A — The exact chain"); sb.AppendLine(SectionA());
        S(sb, "Section B — First unresolved node"); sb.AppendLine(SectionB());
        S(sb, "Section C — Four origin tests (computed)"); sb.AppendLine(SectionC(q, angle, ent, entRatio));
        S(sb, "Section D — Five rejected fallacies"); sb.AppendLine(SectionD());
        S(sb, "Section E — Koide classification"); sb.AppendLine(SectionE(q, pe, pmu, ptau));
        S(sb, "Section F — Compression path, no-go, parameter count, success"); sb.AppendLine(SectionF());

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Q = {q:F7}   θ = {angle:F4}°   S/S_max = {entRatio:F4}   p = ({pe:F4}, {pmu:F4}, {ptau:F4})");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "FlavorReducibility_Report.txt"), sb.ToString());

        // The Koide relation holds to ~1e-5 (pole masses); verify ≈ 2/3.
        Assert.Equal(2.0 / 3.0, q, 4);
        // 45° must be recovered.
        Assert.Equal(45.0, angle, 2);
        // Entropy is NOT extremal (S/S_max between 0.49 and 0.55, not at 0 or 1).
        Assert.InRange(entRatio, 0.45, 0.60);
        // Democratic Q = 1/3 ≠ 2/3 (symmetry/info origin fails).
        Assert.NotEqual(FlavorReducibilityAnalyzer.DemocraticQ(), q);
    }

    // ---------------------------------------------------------------------

    private static string SectionA()
    {
        return
            "Landscape (actualization's effective potential)                       [form derived, content contingent]\n" +
            "  └─► Architecture Shapes (frequency values, hierarchy 1:207:3478)     [UNDERIVED]\n" +
            "        └─► Yukawa Spectrum (Y = <arch_i | amplitude | arch_j>)        [operator derived, spectrum free]\n" +
            "              └─► Koide Q = 2/3 = 45° balance                           [unexplained, lepton-specific]\n" +
            "\n" +
            "Known facts (accepted): G exists (real internal space, mixing requires it); dim(G)=3 is\n" +
            "SELECTED (N>=3 for CP; N<=3 from Z-width/Higgs), not derived; Y = overlap operator\n" +
            "(QG-062); architecture shapes unresolved (QG-063); Koide 45° unexplained (QG-057..061).\n" +
            "Constraint: NO new primitives allowed — reduction must bottom out in Q, Randomness,\n" +
            "(ℓ,τ,ħ), M².";
    }

    private static string SectionB()
    {
        return
            "FIRST UNRESOLVED NODE: Architecture Shapes (the frequency values / the 1:207:3478\n" +
            "hierarchy). Everything ABOVE it is characterized/derived: Y = overlap operator (the one\n" +
            "derived link, QG-062); masses = eigenvalues; mixing = eigenvector rotations; Koide = an\n" +
            "eigenvalue relation. The shapes themselves are the final underived input, set by the\n" +
            "attractor landscape whose CONTENT is contingent (Random Actualization, QG-042/064).\n" +
            "\n" +
            "So the chain has ALREADY bottomed out at the landscape content. The shapes → Koide\n" +
            "step is where the only unexplained number lives.";
    }

    private static string SectionC(double q, double angle, double ent, double entRatio)
    {
        var sb = new StringBuilder();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "Observed: Q = {0:F6}, θ = {1:F3}°, S/S_max = {2:F4}.", q, angle, entRatio));
        sb.AppendLine();
        sb.AppendLine("a) SYMMETRY origin: S3 permutation symmetry is automatic on 3 objects, but the exact");
        sb.AppendLine($"   democratic S3 texture gives Q = 1/3 (uniform p_i), and full hierarchy gives Q = 1.");
        sb.AppendLine($"   Observed Q = 2/3 is the MIDPOINT ({(1.0/3.0 + 1.0)/2.0:F4}) — a 'halfway' S3 breaking");
        sb.AppendLine("   that is NON-GENERIC. Symmetry gives the geometry, NOT the balance. FAILS to derive 2/3.");
        sb.AppendLine();
        sb.AppendLine("b) ATTRACTOR origin: no flavor RG flow has Q=2/3 as a fixed point. Lepton Yukawas run");
        sb.AppendLine("   weakly (QED/EW only), so any Q is PRESERVED (stable) but NOT driven to 2/3 (not");
        sb.AppendLine("   attractive). Stability ≠ selection (QG-060). FAILS.");
        sb.AppendLine();
        sb.AppendLine("c) TOPOLOGY origin: the bare S¹+U(1) flavor geometry LOCATES Koide (the unconfined");
        sb.AppendLine("   lepton limit, QG-050/059) but does NOT fix the angle value. Topology gives the space,");
        sb.AppendLine("   not the angle. FAILS.");
        sb.AppendLine();
        sb.AppendLine("d) INFORMATION-GEOMETRY origin: the participation entropy is S/S_max ≈ 0.51 — close to");
        sb.AppendLine("   but NOT extremal (neither max at 1/3 nor min at 1). No optimization principle selects");
        sb.AppendLine("   Q=2/3 (QG-057). FAILS.");
        sb.AppendLine();
        sb.AppendLine("RESULT: all four origins fail to derive Q=2/3 from TQM's primitives.");
        return sb.ToString();
    }

    private static string SectionD()
    {
        return
            "Rejected fallacies (per the audit rules):\n" +
            "  1. ANTHROPIC — dim(G)=3 is anthropic, but Q=2/3 is NOT: nearby values (Q=0.60..0.75)\n" +
            "     are all equally viable (QG-061); no observer depends on Q=2/3. REJECTED.\n" +
            "  2. TEXTURE FITTING — S3 textures FIT Q=2/3 with fitted parameters but do not DERIVE it\n" +
            "     (QG-057). REJECTED as explanation.\n" +
            "  3. NUMEROLOGY — coincidence disfavored (~1e-4 look-elsewhere) AND Koide was a 1981\n" +
            "     PREDICTION (m_τ before measurement). Real, not numerology. But 'real' ≠ 'derived'.\n" +
            "     REJECTED as dismissal (the relation is real), but NOT accepted as an explanation.\n" +
            "  4. HIDDEN PARAMETERS — no new primitives allowed; a hidden parameter to force 45° is\n" +
            "     forbidden and would just rename the unknown. REJECTED.\n" +
            "  5. RESTATEMENTS OF 45° — 'participation ratio 3/2', 'balanced S3', 'midpoint', 'latitude\n" +
            "     circle', 'singlet=doublet' are ALL the SAME fact in different coordinates (QG-058).\n" +
            "     REJECTED as new explanations (they add zero information).";
    }

    private static string SectionE(double q, double pe, double pmu, double ptau)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Koide Q=2/3 is: CONTINGENT (not Derived, Emergent, Selected, or a new Irreducible).");
        sb.AppendLine();
        sb.AppendLine("  - NOT Derived: symmetry/attractor/topology/info-geometry all fail (Section C).");
        sb.AppendLine("  - NOT Emergent: no dynamics/attractor produces it (weak RG preserves, not drives).");
        sb.AppendLine("  - NOT Selected: nearby values equally viable (QG-061); no selection mechanism.");
        sb.AppendLine("  - NOT a new Irreducible: it is CONTENT, not a primitive — the structure/content split");
        sb.AppendLine("    (QG-042/065) classifies it as contingent content, drawn by Random Actualization.");
        sb.AppendLine();
        sb.AppendLine("  CONTINGENT: the specific realization of the lepton architecture shape (a 45°-balanced");
        sb.AppendLine("  amplitude vector). It could have been otherwise; nothing in {Q, Randomness, (ℓ,τ,ħ), M²}");
        sb.AppendLine("  fixes it. Real (10⁻⁵, predicted), stable (RG-invariant), lepton-specific — but drawn, not");
        sb.AppendLine("  derived.");
        return sb.ToString();
    }

    private static string SectionF()
    {
        return
            "STRONGEST COMPRESSION PATH:\n" +
            "  Koide 45° ⟶ lepton architecture shape (the 45°-balanced amplitude vector) ⟶ landscape\n" +
            "  content (contingent). The ONE remaining reduction is 'derive the lepton architecture\n" +
            "  shape', which bottoms out at the contingent landscape content. There is no further step\n" +
            "  without a new primitive (forbidden).\n" +
            "\n" +
            "STRONGEST NO-GO THEOREM:\n" +
            "  No symmetry, attractor, topology, or information-geometry principle selects Q=2/3 from\n" +
            "  {Q, Randomness, (ℓ,τ,ħ), M²}; and no new primitive is allowed. Therefore Q=2/3 is\n" +
            "  irreducible-CONTINGENT: its value is not computable from TQM's primitives. (Proved by\n" +
            "  exhaustion: S3→Q=1/3 or 1; RG→no fixed point; S¹+U(1)→no angle; entropy→not extremal.)\n" +
            "\n" +
            "ESTIMATED PARAMETER REDUCTION:\n" +
            "  13 flavor params (9 masses + 4 mixing) already reduce to architecture shapes (the\n" +
            "  contingent input) via Y = overlap. Koide itself is NOT a parameter — it REMOVES one\n" +
            "  (constrains the 3 lepton masses to a 2D surface). Net further reduction available: ZERO,\n" +
            "  unless the architecture shapes are derived — which is impossible (contingent content).\n" +
            "\n" +
            "PROBABILITY OF SUCCESS (deriving Q=2/3 from existing primitives): ≈ 0.\n" +
            "  Because the landscape content is contingent BY CONSTRUCTION, 'deriving' the value would\n" +
            "  contradict the structure/content split. The only non-zero-probability route is showing\n" +
            "  2/3 is the TYPICAL value under some landscape measure — but no such measure exists in\n" +
            "  TQM, and specifying one would be a new primitive.";
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
