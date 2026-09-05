using System.Globalization;
using AT.Book.Domain;

namespace AT.Book.Services.Calculations;

/// <summary>
/// The D96 circulant spectrum: eigenvalue λ_k = Σ_{s=1..K} 2(1 − cos(2πks/N)), ω_k = √λ_k.
/// Executable — recomputes the spectrum from N and the coupling range K.
/// </summary>
public sealed class SpectrumService : ICalculationService
{
    public const int N = 96;
    public const int K = 6;

    public string Name => "Spectrum";

    public IReadOnlyList<CalculationResult> Results { get; }

    public SpectrumService()
    {
        var modes = Modes(N, K);
        double w1 = modes[0];
        double wMax = modes[^1];
        Results =
        [
            new(
                "spectrum",
                "D96 Spectrum (circulant C_96(±1..±6))",
                "λ_k = Σ_{s=1..6} 2(1 − cos(2πks/96)),  ω_k = √λ_k",
                [
                    new("N", N.ToString(CultureInfo.InvariantCulture), "ring size"),
                    new("K", K.ToString(CultureInfo.InvariantCulture), "nearest-neighbour coupling range"),
                    new("ω₁", w1.ToString("0.0000", CultureInfo.InvariantCulture), "fundamental mode (k=1)"),
                    new("ω_max", wMax.ToString("0.0000", CultureInfo.InvariantCulture), "band edge (k=48)"),
                    new("span", (wMax / w1).ToString("0.0000", CultureInfo.InvariantCulture), "ω_max / ω₁"),
                    new("# modes", modes.Length.ToString(CultureInfo.InvariantCulture), "N−1 positive modes"),
                ],
                "The D96 ring is a 1D structure: one integer mode index k, linear low-frequency dispersion ω_k ≈ (2π√91/N)·k."),
        ];
    }

    public static double LambdaK(int k, int n, int kMax = K)
    {
        double sum = 0;
        for (int s = 1; s <= kMax; s++)
            sum += 2 * (1 - Math.Cos(2 * Math.PI * k * s / n));
        return sum;
    }

    public static double OmegaK(int k, int n, int kMax = K) => Math.Sqrt(LambdaK(k, n, kMax));

    public static double[] Modes(int n, int kMax = K)
    {
        var w = new double[n - 1];
        for (int k = 1; k < n; k++) w[k - 1] = OmegaK(k, n, kMax);
        Array.Sort(w);
        return w;
    }
}
