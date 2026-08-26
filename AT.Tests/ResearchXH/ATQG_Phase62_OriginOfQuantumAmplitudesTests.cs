using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 62 — origin of quantum amplitudes. Determines whether complex amplitudes emerge from the network.
/// Classify: COMPATIBLE / EMERGENT / REQUIRES NEW PRIMITIVE.
///
/// Tests: ATQG620 (no native phase), ATQG621 (no emergence from loops), ATQG622 (classification).
/// </summary>
public class ATQG_Phase62_OriginOfQuantumAmplitudesTests : ResearchTestBase
{
    public ATQG_Phase62_OriginOfQuantumAmplitudesTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG620: the network has no native phase ────────────────────────────────────

    [Fact]
    public void ATQG620_NoNativePhase()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG620: the network has no native U(1) phase, but links can host one");

        bool nativePhase = OriginOfQuantumAmplitudes.NetworkHasNativePhase();
        bool linksHostPhase = OriginOfQuantumAmplitudes.LinksCanHostPhase();

        sb.AppendLine($"network NATIVELY has a phase (U(1)): {nativePhase}");
        sb.AppendLine($"links CAN host a phase (connection):  {linksHostPhase}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the network's native content is scalar (nodes) + rank-2 (links) — no phase. But the links can");
        sb.AppendLine("carry a U(1) connection (lattice gauge theory, QG60), so a phase is COMPATIBLE, not native.");
        Output.WriteLine(sb.ToString());

        Assert.False(nativePhase, "the network should not have a native phase");
        Assert.True(linksHostPhase, "links should be able to host a phase");
    }

    // ── ATQG621: no emergence from closed loops ─────────────────────────────────────

    [Fact]
    public void ATQG621_NoEmergenceFromLoops()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG621: closed loops are trivial without a phase");

        bool holonomy = OriginOfQuantumAmplitudes.HolonomyWithoutPhase();
        bool emergent = OriginOfQuantumAmplitudes.Emergent();

        sb.AppendLine($"closed loop gives a nontrivial holonomy WITHOUT a phase: {holonomy}");
        sb.AppendLine($"complex amplitudes EMERGE from the network natively:   {emergent}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: without a U(1) phase on the links, a closed loop has holonomy 1 (no interference phase). The");
        sb.AppendLine("loop structure alone does not produce amplitudes — a phase must be added, so QM does not emerge natively.");
        Output.WriteLine(sb.ToString());

        Assert.False(holonomy, "closed loops should be trivial without a phase");
        Assert.False(emergent, "amplitudes should not emerge natively");
    }

    // ── ATQG622: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG622_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG622: COMPATIBLE / EMERGENT / REQUIRES NEW PRIMITIVE?");

        bool compatible = OriginOfQuantumAmplitudes.Compatible();
        bool emergent = OriginOfQuantumAmplitudes.Emergent();
        bool requires = OriginOfQuantumAmplitudes.RequiresNewPrimitive();

        sb.AppendLine($"COMPATIBLE (link phases fit):      {compatible}");
        sb.AppendLine($"EMERGENT (native, no new input):   {emergent}");
        sb.AppendLine($"REQUIRES NEW PRIMITIVE (phase):    {requires}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {OriginOfQuantumAmplitudes.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • COMPATIBLE: a U(1) phase fits on the links as a connection (like the gauge fields of QG60).");
        sb.AppendLine("  • NOT EMERGENT: the network's scalar/rank-2 content has no phase; closed loops are trivial without one.");
        sb.AppendLine("  • REQUIRES NEW PRIMITIVE: the complex amplitude (a U(1) phase) is a new degree of freedom — QM needs it.");
        sb.AppendLine();
        sb.AppendLine("So quantum mechanics requires a new phase/amplitude primitive, compatible with the network but not derivable");
        sb.AppendLine("from it — exactly parallel to how ψ required a new spin-2 primitive (QG23).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("REQUIRES NEW PRIMITIVE", OriginOfQuantumAmplitudes.Classify());
        Assert.True(compatible);
        Assert.False(emergent);
        Assert.True(requires);
    }
}
