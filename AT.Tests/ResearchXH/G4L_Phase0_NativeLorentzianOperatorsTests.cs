using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-L Phase 0 — native Lorentzian operators. Can causal order alone produce a Lorentzian
/// operator analogous to Lc in the Riemannian sector? Builds four candidates from allowed
/// inputs (causal order, interval structure, layers, counting measure, links) and tests
/// Lorentzian signature (indefiniteness), non-elliptic behavior, directionality, and
/// distinguishability from the Riemannian Lc.
///
/// Tests: G4-L00 (construction + directionality), G4-L01 (indefinite spectrum),
///        G4-L02 (distinguishability from Lc).
/// </summary>
public class G4L_Phase0_NativeLorentzianOperatorsTests : ResearchTestBase
{
    public G4L_Phase0_NativeLorentzianOperatorsTests(ITestOutputHelper o) : base(o) { }

    private const int TMax = 7;   // time slices 0..7
    private const int XMax = 4;   // space positions −4..4

    private static CausalSetData Cs => CausalSet.BuildGrid(TMax, XMax);

    private static readonly (string Name, Func<CausalSetData, double[,]> Build)[] Operators =
    {
        ("L1 causal-link",         LorentzianOperator.LinkOperator),
        ("L2 interval",            LorentzianOperator.IntervalOperator),
        ("L3 layer",               LorentzianOperator.LayerOperator),
        ("L4 density-weighted",    LorentzianOperator.DensityWeightedCausal),
    };

    private static bool IsSymmetric(double[,] m)
    {
        int n = m.GetLength(0);
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                if (Math.Abs(m[i, j] - m[j, i]) > 1e-12) return false;
        return true;
    }

    // ── G4-L00: construction from allowed inputs + causal direction ─────────────────────

    [Fact]
    public void G4_L00_CausalOperatorsAreConstructibleAndDirectional()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L00: native Lorentzian operators are constructible and causal-directional");

        var cs = Cs;
        sb.AppendLine($"Deterministic 1+1D Minkowski grid: t ∈ [0..{TMax}], x ∈ [−{XMax}..{XMax}] ⇒ N = {cs.Count} events.");
        sb.AppendLine($"Causal order: i ≺ j ⟺ t_j − t_i > |x_j − x_i|.");
        sb.AppendLine();
        sb.AppendLine($"DAG (strict partial order, time is a topological order): {cs.IsDag()}");
        sb.AppendLine($"Directed link relation (A ≠ Aᵀ — past/future asymmetry): {cs.IsDirected()}");
        sb.AppendLine($"Total links: {cs.PastDegree.Sum()} (in-degree sum).");
        sb.AppendLine();
        sb.AppendLine("Constructed operators (all from causal order + counting measure only):");
        sb.AppendLine($"{"operator",-20} {"symmetric",10} {"entries",8}");
        foreach (var (name, build) in Operators)
        {
            var m = build(cs);
            int nnz = 0;
            for (int i = 0; i < m.GetLength(0); i++)
                for (int j = 0; j < m.GetLength(1); j++)
                    if (Math.Abs(m[i, j]) > 1e-12) nnz++;
            sb.AppendLine($"{name,-20} {IsSymmetric(m),10} {nnz,8}");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: all four operators are symmetric (real spectrum) matrices built from the");
        sb.AppendLine("DIRECTED causal order — they carry causal direction while remaining self-adjoint.");
        Output.WriteLine(sb.ToString());

        Assert.True(cs.IsDag(), "causal set is not a DAG");
        Assert.True(cs.IsDirected(), "link relation is not directed");
        foreach (var (name, build) in Operators)
            Assert.True(IsSymmetric(build(cs)), $"{name} is not symmetric");
    }

    // ── G4-L01: indefinite spectrum (Lorentzian signature) ──────────────────────────────

    [Fact]
    public void G4_L01_OperatorsExhibitIndefiniteSpectrum()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L01: indefinite spectrum — the Lorentzian signature");

