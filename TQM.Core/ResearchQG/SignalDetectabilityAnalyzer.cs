namespace TQM.Core.ResearchQG;

/// <summary>QG-094 signal detectability: builds the channel-by-channel signal amplitude vs
/// detector sensitivity, computing the signal-to-noise ratio for each candidate observable.
/// All Planck-suppressed channels have S/N ≪ 1 (unobservable); the CMB low-l cosmic variance
/// is O(1) but is not uniquely a discreteness signature (continuum cosmic variance is identical).</summary>
public static class SignalDetectabilityAnalyzer
{
    public static DiscretenessSignal[] Signals()
    {
        var list = new List<DiscretenessSignal>();

        void Add(string channel, string effect, double amp, double sens, bool special = false)
            => list.Add(new DiscretenessSignal(channel, effect, amp, sens, amp / Math.Max(sens, 1e-300), amp > sens || special));

        // Λ itself (dark energy) — already observed at O(1).
        Add("dark energy Λ", "ever-present Λ = 1/√N in Planck units", 1.0, 1.0);

        // Λ fluctuation (unobservable).
        Add("Λ fluctuation", "δΛ/Λ ~ 1/√N", CausalDiscretenessModel.LambdaFluctuation, 1e-4);

        // GW phase noise.
        double gwFreq = 100.0; // Hz (LIGO band)
        Add("GW phase noise", "δφ ~ l_P·f/c", CausalDiscretenessModel.PhaseNoise(gwFreq), 1e-22);

        // Photon energy-dependent delay / spectral broadening.
        Add("photon time-delay", "Δt ~ l_P/c · (E/Ep)^α", CausalDiscretenessModel.PropagationSuppression(), 1e-16);

        // CMB low-l anomaly (NOT Planck-suppressed, but not unique).
        Add("CMB low-l variance", "cosmic variance ~ 1/√l at l=2", CausalDiscretenessModel.CmbLowLAnomaly, 0.1, special: true);

        // LSS correlation excess (suppressed by 1/√N).
        Add("LSS correlation excess", "δξ/ξ ~ 1/√N", CausalDiscretenessModel.LambdaFluctuation, 1e-4);

        return list.ToArray();
    }

    /// <summary>Rank signals by signal-to-noise (descending).</summary>
    public static DiscretenessSignal[] Ranked() => Signals().OrderByDescending(s => s.SignalToNoise).ToArray();
}
