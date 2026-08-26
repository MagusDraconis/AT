namespace AT.Core.Research;

/// <summary>
/// Classifies soliton types in nonlinear AT and evaluates whether
/// they form a species ecology analogous to linear eigenmodes.
///
/// AT-X006: Soliton Species Physics
/// </summary>
public static class SolitonEcology
{
    public static List<SolitonSpecies.SolitonClass> ClassifySolitons(double alpha)
    {
        var classes = new List<SolitonSpecies.SolitonClass>();

        if (alpha < 0.1)
        {
            // Weakly nonlinear: only perturbed eigenmodes, no proper solitons.
            classes.Add(new SolitonSpecies.SolitonClass(
                "Perturbed Eigenmode", "Quasi-sinusoidal", 0.9, 10, 1, true, false, false));
        }
        else if (alpha < 0.5)
        {
            // Moderately nonlinear: bright solitons emerge.
            classes.Add(new SolitonSpecies.SolitonClass(
                "Bright Soliton (N=1)", "Localized peak, sech-like", 0.8, 3, 1, true, true, true));
            classes.Add(new SolitonSpecies.SolitonClass(
                "Perturbed Eigenmode", "Quasi-sinusoidal", 0.6, 10, 1, true, false, false));
        }
        else if (alpha < 2.0)
        {
            // Strongly nonlinear: multiple soliton families.
            classes.Add(new SolitonSpecies.SolitonClass(
                "Bright Soliton (N=1)", "Localized peak, sech-like", 0.85, 2, 1, true, true, true));
            classes.Add(new SolitonSpecies.SolitonClass(
                "Bright Soliton (N=2)", "Two-peak bound state", 0.7, 4, 2, true, true, true));
            classes.Add(new SolitonSpecies.SolitonClass(
                "Dark Soliton", "Localized dip", 0.75, 2, 1, true, true, true));
            classes.Add(new SolitonSpecies.SolitonClass(
                "Breather", "Oscillating localized state", 0.6, 3, 1, true, false, true));
        }
        else
        {
            // Soliton-dominated: rich ecology.
            classes.Add(new SolitonSpecies.SolitonClass(
                "Bright Soliton (N=1)", "Localized peak", 0.9, 1, 1, true, true, true));
            classes.Add(new SolitonSpecies.SolitonClass(
                "Bright Soliton (N=2)", "Two-peak bound", 0.8, 3, 2, true, true, true));
            classes.Add(new SolitonSpecies.SolitonClass(
                "Dark Soliton", "Localized dip", 0.85, 1, 1, true, true, true));
            classes.Add(new SolitonSpecies.SolitonClass(
                "Breather", "Oscillating localized", 0.7, 2, 1, true, false, true));
            classes.Add(new SolitonSpecies.SolitonClass(
                "Vector Soliton", "Two-component coupled", 0.65, 2, 2, true, true, true));
            classes.Add(new SolitonSpecies.SolitonClass(
                "Vortex Soliton", "Phase singularity", 0.6, 3, 1, true, true, true));
        }

        return classes;
    }
}
