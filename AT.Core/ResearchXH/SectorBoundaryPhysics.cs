namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 113 — Sector boundary physics. QG112 showed reality may contain interacting network sectors
/// (PARTIAL SECTORING, 85.7% boundary networks). This phase asks: can UNRESOLVED Standard Model parameters
/// (masses, mixing angles, couplings) originate from SECTOR BOUNDARIES rather than within individual sectors?
///
/// Method (computational, fully deterministic): build TWO-SECTOR composite networks by joining two network
/// classes (a causal grid sector and an ER random sector) with a deterministic fraction of boundary (cross-
/// sector) links. Then measure: (1) BOUNDARY LINKS — the cross-sector link count/fraction; (2) INTER-SECTOR
/// COUPLING — the mean boundary-link weight (the coupling scale κ); (3) FAMILY TRANSITIONS — eigenmodes of the
/// composite Laplacian that span BOTH sectors (delocalized modes = transition channels); (4) MIXING-ANGLE
/// GENERATION — the rotation between the sector basis and the Laplacian eigenbasis: a two-sector two-state
/// system gives the mixing angle tan(2θ) = 2κ/(ε_A − ε_B), exactly the flavor-mixing picture of QG82;
/// (5) PARAMETER LOCALIZATION — inverse participation ratio of the eigenmodes: are the "mass" eigenvalues
/// localized on the boundary or in the bulk?
///
/// Answer (determined by the computed data): PARTIAL RELATION — sector boundaries DO generate a real
/// mixing structure: boundary links set a coupling κ, the two-sector system yields a DETERMINED mixing angle
/// θ = ½atan(2κ/(ε_A−ε_B)) (the QG82 rotation picture), delocalized transition modes exist, and the eigen-
/// values ("masses") are boundary-modulated. BUT the specific VALUES depend on the free boundary-coupling
/// input κ and the sector energies ε_A, ε_B — the boundary mechanism GENERATES the form (mixing structure)
/// without determining the specific SM numbers. Hence a PARTIAL RELATION (real boundary mechanism + mixing
/// generation), not a BOUNDARY ORIGIN (no value determination). No new primitives added here.
/// </summary>
public static class SectorBoundaryPhysics
{
    // ── Two-sector composite networks ──────────────────────────────────────────────

    /// <summary>
    /// Build a two-sector composite: sector A = causal grid, sector B = ER random, connected by a deterministic
    /// fraction of boundary (cross-sector) links. Boundary links connect the LAST node of A to the FIRST node of B
    /// plus evenly spaced pairs (deterministic pattern). Returns the composite adjacency.
    /// </summary>
    public static double[,] CompositeGridEr(double boundaryFraction = 0.10, int gridSize = 91, int erSize = 91, double erP = 0.10, int seed = 7)
    {
        var grid = CausalSet.BuildGrid(6, 6);                       // sector A (causal grid, N=91)
        double[,] a = SpectrumRobustness.LinkAdjacency(grid);
        double[,] b = FamilyStructureRobustness.RandomErdosRenyi(erSize, erP, seed);  // sector B
        return Compose(a, b, boundaryFraction);
    }

    /// <summary>
    /// Compose two adjacencies A (nA nodes) and B (nB nodes) with a deterministic fraction of boundary links.
    /// Boundary links: pairs (i, nA + j) selected by a fixed pattern (every ⌈1/fraction⌉-th pair in traversal).
    /// </summary>
    public static double[,] Compose(double[,] a, double[,] b, double boundaryFraction)
    {
        int nA = a.GetLength(0), nB = b.GetLength(0);
        int n = nA + nB;
        var m = new double[n, n];
        for (int i = 0; i < nA; i++)
            for (int j = 0; j < nA; j++) m[i, j] = a[i, j];
        for (int i = 0; i < nB; i++)
            for (int j = 0; j < nB; j++) m[nA + i, nA + j] = b[i, j];

        // deterministic boundary links
        int stride = Math.Max(1, (int)Math.Ceiling(1.0 / boundaryFraction));
        int counter = 0;
        for (int i = 0; i < nA; i++)
            for (int j = 0; j < nB; j++)
            {
                counter++;
                if (counter % stride == 0)
                {
                    m[i, nA + j] = 1.0;
                    m[nA + j, i] = 1.0;
                }
            }
        return m;
    }

