namespace TQM.Core.ResearchQG;

/// <summary>Per-galaxy RAR fit: g_obs = g_bar·sqrt(1 + g†/g_bar) with free g†.</summary>
public sealed record RARFit(
    string ObjectId,
    double Redshift,
    double Gdagger_m_s2,
    double Gdagger_err_m_s2,
    double LogGdagger,
    double LogGdagger_err,
    double Chi2,
    int Npoints);
