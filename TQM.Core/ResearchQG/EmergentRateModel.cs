namespace TQM.Core.ResearchQG;

/// <summary>QG-098 emergent-rate model. The central circularity: N = (R_H/l_P)⁴ uses
/// R_H = c/H, so H is the INPUT that defines N — H is not derived from N. Λ ~ 1/√N is
/// therefore the relation Λ ~ H²/c² (the 'why now' coincidence), with H given.</summary>
public static class EmergentRateModel
{
    public const double C = 299792458.0;
    public const double PlanckLength = 1.616255e-35;
    public const double H0 = 67.4;
    public const double Lambda = 1.1e-52;
    public const double A0 = 1.2e-10;
    public const double Kpc_m = 3.0857e19;

    public static double H0PerS => H0 / Kpc_m;
    public static double CH => C * H0PerS;

    /// <summary>N from H: N = (c/(H·l_P))⁴. H is the INPUT.</summary>
    public static double NFromH(double h = H0) => Math.Pow(C / (h / Kpc_m) / PlanckLength, 4.0);

    /// <summary>Λ from N (causal set): Λ·l_P² = 1/√N = (H·l_P/c)².</summary>
    public static double LambdaFromN(double h = H0) => 1.0 / Math.Sqrt(NFromH(h)) / (PlanckLength * PlanckLength);

    /// <summary>a₀ from the rate: a₀ = c·H (order of magnitude).</summary>
    public static double A0FromRate(double h = H0) => C * (h / Kpc_m);

    /// <summary>Is H emergent from N? NO — N is defined by H (R_H = c/H).</summary>
    public static bool HEmergesFromN => false;

    public static string CircularityReason =>
        "N = (R_H/l_P)⁴ with R_H = c/H ⇒ H is the input that defines N; H is not derived from N";

    /// <summary>The single 'rate' R = H is the common input; Λ ~ H²/c² and a₀ ~ cH are projections.</summary>
    public static string CommonInput => "R = H (input); Λ ~ H²/c² and a₀ ~ cH are two projections of H";
}
