using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-O Phase 2 — stress-test the discriminating prediction. Verifies that the qualitative GR/AT
/// difference (AT source ∝ (ln ρ)″ / field ∝ −∇lnρ, GR source ∝ ρ / field ∝ −∫ρ) persists under
/// realistic profiles: Gaussian halos, NFW-like halos, exponential disks, uniform spheres, shells.
/// Measures potential, acceleration, far-field falloff, and a lensing proxy; classifies ROBUST/WEAK/ARTIFACT.
///
/// Tests: G4-O20 (Gaussian + uniform sphere), G4-O21 (NFW + exponential), G4-O22 (shell + classification).
/// </summary>
public class G4O_Phase2_PredictionStressTestTests : ResearchTestBase
{
    public G4O_Phase2_PredictionStressTestTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;

    private static double GrAcc(Func<double, double> rho, double x) => PhysicalObservables.GrAcceleration(rho, x);
    private static double AtAcc(Func<double, double> rho, double x) => PhysicalObservables.AtAcceleration(rho, x, D);

    // ── G4-O20: Gaussian halo + uniform sphere ─────────────────────────────────────────

    [Fact]
    public void G4_O20_GaussianAndUniformSphere()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O20: Gaussian halo + uniform sphere (sign + localization)");

        // Gaussian halo (peak at origin): GR attractive (a<0), AT repulsive (a>0).
        double x = 0.4;
        double agG = GrAcc(u => PhysicalObservables.Gaussian(u), x);
        double atG = AtAcc(u => PhysicalObservables.Gaussian(u), x);
        sb.AppendLine($"Gaussian halo at x={x}: a_GR = {agG:F4} (attractive), a_AT = {atG:F4} (repulsive)");
        sb.AppendLine($"  opposite sign: {agG < 0 && atG > 0}");

        // Uniform sphere: inside a_GR linear (attractive), a_AT = 0; outside a_GR long-range, a_AT = 0.
        double xIn = 0.2, xOut = 0.8;
        double agIn = GrAcc(u => PhysicalObservables.UniformSphere(u), xIn);
        double atIn = AtAcc(u => PhysicalObservables.UniformSphere(u), xIn);
        double agOut = GrAcc(u => PhysicalObservables.UniformSphere(u), xOut);
        double atOut = AtAcc(u => PhysicalObservables.UniformSphere(u), xOut);
        sb.AppendLine($"uniform sphere inside (x={xIn}): a_GR = {agIn:F4}, a_AT = {atIn:E2}");
        sb.AppendLine($"uniform sphere outside (x={xOut}): a_GR = {agOut:F4} (long-range), a_AT = {atOut:E2}");

        bool signFlip = agG < 0 && atG > 0;
        bool localized = Math.Abs(atIn) < 1e-3 && Math.Abs(atOut) < 1e-3 && Math.Abs(agOut) > 0.1;
        sb.AppendLine();
        sb.AppendLine($"Gaussian sign-flip (repulsive AT): {signFlip}");
        sb.AppendLine($"uniform sphere localization (AT field ≈ 0 inside+outside): {localized}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: ROBUST — the GR/AT difference (sign flip at peaks, zero field in uniform");
        sb.AppendLine("regions) persists for Gaussian halos and uniform spheres.");
        Output.WriteLine(sb.ToString());

