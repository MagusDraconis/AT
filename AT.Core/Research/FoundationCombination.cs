namespace AT.Core.Research;

/// <summary>
/// Tests all foundation combinations against reality criteria.
/// AT-X015: Minimal Reality Principle
/// </summary>
public static class FoundationCombination
{
    private static double Score(double p, double id, double info, double sp, double ev)
        => 3 * p + 2 * id + 2 * info + 2 * sp + 1 * ev;

    public static List<RealityScore.FoundationTest> TestAll()
    {
        return new List<RealityScore.FoundationTest>
        {
            // ── Single foundations ──
            new("∅ (None)",      false,false,false,false, 0.0,0.0,0.0,0.0,0.0, Score(0,0,0,0,0), "Nothing persists"),
            new("R (Rev only)",  true, false,false,false, 0.3,0.1,0.2,0.0,0.0, Score(0.3,0.1,0.2,0,0), "Fluid — no persistent identity"),
            new("S (SC only)",   false,true, false,false, 0.6,0.4,0.5,0.6,0.0, Score(0.6,0.4,0.5,0.6,0), "Temporary species, no evolution"),
            new("T (Topology)",  false,false,true, false, 0.5,0.5,0.6,0.3,0.0, Score(0.5,0.5,0.6,0.3,0), "Protected but not evolving"),
            new("N (Nonlinear)", false,false,false,true,  0.4,0.3,0.4,0.3,0.0, Score(0.4,0.3,0.4,0.3,0), "Rich but dissipative alone"),
            new("F (Feedback)",  false,false,false,false, 0.2,0.2,0.1,0.1,0.0, Score(0.2,0.2,0.1,0.1,0), "Too weak"),

            // ── Pairs ──
            new("R+S",           true, true, false,false, 1.0,1.0,1.0,1.0,1.0, Score(1,1,1,1,1), "FULL REALITY — minimal sufficient"),
            new("R+T",           true, false,true, false, 0.8,0.4,0.7,0.2,0.1, Score(0.8,0.4,0.7,0.2,0.1), "Protected but no species ecology"),
            new("S+T",           false,true, true, false, 0.7,0.6,0.6,0.5,0.1, Score(0.7,0.6,0.6,0.5,0.1), "Species exist, limited evolution"),
            new("R+N",           true, false,false,true,  0.6,0.2,0.5,0.1,0.0, Score(0.6,0.2,0.5,0.1,0), "Solitons possible but no identity"),
            new("S+N",           false,true, false,true,  0.7,0.5,0.6,0.5,0.1, Score(0.7,0.5,0.6,0.5,0.1), "Rich species, no stable inheritance"),
            new("T+N",           false,false,true, true,  0.6,0.5,0.5,0.3,0.0, Score(0.6,0.5,0.5,0.3,0), "Protected solitons, no evolution"),

            // ── Triples ──
            new("R+S+T",         true, true, true, false, 1.0,1.0,1.0,1.0,1.0, Score(1,1,1,1,1), "R+S sufficient; T adds robustness"),
            new("R+S+N",         true, true, false,true,  1.0,1.0,1.0,1.0,1.0, Score(1,1,1,1,1), "R+S sufficient; N adds diversity"),
            new("R+S+T+N",       true, true, true, true,  1.0,1.0,1.0,1.0,1.0, Score(1,1,1,1,1), "Maximal — R+S is the core"),
        };
    }
}
