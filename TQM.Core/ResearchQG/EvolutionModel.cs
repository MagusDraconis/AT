namespace TQM.Core.ResearchQG;

/// <summary>Redshift-binned population estimate of the acceleration scale g†.</summary>
public sealed record EvolutionBin(
    double Zmean,
    double Zmin,
    double Zmax,
    double Gdagger_mean_m_s2,
    double Gdagger_median_m_s2,
    double Gdagger_err_m_s2,
    int Ngalaxies,
    double TQMPrediction_m_s2);
