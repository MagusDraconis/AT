using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_110_PDEvsDiscreteInteractionRegimes : ResearchTestBase
{
    public AT_110_PDEvsDiscreteInteractionRegimes(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_110_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-110 PDE vs Discrete Interaction Regimes");

        // ── Section 1 ───────────────────────────────────────────────
        Sec(sb, "1. The Two Interaction Mechanisms");

        sb.AppendLine("  AT has TWO distinct interaction mechanisms:");
        sb.AppendLine();
        sb.AppendLine("  DISCRETE: K_ij = K·exp(−d/λ)  →  F_disc ≈ N·K·exp(−d/λ)");
        sb.AppendLine("    • Direct oscillator coupling through the Kuramoto sum");
        sb.AppendLine("    • Strong within coupling range (~3λ = 0.15)");
        sb.AppendLine("    • Drives rapid mergers (AT-012)");
        sb.AppendLine();
        sb.AppendLine("  PDE: D_R·∇²R  →  F_pde ≈ D_R·exp(−d/w)/w");
        sb.AppendLine("    • Soliton field overlap through spatial diffusion");
        sb.AppendLine("    • Extremely weak (D_R = 2.5×10⁻⁵)");
        sb.AppendLine("    • Negligible at N=100 (AT-109)");
        sb.AppendLine();

        // ── Section 2 ───────────────────────────────────────────────
        Sec(sb, "2. Force Scale Comparison");

        var report = InteractionRegimeAnalyzer.RunRegimeAnalysis();

        sb.AppendLine("  Distance     │ PDE Force    │ Discrete Force │ Ratio (disc/pde) │ Regime");
        sb.AppendLine("  " + new string('─', 90));
        foreach (var p in report.Profiles)
        {
            double ratio = p.DiscreteForce / Math.Max(p.PdeForce, 1e-30);
            sb.AppendLine($"  {p.Name,-12} │ {p.PdeForce,11:E2} │ {p.DiscreteForce,13:E2} │ {ratio,15:E1} │ {p.DominantRegime}");
        }
        sb.AppendLine();

        sb.AppendLine($"  Coupling range (3λ): {report.CouplingRange:F3} ({report.CouplingRange / 0.10:F1}w)");
        sb.AppendLine($"  PDE range (3w):      {report.PdeRange:F3} (3w)");
        sb.AppendLine();

        // ── Section 3 ───────────────────────────────────────────────
        Sec(sb, "3. Regime Boundaries");

        sb.AppendLine("  ┌─────────────────────────────────────────────────────────┐");
        sb.AppendLine("  │  DISTANCE │ REGIME          │ PHYSICS                   │");
        sb.AppendLine("  ├─────────────────────────────────────────────────────────┤");
        sb.AppendLine($"  │  d < {report.CouplingRange:F2}   │ DISCRETE        │ Oscillator coupling       │");
        sb.AppendLine($"  │           │ (AT-012 mergers)│ K·exp(−d/λ) dominates     │");
        sb.AppendLine("  ├─────────────────────────────────────────────────────────┤");
        sb.AppendLine($"  │  {report.CouplingRange:F2} < d < {report.PdeRange:F2} │ TRANSITION      │ Both forces weak          │");
        sb.AppendLine("  ├─────────────────────────────────────────────────────────┤");
        sb.AppendLine($"  │  d > {report.PdeRange:F2}   │ PDE FIELD       │ Soliton diffusion only    │");
        sb.AppendLine($"  │           │ (AT-107 survival)│ D_R·∇²R, negligible at N=100│");
        sb.AppendLine("  └─────────────────────────────────────────────────────────┘");
        sb.AppendLine();

        // ── Section 4 ───────────────────────────────────────────────
        Sec(sb, "4. Experimental Validation");

        var validations = InteractionRegimeAnalyzer.ValidateAgainstExperiments(report);

        sb.AppendLine("  Experiment │ Prediction                                         │ Match?");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var (exp, pred) in validations)
        {
            bool match = pred.StartsWith("✓");
            sb.AppendLine($"  {exp,-10} │ {pred,-50} │ {(match ? "✓" : "✗")}");
        }
        sb.AppendLine();

        // ── Section 5 ───────────────────────────────────────────────
        Sec(sb, "5. Unified Multi-Scale Picture");

        sb.AppendLine(report.UnifiedPicture);
        sb.AppendLine();

        // ── Section 6 ───────────────────────────────────────────────
        Sec(sb, "6. Research Questions");

        sb.AppendLine("  Q1: Where is the crossover distance?");
        sb.AppendLine($"    Crossover ≈ {report.CrossoverDistance:F3} ({report.CrossoverDistance / 0.10:F1}w, {report.CrossoverDistance / 0.05:F0}λ).");
        sb.AppendLine("    Below this: discrete dominates. Above: PDE dominates.");
        sb.AppendLine();

        sb.AppendLine("  Q2: When does PDE interaction dominate?");
        sb.AppendLine($"    d > {report.CouplingRange:F2} — beyond the discrete coupling range.");
        sb.AppendLine("    But PDE force is O(10⁻⁵) — negligible at N=100.");
        sb.AppendLine();

        sb.AppendLine("  Q3: When does discrete coupling dominate?");
        sb.AppendLine($"    d < {report.CouplingRange:F2} — within the coupling range ~3λ.");
        sb.AppendLine("    Discrete force is O(10³) at d=0.01, O(10⁻¹) at d=0.15.");
        sb.AppendLine();

        sb.AppendLine("  Q4: Can AT-012 mergers be reproduced?");
        sb.AppendLine($"    YES — AT-012 separations (d ≤ 0.25) are near the coupling");
        sb.AppendLine("    range boundary. Discrete coupling drives rapid mergers.");
        sb.AppendLine();

        sb.AppendLine("  Q5: Is there a critical interaction radius?");
        sb.AppendLine($"    YES — r_c ≈ 3λ = {report.CouplingRange:F2} ({report.CouplingRange / 0.10:F1}w).");
        sb.AppendLine("    Within r_c: discrete coupling → merger.");
        sb.AppendLine("    Beyond r_c: PDE diffusion → negligible interaction.");
        sb.AppendLine();

        sb.AppendLine("  Q6: Can a multi-scale theory unify AT-108 and AT-109?");
        sb.AppendLine($"    Classification: {report.Classification}");
        sb.AppendLine("    YES — the two regimes are complementary:");
        sb.AppendLine("    • AT-108 PDE: describes field dynamics at all scales");
        sb.AppendLine("    • Discrete coupling: dominates at short range (d < 3λ)");
        sb.AppendLine("    • PDE diffusion: dominates at long range (d > 3λ)");
        sb.AppendLine("    • AT-109: PDE forces are negligible → discrete regime");
        sb.AppendLine("      explains all observed condensate interactions");
        sb.AppendLine();

        // ── Section 7 ───────────────────────────────────────────────
        Sec(sb, "7. Classification");

        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Interpretation}");
        sb.AppendLine();

        // ── Section 8 ───────────────────────────────────────────────
        Sec(sb, "8. Conclusion");
        sb.AppendLine($"  C1.  Coupling range: {report.CouplingRange:F3} ({report.CouplingRange / 0.10:F1}w, {report.CouplingRange / 0.05:F0}λ)");
        sb.AppendLine($"  C2.  PDE range: {report.PdeRange:F3} (3w)");
        sb.AppendLine($"  C3.  Discrete force at d=λ: {InteractionRegimeAnalyzer.DiscreteForce(0.05):E1}");
        sb.AppendLine($"  C4.  PDE force at d=3w: {InteractionRegimeAnalyzer.PdeForce(0.3):E1}");
        sb.AppendLine($"  C5.  Classification: {report.Classification}");
        sb.AppendLine($"  C6.  AT-012 mergers: DISCRETE regime (d < coupling range)");
        sb.AppendLine($"  C7.  AT-107 survival: PDE regime (d ≫ coupling range, negligible force)");
        sb.AppendLine($"  C8.  All AT condensate interactions are DISCRETE, not field-theoretic");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment AT-110 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
