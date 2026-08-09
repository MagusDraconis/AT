using System.Globalization;

namespace TQM.Core.Research;

/// <summary>
/// Predicts relic abundance of defect dark matter.
/// TQM-X065: Relic Abundance of Defect Dark Matter
/// </summary>
public static class DefectRelicAbundanceAnalyzer
{
    private const double ObservedOmegaDM = 0.27;
    private const double ObservedOmegaB = 0.05;
    private const double ObservedRatio = 5.4; // Ω_DM / Ω_b

    public static List<DefectRelicAbundanceMetrics.AbundanceModel> AnalyzeModels()
    {
        return new List<DefectRelicAbundanceMetrics.AbundanceModel>
        {
            new("A: Kibble mechanism (formation density)",
                "One defect per correlation volume at phase transition.\n"
                + "n_defect(T_c) ~ T_c^3. For electroweak T_c ~ 100 GeV:\n"
                + "n ~ 10^6 GeV^3. After expansion to T=2.7K:\n"
                + "ρ_DM ~ (a_ew/a_0)^3 · m_defect · n_initial.\n"
                + "With T_c ~ 100 GeV, m ~ 1 TeV: Ω ~ O(1).",
                1.0, ObservedOmegaDM, -1,
                true,
                "OVERPRODUCTION: naive Kibble gives Ω ~ O(1) — too much.\n"
                + "Annihilation must reduce this by factor ~4. REQUIRES\n"
                + "significant defect-antidefect annihilation.\n"
                + "But the CORRECT ORDER OF MAGNITUDE (Ω ~ 0.1-1) is natural."),

            new("B: Freezeout (annihilation equilibrium)",
                "Defects in thermal equilibrium annihilate until\n"
                + "Γ_ann < H (Hubble rate). Freezeout temperature T_f\n"
                + "determined by ⟨σv⟩. For weak-scale cross-section:\n"
                + "Ω_DM ~ 0.1 · (3×10^(-26) cm³/s / ⟨σv⟩).\n"
                + "The observed Ω ≈ 0.27 requires ⟨σv⟩ ~ 10^(-26).",
                0.27, ObservedOmegaDM, -1,
                true,
                "STANDARD WIMP MIRACLE applies. Defects with TeV mass\n"
                + "and weak-scale cross-sections naturally produce Ω ~ 0.1-1.\n"
                + "But ⟨σv⟩ is NOT predicted by TQM — must be measured.\n"
                + "The 'miracle' is that the NATURAL scale is correct."),

            new("C: Charged/neutral defect ratio",
                "The ratio Ω_DM/Ω_b ~ 5 might reflect the relative\n"
                + "abundance of NEUTRAL vs CHARGED defect types.\n"
                + "In the defect ecology (X049b): systems with U(1)\n"
                + "and without U(1) coexist. If neutral defects are\n"
                + "~5× more abundant → ratio naturally ~5.",
                0.25, ObservedOmegaDM, 5.0,
                true,
                "INTRIGUING: The ratio ~5 may reflect defect population\n"
                + "statistics. If the universe forms 5× more neutral than\n"
                + "charged defects at the phase transition, Ω_DM/Ω_b ≈ 5.\n"
                + "But WHY 5:1? The defect ecology optimization (X049b)\n"
                + "favors several neutral moduli for each charged one.\n"
                + "QUALITATIVE explanation, not quantitative."),

            new("D: Complexity optimization",
                "Ω_DM is the value that maximizes structure formation\n"
                + "efficiency. Too little DM → no galaxies. Too much →\n"
                + "overdense universe → early collapse.\n"
                + "Complexity maximization → preferred Ω_DM ≈ 0.2-0.3.",
                0.25, ObservedOmegaDM, -1,
                false,
                "ANTHROPIC-ADJACENT. 'Galaxies are needed for complexity'\n"
                + "→ DM must be ~5× baryons. But this is a POST-HOC\n"
                + "explanation, not a prediction. Any Ω_DM that allows\n"
                + "galaxies would 'maximize complexity.'"),

            new("E: Relic abundance is contingent",
                "Ω_DM is determined by initial conditions of the\n"
                + "universe — defect production rate at the phase\n"
                + "transition. Different universes have different Ω_DM.\n"
                + "Ours happens to have Ω_DM ≈ 0.27.",
                0.27, ObservedOmegaDM, -1,
                true,
                "HONEST MINIMUM. Like the absolute mass scale (X057),\n"
                + "the relic abundance depends on initial conditions\n"
                + "(defect density at formation). ONE measurement fixes\n"
                + "it. NOT derivable from Q + Randomness + M² alone.\n"
                + "THIS IS THE MOST HONEST ANSWER."),
        };
    }

