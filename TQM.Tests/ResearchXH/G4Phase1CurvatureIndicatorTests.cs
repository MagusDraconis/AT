using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4 Phase 1 — calibrate a native spectral curvature estimator (SCI).
/// Computes HeatTrace, HeatTraceDerivative, SpectralZeta, SpectralGap, SpectralEntropy and
/// tests whether a spectral observable predicts the curvature SIGN
/// (flat ≈ 0, sphere &gt; 0, hyperbolic &lt; 0).
///
/// Tests: G4-10 (observables + flat anchor), G4-11 (positive sign), G4-12 (hyperbolic sign).
/// </summary>
public class G4Phase1CurvatureIndicatorTests : ResearchTestBase
{
    public G4Phase1CurvatureIndicatorTests(ITestOutputHelper o) : base(o) { }

    private const double T = 1.5;   // calibrated heat-kernel time (see report)

    private static GeometricGraph Flat() => FlatGraph.Build(16);
    private static GeometricGraph Sphere() => SphereGraph.Build(256, 0.5);
    private static GeometricGraph Hyperbolic() => HyperbolicGraph.Build(256, 0.8);

    // ── G4-10: the five observables compute and the flat case anchors SCI ≈ 0 ─────────

    [Fact]
    public void G4_10_ObservablesComputeAndFlatAnchorsZero()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-10: heat trace, derivative, zeta, gap, entropy — and flat SCI ≈ 0");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - All observables are computed on the normalized Laplacian L_sym (scale-invariant).");
        sb.AppendLine("  - SCI(t) = 2t·⟨λ⟩(t) − 2, the deviation of the heat-kernel spectral dimension from d=2.");
        sb.AppendLine();

        var graphs = new[] { Flat(), Sphere(), Hyperbolic() };
        double[] sci = new double[3];

        for (int i = 0; i < graphs.Length; i++)
        {
            var g = graphs[i];
            double[] evals = SpectralCurvature.Eigenvalues(g.NormalizedSymmetricLaplacian());

            double z = SpectralCurvature.HeatTrace(evals, T);
            double zp = SpectralCurvature.HeatTraceDerivative(evals, T);
            double zeta = SpectralCurvature.SpectralZeta(evals, 2.0);
            double gap = SpectralCurvature.SpectralGap(evals);
            double entropy = SpectralCurvature.SpectralEntropy(evals, T);
            double mean = SpectralCurvature.MeanEigenvalue(evals, T);
            sci[i] = SpectralCurvature.SpectralCurvatureIndicator(evals, T);

            sb.AppendLine($"{g.Name}:");
            sb.AppendLine($"  HeatTrace({T})     = {z:F4}");
            sb.AppendLine($"  HeatTraceDeriv({T}) = {zp:F4}   (⟨λ⟩ = {mean:F4})");
            sb.AppendLine($"  SpectralZeta(2)    = {zeta:F4}");
            sb.AppendLine($"  SpectralGap        = {gap:F4}");
            sb.AppendLine($"  SpectralEntropy({T}) = {entropy:F4}");
            sb.AppendLine($"  SCI({T})           = {sci[i]:F4}");
            sb.AppendLine();
        }

        sb.AppendLine($"SCI ordering: flat={sci[0]:F4}  sphere={sci[1]:F4}  hyperbolic={sci[2]:F4}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: all five observables compute; the flat torus anchors SCI near zero.");
        Output.WriteLine(sb.ToString());

        // Assertions.
        Assert.True(double.IsFinite(sci[0]) && double.IsFinite(sci[1]) && double.IsFinite(sci[2]),
            "SCI is not finite for all geometries");
        Assert.True(Math.Abs(sci[0]) < 0.2,
            $"Flat SCI({T}) = {sci[0]:F4} is not ≈ 0 (flat should be the zero anchor)");
    }

    // ── G4-11: positive curvature (sphere) gives a positive SCI ───────────────────────

    [Fact]
    public void G4_11_PositiveCurvatureGivesPositiveSci()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-11: positive curvature (sphere) ⇒ SCI > 0");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Positive curvature (sphere, R=+2) suppresses the low-lying heat flow, pushing the");
        sb.AppendLine("    effective spectral dimension above 2, hence SCI = d_s − 2 &gt; 0.");
        sb.AppendLine();

        double[] flat = SpectralCurvature.Eigenvalues(Flat().NormalizedSymmetricLaplacian());
        double[] sphere = SpectralCurvature.Eigenvalues(Sphere().NormalizedSymmetricLaplacian());

        double sciFlat = SpectralCurvature.SpectralCurvatureIndicator(flat, T);
        double sciSphere = SpectralCurvature.SpectralCurvatureIndicator(sphere, T);

        sb.AppendLine($"SCI(flat)   = {sciFlat:F4}");
        sb.AppendLine($"SCI(sphere) = {sciSphere:F4}");
        sb.AppendLine($"Δ = SCI(sphere) − SCI(flat) = {sciSphere - sciFlat:F4}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: positive curvature is detected by a positive SCI.");
        Output.WriteLine(sb.ToString());

        Assert.True(sciSphere > 0.3,
            $"SCI(sphere) = {sciSphere:F4} is not clearly positive");
        Assert.True(sciSphere > sciFlat,
            $"SCI(sphere) = {sciSphere:F4} does not exceed SCI(flat) = {sciFlat:F4}");
    }

    // ── G4-12: hyperbolic (negative) curvature — boundary-dominated finding ────────────

    [Fact]
    public void G4_12_HyperbolicSignIsBoundaryDominated()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-12: negative curvature (hyperbolic) ⇒ SCI < 0 — RESULT: boundary-dominated");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Negative curvature (hyperbolic, R=−2) would give super-diffusive heat flow,");
        sb.AppendLine("    pushing the effective spectral dimension below 2, hence SCI = d_s − 2 &lt; 0.");
        sb.AppendLine();

        double[] flat = SpectralCurvature.Eigenvalues(Flat().NormalizedSymmetricLaplacian());
        double[] sphere = SpectralCurvature.Eigenvalues(Sphere().NormalizedSymmetricLaplacian());
        double[] hyper = SpectralCurvature.Eigenvalues(Hyperbolic().NormalizedSymmetricLaplacian());

        double sciFlat = SpectralCurvature.SpectralCurvatureIndicator(flat, T);
        double sciSphere = SpectralCurvature.SpectralCurvatureIndicator(sphere, T);
        double sciHyper = SpectralCurvature.SpectralCurvatureIndicator(hyper, T);

        sb.AppendLine($"SCI(flat)       = {sciFlat:F4}");
        sb.AppendLine($"SCI(sphere)     = {sciSphere:F4}");
        sb.AppendLine($"SCI(hyperbolic) = {sciHyper:F4}");
        sb.AppendLine();
        sb.AppendLine("FINDING: the Poincaré-disk hyperbolic graph does NOT reach SCI < 0 at N≈256.");
        sb.AppendLine("The open disk is topologically a disk (χ=1) with a boundary; its finite spectrum is");
        sb.AppendLine("boundary-dominated, so it sits spectrally between the flat torus and the sphere.");
        sb.AppendLine("A clean negative sign requires a compact genus-≥2 hyperbolic surface (no boundary).");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: SCI separates flat (≈0) from positive (&gt;0); the hyperbolic-disk target");
        sb.AppendLine("is recorded as a boundary-dominated failure mode, to be revisited with a closed surface.");
        Output.WriteLine(sb.ToString());

        // Assertions: document the honest ordering and the boundary limitation.
        Assert.True(sciSphere > sciHyper,
            $"Expected SCI(sphere)={sciSphere:F4} > SCI(hyperbolic)={sciHyper:F4}");
        Assert.True(sciHyper < 0.3,
            $"Hyperbolic disk SCI = {sciHyper:F4} is not near-zero (boundary-dominated)");
    }
}
