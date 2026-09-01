using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 36 — derive the TRM regular-core profile. Tests whether M_eff(r)=M(1−e^(−r³/r_c³)) follows from a
/// ψ-dynamics. Classify: DERIVED / PREFERRED / ANSATZ.
///
/// Tests: ATQG360 (Poisson saturation reproduces the form), ATQG361 (mechanism census), ATQG362 (classification).
/// </summary>
public class ATQG_Phase36_TRMProfileOriginTests : ResearchTestBase
{
    public ATQG_Phase36_TRMProfileOriginTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG360: Poisson saturation reproduces the profile exactly ───────────────────

    [Fact]
    public void ATQG360_PoissonSaturationReproducesProfile()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG360: 1 - e^(-r^3/rc^3) = Poisson saturation in 3D volume");

        double M = 1.0, rc = 1.0;
        double[] rs = { 0.0, 0.5, 1.0, 2.0 };

        bool exact = true;
        foreach (var r in rs)
        {
            double mass = TRMProfileOrigin.RegularMass(r, M, rc);
            double target = M * (1.0 - Math.Exp(-Math.Pow(r, 3) / Math.Pow(rc, 3)));
            bool ok = Math.Abs(mass - target) < 1e-12;
            exact &= ok;
            sb.AppendLine($"r = {r,4:F2}  M_eff = {mass:F6}   target M(1-e^(-r^3/rc^3)) = {target:F6}   match: {ok}");
        }

        int exponent = TRMProfileOrigin.SpatialDimension();

        sb.AppendLine();
        sb.AppendLine($"exact reproduction at all sample points: {exact}");
        sb.AppendLine($"exponent = {exponent} (spatial dimension; volume ∝ r³)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the profile is the Poisson saturation function 1−e^(−N), where N = (r/rc)³ is the expected");
        sb.AppendLine("Q-event count in a 3-ball. The exponent 3 is the spatial dimension, not a free ansatz parameter.");
        Output.WriteLine(sb.ToString());

        Assert.True(exact, "Poisson saturation should reproduce the profile exactly");
        Assert.Equal(3, exponent);
    }

    // ── ATQG361: mechanism census ─────────────────────────────────────────────────────

    [Fact]
    public void ATQG361_MechanismCensus()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG361: which dynamics yields the profile?");

        bool maxEntropy = TRMProfileOrigin.MaxEntropyGivesScale();
        bool diffusion = TRMProfileOrigin.DiffusionGivesProfile();
        bool network = TRMProfileOrigin.NetworkPropagationGivesProfile();
        bool poisson = TRMProfileOrigin.PoissonSaturationGivesProfile();
        bool updateSetsScale = TRMProfileOrigin.QEventUpdateSetsScale();

        sb.AppendLine($"max-entropy (scale-free)   gives a length scale:  {maxEntropy}");
        sb.AppendLine($"diffusion (α=0 attractor)  gives the profile:     {diffusion}");
        sb.AppendLine($"network propagation         gives the profile:     {network}");
        sb.AppendLine($"finite-density saturation   gives the profile:     {poisson}");
        sb.AppendLine($"Q-event update rules set r_c via ρ_c:             {updateSetsScale}");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: entropy maximization and diffusion give SCALE-FREE profiles (no r_c) and cannot reproduce a");
        sb.AppendLine("regular core. Only finite-density saturation — Poisson Q-event counting at critical density ρ_c — yields");
        sb.AppendLine("1−e^(−r³/r_c³), with r_c set by ρ_c.");
        Output.WriteLine(sb.ToString());

        Assert.False(maxEntropy, "max entropy should not give a scale");
        Assert.False(diffusion, "diffusion should not give the profile");
        Assert.False(network, "network propagation should not give the profile");
        Assert.True(poisson, "Poisson saturation should give the profile");
        Assert.True(updateSetsScale, "critical density should set the scale");
    }

    // ── ATQG362: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG362_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG362: DERIVED / PREFERRED / ANSATZ?");

        double rhoC = 0.1;
        double rc = TRMProfileOrigin.CoreScale(rhoC);
        bool scaleFree = TRMProfileOrigin.CoreScaleIsFree();

        sb.AppendLine($"r_c from ρ_c = {rhoC}: r_c = {rc:F6}  (= (3/4πρ_c)^(1/3))");
        sb.AppendLine($"r_c is a free (supplied) parameter: {scaleFree}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {TRMProfileOrigin.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT ANSATZ: the form 1−e^(−r³/r_c³) is the Poisson saturation function, with exponent 3 = spatial");
        sb.AppendLine("    dimension — a genuine consequence of Q-event counting at critical density.");
        sb.AppendLine("  • DERIVED from finite-density saturation: mass M_eff(r) = M·(fraction of Q-events activated within r),");
        sb.AppendLine("    = M(1−e^(−N(r))) with N(r) = (r/r_c)³.");
        sb.AppendLine("  • CAVEAT: r_c is not itself derivable — it is set by the critical density ρ_c, which is supplied (as in");
        sb.AppendLine("    QG14, AT has bounds but no native value for the cutoff). The Poisson-independence assumption is the");
        sb.AppendLine("    max-entropy counting model (AT-F Phase 1). So the profile is DERIVED, with r_c as the one free scale.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("DERIVED", TRMProfileOrigin.Classify());
        Assert.True(scaleFree, "the core scale should be a free parameter");
        Assert.True(rc > 0.0, "the core scale should be positive");
    }
}
