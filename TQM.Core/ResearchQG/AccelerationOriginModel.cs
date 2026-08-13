namespace TQM.Core.ResearchQG;

/// <summary>QG-086 origin hypotheses for the ~1e-10 m/s² acceleration scale.</summary>
public sealed record AccelerationOrigin(
    string Name,
    string Description,
    int ParameterCount,
    double Naturalness,
    double ExplanatoryPower,
    double PredictivePower,
    string EvolutionPrediction,
    double Score);

/// <summary>Static catalog of the origin hypotheses.</summary>
public static class AccelerationOriginModel
{
    public static AccelerationOrigin[] Origins() => new[]
    {
        new AccelerationOrigin("Cosmological (cH)", "a0 = c·H(z); 0 free params; set by Hubble rate",
            0, 0.9, 0.8, 0.9, "a0 EVOLVES ∝ H(z) (rising in the past)", 2.6),
        new AccelerationOrigin("Cosmological (c²√Λ)", "a0 = c²√Λ; 0 free params; set by Λ (constant)",
            0, 0.8, 0.8, 0.7, "a0 CONSTANT (Λ is constant; = MOND at z-evolution level)", 2.3),
        new AccelerationOrigin("Cosmological (c/t)", "a0 = c/t_universe; 0 free params; set by cosmic age",
            0, 0.7, 0.7, 0.6, "a0 EVOLVES ∝ 1/t (different slope from cH)", 2.0),
        new AccelerationOrigin("Information", "a0 from finite information-processing rate (Verlinde-type)",
            1, 0.6, 0.7, 0.5, "entropic signature; order-of-magnitude cH", 1.8),
        new AccelerationOrigin("Quantum (horizon fluct.)", "a_min ~ cH from de Sitter horizon fluctuations",
            0, 0.6, 0.5, 0.4, "gives cH (no 2π) → 5.4× too large", 1.5),
        new AccelerationOrigin("Emergent gravity (MOND)", "a0 is a new fundamental constant modifying gravity",
            1, 0.4, 0.5, 0.8, "a0 CONSTANT (no evolution)", 1.7),
        new AccelerationOrigin("Coincidence", "no physical meaning; a0 free parameter near cH",
            1, 0.5, 0.1, 0.0, "no prediction", 0.6),
    };
}