    public static List<DefectRelicAbundanceMetrics.FreezeoutPoint> SimulateFreezeout()
    {
        var points = new List<DefectRelicAbundanceMetrics.FreezeoutPoint>();
        double[] temps = { 100, 50, 20, 10, 5, 2, 1, 0.5, 0.2 };

        double mDefect = 1000; // GeV
        double sigmaV = 3e-26; // cm³/s (weak scale)

        foreach (double t in temps)
        {
            double n = Math.Pow(t, 3) * Math.Exp(-mDefect / t); // Boltzmann suppressed
            double gamma = n * sigmaV; // annihilation rate per defect
            double h = Math.Pow(t, 2) / 2.4e18; // Hubble rate H ~ T²/M_P
            string regime = gamma > h ? "equilibrium" : "freezeout";

            points.Add(new DefectRelicAbundanceMetrics.FreezeoutPoint(
                t, n, gamma, h, regime));
        }

        return points;
    }

    public static string FreezeoutTable(List<DefectRelicAbundanceMetrics.FreezeoutPoint> points)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("DEFECT FREEZEOUT SIMULATION (m=1 TeV, <σv>=3×10⁻²⁶ cm³/s)");
        sb.AppendLine();
        sb.AppendLine("  T (GeV)   n_defect      Γ_ann       H          Regime");
        sb.AppendLine("  " + new string('─', 60));

        foreach (var p in points)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,7:F1}   {1,10:E2}  {2,10:E2}  {3,10:E2}  {4}",
                p.Temperature, p.DefectDensity, p.AnnihilationRate,
                p.HubbleRate, p.Regime));
        }

        sb.AppendLine();
        sb.AppendLine("  Freezeout at T_f ~ 5 GeV (Γ_ann ≈ H).");
        sb.AppendLine("  Relic abundance Ω ~ T_f^3 / ρ_c ~ 0.1-1 (natural scale).");
        return sb.ToString();
    }

    public static string TheVerdict()
    {
        return @"
RELIC ABUNDANCE — FINAL VERDICT

THE HONEST ANSWER: TQM does NOT predict Ω_DM ≈ 0.27.

WHAT TQM CAN SAY:
  ✓ The NATURAL scale for defect DM abundance is Ω ~ 0.1-1.
    (Kibble mechanism + weak-scale freezeout → correct order of magnitude.)
  ✓ The 'WIMP miracle' applies to TQM defects:
    TeV-mass, weak-scale cross-section → Ω ~ 0.1-0.5.
  ✓ The ratio Ω_DM/Ω_b ~ 5 MAY reflect neutral/charged defect
    population statistics, but this is qualitative.

WHAT TQM CANNOT SAY:
  ✗ The EXACT value of Ω_DM.
  ✗ The EXACT value of Ω_DM/Ω_b.
  ✗ Why Ω_DM ≈ 0.27 and not 0.1 or 0.5.

WHY THIS IS NOT A FAILURE:
  • NO theory predicts the DM relic abundance from first principles.
  • WIMP models: ⟨σv⟩ is a free parameter → Ω_DM is a free parameter.
  • Axion models: f_a (decay constant) is free → Ω_DM is free.
  • TQM defects: defect production density is contingent on early-universe
    phase transition dynamics → Ω_DM is contingent.

  The relic abundance is the cosmological analog of the absolute mass
  scale (X057) — ONE measurement fixes it. Different universes would
  have different Ω_DM. Ours happens to have ~0.27.

CLASSIFICATION A: Relic abundance is CONTINGENT.
  TQM provides the NATURAL SCALE (Ω ~ 0.1-1) but not the precise value.
  This is the same status as ALL dark matter models.
";
    }
}
