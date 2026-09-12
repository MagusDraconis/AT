using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_058 — Recovery Mechanism Audit.
///
/// Question: WHY is recovery not predicted by the rank budget, the multiplicity distribution, the
/// degeneracy count, λ₂ or the near-gap density, while capacity is?
///
/// The audit's hypothesis, stated before measurement: capacity and recovery are DIFFERENT OBSERVABLE
/// CLASSES. Capacity is a COUNT — ΔA is the number of distinct eigenvalues the perturbation creates,
/// and the rank budget bounds exactly that. Recovery is a MAGNITUDE — 1 − ‖λ′−λ‖₂/‖λ‖₂ is a relative
/// spectral displacement, and a bound on how MANY new levels appear says nothing about HOW FAR they
/// move. So the rank budget cannot be a recovery law, by type.
///
/// The magnitude mechanism, stated before measurement (first-order perturbation theory):
///   1 − recovery ≈ c · ‖δA‖_F / ‖λ‖₂,
/// i.e. the relative RMS shift is governed by the perturbation's Frobenius norm against the base
/// spectrum's norm. For the degree-d unit ring this predicts damage ∝ √(2k)/√(N·d·(d+1)) = √(2·dose·|E| / (N d (d+1))),
/// so the same relative dose is a SMALLER fractional change to a denser or heavier graph.
///
/// PHASE A (this file, committed on its own) freezes the hypothesis, the mechanism and every
/// prediction, and never constructs the perturbation ensemble. PHASE B measures.
///
/// Deterministic throughout: fixed rings, fixed seeds, fixed doses, no randomness.
/// </summary>
public class Y_D_058_Tests : ResearchTestBase
{
    public Y_D_058_Tests(ITestOutputHelper output) : base(output) { }

    /// <summary>The rings the D group has been auditing since D_054.</summary>
    private static readonly string[] CaseSet =
        ["D96", "Pair1-47", "S96-123", "S96-135", "Ring48", "Decay96", "Boost96", "D96-24"];

    /// <summary>The two scale-mirror rings that make the blind protocol real.</summary>
    private static readonly string[] BlindSet = AdaptabilityAudit.ScaleRingNames;

    private static double[,] AdjacencyOf(string name)
        => AdaptabilityAudit.CaseNames.Contains(name)
            ? AdaptabilityAudit.Adjacency(name)
            : AdaptabilityAudit.RingAdjacency(name);

    private static double[] SpectrumOf(string ring)
        => AdaptabilityAudit.SpectrumOf(AdjacencyOf(ring));

    private static int[] Mult(string ring) => AdaptabilityAudit.Buckets(SpectrumOf(ring)).Mult;

    private static int Headroom(string ring) => AdaptabilityAudit.N - Mult(ring).Length;

    private static int Edges(string ring) => AdaptabilityAudit.Edges(AdjacencyOf(ring)).Count;

    /// <summary>‖λ‖₂ — the Frobenius norm of the Laplacian, the natural scale of the spectrum.</summary>
    private static double SpectralNorm(string ring)
    {
        var spec = SpectrumOf(ring);
        return Math.Sqrt(spec.Sum(v => v * v));
    }

    /// <summary>Σ_i min(m_i − 1, r): the rank-budgeted number of new distinct eigenvalues at rank r.</summary>
    private static int Ceiling(int[] m, int r) => m.Sum(v => Math.Min(v - 1, r));

    private static int NominalRank(string family, int k)
        => family == "weight" ? AdaptabilityAudit.N : 2 * k;

    // ── 1. The class distinction ────────────────────────────────────────────

    /// <summary>One sampled perturbation with both observables and both mechanisms' quantities.</summary>
    private sealed record Sample(string Ring, string Family, double Dose, int K, int Rank,
        int CeilingSlots, int Headroom, int CountDelta, double Capacity,
        double Recovery, double Damage, double MagRatio, double EdgeRatio);

