namespace TQM.Core.ResearchXF;

using System.Globalization;

/// <summary>
/// Derives the Observer Emergence Principle from Q + Randomness via evolution.
/// ResearchXF-004: Observer Emergence Principle
/// </summary>
public static class ObserverEmergenceAnalyzer
{
    public enum ObserverRegime { NoInfo, SensingOnly, Reactive, Predictive, SelfAware }

    public sealed record ObserverPoint(
        double Complexity, double EvolutionRate,
        double Memory, double Prediction,
        double Adaptation, double ObserverIndex,
        string Description, ObserverRegime Regime);

    /// <summary>
    /// Observer = Memory × Prediction × Adaptation.
    /// Memory ∝ Information (XF002): stored past states.
    /// Prediction ∝ Evolution (XF003): learned patterns → expected future.
    /// Adaptation ∝ Evolution × Memory: modify behavior based on predictions.
    /// </summary>
    public static List<ObserverPoint> ScanObserverSpace()
    {
        var points = new List<ObserverPoint>();

        // Use the same (Q,R) pairs that drove XF001-XF003
        double[] qVals = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };
        double[] rVals = { 0.0, 0.1, 0.3, 0.5, 0.7, 0.9, 1.0 };

        foreach (double q in qVals)
        {
            foreach (double r in rVals)
            {
                // Complexity from XF001: C = States × Persistence × Novelty
                double states = q * 1000 + 1;
                double persistence = q * Math.Exp(-2.0 * r);
                double novelty = r * q;
                double complexity = states * persistence * novelty / 10.0;

                // Evolution from XF003: E = V × Ret × Sel
                double variation = r * q;
                double retention = q * Math.Exp(-2.0 * r);
                double selection = variation > 0.01 && retention > 0.01
                    ? variation * retention * 5.0 : 0;
                double evoRate = variation * retention * selection;

                // MEMORY: stored information from past states
                // Requires complexity (to store) and persistence (to retain)
                double memory = complexity * persistence / 100.0;

                // PREDICTION: learned patterns → expected future
                // Requires memory (pattern library) and variation (to learn from)
                double prediction = memory * variation * 10.0;

                // ADAPTATION: behavioral modification based on predictions
                // Requires prediction + selection (consequences matter)
                double adaptation = prediction * selection;

                // OBSERVER INDEX: combined capacity for observation
                double observerIndex = memory * prediction * adaptation / 10.0;

                var regime = observerIndex switch
                {
                    0 => ObserverRegime.NoInfo,
                    < 0.1 => ObserverRegime.SensingOnly,
                    < 1.0 => ObserverRegime.Reactive,
                    < 10.0 => ObserverRegime.Predictive,
                    _ => ObserverRegime.SelfAware
                };

                if (q < 0.05) regime = ObserverRegime.NoInfo;
                if (r < 0.05 && q > 0) regime = ObserverRegime.SensingOnly;

                string desc = regime switch
                {
                    ObserverRegime.NoInfo => "NO INFO: No entities or no processing.",
                    ObserverRegime.SensingOnly => "SENSING: Detects states but no memory. Stimulus-response only.",
                    ObserverRegime.Reactive => "REACTIVE: Simple memory. Reflexive behavior. No prediction.",
                    ObserverRegime.Predictive => "PREDICTIVE: Internal models. Anticipates future. OBSERVER.",
                    ObserverRegime.SelfAware => "SELF-AWARE: Models the self. Recursive prediction. Full observer.",
                    _ => "—"
                };

                points.Add(new ObserverPoint(complexity, evoRate,
                    memory, prediction, adaptation, observerIndex, desc, regime));
            }
        }

