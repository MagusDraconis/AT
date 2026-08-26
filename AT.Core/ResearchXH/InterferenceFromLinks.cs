using System.Numerics;

namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 65 — can quantum interference emerge? QG63/64 showed links carry a U(1) phase θ. Here we test
/// whether interference phenomena are naturally recovered from link phases. Given the phase, a path accumulates
/// θ_path = Σ θ_links and carries the amplitude e^{iθ_path}; a loop holonomy is the gauge-invariant sum around a
/// closed loop (Aharonov–Bohm); the double-slit amplitude is e^{iθ1} + e^{iθ2} with probability
/// |e^{iθ1} + e^{iθ2}|² = 2 + 2cos(θ1−θ2) (the interference pattern); the Born rule P = |amplitude|² is the natural
/// probability. So interference is MATCH — naturally recovered from link phases (the phases being the new primitive
/// of QG62). No new primitives added here.
/// </summary>
public static class InterferenceFromLinks
{
    /// <summary>Path amplitude = e^{iθ} for an accumulated phase θ.</summary>
    public static Complex PathAmplitude(double phase) => Complex.Exp(new Complex(0.0, phase));

    /// <summary>Phase accumulation along a path = the sum of link phases.</summary>
    public static double PhaseAccumulation(params double[] phases) => phases.Sum();

    /// <summary>Loop holonomy = the sum of phases around a closed loop (gauge-invariant).</summary>
    public static double LoopHolonomy(params double[] phases) => phases.Sum();

    /// <summary>Double-slit probability = |e^{iθ1} + e^{iθ2}|² = 2 + 2cos(θ1 − θ2).</summary>
    public static double DoubleSlitProbability(double theta1, double theta2)
    {
        Complex a = PathAmplitude(theta1) + PathAmplitude(theta2);
        return (a * Complex.Conjugate(a)).Real;
    }

    /// <summary>Born rule: probability = |amplitude|².</summary>
    public static double BornRule(Complex amplitude) => (amplitude * Complex.Conjugate(amplitude)).Real;
}
