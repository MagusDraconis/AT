namespace AT.Core.ResearchQG;

/// <summary>QG-087 event cosmology: the candidate event-growth models and their
/// effective Hubble rate H_event(z), with N(t0)=1 normalization.</summary>
public static class EventCosmology
{
    static double t0 => EventCosmologyConstants.T0Seconds;

    public static EventGrowthModel[] Models() => new[]
    {
        // N = a (FLRW): H_event = H_ΛCDM exactly (trivial identification).
        new EventGrowthModel("N = a (FLRW)", "a(t)", z => EventCosmologyConstants.HLambda(z)),

        // N ∝ t (linear / coasting): 1+z = t0/t → t = t0/(1+z); H = 1/t = (1+z)/t0.
        new EventGrowthModel("N ∝ t (linear)", "t/t0", z => (1 + z) / t0),

        // N ∝ t^n (power): 1+z = (t0/t)^n → H = n/t = n(1+z)^{1/n}/t0.
        new EventGrowthModel("N ∝ t^n (n=2)", "(t/t0)^2", z => 2.0 * Math.Pow(1 + z, 0.5) / t0),

        // N ∝ e^{λt} (exponential/de Sitter): H = λ = H0 constant.
        new EventGrowthModel("N ∝ e^{λt} (exponential)", "e^{H0(t−t0)}", _ => EventCosmologyConstants.H0PerS),

        // N ∝ ln t (information saturation): 1+z = ln(t0/tref)/ln(t/tref).
        new EventGrowthModel("N ∝ ln t (saturation)", "ln(t/tref)/ln(t0/tref)", z => SatHEvent(z)),
    };

    static double SatHEvent(double z)
    {
        // Normalize so H_event(0) = H0: ln(t0/tref) = 1/(H0·t0).
        double t0 = EventCosmologyConstants.T0Seconds;
        double H0 = EventCosmologyConstants.H0PerS;
        double lnT0 = 1.0 / (H0 * t0);
        double tref = t0 / Math.Exp(lnT0);
        double t = tref * Math.Exp(lnT0 / (1 + z));
        return 1.0 / (t * Math.Log(t / tref));
    }
}
