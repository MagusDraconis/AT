namespace AT.Core.ResearchQG;

/// <summary>Rotation-curve data for one galaxy: radii, deprojected velocities,
/// and the observed centripetal acceleration g_obs = V_rot²/r.</summary>
public sealed record RotationCurveData(
    string ObjectId,
    double Redshift,
    double[] Radius_kpc,
    double[] Vrot_kms,
    double[] Vrot_err_kms,
    double[] Gobs_m_s2,
    double[] Gobs_err_m_s2);
