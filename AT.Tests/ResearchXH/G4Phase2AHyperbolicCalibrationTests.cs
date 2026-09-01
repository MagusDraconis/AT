using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4 Phase 2A — calibrate the Spectral Curvature Indicator (SCI) for NEGATIVE curvature by
/// replacing the open Poincaré disk with compact genus-≥2 hyperbolic surfaces
/// (Desargues G(10,3), Nauru G(12,5)). Target: Sphere SCI &gt; 0, Flat SCI ≈ 0, Hyperbolic SCI &lt; 0.
///
/// Tests: G4-2A-00 (validity), G4-2A-01 (observables), G4-2A-02 (sign calibration).
/// </summary>
public class G4Phase2AHyperbolicCalibrationTests : ResearchTestBase
{
    public G4Phase2AHyperbolicCalibrationTests(ITestOutputHelper o) : base(o) { }

    private const double T = 1.5;

    private static GeometricGraph Flat() => FlatGraph.Build(16);
    private static GeometricGraph Sphere() => SphereGraph.Build(256, 0.5);
    private static GeometricGraph Desargues() => CompactHyperbolicGraph.Desargues();
    private static GeometricGraph Nauru() => CompactHyperbolicGraph.Nauru();

    // ── G4-2A-00: compact hyperbolic surface graphs are valid and deterministic ─────────