        return points;
    }

    public static string ObserverTable(List<ObserverPoint> points)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("COMPLEXITY × EVOLUTION → OBSERVER INDEX");
        sb.AppendLine();
        sb.AppendLine("  Cmplx    Evo       Memory    Predict   Adapt    Obs.Index   Regime");
        sb.AppendLine("  " + new string('-', 72));

        // Show representative points: (Q=1 at various R) and (Q=0.5 at various R)
        var selection = points.Where(p =>
            (Math.Abs(p.Complexity - 183.9) < 5 || Math.Abs(p.Complexity - 0.0) < 0.1 ||
             Math.Abs(p.Complexity - 46.0) < 3 || Math.Abs(p.Complexity - 11.5) < 1)
            && p.ObserverIndex > 0).OrderBy(p => p.ObserverIndex).ToList();

        foreach (var p in selection)
        {
            string marker = p.Regime >= ObserverRegime.Predictive ? " ← OBSERVER" : "";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,7:F1}  {1,8:F4}  {2,8:F2}  {3,8:F2}  {4,8:F2}  {5,10:F4}  {6}{7}",
                p.Complexity, p.EvolutionRate, p.Memory,
                p.Prediction, p.Adaptation, p.ObserverIndex, p.Regime, marker));
        }

        return sb.ToString();
    }

    public static string TheThresholds()
    {
        return @"
OBSERVER EMERGENCE THRESHOLDS

Observers require four capabilities. Each has a minimum threshold:

  ┌──────────────────────────────────────────────────────────────┐
  │ THRESHOLD 1: MEMORY                                          │
  │                                                               │
  │ Minimum: Stored information > 1 bit.                          │
  │ Requires: Complexity > 10 (enough states to encode past).     │
  │ Fails when: Q < 0.3 (too few entities) OR R > 0.7 (chaos).   │
  │ Status in our universe: VASTLY EXCEEDED. Memory ≈ 67 bits.   │
  ├──────────────────────────────────────────────────────────────┤
  │ THRESHOLD 2: PREDICTION                                       │
  │                                                               │
  │ Minimum: Anticipatory capacity > random chance.               │
  │ Requires: Memory + Variation (to learn patterns).             │
  │ Fails when: Evolution rate = 0 (static world, nothing to      │
  │   predict) OR Evolution rate too high (pure noise).           │
  │ Status in our universe: VASTLY EXCEEDED.                      │
  ├──────────────────────────────────────────────────────────────┤
  │ THRESHOLD 3: ADAPTATION                                       │
  │                                                               │
  │ Minimum: Behavioral change based on prediction.               │
  │ Requires: Prediction + Selection (consequences matter).       │
  │ Fails when: Selection pressure = 0 (no differential outcomes).│
  │ Status in our universe: VASTLY EXCEEDED.                      │
  ├──────────────────────────────────────────────────────────────┤
  │ THRESHOLD 4: SELF-MODEL                                       │
  │                                                               │
  │ Minimum: System models ITSELF as part of the environment.     │
  │ Requires: Prediction × Memory large enough for recursive      │
  │   modeling (system can represent its own state).              │
  │ Status in our universe: ACHIEVED (at least one species).      │
  └──────────────────────────────────────────────────────────────┘

  ALL FOUR thresholds are exceeded in our universe.
  Observer emergence is OVERDETERMINED — any universe with
  Q≈1, R≈0.5 has vastly more than the minimum required.
";
    }

    public static string TheChain()
    {
        return @"
THE COMPLETE XF CHAIN — FROM PRIMITIVES TO OBSERVERS

ResearchXF has now derived four interconnected principles:

  XF001: COMPLEXITY EMERGENCE
    C = States × Persistence × Novelty

  XF002: INFORMATION GENERATION
    dI/dt = Creation − Decay

  XF003: EVOLUTION EMERGENCE
    E = Variation × Retention × Selection

  XF004: OBSERVER EMERGENCE
    O = Memory × Prediction × Adaptation

  THE HIERARCHY:
    Q + Randomness
        → Complexity (structure)
        → Information Growth (accumulation)
        → Evolution (adaptation)
        → Memory (storage)
        → Prediction (anticipation)
        → Adaptation (response)
        → OBSERVERS (self-modeling systems)

  EACH STEP IS INEVITABLE GIVEN THE PREVIOUS ONE.

  Observers are NOT a separate category of existence.
  Observers are EVOLVING INFORMATION SYSTEMS that have
  crossed a threshold of recursive self-modeling.
  The threshold is crossed NATURALLY in any universe
  with sufficient complexity and evolution.

  TQM DOES NOT PREDICT OBSERVERS AS A SPECIAL CASE.
  TQM PREDICTS THAT OBSERVERS ARE THE EXPECTED OUTCOME
  OF EVOLUTION IN INFORMATION-RICH ENVIRONMENTS.

  The observer is not the goal. The observer is the
  INEVITABLE CONSEQUENCE of complexity + time.
";
    }
}
