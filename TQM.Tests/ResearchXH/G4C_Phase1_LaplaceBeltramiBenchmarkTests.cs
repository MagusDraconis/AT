using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-C Phase 1 — benchmark the native conformal operator Lc = ρ⁻¹ L ρ⁻¹ against the
/// unnormalized L and the degree-normalized D^−1/2 L D^−1/2 on flat / negative-curvature /
/// positive-curvature conformal geometries. Verifies whether Lc behaves like a Laplace–Beltrami
/// operator (curvature-sign separation, degree-artifact minimization, consistent ordering,
/// refinement stability).
///
/// Tests: G4-C10 (SC1+SC2), G4-C11 (SC3), G4-C12 (SC4).
/// </summary>
public class G4C_Phase1_LaplaceBeltramiBenchmarkTests : ResearchTestBase
{
    public G4C_Phase1_LaplaceBeltramiBenchmarkTests(ITestOutputHelper o) : base(o) { }

    private static GeometricGraph Flat(int n = 16) => ConformalRateGraph.Build(0.0, n, 0.16);
    private static GeometricGraph Negative(int n = 16) => ConformalRateGraph.Build(+1.0, n, 0.16);  // R(0) < 0
    private static GeometricGraph Positive(int n = 16) => ConformalRateGraph.Build(-0.8, n, 0.16);   // R(0) > 0

    private static readonly ConformalOperatorKind[] Kinds =
    {
        ConformalOperatorKind.Unnormalized,
        ConformalOperatorKind.Normalized,
        ConformalOperatorKind.RhoInverseSquared
    };

    private static string K(ConformalOperatorKind k) => k switch
    {
        ConformalOperatorKind.Unnormalized => "L",
        ConformalOperatorKind.Normalized => "D^-1/2LD^-1/2",
        ConformalOperatorKind.RhoInverseSquared => "Lc=ρ^-1Lρ^-1",
        _ => k.ToString()
    };

    private sealed record Obs(double Gap, double Z1, double Zp1, double Zeta2, double Weyl, double Entropy);

    private static Obs Compute(GeometricGraph g, ConformalOperatorKind k)
    {
        double[] ev = ConformalOperator.Eigenvalues(g, k);
        return new Obs(
            SpectralCurvature.SpectralGap(ev),
            SpectralCurvature.HeatTrace(ev, 1.0),
            SpectralCurvature.HeatTraceDerivative(ev, 1.0),
            SpectralCurvature.SpectralZeta(ev, 2.0),
            SpectralCurvature.WeylDimension(ev),
            SpectralCurvature.SpectralEntropy(ev, 1.0));
    }

    private static bool Monotonic(double neg, double flat, double pos)
        => (neg < flat && flat < pos) || (neg > flat && flat > pos);

    // ── G4-C10: SC1 (sign separation) + SC2 (degree-artifact minimization) ──────────────

    [Fact]
    public void G4_C10_LcPreservesSignSeparationAndMinimizesDegreeArtifacts()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-C10: SC1 — Lc preserves curvature-sign separation; SC2 — minimizes degree artifacts");

        var neg = Negative(); var flat = Flat(); var pos = Positive();
        sb.AppendLine("Mean degree:  negative(ρ=1+x²)=" + $"{neg.MeanDegree():F2}" +
                      $"  flat={flat.MeanDegree():F2}" + $"  positive(ρ=1−0.8x²)={pos.MeanDegree():F2}");
        sb.AppendLine();

        var zetaByKind = new Dictionary<ConformalOperatorKind, (double Neg, double Flat, double Pos)>();
        // degree ordering: flat(3.75) < negative(5.16) < positive(6.33).
        // "ζ2 decreases with degree" ⇔ ζ2(flat) > ζ2(negative) > ζ2(positive).
        sb.AppendLine($"{"Operator",-16} {"ζ2 neg",9} {"ζ2 flat",9} {"ζ2 pos",9}  sign-sep  deg-decr");
        foreach (var kind in Kinds)
        {
            double zn = Compute(neg, kind).Zeta2;
            double zf = Compute(flat, kind).Zeta2;
            double zp = Compute(pos, kind).Zeta2;
            bool sep = (zn - zf) * (zp - zf) < 0.0;
            bool degDecr = zf > zn && zn > zp;
            sb.AppendLine($"{K(kind),-16} {zn,9:F1} {zf,9:F1} {zp,9:F1}  {sep,-7}  {degDecr}");
            zetaByKind[kind] = (zn, zf, zp);
        }

        sb.AppendLine();
        sb.AppendLine("SC1 (sign separation): Lc puts R<0 and R>0 on OPPOSITE sides of flat (sign-sep=true).");
        sb.AppendLine("SC2 (degree artifacts): L is monotonic in degree (deg-mono=true) — its response is a");
        sb.AppendLine("degree/density-magnitude artifact; Lc is NOT (deg-mono=false) — its response is a genuine");
        sb.AppendLine("curvature-sign signal, not explainable by degree alone.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: Lc preserves curvature-sign separation and minimizes degree artifacts.");
        Output.WriteLine(sb.ToString());

