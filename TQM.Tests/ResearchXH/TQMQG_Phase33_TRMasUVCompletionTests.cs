using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 33 — interpret TRM as a UV completion. Tests whether the TRM (ψ) kernel is a pure high-density/UV
/// extension, a separate theory, or a partial extension. Classify: UV COMPLETION / SEPARATE THEORY / PARTIAL EXTENSION.
///
/// Tests: TQMQG330 (weak-field reduction), TQMQG331 (strong-field departure + regular core), TQMQG332 (classification).
/// </summary>
public class TQMQG_Phase33_TRMasUVCompletionTests : ResearchTestBase
{
    public TQMQG_Phase33_TRMasUVCompletionTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG330: weak-field reduction (TRM → TQM in the IR) ──────────────────────────

    [Fact]
    public void TQMQG330_WeakFieldReduction()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG330: does TRM reduce to TQM in the weak-field (low-density) limit?");

        double b = 0.3;
        double corr0 = TRMasUVCompletion.G00Correction(0.0, b);
        bool reducesAtZero = TRMasUVCompletion.ReducesToTqm(corr0);

        double d1 = TRMasUVCompletion.Departure(0.01, b);
        double d2 = TRMasUVCompletion.Departure(0.05, b);
        double d3 = TRMasUVCompletion.Departure(0.10, b);
        bool monotonic = d1 < d2 && d2 < d3;

        sb.AppendLine($"x = 0.00  e^(2ψ) = {corr0:F6}  reduces to TQM exactly: {reducesAtZero}");
        sb.AppendLine($"departure |e^(2ψ)-1|  x=0.01: {d1:F6}  x=0.05: {d2:F6}  x=0.10: {d3:F6}  (monotonic → 0: {monotonic})");

        bool irReduction = TRMasUVCompletion.IrReductionHolds();

        sb.AppendLine();
        sb.AppendLine($"IR reduction holds (correction → 1 as x → 0): {irReduction}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: in the weak-field limit ψ → 0, so e^{2ψ} → 1 exactly and TRM's metric collapses to TQM's");
        sb.AppendLine("conformal metric g = ρ^(2/d)η. TQM is the EXACT IR limit of the TRM extension — not a separate theory.");
        Output.WriteLine(sb.ToString());

        Assert.True(irReduction, "TRM should reduce to TQM in the IR");
        Assert.True(reducesAtZero, "the correction should be exactly 1 at x=0");
        Assert.True(monotonic, "the departure should decrease monotonically toward the weak-field limit");
    }

    // ── TQMQG331: strong-field departure + regular core ───────────────────────────────

    [Fact]
    public void TQMQG331_StrongFieldDepartureAndCore()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG331: strong-field departure grows; the core stays regular");

        double b = 0.3;
        double weak = TRMasUVCompletion.Departure(0.05, b);
        double strong = TRMasUVCompletion.Departure(2.0, b);

        int d = 3;
        double rho0 = TRMasUVCompletion.CoreDensity();
        double vol0 = TRMasUVCompletion.TrmCoreVolumeElement(d);

        sb.AppendLine($"departure |e^(2 psi) - 1|  weak field (x=0.05): {weak:F6}");
        sb.AppendLine($"departure |e^(2 psi) - 1|  strong field (x=2.0): {strong:F6}");
        sb.AppendLine($"core density ρ(0)      = {rho0:F6}  (finite)");
        sb.AppendLine($"TRM core √(−g)(0)      = {vol0:F6}  (= ρ(0), volume-preserving)");

        bool departureGrows = strong > weak;
        bool coreRegular = rho0 == 1.0 && Math.Abs(vol0 - rho0) < 1e-12;

        sb.AppendLine();
        sb.AppendLine($"departure grows with field strength: {departureGrows}");
        sb.AppendLine($"core remains regular (ρ(0)=1 finite, √(−g)=ρ preserved): {coreRegular}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the ψ correction is negligible in the weak field and grows in the strong field (high density),");
        sb.AppendLine("so TRM departs from TQM specifically in the UV. The core stays regular — ψ is volume-preserving and smooth,");
        sb.AppendLine("so it does not introduce a central singularity.");
        Output.WriteLine(sb.ToString());

        Assert.True(departureGrows, "departure should grow with field strength");
        Assert.True(coreRegular, "core should remain regular");
    }

    // ── TQMQG332: classification ──────────────────────────────────────────────────────

    [Fact]
    public void TQMQG332_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG332: UV COMPLETION / SEPARATE THEORY / PARTIAL EXTENSION?");

        bool irReduction = TRMasUVCompletion.IrReductionHolds();
        bool uvConfined = TRMasUVCompletion.TensorDofUvConfined();

        sb.AppendLine($"reduces to TQM in the IR (weak field): {irReduction}");
        sb.AppendLine($"graviton d.o.f. is UV-confined:          {uvConfined}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: PARTIAL EXTENSION.");
        sb.AppendLine();
        sb.AppendLine("  • NOT SEPARATE THEORY: TRM reduces EXACTLY to TQM in the weak-field/IR limit (e^{2ψ} → 1).");
        sb.AppendLine("  • NOT A PURE UV COMPLETION: the ψ extension adds a propagating spin-2 (graviton) degree of freedom that");
        sb.AppendLine("    exists at ALL scales (GWs are observed in the IR), so its new content is not confined to high density.");
        sb.AppendLine("  • PARTIAL EXTENSION: TRM = TQM (IR) + a strong-field/UV correction AND an all-scale tensor sector.");
        sb.AppendLine("    It regularizes nothing that TQM left divergent (TQM's core is already regular), and it changes only the");
        sb.AppendLine("    Einstein sector (QG32). It is the minimal non-conformal extension, not a UV completion of TQM's scalar core.");
        Output.WriteLine(sb.ToString());

        Assert.True(irReduction, "IR reduction should hold");
        Assert.False(uvConfined, "graviton d.o.f. should not be UV-confined");
    }
}
