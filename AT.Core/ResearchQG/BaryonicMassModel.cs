namespace AT.Core.ResearchQG;

/// <summary>Baryonic mass model for one galaxy: stellar/gas mass from the
/// H-alpha -> SFR -> M* (main sequence) + Mgas (depletion time) chain, and the
/// resulting baryonic acceleration profile g_bar(r).</summary>
public sealed record BaryonicModel(
    string ObjectId,
    double HaLuminosity_erg_s,
    double SFR_MsunPerYr,
    double StellarMass_Msun,
    double GasMass_Msun,
    double TotalBaryonicMass_Msun,
    double[] Gbar_m_s2);