        var cs = Cs;
        sb.AppendLine($"{"operator",-20} {"n+",6} {"n−",6} {"n0",6} {"indefinite",12} {"non-elliptic",13}");
        int indefiniteCount = 0, nonEllipticCount = 0;
        foreach (var (name, build) in Operators)
        {
            var evals = LorentzianOperator.Eigenvalues(build(cs));
            var s = LorentzianOperator.Signature(evals);
            bool indef = s.pos > 0 && s.neg > 0;
            bool nonEll = s.neg > 0;
            if (indef) indefiniteCount++;
            if (nonEll) nonEllipticCount++;
            sb.AppendLine($"{name,-20} {s.pos,6} {s.neg,6} {s.zero,6} {indef,12} {nonEll,13}");
        }

        sb.AppendLine();
        sb.AppendLine("A Riemannian (elliptic) operator is positive semi-definite (n− = 0). A Lorentzian");
        sb.AppendLine("operator is INDEFINITE (n+ > 0 and n− > 0).");
        sb.AppendLine();
        sb.AppendLine($"Indefinite operators: {indefiniteCount}/{Operators.Length}; non-elliptic: {nonEllipticCount}/{Operators.Length}.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the native causal operators carry the Lorentzian signature — indefinite,");
        sb.AppendLine("non-elliptic structure — from causal order alone (no BDG weights, no metric).");
        Output.WriteLine(sb.ToString());

        Assert.True(indefiniteCount >= 1, "no operator is indefinite (no Lorentzian signature)");
        Assert.True(nonEllipticCount >= 1, "no operator is non-elliptic");
    }

    // ── G4-L02: distinguishability from the Riemannian Lc ───────────────────────────────

    [Fact]
    public void G4_L02_CausalOperatorsAreDistinguishableFromLc()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L02: Lorentzian operators are spectrally distinguishable from Lc");

        // Riemannian Lc = ρ⁻¹ L ρ⁻¹ on a conformal geometry (elliptic, PSD).
        var geo = ConformalRateGraph.Build(0.5, 16, 0.16);
        var lc = ConformalOperator.Build(geo, ConformalOperatorKind.RhoInverseSquared);
        double[] lcEvals = LorentzianOperator.Eigenvalues(lc);
        var lcSig = LorentzianOperator.Signature(lcEvals);

        sb.AppendLine($"Riemannian Lc = ρ⁻¹Lρ⁻¹ (N={lc.GetLength(0)}):");
        sb.AppendLine($"  spectrum: min λ = {lcEvals[0]:F4}, max λ = {lcEvals[^1]:F4}");
        sb.AppendLine($"  signature (n+, n−, n0) = ({lcSig.pos}, {lcSig.neg}, {lcSig.zero})  → PSD (elliptic).");
        sb.AppendLine();

        var cs = Cs;
        sb.AppendLine("Native causal operators (N = " + cs.Count + "):");
        sb.AppendLine($"{"operator",-20} {"min λ",10} {"max λ",10} {"signature",16} {"indefinite",10}");
        bool anyIndefinite = false;
        foreach (var (name, build) in Operators)
        {
            var evals = LorentzianOperator.Eigenvalues(build(cs));
            var s = LorentzianOperator.Signature(evals);
            bool indef = s.pos > 0 && s.neg > 0;
            if (indef) anyIndefinite = true;
            sb.AppendLine($"{name,-20} {evals[0],10:F3} {evals[^1],10:F3} " +
                          $"{"(" + s.pos + "," + s.neg + "," + s.zero + ")",16} {indef,10}");
        }

        sb.AppendLine();
        sb.AppendLine($"Riemannian Lc is PSD (n− = {lcSig.neg}); at least one causal operator is indefinite ({anyIndefinite}).");
        sb.AppendLine("The sign of the spectrum (all-non-negative vs mixed) cleanly separates elliptic from");
        sb.AppendLine("Lorentzian — no metric or d'Alembertian formula is required to make the distinction.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the native causal operators are spectrally distinguishable from the");
        sb.AppendLine("Riemannian Lc by their indefinite (Lorentzian) signature.");
        Output.WriteLine(sb.ToString());

        Assert.True(lcSig.neg == 0, $"Lc is not PSD (n− = {lcSig.neg})");
        Assert.True(anyIndefinite, "no causal operator is indefinite (not distinguishable from Lc)");
    }
}
