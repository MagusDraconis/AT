using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

/// <summary>Koide hidden-structure search (indicators only, no derivation).</summary>
public class TQM_KoideHiddenStructureAudit : ResearchTestBase
{
    public TQM_KoideHiddenStructureAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void KoideHiddenStructure_Audit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        double q = KoideHiddenStructureAnalyzer.KoideQ();
        double prec = KoideHiddenStructureAnalyzer.Precision();
        double angle = KoideHiddenStructureAnalyzer.AngleDeg();
        double bfReal = KoideHiddenStructureAnalyzer.BayesFactorRealVsCoincidence();

        var sb = new StringBuilder();
        PrintHeader("Koide Q=2/3 — Hidden-Structure Indicator Search");

        S(sb, "Section A — The facts"); sb.AppendLine(SectionA(q, prec, angle));
        S(sb, "Section B — Strongest evidence CONTINGENT"); sb.AppendLine(SectionB());
        S(sb, "Section C — Strongest evidence HIDDEN STRUCTURE"); sb.AppendLine(SectionC());
        S(sb, "Section D — Bayesian balance"); sb.AppendLine(SectionD(bfReal));
        S(sb, "Section E — Remaining falsifiable test"); sb.AppendLine(SectionE());

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Q = {q:F7}  deviation = {prec:E1}  BF(real vs coincidence) = {bfReal:E1}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "KoideHiddenStructure_Report.txt"), sb.ToString());

        Assert.Equal(2.0 / 3.0, q, 4);
        Assert.True(prec < 1e-4);            // precision ≲ 1e-5
        Assert.True(bfReal > 1000);          // real-structure strongly favored over coincidence
    }

    // ---------------------------------------------------------------------

    private static string SectionA(double q, double prec, double angle)
    {
        var sb = new StringBuilder();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Q = {0:F7} (2/3 to {1:E1})", q, prec));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "  θ = {0:F4}° (arccos(1/√2) = 45°)", angle));
        sb.AppendLine("  RG-stable (lepton Yukawas run weakly); dimensionless, scale-free.");
        sb.AppendLine("  No symmetry/topology/attractor derivation (QG-057/059/060/061).");
        return sb.ToString();
    }

    private static string SectionB()
    {
        return
            "STRONGEST EVIDENCE FOR CONTINGENT:\n" +
            "  1. No symmetry/topology/attractor/info-geometry derivation (exhaustively searched).\n" +
            "  2. LEPTON-SPECIFIC: Koide FAILS for quarks (QG-048) — a universal structure would be\n" +
            "     universal; the observed structure is sector-specific.\n" +
            "  3. The value 2/3 = midpoint of [1/3,1] is a 'nice number' — consistent with a\n" +
            "     nice-number accident (nearby Q=0.60..0.75 all equally viable, QG-061).\n" +
            "  4. The structure/content split (QG-042/065) classifies the VALUE (45°) as content,\n" +
            "     drawn by Random Actualization.";
    }

    private static string SectionC()
    {
        return
            "STRONGEST EVIDENCE FOR HIDDEN STRUCTURE:\n" +
            "  1. EXTREME PRECISION: Q=2/3 holds to ~1e-5 (pole masses). A contingent draw of 3\n" +
            "     log-normal masses gives Q spread over [1/3,1]; landing within 1e-5 of 2/3 has\n" +
            "     probability ~1e-5. This precision is the signature of a REAL constraint, not noise.\n" +
            "  2. PREDICTION BEFORE MEASUREMENT: Koide (1981) predicted m_τ = 1776.97 MeV, confirmed\n" +
            "     by the 1992 measurement. A coincidence cannot predict.\n" +
            "  3. SCALE-FREE + RG-STABLE: dimensionless, runs weakly — a UV property, consistent with\n" +
            "     a structure of the unconfined lepton sector (QG-050/059), not an IR accident.\n" +
            "  4. GEOMETRIC DISTINCTION: θ=45° = arccos(1/√2) is the unique BALANCE point (singlet=\n" +
            "     doublet in the S3 decomposition) — a distinguished, non-generic angle.";
    }

    private static string SectionD(double bfReal)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Bayesian balance:");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  BF(real structure : contingent coincidence) = (1/precision)/look-elsewhere = {0:E1}.",
            bfReal));
        sb.AppendLine("  Look-elsewhere ≈ 5 (only ~5 flavor relations actually tested).");
        sb.AppendLine();
        sb.AppendLine("  ⇒ The data STRONGLY favor 'real hidden structure' over 'coincidence' (~1e4–1e5).");
        sb.AppendLine();
        sb.AppendLine("  BUT the SECOND question — derived vs contingent ORIGIN — has BF ≈ 1: there is no");
        sb.AppendLine("  evidence for a derived origin (no mechanism) and no evidence against it. So:");
        sb.AppendLine();
        sb.AppendLine("  RESOLUTION: Koide is a REAL HIDDEN STRUCTURE (precise, predictive, non-coincidental)");
        sb.AppendLine("  whose ORIGIN (why 45°) is CONTINGENT — not derivable from the primitives. 'Contingent'");
        sb.AppendLine("  (Phase 148) describes the ORIGIN, not the reality of the relation.");
        return sb.ToString();
    }

    private static string SectionE()
    {
        return
            "REMAINING FALSIFIABLE TEST: the NEUTRINO-KOIDE prediction (Q=2/3 for neutrino masses).\n" +
            "\n" +
            "  - If neutrino masses ALSO satisfy Q=2/3 (measurable by DUNE/Hyper-K, 5-10 yr): this would\n" +
            "    CONFIRM a hidden STRUCTURE spanning the whole lepton sector (charged + neutral), demoting\n" +
            "    'contingent' toward 'emergent/selected' — a lepton-sector relation, not a charged-lepton\n" +
            "    accident.\n" +
            "  - If neutrino masses FAIL Q=2/3: this would CONFIRM 'contingent' — the 45° is a\n" +
            "    charged-lepton-specific realization, with no universal lepton structure.\n" +
            "\n" +
            "  This is the ONLY remaining test that can distinguish 'hidden structure' from 'contingent\n" +
            "  correlation' for Koide — and it is a TQM prediction (QG-068/069), already in the falsification\n" +
            "  roadmap (priority #3).";
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
