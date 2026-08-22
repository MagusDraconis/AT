using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 208 — Hawking Temperature With Psi. Determine whether the ψ sector changes the Hawking
/// temperature or leaves T ∝ 1/R unchanged. Uses ρ, ψ, the horizon profile, and the mass-radius relation.
/// Surface gravity κ, temperature scaling, ψ corrections. No new primitives, deterministic.
/// </summary>
public class TQMQG_Phase208_HawkingTemperatureWithPsiTests : ResearchTestBase
{
    public TQMQG_Phase208_HawkingTemperatureWithPsiTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2080_SurfaceGravityAndTemperatureScaling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2080: surface gravity and temperature scaling in the ψ sector");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - ψ-completed metric (QG207): g_00 = −ρ^(2/d)e^(2ψ), g_ii = ρ^(2/d)e^(−2ψ/(d−1)).");
        sb.AppendLine("  - Surface gravity κ = (1/2)·√(−g^00 g^11)·|g_00′| at the horizon.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  ψ exponent 1+1/(d−1) at d=3: {HawkingTemperatureWithPsi.PsiExponent(3):F3}");
        sb.AppendLine($"  κ ~ (1/R)·e^(ψ·{HawkingTemperatureWithPsi.PsiExponent(3):F1}) — the density-gradient scale |ρ′|/ρ ~ 1/R_h");
        sb.AppendLine($"  T_0(R) at d=3: T(1)={HawkingTemperatureWithPsi.TemperatureZeroPsi(3, 1):F4}, T(2)={HawkingTemperatureWithPsi.TemperatureZeroPsi(3, 2):F4}, T(4)={HawkingTemperatureWithPsi.TemperatureZeroPsi(3, 4):F4}");
        sb.AppendLine($"  T_0·R constant? {HawkingTemperatureWithPsi.TZeroInverseRadius(3, 2)}");
        sb.AppendLine($"  T ∝ 1/R survives at ψ=0? {HawkingTemperatureWithPsi.HawkingSurvivesWithoutPsi()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The surface gravity in the ψ sector is κ ~ (1/R)·e^(ψ(1+1/(d−1))).");
        sb.AppendLine("  - At ψ = 0 this is exactly QG184: T ∝ 1/R.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(1.5, HawkingTemperatureWithPsi.PsiExponent(3), 6);
        Assert.True(HawkingTemperatureWithPsi.TZeroInverseRadius(3, 2), "T_0·R must be constant (T ∝ 1/R)");
        Assert.True(HawkingTemperatureWithPsi.HawkingSurvivesWithoutPsi(), "T ∝ 1/R must survive without ψ");
    }

    [Fact]
    public void TQMQG2081_PsiCorrectionIsPrefactorial()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2081: the ψ correction is a prefactor — the T ∝ 1/R law is invariant");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - T_ψ = T_0·e^(ψ(1+1/(d−1))) — a multiplicative, radius-independent correction.");
        sb.AppendLine();

        double factor = HawkingTemperatureWithPsi.PsiCorrectionFactor(0.1, 3);
        bool invariant = HawkingTemperatureWithPsi.InverseRadiusLawPsiInvariant();
        bool recovers = HawkingTemperatureWithPsi.PsiZeroRecoversQ184();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  ψ correction factor at ψ=0.1, d=3: e^(1.5·0.1) = {factor:F4}");
        sb.AppendLine($"  T(1)/T(2) with ψ=0.2 = {HawkingTemperatureWithPsi.TemperatureWithPsi(3, 1, 0.2) / HawkingTemperatureWithPsi.TemperatureWithPsi(3, 2, 0.2):F4}");
        sb.AppendLine($"  T(1)/T(2) without ψ      = {HawkingTemperatureWithPsi.TemperatureRatio(3, 1, 2):F4}");
        sb.AppendLine($"  T ∝ 1/R law ψ-invariant? {invariant}");
        sb.AppendLine($"  ψ=0 recovers QG184? {recovers}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The correction factor e^(ψ(1+1/(d−1))) is radius-independent.");
        sb.AppendLine("  - The T(R₁)/T(R₂) ratio is ψ-invariant — the T ∝ 1/R LAW is preserved.");
        sb.AppendLine("  - ψ rescales only the overall prefactor, not the scaling.");

        Output.WriteLine(sb.ToString());

        Assert.True(invariant, "the T ∝ 1/R law must be ψ-invariant");
        Assert.True(recovers, "ψ=0 must recover QG184 exactly");
        Assert.True(Math.Abs(factor - Math.Exp(0.15)) < 1e-9, "the correction factor must be e^(1.5ψ)");
    }

    [Fact]
    public void TQMQG2082_ClassificationHawkingOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2082: classification — HAWKING ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Horizon regularity (asymptotic flatness): ψ(R_h) → 0 removes the correction.");
        sb.AppendLine("  - Contrast: frame dragging (QG186) REQUIRES ψ; Hawking T does not.");
        sb.AppendLine();

        int score = HawkingTemperatureWithPsi.OriginScore();
        string classification = HawkingTemperatureWithPsi.Classify();
        bool regular = HawkingTemperatureWithPsi.RegularHorizonRemovesPsiCorrection();
        bool fddr = HawkingTemperatureWithPsi.FrameDraggingRequiresPsi();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Origin score (max 4) = {score}");
        sb.AppendLine($"    +1 κ ∝ (1/R)·e^(ψ·3/2) ({HawkingTemperatureWithPsi.PsiExponent(3) == 1.5})");
        sb.AppendLine($"    +1 T ∝ 1/R ψ-invariant ({HawkingTemperatureWithPsi.InverseRadiusLawPsiInvariant()})");
        sb.AppendLine($"    +1 ψ=0 ⇒ QG184 ({HawkingTemperatureWithPsi.PsiZeroRecoversQ184()})");
        sb.AppendLine($"    +1 ψ(R_h)→0 ⇒ T_ψ = T_0 ({regular})");
        sb.AppendLine($"  Frame dragging requires ψ? {fddr}   (Hawking does not)");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The ψ sector does NOT change the Hawking temperature law: T ∝ 1/R survives.");
        sb.AppendLine("  - ψ contributes only the prefactor e^(ψ(1+1/(d−1))), removed by horizon regularity.");
        sb.AppendLine("  - Hawking T is a ρ-sector (first-law) observable — unlike frame dragging (ψ-sector).");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("HAWKING ORIGIN", classification);
        Assert.Equal(4, score);
        Assert.True(regular, "horizon regularity must remove the ψ correction");
        Assert.True(fddr, "frame dragging must require ψ (contrast)");
    }
}
