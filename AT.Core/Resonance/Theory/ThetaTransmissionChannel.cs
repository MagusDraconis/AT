namespace AT.Core.Resonance.Theory;

/// <summary>
/// Information-theoretic analysis of the Θ field as a transmission channel.
/// Computes mutual information, channel capacity, propagation metrics,
/// and signal quality.
///
/// AT-129: Information Transport in the Θ Field
/// </summary>
public static class ThetaTransmissionChannel
{
    // ══════════════════════════════════════════════════════════════════
    // Estimate mutual information from signal time series.
    // ══════════════════════════════════════════════════════════════════

    public static double EstimateMutualInformation(
        double[] sourceSignal, double[] receiverSignal, int nBins = 10)
    {
        if (sourceSignal.Length < 4 || receiverSignal.Length < 4) return 0;
        int n = Math.Min(sourceSignal.Length, receiverSignal.Length);

        // Simple correlation-based MI: I ≈ −½log(1−r²)
        double sx = 0, sy = 0, sxx = 0, syy = 0, sxy = 0;
        for (int i = 0; i < n; i++)
        {
            sx += sourceSignal[i]; sy += receiverSignal[i];
            sxx += sourceSignal[i] * sourceSignal[i];
            syy += receiverSignal[i] * receiverSignal[i];
            sxy += sourceSignal[i] * receiverSignal[i];
        }
        double r = (n * sxy - sx * sy) /
            Math.Sqrt((n * sxx - sx * sx) * (n * syy - sy * sy) + 1e-10);
        r = Math.Clamp(r, -0.999, 0.999);

        // Gaussian channel mutual information approximation.
        return -0.5 * Math.Log(1.0 - r * r);
    }

    // ══════════════════════════════════════════════════════════════════
    // Estimate channel capacity from BER and SNR.
    // ══════════════════════════════════════════════════════════════════

    public static double EstimateChannelCapacity(double ber, double snr, int bitsSent)
    {
        // Binary symmetric channel capacity: C = 1 − H(BER)
        double h = 0;
        if (ber > 0 && ber < 1)
            h = -ber * Math.Log2(ber) - (1 - ber) * Math.Log2(1 - ber);
        double capacity = 1.0 - h;

        // Scale by SNR.
        double snrCapacity = 0.5 * Math.Log2(1.0 + snr);
        return Math.Min(capacity, snrCapacity);
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute propagation velocity from arrival time.
    // ══════════════════════════════════════════════════════════════════

    public static double EstimatePropagationVelocity(
        double distance, double arrivalTime)
    {
        return arrivalTime > 1e-10 ? distance / arrivalTime : 0;
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute attenuation rate.
    // ══════════════════════════════════════════════════════════════════

    public static double EstimateAttenuation(
        double sourceAmplitude, double receiverAmplitude, double distance)
    {
        if (receiverAmplitude < 1e-10 || distance < 1e-10) return double.PositiveInfinity;
        return -20.0 * Math.Log10(receiverAmplitude / sourceAmplitude) / distance;
    }

    // ══════════════════════════════════════════════════════════════════
    // Estimate SNR.
    // ══════════════════════════════════════════════════════════════════

    public static double EstimateSNR(double[] signal, double[] background)
    {
        if (signal.Length == 0) return 0;
        double sigPower = signal.Average(s => s * s);
        double bgPower = background.Length > 0 ? background.Average(b => b * b) : 1e-6;
        return sigPower / Math.Max(bgPower, 1e-10);
    }

    // ══════════════════════════════════════════════════════════════════
    // Simple binary encoding/decoding via phase pulse.
    // ══════════════════════════════════════════════════════════════════

    public static (int recovered, double ber) EncodeDecodeBinary(
        int[] bits, double[] receiverSignal, double threshold = 0)
    {
        int recovered = 0;
        int n = Math.Min(bits.Length, receiverSignal.Length);
        for (int i = 0; i < n; i++)
        {
            int decoded = receiverSignal[i] > threshold ? 1 : 0;
            if (decoded == bits[i]) recovered++;
        }
        double ber = n > 0 ? 1.0 - (double)recovered / n : 1.0;
        return (recovered, ber);
    }

    // ══════════════════════════════════════════════════════════════════
    // Simulate a phase pulse propagating through Θ field.
    // ══════════════════════════════════════════════════════════════════

    public static ThetaSignalProfile.TransmissionResult SimulateTransmission(
        double distance, double density, double velocity, double damping,
        int bitsSent = 8, double sourceAmplitude = 1.0)
    {
        // Signal amplitude at receiver: A(d) = A₀·exp(−d/ξ)·exp(−γ·d/v)
        double xi = density * 2.0; // coherence length scales with density
        double amp = sourceAmplitude * Math.Exp(-distance / xi) *
                     Math.Exp(-damping * distance / Math.Max(velocity, 1e-10));
        double arrivalTime = distance / Math.Max(velocity, 1e-10);
        double attenuation = arrivalTime > 0
            ? -20.0 * Math.Log10(Math.Max(amp / sourceAmplitude, 1e-10)) / arrivalTime : 0;

        // SNR: higher density → better signal quality.
        double snr = amp * amp * density * 10.0;

        // BER from SNR for binary signaling.
        double ber = 0.5 * (1.0 - ErfApprox(Math.Sqrt(snr / 2.0)));
        ber = Math.Clamp(ber, 0, 0.5);

        int recovered = (int)(bitsSent * (1.0 - ber));
        double mi = amp > 0.1 ? Math.Log2(1.0 + snr) * 0.5 : 0;

        string quality = ber < 0.05 ? "Excellent"
                       : ber < 0.15 ? "Good"
                       : ber < 0.30 ? "Marginal" : "Failed";

        return new ThetaSignalProfile.TransmissionResult(
            distance, bitsSent, recovered, ber, amp, velocity,
            attenuation, snr, mi, quality);
    }

    // ══════════════════════════════════════════════════════════════════
    // Build information channel catalog.
    // ══════════════════════════════════════════════════════════════════

    public static List<ThetaSignalProfile.InformationChannel> BuildChannels(
        List<ThetaSignalProfile.TransmissionResult> transmissions,
        double density)
    {
        var channels = new List<ThetaSignalProfile.InformationChannel>();
        if (transmissions.Count == 0) return channels;

        double maxRange = transmissions
            .Where(t => t.Quality != "Failed")
            .Select(t => t.Distance).DefaultIfEmpty(0).Max();
        double bestCap = transmissions.Max(t => t.MutualInfo);

        channels.Add(new ThetaSignalProfile.InformationChannel(
            "Phase Pulse Channel",
            1.0, bestCap, maxRange, density,
            "Binary phase modulation (0→0, 1→π)"));

        channels.Add(new ThetaSignalProfile.InformationChannel(
            "Amplitude Channel",
            0.5, bestCap * 0.7, maxRange * 0.7, density,
            "Amplitude modulation — more susceptible to attenuation"));

        return channels;
    }

    /// <summary>Error function approximation.</summary>
    private static double ErfApprox(double x)
    {
        double t = 1.0 / (1.0 + 0.47047 * Math.Abs(x));
        double poly = t * (0.3480242 + t * (-0.0958798 + t * 0.7478556));
        double result = 1.0 - poly * Math.Exp(-x * x);
        return x >= 0 ? result : -result;
    }
}
