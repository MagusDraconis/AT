namespace AT.Core.Research;

/// <summary>
/// Maps systems from multiple domains into (R,S) reality space.
/// AT-X016: Reality Classification Theory
/// </summary>
public static class RealityClassifier
{
    private static string Region(double r, double s) => (r, s) switch
    {
        (>= 0.7, >= 0.7) => "QUANTUM REALITY (Rev∩SC)",
        (>= 0.7, < 0.7)  => "DYNAMIC REALITY (Rev only)",
        (< 0.7, >= 0.7)  => "CARRIER REALITY (SC only)",
        (>= 0.3, >= 0.3) => "WEAK REALITY",
        _                 => "NOISE ZONE"
    };

    public static List<RealityCoordinates.SystemPlacement> MapAll()
    {
        return new List<RealityCoordinates.SystemPlacement>
        {
            // ── QUANTUM DOMAIN ──
            new("Schrödinger Eigenstate",  "Quantum",   1.0, 1.0, true,  true,  Region(1.0,1.0), "Maximal Reality"),
            new("Harmonic Oscillator",     "Quantum",   1.0, 1.0, true,  true,  Region(1.0,1.0), "Maximal Reality"),
            new("Topological Edge State",  "Quantum",   1.0, 1.0, true,  true,  Region(1.0,1.0), "Maximal Reality"),
            new("Quantum Vortex",          "Quantum",   0.9, 0.9, true,  true,  Region(0.9,0.9), "Near-Maximal"),
            new("Bright Soliton (NLS)",    "Quantum",   0.9, 0.9, true,  true,  Region(0.9,0.9), "Near-Maximal"),
            new("Coherent Breather",       "Quantum",   0.8, 0.8, true,  false, Region(0.8,0.8), "Quantum — non-evolving"),

            // ── CLASSICAL DOMAIN ──
            new("Free Particle",           "Classical", 1.0, 0.0, false, false, Region(1.0,0.0), "Fluid — no structure"),
            new("Hamiltonian Chaos",       "Classical", 1.0, 0.0, false, false, Region(1.0,0.0), "Fluid — no structure"),
            new("Diffusion Eigenmode",     "Classical", 0.0, 0.8, true,  false, Region(0.0,0.8), "Temporary species"),
            new("Damped Oscillator",       "Classical", 0.0, 0.6, true,  false, Region(0.0,0.6), "Temporary species"),
            new("Kuramoto Sync",           "Classical", 0.0, 0.7, true,  false, Region(0.0,0.7), "Sync species"),
            new("Turbulence",              "Classical", 0.0, 0.0, false, false, Region(0.0,0.0), "Noise"),

            // ── BIOLOGICAL DOMAIN ──
            new("DNA Replicator",          "Biological",0.2, 0.9, true,  true,  Region(0.2,0.9), "Carrier — evolving"),
            new("Biological Species",      "Biological",0.3, 0.9, true,  true,  Region(0.3,0.9), "Carrier — evolving"),
            new("Ecosystem",               "Biological",0.3, 0.8, true,  true,  Region(0.3,0.8), "Carrier — evolving"),
            new("Evolutionary Population", "Biological",0.3, 0.7, true,  true,  Region(0.3,0.7), "Carrier — evolving"),
            new("Prion (misfolded protein)","Biological",0.0, 0.9, true,  false, Region(0.0,0.9), "SC only — no evolution"),

            // ── INFORMATION DOMAIN ──
            new("Cellular Automaton (Rule 110)","Info", 0.5, 0.5, false, false, Region(0.5,0.5), "Weak Reality"),
            new("Neural Network (trained)","Info",    0.1, 0.6, false, false, Region(0.1,0.6), "SC-dominant"),
            new("Error-Correcting Memory", "Info",    0.7, 0.8, false, false, Region(0.7,0.8), "Near-Quantum"),
            new("Communication Channel",   "Info",    0.5, 0.3, false, false, Region(0.5,0.3), "Weak Reality"),

            // ── COMPLEX SYSTEMS ──
            new("Financial Market",        "Complex", 0.1, 0.2, false, false, Region(0.1,0.2), "Near-Noise"),
            new("Social Network",          "Complex", 0.2, 0.5, false, false, Region(0.2,0.5), "Weak Reality"),
            new("Optimization (gradient)", "Complex", 0.0, 0.4, false, false, Region(0.0,0.4), "SC-dominant"),
            new("Thermal Noise",           "Complex", 0.0, 0.0, false, false, Region(0.0,0.0), "Noise"),
        };
    }
}
