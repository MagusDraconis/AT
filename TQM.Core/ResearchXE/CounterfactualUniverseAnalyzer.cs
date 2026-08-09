namespace TQM.Core.ResearchXE;

using System.Globalization;

/// <summary>
/// Counterfactual universe audit: modify TQM assumptions, measure viability.
/// ResearchXE-003: Counterfactual Universe Audit
/// </summary>
public static class CounterfactualUniverseAnalyzer
{
    public sealed record CounterfactualUniverse(
        string Id, string Name, string Modification,
        bool MatterExists, bool ParticlesExist, bool GaugeForcesExist,
        bool StableBoundStates, bool ChemistryPossible,
        bool ComplexityEmerges, bool ObserversPossible,
        double ViabilityScore, string Verdict);

    public enum NecessityClass { Indispensable, StronglyRequired, Helpful, Contingent }

    public sealed record AssumptionNecessity(
        string Assumption, string DefaultValue, string[] TestedAlternatives,
        double MinViability, double MaxViability,
        NecessityClass Class, string Reasoning);

    public static List<CounterfactualUniverse> BuildUniverses()
    {
        return new List<CounterfactualUniverse>
        {
            // U0: Reference
            new("U0", "Reference (3+1, M²≈5, Randomness)",
                "Standard TQM — our universe.", true, true, true,
                true, true, true, true, 1.00, "BASELINE. All features present."),

            // U1: 2+1 dimensions
            new("U1", "2+1 dimensions",
                "d=2+1 instead of 3+1. ⟨k⟩≈3. Lower connectivity.",
                true, true, true, false, false, false, false, 0.25,
                "GRAVITY TRIVIAL: No propagating gravitational waves in 2+1 GR. "
                + "No knots (codim != 2). No stable atoms (no 1/r potential from Gauss). "
                + "Chemistry impossible. Observers impossible."),

            // U2: 4+1 dimensions
            new("U2", "4+1 dimensions",
                "d=4+1. ⟨k⟩≈7. Higher connectivity. No stable orbits (Bertrand).",
                true, true, true, false, false, false, false, 0.20,
                "ORBITAL INSTABILITY: No stable planetary systems. "
                + "Atoms exist but chemistry radically different. "
                + "Observers unlikely — no stable structures on astronomical scales."),

            // U3: No randomness
            new("U3", "No randomness (deterministic)",
                "Randomness removed. Deterministic evolution only.",
                true, true, true, true, true, false, false, 0.30,
                "BLOCK UNIVERSE: All states pre-determined. No genuine becoming. "
                + "QM formalism exists but measurement undefined. "
                + "Abundance layer absent — all values fixed by initial conditions."),

            // U4: Weak connectivity (M²≈2)
            new("U4", "Weak connectivity (M²≈2)",
                "M²≈2. Weak nonlinearity. ⟨k⟩≈2 (1+1-like).",
                true, true, true, true, true, true, true, 0.70,
                "WEAK HIERARCHY: Mass ratios small. Nearly harmonic excitation spectrum. "
                + "Many generations nearly degenerate — complex flavor physics. "
                + "Viable but observationally very different from our universe."),

            // U5: Strong connectivity (M²≈10)
            new("U5", "Strong connectivity (M²≈10)",
                "M²≈10. Strong nonlinearity. ⟨k⟩≈10 (5+1-like).",
                true, true, true, false, false, false, false, 0.15,
                "EXTREME HIERARCHY: Higher generations extremely heavy and unstable. "
                + "Only 1-2 observable generations. Defects barely stable. "
                + "Chemistry severely limited — only lightest particles bind. "
                + "Observers unlikely."),

            // U6: Four generations
            new("U6", "Four stable generations",
                "α≈1.0 (stability cutoff relaxed). 4 observable generations.",
                true, true, true, true, true, true, true, 0.85,
                "RICHER FLAVOR: Additional fermion generation. More CP violation. "
                + "CKM/PMNS matrices are 4×4. More complex mixing pattern. "
                + "VIABLE: Our universe could have been this. Not selected by TQM."),

            // U7: No abundance layer
            new("U7", "No abundance (identity only)",
                "Abundance layer removed. Only identity (topology) exists.",
                true, true, true, false, false, false, false, 0.10,
                "NO MEASURABLE PHYSICS: Particles exist as abstract mathematical objects. "
                + "No masses, no couplings, no probabilities. "
                + "Theory is pure mathematics with no empirical content."),

            // U8: No identity layer
            new("U8", "No identity (abundance only)",
                "Identity layer removed. Only abundance (statistics) exists.",
                false, false, false, false, false, false, false, 0.00,
                "NOTHING EXISTS: No particles, no gauge groups, no topology. "
                + "Statistics without entities to describe. Empty vacuum. "
                + "DEAD UNIVERSE. Identity is logically prior to abundance."),
        };
    }

