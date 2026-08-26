namespace AT.Core.ResearchQG;

/// <summary>
/// QG-076 discrimination engine: χ²/AIC/BIC/Bayes-factor comparison of AT (rising)
/// vs MOND (fitted constant) vs NULL (local constant) against the OBSERVED g† values,
/// plus the gas precision required to reach 1σ/2σ/3σ/5σ AT-vs-MOND separation.
/// </summary>
public static class DiscriminationAnalyzer
{
    /// <summary>Model comparison against the observed per-galaxy g†, weighted by the
    /// full error budget at σ(log Mgas) = sigmaGasDex.</summary>
    public static DiscriminationRow[] Compute(GalaxyGasSystematics[] gals, double sigmaGasDex = 0.30)
    {
        int n = gals.Length;
        double chi2At = 0, chi2Null = 0;
        double sumW = 0, sw = 0;
        foreach (var g in gals)
        {
            double s = MonteCarloRARAnalyzer.TotalSigma(g, sigmaGasDex);
            double w = 1.0 / (s * s);
            double at = RARPhysics.LogGdaggerAt(g.Z);
            double nul = Math.Log10(RARPhysics.GdaggerLocal());
            chi2At += w * (g.LogGdagger - at) * (g.LogGdagger - at);
            chi2Null += w * (g.LogGdagger - nul) * (g.LogGdagger - nul);
            sumW += w;
            sw += w * g.LogGdagger;
        }
        double bestConst = sw / sumW;
        double chi2Mond = 0;
        foreach (var g in gals)
        {
            double s = MonteCarloRARAnalyzer.TotalSigma(g, sigmaGasDex);
            double w = 1.0 / (s * s);
            chi2Mond += w * (g.LogGdagger - bestConst) * (g.LogGdagger - bestConst);
        }

        double lnN = Math.Log(Math.Max(n, 1));
        double aicAt = chi2At + 0, bicAt = chi2At + 0 * lnN;
        double aicMond = chi2Mond + 2, bicMond = chi2Mond + 1 * lnN;
        double aicNull = chi2Null + 0, bicNull = chi2Null + 0 * lnN;

        // Bayes factors relative to MOND.
        double bfAt = Math.Exp(-0.5 * (chi2At - chi2Mond));
        double bfNull = Math.Exp(-0.5 * (chi2Null - chi2Mond));

        return new[]
        {
            new DiscriminationRow("AT  (g† ∝ H(z))", chi2At, aicAt, bicAt, bfAt),
            new DiscriminationRow("MOND (g† = constant)", chi2Mond, aicMond, bicMond, 1.0),
            new DiscriminationRow("NULL (g† = local)", chi2Null, aicNull, bicNull, bfNull),
        };
    }

    /// <summary>Analytic AT-vs-MOND separation signal-to-noise at key gas precisions:
    /// baseline (0.3 dex), perfect gas (0.0), and 2× / 5× improved (0.15 / 0.06 dex).
    /// Returns SNR² values (noncentrality of Δχ²).</summary>
    public static (double snr2At03, double snr2At0, double snr2GasHalf, double snr2GasFifth)
        RequiredPrecision(GalaxyGasSystematics[] gals)
    {
        return (
            MonteCarloRARAnalyzer.Snr2(gals, 0.30),
            MonteCarloRARAnalyzer.Snr2(gals, 0.00),
            MonteCarloRARAnalyzer.Snr2(gals, 0.15),
            MonteCarloRARAnalyzer.Snr2(gals, 0.06));
    }
}
