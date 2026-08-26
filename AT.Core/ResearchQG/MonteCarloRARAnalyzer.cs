using System.Globalization;

namespace AT.Core.ResearchQG;

/// <summary>
/// QG-076 Monte Carlo / error-budget core: builds per-galaxy gas-mass systematics
/// from the KMOS3D + COSMOS2015 inputs and computes the g† error budget and the
/// σ(log g†) vs σ(log Mgas) sensitivity curve.
///
/// Physics: g† = g_obs²/g_bar − g_bar  (from g_obs = g_bar·√(1+g†/g_bar)), so
///   d log g† / d log g_bar = −(1 + 2·g_bar/g†) ≡ −S.
/// A fractional gas-mass error δ log Mgas shifts log g_bar by the LOCAL gas fraction
/// f_gas, so σ(log g†) = S · f_gas · σ(log Mgas). The same S maps stellar-mass and
/// radius errors. All terms combine in quadrature.
/// </summary>
public static class MonteCarloRARAnalyzer
{
    /// <summary>Build per-galaxy systematics from parsed mass / rotation-curve / g† data.</summary>
    public static GalaxyGasSystematics[] BuildSystematics(
        Dictionary<string, (double z, double mStar, double sfr, double reKpc)> masses,
        Dictionary<string, (double[] radius, double[] gobs)> curves,
        Dictionary<string, (double logGdagger, bool constrained)> fits)
    {
        var result = new List<GalaxyGasSystematics>();
        foreach (var kv in masses)
        {
            string obj = kv.Key;
            var (z, mStar, sfr, reKpc) = kv.Value;
            if (!curves.TryGetValue(obj, out var rc)) continue;
            if (!fits.TryGetValue(obj, out var f)) continue;
            if (double.IsNaN(mStar) || mStar <= 0 || double.IsNaN(reKpc) || reKpc <= 0) continue;
            if (double.IsNaN(f.logGdagger) || double.IsInfinity(f.logGdagger)) continue;

            double gdagger = Math.Pow(10, f.logGdagger);

            // Stellar exponential disk + gas (depletion time), identical to QG-075.
            double rd = reKpc / 1.678;
            double rdGas = 1.5 * rd;
            double tDepYr = 1.5e9 / Math.Sqrt(1 + z);
            double mGas = double.IsNaN(sfr) ? 0 : Math.Max(sfr, 0) * tDepYr;

            // Transition probe = outermost valid rotation-curve point.
            double rOut = double.NaN, gobsOut = double.NaN;
            for (int i = rc.radius.Length - 1; i >= 0; i--)
                if (rc.radius[i] > 0 && rc.gobs[i] > 0) { rOut = rc.radius[i]; gobsOut = rc.gobs[i]; break; }
            if (double.IsNaN(rOut) || rOut <= 0) continue;

            double fStar = 1 - (1 + rOut / rd) * Math.Exp(-rOut / rd);
            double fGas = 1 - (1 + rOut / rdGas) * Math.Exp(-rOut / rdGas);
            double gbarOut = RARPhysics.G_ACC * (mStar * fStar + mGas * fGas) / (rOut * rOut);
            if (gbarOut <= 0) continue;

            double fGasLocal = mGas * fGas / (mStar * fStar + mGas * fGas);
            double s = Math.Abs(1 + 2 * gbarOut / gdagger);

            result.Add(new GalaxyGasSystematics(
                obj, z, mStar, mGas, reKpc, f.logGdagger, gbarOut, gobsOut,
                fGasLocal, s,
                s * (1 - fGasLocal) * RARPhysics.SigmaStellarDex,
                s * fGasLocal * 0.30,
                RARPhysics.SigmaInclDex,
                RARPhysics.SigmaRcDex,
                s * RARPhysics.SigmaRadiusDex,
                RARPhysics.SigmaIntrinsicDex,
                f.constrained));
        }
        return result.ToArray();
    }

    /// <summary>Total per-galaxy σ(log g†) for a given gas-mass uncertainty σ(log Mgas).</summary>
    public static double TotalSigma(GalaxyGasSystematics g, double sigmaGasDex)
    {
        double sGas = g.SFactor * g.FGasLocal * sigmaGasDex;
        double sStellar = g.SigmaStellar;
        double sRadius = g.SigmaRadius;
        double sIncl = g.SigmaIncl;
        double sRc = g.SigmaRc;
        double sInt = g.SigmaIntrinsic;
        return Math.Sqrt(sGas * sGas + sStellar * sStellar + sRadius * sRadius + sIncl * sIncl + sRc * sRc + sInt * sInt);
    }

    /// <summary>σ(log g†) vs σ(log Mgas): median/mean and 16–84 percentile spread over galaxies.</summary>
    public static GdaggerSensitivityPoint[] SensitivityCurve(GalaxyGasSystematics[] gals, double[] sigmaGasLevels)
    {
        var result = new List<GdaggerSensitivityPoint>();
        foreach (double sg in sigmaGasLevels)
        {
            var sigmas = gals.Select(g => TotalSigma(g, sg)).OrderBy(x => x).ToArray();
            if (sigmas.Length == 0) continue;
            double median = sigmas[sigmas.Length / 2];
            double mean = sigmas.Average();
            double p16 = sigmas[(int)Math.Floor(0.16 * (sigmas.Length - 1))];
            double p84 = sigmas[(int)Math.Ceiling(0.84 * (sigmas.Length - 1))];
            result.Add(new GdaggerSensitivityPoint(sg, median, mean, p16, p84));
        }
        return result.ToArray();
    }

    /// <summary>Analytic discrimination signal-to-noise² between AT (rising) and the
    /// best-fit constant (MOND) at a given gas precision: Σ (AT_pred − <AT_pred>w)²/σ_i².</summary>
    public static double Snr2(GalaxyGasSystematics[] gals, double sigmaGasDex)
    {
        if (gals.Length == 0) return 0;
        double sumW = 0, sw = 0;
        foreach (var g in gals)
        {
            double s = TotalSigma(g, sigmaGasDex);
            double wi = 1.0 / (s * s);
            sumW += wi;
            sw += wi * RARPhysics.LogGdaggerAt(g.Z);
        }
        double mean = sw / sumW;
        double snr2 = 0;
        foreach (var g in gals)
        {
            double s = TotalSigma(g, sigmaGasDex);
            double wi = 1.0 / (s * s);
            double d = RARPhysics.LogGdaggerAt(g.Z) - mean;
            snr2 += wi * d * d;
        }
        return snr2;
    }

    /// <summary>Per-galaxy error-budget CSV rows (at the baseline σ(log Mgas) = 0.30 dex).</summary>
    public static GasErrorBudgetRow[] ErrorBudgetRows(GalaxyGasSystematics[] gals, double sigmaGasDex = 0.30)
    {
        return gals.Select(g => new GasErrorBudgetRow(
            g.Object, g.Z, g.FGasLocal, g.SFactor,
            g.SigmaStellar, g.SFactor * g.FGasLocal * sigmaGasDex,
            g.SigmaIncl, g.SigmaRc, g.SigmaRadius, g.SigmaIntrinsic,
            TotalSigma(g, sigmaGasDex))).ToArray();
    }
}
