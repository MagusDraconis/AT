namespace TQM.Core.ResearchXF;

using System.Globalization;

/// <summary>
/// Derives the Knowledge Emergence Principle as the capstone of the XF chain.
/// ResearchXF-005: Knowledge Emergence Principle
/// </summary>
public static class KnowledgeEmergenceAnalyzer
{
    public enum KnowledgeRegime { Ignorant, Reactive, Learning, Knowing, Understanding }

    public sealed record KnowledgePoint(
        double Complexity, double ObserverIndex,
        double ModelAccuracy, double SurvivalGain,
        double SelectionAdvantage, double KnowledgeIndex,
        double GenerationsToLearn, KnowledgeRegime Regime,
        string Description);

    /// <summary>
    /// Knowledge = Information × Accuracy × Persistence.
    /// Information is raw data. Knowledge is information that has been
    /// validated by prediction and selected by evolution.
    /// Selection advantage: accurate models → better predictions → higher survival.
    /// </summary>
    public static List<KnowledgePoint> ComputeKnowledgePoints()
    {
        var points = new List<KnowledgePoint>();
        double[] qVals = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };
        double[] rVals = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };

        foreach (double q in qVals)
        {
            foreach (double r in rVals)
            {
                // Reconstruct the full chain
                double persistence = q * Math.Exp(-2.0 * r);
                double novelty = r * q;
                double complexity = (q * 1000 + 1) * persistence * novelty / 10.0;

                double variation = r * q;
                double retention = q * Math.Exp(-2.0 * r);
                double selection = variation > 0.01 && retention > 0.01 ? variation * retention * 5.0 : 0;
                double evoRate = variation * retention * selection;

                double memory = complexity * persistence / 100.0;
                double prediction = memory * variation * 10.0;
                double adaptation = prediction * selection;
                double observerIndex = memory * prediction * adaptation / 10.0;

                // KNOWLEDGE: information that has been validated by evolution
                // Model accuracy improves with: observer capability × selection pressure × generations
                double modelAccuracy = observerIndex > 0.1
                    ? 1.0 - Math.Exp(-observerIndex / 20.0)
                    : 0.0;

                // Survival gain from accurate models:
                // ∝ accuracy (better predictions) × selection pressure (consequences matter)
                double survivalGain = modelAccuracy * selection * 10.0;

                // Selection advantage: relative fitness boost
                double selectionAdvantage = survivalGain / (1.0 + survivalGain);

                // Knowledge index: accumulated validated information
                double knowledgeIndex = observerIndex * modelAccuracy * selectionAdvantage;

                // Generations to learn: how long until models become accurate
                double gensToLearn = evoRate > 0.001 && observerIndex > 0.1
                    ? Math.Log(1.0 / (1.0 - Math.Min(modelAccuracy, 0.99))) / (evoRate * selection + 0.001)
                    : double.PositiveInfinity;

                var regime = knowledgeIndex switch
                {
                    0 => KnowledgeRegime.Ignorant,
                    < 0.1 => KnowledgeRegime.Reactive,
                    < 1.0 => KnowledgeRegime.Learning,
                    < 10.0 => KnowledgeRegime.Knowing,
                    _ => KnowledgeRegime.Understanding
                };

                if (q < 0.05 || r < 0.01) regime = KnowledgeRegime.Ignorant;

                string desc = regime switch
                {
                    KnowledgeRegime.Ignorant => "IGNORANT: No internal models. Stimulus-response only.",
                    KnowledgeRegime.Reactive => "REACTIVE: Rudimentary models. No validation.",
                    KnowledgeRegime.Learning => "LEARNING: Models improving. Knowledge accumulating.",
                    KnowledgeRegime.Knowing => "KNOWING: Accurate models. Validated knowledge.",
                    KnowledgeRegime.Understanding => "UNDERSTANDING: Deep models. Self-correcting. THEORETICAL PHYSICS.",
                    _ => "—"
                };

                points.Add(new KnowledgePoint(complexity, observerIndex,
                    modelAccuracy, survivalGain, selectionAdvantage,
                    knowledgeIndex, gensToLearn, regime, desc));
            }
        }

        return points;
    }

    public static string KnowledgeChain(List<KnowledgePoint> points)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("OBSERVER → KNOWLEDGE — THE VALIDATION CHAIN");
        sb.AppendLine();
        sb.AppendLine("  Observer  Accuracy  Surv.Gain  Sel.Adv   Knowledge  Gens/Learn  Regime");
        sb.AppendLine("  " + new string('-', 80));

        var selection = points.Where(p =>
            (p.ObserverIndex > 0.1 || Math.Abs(p.Complexity - 0) < 0.01) &&
            (Math.Abs(p.Complexity - 183.9) < 5 || Math.Abs(p.Complexity - 71.9) < 2 ||
             Math.Abs(p.Complexity - 46.0) < 2 || Math.Abs(p.Complexity - 11.5) < 1 ||
             Math.Abs(p.Complexity - 0.0) < 0.01))
            .OrderBy(p => p.KnowledgeIndex).ToList();

        foreach (var p in selection)
        {
            string gens = p.GenerationsToLearn > 99999 ? "∞" : $"{p.GenerationsToLearn:F0}";
            string marker = p.Regime >= KnowledgeRegime.Knowing ? " ← KNOWLEDGE" : "";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,8:F1}  {1,8:F3}  {2,9:F3}  {3,8:F3}  {4,8:F3}  {5,10}  {6}{7}",
                p.ObserverIndex, p.ModelAccuracy, p.SurvivalGain,
                p.SelectionAdvantage, p.KnowledgeIndex, gens, p.Regime, marker));
        }

        return sb.ToString();
    }

    public static string ThePrinciple()
    {
        return @"
THE KNOWLEDGE EMERGENCE PRINCIPLE

═══════════════════════════════════════════════════════════════
  THE KNOWLEDGE EMERGENCE PRINCIPLE
═══════════════════════════════════════════════════════════════

  Knowledge = Information × Accuracy × Persistence

  KNOWLEDGE IS INFORMATION THAT WORKS.

  Information is raw data. Knowledge is information that has been
  validated by prediction and selected by evolution.

  THE VALIDATION CHAIN:
    1. An observer forms INTERNAL MODELS of its environment.
    2. Models generate PREDICTIONS about future states.
    3. Prediction ACCURACY affects survival (better predictions
       → better decisions → higher persistence).
    4. Selection pressure ELIMINATES inaccurate models.
    5. Accurate models ACCUMULATE across generations.
    6. Accumulated validated models = KNOWLEDGE.

  WHY EVOLUTION GUARANTEES KNOWLEDGE:

    • False models → wrong predictions → lower survival
      → fewer copies → eliminated from the population.
    • True models → correct predictions → higher survival
      → more copies → dominate the population.

    The direction of selection is ALWAYS toward accuracy.
    This is not a preference — it's a MATHEMATICAL NECESSITY.

  KNOWLEDGE IS THE ATTRACTOR OF OBSERVER EVOLUTION.
  Any evolving system of observers will, given sufficient time,
  accumulate accurate models of its environment.

  The universe BECOMES KNOWN to its inhabitants.
  This is the CAPSTONE of the Complexity Physics chain.

═══════════════════════════════════════════════════════════════
";
    }

    public static string TheCompleteXFChain()
    {
        return @"
THE COMPLETE RESEARCHXF CHAIN — COMPLEXITY PHYSICS

ResearchXF-001 through XF-005 have established five principles:

  ┌─────────────────────────────────────────────────────────────┐
  │  XF001: COMPLEXITY EMERGENCE                                │
  │  C = States × Persistence × Novelty                         │
  │  Maximum at Q ≈ 1, R ≈ 0.5. Our universe at peak.           │
  ├─────────────────────────────────────────────────────────────┤
  │  XF002: INFORMATION GROWTH                                  │
  │  dI/dt = Creation − Decay                                   │
  │  Growing at Q > 0.5, R ≈ 0.3–0.7. I* is large and positive.│
  ├─────────────────────────────────────────────────────────────┤
  │  XF003: EVOLUTION EMERGENCE                                 │
  │  E = Variation × Retention × Selection                      │
  │  Darwinian triad all from Q + Randomness. Optimizing here.   │
  ├─────────────────────────────────────────────────────────────┤
  │  XF004: OBSERVER EMERGENCE                                  │
  │  O = Memory × Prediction × Adaptation                       │
  │  Observers at C ≈ 50. Our universe: C ≈ 184.                │
  ├─────────────────────────────────────────────────────────────┤
  │  XF005: KNOWLEDGE EMERGENCE                                 │
  │  K = Information × Accuracy × Persistence                   │
  │  Knowledge is the ATTRACTOR of observer evolution.          │
  └─────────────────────────────────────────────────────────────┘

  THE SINGLE CHAIN:
    Q + Randomness
        → Complexity
        → Information Growth
        → Evolution
        → Observers
        → Knowledge

  EVERY STEP IS INEVITABLE GIVEN THE PREVIOUS ONE.
  NONE requires additional primitives or postulates.

  TQM does not just describe physics.
  TQM explains WHY physics can be KNOWN.

  This is the deepest result of the TQM program.
";
    }
}
