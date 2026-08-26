namespace AT.Core.ResearchQG;

/// <summary>QG-093 causal-set cosmological constant model: Λ ~ 1/√N with N = (R_H/l_P)⁴.
/// Also Sorkin's ever-present Λ: Λ = α/√N · 1/l_P² where α ~ O(1) is the dimensionless
/// amplitude (the 'tuning' parameter that must be O(1) for the prediction to be genuine).</summary>
public static class CausalSetLambdaModel
{
    public const double PlanckLength = 1.616255e-35;  // m
    public const double H0 = 67.4, OmM = 0.315, OmL = 0.685;
    public const double C = 299792458.0;
    public const double LambdaObs = 1.1e-52;          // m^-2

    public static double HubbleLength(double h0 = H0) => C / (h0 / 3.0857e19);

    public static double N(double h0 = H0)
        => Math.Pow(HubbleLength(h0) / PlanckLength, 4.0);

    /// <summary>Predicted Λ in Planck units for a given exponent: Λ·l_P² = N^exponent.</summary>
    public static double LambdaPlanck(double exponent = -0.5, double h0 = H0)
        => Math.Pow(N(h0), exponent);

    /// <summary>Predicted Λ in m^-2.</summary>
    public static double LambdaM2(double exponent = -0.5, double h0 = H0)
        => LambdaPlanck(exponent, h0) / (PlanckLength * PlanckLength);

    /// <summary>Observed Λ in Planck units.</summary>
    public static double ObservedLambdaPlanck()
        => LambdaObs * PlanckLength * PlanckLength;

    /// <summary>The dimensionless amplitude α such that Λ = α/√N · 1/l_P². α = O(1) is the
    /// no-tuning criterion.</summary>
    public static double AmplitudeAlpha(double h0 = H0)
        => ObservedLambdaPlanck() / LambdaPlanck(-0.5, h0);

    /// <summary>Propagated uncertainty in log Λ from δH0/H0 = 1% (numerical derivative).</summary>
    public static double DLogLambda_DLogH(double exponent = -0.5, double h0 = H0)
    {
        double dh = 0.01 * h0;
        double l1 = Math.Log(LambdaPlanck(exponent, h0 - dh));
        double l2 = Math.Log(LambdaPlanck(exponent, h0 + dh));
        return (l2 - l1) / (2.0 * 0.01);
    }
}