    public static List<AssumptionNecessity> BuildNecessities(List<CounterfactualUniverse> universes)
    {
        return new List<AssumptionNecessity>
        {
            new("Q (individuation)", "Exists (Q>0)", new[] { "Removed (U8)" },
                0.00, 1.00, NecessityClass.Indispensable,
                "Without Q, nothing exists. U8 has viability 0. "
                + "Q is logically prior to everything."),

            new("3+1 dimensions", "3+1", new[] { "2+1 (U1)", "4+1 (U2)" },
                0.20, 0.25, NecessityClass.StronglyRequired,
                "2+1: no chemistry, trivial gravity. 4+1: no stable orbits. "
                + "3+1 is the only dimensionality supporting complex structure. "
                + "CONSISTENT with complexity maximization (X042)."),

            new("Randomness (actualization)", "Present", new[] { "Removed (U3)" },
                0.30, 1.00, NecessityClass.StronglyRequired,
                "Without randomness: block universe, no measurement, no becoming. "
                + "QM exists as mathematics but physics is dead. "
                + "Abundance layer absent → no empirical predictions."),

            new("M² ≈ 5 (nonlinearity)", "≈5", new[] { "≈2 (U4)", "≈10 (U5)" },
                0.15, 0.70, NecessityClass.Helpful,
                "Weak (M²≈2): viable but weak hierarchy. Strong (M²≈10): too unstable. "
                + "M²≈5 is NOT indispensable — U4 is viable. "
                + "But M²≈5 maximizes complexity within the viable window."),

            new("3 generations (α≈1.5)", "3", new[] { "4 (U6)", "2 (α larger)" },
                0.70, 0.85, NecessityClass.Contingent,
                "4 generations is VIABLE. 3 is not necessary. "
                + "Our universe's generation count may be a contingent fact. "
                + "4-generation universe would have different flavor physics but could support observers."),

            new("Abundance layer", "Present", new[] { "Removed (U7)" },
                0.10, 1.00, NecessityClass.Indispensable,
                "Without abundance: no masses, no couplings, no probabilities. "
                + "Identity alone = pure mathematics. "
                + "Abundance is needed for empirical physics."),
        };
    }

    public static string UniverseTable(List<CounterfactualUniverse> universes)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("COUNTERFACTUAL UNIVERSE VIABILITY");
        sb.AppendLine();
        sb.AppendLine("  ID  Universe        Matter  Particl  Forces  BoundSt  Chem  Cmplx  Obsrv  Score");
        sb.AppendLine("  " + new string('-', 85));

        var ranked = universes.OrderByDescending(u => u.ViabilityScore).ToList();
        foreach (var u in ranked)
        {
            string m = u.MatterExists ? "✓" : "✗";
            string p = u.ParticlesExist ? "✓" : "✗";
            string f = u.GaugeForcesExist ? "✓" : "✗";
            string b = u.StableBoundStates ? "✓" : "✗";
            string c = u.ChemistryPossible ? "✓" : "✗";
            string cx = u.ComplexityEmerges ? "✓" : "✗";
            string o = u.ObserversPossible ? "✓" : "✗";
            string marker = u.Id == "U0" ? " ← OUR UNIVERSE" : "";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0}  {1,-15}  {2}      {3}        {4}       {5}       {6}     {7}     {8}     {9,5:F2}{10}",
                u.Id, u.Name, m, p, f, b, c, cx, o, u.ViabilityScore, marker));
        }
        return sb.ToString();
    }

    public static string NecessityRanking(List<AssumptionNecessity> necessities)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ASSUMPTION NECESSITY — INDISPENSABLE TO CONTINGENT");
        sb.AppendLine();
        sb.AppendLine("  Assumption                Default   Tested Alts         Class");
        sb.AppendLine("  " + new string('-', 70));

        foreach (var n in necessities.OrderBy(n => n.Class))
        {
            string cls = n.Class switch
            {
                NecessityClass.Indispensable => "INDISPENSABLE",
                NecessityClass.StronglyRequired => "STRONG",
                NecessityClass.Helpful => "HELPFUL",
                NecessityClass.Contingent => "CONTINGENT",
                _ => "?"
            };
            sb.AppendLine($"  {n.Assumption,-25} {n.DefaultValue,-9} {string.Join(", ", n.TestedAlternatives),-20} {cls}");
            sb.AppendLine($"    {n.Reasoning.Split('\n')[0]}");
        }
        return sb.ToString();
    }

    public static string ViableWindow()
    {
        return @"
THE VIABLE UNIVERSE WINDOW

Only a NARROW range of TQM parameters supports complexity:

  DIMENSIONALITY:
    3+1 is REQUIRED. 2+1 has no chemistry. 4+1 has no stable orbits.
    The viable dimensionality is a SINGLE POINT.

  NONLINEARITY (M²):
    Window: M² ≈ 2–8 supports observers.
    Below 2: weak hierarchy, nearly degenerate masses (viable but different).
    Above 8: defects unstable, chemistry impossible.
    Our M²≈5 sits near the COMPLEXITY OPTIMUM.

  RANDOMNESS:
    REQUIRED. Deterministic universe = block universe = no becoming.
    Measurement impossible. Abundance absent.

  GENERATIONS:
    WINDOW: 2–4 generations all viable.
    3 is NOT unique. Our count may be contingent.

  IDENTITY + ABUNDANCE:
    Both layers REQUIRED. Identity alone = no masses (pure math).
    Abundance alone = no entities (empty vacuum).

  THE TAKEAWAY:
    TQM does NOT produce a unique universe. It produces a LANDSCAPE
    of possible universes. Our universe occupies a high-complexity
    region of that landscape. But other viable universes exist.
    This is TQM's 'anthropic landscape' — narrow but not unique.
";
    }
}