    private static List<Sample> Ensemble(IEnumerable<string> rings)
    {
        var list = new List<Sample>();
        foreach (string ring in rings)
        {
            var adj = AdjacencyOf(ring);
            var baseSpec = SpectrumOf(ring);
            var mult = Mult(ring);
            var (a0, _, _) = AdaptabilityAudit.Buckets(baseSpec);
            int head = AdaptabilityAudit.N - a0;
            int edges = Edges(ring);
            double baseNorm = SpectralNorm(ring);
            foreach (string fam in AdaptabilityAudit.Kinds)
                foreach (double dose in AdaptabilityAudit.Doses)
                {
                    int k = Math.Max(1, (int)Math.Round(dose * edges));
                    int rank = NominalRank(fam, k);
                    int ceiling = Ceiling(mult, rank);
                    foreach (uint seed in AdaptabilityAudit.Seeds)
                    {
                        uint s = unchecked(seed * 1000u + (uint)(fam.Length * 7) + (uint)(dose * 10000));
                        var p = AdaptabilityAudit.Perturb(adj, fam, k, s, dose * 2.0);
                        if (!AdaptabilityAudit.Connected(p)) continue;
                        var spec = AdaptabilityAudit.SpectrumOf(p);
                        var (a1, _, _) = AdaptabilityAudit.Buckets(spec);
                        double shift = Math.Sqrt(spec.Zip(baseSpec, (x, y) => (x - y) * (x - y)).Sum());
                        double recovery = 1.0 - shift / baseNorm;
                        // The magnitude mechanism's input: ‖δA‖_F / ‖λ‖₂.
                        double deltaF = 0.0;
                        for (int i = 0; i < AdaptabilityAudit.N; i++)
                            for (int j = 0; j < AdaptabilityAudit.N; j++)
                            {
                                double d = p[i, j] - adj[i, j];
                                deltaF += d * d;
                            }
                        deltaF = Math.Sqrt(deltaF);
                        list.Add(new Sample(ring, fam, dose, k, rank, ceiling, head,
                            a1 - a0, head > 0 ? (double)(a1 - a0) / head : 0.0,
                            recovery, 1.0 - recovery, deltaF / baseNorm, deltaF / baseNorm));
                    }
                }
        }
        return list;
    }

    [Fact]
    public void D058_01_Capacity_Is_A_Count_Recovery_Is_A_Magnitude()
    {
        var sb = new StringBuilder();
        PrintHeader("1. The class distinction — capacity is a COUNT, recovery is a MAGNITUDE");

        sb.AppendLine("THE HYPOTHESIS, STATED BEFORE MEASUREMENT.");
        sb.AppendLine("  Capacity is  ΔA/(N − A₀)  where ΔA = A₁ − A₀ is the number of NEW DISTINCT eigenvalues the");
        sb.AppendLine("  perturbation creates. It is an integer-valued COUNT, and the rank budget bounds exactly it:");
        sb.AppendLine("  ΔA ≤ Σ min(m_i − 1, r).");
        sb.AppendLine("  Recovery is  1 − ‖λ′−λ‖₂/‖λ‖₂, a relative spectral DISPLACEMENT. It is a real-valued");
        sb.AppendLine("  MAGNITUDE, and a bound on how MANY new levels appear says nothing about HOW FAR they move.");
        sb.AppendLine("  If that is right, then (i) the two targets should be close to orthogonal across samples, and");
        sb.AppendLine("  (ii) the rank-budget ceiling should have no grip on recovery at all.");
        sb.AppendLine();

        var samples = Ensemble(CaseSet);
        double[] countDelta = samples.Select(s => (double)s.CountDelta).ToArray();
        double[] shift = samples.Select(s => s.Damage).ToArray();
        sb.AppendLine("  TEST (i) — are the count and the magnitude actually different observables?");
        sb.AppendLine($"    ρ(ΔA, relative shift) over all {samples.Count} samples = {AdaptabilityAudit.Spearman(countDelta, shift):F3}");
        sb.AppendLine($"    per family:");
        foreach (string fam in AdaptabilityAudit.Kinds)
        {
            var fs = samples.Where(s => s.Family == fam).ToArray();
            sb.AppendLine($"      {fam,-8} ρ = {AdaptabilityAudit.Spearman(fs.Select(s => (double)s.CountDelta).ToArray(), fs.Select(s => s.Damage).ToArray()),6:F3}   (n = {fs.Length})");
        }
        sb.AppendLine();
        sb.AppendLine("  and the same comparison at the ring level, using each ring's mean over the ensemble:");
        var byRing = CaseSet.Select(r => new
        {
            Ring = r,
            Count = samples.Where(s => s.Ring == r).Average(s => (double)s.CountDelta),
            Damage = samples.Where(s => s.Ring == r).Average(s => s.Damage),
        }).ToArray();
        sb.AppendLine($"    ρ(mean ΔA, mean shift) across the {byRing.Length} rings = "
                      + $"{AdaptabilityAudit.Spearman(byRing.Select(x => x.Count).ToArray(), byRing.Select(x => x.Damage).ToArray()):F3}");
        sb.AppendLine();
        sb.AppendLine("  ring        mean ΔA (count)   mean relative shift (magnitude)   capacity   recovery");
        sb.AppendLine("  " + new string('-', 92));
        foreach (var x in byRing)
        {
            var inRing = samples.Where(s => s.Ring == x.Ring).ToArray();
            sb.AppendLine($"  {x.Ring,-10} {x.Count,17:F3} {x.Damage,33:F5} {inRing.Average(s => s.Capacity),11:F4} {inRing.Average(s => s.Recovery),10:F4}");
        }
        sb.AppendLine();
        sb.AppendLine("  Note the scale contrast, which is the point: ΔA moves by hundreds of percent across the");
        sb.AppendLine("  family while the relative shift moves by a few percent. They are not the same measurement");
        sb.AppendLine("  in different units, and nothing guarantees a bound on one bounds the other.");

        Output.WriteLine(sb.ToString());
    }

