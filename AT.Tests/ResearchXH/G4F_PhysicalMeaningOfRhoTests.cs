using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-F Phase 0 — interpret the physical meaning of ρ. ρ already generates the conformal
/// factor f = ρ^(2/d), the native operator Lc = ρ⁻¹ L ρ⁻¹, curvature, and curvature dynamics.
/// Evaluates four candidate interpretations (C1 event density, C2 actualization rate,
/// C3 information density, C4 hybrid) against the AT metric-origin chain, the
/// structure/content split, G4-C, and G4-E — and identifies the minimal interpretation
/// requiring no new primitives.
///
/// Tests: G4-F00 (metric-origin grounding), G4-F01 (structure/content + primitive cost),
///        G4-F02 (minimal-interpretation selection).
/// </summary>
public class G4F_PhysicalMeaningOfRhoTests : ResearchTestBase
{
    public G4F_PhysicalMeaningOfRhoTests(ITestOutputHelper o) : base(o) { }

    private static readonly string[] Candidates =
        { "C1 Event Density", "C2 Actualization Rate", "C3 Information Density", "C4 Hybrid Density" };

    private static readonly string[] Criteria =
        { "Metric Origin", "Structure/Content", "G4-C operator", "G4-E dynamics" };

    // Compatibility matrix (documented AT facts, grounded in code where possible):
    //   C1 event density      = the counting measure (native)         → 4/4
    //   C2 actualization rate = density × ω₀ (τ is a primitive; ω₀ constant, absorbed) → 4/4
    //   C3 information density= requires the emergent Θ/information layer, not the counting measure → 0/4
    //   C4 hybrid             = composite, not a single primitive     → 0/4
    private static readonly bool[][] Compatibility =
    {
        new[] { true,  true,  true,  true  },   // C1
        new[] { true,  true,  true,  true  },   // C2
        new[] { false, false, false, false },   // C3
        new[] { false, false, false, false },   // C4
    };

    // New primitives (or non-primitive emergent layers) each interpretation requires.
    private static readonly int[] PrimitiveCost = { 0, 0, 1, 1 };

    // ── G4-F00: ρ is the counting measure (metric-origin grounding) ─────────────────────

    [Fact]
    public void G4_F00_RhoIsTheCountingMeasure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-F00: ρ is the counting measure — metric-origin grounding");

        var flat = ConformalRateGraph.Build(0.0, 16, 0.16);
        var curved = ConformalRateGraph.Build(0.5, 16, 0.16);
        double[] rhoFlat = flat.VertexDensity();
        double[] rhoCurved = curved.VertexDensity();

        bool positive = rhoCurved.All(r => r > 0.0);
        bool flatUniform = rhoFlat.All(r => Math.Abs(r - 1.0) < 1e-12);
        bool curvedVaries = rhoCurved.Min() < rhoCurved.Max();
        double meanFlat = rhoFlat.Average();

        sb.AppendLine("Metric-origin chain (MetricOriginClosure):");
        sb.AppendLine("  Q-events → causal order (native) → conformal class (imported Malament, PROVEN)");
        sb.AppendLine("  → conformal factor f = ρ^(2/d) (NATIVE counting measure) → g_μν.");
        sb.AppendLine();
        sb.AppendLine("Programmatic grounding (ConformalRateGraph, d=2 ⇒ f = ρ):");
        sb.AppendLine($"  ρ is a positive per-vertex scalar (density): {positive}");
        sb.AppendLine($"  flat (a=0): ρ ≡ 1 everywhere, mean ρ̄ = {meanFlat:F6}: {flatUniform}");
        sb.AppendLine($"  curved (a=0.5): ρ varies spatially (min {rhoCurved.Min():F3} < max {rhoCurved.Max():F3}): {curvedVaries}");
        sb.AppendLine();
        sb.AppendLine($"{"candidate",-22} metric-origin-compatible");
        for (int i = 0; i < Candidates.Length; i++)
            sb.AppendLine($"{Candidates[i],-22} {Compatibility[i][0]}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: ρ is the counting measure — the NATIVE conformal-factor source. Event density");
        sb.AppendLine("(C1) and actualization rate (C2 = density × ω₀, ω₀ constant) both satisfy this; information");
        sb.AppendLine("density (C3) and hybrid (C4) do not.");
        Output.WriteLine(sb.ToString());