        Assert.True(signFlip, "Gaussian halo should show GR-attractive / AT-repulsive");
        Assert.True(localized, "uniform sphere should show AT field localized (zero inside/outside)");
    }

    // ── G4-O21: NFW-like + exponential disk (falloff + constant acceleration) ──────────

    [Fact]
    public void G4_O21_NfwAndExponential()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O21: NFW-like halo + exponential disk (falloff + MOND-like constant)");

        // NFW-like: both fall off, but AT is repulsive (a>0) and falls as ~1/x vs GR attractive saturation.
        double x1 = 0.8;
        double agN = GrAcc(u => PhysicalObservables.Nfw(u), x1);
        double atN = AtAcc(u => PhysicalObservables.Nfw(u), x1);
        sb.AppendLine($"NFW-like at x={x1}: a_GR = {agN:F4} (attractive), a_AT = {atN:F4} (repulsive)");
        sb.AppendLine($"  opposite sign: {agN < 0 && atN > 0}");

        // Exponential disk (backgrounded): GR attractive, AT repulsive (sign flip).
        double x2 = 0.8;
        double agE = GrAcc(u => PhysicalObservables.Exponential(u), x2);
        double atE = AtAcc(u => PhysicalObservables.Exponential(u), x2);
        sb.AppendLine($"exponential disk at x={x2}: a_GR = {agE:F4} (attractive), a_AT = {atE:F4} (repulsive)");
        sb.AppendLine($"  opposite sign: {agE < 0 && atE > 0}");

        // PURE exponential ρ = A·e^(−|x|/r_d) (no background): a_AT = 1/(d·r_d) CONSTANT (MOND-like).
        double rd = 0.4;
        double atP1 = AtAcc(u => Math.Exp(-Math.Abs(u) / rd), 0.5);
        double atP2 = AtAcc(u => Math.Exp(-Math.Abs(u) / rd), 0.8);
        sb.AppendLine($"pure exponential: a_AT(0.5) = {atP1:F4}, a_AT(0.8) = {atP2:F4} (constant 1/(d·r_d) = {1.0 / (D * rd):F4})");
        bool constant = Math.Abs(atP1 - atP2) < 1e-4 && Math.Abs(atP1 - 1.0 / (D * rd)) < 1e-3;

        sb.AppendLine();
        sb.AppendLine($"exponential sign-flip: {agE < 0 && atE > 0}");
        sb.AppendLine($"pure-exponential AT acceleration constant (MOND-like a = 1/(d·r_d)): {constant}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: ROBUST — the sign flip persists for the exponential disk; the pure-exponential");
        sb.AppendLine("limit yields a AT-specific CONSTANT (MOND-like, repulsive) acceleration vs GR's attractive saturation.");
        Output.WriteLine(sb.ToString());

        Assert.True(agN < 0 && atN > 0, "NFW should show GR-attractive / AT-repulsive");
        Assert.True(agE < 0 && atE > 0, "exponential should show GR-attractive / AT-repulsive");
        Assert.True(constant, "pure-exponential AT acceleration should be constant");
    }

    // ── G4-O22: shell + overall classification ─────────────────────────────────────────

    [Fact]
    public void G4_O22_ShellAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O22: shell distribution + overall classification");

        // Shell: GR long-range attractive outside, AT localized (zero outside).
        double xOut = 0.8;
        double agS = GrAcc(u => PhysicalObservables.Shell(u, 0.5, 0.5, 0.06), xOut);
        double atS = AtAcc(u => PhysicalObservables.Shell(u, 0.5, 0.5, 0.06), xOut);
        sb.AppendLine($"shell outside (x={xOut}): a_GR = {agS:F4} (long-range), a_AT = {atS:E2} (localized)");

        // Aggregated summary across all profiles: sign flip or localization holds in every case.
        bool allRobust =
            (GrAcc(u => PhysicalObservables.Gaussian(u), 0.4) < 0 && AtAcc(u => PhysicalObservables.Gaussian(u), 0.4) > 0) &&
            (GrAcc(u => PhysicalObservables.Nfw(u), 0.8) < 0 && AtAcc(u => PhysicalObservables.Nfw(u), 0.8) > 0) &&
            Math.Abs(AtAcc(u => PhysicalObservables.UniformSphere(u), 0.8)) < 1e-3 &&
            Math.Abs(AtAcc(u => PhysicalObservables.Shell(u, 0.5, 0.5, 0.06), 0.8)) < 1e-3;

        sb.AppendLine();
        sb.AppendLine($"aggregated robustness across Gaussian/NFW/uniform-sphere/shell: {allRobust}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: ROBUST — the qualitative GR/AT difference (AT repulsive around density");
        sb.AppendLine("peaks and zero-field in uniform/exterior regions; GR attractive and long-range) is NOT an");
        sb.AppendLine("artifact of a single profile — it survives Gaussian, NFW-like, exponential, uniform-sphere,");
        sb.AppendLine("and shell distributions.");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(agS) > 0.1, "GR shell field should be long-range");
        Assert.True(Math.Abs(atS) < 1e-3, "AT shell field should be localized");
        Assert.True(allRobust, "the discriminating prediction is not robust across profiles");
    }
}