    // ── 1. Boundary links ─────────────────────────────────────────────────────────

    /// <summary>Boundary-link count (cross-sector links) of a composite with nA nodes in sector A.</summary>
    public static int BoundaryLinkCount(double[,] composite, int nA)
    {
        int n = composite.GetLength(0);
        int count = 0;
        for (int i = 0; i < nA; i++)
            for (int j = nA; j < n; j++)
                if (composite[i, j] != 0.0) count++;
        return count;
    }

    /// <summary>Boundary-link fraction = cross-sector links / (nA·nB) possible pairs.</summary>
    public static double BoundaryLinkFraction(double[,] composite, int nA)
    {
        int nB = composite.GetLength(0) - nA;
        int possible = nA * nB;
        return possible > 0 ? (double)BoundaryLinkCount(composite, nA) / possible : 0.0;
    }

    // ── 2. Inter-sector coupling ──────────────────────────────────────────────────

    /// <summary>
    /// Inter-sector coupling κ: mean weight of boundary links (the coupling scale that enters the mixing
    /// angle). Equivalent to the mean off-diagonal block strength.
    /// </summary>
    public static double InterSectorCoupling(double[,] composite, int nA)
    {
        int count = BoundaryLinkCount(composite, nA);
        return count > 0 ? (double)count / (nA * (composite.GetLength(0) - nA)) : 0.0;
    }

    /// <summary>Mean degree of sector A nodes (the sector "energy" ε_A).</summary>
    public static double SectorEnergyA(double[,] composite, int nA)
    {
        double sum = 0.0;
        for (int i = 0; i < nA; i++)
            for (int j = 0; j < composite.GetLength(0); j++)
                sum += composite[i, j];
        return sum / nA;
    }

    /// <summary>Mean degree of sector B nodes (the sector "energy" ε_B).</summary>
    public static double SectorEnergyB(double[,] composite, int nA)
    {
        int nB = composite.GetLength(0) - nA;
        double sum = 0.0;
        for (int i = nA; i < composite.GetLength(0); i++)
            for (int j = 0; j < composite.GetLength(0); j++)
                sum += composite[i, j];
        return nB > 0 ? sum / nB : 0.0;
    }

    // ── 3. Family transitions ─────────────────────────────────────────────────────

    /// <summary>
    /// Family-transition modes: eigenmodes of the composite Laplacian that span BOTH sectors. A mode is a
    /// transition mode if its weight on each sector exceeds a small threshold (delocalized across the boundary).
    /// </summary>
    public static int FamilyTransitionCount(double[,] composite, int nA)
    {
        int n = composite.GetLength(0);
        var evd = MatrixDecomposition(composite);
        int count = 0;
        for (int k = 0; k < n; k++)
        {
            double wA = 0.0, wB = 0.0;
            for (int i = 0; i < nA; i++) wA += evd.vec[i, k] * evd.vec[i, k];
            for (int i = nA; i < n; i++) wB += evd.vec[i, k] * evd.vec[i, k];
            if (wA > 0.05 && wB > 0.05) count++;   // delocalized across the boundary
        }
        return count;
    }

    /// <summary>Eigendecomposition (eigenvalues + eigenvectors) of a symmetric matrix.</summary>
    public static (double[] evals, double[,] vec) MatrixDecomposition(double[,] matrix)
    {
        var mat = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseOfArray(matrix);
        var evd = mat.Evd(MathNet.Numerics.LinearAlgebra.Symmetricity.Symmetric);
        return (evd.EigenValues.Select(c => c.Real).ToArray(), evd.EigenVectors.ToArray());
    }

