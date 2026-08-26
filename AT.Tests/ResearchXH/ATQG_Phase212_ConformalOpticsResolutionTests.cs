using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 212 — Conformal Optics Resolution. Resolve the conformal-optics frontier: is conformal
/// no-lensing physical, an artifact, or a restricted sector? No new primitives, deterministic.
/// </summary>
public class ATQG_Phase212_ConformalOpticsResolutionTests : ResearchTestBase
{
    public ATQG_Phase212_ConformalOpticsResolutionTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2120_PsiZeroSectorNoLensing()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2120: the ψ = 0 sector — PPN γ = −1, all lensing observables vanish");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The conformally-flat metric g = ρ^(2/d)η (ψ = 0) has PPN γ = −1 (QG26).");
        sb.AppendLine("  - Every lensing observable and the Shapiro delay are ∝ (1+γ)/2.");
        sb.AppendLine();

        double g = ConformalOpticsResolution.GammaPsiZero();
        double factor = ConformalOpticsResolution.LensingFactor(g);
        double defl = ConformalOpticsResolution.Deflection(g);
        double shapiro = ConformalOpticsResolution.Shapiro(g);

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  PPN γ(ψ=0) = {g:F1}");
        sb.AppendLine($"  Lensing factor (1+γ)/2 = {factor:F2}");
        sb.AppendLine($"  Deflection = {defl:F1}  (zero)");
        sb.AppendLine($"  Shapiro delay = {shapiro:F1}  (zero)");
        sb.AppendLine($"  Redshift survives: z = {ConformalOpticsResolution.Redshift(3, 1.1, 1.0):F4}  (g_00 governs)");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - In the ψ = 0 sector all lensing observables vanish exactly (γ = −1).");
        sb.AppendLine("  - Only the gravitational redshift survives — governed by g_00 alone.");

        Output.WriteLine(sb.ToString());

        Assert.True(ConformalOpticsResolution.PsiZeroHasNoLensing(), "ψ=0 must have no lensing");
        Assert.Equal(-1.0, g, 6);
    }

    [Fact]
    public void ATQG2121_PsiNonZeroRestoresLensing()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2121: the ψ ≠ 0 sector — PPN γ = +1, full GR optics restored");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The ψ tensor sector (QG44 Fierz-Pauli) has the linearized-GR limit: PPN γ = +1.");
        sb.AppendLine();

        double g = ConformalOpticsResolution.GammaPsiNonZero();
        double factor = ConformalOpticsResolution.LensingFactor(g);
        double defl = ConformalOpticsResolution.Deflection(g);
        double shapiro = ConformalOpticsResolution.Shapiro(g);

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  PPN γ(ψ≠0) = {g:F1}");
        sb.AppendLine($"  Lensing factor (1+γ)/2 = {factor:F2}");
        sb.AppendLine($"  Deflection = {defl:F1}  (full GR strength)");
        sb.AppendLine($"  Shapiro delay = {shapiro:F1}  (full GR strength)");
        sb.AppendLine($"  Frame dragging restored? {ConformalOpticsResolution.PsiRestoresFrameDragging()}");
        sb.AppendLine($"  Shapiro follows γ? {ConformalOpticsResolution.ShapiroFollowsGamma()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - In the ψ ≠ 0 sector lensing, Shapiro delay, and frame dragging are all restored");
        sb.AppendLine("    at full GR strength (γ = +1).");

        Output.WriteLine(sb.ToString());

        Assert.True(ConformalOpticsResolution.PsiNonZeroRestoresLensing(), "ψ≠0 must restore lensing");
        Assert.True(ConformalOpticsResolution.ShapiroFollowsGamma(), "the Shapiro delay must follow γ");
        Assert.True(ConformalOpticsResolution.PsiRestoresFrameDragging(), "ψ must restore frame dragging");
    }

    [Fact]
    public void ATQG2122_ClassificationOpticsResolved()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2122: classification — OPTICS RESOLVED (restricted sector)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - QG207: the conformal ansatz is the ψ = 0 isotropic member; ψ completes the class.");
        sb.AppendLine();

        int score = ConformalOpticsResolution.OriginScore();
        string classification = ConformalOpticsResolution.Classify();
        bool restricted = ConformalOpticsResolution.ConformalIsRestrictedSector();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Origin score (max 4) = {score}");
        sb.AppendLine($"    +1 ψ=0 no lensing ({ConformalOpticsResolution.PsiZeroHasNoLensing()})");
        sb.AppendLine($"    +1 ψ≠0 restores lensing ({ConformalOpticsResolution.PsiNonZeroRestoresLensing()})");
        sb.AppendLine($"    +1 Shapiro follows γ ({ConformalOpticsResolution.ShapiroFollowsGamma()})");
        sb.AppendLine($"    +1 conformal = restricted sector ({restricted})");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Conformal no-lensing is a RESTRICTED SECTOR: the ψ=0 isotropic slice (γ=−1),");
        sb.AppendLine("    real within that slice but not the physical vacuum.");
        sb.AppendLine("  - The physical sector is ψ≠0 (tensor completion): full GR lensing, Shapiro, frame");
        sb.AppendLine("    dragging (γ=+1). Closes C1 (lensing present vs absent) and C5 (no-lensing");
        sb.AppendLine("    fundamental vs artifact).");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("OPTICS RESOLVED", classification);
        Assert.Equal(4, score);
        Assert.True(restricted, "the conformal ansatz must be the restricted ψ=0 member");
    }
}
