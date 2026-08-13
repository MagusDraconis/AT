namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-076 synthetic-truth recovery audit. Injects a known g†(z) truth (TQM rising,
/// or MOND constant), adds per-galaxy Gaussian noise from the full error budget
/// (whose gas term scales with σ(log Mgas)), and measures how often the TQM-vs-MOND
/// χ² comparison recovers the truth / produces a false TQM detection. Deterministic
/// (fixed seed). 10,000 realizations per gas precision by default.
/// </summary>
public static class SignalRecoveryAnalyzer
{
    public static RecoveryPoint[] Run(GalaxyGasSystematics[] gals, double[] sigmaGasLevels,
        int nReal = 10000, int seed = 42)
    {
        var result = new List<RecoveryPoint>();
        int n = gals.Length;
        if (n == 0) return result.ToArray();

        // Model predictions per galaxy (independent of gas precision).
        var tqmPred = new double[n];
        var constPred = new double[n];
        for (int i = 0; i < n; i++)
        {
            tqmPred[i] = RARPhysics.LogGdaggerTqm(gals[i].Z);
            constPred[i] = Math.Log10(RARPhysics.GdaggerLocal());
        }

        foreach (double sg in sigmaGasLevels)
        {
            var sigma = new double[n];
            var w = new double[n];
            double sumW = 0;
            for (int i = 0; i < n; i++)
            {
                sigma[i] = MonteCarloRARAnalyzer.TotalSigma(gals[i], sg);
                w[i] = 1.0 / (sigma[i] * sigma[i]);
                sumW += w[i];
            }

            // Best-fit constant (MOND) under TQM truth = weighted mean of TQM predictions.
            double mondBestTqm = 0;
            for (int i = 0; i < n; i++) mondBestTqm += w[i] * tqmPred[i];
            mondBestTqm /= sumW;

            // Analytic noncentrality: expected Δχ² = χ²_MOND − χ²_TQM under TQM truth.
            double snr2 = 0;
            for (int i = 0; i < n; i++)
                snr2 += w[i] * (tqmPred[i] - mondBestTqm) * (tqmPred[i] - mondBestTqm);

            var rng = new Random(seed + (int)Math.Round(sg * 1000));
            int recover = 0, falsePos = 0;
            double meanDchi2Tqm = 0;
            var obs = new double[n];

            for (int r = 0; r < nReal; r++)
            {
                // ---- Truth = TQM ----
                double sw = 0, swm = 0, chi2Tqm = 0;
                for (int i = 0; i < n; i++)
                {
                    obs[i] = tqmPred[i] + Gaussian(rng) * sigma[i];
                    double d = (obs[i] - tqmPred[i]) / sigma[i];
                    chi2Tqm += d * d;
                    sw += w[i];
                    swm += w[i] * obs[i];
                }
                double mondBest = swm / sw;
                double chi2Mond = 0;
                for (int i = 0; i < n; i++)
                {
                    double d = (obs[i] - mondBest) / sigma[i];
                    chi2Mond += d * d;
                }
                double dchi2 = chi2Mond - chi2Tqm;
                meanDchi2Tqm += dchi2;
                if (dchi2 > 4) recover++;

                // ---- Truth = MOND (constant = local g†) ----
                double sw2 = 0, swm2 = 0, chi2TqmMond = 0;
                for (int i = 0; i < n; i++)
                {
                    obs[i] = constPred[i] + Gaussian(rng) * sigma[i];
                    double d = (obs[i] - tqmPred[i]) / sigma[i];
                    chi2TqmMond += d * d;
                    sw2 += w[i];
                    swm2 += w[i] * obs[i];
                }
                double mondBest2 = swm2 / sw2;
                double chi2MondMond = 0;
                for (int i = 0; i < n; i++)
                {
                    double d = (obs[i] - mondBest2) / sigma[i];
                    chi2MondMond += d * d;
                }
                double dchi2Mond = chi2MondMond - chi2TqmMond;
                if (dchi2Mond > 4) falsePos++;  // TQM falsely preferred under MOND truth
            }

            result.Add(new RecoveryPoint(sg, recover / (double)nReal, falsePos / (double)nReal,
                meanDchi2Tqm / nReal, snr2));
        }
        return result.ToArray();
    }

    private static double Gaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
    }
}
