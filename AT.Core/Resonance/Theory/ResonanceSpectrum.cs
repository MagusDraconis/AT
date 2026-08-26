using System.Numerics;

namespace AT.Core.Resonance.Theory;

/// <summary>
/// Spectral analysis of coherent excitations in topological charge
/// condensates. Computes FFT spectra of local R and M fields, detects
/// resonance peaks, and measures quality factors.
///
/// AT-124: Coherent Field Excitations of Topological Charge
/// </summary>
public static class ResonanceSpectrum
{
    // ══════════════════════════════════════════════════════════════════
    // Compute power spectrum from time series.
    // ══════════════════════════════════════════════════════════════════

    public static CoherentModeProfile.ExcitationSpectrum ComputeSpectrum(
        double[] timeSeries, double dt, string label = "signal")
    {
        int n = timeSeries.Length;
        if (n < 4)
            return new CoherentModeProfile.ExcitationSpectrum(
                Array.Empty<double>(), Array.Empty<double>(),
                new List<CoherentModeProfile.ResonancePeak>(),
                0, 0, 0, "Noise");

        // Detrend: subtract mean.
        double mean = timeSeries.Average();
        var detrended = timeSeries.Select(x => x - mean).ToArray();

        // Next power of 2 for FFT.
        int nFFT = 1;
        while (nFFT < n) nFFT <<= 1;

        var complex = new Complex[nFFT];
        for (int i = 0; i < n; i++) complex[i] = new Complex(detrended[i], 0);
        for (int i = n; i < nFFT; i++) complex[i] = Complex.Zero;

        // Simple DFT (not FFT for simplicity — n is small enough).
        int nFreq = nFFT / 2 + 1;
        var power = new double[nFreq];
        var freqs = new double[nFreq];
        double T = n * dt; // total time
        double df = 1.0 / T;

        for (int k = 0; k < nFreq; k++)
        {
            freqs[k] = k * df;
            Complex sum = Complex.Zero;
            for (int j = 0; j < n; j++)
            {
                double angle = -2.0 * Math.PI * k * j / nFFT;
                sum += new Complex(
                    detrended[j] * Math.Cos(angle),
                    detrended[j] * Math.Sin(angle));
            }
            power[k] = sum.Magnitude * sum.Magnitude / (n * n);
        }

        // Estimate noise floor from high-frequency tail.
        int tailStart = Math.Max(nFreq * 3 / 5, nFreq / 2);
        double noiseFloor = 0;
        if (tailStart < nFreq)
            noiseFloor = power[tailStart..].Average();

        // Detect peaks.
        var peaks = DetectPeaks(freqs, power, noiseFloor);
        double totalPower = power.Sum();

        string spectrumType = peaks.Count(p => p.IsSignificant) >= 2
            ? "Discrete" : peaks.Any(p => p.IsSignificant) ? "Discrete"
            : totalPower > noiseFloor * 3 ? "Continuous" : "Noise";

        return new CoherentModeProfile.ExcitationSpectrum(
            freqs, power, peaks, noiseFloor, totalPower,
            peaks.Count(p => p.IsSignificant), spectrumType);
    }

    // ══════════════════════════════════════════════════════════════════
    // Detect resonance peaks above noise.
    // ══════════════════════════════════════════════════════════════════

    private static List<CoherentModeProfile.ResonancePeak> DetectPeaks(
        double[] freqs, double[] power, double noiseFloor)
    {
        var peaks = new List<CoherentModeProfile.ResonancePeak>();
        double threshold = Math.Max(noiseFloor * 3, power.Average() * 0.5);

        for (int k = 1; k < freqs.Length - 1; k++)
        {
            if (power[k] > power[k - 1] && power[k] > power[k + 1] && power[k] > threshold)
            {
                // Compute FWHM by finding half-power points.
                double halfPower = power[k] / 2.0;
                int left = k, right = k;
                while (left > 0 && power[left] > halfPower) left--;
                while (right < freqs.Length - 1 && power[right] > halfPower) right++;
                double fwhm = (right - left) * (freqs[1] - freqs[0]);

                double q = fwhm > 1e-15 ? freqs[k] / fwhm : 0;
                bool significant = power[k] > noiseFloor * 10 || q > 3;

                // Classify mode type.
                string modeType = "Unknown";
                if (significant && freqs[k] > 0.01 && q > 5) modeType = "Oscillation";
                else if (significant && power[k] > noiseFloor * 20) modeType = "Breathing";
                else if (significant) modeType = "Shape";

                // Harmonic order: ratio to fundamental.
                double fundamental = peaks.Count > 0 ? peaks[0].Frequency : freqs[k];
                int harmonic = (int)Math.Round(freqs[k] / Math.Max(fundamental, 1e-10));

                peaks.Add(new CoherentModeProfile.ResonancePeak(
                    freqs[k], power[k], fwhm, q, harmonic, significant, modeType));
            }
        }

        return peaks.OrderByDescending(p => p.Power).Take(10).ToList();
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute local R(t) at a specific grid point.
    // ══════════════════════════════════════════════════════════════════

    public static double[] ExtractTimeSeries(
        List<double[,]> RfieldHistory, int gx, int gy)
    {
        int n = RfieldHistory.Count;
        var series = new double[n];
        for (int t = 0; t < n; t++)
            series[t] = RfieldHistory[t][gx, gy];
        return series;
    }

    /// <summary>Extract mean R inside condensate over time.</summary>
    public static double[] ExtractMeanRTimeSeries(
        List<double[,]> RfieldHistory)
    {
        int n = RfieldHistory.Count;
        var series = new double[n];
        for (int t = 0; t < n; t++)
        {
            double sum = 0;
            int count = 0;
            int gs = RfieldHistory[t].GetLength(0);
            for (int gx = 0; gx < gs; gx++)
                for (int gy = 0; gy < gs; gy++)
                {
                    sum += RfieldHistory[t][gx, gy];
                    count++;
                }
            series[t] = count > 0 ? sum / count : 0;
        }
        return series;
    }

    /// <summary>Extract condensate width over time (std dev of R>0.5 region).</summary>
    public static double[] ExtractWidthTimeSeries(
        List<double[,]> RfieldHistory)
    {
        int n = RfieldHistory.Count;
        var series = new double[n];
        for (int t = 0; t < n; t++)
        {
            double cx = 0, cy = 0, totalR = 0;
            int gs = RfieldHistory[t].GetLength(0);
            double cellSize = 1.0 / gs;

            // Find center of mass of R>0.3 region.
            for (int gx = 0; gx < gs; gx++)
                for (int gy = 0; gy < gs; gy++)
                {
                    double r = RfieldHistory[t][gx, gy];
                    if (r > 0.3)
                    {
                        cx += (gx + 0.5) * cellSize * r;
                        cy += (gy + 0.5) * cellSize * r;
                        totalR += r;
                    }
                }
            if (totalR > 0) { cx /= totalR; cy /= totalR; }

            // Compute rms width.
            double sumSq = 0, sumWeight = 0;
            for (int gx = 0; gx < gs; gx++)
                for (int gy = 0; gy < gs; gy++)
                {
                    double r = RfieldHistory[t][gx, gy];
                    if (r > 0.3)
                    {
                        double dx = (gx + 0.5) * cellSize - cx;
                        double dy = (gy + 0.5) * cellSize - cy;
                        sumSq += (dx * dx + dy * dy) * r;
                        sumWeight += r;
                    }
                }
            series[t] = sumWeight > 0 ? Math.Sqrt(sumSq / sumWeight) : 0;
        }
        return series;
    }
}
