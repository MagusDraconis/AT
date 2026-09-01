using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

/// <summary>Neutrino-Koide: derive neutrino-mass implications of Q=2/3.</summary>
public class AT_NeutrinoKoideAudit : ResearchTestBase
{
    public AT_NeutrinoKoideAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void NeutrinoKoide_Audit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("Neutrino-Koide — Flavor-Constraint Implications");

        S(sb, "Section A — Deriving neutrino-mass implications"); sb.AppendLine(SectionA());
        S(sb, "Section B — Charged-only vs all-lepton-sector"); sb.AppendLine(SectionB());
        S(sb, "Section C — Likelihood shift"); sb.AppendLine(SectionC());
        S(sb, "Section D — Outputs"); sb.AppendLine(SectionD());

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  NO lightest m: " + Fmt(NeutrinoKoideAnalyzer.SolveLightestMass(true)) +
                      "   IO lightest m: " + Fmt(NeutrinoKoideAnalyzer.SolveLightestMass(false)));
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "NeutrinoKoide_Report.txt"), sb.ToString());

        // The charged-lepton Koide holds (sanity).
        var leptonQ = FlavorReducibilityAnalyzer.KoideQ();
        Assert.Equal(2.0 / 3.0, leptonQ, 4);
        // Neutrino-Koide is FALSIFIED: Q_ν is capped below 2/3 for both orderings.
        Assert.True(NeutrinoKoideAnalyzer.MaxQ(true) < 2.0 / 3.0);
        Assert.True(NeutrinoKoideAnalyzer.MaxQ(false) < 2.0 / 3.0);
        // No solution exists for either ordering.
        Assert.Null(NeutrinoKoideAnalyzer.SolveLightestMass(true));
        Assert.Null(NeutrinoKoideAnalyzer.SolveLightestMass(false));
    }

    // ---------------------------------------------------------------------

    private static string SectionA()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Assuming Koide Q=2/3 is a flavor constraint (scale-invariant, fixes the mass-SHAPE):");
        sb.AppendLine();
        sb.AppendLine($"  Δm²_21 = {NeutrinoKoideAnalyzer.Dm21:E2} eV²,  Δm²_32 = {NeutrinoKoideAnalyzer.Dm32:E2} eV²");
        sb.AppendLine();
        sb.AppendLine("  With Q=2/3 + the two measured Δm², the absolute scale is FIXED. Solving for the");
        sb.AppendLine("  lightest mass gives: NO solution for EITHER ordering.");
        sb.AppendLine();
        sb.AppendLine("  WHY: the neutrino mass spectrum CANNOT reach Q=2/3. The maximum achievable Q");
        sb.AppendLine("  (at the most hierarchical limit, m_light→0) is:");
        foreach (bool no in new[] { true, false })
        {
            string label = no ? "NORMAL ordering" : "INVERTED ordering";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "    {0,-18} Q_max = {1:F4}   (must be ≥ 2/3 = 0.6667 to satisfy Koide)",
                label, NeutrinoKoideAnalyzer.MaxQ(no)));
        }
        sb.AppendLine();
        sb.AppendLine("  DECISIVE: both Q_max values are BELOW 2/3. The measured neutrino Δm² imply Q_ν ≤ 0.59");
        sb.AppendLine("  (NO) or ≤ 0.50 (IO) for ANY absolute scale. The neutrino mass spectrum cannot satisfy");
        sb.AppendLine("  the standard Koide relation — NEUTRINO-KOIDE IS ALREADY FALSIFIED, no DUNE needed.");
        return sb.ToString();
    }

    private static string SectionB()
    {
        return
            "CHARGED-LEPTON-ONLY vs ALL-LEPTON-SECTOR:\n" +
            "\n" +
            "  Charged-lepton-only: Koide is a contingent correlation SPECIFIC to charged leptons\n" +
            "    (QG-048/059). Then neutrino-Koide has NO reason to hold — it would require a SECOND\n" +
            "    independent coincidence at ~1e-5 precision. P(neutrino-Koide | charged-only) ≈ 1e-5.\n" +
            "\n" +
            "  All-lepton-sector: Koide is a flavor constraint on the WHOLE lepton sector (charged +\n" +
            "    neutral). Then neutrino masses MUST also satisfy Q=2/3 (up to the same RG/precision\n" +
            "    caveats). P(neutrino-Koide | all-lepton) ≈ 1.\n" +
            "\n" +
            "  These are sharply DIFFERENT predictions: the two hypotheses are discriminated by whether\n" +
            "  the neutrino mass spectrum (once measured) satisfies Q=2/3.";
    }

    private static string SectionC()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Likelihood shift (COUNTERFACTUAL, since neutrino-Koide is already falsified):");
        sb.AppendLine();
        sb.AppendLine("  Had neutrino-Koide HELD, the shift would have been:");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    BF(all-lepton : charged-only) ≈ 1/{0:E0} = {1:E1}.", 1e-5, NeutrinoKoideAnalyzer.LikelihoodShift()));
        sb.AppendLine();
        sb.AppendLine("  BUT it does NOT hold: Q_ν ≤ 0.59 (NO) / 0.50 (IO) < 2/3 for ANY absolute scale.");
        sb.AppendLine("  So the likelihood of all-lepton-sector is ≈ 0 (excluded), and charged-lepton-only wins.");
        sb.AppendLine();
        sb.AppendLine("  NET: the all-lepton-sector hypothesis is FALSIFIED by existing Δm²; Koide is confirmed");
        sb.AppendLine("  charged-lepton-specific (contingent).");
        return sb.ToString();
    }

    private static string SectionD()
    {
        return
            "BEST DISCRIMINATOR (already resolved): the measured neutrino Δm² already cap Q_ν below 2/3,\n" +
            "  so the all-lepton-Koide hypothesis is EXCLUDED without needing a new measurement.\n" +
            "\n" +
            "STRONGEST FALSIFICATION PATH: none needed for the STANDARD Koide — it is already falsified\n" +
            "  for neutrinos. (A MODIFIED neutrino relation — e.g. a different Q, or a Koide-like relation\n" +
            "  for the neutrino MASS MATRIX rather than eigenvalues — would be a NEW hypothesis, requiring\n" +
            "  its own derivation, and is outside this audit's scope.)\n" +
            "\n" +
            "CLASSIFICATION UPDATE RULE (resolved):\n" +
            "  neutrino-Koide FAILS (already) → Koide stays CONTINGENT, confirmed CHARGED-LEPTON-SPECIFIC.\n" +
            "  The 'hidden structure' (Phase 154) is sector-LOCAL: it holds for charged leptons only, and\n" +
            "  does NOT extend to neutrinos. Koide = a real but non-universal relation, origin contingent.";
    }

    private static string Fmt(double? v) => v.HasValue ? v.Value.ToString("E2", CultureInfo.InvariantCulture) : "none";

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
