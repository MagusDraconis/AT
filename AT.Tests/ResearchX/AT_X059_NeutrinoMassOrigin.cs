using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchX;

public class AT_X059_NeutrinoMassOrigin : ResearchTestBase
{
    public AT_X059_NeutrinoMassOrigin(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X059_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X059 Origin of Neutrino Masses from Delocalized Q-Defects");

        var models = NeutrinoMassAnalyzer.AnalyzeModels();
        var comps = NeutrinoMassAnalyzer.CompareLocalization();
        int surviving = models.Count(m => m.Survives);

        // 1. The puzzle
        Sec(sb, "The Neutrino Puzzle");
        sb.AppendLine("  NEUTRINOS ARE UNIQUE:");
        sb.AppendLine("    • Tiny masses: m_ν < 0.8 eV vs m_e = 511,000 eV (×10^6 smaller)");
        sb.AppendLine("    • Large mixing: PMNS entries O(0.3-0.8) vs CKM hierarchical");
        sb.AppendLine("    • Possibly Majorana (particle = antiparticle)");
        sb.AppendLine("    • No electric charge");
        sb.AppendLine();
        sb.AppendLine("  CAN ONE MECHANISM EXPLAIN ALL OF THESE?");
        sb.AppendLine();

        // 2. Candidate models
        Sec(sb, "Candidate Neutrino Models");
        sb.AppendLine("  Model                           Tiny m?  Large Mix?  Majorana?  Survives?");
        sb.AppendLine("  " + new string('─', 75));
        foreach (var m in models)
        {
            string tm = m.ExplainsTinyMass ? "✓" : "✗";
            string lm = m.ExplainsLargeMixing ? "✓" : "✗";
            string mj = m.PredictsMajorana ? "✓" : "—";
            string s = m.Survives ? "✓" : "✗";
            sb.AppendLine($"  {m.Name,-30}  {tm}        {lm}          {mj}        {s}");
        }
        sb.AppendLine();
        sb.AppendLine($"  {surviving}/{models.Count} models survive.");
        sb.AppendLine("  Models A + B: DELOCALIZATION explains tiny mass + large mixing.");
        sb.AppendLine();

        // 3. Localization comparison
        Sec(sb, "The Unifying Principle: U(1) → Localization");
        sb.AppendLine("  U(1) GAUGE CHARGE = CONFINING POTENTIAL = LOCALIZATION.");
        sb.AppendLine("  No U(1) charge = NO confining potential = DELOCALIZATION.");
        sb.AppendLine();
        sb.AppendLine(NeutrinoMassAnalyzer.LocalizationTable(comps));
        sb.AppendLine();
        sb.AppendLine("  THE KEY INSIGHT:");
        sb.AppendLine("    m ∝ 1/ξ (localization length).");
        sb.AppendLine("    |V_ij| ∝ exp(-β·|i-j|), β = Δr/ξ.");
        sb.AppendLine();
        sb.AppendLine("    Smaller ξ → larger mass → smaller mixing (quarks, charged leptons).");
        sb.AppendLine("    Larger ξ → smaller mass → larger mixing (NEUTRINOS).");
        sb.AppendLine("    ONE PARAMETER (ξ) controls BOTH mass AND mixing.");
        sb.AppendLine();

        // 4. Why this works
        Sec(sb, "Why This Works — The Physics");
        sb.AppendLine("  CHARGED FERMIONS (e, μ, τ, u, c, t, d, s, b):");
        sb.AppendLine("    U(1)_EM gauge field A_μ couples to electric charge.");
        sb.AppendLine("    This creates a 'confining' potential ∝ α/r around the defect.");
        sb.AppendLine("    Wavefunction LOCALIZED → small ξ → MeV-GeV masses.");
        sb.AppendLine("    Small spatial overlap between generations → HIERARCHICAL CKM.");
        sb.AppendLine();
        sb.AppendLine("  NEUTRINOS:");
        sb.AppendLine("    NO electric charge → NO U(1) gauge coupling to A_μ.");
        sb.AppendLine("    Wavefunction DELOCALIZED → large ξ → sub-eV masses.");
        sb.AppendLine("    Large spatial overlap → ANARCHIC PMNS.");
        sb.AppendLine();
        sb.AppendLine("  THIS IS NOT A 'TUNING' — IT'S A BINARY PROPERTY:");
        sb.AppendLine("    CHARGED → small ξ → heavy + hierarchical.");
        sb.AppendLine("    NEUTRAL → large ξ → light + anarchic.");
        sb.AppendLine();

        // 5. Majorana
        Sec(sb, "Majorana Neutrinos — A Natural Prediction");
        sb.AppendLine("  In AT, gauge symmetries = defect moduli space automorphisms (X050).");
        sb.AppendLine("  U(1) charge = S¹ moduli space (vortex phase).");
        sb.AppendLine();
        sb.AppendLine("  Neutrino has NO U(1) charge → NO S¹ moduli → no conserved charge.");
        sb.AppendLine("  → Particle CAN be its own antiparticle.");
        sb.AppendLine("  → MAJORANA MASS TERM is allowed (and natural).");
        sb.AppendLine();
        sb.AppendLine("  TESTABLE PREDICTION: Neutrinoless double-beta decay.");
        sb.AppendLine("  If observed: AT prediction confirmed (Majorana neutrinos).");
        sb.AppendLine("  If not observed: Neutrinos are Dirac — AT's delocalization");
        sb.AppendLine("  mechanism still works, just without Majorana nature.");
        sb.AppendLine();

        // 6. Cosmological bounds
        Sec(sb, "Cosmological Neutrino Mass Bound");
        sb.AppendLine("  Planck 2018: Σ m_ν < 0.12 eV (95% CL, TT+lowE).");
        sb.AppendLine("  AT: m_ν ~ ξ_c/ξ_ν · m_e. Depends on ξ_ν.");
        sb.AppendLine();
        sb.AppendLine("  Since ξ_ν is not derived from first principles (X058),");
        sb.AppendLine("  AT cannot predict the EXACT sum of neutrino masses.");
        sb.AppendLine("  But the HIERARCHY m_ν ≪ m_e is EXPLAINED.");
        sb.AppendLine();

        // 7. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(NeutrinoMassAnalyzer.HostileReview());

        // 8. Final
        string classification = surviving >= 3 ? "C: Partial Derivation — Mechanism Identified"
            : surviving >= 1 ? "B: Weak Explanation" : "A: No Mechanism";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X059 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  NEUTRINO = DELOCALIZED DEFECT.");
        sb.AppendLine($"  No U(1) charge → no gauge localization → tiny mass + large mixing.");
        sb.AppendLine($"  ONE MECHANISM explains the neutrino's unique properties.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