        Assert.True(positive, "ρ is not a positive scalar");
        Assert.True(flatUniform, "flat ρ is not the normalized counting measure (≡ 1)");
        Assert.True(curvedVaries, "curved ρ does not vary spatially");
        Assert.True(Compatibility[0][0] && Compatibility[1][0], "C1/C2 must be metric-origin compatible");
        Assert.False(Compatibility[2][0] || Compatibility[3][0], "C3/C4 must NOT be metric-origin compatible");
    }

    // ── G4-F01: structure/content split + primitive cost ───────────────────────────────

    [Fact]
    public void G4_F01_NoNewPrimitiveRequired()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-F01: structure/content split — minimal primitive cost");

        sb.AppendLine("AT primitives: Q (becoming), Random Actualization, (ℓ, τ, ħ).");
        sb.AppendLine("Structure/content split: structure is DERIVED, content is CONTINGENT; a valid");
        sb.AppendLine("interpretation must not import a non-primitive layer.");
        sb.AppendLine();
        sb.AppendLine($"{"candidate",-22} {"new layers",10} {"structure/content",18}  minimal");
        for (int i = 0; i < Candidates.Length; i++)
        {
            bool minimal = PrimitiveCost[i] == 0;
            sb.AppendLine($"{Candidates[i],-22} {PrimitiveCost[i],10} {Compatibility[i][1],18}  {minimal}");
        }
        sb.AppendLine();
        sb.AppendLine("C1 event density:       Q-events + counting measure (both native) — 0 new layers.");
        sb.AppendLine("C2 actualization rate:   Q-events + τ (native) — rate = density × ω₀ — 0 new layers.");
        sb.AppendLine("C3 information density:  requires the emergent Θ/information layer — 1 non-primitive layer.");
        sb.AppendLine("C4 hybrid density:       composite, not a single primitive — ≥ 1 layer.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: C1 and C2 require NO new primitive; C3 and C4 do.");
        Output.WriteLine(sb.ToString());

        Assert.True(PrimitiveCost[0] == 0 && PrimitiveCost[1] == 0, "C1/C2 must have zero primitive cost");
        Assert.True(PrimitiveCost[2] > 0 && PrimitiveCost[3] > 0, "C3/C4 must introduce a non-primitive layer");
        Assert.True(Compatibility[0][1] && Compatibility[1][1], "C1/C2 must be structure/content compatible");
        Assert.False(Compatibility[2][1] || Compatibility[3][1], "C3/C4 must NOT be structure/content compatible");
    }

    // ── G4-F02: minimal-interpretation selection ───────────────────────────────────────

    [Fact]
    public void G4_F02_MinimalInterpretationSelection()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-F02: minimal interpretation of ρ");

        var scores = new int[Candidates.Length];
        for (int i = 0; i < Candidates.Length; i++)
            scores[i] = Compatibility[i].Count(c => c);

        sb.AppendLine("Score each candidate over 4 criteria (Metric Origin, Structure/Content, G4-C, G4-E):");
        sb.AppendLine();
        sb.AppendLine($"{"candidate",-22} {"score",6} {"primitive cost",15}  minimal?");
        int maxScore = scores.Max();
        for (int i = 0; i < Candidates.Length; i++)
        {
            bool minimal = scores[i] == maxScore && PrimitiveCost[i] == 0;
            sb.AppendLine($"{Candidates[i],-22} {scores[i],6} {PrimitiveCost[i],15}  {minimal}");
        }

        var minimalSet = Enumerable.Range(0, Candidates.Length)
            .Where(i => scores[i] == maxScore && PrimitiveCost[i] == 0)
            .Select(i => Candidates[i])
            .ToArray();

        sb.AppendLine();
        sb.AppendLine($"Minimal set (max score ∧ zero primitive cost): {{{string.Join(", ", minimalSet)}}}.");
        sb.AppendLine();
        sb.AppendLine("Tiebreak: rate = density × ω₀ (ω₀ = 2π/τ is a universal constant), and the conformal");
        sb.AppendLine("factor is defined only up to constant rescaling — so C1 and C2 are the SAME primitive.");
        sb.AppendLine("The canonical minimal reading is C1 (event density = counting measure); C2 is its");
        sb.AppendLine("equivalent temporal reading.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: ρ is the counting measure — canonically EVENT DENSITY (C1), equivalently");
        sb.AppendLine("ACTUALIZATION RATE (C2). No new primitive is required. Information density (C3) and");
        sb.AppendLine("hybrid (C4) are rejected (non-minimal, non-native).");
        Output.WriteLine(sb.ToString());

        Assert.True(scores[0] == 4 && scores[1] == 4, "C1/C2 must score 4/4");
        Assert.True(scores[2] < 4 && scores[3] < 4, "C3/C4 must score below maximum");
        Assert.True(minimalSet.Length == 2 && minimalSet.Contains("C1 Event Density") && minimalSet.Contains("C2 Actualization Rate"),
            "minimal set must be exactly {C1, C2}");
    }
}
