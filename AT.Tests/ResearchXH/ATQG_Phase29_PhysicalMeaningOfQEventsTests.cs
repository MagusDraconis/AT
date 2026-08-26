using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 29 — physical interpretation of Q-events. Determines the minimal physical meaning of a Q-event:
/// primitive point vs state transition in a temporal network. Classify: REAL-UNDERIVED / EMERGENT / NETWORK
/// TRANSITION.
///
/// Tests: ATQG290 (criteria scoring), ATQG291 (determination), ATQG292 (minimal meaning synthesis).
/// </summary>
public class ATQG_Phase29_PhysicalMeaningOfQEventsTests : ResearchTestBase
{
    public ATQG_Phase29_PhysicalMeaningOfQEventsTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG290: criteria scoring ────────────────────────────────────────────────────

    [Fact]
    public void ATQG290_CriteriaScoring()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG290: which pictures satisfy the four criteria?");

        string[] pictures = { "temporal-lattice", "clock-network", "time-state-change", "network-update", "primitive-point" };
        int full = 0, partial = 0;
        foreach (var p in pictures)
        {
            int score = PhysicalMeaningOfQEvents.CriteriaSatisfied(p);
            bool transition = PhysicalMeaningOfQEvents.IsTransitionPicture(p);
            string kind = transition ? "NETWORK TRANSITION" : "BARE POINT";
            sb.AppendLine($"{p,-18} -> {score}/4 criteria  ({kind})");
            if (score == 4) full++; else partial++;
        }

        sb.AppendLine();
        sb.AppendLine($"full (4/4) network-transition pictures: {full}");
        sb.AppendLine($"insufficient bare-point picture:        {partial} (only primitive-status, no actualization)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: all four transition pictures satisfy actualization-content, counting-compatibility,");
        sb.AppendLine("causal-order-compatibility, and primitive-status. The bare 'primitive point' fails actualization —");
        sb.AppendLine("a static point cannot 'happen', so it cannot generate ρ or support the generation relation.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, full);
        Assert.Equal(1, partial);
        Assert.False(PhysicalMeaningOfQEvents.PrimitivePointSufficient());
    }

    // ── ATQG291: determination ────────────────────────────────────────────────────────

    [Fact]
    public void ATQG291_Determination()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG291: primitive point vs network transition vs emergent");

        bool emergent = PhysicalMeaningOfQEvents.Emergent();
        bool realUnderived = PhysicalMeaningOfQEvents.RealUnderived();
        bool rhoCounts = PhysicalMeaningOfQEvents.RhoCountsQEvents();

        sb.AppendLine($"EMERGENT (from a deeper substrate): {emergent}");
        sb.AppendLine($"REAL-UNDERIVED (a primitive):       {realUnderived}");
        sb.AppendLine($"ρ counts Q-events (density):        {rhoCounts}");

        bool transitionIsMinimal = !PhysicalMeaningOfQEvents.PrimitivePointSufficient()
                                   && realUnderived && !emergent;

        sb.AppendLine();
        sb.AppendLine($"Q-event is a NETWORK TRANSITION (not a bare point, not emergent): {transitionIsMinimal}");
        sb.AppendLine();
        sb.AppendLine("DETERMINATION: a Q-event is REAL-UNDERIVED (a primitive) — it is not derived from anything deeper. Its");
        sb.AppendLine("minimal physical content is a NETWORK TRANSITION: a local time-state change / clock tick that the");
        sb.AppendLine("counting measure ρ counts (one Q-event = one counted unit of ρ). The generation relation is the update rule.");
        Output.WriteLine(sb.ToString());

        Assert.False(emergent);
        Assert.True(realUnderived);
        Assert.True(transitionIsMinimal);
    }

    // ── ATQG292: minimal meaning synthesis ────────────────────────────────────────────

    [Fact]
    public void ATQG292_MinimalMeaning()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG292: the minimal physical meaning of a Q-event");

        sb.AppendLine("MINIMAL MEANING: REAL-UNDERIVED NETWORK TRANSITION.");
        sb.AppendLine();
        sb.AppendLine("  • NOT EMERGENT: Q-events are a primitive — the actualization substrate, not a product of one.");
        sb.AppendLine("  • NOT A BARE POINT: a static point cannot 'happen'; it has no actualization content and cannot");
        sb.AppendLine("    generate ρ or the generation relation.");
        sb.AppendLine("  • A NETWORK TRANSITION: the Q-event is a local time-state change (clock tick) in a temporal network.");
        sb.AppendLine("      - the generation relation = the network's update rule (→ causal order, DERIVED per QG11);");
        sb.AppendLine("      - the counting measure ρ = the density of these updates (Q-event = one counted unit);");
        sb.AppendLine("      - actualization = the network updating (local time advancing by one tick).");
        sb.AppendLine();
        sb.AppendLine("So the minimal physical content of a Q-event is: ONE LOCAL TIME-STATE CHANGE (a tick of actualization).");
        Output.WriteLine(sb.ToString());

        Assert.True(PhysicalMeaningOfQEvents.RealUnderived());
        Assert.True(PhysicalMeaningOfQEvents.RhoCountsQEvents());
        Assert.False(PhysicalMeaningOfQEvents.Emergent());
        Assert.True(PhysicalMeaningOfQEvents.IsTransitionPicture("clock-network"));
    }
}
