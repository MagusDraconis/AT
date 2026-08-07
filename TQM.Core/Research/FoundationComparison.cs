namespace TQM.Core.Research;

/// <summary>
/// Classifies systems across both reversibility and self-consistency axes.
/// TQM-X011: Reversibility vs Self-Consistency
/// </summary>
public static class FoundationComparison
{
    public static List<ReversibilityVsSelfConsistency.SystemClassification> Classify()
    {
        return new List<ReversibilityVsSelfConsistency.SystemClassification>
        {
            // ── BOTH reversible AND self-consistent ──
            new("Schrödinger eigenmodes", true, true,
                "BOTH: i∂ψ/∂t = L_Q ψ (reversible) + L·v=λv (self-consistent)"),
            new("Solitons (NLS, focusing)", true, true,
                "BOTH: NLS is Hamiltonian (reversible) + balance eq (self-consistent)"),
            new("Harmonic oscillator", true, true,
                "BOTH: energy conserved (reversible) + periodic orbits (self-consistent)"),

            // ── Reversible only (NOT self-consistent) ──
            new("Free particle (unbounded)", true, false,
                "REVERSIBLE ONLY: i∂ψ/∂t = -∇²ψ is unitary, but wavepacket disperses — no fixed point"),
            new("Hamiltonian chaos", true, false,
                "REVERSIBLE ONLY: energy conserved, but trajectories never settle to fixed points"),
            new("1D ring (all modes degenerate)", true, false,
                "REVERSIBLE ONLY: dynamics are unitary, but no preferred mode — no self-consistent structure"),

            // ── Self-consistent only (NOT reversible) ──
            new("Diffusion eigenmodes", false, true,
                "SC ONLY: ∂u/∂t = -L_Q u is dissipative (norm decays), but eigenmodes are F(x)=x up to scale"),
            new("Coupled oscillators with damping", false, true,
                "SC ONLY: attractor exists, but energy dissipates — not reversible"),
            new("Kuramoto sync state", false, true,
                "SC ONLY: phase-locked state is a fixed point, but dynamics are dissipative"),
            new("Pattern-forming systems (Turing)", false, true,
                "SC ONLY: Turing patterns are self-consistent, but the dynamics are dissipative"),

            // ── NEITHER ──
            new("Random noise", false, false,
                "NEITHER: no conservation, no fixed point"),
            new("Fully chaotic dissipative", false, false,
                "NEITHER: energy dissipates, trajectories never repeat"),
            new("Unstable transient", false, false,
                "NEITHER: decays without reaching any persistent state"),
        };
    }
}