    [Fact]
    public void G4_2A_00_CompactHyperbolicGraphsAreValid()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-2A-00: compact genus-≥2 hyperbolic surface graphs");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Desargues G(10,3): cubic, 20 vertices, girth 6, genus 2 (χ = −2).");
        sb.AppendLine("  - Nauru G(12,5): cubic, 24 vertices, girth 6, genus 4 (χ = −6).");
        sb.AppendLine("  - Built as generalized Petersen graphs (boundary-free, cubic).");
        sb.AppendLine();

        foreach (var g in new[] { Desargues(), Nauru() })
        {
            var a = g.Adjacency;
            int n = g.VertexCount;
            int[] deg = g.Degrees();
            bool cubic = deg.All(d => d == 3);
            bool symmetric = true, noSelfLoops = true;
            for (int i = 0; i < n; i++)
            {
                if (a[i, i] != 0.0) noSelfLoops = false;
                for (int j = 0; j < n; j++)
                    if (a[i, j] != a[j, i]) symmetric = false;
            }
            sb.AppendLine($"{g.Name}: N={n}, cubic={cubic}, symmetric={symmetric}, " +
                          $"noSelfLoops={noSelfLoops}, connected={g.IsConnected()}");

            Assert.True(cubic, $"{g.Name}: not cubic");
            Assert.True(symmetric && noSelfLoops, $"{g.Name}: adjacency invalid");
            Assert.True(g.IsConnected(), $"{g.Name}: not connected");
        }

        Assert.True(SameMatrix(Desargues().Adjacency, Desargues().Adjacency), "Desargues not deterministic");
        Assert.True(SameMatrix(Nauru().Adjacency, Nauru().Adjacency), "Nauru not deterministic");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: two valid, deterministic compact hyperbolic surface graphs.");
        Output.WriteLine(sb.ToString());
    }

    // ── G4-2A-01: heat trace and spectral zeta compute for all four geometries ──────────

    [Fact]
    public void G4_2A_01_HeatTraceAndZetaCompute()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-2A-01: heat trace and spectral zeta (flat / sphere / genus-2 / genus-4)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Observables on the normalized Laplacian (scale-invariant).");
        sb.AppendLine();

        var graphs = new[] { Flat(), Sphere(), Desargues(), Nauru() };
        sb.AppendLine($"  {"Geometry",-22} {"N",4} {"gap",7} {"Z(1)",8} {"ζ(2)",8}");
        foreach (var g in graphs)
        {
            double[] ev = SpectralCurvature.Eigenvalues(g.NormalizedSymmetricLaplacian());
            double gap = SpectralCurvature.SpectralGap(ev);
            double z1 = SpectralCurvature.HeatTrace(ev, 1.0);
            double zeta = SpectralCurvature.SpectralZeta(ev, 2.0);
            sb.AppendLine($"  {g.Name,-22} {ev.Length,4} {gap,7:F4} {z1,8:F2} {zeta,8:F2}");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: heat trace and spectral zeta computed for all geometries.");
        Output.WriteLine(sb.ToString());

        foreach (var g in graphs)
        {
            double[] ev = SpectralCurvature.Eigenvalues(g.NormalizedSymmetricLaplacian());
            Assert.True(double.IsFinite(SpectralCurvature.HeatTrace(ev, 1.0)), $"{g.Name}: Z(1) not finite");
            Assert.True(double.IsFinite(SpectralCurvature.SpectralZeta(ev, 2.0)), $"{g.Name}: ζ(2) not finite");
        }
    }

    // ── G4-2A-02: SCI sign calibration (sphere>0, flat≈0, hyperbolic<0) ────────────────

    [Fact]
    public void G4_2A_02_SciSignCalibration()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-2A-02: SCI sign — sphere > 0, flat ≈ 0, hyperbolic < 0");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - SCI(t) = 2t·⟨λ⟩(t) − 2 (deviation of the heat-kernel spectral dimension from 2).");
        sb.AppendLine("  - Compact genus-2+ surfaces replace the boundary-dominated Poincaré disk.");
        sb.AppendLine();

        double sciFlat = Sci(Flat());
        double sciSphere = Sci(Sphere());
        double sciDesargues = Sci(Desargues());
        double sciNauru = Sci(Nauru());

        sb.AppendLine($"SCI(flat, t={T})        = {sciFlat:F4}");
        sb.AppendLine($"SCI(sphere, t={T})      = {sciSphere:F4}");
        sb.AppendLine($"SCI(Desargues, t={T})   = {sciDesargues:F4}");
        sb.AppendLine($"SCI(Nauru, t={T})       = {sciNauru:F4}");
        sb.AppendLine();
        sb.AppendLine("TARGET CHECK:");
        sb.AppendLine($"  sphere > 0    : {sciSphere:F4}  {(sciSphere > 0.0 ? "PASS" : "FAIL")}");
        sb.AppendLine($"  flat ≈ 0      : {sciFlat:F4}  {(Math.Abs(sciFlat) < 0.2 ? "PASS" : "FAIL")}");
        sb.AppendLine($"  hyperbolic < 0: {Math.Min(sciDesargues, sciNauru):F4}  {(Math.Min(sciDesargues, sciNauru) < 0.0 ? "PASS" : "FAIL")}");
        sb.AppendLine();
        sb.AppendLine("CAVEAT: SCI = 2t⟨λ⟩−2 is degree-dependent. The dense sphere (deg 15.7) vs the cubic");
        sb.AppendLine("compact surfaces (deg 3) contributes to the sign separation; see the Phase-2A report.");
        Output.WriteLine(sb.ToString());

        Assert.True(sciSphere > 0.3, $"SCI(sphere) = {sciSphere:F4} not clearly positive");
        Assert.True(Math.Abs(sciFlat) < 0.2, $"SCI(flat) = {sciFlat:F4} not ≈ 0");
        Assert.True(sciDesargues < -0.15, $"SCI(Desargues) = {sciDesargues:F4} not negative");
        Assert.True(sciNauru < -0.15, $"SCI(Nauru) = {sciNauru:F4} not negative");
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static double Sci(GeometricGraph g)
        => SpectralCurvature.SpectralCurvatureIndicator(
            SpectralCurvature.Eigenvalues(g.NormalizedSymmetricLaplacian()), T);

    private static bool SameMatrix(double[,] a, double[,] b)
    {
        if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1)) return false;
        for (int i = 0; i < a.GetLength(0); i++)
            for (int j = 0; j < a.GetLength(1); j++)
                if (a[i, j] != b[i, j]) return false;
        return true;
    }
}
