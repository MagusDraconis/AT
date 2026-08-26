using System.Globalization;
using System.Text;

namespace AT.Core.ResearchQG;

/// <summary>
/// Hostile audit of the Flavor/Yukawa sector: is it still reducible? Works from the accepted
/// hierarchy Q + Random Actualization + (ℓ,τ,ħ) + M² → Oscillation → Phase → QM → Topological
/// Defects → Flavor Sector. Known facts: G exists; dim(G)=3 is SELECTED not derived; Y is the
/// overlap operator; architecture shapes are unresolved; Koide Q=2/3 = 45° balance is unexplained.
/// No new primitives allowed. Rejects anthropic/texture/numerology/hidden-params/45°-restatements.
/// </summary>
public static class FlavorReducibilityAnalyzer
{
    // Charged-lepton pole masses (MeV).
    public const double Me = 0.51099895;
    public const double Mmu = 105.6583755;
    public const double Mtau = 1776.86;

    public static double SqrtMe => Math.Sqrt(Me);
    public static double SqrtMmu => Math.Sqrt(Mmu);
    public static double SqrtMtau => Math.Sqrt(Mtau);

    /// <summary>Koide Q = (Σm)/(Σ√m)². Equals the participation ratio Σ p_i² with p_i = √m_i/Σ√m.</summary>
    public static double KoideQ()
    {
        double s = SqrtMe + SqrtMmu + SqrtMtau;
        return (Me + Mmu + Mtau) / (s * s);
    }

    /// <summary>Participation weights p_i = √m_i / Σ√m.</summary>
    public static (double pe, double pmu, double ptau) Participation()
    {
        double s = SqrtMe + SqrtMmu + SqrtMtau;
        return (SqrtMe / s, SqrtMmu / s, SqrtMtau / s);
    }

    /// <summary>The 45° angle θ such that Q = 1/(3cos²θ).</summary>
    public static double KoideAngleDeg()
    {
        double cos2 = 1.0 / (3.0 * KoideQ());
        return Math.Acos(Math.Sqrt(cos2)) * 180.0 / Math.PI;
    }

    /// <summary>Shannon entropy (nats) of the participation weights, and its ratio to log(3).</summary>
    public static (double entropy, double ratio) ShannonEntropy()
    {
        var (pe, pmu, ptau) = Participation();
        double s = 0;
        foreach (double p in new[] { pe, pmu, ptau })
            if (p > 0) s -= p * Math.Log(p);
        double smax = Math.Log(3.0);
        return (s, s / smax);
    }

    /// <summary>Q for the democratic (uniform) spectrum: Q = 3·(1/3)² = 1/3.</summary>
    public static double DemocraticQ() => 1.0 / 3.0;

    /// <summary>Q for the hierarchical (concentrated) limit: Q → 1.</summary>
    public static double HierarchicalQ() => 1.0;

    /// <summary>Midpoint of [democracy, hierarchy] = 2/3 — the 'balance'.</summary>
    public static double MidpointQ() => (DemocraticQ() + HierarchicalQ()) / 2.0;

    /// <summary>Distance of the observed Q from the four candidate origins (in the Q coordinate).</summary>
    public static (string Origin, double PredictedQ, double Distance)[] OriginTests()
    {
        return new[]
        {
            ("symmetry (S3 democratic)", DemocraticQ(), Math.Abs(KoideQ() - DemocraticQ())),
            ("symmetry (S3 full hierarchy)", HierarchicalQ(), Math.Abs(KoideQ() - HierarchicalQ())),
            ("attractor (no fixed point)", double.NaN, double.NaN), // no attractor predicts a Q value
            ("topology (S1+U1)", double.NaN, double.NaN),           // locates geometry, not value
            ("information (max entropy)", DemocraticQ(), Math.Abs(KoideQ() - DemocraticQ())),
            ("information (min entropy)", HierarchicalQ(), Math.Abs(KoideQ() - HierarchicalQ())),
        };
    }
}

/// <summary>Aggregate report.</summary>
public sealed record FlavorReducibilityReport(
    string SA, string SB, string SC, string SD, string SE,
    double KoideQ, double AngleDeg, double EntropyRatio, string OutDir);
