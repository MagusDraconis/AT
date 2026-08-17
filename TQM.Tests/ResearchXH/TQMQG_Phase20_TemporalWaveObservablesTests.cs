using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 20 — temporal-wave interpretation of GW observations. Tests whether propagating time-rate
/// oscillations can generate the same detector observables as GR tensor waves. Classify: MATCH / PARTIAL MATCH /
/// NO MATCH.
///
/// Tests: TQMQG200 (round-trip time conformal invariance), TQMQG201 (differential strain: breathing vs tensor),
///        TQMQG202 (classification).
/// </summary>
public class TQMQG_Phase20_TemporalWaveObservablesTests : ResearchTestBase
{
    public TQMQG_Phase20_TemporalWaveObservablesTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG200: round-trip light travel time is conformally invariant ──────────────

    [Fact]
    public void TQMQG200_RoundTripConformalInvariance()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG200: a temporal wave (δρ) does NOT change light travel time (conformal invariance)");

        double L = 4.0;   // arm length (km, arbitrary units)
        double tau = TemporalWaveObservables.RoundTripTime(L);
        double dtau = TemporalWaveObservables.RoundTripTimeChange(L);

        sb.AppendLine($"round-trip time τ = 2L = {tau} (independent of ρ)");
        sb.AppendLine($"change from a temporal wave δρ: δτ = {dtau}");
        sb.AppendLine();
        sb.AppendLine("why: in g = ρ^(2/d)η, g_00 = −ρ^(2/d) and g_ii = ρ^(2/d) multiply EQUALLY, so the light-cone");
        sb.AppendLine("condition ds²=0 is conformally invariant — the conformal factor ρ cancels out of null geodesics.");

        bool timeIndependentOfRho = tau == 2.0 * L;
        bool waveGivesZero = dtau == 0.0;

        sb.AppendLine();
        sb.AppendLine($"light travel time independent of ρ: {timeIndependentOfRho}");
        sb.AppendLine($"temporal wave produces zero round-trip change: {waveGivesZero}");
        Output.WriteLine(sb.ToString());

        Assert.True(timeIndependentOfRho, "round-trip time should be 2L (conformally invariant)");
        Assert.True(waveGivesZero, "temporal wave should give zero round-trip change");
    }

    // ── TQMQG201: breathing (common-mode) vs tensor (differential) arm strain ────────

    [Fact]
    public void TQMQG201_DifferentialStrain()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG201: LIGO measures differential strain — blind to breathing, sensitive to tensor");

        double h0 = 1e-21;   // typical GW strain amplitude
        double breathing = TemporalWaveObservables.BreathingDifferentialStrain(h0);
        double tensor = TemporalWaveObservables.TensorDifferentialStrain(h0);

        sb.AppendLine($"breathing (scalar) differential strain = {breathing:E2} (common-mode → zero)");
        sb.AppendLine($"tensor (+) differential strain = {tensor:E2} (one arm stretches, other squeezes)");
        sb.AppendLine();
        sb.AppendLine("LIGO/Virgo measure the DIFFERENCE in arm round-trip times (phase). A breathing mode stretches");
        sb.AppendLine("both arms equally (common-mode) → zero differential signal; a tensor mode → non-zero.");

        bool breathingInvisible = breathing == 0.0;
        bool tensorVisible = tensor == 2.0 * h0;

        sb.AppendLine();
        sb.AppendLine($"breathing mode is INVISIBLE to a Michelson interferometer: {breathingInvisible}");
        sb.AppendLine($"tensor mode is VISIBLE (differential 2h0): {tensorVisible}");
        Output.WriteLine(sb.ToString());

        Assert.True(breathingInvisible, "breathing mode should be invisible (zero differential strain)");
        Assert.True(tensorVisible, "tensor mode should be visible (differential 2h0)");
    }

    // ── TQMQG202: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG202_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG202: can temporal waves generate the LIGO/Virgo observables?");

        sb.AppendLine("CLASSIFICATION: NO MATCH.");
        sb.AppendLine();
        sb.AppendLine("  • A temporal wave is a propagating oscillation of ρ (the actualization/time-rate), i.e. a CONFORMAL");
        sb.AppendLine("    (scalar) disturbance. Null geodesics (light) are conformally invariant, so δρ produces ZERO change");
        sb.AppendLine("    in light round-trip times (TQMQG200) — an interferometer sees nothing.");
        sb.AppendLine("  • Even ignoring that, the breathing (scalar) mode is COMMON-MODE: it stretches both arms equally, so");
        sb.AppendLine("    the differential phase (the LIGO observable) is ZERO (TQMQG201).");
        sb.AppendLine("  • GR tensor waves (+/×) are DIFFERENTIAL (one arm stretches, the other squeezes), producing the");
        sb.AppendLine("    observed signal. Temporal waves cannot reproduce this.");
        sb.AppendLine("  • Therefore temporal waves do NOT generate the LIGO/Virgo observables: NO MATCH. The observed GWs are");
        sb.AppendLine("    tensor (spin-2), consistent with QG18/QG19 (scalar sector fails polarization; new tensor primitive).");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
