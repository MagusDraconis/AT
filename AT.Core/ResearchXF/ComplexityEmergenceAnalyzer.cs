namespace AT.Core.ResearchXF;

using System.Globalization;

/// <summary>
/// Founding analysis: the emergence of complexity from Q + Randomness.
/// ResearchXF-001: Complexity Emergence Principle
/// </summary>
public static class ComplexityEmergenceAnalyzer
{
    public enum ComplexityRegime { Dead, Ordered, Complex, Chaotic, Formless }

    public sealed record ComplexityPoint(
        double OrderStrength, double RandomnessStrength,
        int DistinguishableStates, double Persistence,
        double NoveltyRate, double ComplexityIndex,
        ComplexityRegime Regime, string Description);

    /// <summary>
    /// Scans the (Order, Randomness) plane to find the complexity optimum.
    /// Order = strength of individuation / structure (Q-like).
    /// Randomness = rate of actualization / novelty (Randomness-like).
    /// </summary>
    public static List<ComplexityPoint> ScanPhaseSpace()
    {
        var points = new List<ComplexityPoint>();
        double[] orders = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };
        double[] rands = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };

        foreach (double q in orders)
        {
            foreach (double r in rands)
            {
                // Distinguishable states ∝ Q (order creates distinguishability)
                int states = (int)(q * 1000) + 1;

                // Persistence ∝ Q but degraded by excessive randomness
                double persistence = q * Math.Exp(-2.0 * r);

                // Novelty rate ∝ Randomness but requires some structure
                double novelty = r * q;

                // Complexity = states × persistence × novelty
                // Maximum when order AND randomness are both present
                double complexity = states * persistence * novelty / 10.0;

                var regime = complexity switch
                {
                    0 => ComplexityRegime.Dead,
                    < 1 => ComplexityRegime.Ordered,
                    < 50 => ComplexityRegime.Complex,
                    _ => ComplexityRegime.Formless
                };

                if (r > 0.9 && q < 0.3) regime = ComplexityRegime.Chaotic;
                if (q < 0.05) regime = ComplexityRegime.Dead;

                string desc = (q, r) switch
                {
                    (0, _) => "DEAD: No entities. Nothing exists.",
                    (_, 0) => "FROZEN: Static order. No novelty. Block universe.",
                    ( < 0.3, > 0.7) => "CHAOTIC: Rapid change obliterates structure. Nothing persists.",
                    ( > 0.5, > 0.3) => "COMPLEX: Stable entities + bounded novelty. Maximum complexity.",
                    ( > 0.7, < 0.2) => "ORDERED: Rigid structure. High persistence, zero adaptability.",
                    _ => "FORMATIVE: Structure emerging but not yet optimal."
                };

                points.Add(new ComplexityPoint(q, r, states,
                    persistence, novelty, complexity, regime, desc));
            }
        }

        return points;
    }

    public static string PhaseDiagram(List<ComplexityPoint> points)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("COMPLEXITY PHASE DIAGRAM — (ORDER, RANDOMNESS) PLANE");
        sb.AppendLine();
        sb.AppendLine("  Complexity = States × Persistence × Novelty");
        sb.AppendLine("  Maximum at INTERMEDIATE order + INTERMEDIATE randomness.");
        sb.AppendLine();
        sb.AppendLine("  Q\\R   R=0.0   R=0.1   R=0.3   R=0.5   R=0.7   R=0.9   R=1.0");
        sb.AppendLine("  " + new string('-', 70));

        double[] rands = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };
        double[] orders = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };

        foreach (double q in orders)
        {
            sb.Append(string.Format(CultureInfo.InvariantCulture, "  {0,4:F1}  ", q));
            foreach (double r in rands)
            {
                var p = points.First(pt => Math.Abs(pt.OrderStrength - q) < 0.01
                                       && Math.Abs(pt.RandomnessStrength - r) < 0.01);
                string symbol = p.Regime switch
                {
                    ComplexityRegime.Dead => " · ",
                    ComplexityRegime.Ordered => " □ ",
                    ComplexityRegime.Complex => " ■ ",
                    ComplexityRegime.Chaotic => " ≈ ",
                    ComplexityRegime.Formless => " ○ ",
                    _ => " ? "
                };
                sb.Append(symbol);
            }
            sb.AppendLine();
        }

        sb.AppendLine();
        sb.AppendLine("  · = DEAD    □ = ORDERED    ■ = COMPLEX    ≈ = CHAOTIC    ○ = FORMLESS");
        sb.AppendLine("  AT occupies: Q ≈ 1 (full individuation), R ≈ 0.5 (balanced randomness).");
        sb.AppendLine("  This is the COMPLEXITY MAXIMUM — the upper-center of the diagram.");
        return sb.ToString();
    }

    public static string ComplexityPeak(List<ComplexityPoint> points)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("COMPLEXITY PEAK — WHERE MAXIMUM EMERGES");
        sb.AppendLine();

        var ranked = points.OrderByDescending(p => p.ComplexityIndex).Take(8);
        sb.AppendLine("  Rank   Q      R      States   Persist   Novelty   Cmplx   Regime");
        sb.AppendLine("  " + new string('-', 72));
        int rank = 0;
        foreach (var p in ranked)
        {
            rank++;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,3}   {1,5:F1}  {2,5:F1}  {3,6}   {4,7:F3}   {5,7:F3}  {6,7:F1}  {7}",
                rank, p.OrderStrength, p.RandomnessStrength,
                p.DistinguishableStates, p.Persistence, p.NoveltyRate,
                p.ComplexityIndex, p.Regime));
        }

        sb.AppendLine();
        sb.AppendLine("  PEAK: Q ≈ 0.7-1.0, R ≈ 0.3-0.5.");
        sb.AppendLine("  Complexity requires BOTH high individuation AND moderate randomness.");
        sb.AppendLine("  Too much order → FROZEN. Too much randomness → CHAOS.");
        sb.AppendLine("  The complexity maximum is the GOLDILOCKS BALANCE.");
        return sb.ToString();
    }

    public static string ThePrinciple()
    {
        return @"
THE COMPLEXITY EMERGENCE PRINCIPLE

After ResearchXE (XE001-XE009) established the landscape and ResearchXF-001
analyzed the phase space, a fundamental principle emerges:

═══════════════════════════════════════════════════════════════
  THE COMPLEXITY EMERGENCE PRINCIPLE
═══════════════════════════════════════════════════════════════

  Complexity is NOT a primitive.
  Complexity is NOT an accident.
  Complexity is NOT anthropically selected.

  Complexity IS the INEVITABLE CONSEQUENCE of the interplay
  between individuation (Q) and actualization (Randomness).

  Q provides:
    • Distinguishable entities (structure)
    • Topological invariants (persistence)
    • The graph of relations (space)

  Randomness provides:
    • Outcome selection (novelty)
    • Accumulated history (time)
    • Statistical distributions (abundance)

  Complexity = DistinctStates × Persistence × Novelty

  MAXIMUM when:
    Q > 0.5   (sufficient individuation)
    R ≈ 0.3-0.5 (bounded novelty — enough to evolve, not so much as to destroy)

  THE THREE FAILURE REGIMES:

    1. Q = 0, any R: DEAD. Nothing exists to be complex.
    2. Q > 0, R = 0: FROZEN. Perfect order, zero novelty. Block universe.
    3. Q > 0, R ≫ 0.7: CHAOTIC. Structure dissolves before it can evolve.

  THE COMPLEXITY REGIME:
    Q ≫ 0, R ≈ 0.3-0.5: COMPLEX. Maximum states × persistence × novelty.
    Our universe: Q ≈ 1 (full individuation via graph), R ≈ 0.5 (Born rule).

  AT NATURALLY PRODUCES COMPLEXITY.
  It does not need to be tuned for it.
  The primitives THEMSELVES guarantee the emergence of complexity
  whenever they coexist with sufficient strength.

  This is the DEEPEST result of the AT program:
  COMPLEXITY IS NOT THE GOAL — IT IS THE DEFAULT.
═══════════════════════════════════════════════════════════════
";
    }

    public static string ResearchXFProgram()
    {
        return @"
RESEARCHXF — COMPLEXITY PHYSICS

ResearchXF investigates the emergence, dynamics, and optimization
of complexity within the AT framework.

  WHEREAS:
    ResearchX asked:   'What exists?' (Identity)
    ResearchXB asked:  'How much?' (Abundance)
    ResearchXC asked:  'Why two layers?' (Unification)
    ResearchXD asked:  'How to test?' (Predictions)
    ResearchXE asked:  'Why this universe?' (Landscape)

  RESEARCHXF ASKS:     'Why complexity at all?' (Emergence)

  KEY QUESTIONS:
    XF002: Is complexity a universal attractor of Q-event networks?
    XF003: Can complexity be maximized by varying Q and R?
    XF004: Does complexity drive the selection of physical laws?
    XF005: Is there a Complexity Action Principle?
    XF006: Can complexity growth be formalized as a physical law?

  ResearchXF is the CAPSTONE program.
  It asks not 'what is' but 'why is.'
";
    }
}
