namespace TQM.Core.Research;

/// <summary>
/// Compares finite-system architectures for complexity efficiency.
/// TQM-X029: Finite Complexity Optimization Principle
/// </summary>
public static class FiniteComplexityLandscape
{
    public static List<ComplexityEfficiencyMetrics.ArchitectureScore> CompareArchitectures(int N = 100)
    {
        return new List<ComplexityEfficiencyMetrics.ArchitectureScore>
        {
            // Pure Fourier: 1 carrier class, N species.
            new("Pure Fourier (Linear)",
                1, N, N * 1.0, N / (double)N, false),

            // Pure NLS: 6 carrier classes, species depend on nonlinearity α.
            new("Pure NLS (Solitons)",
                6, 6 * N, 6 * N / 6.0, (6 * N) / (6.0 * N), false),

            // Topological: protected, fewer species but infinite persistence.
            new("Topological Edge States",
                3, 3 * N / 2, 3 * N / 2.0 * 1.5, (3 * N / 2.0 * 1.5) / (6 * N), false),

            // Hybrid (Fourier + NLS): both carrier families simultaneously.
            new("Hybrid (Linear + NLS)",
                7, 7 * N, 7 * N / 5.0, (7 * N) / (6.0 * N), false),

            // Meta-operator tower: theoretically highest diversity.
            new("Meta-Operator Tower",
                10, 10 * N, 10 * N / 4.0, (10 * N) / (6.0 * N), false),

            // Quantum Reality (Rev∩SC): optimal for given state space.
            new("Quantum Reality (Rev∩SC)",
                7, 7 * N, 7 * N / 3.0, (7 * N / 3.0) / (7 * N / 3.0), true),

            // Fully mixed: all carrier classes simultaneously.
            new("Universal Hybrid (All Classes)",
                16, 16 * N, 16 * N / 2.0, (16 * N / 2.0) / (16 * N / 2.0), true),
        };
    }
}
