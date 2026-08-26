namespace AT.Core.ResearchQG;

/// <summary>
/// QG-083 experimental constraints on clock-family drifts (order-of-magnitude,
/// over redshift z=0→3 unless noted). Values are conservative upper bounds.
/// </summary>
public static class ClockDriftConstraints
{
    /// <summary>Drift bound ε_i = |γ_i/γ_A − 1| over z=0→3, relative to the atomic clock.</summary>
    public static (ClockFamily Family, double Epsilon, string Basis)[] Bounds() => new[]
    {
        (new ClockFamily("Atomic", "A", "electromagnetic transitions (α, m_e)", 0.0),
            0.0, "reference — defines z"),
        (new ClockFamily("Nuclear", "N", "strong-force transitions / decays", 1e-6),
            1e-6, "Δα/α ≲ 1e-6 (quasar many-multiplet); Oklo ≲1e-7"),
        (new ClockFamily("Gravitational", "G", "free-fall / orbital timescale (G, ρ)", 5e-3),
            5e-3, "ΔG/G ≲ 1e-2 (BBN+CMB+lunar ranging); ε_G ≈ ½ΔG/G"),
        (new ClockFamily("Orbital/Dynamical", "D", "Kepler orbits, pulsar binaries", 1e-3),
            1e-3, "double pulsar GR orbital-decay test (~0.05%)"),
        (new ClockFamily("Quantum", "Q", "quantum transition / phase evolution", 1e-6),
            1e-6, "indistinguishable from atomic at current precision"),
    };

    /// <summary>Probe-level constraints (for the report table).</summary>
    public static ClockDriftConstraint[] ProbeConstraints() => new[]
    {
        new ClockDriftConstraint("quasar absorption (Δα/α)", "Atomic ↔ Nuclear", "1e-6", "many-multiplet method"),
        new ClockDriftConstraint("Oklo natural reactor", "Atomic ↔ Nuclear", "1e-7", "neutron-capture cross-section"),
        new ClockDriftConstraint("BBN + CMB (ΔG/G)", "Atomic ↔ Gravitational", "1e-2", "He abundance + acoustic scale"),
        new ClockDriftConstraint("lunar laser ranging", "Atomic ↔ Gravitational", "1e-13 /yr", "Ġ/G local bound"),
        new ClockDriftConstraint("double pulsar PSR J0737−3039", "Atomic ↔ Orbital", "1e-3", "orbital decay vs GR"),
        new ClockDriftConstraint("cosmic chronometers", "Atomic ↔ Dynamical", "1e-2", "galaxy-age vs Δz"),
        new ClockDriftConstraint("CMB T(z)=T0(1+z)", "Atomic ↔ Thermal", "1e-2", "SZ + molecular absorption"),
    };
}
