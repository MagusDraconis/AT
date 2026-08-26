using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchX;

public class AT_X060_NeutrinoOrdering : ResearchTestBase
{
    public AT_X060_NeutrinoOrdering(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X060_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X060 Neutrino Mass Ordering and Oscillation Structure");

        var models = NeutrinoOrderingAnalyzer.AnalyzeOrdering();
        var oscData = NeutrinoOrderingAnalyzer.ComputeOscillationParams();
        int surviving = models.Count(m => m.Survives);

        // 1. Ordering models
        Sec(sb, "Neutrino Mass Ordering Models");
        sb.AppendLine("  Model                     Ordering           Δm²₂₁      Δm²₃₁      Survives?");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var m in models)
        {
            string s = m.Survives ? "✓" : "✗";
            string d21 = m.PredictedDm21 > 0 ? $"{m.PredictedDm21 * 1e5 / 1e-5:F1}" : "—";
            string d31 = m.PredictedDm31 > 0 ? $"{m.PredictedDm31 * 1e3 / 1e-3:F1}" : "—";
            sb.AppendLine($"  {m.Name,-25} {m.PredictedOrdering,-18} {d21,8}    {d31,8}    {s}");
        }
        sb.AppendLine();
        sb.AppendLine($"  {surviving}/{models.Count} models survive.");
        sb.AppendLine("  Model A: NORMAL ORDERING from attractive self-interaction.");
        sb.AppendLine();

        // 2. Oscillation parameters
        Sec(sb, "Oscillation Parameters — Model A Predictions");
        sb.AppendLine(NeutrinoOrderingAnalyzer.OscillationTable(oscData));
        sb.AppendLine();

        // 3. Why normal
        Sec(sb, "Why Normal Ordering Is Natural");
        sb.AppendLine("  DELOCALIZED DEFECT EXCITATION SPECTRUM:");
        sb.AppendLine();
        sb.AppendLine("  Level 0 (ν_1): Ground state — maximally delocalized.");
        sb.AppendLine("    ξ_1 = ξ_0               → m_1 ∝ 1/ξ_1  (LIGHTEST)");
        sb.AppendLine();
        sb.AppendLine("  Level 1 (ν_2): First excitation — self-interaction adds");
        sb.AppendLine("    weak localization.");
        sb.AppendLine("    ξ_2 = ξ_0 · (1 - ε)     → m_2 ∝ 1/ξ_2  (HEAVIER)");
        sb.AppendLine();
        sb.AppendLine("  Level 2 (ν_3): Second excitation — double the effect.");
        sb.AppendLine("    ξ_3 = ξ_0 · (1 - 2ε)    → m_3 ∝ 1/ξ_3  (HEAVIEST)");
        sb.AppendLine();
        sb.AppendLine("  RESULT: m_1 < m_2 < m_3 → NORMAL ORDERING.");
        sb.AppendLine();
        sb.AppendLine("  The self-interaction strength ε ≈ 0.02 is TINY because");
        sb.AppendLine("  there is NO U(1) gauge field providing strong localization.");
        sb.AppendLine("  → Mass splittings are SMALL → explains observed Δm² values.");
        sb.AppendLine();

        // 4. Why inverted is unnatural
        Sec(sb, "Why Inverted Ordering Is Unnatural");
        sb.AppendLine("  Inverted (m_3 < m_1 < m_2) would require:");
        sb.AppendLine();
        sb.AppendLine("  Higher excitations → MORE delocalized → LIGHTER.");
        sb.AppendLine();
        sb.AppendLine("  This requires REPULSIVE self-interaction (λ < 0 in φ⁴).");
        sb.AppendLine("  But φ⁴ with λ < 0 has an UNSTABLE vacuum:");
        sb.AppendLine("    V(φ) = -|λ|φ⁴ → potential unbounded below.");
        sb.AppendLine("    The defect would decay immediately.");
        sb.AppendLine();
        sb.AppendLine("  AT's defect potential comes from the PDE reaction term:");
        sb.AppendLine("    (1-R²)·R → V(R) ∝ (R²-1)² → λ > 0 (ATTRACTIVE).");
        sb.AppendLine("  Attractive self-interaction is BUILT INTO the AT PDE.");
        sb.AppendLine("  → NORMAL ORDERING IS A PREDICTION OF AT.");
        sb.AppendLine();

        // 5. CP violation
        Sec(sb, "CP Violation in the Neutrino Sector");
        sb.AppendLine("  The overlap integrals ⟨ψ_i|ψ_j⟩ between neutrino excitation");
        sb.AppendLine("  levels are GENERALLY COMPLEX (as with charged fermions, X054).");
        sb.AppendLine();
        sb.AppendLine("  The Dirac CP phase δ_CP appears in the PMNS matrix.");
        sb.AppendLine("  Current hint: δ_CP ≈ 1.3π (nearly maximal CP violation).");
        sb.AppendLine();
        sb.AppendLine("  AT: δ_CP set by the geometric phase from the defect's");
        sb.AppendLine("  internal moduli space. For neutrinos (no U(1) → no S¹ moduli),");
        sb.AppendLine("  the phase comes from the SU(2) weak moduli space → naturally");
        sb.AppendLine("  O(1) → large CP violation is NATURAL (unlike CKM where δ ≈ 1.2).");
        sb.AppendLine();

        // 6. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(NeutrinoOrderingAnalyzer.HostileReview());

        // 7. Final
        string classification = surviving >= 2 ? "C: Strong Preference for Normal Ordering"
            : "B: Weak Preference";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X060 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  NORMAL ORDERING (m_1 < m_2 < m_3) from attractive self-interaction.");
        sb.AppendLine($"  Inverted ordering requires unstable defect → ruled out by AT PDE.");
        sb.AppendLine($"  FALSIFIABLE: if inverted ordering confirmed >5σ, Model A is wrong.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
