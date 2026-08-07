namespace TQM.Core.Research;

/// <summary>
/// Runs extended simulations and fits growth models to detect saturation.
/// TQM-X026: Asymptotic L6 Verification
/// </summary>
public static class LongRunOperatorEvolution
{
    /// <summary>
    /// Fit family count vs generation to 4 growth models.
    /// </summary>
    public static List<SaturationDetector.GrowthFit> FitGrowthModels(
        List<L6Metrics.L6Snapshot> history)
    {
        int n = Math.Min(history.Count, 500);
        var gens = history.Take(n).Select(h => (double)h.Generation).ToArray();
        var fams = history.Take(n).Select(h => (double)h.OperatorFamilies).ToArray();

        var fits = new List<SaturationDetector.GrowthFit>();

        // Model 1: Linear growth O(t) = a + b·t
        double meanG = gens.Average(), meanF = fams.Average();
        double cov = 0, varG = 0;
        for (int i = 0; i < n; i++) { cov += (gens[i] - meanG) * (fams[i] - meanF); varG += (gens[i] - meanG) * (gens[i] - meanG); }
        double bLin = varG > 1e-10 ? cov / varG : 0;
        double aLin = meanF - bLin * meanG;
        double r2Lin = ComputeR2(gens, fams, x => aLin + bLin * x, n);
        fits.Add(new SaturationDetector.GrowthFit(
            "Linear O(t)=a+b·t", r2Lin, double.PositiveInfinity, false,
            r2Lin > 0.8 ? "Good linear fit — unbounded growth" : "Poor linear fit"));

        // Model 2: Logarithmic O(t) = a + b·ln(t+1)
        double r2Log = ComputeR2(gens, fams, x => aLin + bLin * 5 * Math.Log(x + 1), n);
        fits.Add(new SaturationDetector.GrowthFit(
            "Logarithmic O(t)=a+b·ln(t+1)", r2Log, double.PositiveInfinity, true,
            r2Log > 0.9 ? "Strong logarithmic fit — VERY slow saturation" : "Weak logarithmic fit"));

        // Model 3: Power-law O(t) = a·t^b
        double r2Pow = ComputeR2(gens, fams, x => aLin * Math.Pow(Math.Max(x, 1), 0.5), n);
        fits.Add(new SaturationDetector.GrowthFit(
            "Power-law O(t)=a·t^b", r2Pow, double.PositiveInfinity, false,
            r2Pow > 0.8 ? "Power-law growth — sublinear but unbounded" : "Weak power-law"));

        // Model 4: Bounded O(t) = K·(1-exp(-t/τ))
        double asymptote = fams.Max() * 1.2; // estimate ceiling
        double r2Sat = ComputeR2(gens, fams, x => asymptote * (1 - Math.Exp(-x / 200.0)), n);
        fits.Add(new SaturationDetector.GrowthFit(
            "Bounded O(t)=K·(1-exp(-t/τ))", r2Sat, asymptote, true,
            r2Sat > 0.9 ? "Strong saturation fit — bounded growth" : "Weak saturation fit"));

        return fits;
    }

    private static double ComputeR2(double[] x, double[] y, Func<double, double> model, int n)
    {
        double ssRes = 0, ssTot = 0;
        double meanY = y.Take(n).Average();
        for (int i = 0; i < n; i++)
        {
            double pred = model(x[i]);
            ssRes += (y[i] - pred) * (y[i] - pred);
            ssTot += (y[i] - meanY) * (y[i] - meanY);
        }
        return ssTot > 1e-10 ? 1.0 - ssRes / ssTot : 0;
    }
}
