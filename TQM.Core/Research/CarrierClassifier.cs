namespace TQM.Core.Research;

/// <summary>
/// Classifies all known and candidate information carrier types
/// across linear, nonlinear, and topological regimes.
///
/// TQM-X008: Information Carrier Taxonomy
/// </summary>
public static class CarrierClassifier
{
    public static List<CarrierTaxonomy.CarrierClass> BuildTaxonomy()
    {
        return new List<CarrierTaxonomy.CarrierClass>
        {
            // ── LINEAR REGIME ──
            new("Eigenmode (Uniform)", "Constant amplitude",
                CarrierTaxonomy.CarrierRegime.Linear,
                true, false, false, true, false, "None", 1),
            new("Eigenmode (Standing Wave)", "Sinusoidal, n nodes",
                CarrierTaxonomy.CarrierRegime.Linear,
                true, false, false, true, false, "Eigenmode (Uniform)", 3),
            new("Eigenmode (Anti-Phase)", "Domain wall",
                CarrierTaxonomy.CarrierRegime.Linear,
                true, false, false, true, false, "Eigenmode (Standing Wave)", 2),
            new("Composite Mode", "Linear superposition of ≤2 eigenmodes",
                CarrierTaxonomy.CarrierRegime.Linear,
                true, false, false, true, false, "Eigenmode (Standing Wave)", 4),

            // ── WEAKLY NONLINEAR ──
            new("Perturbed Eigenmode", "Quasi-sinusoidal with nonlinear shift",
                CarrierTaxonomy.CarrierRegime.WeaklyNonlinear,
                true, false, false, true, false, "Eigenmode (Standing Wave)", 2),
            new("Amplitude Breather", "Oscillating localized envelope",
                CarrierTaxonomy.CarrierRegime.WeaklyNonlinear,
                true, true, false, true, false, "Eigenmode (Standing Wave)", 3),

            // ── STRONGLY NONLINEAR ──
            new("Bright Soliton (N=1)", "Localized peak, sech profile",
                CarrierTaxonomy.CarrierRegime.StronglyNonlinear,
                true, true, false, true, true, "Perturbed Eigenmode", 5),
            new("Bright Soliton (N=2)", "Two-peak bound state",
                CarrierTaxonomy.CarrierRegime.StronglyNonlinear,
                true, true, false, true, true, "Bright Soliton (N=1)", 4),
            new("Dark Soliton", "Localized amplitude dip",
                CarrierTaxonomy.CarrierRegime.StronglyNonlinear,
                true, true, false, true, true, "Bright Soliton (N=1)", 4),
            new("Vector Soliton", "Two-component coupled",
                CarrierTaxonomy.CarrierRegime.StronglyNonlinear,
                true, true, false, true, true, "Bright Soliton (N=1)", 4),
            new("Breather Soliton", "Oscillating localized state",
                CarrierTaxonomy.CarrierRegime.StronglyNonlinear,
                true, true, false, true, false, "Bright Soliton (N=1)", 4),

            // ── TOPOLOGICAL ──
            new("Vortex", "Phase singularity, quantized circulation",
                CarrierTaxonomy.CarrierRegime.Topological,
                true, true, true, true, true, "Bright Soliton (N=1)", 5),
            new("Domain Wall", "Boundary between two phases",
                CarrierTaxonomy.CarrierRegime.Topological,
                true, true, true, true, false, "Dark Soliton", 4),
            new("Topological Edge State", "Boundary-localized mode",
                CarrierTaxonomy.CarrierRegime.Topological,
                true, true, true, true, false, "Eigenmode (Standing Wave)", 5),

            // ── HYBRID ──
            new("Soliton-Mode Hybrid", "Localized on top of extended mode",
                CarrierTaxonomy.CarrierRegime.Hybrid,
                true, true, false, true, true, "Bright Soliton (N=1)", 3),
            new("Localized Attractor", "Graph-localized persistent state",
                CarrierTaxonomy.CarrierRegime.Hybrid,
                true, true, false, true, false, "Eigenmode (Standing Wave)", 3),
        };
    }
}
