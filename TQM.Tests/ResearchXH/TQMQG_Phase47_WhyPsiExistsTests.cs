using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 47 — why does Primitive 2 (ψ) exist? Determines the principle behind ψ's existence.
/// Classify: FORCED / PREFERRED / CONTINGENT / NEW POSTULATE.
///
/// Tests: TQMQG470 (Q-event-only vs Q-event+ψ), TQMQG471 (no internal forcing), TQMQG472 (classification).
/// </summary>
public class TQMQG_Phase47_WhyPsiExistsTests : ResearchTestBase
{
    public TQMQG_Phase47_WhyPsiExistsTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG470: what becomes impossible without ψ ──────────────────────────────────

    [Fact]
    public void TQMQG470_WhatIsImpossibleWithoutPsi()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG470: Q-event-only vs Q-event + ψ");

        sb.AppendLine("Q-EVENT-ONLY UNIVERSE (scalar conformal, γ=−1) STILL HAS:");
        sb.AppendLine("  • gravitational redshift, matter attraction, flat rotation curves (log-deficit),");
        sb.AppendLine("    regular cores (saturation)");
        sb.AppendLine();
        sb.AppendLine("OBSERVATIONS IMPOSSIBLE WITHOUT ψ:");
        foreach (var ob in WhyPsiExists.ImpossibleWithoutPsi)
            sb.AppendLine($"  • {ob}");

        sb.AppendLine();
        sb.AppendLine($"impossible observations: {WhyPsiExists.ImpossibleWithoutPsi.Length}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the scalar universe is rich but cannot bend light or emit gravitational waves. Exactly one of");
        sb.AppendLine("these (GW polarization) is spin-2 and uniquely requires the tensor ψ (QG43); the others need only a scalar ψ.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, WhyPsiExists.ImpossibleWithoutPsi.Length);
        Assert.Contains("gw-polarization", WhyPsiExists.ImpossibleWithoutPsi);
        Assert.Contains("lensing", WhyPsiExists.ImpossibleWithoutPsi);
    }

    // ── TQMQG471: no internal consistency principle forces ψ ─────────────────────────

    [Fact]
    public void TQMQG471_NoInternalForcing()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG471: is ψ forced by internal consistency?");

        bool selfConsistent = WhyPsiExists.ScalarUniverseSelfConsistent();
        bool forced = WhyPsiExists.ForcedByInternalConsistency();
        bool contingent = WhyPsiExists.ContingentOnObservation();
        bool fullStressEnergy = WhyPsiExists.ScalarRespondsToFullStressEnergy();

        sb.AppendLine($"scalar universe is internally self-consistent: {selfConsistent}");
        sb.AppendLine($"ψ is FORCED by internal consistency:            {forced}");
        sb.AppendLine($"ψ's existence is CONTINGENT on observation:     {contingent}");
        sb.AppendLine($"scalar responds to the full stress-energy:      {fullStressEnergy}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the scalar universe has no internal contradiction — it simply fails to bend light and emit GWs.");
        sb.AppendLine("The 'principle' that motivates ψ is OBSERVATIONAL completeness (the equivalence principle / light bending),");
        sb.AppendLine("not an internal necessity. ψ is added because we observe GWs, not because the theory breaks without it.");
        Output.WriteLine(sb.ToString());

        Assert.True(selfConsistent, "the scalar universe should be self-consistent");
        Assert.False(forced, "psi should not be forced by internal consistency");
        Assert.True(contingent, "psi existence should be contingent on observation");
        Assert.False(fullStressEnergy, "the scalar should not respond to the full stress-energy");
    }

    // ── TQMQG472: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG472_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG472: FORCED / PREFERRED / CONTINGENT / NEW POSTULATE?");

        sb.AppendLine($"CLASSIFICATION: {WhyPsiExists.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT FORCED: the Q-event-only universe is self-consistent; no internal principle demands ψ.");
        sb.AppendLine("  • CONTINGENT: ψ is added because of a specific observation — gravitational-wave polarization (QG43).");
        sb.AppendLine("  • PREFERRED (form only): given that GWs exist, spin-2 is the unique viable spin (QG46).");
        sb.AppendLine("  • NEW POSTULATE: ψ is a primitive axiom — it cannot be derived or emerge from Q-events (QG23/24/37).");
        sb.AppendLine();
        sb.AppendLine("WHY ψ EXISTS: not because TQM forces it, but because the universe demonstrably has spin-2 gravitational waves");
        sb.AppendLine("and light bending, which the derived scalar sector cannot produce. ψ is the minimal new postulate that closes");
        sb.AppendLine("this observational gap — the second and final primitive of the theory.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("NEW POSTULATE", WhyPsiExists.Classify());
        Assert.True(WhyPsiExists.IsNewPostulate());
        Assert.False(WhyPsiExists.ForcedByInternalConsistency());
    }
}