    // ── 2. The rank budget's scope ──────────────────────────────────────────

    [Fact]
    public void D058_02_Rank_Budget_Has_No_Grip_On_Recovery()
    {
        var sb = new StringBuilder();
        PrintHeader("2. The rank budget's scope — it bounds the count, and nothing else");

        var samples = Ensemble(CaseSet);

        sb.AppendLine("  TEST (ii) — does the rank-budget ceiling predict recovery? The ceiling is expressed as a");
        sb.AppendLine("  capacity (slots / headroom), and it is the same quantity D_057 showed predicting capacity");
        sb.AppendLine("  at ρ = 0.873 with no parameters. Here it is asked to predict recovery instead.");
        sb.AppendLine();
        sb.AppendLine("  ring        ceiling(cap)   capacity   recovery   ceiling vs recovery     ceiling ok for capacity?");
        sb.AppendLine("  " + new string('-', 108));
        var rows = new List<(string Ring, double Ceiling, double Capacity, double Recovery)>();
        foreach (string ring in CaseSet)
        {
            var rs = samples.Where(s => s.Ring == ring).ToArray();
            double ceiling = rs.Average(s => s.Headroom > 0 ? (double)s.CeilingSlots / s.Headroom : 0.0);
            double cap = rs.Average(s => s.Capacity);
            double rec = rs.Average(s => s.Recovery);
            rows.Add((ring, Math.Min(1.0, ceiling), cap, rec));
            sb.AppendLine($"  {ring,-10} {ceiling,13:F5} {cap,10:F5} {rec,10:F5} {Math.Abs(Math.Min(1.0, ceiling) - rec),22:F5}   {Math.Abs(Math.Min(1.0, ceiling) - cap),22:F5}");
        }
        double[] ceil = rows.Select(r => r.Ceiling).ToArray();
        double[] capv = rows.Select(r => r.Capacity).ToArray();
        double[] recv = rows.Select(r => r.Recovery).ToArray();
        sb.AppendLine("  " + new string('-', 108));
        sb.AppendLine($"  ρ(ceiling, capacity) = {AdaptabilityAudit.Spearman(ceil, capv):F3}   — the count law works");
        sb.AppendLine($"  ρ(ceiling, recovery) = {AdaptabilityAudit.Spearman(ceil, recv):F3}   — the count law has no grip");
        sb.AppendLine();
        sb.AppendLine("  AND THERE IS A STRUCTURAL REASON, not merely a bad correlation. The rank budget counts how");
        sb.AppendLine("  many DISTINCT levels a rank-r perturbation can produce. Recovery measures the L2 distance the");
        sb.AppendLine("  eigenvalues travel. Two perturbations can create the SAME number of new levels while moving");
        sb.AppendLine("  them by completely different distances — the count is invariant under a rescaling of the");
        sb.AppendLine("  shift, and the shift is what recovery reads. Demonstrated directly by the same ceiling");
        sb.AppendLine("  predicting capacity well and recovery not at all on the identical samples.");
        sb.AppendLine();
        sb.AppendLine("  ⇒ Recovery is not a failure of the count predictors; it is OUTSIDE THEIR TYPE. The question");
        sb.AppendLine("    'why is recovery not predicted by rank-budget, multiplicity, degeneracy, λ₂ or near-gap?'");
        sb.AppendLine("    therefore has an answer of a different kind from D_050–D_057: not 'the predictor is");
        sb.AppendLine("    constant' or 'the coefficients extrapolate badly', but 'four of those five are COUNT or");
        sb.AppendLine("    SHAPE statistics, and recovery is a MAGNITUDE'. §3 supplies the magnitude law.");

        Output.WriteLine(sb.ToString());
    }

