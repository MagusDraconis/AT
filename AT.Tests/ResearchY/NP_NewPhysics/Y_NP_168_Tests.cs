using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_168 — Yang–Mills Existence and Mass Gap Audit.
///
/// Verdict tested: AT derives a positive, dimensionless kinematic gap for the finite
/// circulant graph C_96(1..6), but it does not currently construct an interacting
/// four-dimensional quantum Yang–Mills theory or establish a positive physical,
/// gauge-invariant Hamiltonian gap in the regulator-removal and infinite-volume limits.
///
/// Deterministic: closed-form circulant eigenvalues, fixed refinements, and a fixed
/// Clay-requirements inventory; no randomness, fitted physical scale, or external dependency.
/// </summary>
public class Y_NP_168_Tests : ResearchTestBase
{
    private const int StepMax = 6;
    private const int D96Size = 96;

    public Y_NP_168_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n)
    {
        double sum = 0.0;
        for (int d = 1; d <= StepMax; d++)
            sum += 1.0 - Math.Cos(2.0 * Math.PI * d * k / n);
        return 2.0 * sum;
    }

    private static double MinimumPositiveLambda(int n)
    {
        double minimum = double.PositiveInfinity;
        for (int k = 1; k < n; k++)
            minimum = Math.Min(minimum, Lambda(k, n));
        return minimum;
    }

    private static double OmegaGap(int n) => Math.Sqrt(MinimumPositiveLambda(n));

    private static int SumOfSquaredSteps()
    {
        int sum = 0;
        for (int d = 1; d <= StepMax; d++) sum += d * d;
        return sum;
    }

    private static ClayRequirement[] ClayRequirements()
    {
        // Executable repository proxy: a completed construction must expose an explicit public
        // AT.Core capability, rather than only prose or a Lagrangian string. Composite marker
        // names deliberately require YangMills scope so unrelated Hilbert/limit code does not count.
        string[] publicSymbols = typeof(LagrangianOrigin).Assembly
            .GetExportedTypes()
            .SelectMany(type => new[] { type.FullName ?? type.Name }
                .Concat(type.GetMembers().Select(member => $"{type.FullName}.{member.Name}")))
            .ToArray();

        bool HasCapability(params string[] markers) => markers.Any(marker =>
            publicSymbols.Any(symbol =>
                symbol.Contains(marker, StringComparison.OrdinalIgnoreCase)));

        return
        [
            new("Quantum Hilbert/measure construction",
                HasCapability("YangMillsHilbert", "YangMillsEuclideanMeasure"),
                "No explicit Yang–Mills Hilbert-space or Euclidean-measure construction API."),
            new("Gauge constraints and gauge-invariant observable sector",
                HasCapability("YangMillsGaussLaw", "YangMillsGaugeInvariantObservable"),
                "No explicit Gauss-law or gauge-invariant-observable construction API."),
            new("Interacting Hamiltonian or transfer matrix",
                HasCapability("YangMillsHamiltonian", "YangMillsTransferMatrix"),
                "QG244 exposes a Lagrangian form, not an interacting quantum-operator API."),
            new("Regulator/refinement limit",
                HasCapability("YangMillsRegulatorLimit", "YangMillsContinuumLimit"),
                "No explicit interacting regulator-removal or continuum-limit construction API."),
            new("Infinite-volume R^4 limit",
                HasCapability("YangMillsInfiniteVolume", "YangMillsR4Limit"),
                "No explicit four-dimensional infinite-volume construction API."),
            new("Uniform positive physical lower bound",
                HasCapability("YangMillsUniformGap", "YangMillsGlueball"),
                "No explicit uniform gauge-invariant/glueball gap theorem API."),
        ];
    }

    private static string ClassifyCurrentEvidence(
        bool finiteGraphGapDerived,
        bool qg244LagrangianStructurePresent,
        ClayRequirement[] requirements)
    {
        if (requirements.All(r => r.SatisfiedByCurrentAtEvidence))
            return "CLAY REQUIREMENTS ESTABLISHED";
        if (finiteGraphGapDerived && qg244LagrangianStructurePresent)
            return "FINITE GAP DERIVED; REGULATOR SCAFFOLD EMERGENT; CLAY CLAIM REFUTED";
        return "NO YANG-MILLS SCAFFOLD";
    }

    // ── [Required] Y_NP_168_D96Gap ─────────────────────────────

    [Fact]
    public void Y_NP_168_D96Gap()
    {
        double lambdaGap = MinimumPositiveLambda(D96Size);
        double omegaGap = Math.Sqrt(lambdaGap);

        Assert.InRange(Math.Abs(lambdaGap - 0.386350893377790), 0.0, 5e-14);
        Assert.InRange(Math.Abs(omegaGap - 0.621571309969974), 0.0, 5e-14);
        Assert.InRange(Math.Abs(lambdaGap - Lambda(1, D96Size)), 0.0, 1e-13);

        int multiplicity = 0;
        for (int k = 1; k < D96Size; k++)
            if (Math.Abs(Lambda(k, D96Size) - lambdaGap) < 1e-12) multiplicity++;
        Assert.Equal(2, multiplicity); // k=1 and k=95
    }

    // ── [Required] Y_NP_168_RefinementScaling ──────────────────

    [Fact]
    public void Y_NP_168_RefinementScaling()
    {
        int[] sizes = [48, 96, 192, 384, 768, 1536];
        int sumD2 = SumOfSquaredSteps();
        double leadingCoefficient = 4.0 * Math.PI * Math.PI * sumD2;
        double previousGap = double.PositiveInfinity;
        double previousScaled = 0.0;

        foreach (int n in sizes)
        {
            double gap = MinimumPositiveLambda(n);
            double scaled = n * (double)n * gap;

            Assert.InRange(Math.Abs(gap - Lambda(1, n)), 0.0, 1e-12);
            Assert.True(gap < previousGap, $"gap must decrease at N={n}");
            Assert.True(scaled > previousScaled, $"N^2 gap must approach its limit at N={n}");
            Assert.True(scaled < leadingCoefficient);

            previousGap = gap;
            previousScaled = scaled;
        }

        Assert.Equal(91, sumD2);
        Assert.InRange(
            Math.Abs(previousScaled / leadingCoefficient - 1.0),
            0.0,
            4e-5);

        for (int i = 1; i < sizes.Length; i++)
        {
            double ratio = MinimumPositiveLambda(sizes[i]) /
                           MinimumPositiveLambda(sizes[i - 1]);
            Assert.InRange(ratio, 0.249, 0.257);
        }
    }

    // ── [Required] Y_NP_168_ScalingConventions ─────────────────

    [Fact]
    public void Y_NP_168_ScalingConventions()
    {
        const double compactCircumference = 1.0; // dimensionless convention, not a physical scale
        const int n = 1536;
        double latticeSpacing = compactCircumference / n;
        double gap = MinimumPositiveLambda(n);
        double compactScaledGap = gap / (latticeSpacing * latticeSpacing);
        double normalizedCompactGap = compactScaledGap / SumOfSquaredSteps();

        // Fixed compact circumference: L/a^2 has a finite kinematic continuum gap.
        Assert.InRange(
            Math.Abs(compactScaledGap / (4.0 * Math.PI * Math.PI * 91.0) - 1.0),
            0.0,
            4e-5);
        Assert.InRange(
            Math.Abs(normalizedCompactGap / (4.0 * Math.PI * Math.PI) - 1.0),
            0.0,
            4e-5);

        // Fixed unit lattice spacing: circumference grows with N and the gap tends to zero.
        Assert.True(gap < MinimumPositiveLambda(D96Size));
        Assert.True(gap < 0.002);

        // The corresponding frequency spacing scales as O(N^-1).
        double scaledOmega = n * OmegaGap(n);
        Assert.InRange(
            Math.Abs(scaledOmega / Math.Sqrt(4.0 * Math.PI * Math.PI * 91.0) - 1.0),
            0.0,
            2e-5);
    }

    // ── [Required] Y_NP_168_QG244Scope ─────────────────────────

    [Fact]
    public void Y_NP_168_QG244Scope()
    {
        Assert.True(LagrangianOrigin.StrongLagrangianDerived());
        Assert.Contains("F^a_μν", LagrangianOrigin.LagrangianDensity());
        Assert.Contains("D_μ", LagrangianOrigin.LagrangianDensity());
        Assert.True(LagrangianOrigin.HiggsYukawaPartial());
    }

    // ── [Required] Y_NP_168_ClayRequirements ───────────────────

    [Fact]
    public void Y_NP_168_ClayRequirements()
    {
        ClayRequirement[] requirements = ClayRequirements();

        Assert.Equal(6, requirements.Length);
        Assert.All(requirements, requirement =>
            Assert.False(requirement.SatisfiedByCurrentAtEvidence, requirement.Name));
        Assert.Contains(requirements, r => r.Name.Contains("R^4", StringComparison.Ordinal));
        Assert.Contains(requirements, r => r.Name.Contains("gauge-invariant", StringComparison.Ordinal));
    }

    // ── [Required] Y_NP_168_GapDistinction ─────────────────────

    [Fact]
    public void Y_NP_168_GapDistinction()
    {
        double finiteGap = MinimumPositiveLambda(D96Size);
        double refinedGap = MinimumPositiveLambda(1536);

        Assert.True(finiteGap > 0.0);
        Assert.True(refinedGap < finiteGap / 250.0);
        Assert.InRange(1536.0 * 1536.0 * refinedGap, 3592.0, 3593.0);
        Assert.DoesNotContain(ClayRequirements(), r => r.SatisfiedByCurrentAtEvidence);
    }

    // ── [Required] Y_NP_168_ExistingNomenclature ───────────────

    [Fact]
    public void Y_NP_168_ExistingNomenclature()
    {
        // Multiple QG phases reuse λ₂ under the sector-independent "mass-gap scale"
        // label. Numerically it is exactly the D96 graph gap; this test prevents that
        // older nomenclature from being mistaken for an independently computed YM spectrum.
        double graphGap = MinimumPositiveLambda(D96Size);
        Assert.InRange(
            Math.Abs(GaugeSectorOrigin.SpectralGap() - graphGap),
            0.0,
            1e-12);
        Assert.InRange(
            Math.Abs(HiggsMassOrigin.SpectralGap() - graphGap),
            0.0,
            1e-12);
        Assert.InRange(
            Math.Abs(HiggsPotentialOrigin.SpectralGap() - graphGap),
            0.0,
            1e-12);
        Assert.InRange(
            Math.Abs(MuonG2Origin.SpectralGap() - graphGap),
            0.0,
            1e-12);
        Assert.InRange(
            Math.Abs(ResonanceLayerAudit.Lambda2() - graphGap),
            0.0,
            1e-12);
        Assert.Contains(OperatorSectorAudit.Observables(),
            observable => observable.Formula.Contains("λ₂", StringComparison.Ordinal));
        Assert.Contains(OperatorUniversalityPrediction.NewObservables(),
            observable => observable.Formula.Contains("λ₂", StringComparison.Ordinal));
    }

    // ── [Required] Y_NP_168_Classification ─────────────────────

    [Fact]
    public void Y_NP_168_Classification()
    {
        bool finiteNetworkGapDerived = MinimumPositiveLambda(D96Size) > 0.0;
        bool qg244LagrangianStructurePresent =
            LagrangianOrigin.StrongLagrangianDerived() &&
            LagrangianOrigin.LagrangianDensity().Contains("F^a_μν", StringComparison.Ordinal);

        string classification = ClassifyCurrentEvidence(
            finiteNetworkGapDerived,
            qg244LagrangianStructurePresent,
            ClayRequirements());

        Assert.Equal(
            "FINITE GAP DERIVED; REGULATOR SCAFFOLD EMERGENT; CLAY CLAIM REFUTED",
            classification);
    }

    // ── [Required] Y_NP_168_Run ────────────────────────────────

    [Fact]
    public void Y_NP_168_Run()
    {
        CultureInfo original = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = original; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_168 — Yang–Mills Existence and Mass Gap Audit");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  1. Clay target = rigorous interacting non-Abelian quantum Yang–Mills on R^4");
        sb.AppendLine("     plus a positive physical gauge-invariant spectral gap above the vacuum.");
        sb.AppendLine("  2. QG244 supplies the classical/minimal Yang–Mills Lagrangian structure from");
        sb.AppendLine("     D96; this is not assumed to be a quantum construction.");
        sb.AppendLine("  3. C_N(1..6) uses λ_k = 2Σ_d[1-cos(2πdk/N)] and ω_k = √λ_k.");
        sb.AppendLine("  4. No dimensional physical scale is imported.");
        sb.AppendLine();

        double lambda96 = MinimumPositiveLambda(D96Size);
        double omega96 = Math.Sqrt(lambda96);
        double coefficient = 4.0 * Math.PI * Math.PI * SumOfSquaredSteps();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Σ_{{d=1..6}} d² = {SumOfSquaredSteps()}");
        sb.AppendLine($"  N=96 λ_gap = {lambda96:F15}");
        sb.AppendLine($"  N=96 ω_gap = √λ_gap = {omega96:F15}");
        sb.AppendLine("  Refinement (fixed step set {1..6}, unscaled graph):");
        sb.AppendLine("      N       λ_gap              ω_gap              N² λ_gap");
        foreach (int n in new[] { 48, 96, 192, 384, 768, 1536 })
        {
            double gap = MinimumPositiveLambda(n);
            sb.AppendLine(
                $"    {n,4}  {gap,18:F15}  {Math.Sqrt(gap),18:F15}  {n * (double)n * gap,14:F9}");
        }
        sb.AppendLine($"  Analytic limit: N²λ_gap → 4π²Σd² = {coefficient:F12}");
        sb.AppendLine("  Therefore λ_gap = O(N^-2) and ω_gap = O(N^-1).");
        sb.AppendLine();

        sb.AppendLine("CLAY REQUIREMENT AUDIT:");
        foreach (ClayRequirement requirement in ClayRequirements())
            sb.AppendLine($"  MISSING — {requirement.Name}: {requirement.Evidence}");
        sb.AppendLine();

        sb.AppendLine("ADVERSARIAL ATTACK:");
        sb.AppendLine("  - Finite connected graphs have positive Laplacian gaps kinematically.");
        sb.AppendLine("  - Scaling by 1/a² at fixed compact circumference preserves a compact-domain");
        sb.AppendLine("    continuum gap; it does not establish an infinite-volume interacting gap.");
        sb.AppendLine("  - The graph Laplacian acts on site functions, not on the physical");
        sb.AppendLine("    gauge-invariant Yang–Mills Hilbert space (for example, glueball states).");
        sb.AppendLine("  - QG244's Lagrangian form does not supply a measure/Hilbert space, Gauss law,");
        sb.AppendLine("    interacting Hamiltonian/transfer matrix, or controlled limits.");
        sb.AppendLine("  - QG161/169/171/246/260/262/300 reuse λ₂ as a 'mass-gap scale'");
        sb.AppendLine("    across sectors; that nomenclature is not an independent pure-YM spectrum.");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - DERIVED: finite-network kinematic gap λ_gap=0.386350893377790 and");
        sb.AppendLine("    ω_gap=0.621571309969974 for C_96(1..6), in graph units.");
        sb.AppendLine("  - EMERGENT: a plausible finite-regulator scaffold, not a completed regulator.");
        sb.AppendLine("  - REFUTED: any claim that current AT solves the Clay Yang–Mills problem.");
        sb.AppendLine("  - No physical mass scale is inferred; canonical D-series classifications are unchanged.");

        Output.WriteLine(sb.ToString());
    }

    private sealed record ClayRequirement(
        string Name,
        bool SatisfiedByCurrentAtEvidence,
        string Evidence);
}
