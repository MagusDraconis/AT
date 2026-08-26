using System.Globalization;

namespace AT.Core.ResearchQG;

public static class NegativeLengthTimeAnalyzer
{
    const double ell = 1.616255e-35;
    const double tau = 5.391247e-44;
    const double c_c = 2.99792458e8;

    public static NTResult RunFullAnalysis()
    {
        var args = new NegArg[]
        {
            new NegArg("Spatial distance","ℓ < 0","Distance is a METRIC: d(x,y) ≥ 0 by definition. 'Negative distance' is a category error — like asking for a negative area or a negative probability. ℓ is a LENGTH, not a coordinate.","D: CATEGORY ERROR. Distance is non-negative by definition."),
            new NegArg("Coordinate position","x < 0","Coordinate sign is CONVENTION. x → -x is a reflection — physically identical. But the distance BETWEEN points (|x₁-x₂|) remains ≥ 0. ℓ is a distance, not a coordinate.","A: COORDINATE CONVENTION. Position sign is arbitrary. Distance sign is impossible."),
            new NegArg("Causal ordering","Negative causal interval","Causal structure requires partial ordering. If ds² < 0 for two Q-events, they cannot be causally connected. But the minimal interval ℓ is a MAGNITUDE — it defines the threshold below which no two events can be distinct.","D: LOGICAL CONTRADICTION. Causal separation can be spacelike. Causal threshold cannot be negative."),
            new NegArg("Temporal interval","τ < 0","Duration is the measure of interval between successive Q-events. τ is the MINIMAL interval. Duration ≥ 0 by definition. Negative duration = event B occurs before event A while being AFTER event A. Contradiction.","D: LOGICAL CONTRADICTION. τ < 0 → succession runs backward → definitional impossibility."),
            new NegArg("Time coordinate","t < 0","Coordinate time sign is CONVENTION. t → -t is time reversal — formally valid in many equations (CPT). But the INTERVAL τ between actualization events is invariant. You can label times negatively; you cannot have negative intervals.","A: COORDINATE CONVENTION. Time coordinate can be negative. Time interval cannot."),
            new NegArg("Causal propagation","c < 0","c = ℓ/τ. If both ℓ and τ are positive, c > 0. If one were negative, c < 0 — but signal propagation speed is a magnitude. Negative speed = signal arrives before it was sent = closed timelike curve = causal paradox.","D: LOGICAL CONTRADICTION. c is a speed — magnitude, non-negative. c = ℓ/τ with both > 0 → c > 0."),
            new NegArg("Oscillation frequency","ω < 0","ω = 2π/τ. τ < 0 → ω < 0. But negative frequency = e^{-i|ω|t} = e^{+i|ω|(-t)} — mathematically equivalent to positive frequency with time reversal. No new physics. Just reversed phase rotation convention.","A: CONVENTION EQUIVALENCE. Negative frequency = phase reversal. Same physics."),
            new NegArg("Phase gradient","∇θ < 0","Phase gradient SIGN is physically meaningful (QG-029: +∇θ = attraction, -∇θ = repulsion). But ∇θ derives from spatial arrangement of Q-events, not from ℓ < 0. Phase can decrease in space — that's a CONFIGURATION, not a fundamental sign flip.","B: WEAK PHYSICAL MEANING. Phase gradient sign is real. ℓ sign is not the source."),
            new NegArg("Action","ħ < 0","ħ has units [L²·M/T]. If ℓ < 0 → ℓ² > 0 → ħ still positive (ℓ² masks sign). If τ < 0 → ħ < 0. But ħ is a QUANTUM of action — a minimum magnitude. Negative action = backwards quantum evolution = anti-unitary = formally valid (Wigner) but physically equivalent.","B: WEAK PHYSICAL MEANING. Action sign = evolution direction. ℓ² positivity masks ℓ sign."),
        };

        return new NTResult(BuildA(),BuildB(),BuildC(),BuildD(),BuildE(),BuildF(args),args);
    }

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE LOGICAL STATUS OF ℓ AND τ");
        sb.AppendLine();
        sb.AppendLine("  ℓ = 1.616×10⁻³⁵ m — minimal causal resolution (spatial grain)");
        sb.AppendLine("  τ = 5.391×10⁻⁴⁴ s — minimal actualization interval (temporal grain)");
        sb.AppendLine();
        sb.AppendLine("  CATEGORY ANALYSIS:");
        sb.AppendLine();
        sb.AppendLine("  ℓ is a DISTANCE — an instance of a METRIC.");
        sb.AppendLine("  A metric d(x,y) satisfies:");
        sb.AppendLine("    1. d(x,y) ≥ 0           (non-negativity)");
        sb.AppendLine("    2. d(x,y) = 0 ↔ x = y   (identity of indiscernibles)");
        sb.AppendLine("    3. d(x,y) = d(y,x)      (symmetry)");
        sb.AppendLine("    4. d(x,z) ≤ d(x,y)+d(y,z) (triangle inequality)");
        sb.AppendLine();
        sb.AppendLine("  Axiom 1 is NOT optional. It is part of the DEFINITION of distance.");
        sb.AppendLine("  'Negative distance' is no more meaningful than 'negative probability'");
        sb.AppendLine("  or 'negative area'. It is a category error.");
        sb.AppendLine();
        sb.AppendLine("  τ is a DURATION — an instance of a TEMPORAL METRIC.");
        sb.AppendLine("  The same axioms apply: duration ≥ 0, identity, symmetry, triangle.");
        sb.AppendLine();
        sb.AppendLine("  KEY INSIGHT:");
        sb.AppendLine("    ℓ > 0 and τ > 0 are not PHYSICAL discoveries.");
        sb.AppendLine("    They are DEFINITIONAL consequences of what ℓ and τ ARE.");
        sb.AppendLine("    ℓ is a distance. Distances are ≥ 0. QED.");
        sb.AppendLine("    τ is a duration. Durations are ≥ 0. QED.");
        sb.AppendLine();
        sb.AppendLine("  The sign is not a free parameter — it's forbidden by category.");
        return sb.ToString();
    }

    static string BuildB()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("NEGATIVE LENGTH: COORDINATE VS METRIC");
        sb.AppendLine();
        sb.AppendLine("  CONFUSION 1: Conflating POSITION with DISTANCE.");
        sb.AppendLine();
        sb.AppendLine("  Position x: can be negative, zero, or positive.");
        sb.AppendLine("    This is a COORDINATE CONVENTION. Origin is arbitrary.");
        sb.AppendLine("    x → -x is a spatial reflection. Physics is unchanged.");
        sb.AppendLine();
        sb.AppendLine("  Distance d(x₁, x₂) = |x₁ - x₂| ≥ 0 ALWAYS.");
        sb.AppendLine("    This is a METRIC. It is not conventional — it's definitional.");
        sb.AppendLine("    No coordinate transformation can make distance negative.");
        sb.AppendLine();
        sb.AppendLine("  ℓ IS A DISTANCE, NOT A POSITION.");
        sb.AppendLine("  ℓ = minimal distance between two distinct Q-events.");
        sb.AppendLine("  ℓ < 0 would mean: the minimal separation between");
        sb.AppendLine("    two distinct events is negative.");
        sb.AppendLine("  This is MEANINGLESS. Events cannot be 'negatively far apart'.");
        sb.AppendLine();
        sb.AppendLine("  PHYSICAL MEANING OF ℓ < 0: NONE.");
        sb.AppendLine("  It is a category error — applying coordinate conventions");
        sb.AppendLine("  to a quantity defined as a metric.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("NEGATIVE TIME: THE ARROW OF BECOMING");
        sb.AppendLine();
        sb.AppendLine("  CONFUSION 2: Conflating COORDINATE TIME with DURATION.");
        sb.AppendLine();
        sb.AppendLine("  Coordinate t: can be negative (before some epoch).");
        sb.AppendLine("    t → -t = time reversal. Formally valid in many equations.");
        sb.AppendLine("    CPT theorem: physics is symmetric under combined C,P,T.");
        sb.AppendLine();
        sb.AppendLine("  Duration Δt = t₂ - t₁: ALWAYS ≥ 0 for causally ordered events.");
        sb.AppendLine("    τ IS A DURATION — the minimal interval between");
        sb.AppendLine("    successive actualization events.");
        sb.AppendLine();
        sb.AppendLine("  τ < 0 WOULD MEAN:");
        sb.AppendLine("    The NEXT Q-event occurs BEFORE the current one.");
        sb.AppendLine("    But 'next' MEANS 'after' — this is a definitional contradiction.");
        sb.AppendLine("    Succession cannot run backward. Becoming cannot un-become.");
        sb.AppendLine();
        sb.AppendLine("  AT CONTEXT:");
        sb.AppendLine("    Q is the process of BECOMING.");
        sb.AppendLine("    Becoming has a direction: potential → actual.");
        sb.AppendLine("    τ < 0 = actual → potential = UN-BECOMING.");
        sb.AppendLine("    This contradicts the very definition of Q.");
        sb.AppendLine();
        sb.AppendLine("  PHYSICAL MEANING OF τ < 0: LOGICAL CONTRADICTION.");
        sb.AppendLine("    The arrow of becoming is intrinsic to actualization.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CAUSALITY UNDER ℓ < 0 AND τ < 0");
        sb.AppendLine();
        sb.AppendLine("  CAUSAL STRUCTURE IN AT:");
        sb.AppendLine("    Q-events form a partially ordered set (causal set).");
        sb.AppendLine("    x ≺ y means: x is in the causal past of y.");
        sb.AppendLine("    ≺ is: transitive, acyclic, locally finite.");
        sb.AppendLine();
        sb.AppendLine("  WITH τ < 0:");
        sb.AppendLine("    If event A at time t has next actualization at t-τ,");
        sb.AppendLine("    then the successor of A is in A's causal PAST.");
        sb.AppendLine("    This creates a cycle: A ≺ B ≺ A.");
        sb.AppendLine("    The partial order collapses. Causality is destroyed.");
        sb.AppendLine();
        sb.AppendLine("  WITH ℓ < 0:");
        sb.AppendLine("    Minimal spatial separation is negative.");
        sb.AppendLine("    Two events at distance |ℓ| would be distinct,");
        sb.AppendLine("    but events at distance -|ℓ| would be... what?");
        sb.AppendLine("    'Closer than identical' — a contradiction.");
        sb.AppendLine("    The metric space collapses to a single point.");
        sb.AppendLine();
        sb.AppendLine("  COMBINED: ℓ < 0, τ < 0 → c = (-ℓ)/(-τ) = c > 0.");
        sb.AppendLine("    Signs cancel! c could remain positive.");
        sb.AppendLine("    But ℓ and τ individually are still nonsensical.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: τ < 0 DESTROYS CAUSALITY.");
        sb.AppendLine("    ℓ < 0 DESTROYS DISTINCTNESS.");
        sb.AppendLine("    Neither is physically viable.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("OSCILLATION AND PHASE WITH τ < 0");
        sb.AppendLine();
        sb.AppendLine("  OSCILLATION: ω = 2π/τ.");
        sb.AppendLine();
        sb.AppendLine("  τ < 0 → ω < 0 → e^{-i|ω|t} = cos(|ω|t) + i·sin(|ω|t)");
        sb.AppendLine("  vs τ > 0 → ω > 0 → e^{+i|ω|t} = cos(|ω|t) - i·sin(|ω|t)");
        sb.AppendLine();
        sb.AppendLine("  The sign change flips the direction of phase rotation.");
        sb.AppendLine("  But this is equivalent to replacing t → -t in the wavefunction.");
        sb.AppendLine();
        sb.AppendLine("  PHYSICAL INTERPRETATION:");
        sb.AppendLine("    Negative frequency = phase rotates 'backwards' in time.");
        sb.AppendLine("    Mathematically: e^{-iωt} and e^{+iωt} are both valid solutions.");
        sb.AppendLine("    Convention: positive frequency = particle, negative = antiparticle.");
        sb.AppendLine("    This is a CONVENTION, not new physics (Feynman-Stückelberg).");
        sb.AppendLine();
        sb.AppendLine("  AT CONTEXT:");
        sb.AppendLine("    Oscillation = temporal succession at interval τ (QG-026).");
        sb.AppendLine("    τ < 0 means succession runs backward.");
        sb.AppendLine("    The phase rotation DIRECTION flips but the STRUCTURE of");
        sb.AppendLine("    oscillation (the fact of rhythm) remains unchanged.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT:");
        sb.AppendLine("    τ < 0 → mathematically consistent negative frequency.");
        sb.AppendLine("    But τ is a DURATION, not a frequency parameter.");
        sb.AppendLine("    The category error persists: duration < 0 is undefined.");
        return sb.ToString();
    }

    static string BuildF(NegArg[] args)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("DOMAIN-BY-DOMAIN AUDIT");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-24} {1,-18} {2}", "Domain", "Negative value", "Verdict"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var a in args)
        {
            string v = a.Verdict.Length > 75 ? a.Verdict[..72]+"..." : a.Verdict;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-24} {1,-18} {2}", a.Domain, a.NegativeValue, v));
        }
        sb.AppendLine();
        sb.AppendLine("  SUMMARY:");
        sb.AppendLine("    Category errors (D): Distance, duration, causal threshold, speed. (4)");
        sb.AppendLine("    Coordinate conventions (A): Position, time coordinate. (2)");
        sb.AppendLine("    Convention equivalence (A): Frequency sign. (1)");
        sb.AppendLine("    Weak physical meaning (B): Phase gradient, action. (2)");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  FINAL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  ℓ < 0: CATEGORY ERROR. Distance is a metric. d ≥ 0 by definition.");
        sb.AppendLine("  τ < 0: LOGICAL CONTRADICTION. Duration cannot be negative.");
        sb.AppendLine("         Becoming cannot run backward. Succession is directed.");
        sb.AppendLine();
        sb.AppendLine("  ℓ > 0 and τ > 0 are DEFINITIONAL TRUTHS, not empirical discoveries.");
        sb.AppendLine("  They follow from the categories:");
        sb.AppendLine("    ℓ = minimal DISTANCE → ℓ ≥ 0 → ℓ > 0 (QG-009: events distinct → ℓ ≠ 0)");
        sb.AppendLine("    τ = minimal DURATION → τ ≥ 0 → τ > 0 (QG-011: succession → τ ≠ 0)");
        sb.AppendLine();
        sb.AppendLine("  THE POSITIVITY OF ℓ AND τ IS INEVITABLE.");
        sb.AppendLine("  The sign question was already answered by the metric axioms.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: D — LOGICAL CONTRADICTION");
        sb.AppendLine("  ℓ < 0 and τ < 0 are NOT coordinate conventions.");
        sb.AppendLine("  They are violations of the definition of distance and duration.");
        sb.AppendLine("  QG program: 32 experiments.");
        return sb.ToString();
    }
}