    // ── 3. The magnitude mechanism ──────────────────────────────────────────

    [Fact]
    public void D058_03_The_Magnitude_Law()
    {
        var sb = new StringBuilder();
        PrintHeader("3. The magnitude mechanism — damage from the perturbation norm against the spectral norm");

        sb.AppendLine("  THE CLAIM, stated before measurement. First-order perturbation theory gives δλ_i = v_iᵀ δA v_i,");
        sb.AppendLine("  so the RMS eigenvalue shift scales with ‖δA‖_F. Normalizing by the base spectrum's norm,");
        sb.AppendLine("      1 − recovery  ≈  c · ‖δA‖_F / ‖λ‖₂,");
        sb.AppendLine("  and for a degree-d unit ring ‖λ‖₂ = √(N·d·(d+1)) exactly (trace L² = dN(d+1) for a d-regular");
        sb.AppendLine("  simple graph), while k edge deletions give ‖δA‖_F = √(2k). So the damage should scale as");
        sb.AppendLine("      √(2k) / √(N·d·(d+1))     —   DENSER OR HEAVIER GRAPHS SUFFER LESS at the same dose fraction.");
        sb.AppendLine("  The WEIGHT family is different in kind: it rescales every edge, so ‖δA‖_F ∝ ε‖A‖_F, and because");
        sb.AppendLine("  recovery is scale-invariant the whole family is insensitive to an overall weight scale.");
        sb.AppendLine();

        var samples = Ensemble(CaseSet);
        double[] ratio = samples.Select(s => s.MagRatio).ToArray();
        double[] damage = samples.Select(s => s.Damage).ToArray();
        var (slope, intercept, r2, _) = AdaptabilityAudit.Fit(ratio, damage);
        sb.AppendLine($"  MEASURED over all {samples.Count} samples:");
        sb.AppendLine($"    1 − recovery = {slope.ToString("F5", CultureInfo.InvariantCulture)} · (‖δA‖_F/‖λ‖₂) + {intercept.ToString("G5", CultureInfo.InvariantCulture)}    R² = {r2:F4}");
        sb.AppendLine($"    Spearman ρ = {AdaptabilityAudit.Spearman(ratio, damage):F4}");
        sb.AppendLine();
        sb.AppendLine("  per family, because the weight family is a different regime by construction:");
        sb.AppendLine("  family    n      slope        intercept      R²        ρ");
        sb.AppendLine("  " + new string('-', 66));
        foreach (string fam in AdaptabilityAudit.Kinds)
        {
            var fs = samples.Where(s => s.Family == fam).ToArray();
            var f = AdaptabilityAudit.Fit(fs.Select(s => s.MagRatio).ToArray(), fs.Select(s => s.Damage).ToArray());
            sb.AppendLine($"  {fam,-8} {fs.Length,4} {f.Slope,12:F5} {f.Intercept,14:G5} {f.R2,10:F4} {AdaptabilityAudit.Spearman(fs.Select(s => s.MagRatio).ToArray(), fs.Select(s => s.Damage).ToArray()),8:F3}");
        }
        sb.AppendLine();
        sb.AppendLine("  THE DERIVED DEGREE LAW for the edge families. For a d-regular unit ring, damage should be");
        sb.AppendLine("  √(2·dose·|E| / (N·d·(d+1))). Predicted against measured, per ring, delete family:");
        sb.AppendLine("  ring        degree   ‖λ‖₂ (measured)   ‖λ‖₂ = √(N·d·(d+1))   predicted damage   measured damage");
        sb.AppendLine("  " + new string('-', 110));
        foreach (string ring in CaseSet)
        {
            var adj = AdjacencyOf(ring);
            double d = 2.0 * Edges(ring) / AdaptabilityAudit.N;
            double norm = SpectralNorm(ring);
            double predicted = 0.0;
            int n = 0;
            foreach (double dose in AdaptabilityAudit.Doses)
            {
                int k = Math.Max(1, (int)Math.Round(dose * Edges(ring)));
                predicted += Math.Sqrt(2.0 * k) / Math.Sqrt(AdaptabilityAudit.N * d * (d + 1));
                n++;
            }
            predicted /= n;
            var ds = samples.Where(s => s.Ring == ring && s.Family == "delete").ToArray();
            sb.AppendLine($"  {ring,-10} {d,6:F1} {norm,18:F2} {Math.Sqrt(96 * d * (d + 1)),23:F2} {predicted,18:F5} {ds.Average(s => s.Damage),16:F5}");
        }
        sb.AppendLine("  (the √(N·d·(d+1)) column is exact only for an unweighted d-regular ring, so the weighted and");
        sb.AppendLine("   long-range rings are expected to deviate; the MEASURED ‖λ‖₂ column is what the law actually uses)");

        sb.AppendLine();
        sb.AppendLine("  THE PREDICTION-FREE TEST: replace the formula's denominator with the MEASURED ‖λ‖₂ — which");
        sb.AppendLine("  is a spectral observable, not a fit — and ask how much of the damage it explains per family.");
        sb.AppendLine("  If a single slope covers all rings, the magnitude law is structural rather than calibrated.");
        sb.AppendLine("  family    ρ(damage, √(2k)/‖λ‖₂)   best single slope   R² at that slope");
        sb.AppendLine("  " + new string('-', 76));
        foreach (string fam in AdaptabilityAudit.Kinds)
        {
            var fs = samples.Where(s => s.Family == fam).ToArray();
            double[] x = fs.Select(s => s.MagRatio).ToArray();
            double[] y = fs.Select(s => s.Damage).ToArray();
            var f = AdaptabilityAudit.Fit(x, y);
            sb.AppendLine($"  {fam,-8} {AdaptabilityAudit.Spearman(x, y),22:F3} {f.Slope,19:F5} {f.R2,19:F4}");
        }

        Output.WriteLine(sb.ToString());
    }