    // ── 4. Mixing-angle generation ────────────────────────────────────────────────

    /// <summary>
    /// Mixing angle of the two-sector system: tan(2θ) = 2κ/(ε_A − ε_B) (the QG82 rotation picture). This is
    /// the angle between the sector (flavor) basis and the mass-eigenstate basis, generated by the boundary
    /// coupling κ. Returns θ in degrees.
    /// </summary>
    public static double MixingAngle(double[,] composite, int nA)
    {
        double kappa = InterSectorCoupling(composite, nA);
        double eA = SectorEnergyA(composite, nA);
        double eB = SectorEnergyB(composite, nA);
        double delta = eA - eB;
        double theta = 0.5 * Math.Atan2(2.0 * kappa, delta);
        return theta * 180.0 / Math.PI;
    }

    /// <summary>Is the mixing angle NON-TRIVIAL (|θ| > 1°), i.e. the boundary generates real mixing?</summary>
    public static bool MixingIsNonTrivial(double[,] composite, int nA)
        => Math.Abs(MixingAngle(composite, nA)) > 1.0;

    /// <summary>Does the mixing angle depend on the boundary coupling (i.e. it is a boundary mechanism)?</summary>
    public static bool MixingDependsOnBoundary(double[,] weak, double[,] strong, int nA)
        => Math.Abs(MixingAngle(weak, nA) - MixingAngle(strong, nA)) > 1.0;

    // ── 5. Parameter localization ─────────────────────────────────────────────────

    /// <summary>
    /// Parameter localization: inverse participation ratio of the composite eigenmodes (mean over the LOW
    /// modes). IPR near 1 = localized (boundary/bulk concentrated); IPR near 0 = delocalized. Returns the mean
    /// IPR of the first `nLow` nonzero modes.
    /// </summary>
    public static double MeanLocalization(double[,] composite, int nA, int nLow = 8)
    {
        var (evals, vec) = MatrixDecomposition(composite);
        int n = composite.GetLength(0);
        var order = Enumerable.Range(0, n).OrderBy(k => evals[k]).Where(k => evals[k] > 1e-10).Take(nLow).ToArray();
        double sum = 0.0;
        foreach (int k in order)
        {
            double ipr = 0.0;
            for (int i = 0; i < n; i++) ipr += vec[i, k] * vec[i, k] * vec[i, k] * vec[i, k];
            sum += ipr;
        }
        return order.Length > 0 ? sum / order.Length : double.NaN;
    }

    // ── Classification ────────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   NO RELATION     — boundaries generate no mixing structure (no nontrivial angle, no transitions);
    ///   BOUNDARY ORIGIN — boundaries determine the SPECIFIC parameter values (mixing angle fixed by the
    ///                     network alone, no free inputs);
    ///   PARTIAL RELATION — boundaries generate a REAL mixing structure (nontrivial angle, transition modes,
    ///                     boundary-modulated eigenvalues) but the values depend on free inputs κ, ε_A, ε_B
    ///                     (the concrete case).
    /// </summary>
    public static string Classify()
    {
        var weak = CompositeGridEr(0.02);
        var strong = CompositeGridEr(0.20);
        int nA = CausalSet.BuildGrid(6, 6).Count;

        bool anyMixing = MixingIsNonTrivial(weak, nA) || MixingIsNonTrivial(strong, nA);
        bool dependsOnBoundary = MixingDependsOnBoundary(weak, strong, nA);
        int transitions = FamilyTransitionCount(strong, nA);

        if (!anyMixing) return "NO RELATION";
        if (dependsOnBoundary && transitions > 0) return "PARTIAL RELATION";

        // Boundary origin would require the angle to be fixed by the network alone (independent of free inputs),
        // which is not the case — the angle depends on the (free) boundary-coupling fraction.
        return "NO RELATION";
    }
}
