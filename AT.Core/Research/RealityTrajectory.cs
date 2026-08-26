namespace AT.Core.Research;

/// <summary>
/// Defines reality trajectories for systems across domains.
/// AT-X017: Reality Flow Theory
/// </summary>
public static class RealityTrajectory
{
    public static List<RealityFlowMetrics.RealityTrajectory> MapTrajectories()
    {
        return new List<RealityFlowMetrics.RealityTrajectory>
        {
            // ── QUANTUM: static at maximum ──
            new("Schrödinger Eigenstate",  "Quantum", 1.0,1.0, 1.0,1.0,
                "Fixed (Max Reality)", true, "Eigenstate = eternal stationary point"),
            new("Topological Edge State",  "Quantum", 1.0,1.0, 1.0,1.0,
                "Fixed (Max Reality)", true, "Topologically protected — cannot move"),

            // ── NONLINEAR: oscillate near maximum ──
            new("Bright Soliton",          "Nonlinear", 0.9,0.9, 0.9,0.9,
                "Fixed (Near-Max)", true, "Soliton = balance fixed point"),

            // ── BIOLOGICAL: drift RIGHT (increasing S) ──
            new("DNA Replicator",          "Biological", 0.2,0.5, 0.2,0.9,
                "Rightward (S↑)", false, "Natural selection → increasing self-consistency"),
            new("Biological Species",      "Biological", 0.2,0.6, 0.3,0.9,
                "Rightward (S↑)", false, "Evolution → more complex, more consistent forms"),
            new("Ecosystem",               "Biological", 0.3,0.7, 0.3,0.9,
                "Rightward (S↑)", false, "Stable ecosystems increase structure over time"),

            // ── LEARNING: drift RIGHT (increasing S) ──
            new("Neural Network (untrained)","Learning", 0.1,0.2, 0.1,0.6,
                "Rightward (S↑)", false, "Training → better internal consistency"),
            new("Optimization Process",    "Learning",   0.0,0.1, 0.0,0.5,
                "Rightward (S↑)", false, "Optimization → convergence to solution"),

            // ── DECOHERENCE: drift DOWN-LEFT ──
            new("Quantum → Classical",     "Transition", 1.0,0.8, 0.3,0.5,
                "Downward-Left (R↓,S↓)", false, "Decoherence → loss of quantum properties"),
            new("Measurement Collapse",    "Transition", 1.0,0.5, 0.0,0.8,
                "Leftward (R↓,S↑)", false, "Collapse → irreversible (R=0), definite outcome (S high)"),

            // ── DEATH/DECAY: drift LEFT (S collapse) ──
            new("Organism Death",          "Biological", 0.2,0.9, 0.2,0.0,
                "Downward (S↓)", false, "Death → structure dissolves"),
            new("Memory Decay",            "Information",0.7,0.8, 0.5,0.2,
                "Downward-Left (R↓,S↓)", false, "Information degrades over time"),

            // ── CHAOS: random walk ──
            new("Hamiltonian Chaos",       "Classical",  1.0,0.0, 1.0,0.0,
                "Static (No S)", false, "Reversible but no self-consistency → no flow in S"),
            new("Thermal Noise",           "Classical",  0.0,0.0, 0.0,0.0,
                "Fixed (Noise)", false, "No structure → no flow"),
        };
    }
}