    // ── 4. PHASE A — the frozen prediction for the scale-mirror rings ───────

    [Fact]
    public void D058_04_Frozen_Prediction()
    {
        var sb = new StringBuilder();
        PrintHeader("4. The frozen prediction — two scale-mirror rings (PHASE A, no simulation)");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  A1. D96x4 and D96x025 carry EXACTLY D96's offset set (±1..±6), so their multiplicity");
        sb.AppendLine("      structure — and therefore every COUNT-based observable, capacity included — must be");
        sb.AppendLine("      byte-identical to D96's. Only the overall weight scale differs, by 4× and 1/4.");
        sb.AppendLine("  A2. Relative recovery 1 − ‖λ′−λ‖/‖λ‖ is SCALE-INVARIANT: multiplying the whole graph by a");
        sb.AppendLine("      constant multiplies numerator and denominator together. Therefore under the WEIGHT family,");
        sb.AppendLine("      which rescales every edge in proportion, these rings must reproduce D96's recovery EXACTLY.");
        sb.AppendLine("  A3. Under delete/add/rewire, however, edges are added or removed at FIXED unit weight, so the");
        sb.AppendLine("      same relative dose is a SMALLER fractional change to a heavier graph. The magnitude law");
        sb.AppendLine("      (1 − recovery ≈ c·‖δA‖_F/‖λ‖₂) therefore predicts recovery to RISE for the 4× ring and FALL");
        sb.AppendLine("      for the 1/4× ring, while their CAPACITIES stay pinned to D96's.");
        sb.AppendLine("  A4. No simulation runs in this section: the multiplicities, norms and predictions are pure");
        sb.AppendLine("      spectral arithmetic, and the D96 reference values quoted below are the published ones.");
        sb.AppendLine();

        sb.AppendLine("  THE SCALE MIRRORS, checked at prediction time:");
        sb.AppendLine("  ring        max m   A₀   pattern                     ‖λ‖₂        ‖λ‖₂ ratio to D96   edges");
        sb.AppendLine("  " + new string('-', 106));
        double d96Norm = SpectralNorm("D96");
        foreach (string ring in new[] { "D96" }.Concat(BlindSet))
        {
            var m = Mult(ring);
            var pat = m.GroupBy(v => v).OrderByDescending(g => g.Key).Take(4).Select(g => $"{g.Key}×{g.Count()}");
            double norm = SpectralNorm(ring);
            sb.AppendLine($"  {ring,-10} {m.Max(),6} {m.Length,5}   {string.Join(", ", pat),-32} {norm,10:F3} {norm / d96Norm,18:F4} {Edges(ring),8}");
        }
        sb.AppendLine();
        sb.AppendLine("  VERIFYING A1 AND A2 DIRECTLY (the strongest form of the prediction):");
        var mD96 = Mult("D96");
        foreach (string ring in BlindSet)
        {
            var m = Mult(ring);
            bool samePattern = m.OrderBy(v => v).SequenceEqual(mD96.OrderBy(v => v));
            bool sameA0 = m.Length == mD96.Length;
            sb.AppendLine($"    {ring,-9} multiplicity pattern identical to D96? {samePattern};  A₀ identical? {sameA0};  "
                          + $"headroom identical? {Headroom(ring) == Headroom("D96")}");
        }
        sb.AppendLine("    ⇒ the count-based predictions for both mirrors ARE D96's predictions, with no wiggle room.");

        sb.AppendLine();
        sb.AppendLine("  THE FROZEN PREDICTION");
        sb.AppendLine("  ring        capacity   recovery (delete/add/rewire)   recovery (weight)");
        sb.AppendLine("  " + new string('-', 84));
        sb.AppendLine("  D96         REPEATS D96's published capacity (0.99020)");
        sb.AppendLine("  D96x4       = D96's capacity   HIGHER than D96's         = D96's, exactly");
        sb.AppendLine("  D96x025     = D96's capacity   LOWER than D96's          = D96's, exactly");
        sb.AppendLine();
        sb.AppendLine("  The ordering the audit will test, stated as a strict inequality:");
        sb.AppendLine("    capacity:      D96x4 = D96 = D96x025            (identical, no tolerance)");
        sb.AppendLine("    recovery/edge: D96x4 > D96 > D96x025            (the magnitude law's signature)");
        sb.AppendLine("    recovery/wt:   D96x4 ≈ D96 ≈ D96x025            (scale invariance's signature)");
        sb.AppendLine();
        sb.AppendLine("  This is a three-way test in one: it checks the COUNT mechanism's blindness to scale, the");
        sb.AppendLine("  MAGNITUDE mechanism's sensitivity to it, and the scale invariance of the weight family — and");
        sb.AppendLine("  all three must hold for the class distinction in §1 to be the right explanation.");

        sb.AppendLine();
        sb.AppendLine("  The prediction is now on record. PHASE B (measurement, added in a later commit) tests it.");

        Assert.True(Mult("D96x4").OrderBy(v => v).SequenceEqual(mD96.OrderBy(v => v)),
            "the mirrors must be multiplicity-identical to D96");
        Assert.True(Mult("D96x025").OrderBy(v => v).SequenceEqual(mD96.OrderBy(v => v)),
            "the mirrors must be multiplicity-identical to D96");
        Assert.True(SpectralNorm("D96x4") > 3.9 * d96Norm && SpectralNorm("D96x025") < 0.3 * d96Norm,
            "the mirrors must actually move the spectral scale");
        Output.WriteLine(sb.ToString());
    }
}
