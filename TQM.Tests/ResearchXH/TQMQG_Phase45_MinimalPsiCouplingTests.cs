using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 45 — minimal coupling of ψ. Determines the weakest coupling between ψ and the scalar backbone
/// required to recover GW polarization. Classify: INDEPENDENT / WEAKLY COUPLED / STRONGLY COUPLED.
///
/// Tests: TQMQG450 (zero coupling for polarization), TQMQG451 (polarization vs sourcing), TQMQG452 (classification).
/// </summary>
public class TQMQG_Phase45_MinimalPsiCouplingTests : ResearchTestBase
{
    public TQMQG_Phase45_MinimalPsiCouplingTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG450: zero coupling for the polarization ─────────────────────────────────

    [Fact]
    public void TQMQG450_ZeroCouplingForPolarization()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG450: the GW polarization needs zero coupling to the scalar backbone");

        double required = MinimalPsiCoupling.PolarizationCouplingRequired();

        sb.AppendLine($"coupling strength required for GW POLARIZATION: {required}");
        foreach (var c in MinimalPsiCoupling.Couplings)
        {
            bool needed = MinimalPsiCoupling.RequiredForPolarization(c);
            sb.AppendLine($"{c,-20} required for polarization: {needed}");
        }

        bool zeroCoupling = required == 0.0;

        sb.AppendLine();
        sb.AppendLine($"polarization needs zero coupling: {zeroCoupling}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the two helicities (h_+, h_×) are intrinsic to the FREE massless spin-2 field — no coupling");
        sb.AppendLine("to ρ, the deficit, saturation, or Q-event density is needed to recover the polarization structure.");
        Output.WriteLine(sb.ToString());

        Assert.True(zeroCoupling, "polarization should require zero coupling");
        foreach (var c in MinimalPsiCoupling.Couplings)
            Assert.False(MinimalPsiCoupling.RequiredForPolarization(c), $"coupling {c} should not be required");
    }

    // ── TQMQG451: polarization vs sourcing ────────────────────────────────────────────

    [Fact]
    public void TQMQG451_PolarizationVsSourcing()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG451: polarization is intrinsic; sourcing needs a weak coupling");

        bool weakSourcing = MinimalPsiCoupling.WeakCouplingForSourcing();
        double kappa = MinimalPsiCoupling.GravitationalCouplingWeakness();

        sb.AppendLine($"a weak coupling (κ = 8πG) is needed to SOURCE GWs: {weakSourcing}");
        sb.AppendLine($"gravitational coupling κ = {kappa:F6} (in natural units)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the POLARIZATION (2 helicities) requires no coupling — it is intrinsic. The AMPLITUDE of a");
        sb.AppendLine("gravitational wave (h ~ κ·source) requires coupling ψ to the matter deficit, and that coupling is the");
        sb.AppendLine("WEAK gravitational constant. Polarization: independent; sourcing: weakly coupled.");
        Output.WriteLine(sb.ToString());

        Assert.True(weakSourcing, "sourcing should need a weak coupling");
        Assert.True(kappa > 0.0, "the gravitational coupling should be nonzero");
    }

    // ── TQMQG452: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG452_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG452: INDEPENDENT / WEAKLY COUPLED / STRONGLY COUPLED?");

        sb.AppendLine($"CLASSIFICATION: {MinimalPsiCoupling.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • INDEPENDENT (for the polarization observable): the 2-helicity structure of a massless spin-2 field is");
        sb.AppendLine("    intrinsic — recovering GW polarization requires ZERO coupling to the scalar backbone.");
        sb.AppendLine("  • WEAKLY COUPLED (only when sourced): to give ψ a nonzero amplitude from matter (h ~ κ·source), it must");
        sb.AppendLine("    couple to the deficit with the weak gravitational constant κ = 8πG.");
        sb.AppendLine("  • NOT STRONGLY COUPLED: nothing requires a large coupling; the observed GWs are linear/weak-field.");
        sb.AppendLine();
        sb.AppendLine("So the minimal ψ coupling is ZERO for polarization and WEAK (8πG) for sourcing — ψ is the most decoupled");
        sb.AppendLine("possible new primitive: it rides free, and touches the scalar sector only through the weak source coupling.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("INDEPENDENT", MinimalPsiCoupling.Classify());
        Assert.Equal(0.0, MinimalPsiCoupling.PolarizationCouplingRequired());
        Assert.True(MinimalPsiCoupling.WeakCouplingForSourcing());
    }
}