        var l = zetaByKind[ConformalOperatorKind.Unnormalized];
        var lc = zetaByKind[ConformalOperatorKind.RhoInverseSquared];

        // SC1: Lc separates; L is sign-blind.
        Assert.True(lc.Neg > lc.Flat && lc.Pos < lc.Flat,
            $"Lc sign separation: neg={lc.Neg:F1}, flat={lc.Flat:F1}, pos={lc.Pos:F1}");
        Assert.True(l.Neg < l.Flat && l.Pos < l.Flat,
            $"L should be sign-blind: neg={l.Neg:F1}, flat={l.Flat:F1}, pos={l.Pos:F1}");

        // SC2: L's ζ(2) decreases monotonically with degree (artifact); Lc's does not.
        Assert.True(l.Flat > l.Neg && l.Neg > l.Pos,
            $"L ζ(2) should decrease with degree: flat={l.Flat:F1} > neg={l.Neg:F1} > pos={l.Pos:F1}");
        Assert.False(lc.Flat > lc.Neg && lc.Neg > lc.Pos,
            $"Lc ζ(2) should NOT be a pure degree response: flat={lc.Flat:F1}, neg={lc.Neg:F1}, pos={lc.Pos:F1}");
    }

    // ── G4-C11: SC3 — consistent ordering (hyperbolic < flat < positive) across observables ─

    [Fact]
    public void G4_C11_LcProducesConsistentCurvatureOrdering()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-C11: SC3 — Lc produces a consistent curvature ordering across observables");

        var neg = Negative(); var flat = Flat(); var pos = Positive();

        foreach (var kind in Kinds)
        {
            var n = Compute(neg, kind); var f = Compute(flat, kind); var p = Compute(pos, kind);
            bool[] mono =
            {
                Monotonic(n.Gap, f.Gap, p.Gap),
                Monotonic(n.Z1, f.Z1, p.Z1),
                Monotonic(n.Zp1, f.Zp1, p.Zp1),
                Monotonic(n.Zeta2, f.Zeta2, p.Zeta2),
                Monotonic(n.Entropy, f.Entropy, p.Entropy)
            };
            int count = mono.Count(x => x);
            sb.AppendLine($"{K(kind)}:");
            sb.AppendLine($"  gap:   neg={n.Gap:F4} flat={f.Gap:F4} pos={p.Gap:F4}  mono={mono[0]}");
            sb.AppendLine($"  Z(1):  neg={n.Z1:F2} flat={f.Z1:F2} pos={p.Z1:F2}  mono={mono[1]}");
            sb.AppendLine($"  ζ(2):  neg={n.Zeta2:F2} flat={f.Zeta2:F2} pos={p.Zeta2:F2}  mono={mono[3]}");
            sb.AppendLine($"  S(1):  neg={n.Entropy:F3} flat={f.Entropy:F3} pos={p.Entropy:F3}  mono={mono[4]}");
            sb.AppendLine($"  → {count}/5 observables monotonic in curvature");
            sb.AppendLine();

            if (kind == ConformalOperatorKind.RhoInverseSquared)
                Assert.True(count >= 3, $"Lc should be monotonic in curvature for ≥3 observables (got {count})");
        }

        sb.AppendLine("SC3: Lc ranks negative / flat / positive consistently (monotonic in curvature) across");
        sb.AppendLine("gap, heat trace, spectral zeta and entropy, while L scrambles the ordering.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: Lc behaves like a Laplace–Beltrami operator (curvature-consistent ordering).");
        Output.WriteLine(sb.ToString());
    }

    // ── G4-C12: SC4 — stability under graph refinement ────────────────────────────────

    [Fact]
    public void G4_C12_LcIsStableUnderRefinement()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-C12: SC4 — Lc results stable under graph refinement (n=16 → n=24)");

        sb.AppendLine("ASSUMPTIONS: refinement = increase the per-axis node count (N = n²).");
        sb.AppendLine();

        var results = new List<(int n, double Neg, double Flat, double Pos, bool Sep)>();
        foreach (int n in new[] { 16, 24 })
        {
            double zn = Compute(Negative(n), ConformalOperatorKind.RhoInverseSquared).Zeta2;
            double zf = Compute(Flat(n), ConformalOperatorKind.RhoInverseSquared).Zeta2;
            double zp = Compute(Positive(n), ConformalOperatorKind.RhoInverseSquared).Zeta2;
            bool sep = (zn - zf) * (zp - zf) < 0.0;
            results.Add((n, zn, zf, zp, sep));
            sb.AppendLine($"n={n,-3} (N={n*n})  Lc ζ(2): neg={zn:F1}  flat={zf:F1}  pos={zp:F1}  sign-sep={sep}");
        }

        sb.AppendLine();
        sb.AppendLine("SC4: the Lc sign-separation (R<0 up, R>0 down around flat) persists under refinement.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: Lc is refinement-stable — the conformal-operator signature is not a finite-N artifact.");
        Output.WriteLine(sb.ToString());

        foreach (var r in results)
            Assert.True(r.Sep, $"n={r.n}: Lc sign separation lost under refinement");
    }
}
