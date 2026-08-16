namespace TQM.Core.ResearchXH;

/// <summary>
/// One time frame of a curvature-dynamics trajectory: the conformal parameter A at time t,
/// the analytic scalar curvature R(0) = −4A of g = (1+A·x²)·η, the reconstructed curvature
/// score from Lc = ρ⁻¹ L ρ⁻¹, and the Lc spectral observables used to build it.
/// </summary>
public sealed record CurvatureFrame(
    double Time,
    double A,
    double ExpectedR,
    double Score,
    double MeanDensity,
    double Gap,
    double HeatTrace,
    double Zeta,
    double Entropy);

/// <summary>
/// G4-D Phase 0: evolve the conformal geometry through a time-series of density fields
/// ρ(x,t) = 1 + A(t)·x² and track how the reconstructed curvature (from Lc) follows the
/// analytic curvature R(0,t) = −4·A(t). Deterministic: closed-form graphs + symmetric EVD.
/// </summary>
public static class CurvatureDynamics
{
    /// <summary>
    /// Evolve the conformal geometry along a path A[0..steps], producing one frame per step.
    /// Values of A within 1e−12 of zero are snapped to 0 so the exact-flat frame has score 0.
    /// </summary>
    public static CurvatureFrame[] Evolve(double[] aPath, int nPerSide = 16,
        double epsilon = 0.16, double heatT = 1.0)
    {
        var flat = ConformalRateGraph.Build(0.0, nPerSide, epsilon);
        var frames = new CurvatureFrame[aPath.Length];
        for (int t = 0; t < aPath.Length; t++)
        {
            double a = Math.Abs(aPath[t]) < 1e-12 ? 0.0 : aPath[t];
            var g = ConformalRateGraph.Build(a, nPerSide, epsilon);
            double[] ev = ConformalOperator.Eigenvalues(g, ConformalOperatorKind.RhoInverseSquared);
            frames[t] = new CurvatureFrame(
                t,
                a,
                ConformalRateGraph.ConformalCurvature(a, 0.0),
                CurvatureReconstruction.Score(flat, g),
                g.VertexDensity().Average(),
                SpectralCurvature.SpectralGap(ev),
                SpectralCurvature.HeatTrace(ev, heatT),
                SpectralCurvature.SpectralZeta(ev, 2.0),
                SpectralCurvature.SpectralEntropy(ev, heatT));
        }
        return frames;
    }

    /// <summary>Linear sweep A(t) from aMin to aMax over `steps` intervals (steps+1 samples).</summary>
    public static double[] LinearSweep(int steps, double aMin, double aMax)
    {
        var a = new double[steps + 1];
        for (int t = 0; t <= steps; t++)
            a[t] = aMin + (aMax - aMin) * t / steps;
        return a;
    }

    /// <summary>One full cosine oscillation A(t) = amplitude·cos(2π t/steps) (steps+1 samples).</summary>
    public static double[] Oscillation(int steps, double amplitude)
    {
        var a = new double[steps + 1];
        for (int t = 0; t <= steps; t++)
            a[t] = amplitude * Math.Cos(2.0 * Math.PI * t / steps);
        return a;
    }

    /// <summary>Quadratic-in-time sweep A(t) = aMin + (aMax−aMin)·(t/steps)².</summary>
    public static double[] Quadratic(int steps, double aMin, double aMax)
    {
        var a = new double[steps + 1];
        for (int t = 0; t <= steps; t++)
        {
            double u = (double)t / steps;
            a[t] = aMin + (aMax - aMin) * u * u;
        }
        return a;
    }

    /// <summary>Gaussian pulse localized in time: A(t) = amplitude·exp(−((t−mid)/σ)²).</summary>
    public static double[] Localized(int steps, double amplitude)
    {
        var a = new double[steps + 1];
        double mid = steps / 2.0;
        double sigma = steps / 6.0;
        for (int t = 0; t <= steps; t++)
        {
            double z = (t - mid) / sigma;
            a[t] = amplitude * Math.Exp(-z * z);
        }
        return a;
    }

    /// <summary>Pearson correlation coefficient between two equal-length series.</summary>
    public static double Pearson(double[] x, double[] y)
    {
        int n = x.Length;
        double mx = 0.0, my = 0.0;
        for (int i = 0; i < n; i++) { mx += x[i]; my += y[i]; }
        mx /= n; my /= n;
        double sxx = 0.0, syy = 0.0, sxy = 0.0;
        for (int i = 0; i < n; i++)
        {
            double dx = x[i] - mx, dy = y[i] - my;
            sxx += dx * dx; syy += dy * dy; sxy += dx * dy;
        }
        return sxy / Math.Sqrt(sxx * syy);
    }
}
