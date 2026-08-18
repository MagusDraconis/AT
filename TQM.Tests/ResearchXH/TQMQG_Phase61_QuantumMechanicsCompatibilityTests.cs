using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 61 — quantum mechanics compatibility. Audits whether network ticks reproduce quantum features.
/// Classify: MATCH / PARTIAL / UNKNOWN.
///
/// Tests: TQMQG610 (classification), TQMQG611 (classical network), TQMQG612 (conclusion).
/// </summary>
public class TQMQG_Phase61_QuantumMechanicsCompatibilityTests : ResearchTestBase
{
    public TQMQG_Phase61_QuantumMechanicsCompatibilityTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG610: classification ─────────────────────────────────────────────────────

    [Fact]
    public void TQMQG610_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG610: MATCH / PARTIAL / UNKNOWN for quantum features");

        int match = 0, partial = 0, unknown = 0;
        foreach (var f in QuantumMechanicsCompatibility.Features)
        {
            string c = QuantumMechanicsCompatibility.Classify(f);
            sb.AppendLine($"{f,-14} -> {c}");
            switch (c)
            {
                case "MATCH": match++; break;
                case "PARTIAL": partial++; break;
                case "UNKNOWN": unknown++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"MATCH   : {match}");
        sb.AppendLine($"PARTIAL : {partial}");
        sb.AppendLine($"UNKNOWN : {unknown}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(0, match);
        Assert.Equal(1, partial);
        Assert.Equal(3, unknown);
    }

    // ── TQMQG611: the network is classical ───────────────────────────────────────────

    [Fact]
    public void TQMQG611_ClassicalNetwork()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG611: the Q-event network is a classical discrete structure");

        bool classical = QuantumMechanicsCompatibility.NetworkIsClassical();
        bool correlations = QuantumMechanicsCompatibility.HasClassicalCorrelations();
        bool amplitudes = QuantumMechanicsCompatibility.HasComplexAmplitudes();

        sb.AppendLine($"network is classical (ticks + probabilities): {classical}");
        sb.AppendLine($"has classical correlations (QG30):             {correlations}");
        sb.AppendLine($"has native complex amplitudes / superposition: {amplitudes}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: nodes are discrete ticks (tick/no-tick), ρ is a classical probability, and correlations are");
        sb.AppendLine("classical. There is no native superposition or phase structure — the network is classical, not quantum.");
        Output.WriteLine(sb.ToString());

        Assert.True(classical, "the network should be classical");
        Assert.True(correlations, "the network should have classical correlations");
        Assert.False(amplitudes, "the network should not have complex amplitudes");
    }

    // ── TQMQG612: conclusion ─────────────────────────────────────────────────────────

    [Fact]
    public void TQMQG612_Conclusion()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG612: quantum mechanics is not natively hosted");

        sb.AppendLine("WHAT THE NETWORK HAS:");
        sb.AppendLine("  • classical probability (ρ) and classical correlations (QG30) — a shadow of entanglement");
        sb.AppendLine();
        sb.AppendLine("WHAT IS MISSING:");
        sb.AppendLine("  • superposition and interference (no complex amplitudes/phases)");
        sb.AppendLine("  • entanglement (no quantum non-separability)");
        sb.AppendLine("  • measurement/collapse (no native analogue)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: TQM's causal network is a CLASSICAL gravity framework (spin-0 + spin-2). Quantum mechanics —");
        sb.AppendLine("superposition, interference, entanglement, measurement — is not natively reproduced; whether it emerges from");
        sb.AppendLine("actualization is an open (UNKNOWN) question, mirroring the fermion result of QG60.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("UNKNOWN", QuantumMechanicsCompatibility.Classify("superposition"));
        Assert.Equal("PARTIAL", QuantumMechanicsCompatibility.Classify("entanglement"));
        Assert.True(QuantumMechanicsCompatibility.HasClassicalCorrelations());
    }
}
