namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for autonomous collective wave field analysis.
/// Closure tests, field predictions, and the particle-field
/// phase diagram.
///
/// AT-128: Autonomous Collective Wave Field
/// </summary>
public static class ThetaFieldProfile
{
    public sealed record ThetaFieldRun(
        double K, double Lambda, int N, int Seed,
        int TargetQ, double ChargeDensity,
        double R_Q, double CoherenceLength,
        double FieldPredictionError,    // rms error of Θ-only prediction
        double ParticlePredictionError, // rms error of particle-based prediction
        bool FieldIsAutonomous,         // field prediction error < particle error?
        double EffectiveWaveVelocity,
        double EffectiveDamping,
        string EffectiveEquation,       // best-fit field equation type
        string Regime);                 // "Particle", "Mixed", "Field"

    public sealed record FieldPrediction(
        string ModelType,               // "Wave", "Diffusion", "ReactionWave", "KuramotoContinuum"
        double[] PredictedTheta,        // Θ(x, t+Δt) from field-only model
        double[] ActualTheta,           // Θ(x, t+Δt) from full simulation
        double RMSError,
        double R2Score,
        int NumParameters,
        bool IsAccurate);               // R² > 0.7

    public sealed record ClosureTest(
        double Density,
        double FieldRMSError,
        double ParticleRMSError,
        double ClosureRatio,            // field_error / particle_error (< 1 = field better)
        bool FieldOutperforms,
        string BestModel,
        double InformationRetention);   // % of variance explained by field model

    public sealed record ParticleFieldPhaseDiagram(
        double[] DensityAxis,
        double[] CouplingAxis,
        double[,] ClosureRatioGrid,
        string[,] RegimeGrid,
        string Description);

    public sealed record CollectiveFieldReport(
        List<ThetaFieldRun> Runs,
        List<FieldPrediction> Predictions,
        List<ClosureTest> ClosureTests,
        ParticleFieldPhaseDiagram PhaseDiagram,
        bool FieldAutonomyFound,
        bool FieldEquationFound,
        string BestFieldEquation,
        double CriticalDensityForAutonomy,
        string Classification,
        string Verdict);
}
