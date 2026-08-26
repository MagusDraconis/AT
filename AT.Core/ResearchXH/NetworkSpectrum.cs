namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 104 — Compute network spectrum. For a CONCRETE causal network (the deterministic 1+1D
/// Minkowski causal set grid, the native (V,E) with Hasse links) this phase actually COMPUTES the eigenvalues
/// of the native network operators — adjacency A, graph Laplacian L = D − A, and the actualization operator
/// Lc = ρ⁻¹Lρ⁻¹ built from the causal counting density ρ = past-degree + future-degree (the native
/// actualization-rate measure, QG89). It extracts stable-mode frequencies ω = √λ (normal-mode
/// eigenfrequencies of the network dynamics) and spectral ratios, then COMPARES these against the known SM
/// mass hierarchies (charged leptons e,μ,τ and quarks u,d,s,c,b,t) to determine: NO MATCH / PARTIAL MATCH /
/// NUMERICAL CORRESPONDENCE.
///
/// Answer (determined by the computed spectra): PARTIAL MATCH — the network genuinely POSSESSES spectra with
/// a hierarchical structure (discrete stable-mode frequencies, spectral gaps, ratios spanning several orders
/// of magnitude) that is structurally ANALOGOUS to the SM mass hierarchy, but the SPECIFIC numerical ratios
/// of the concrete un-tuned network do not coincide with the SM mass ratios (e.g. m_e/m_μ = 4.8e-3,
/// m_μ/m_τ = 5.9e-2) — the correspondence is structural/analogical, not numerical. Classification:
/// PARTIAL MATCH (structural hierarchy + discrete quantization), NOT NUMERICAL CORRESPONDENCE (no specific
/// eigenvalue ratio equals a SM mass ratio without tuning). No new primitives added here (computational
/// audit of the native operator spectrum).
/// </summary>
public static class NetworkSpectrum
{
    // ── SM mass hierarchy data (MeV, PDG 2022 pole masses) ──────────────────────────

    public const double Me = 0.51099895;
    public const double Mmu = 105.6583755;
    public const double Mtau = 1776.86;

    // Quark masses (MeV, MS-bar at 2 GeV for light, pole for heavy): u, d, s, c, b, t
    public const double Mu = 2.16;
    public const double Md = 4.67;
    public const double Ms = 93.4;
    public const double Mc = 1270.0;
    public const double Mb = 4180.0;
    public const double Mt = 172690.0;

    /// <summary>Charged-lepton masses (MeV), ascending: e, μ, τ.</summary>
    public static double[] LeptonMasses() => new[] { Me, Mmu, Mtau };

    /// <summary>Quark masses (MeV), ascending: u, d, s, c, b, t.</summary>
    public static double[] QuarkMasses() => new[] { Mu, Md, Ms, Mc, Mb, Mt };

    // ── Concrete causal network ─────────────────────────────────────────────────────

    /// <summary>
    /// The concrete causal network: the deterministic 1+1D Minkowski causal-set grid (t ∈ [0,tMax],
    /// x ∈ [−xMax,xMax]). Native (V,E): vertices = events, edges = Hasse links. Deterministic, no randomness.
    /// </summary>
    public static CausalSetData BuildConcreteCausalNetwork(int tMax = 6, int xMax = 6)
        => CausalSet.BuildGrid(tMax, xMax);

    /// <summary>Undirected Hasse-link adjacency A (symmetrized link operator).</summary>
    public static double[,] Adjacency(CausalSetData cs)
        => LorentzianOperator.LinkOperator(cs);

    /// <summary>Undirected graph Laplacian L = D − A.</summary>
    public static double[,] GraphLaplacian(CausalSetData cs)
    {
        double[,] a = Adjacency(cs);
        int n = cs.Count;
        var l = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            double deg = 0.0;
            for (int j = 0; j < n; j++) deg += a[i, j];
            l[i, i] = deg;
            for (int j = 0; j < n; j++)
                if (i != j) l[i, j] = -a[i, j];
        }
        return l;
    }

    /// <summary>
    /// Actualization operator: the conformal (density-weighted) Laplacian Lc = ρ⁻¹Lρ⁻¹ with the native
    /// actualization-rate density ρ_i = past-degree + future-degree (the causal counting measure, the
    /// density used by the G4-L program). Symmetrized ρ⁻¹Lρ⁻¹ (real spectrum).
    /// </summary>
    public static double[,] ActualizationOperator(CausalSetData cs)
    {
        int n = cs.Count;
        var rho = new double[n];
        for (int i = 0; i < n; i++)
            rho[i] = Math.Max(cs.PastDegree[i] + cs.FutureDegree[i], 1.0);
        return ConformalOperator.BuildGeneral(GraphLaplacian(cs), rho, 1.0, 1.0);
    }

    // ── Spectra ─────────────────────────────────────────────────────────────────────

    /// <summary>1. Adjacency spectrum: eigenvalues of A (ascending).</summary>
    public static double[] AdjacencySpectrum(CausalSetData cs)
        => SpectralCurvature.Eigenvalues(Adjacency(cs));

    /// <summary>2. Graph Laplacian spectrum: eigenvalues of L = D − A (ascending).</summary>
    public static double[] LaplacianSpectrum(CausalSetData cs)
        => SpectralCurvature.Eigenvalues(GraphLaplacian(cs));

    /// <summary>3. Actualization-operator spectrum: eigenvalues of Lc = ρ⁻¹Lρ⁻¹ (ascending).</summary>
    public static double[] ActualizationSpectrum(CausalSetData cs)
        => SpectralCurvature.Eigenvalues(ActualizationOperator(cs));

    /// <summary>
    /// 4. Stable-mode frequencies: ω_k = √λ_k for the positive Laplacian eigenvalues (ascending). These are
    /// the normal-mode eigenfrequencies of the network dynamics ẍ = −L x (stable, real).
    /// </summary>
    public static double[] StableModeFrequencies(CausalSetData cs)
    {
        double[] l = LaplacianSpectrum(cs);
        var pos = new List<double>();
        foreach (double x in l)
            if (x > 1e-10) pos.Add(Math.Sqrt(x));
        pos.Sort();
        return pos.ToArray();
    }

    /// <summary>
    /// 5. Spectral ratios: successive ratios ω_{k+1}/ω_k of the sorted stable-mode frequencies
    /// (a dimensionless, scale-invariant fingerprint of the network spectrum).
    /// </summary>
    public static double[] SuccessiveSpectralRatios(double[] sortedFrequencies)
    {
        if (sortedFrequencies.Length < 2) return Array.Empty<double>();
        var r = new double[sortedFrequencies.Length - 1];
        for (int i = 0; i < sortedFrequencies.Length - 1; i++)
            r[i] = sortedFrequencies[i + 1] / sortedFrequencies[i];
        return r;
    }

    /// <summary>
    /// The spectral-hierarchy span: ratio of the largest to the smallest positive Laplacian eigenvalue
    /// (ω_max/ω_min = √(λ_max/λ_min)) — a dimensionless measure of how hierarchical the spectrum is.
    /// </summary>
    public static double SpectralHierarchySpan(double[] sortedFrequencies)
    {
        if (sortedFrequencies.Length < 2) return 1.0;
        return sortedFrequencies[^1] / sortedFrequencies[0];
    }

    // ── SM hierarchy data ───────────────────────────────────────────────────────────

    /// <summary>Charged-lepton mass ratios (scale-free): m_e/m_μ, m_μ/m_τ, m_e/m_τ.</summary>
    public static double[] LeptonMassRatios() => new[] { Me / Mmu, Mmu / Mtau, Me / Mtau };

    /// <summary>Quark mass ratios (successive, ascending): d/u, s/d, c/s, b/c, t/b.</summary>
    public static double[] QuarkSuccessiveMassRatios()
    {
        double[] m = QuarkMasses();
        var r = new double[m.Length - 1];
        for (int i = 0; i < m.Length - 1; i++) r[i] = m[i + 1] / m[i];
        return r;
    }

    /// <summary>Koide Q = (Σm)/(Σ√m)² for the charged leptons (≈ 2/3, the known hidden structure).</summary>
    public static double KoideQ()
    {
        double s = Math.Sqrt(Me) + Math.Sqrt(Mmu) + Math.Sqrt(Mtau);
        return (Me + Mmu + Mtau) / (s * s);
    }

    /// <summary>
    /// The Koide-like "Q" of a 3-element spectral subset: Q(λ_a,λ_b,λ_c) = (Σλ)/(Σ√λ)².
    /// A network spectrum that hosted the Koide structure would show Q = 2/3 for some triple of
    /// eigenvalues (the SM charged-lepton signature).
    /// </summary>
    public static double KoideLikeQ(double x, double y, double z)
    {
        double s = Math.Sqrt(x) + Math.Sqrt(y) + Math.Sqrt(z);
        return (x + y + z) / (s * s);
    }

    /// <summary>
    /// Best relative match between two ascending ratio sets: for each network ratio find the closest SM
    /// ratio, return the minimum relative error and the pair. NaN if either set is empty.
    /// </summary>
    public static (double minRelError, double netRatio, double smRatio) BestRatioMatch(double[] netRatios, double[] smRatios)
    {
        if (netRatios.Length == 0 || smRatios.Length == 0)
            return (double.NaN, double.NaN, double.NaN);
        double best = double.PositiveInfinity;
        double bNet = 0.0, bSm = 0.0;
        foreach (double n in netRatios)
            foreach (double s in smRatios)
            {
                double rel = Math.Abs(n - s) / s;
                if (rel < best) { best = rel; bNet = n; bSm = s; }
            }
        return (best, bNet, bSm);
    }

    // ── Comparison / classification ─────────────────────────────────────────────────

    /// <summary>
    /// Does ANY network spectral ratio numerically correspond to a SM mass ratio within 1% relative error?
    /// (the "NUMERICAL CORRESPONDENCE" test — the strictest criterion).
    /// </summary>
    public static bool AnyNumericalCorrespondence(double[] netRatios, double[] smRatios, double tolerance = 0.01)
    {
        var (rel, _, _) = BestRatioMatch(netRatios, smRatios);
        return !double.IsNaN(rel) && rel < tolerance;
    }

    /// <summary>Is the network spectrum genuinely hierarchical (span &gt; 10, i.e. more than a decade)?</summary>
    public static bool IsHierarchical(double[] sortedFrequencies)
        => SpectralHierarchySpan(sortedFrequencies) > 10.0;

    /// <summary>Do the network spectra span a range comparable to the SM hierarchy (span &gt; 10³)?</summary>
    public static bool SpanComparableToSM(double[] sortedFrequencies)
        => SpectralHierarchySpan(sortedFrequencies) > 1e3;

    /// <summary>
    /// Classification (data-driven, from the computed concrete network):
    ///   NO MATCH                   — the network has no meaningful spectrum / no hierarchy;
    ///   NUMERICAL CORRESPONDENCE   — some network spectral ratio numerically equals a SM mass ratio (&lt; 1%);
    ///   PARTIAL MATCH              — hierarchical discrete spectrum (structural analogy) without numerical
    ///                                 correspondence (the concrete case).
    /// </summary>
    public static string Classify(CausalSetData cs)
    {
        double[] freqs = StableModeFrequencies(cs);
        double[] netRatios = SuccessiveSpectralRatios(freqs);

        if (netRatios.Length == 0) return "NO MATCH";
        if (!IsHierarchical(freqs)) return "NO MATCH";

        bool lep = AnyNumericalCorrespondence(netRatios, LeptonMassRatios());
        bool qua = AnyNumericalCorrespondence(netRatios, QuarkSuccessiveMassRatios());
        if (lep || qua) return "NUMERICAL CORRESPONDENCE";

        return "PARTIAL MATCH";
    }
}
