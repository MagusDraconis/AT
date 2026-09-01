using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 216 — Quantum Amplitude Origin. Derive |ψ|² from Q-events only (no new primitives,
/// deterministic) via actualization frequency, path multiplicity, counting measure, and network branching.
/// </summary>
public class ATQG_Phase216_QuantumAmplitudeOriginTests : ResearchTestBase
{
    public ATQG_Phase216_QuantumAmplitudeOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2160_AmplitudeMagnitudeIsCountingMeasureShare()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2160: |ψ|² = ρ = the normalized actualization share");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Q-event actualization is a Galton-Watson branching process (QG1).");
        sb.AppendLine("  - The path multiplicity to generation k is μ^k; the total is S = Σ μ^j.");
        sb.AppendLine("  - QG73: ρ = counting measure = |amplitude|².");
        sb.AppendLine();

        double mu = 2.0;
        int K = 8;
        sb.AppendLine("INTERMEDIATE CALCULATIONS (μ=2, K=8, S=255):");
        for (int k = 0; k < K; k++)
        {
            double share = QuantumAmplitudeOrigin.AmplitudeMagnitudeSquared(mu, k, K);
            sb.AppendLine($"  k={k}: path mult = {QuantumAmplitudeOrigin.PathMultiplicity(mu, k):F0}, |ψ|² = {share:F4}, |ψ| = {QuantumAmplitudeOrigin.AmplitudeMagnitude(mu, k, K):F4}");
        }
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - |ψ_k|² = μ^k/S = ρ_k: the amplitude magnitude squared IS the counting measure share.");
        sb.AppendLine("  - The Born rule Σ|ψ|² = 1 holds by construction.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(8.0, QuantumAmplitudeOrigin.PathMultiplicity(2.0, 3), 6);
        Assert.Equal(8.0 / 255.0, QuantumAmplitudeOrigin.AmplitudeMagnitudeSquared(2.0, 3, 8), 9);
    }

    [Fact]
    public void ATQG2161_BornRuleHoldsExactly()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2161: the Born rule Σ|ψ|² = 1 is exact for any branching ratio");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The shares μ^k/S sum to 1 by construction (S is the normalizer).");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        foreach (double mu in new[] { 0.5, 1.0, 2.0, 3.0 })
        {
            double sum = 0;
            for (int k = 0; k < 8; k++) sum += QuantumAmplitudeOrigin.AmplitudeMagnitudeSquared(mu, k, 8);
            sb.AppendLine($"  μ={mu:F1}: Σ|ψ|² = {sum:F6}  (normalized)");
        }
        sb.AppendLine($"  Born rule holds for any μ? {QuantumAmplitudeOrigin.BornRuleHoldsForAnyMu()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The Born rule is exact for every branching ratio — it is a normalization identity.");
        sb.AppendLine("  - The amplitude magnitude follows the branching hierarchy (μ≠1) or is uniform (μ=1).");

        Output.WriteLine(sb.ToString());

        Assert.True(QuantumAmplitudeOrigin.BornRuleHoldsForAnyMu(), "the Born rule must hold for any μ");
        Assert.True(QuantumAmplitudeOrigin.BornRuleNormalized(2.0, 8), "Σ|ψ|² = 1 for μ=2");
    }

    [Fact]
    public void ATQG2162_ClassificationAmplitudeOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2162: classification — AMPLITUDE ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The magnitude |ψ|² is derived from Q-events; the phase remains a separate U(1) (QG62).");
        sb.AppendLine();

        int score = QuantumAmplitudeOrigin.OriginScore();
        string classification = QuantumAmplitudeOrigin.Classify();
        bool criticalUniform = QuantumAmplitudeOrigin.CriticalUniform(8);
        bool criticalAlpha = QuantumAmplitudeOrigin.CriticalityEqualsAlphaZero();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Origin score (max 4) = {score}");
        sb.AppendLine($"    +1 path multiplicity μ^k ({QuantumAmplitudeOrigin.PathMultiplicity(2.0, 3)} = 8)");
        sb.AppendLine($"    +1 |ψ|² = μ^k/S = ρ ({QuantumAmplitudeOrigin.AmplitudeMagnitudeSquared(2.0, 3, 8):F6})");
        sb.AppendLine($"    +1 Born rule exact ({QuantumAmplitudeOrigin.BornRuleHoldsForAnyMu()})");
        sb.AppendLine($"    +1 critical uniform + α=0 ({criticalUniform} && {criticalAlpha})");
        sb.AppendLine($"  Magnitude derived, phase open? {QuantumAmplitudeOrigin.MagnitudeDerivedPhaseOpen()}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - |ψ|² = ρ is derived from Q-events via the branching path multiplicity.");
        sb.AppendLine("  - The Born rule is exact by construction (normalization of the actualization share).");
        sb.AppendLine("  - The phase remains a separate U(1) (QG62) — the magnitude is derived, the argument open.");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("AMPLITUDE ORIGIN", classification);
        Assert.Equal(4, score);
        Assert.True(criticalUniform && criticalAlpha, "criticality must give uniform shares and α=0");
    }
}
