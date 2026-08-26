namespace AT.Core.ResearchXH;

/// <summary>
/// Compact genus-g ≥ 2 hyperbolic surface graphs (boundary-free, negative Euler
/// characteristic χ = 2 − 2g), built as generalized Petersen graphs G(n,k) — cubic
/// (3-regular), high girth.
///   Desargues G(10,3): 20 vertices, girth 6, genus 2 (χ = −2).
///   Nauru     G(12,5): 24 vertices, girth 6, genus 4 (χ = −6).
/// The ScalarCurvature field carries the Euler characteristic χ (the signed topological
/// curvature invariant) for these abstract surface graphs.
/// </summary>
public static class CompactHyperbolicGraph
{
    public static GeometricGraph Desargues() => Build(10, 3, "Desargues G(10,3)", -2.0);
    public static GeometricGraph Nauru() => Build(12, 5, "Nauru G(12,5)", -6.0);

    private static GeometricGraph Build(int n, int k, string name, double eulerChar)
    {
        int N = 2 * n;
        var a = new double[N, N];
        for (int i = 0; i < n; i++)
        {
            SetSym(a, i, (i + 1) % n);           // outer cycle
            SetSym(a, n + i, n + ((i + k) % n)); // inner star
            SetSym(a, i, n + i);                 // spokes
        }
        return new GeometricGraph($"{name} (compact hyperbolic, χ={eulerChar:F0})", 2, eulerChar, a);
    }

    private static void SetSym(double[,] a, int i, int j)
    {
        a[i, j] = 1.0;
        a[j, i] = 1.0;
    }
}
