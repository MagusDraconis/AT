using System.Globalization;
using System.Numerics;
using AT.Book.Domain;

namespace AT.Book.Services.Calculations;

/// <summary>
/// Quantum-layer entanglement: the Bell pair (Schmidt rank 2, concurrence 1, CHSH 2√2)
/// vs the canonical D96 product (rank 1, CHSH 2) — the complete minimal extension.
/// </summary>
public sealed class QuantumService : ICalculationService
{
    public string Name => "Quantum";

    public IReadOnlyList<CalculationResult> Results { get; }

    public QuantumService()
    {
        // Bell pair |Φ+⟩ = (|00⟩+|11⟩)/√2.
        var bell = new Complex[2, 2]
        {
            { 1.0 / Math.Sqrt(2.0), 0.0 },
            { 0.0, 1.0 / Math.Sqrt(2.0) },
        };
        // Canonical D96 product (single-DOF phase): a product state, rank 1.
        var product = new Complex[2, 2]
        {
            { 0.5, 0.5 },
            { 0.5, 0.5 },
        };
        double chshBell = 2.0 * Math.Sqrt(2.0);

        Results =
        [
            new(
                "bell-state",
                "Bell Pair (|00⟩+|11⟩)/√2 — Joint State",
                "Schmidt rank = #singular values of c_{ij} > 0;  concurrence C = 2|det c|;  CHSH = 2√(1+C²)",
                [
                    new("Schmidt rank", SchmidtRank(bell).ToString(CultureInfo.InvariantCulture), "rank 2 ⇒ non-separable"),
                    new("concurrence C", Concurrence(bell).ToString("0.0000", CultureInfo.InvariantCulture), "maximally entangled"),
                    new("CHSH", chshBell.ToString("0.0000", CultureInfo.InvariantCulture), "> 2 ⇒ Bell violation"),
                ],
                "The joint state (one irreducible primitive) reproduces the observed Bell violation S = 2√2."),
            new(
                "d96-rank",
                "Canonical D96 Two-Sector Product",
                "ψA⊗ψB is an outer product ⇒ Schmidt rank 1, CHSH = 2",
                [
                    new("Schmidt rank", SchmidtRank(product).ToString(CultureInfo.InvariantCulture), "rank 1 ⇒ separable"),
                    new("concurrence C", Concurrence(product).ToString("0.0000", CultureInfo.InvariantCulture)),
                    new("CHSH", "2.0000", "no violation"),
                ],
                "Canonical D96 yields correlation only (NP_038) — the entangling gate is the second irreducible primitive."),
        ];
    }

    private static double[] SingularValues(Complex[,] c)
    {
        double m00 = c[0, 0].Magnitude * c[0, 0].Magnitude + c[1, 0].Magnitude * c[1, 0].Magnitude;
        double m11 = c[0, 1].Magnitude * c[0, 1].Magnitude + c[1, 1].Magnitude * c[1, 1].Magnitude;
        Complex m01 = Complex.Conjugate(c[0, 0]) * c[0, 1] + Complex.Conjugate(c[1, 0]) * c[1, 1];
        double tr = m00 + m11;
        double det = m00 * m11 - m01.Magnitude * m01.Magnitude;
        double disc = Math.Sqrt(Math.Max(0.0, tr * tr - 4.0 * det));
        double s0 = Math.Sqrt(Math.Max(0.0, (tr + disc) / 2.0));
        double s1 = Math.Sqrt(Math.Max(0.0, (tr - disc) / 2.0));
        return s0 >= s1 ? new[] { s0, s1 } : new[] { s1, s0 };
    }

    private static int SchmidtRank(Complex[,] c)
    {
        var s = SingularValues(c);
        int rank = 0;
        foreach (var v in s) if (v * v > 1e-12) rank++;
        return rank;
    }

    private static double Concurrence(Complex[,] c)
    {
        var det = c[0, 0] * c[1, 1] - c[0, 1] * c[1, 0];
        return 2.0 * det.Magnitude;
    }
}
