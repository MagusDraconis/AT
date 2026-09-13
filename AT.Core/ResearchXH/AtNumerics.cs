namespace AT.Core.ResearchXH;

/// <summary>
/// Shared numerical helpers for the G-series audits.
///
/// These exist because the framework has no Math.ExpM1, and because several audits evaluate functions near
/// x = 0 where the naive forms lose every significant digit to cancellation. The audits that need them must
/// share one implementation — a second copy is a second chance to get the cancellation wrong.
/// </summary>
public static class AtNumerics
{
    /// <summary>
    /// e^x − 1 computed without cancellation for small x.
    ///
    /// <c>Math.Exp(x)</c> near 1 carries up to ~1 ulp of 1.0 (≈1.1e−16) of **absolute** error, so the naive
    /// <c>Math.Exp(x) - 1.0</c> has a relative error of roughly 1.1e−16 / x: negligible for x ≳ 1e−8, about
    /// 0.3 % at the Pound–Rebka compactness (x = 2.45e−15), and **total** — the subtraction returns exactly
    /// 0.0 — below x ≈ 1e−16, which is exactly where optical-lattice clocks sit (x = 1.1e−18). Any audit that
    /// evaluates a weak-field quantity through the naive form therefore reports "no signal" wherever the
    /// laboratory regime is most precisely probed. Uses the 5-term series below 1e−5.
    /// </summary>
    public static double ExpM1(double x)
        => Math.Abs(x) > 1.0e-5
            ? Math.Exp(x) - 1.0
            : x * (1.0 + x * (0.5 + x * (1.0 / 6.0 + x * (1.0 / 24.0 + x / 120.0))));
}
