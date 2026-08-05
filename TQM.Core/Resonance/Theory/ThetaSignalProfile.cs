namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for information transport analysis in the Θ field.
///
/// TQM-129: Information Transport in the Θ Field
/// </summary>
public static class ThetaSignalProfile
{
    public sealed record SignalRun(
        double K, double Lambda, int N, int Seed,
        double Density, double SourceX, double ReceiverX,
        string EncodingMethod,          // "PhasePulse", "Amplitude", "Frequency", "PulseTrain", "WavePacket"
        int BitsTransmitted,
        int BitsRecovered,
        double BitErrorRate,
        double SignalAmplitudeAtReceiver,
        double PropagationVelocity,     // Δx / Δt
        double AttenuationRate,         // dB per unit distance
        double MutualInformation,       // I(source; receiver) in bits
        double ChannelCapacity,         // max I over encodings
        double CoherenceLifetime,       // how long signal stays coherent
        bool InformationTransported);   // BER < 0.3?

    public sealed record TransmissionResult(
        double Distance,
        int BitsSent, int BitsRecovered,
        double BER,
        double Amplitude, double Velocity,
        double Attenuation,
        double SNR,
        double MutualInfo,
        string Quality);               // "Excellent", "Good", "Marginal", "Failed"

    public sealed record InformationChannel(
        string Name,
        double Bandwidth,               // max information rate
        double Capacity,                // bits per use
        double Range,                   // max distance with BER < 0.1
        double OptimalDensity,
        string EncodingRecommendation);

    public sealed record ThetaInformationReport(
        List<SignalRun> Runs,
        List<TransmissionResult> Transmissions,
        List<InformationChannel> Channels,
        bool InformationTransported,
        bool BinaryRecoveryPossible,
        double MaxRange,
        double BestChannelCapacity,
        double OptimalDensity,
        string Classification,
        string Verdict);
}
