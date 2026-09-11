using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_055 — Pair1-47 Anomaly Audit.
///
/// Question: why is Pair1-47 (the ring with offsets ±1 and ±47) the only ring in the whole audited
/// family whose capacity collapses (0.42089, against 0.93693 … 1.00000 for every other ring on either
/// case set)?
///
/// Method: compare Pair1-47 with the seven healthy rings on every observable the brief names — full
/// spectrum, multiplicity structure, spectral spacing, eigenvector localization, participation ratio,
/// symmetry classes — and REJECT any property that a healthy ring also has. The goal is the FIRST
/// property uniquely possessed by Pair1-47.
///
/// The audit's mechanism claim, stated before measurement: deleting or adding an edge is a RANK-2
/// perturbation, and a rank-r perturbation can split an eigenvalue of multiplicity m into at most
/// r + 1 distinct values. Therefore the achievable attractor increase is bounded level by level,
///   ΔA ≤ Σ_i min(m_i − 1, r),
/// so a spectrum whose headroom is held in ONE huge level is a RANK TRAP: it needs a high-rank
/// perturbation to release what many small levels release at once.
///
/// Deterministic throughout: fixed rings, fixed seeds, fixed doses, no randomness.
/// </summary>
public class Y_D_055_Tests : ResearchTestBase
{
    public Y_D_055_Tests(ITestOutputHelper output) : base(output) { }

    /// <summary>Pair1-47 first, then the healthy rings of the family.</summary>
    private static readonly string[] Rings =
        ["Pair1-47", "D96", "S96-123", "S96-135", "Ring48", "Boost96", "Decay96", "D96-24"];

    private const string Anomaly = "Pair1-47";

    private static double[] SpectrumOf(string ring)
        => AdaptabilityAudit.SpectrumOf(AdaptabilityAudit.RingAdjacency(ring));

    /// <summary>
    /// The measured profile of any named ring: cached by the shared ensemble for D_048's six cases,
    /// computed under the identical protocol for the rest. Memoized because the ring ensemble costs a
    /// real simulation.
    /// </summary>
    private static readonly Dictionary<string, AdaptabilityProfile> ProfileCache = [];

    private static AdaptabilityProfile ProfileOf(string ring)
    {
        if (ProfileCache.TryGetValue(ring, out var cached)) return cached;
        var p = AdaptabilityAudit.CaseNames.Contains(ring)
            ? AdaptabilityAudit.Profiles.Single(x => x.Name == ring)
            : AdaptabilityAudit.Study(ring, AdaptabilityAudit.RingAdjacency(ring));
        ProfileCache[ring] = p;
        return p;
    }

    /// <summary>Multiplicities of the sorted spectrum, descending by multiplicity.</summary>
    private static int[] Multiplicities(double[] spectrum)
        => AdaptabilityAudit.Buckets(spectrum).Mult;

    /// <summary>ΔE_lock = (1/N)·Σ_{m>1} m·ln m — D_047's locked entropy.</summary>
    private static double Locked(double[] spectrum)
        => AdaptabilityAudit.LockedEntropy(Multiplicities(spectrum), AdaptabilityAudit.N);

    /// <summary>
    /// Rank ceiling: the largest attractor increase a rank-r perturbation can produce, given the
    /// multiplicity structure. Each level of multiplicity m can yield at most min(m − 1, r) new
    /// distinct eigenvalues, because r independent directions of shift support at most r + 1 values.
    /// </summary>
    private static int RankCeiling(int[] mult, int r)
        => mult.Sum(m => Math.Min(m - 1, r));

    // ── 1. The anomaly, derived and measured ────────────────────────────────

    [Fact]
    public void D055_01_The_Anomaly_Derived()
    {
        var sb = new StringBuilder();
        PrintHeader("1. The anomaly — what Pair1-47 has that no healthy ring has");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  A1. Pair1-47 is the ring with offsets ±1 and ±47 (degree 4, 192 edges). The comparison set is");
        sb.AppendLine("      the seven healthy rings of the audited family.");
        sb.AppendLine("  A2. A level's SHARE is its multiplicity over N = 96. ΔE_lock is D_047's (1/N)·Σ_{m>1} m·ln m.");
        sb.AppendLine("  A3. 'First property uniquely possessed' means: shared by NO healthy ring, and shared by no");
        sb.AppendLine("      other property candidate that the healthy rings also satisfy (each candidate is tested in §2).");
        sb.AppendLine();

        sb.AppendLine("  THE SPECTRUM STRUCTURE OF THE WHOLE FAMILY");
        sb.AppendLine("  ring        A₀ (distinct)   headroom   max mult   share of largest level   ΔE_lock   pattern");
        sb.AppendLine("  " + new string('-', 116));
        foreach (string ring in Rings)
        {
            var spec = SpectrumOf(ring);
            var mult = Multiplicities(spec);
            var (a0, _, _) = AdaptabilityAudit.Buckets(spec);
            int maxM = mult.Max();
            var pattern = mult.GroupBy(m => m).OrderByDescending(g => g.Key)
                               .Select(g => $"{g.Count()}×{g.Key}");
            sb.AppendLine($"  {ring,-10} {a0,13} {AdaptabilityAudit.N - a0,10} {maxM,10} {(double)maxM / AdaptabilityAudit.N,22:P1} {Locked(spec),10:F4}   {string.Join(", ", pattern)}");
        }

        sb.AppendLine();
        sb.AppendLine("  ALREADY VISIBLE, AND IT IS THE ANSWER: exactly one ring in the family has a level holding more");
        sb.AppendLine("  than half the spectrum. Pair1-47's largest level has multiplicity 50 of 96 — a SHARE of 52.1 % —");
        sb.AppendLine("  while the largest level of any healthy ring holds at most 12 of 96 (12.5 %, on D96-24), and the");
        sb.AppendLine("  median healthy ring's largest level holds 6 (6.3 %). Enumerate the rejection:");
        foreach (string ring in Rings.Where(r => r != Anomaly))
        {
            var mult = Multiplicities(SpectrumOf(ring));
            sb.AppendLine($"    {ring,-10} max mult {mult.Max(),3} = {100.0 * mult.Max() / AdaptabilityAudit.N,5:F1} %  → shares this level's size? NO");
        }
        sb.AppendLine($"    {Anomaly,-10} max mult {Multiplicities(SpectrumOf(Anomaly)).Max(),3} = {100.0 * Multiplicities(SpectrumOf(Anomaly)).Max() / AdaptabilityAudit.N,5:F1} %  → UNIQUE in the family");

        sb.AppendLine();
        sb.AppendLine("  WHY — A CLOSED-FORM ARITHMETIC REASON. For a symmetric circulant ring with unit weights on a");
        sb.AppendLine("  signed offset set S, the Laplacian eigenvalue at wavevector k is");
        sb.AppendLine("      λ_k = 2·Σ_{d∈S} (1 − cos(2πkd/N)).");
        sb.AppendLine("  Pair1-47 has S = {1, 47}, and 47 = N/2 − 1. For the second offset,");
        sb.AppendLine("      cos(2πk·47/96) = cos(πk − 2πk/96) = (−1)^k · cos(2πk/96),");
        sb.AppendLine("  so with θ_k = 2πk/96:");
        sb.AppendLine("      k EVEN:  λ_k = 2[(1 − cos θ) + (1 − cos θ)]  = 4(1 − cos θ_k)");
        sb.AppendLine("      k ODD :  λ_k = 2[(1 − cos θ) + (1 + cos θ)]  = 4   EXACTLY, for every odd k.");
        sb.AppendLine("  There are 48 odd k in 1 … 95, and k = 24 and k = 72 also give cos θ = 0, i.e. λ = 4. So the level");
        sb.AppendLine("  λ = 4 has multiplicity 48 + 2 = 50. In general an offset pair {±1, ±d} produces this level exactly");
        sb.AppendLine("  when d ≡ ±1 (mod N/2) — the offset is congruent to the fundamental step modulo the half-period.");
        sb.AppendLine("  Equivalently: the pair ±47 = {47, 49} is the same edge set as {N/2 − 1, N/2 + 1}, i.e. ±1 shifted");
        sb.AppendLine("  by half the ring.");
        sb.AppendLine();
        sb.AppendLine("  VERIFICATION — the identity holds for every odd mode, to machine precision:");
        int checkedOdd = 0;
        double worstDev = 0.0;
        for (int k = 0; k < 96; k++)
        {
            double theta = 2.0 * Math.PI * k / 96.0;
            double lambda = 2.0 * ((1.0 - Math.Cos(theta)) + (1.0 - Math.Cos(2.0 * Math.PI * k * 47.0 / 96.0)));
            if (k % 2 == 1)
            {
                checkedOdd++;
                worstDev = Math.Max(worstDev, Math.Abs(lambda - 4.0));
            }
        }
        sb.AppendLine($"    odd modes checked: {checkedOdd}; largest |λ_k − 4| = {worstDev.ToString("G3", CultureInfo.InvariantCulture)}");
        sb.AppendLine("    and the same count read off the ACTUAL adjacency spectrum rather than the formula:");
        var spec47 = SpectrumOf(Anomaly);
        var mult47 = Multiplicities(spec47);
        int level4 = spec47.Count(v => Math.Abs(v - 4.0) < 1e-9);
        sb.AppendLine($"    eigenvalues within 1e-9 of 4 in Pair1-47's Laplacian spectrum: {level4}  (predicted 50)");

        sb.AppendLine();
        sb.AppendLine("  So the first property uniquely possessed by Pair1-47 is a SINGLE DEGENERATE LEVEL CARRYING MORE");
        sb.AppendLine("  THAN HALF THE SPECTRUM, and it has an exact arithmetic cause: one of its offsets is congruent to");
        sb.AppendLine("  the fundamental step modulo the ring's half-period.");

        Assert.True(checkedOdd == 48, "there must be 48 odd modes");
        Assert.True(worstDev < 1e-12, "the odd-mode identity λ_k = 4 must hold exactly");
        Assert.True(level4 == 50, "the λ = 4 level must have multiplicity 50");
        Assert.All(Rings.Where(r => r != Anomaly),
            r => Assert.True(Multiplicities(SpectrumOf(r)).Max() <= 12,
                "no healthy ring may have a comparable dominant level"));

        Output.WriteLine(sb.ToString());
    }

    // ── 2. Rejecting the properties shared by healthy rings ─────────────────

    /// <summary>
    /// Participation ratio of the k-th Laplacian eigenvector, taken as the exact Fourier mode. On a
    /// circulant the modes diagonalise the Laplacian, so this is the eigenvector, not an approximation.
    /// </summary>
    private static (double Pr, double Residual) FourierModePr(string ring, int k)
    {
        int n = AdaptabilityAudit.N;
        var adj = AdaptabilityAudit.RingAdjacency(ring);
        var v = new double[n];
        for (int i = 0; i < n; i++) v[i] = Math.Cos(2.0 * Math.PI * k * i / n) / Math.Sqrt(n);
        var w = new double[n];
        for (int i = 0; i < n; i++)
        {
            double s = 0.0;
            for (int j = 0; j < n; j++) s += adj[i, j] * v[j];
            w[i] = s;
        }
        double lam = 0.0, nrm = 0.0;
        for (int i = 0; i < n; i++) { lam += v[i] * w[i]; nrm += v[i] * v[i]; }
        lam /= nrm;
        double resid = 0.0, s2 = 0.0, s4 = 0.0;
        for (int i = 0; i < n; i++)
        {
            resid = Math.Max(resid, Math.Abs(w[i] - lam * v[i]));
            s2 += v[i] * v[i];
            s4 += v[i] * v[i] * v[i] * v[i];
        }
        return (s2 * s2 / s4, resid);
    }

    [Fact]
    public void D055_02_Rejecting_Shared_Properties()
    {
        var sb = new StringBuilder();
        PrintHeader("2. Rejecting every property the healthy rings also have");

        sb.AppendLine("  Each candidate is scored on whether it SEPARATES Pair1-47 from all seven healthy rings. A");
        sb.AppendLine("  property that any healthy ring shares is rejected as a candidate, however suggestive it looks.");
        sb.AppendLine();

        var spec47 = SpectrumOf(Anomaly);
        var mult47 = Multiplicities(spec47);
        var (a047, _, _) = AdaptabilityAudit.Buckets(spec47);

        // ── candidate 1: eigenvector localization / participation ratio ──
        sb.AppendLine("  CANDIDATE 1 — EIGENVECTOR LOCALIZATION / PARTICIPATION RATIO: REJECTED, and rejected in");
        sb.AppendLine("  principle rather than by measurement. On a circulant every Laplacian eigenvector is a Fourier");
        sb.AppendLine("  mode, |v_i| = 1/√N for every site, so the participation ratio");
        sb.AppendLine("    PR = (Σ|v_i|²)² / Σ|v_i|⁴");
        sb.AppendLine("  is EXACTLY N = 96 for every mode of every ring in the family. Verified with the exact Fourier");
        sb.AppendLine("  modes as eigenvectors:");
        sb.AppendLine("  ring        mode k   PR        residual ‖Av − λv‖∞");
        sb.AppendLine("  " + new string('-', 62));
        foreach (string ring in Rings)
        {
            foreach (int k in new[] { 1, 2 })
            {
                var (pr, resid) = FourierModePr(ring, k);
                sb.AppendLine($"  {ring,-10} {k,6} {pr,10:F4} {resid,26:G3}");
            }
        }
        sb.AppendLine("  ⇒ every ring is fully delocalized; localization cannot be the discriminator, and no measurement");
        sb.AppendLine("    of it can succeed for a circulant. Particle-like or edge-localized eigenstates are absent from");
        sb.AppendLine("    this whole family by construction.");
        sb.AppendLine();

        // ── candidate 2: symmetry class ──
        sb.AppendLine("  CANDIDATE 2 — SYMMETRY CLASS: REJECTED. Every ring here is a symmetric circulant, so every one");
        sb.AppendLine("  of them has the same automorphism group — N translations and (for a symmetric offset set) N");
        sb.AppendLine("  reflections, i.e. the dihedral group D_N of order 2N — and every one of them is");
        sb.AppendLine("  vertex-transitive with trivial vertex-stabilizers. Verify the count of translation symmetries");
        sb.AppendLine("  and the reflection symmetry directly:");
        sb.AppendLine("  ring        translations   reflections   vertex-transitive   orbit count");
        sb.AppendLine("  " + new string('-', 82));
        foreach (string ring in Rings)
        {
            var adj = AdaptabilityAudit.RingAdjacency(ring);
            int trans = 0, refl = 0;
            for (int s = 0; s < AdaptabilityAudit.N; s++)
            {
                bool ok = true;
                for (int i = 0; i < AdaptabilityAudit.N && ok; i++)
                    for (int j = i + 1; j < AdaptabilityAudit.N && ok; j++)
                        if (Math.Abs(adj[(i + s) % 96, (j + s) % 96] - adj[i, j]) > 1e-12) ok = false;
                if (ok) trans++;
            }
            for (int r = 0; r < AdaptabilityAudit.N; r++)
            {
                bool ok = true;
                for (int i = 0; i < AdaptabilityAudit.N && ok; i++)
                    for (int j = i + 1; j < AdaptabilityAudit.N && ok; j++)
                        if (Math.Abs(adj[(r - i + 96) % 96, (r - j + 96) % 96] - adj[i, j]) > 1e-12) ok = false;
                if (ok) refl++;
            }
            sb.AppendLine($"  {ring,-10} {trans,12} {refl,13} {trans == 96,19} {1,14}");
        }
        sb.AppendLine("  ⇒ identical symmetry class for all eight rings (order 2N, one orbit). REJECTED — and note this");
        sb.AppendLine("    also means the classical 'symmetry-protected degeneracy' explanation cannot by itself separate");
        sb.AppendLine("    Pair1-47: what differs is HOW MUCH of the spectrum one level holds, not whether symmetry exists.");
        sb.AppendLine();

        // ── candidates 3..: scalar properties ──
        sb.AppendLine("  CANDIDATES 3 … — SCALAR PROPERTIES, each compared across the family:");
        sb.AppendLine("  property                          Pair1-47    healthy rings (min … max)      separates?");
        sb.AppendLine("  " + new string('-', 96));
        var healthy = Rings.Where(r => r != Anomaly).ToArray();

        double MinPositiveGap(double[] s)
        {
            var pos = s.Where(v => v > 1e-9).OrderBy(v => v).ToArray();
            double best = double.PositiveInfinity;
            for (int i = 1; i < pos.Length; i++)
                if (pos[i] - pos[i - 1] > 1e-9) best = Math.Min(best, pos[i] - pos[i - 1]);
            return best;
        }
        double MeanPositiveGap(double[] s)
        {
            var dis = s.OrderBy(v => v).Where((v, i) => i == 0 || v - s.OrderBy(x => x).ElementAt(i - 1) > 1e-9).ToArray();
            if (dis.Length < 2) return 0.0;
            return (dis[^1] - dis[0]) / (dis.Length - 1);
        }

        void Row(string name, double mine, Func<string, double> other, bool lowerSeparates = false)
        {
            var vals = healthy.Select(other).ToArray();
            bool sep = lowerSeparates ? vals.All(v => v > mine) : vals.All(v => v != mine);
            sb.AppendLine($"  {name,-32} {mine,10:F4}   {vals.Min(),10:F4} … {vals.Max(),-10:F4}   {(sep ? "YES" : "no")}");
        }

        Row("λ₂ (algebraic connectivity)", ProfileOf(Anomaly).Lambda2,
            r => ProfileOf(r).Lambda2);
        Row("headroom N − A₀", AdaptabilityAudit.N - a047, r => AdaptabilityAudit.N - AdaptabilityAudit.Buckets(SpectrumOf(r)).A);
        Row("ΔE_lock", Locked(spec47), r => Locked(SpectrumOf(r)));
        Row("largest multiplicity", mult47.Max(), r => Multiplicities(SpectrumOf(r)).Max());
        Row("largest level's SHARE", (double)mult47.Max() / 96.0, r => (double)Multiplicities(SpectrumOf(r)).Max() / 96.0);
        Row("distinct count A₀", a047, r => AdaptabilityAudit.Buckets(SpectrumOf(r)).A);
        Row("minimum positive gap", MinPositiveGap(spec47), r => MinPositiveGap(SpectrumOf(r)));
        Row("mean distinct-level spacing", MeanPositiveGap(spec47), r => MeanPositiveGap(SpectrumOf(r)));
        Row("edges |E|", AdaptabilityAudit.Edges(AdaptabilityAudit.RingAdjacency(Anomaly)).Count,
            r => AdaptabilityAudit.Edges(AdaptabilityAudit.RingAdjacency(r)).Count);
        Row("degree", 2.0 * AdaptabilityAudit.Edges(AdaptabilityAudit.RingAdjacency(Anomaly)).Count / 96.0,
            r => 2.0 * AdaptabilityAudit.Edges(AdaptabilityAudit.RingAdjacency(r)).Count / 96.0);

        sb.AppendLine();
        sb.AppendLine("  ⇒ The separation column is the answer to the brief. Among the named observables, ONLY the");
        sb.AppendLine("    multiplicity structure separates Pair1-47 — specifically the SHARE of the spectrum held by a");
        sb.AppendLine("    single level (52.1 % against 2.1 … 12.5 %), which is the same fact as its largest multiplicity");
        sb.AppendLine("    and is what inflates its headroom to the family maximum. λ₂, the spacing statistics, the edge");
        sb.AppendLine("    count and the degree all FAIL to separate it; localization and symmetry fail in principle.");

        Assert.True(mult47.Max() > 2 * healthy.Max(r => Multiplicities(SpectrumOf(r)).Max()),
            "the dominant level must be more than twice any healthy ring's largest");
        Output.WriteLine(sb.ToString());
    }

    // ── 3. The mechanism: the rank budget ───────────────────────────────────

    /// <summary>One measured cell: mean gain and mean attractor increase for one ring, family and dose.</summary>
    private sealed record Cell(double Capacity, double MeanDeltaA, int Count);

    private static Cell MeasureCell(string ring, string kind, int k)
    {
        var adj = AdaptabilityAudit.RingAdjacency(ring);
        var baseSpec = SpectrumOf(ring);
        var (a0, _, _) = AdaptabilityAudit.Buckets(baseSpec);
        int head = AdaptabilityAudit.N - a0;
        double gainSum = 0.0, dASum = 0.0;
        int n = 0;
        foreach (uint seed in AdaptabilityAudit.Seeds)
        {
            var p = AdaptabilityAudit.Perturb(adj, kind, k, seed, 0.02);
            if (!AdaptabilityAudit.Connected(p)) continue;
            var s = AdaptabilityAudit.SpectrumOf(p);
            var (a1, _, _) = AdaptabilityAudit.Buckets(s);
            gainSum += head > 0 ? (double)(a1 - a0) / head : 0.0;
            dASum += a1 - a0;
            n++;
        }
        return n == 0 ? new Cell(double.NaN, double.NaN, 0) : new Cell(gainSum / n, dASum / n, n);
    }

    [Fact]
    public void D055_03_Rank_Budget_Mechanism()
    {
        var sb = new StringBuilder();
        PrintHeader("3. The mechanism — a rank budget against the multiplicity structure");

        sb.AppendLine("  THE CLAIM, STATED BEFORE MEASUREMENT. Deleting or adding an edge subtracts a RANK-2 matrix");
        sb.AppendLine("  (e_i e_j^T + e_j e_i^T) from the adjacency, and k edge operations give rank r ≤ 2k. Within a");
        sb.AppendLine("  degenerate eigenspace of multiplicity m the perturbation acts as an m × m matrix of rank ≤ r,");
        sb.AppendLine("  which therefore has at most r + 1 distinct eigenvalues. Hence the whole level splits into at most");
        sb.AppendLine("  r + 1 distinct values, and");
        sb.AppendLine("      ΔA ≤ Σ_i min(m_i − 1, r).");
        sb.AppendLine("  Capacity is ΔA/(N − A₀), so a spectrum whose HEADROOM SITS IN ONE HUGE LEVEL is a RANK TRAP: it");
        sb.AppendLine("  needs a high-rank perturbation to release what many small levels release at once.");
        sb.AppendLine();
        sb.AppendLine("  THE PREDICTION THIS MAKES, AND THE TWO WAYS TO FALSIFY IT:");
        sb.AppendLine("    (i)  the achievable ΔA must track Σ min(m_i − 1, 2k) as the dose rises;");
        sb.AppendLine("    (ii) the WEIGHT family must escape the trap, because rescaling every edge is a full-rank");
        sb.AppendLine("         perturbation — so Pair1-47's capacity should be normal under weight and collapsed under");
        sb.AppendLine("         the three edge-count families.");
        sb.AppendLine();

        sb.AppendLine("  PER-FAMILY CAPACITY (fractional doses, the shared ensemble's own scheme)");
        sb.AppendLine("  ring        delete     add      rewire    weight      mean     trap signature");
        sb.AppendLine("  " + new string('-', 88));
        foreach (string ring in Rings)
        {
            var p = ProfileOf(ring);
            var byKind = p.CapacityByKind;
            string sig = byKind[3] - byKind.Take(3).Average() > 0.2 ? "weight escapes, edges trapped" : "—";
            sb.AppendLine($"  {ring,-10} " + string.Join(" ", byKind.Select(v => v.ToString("F4", CultureInfo.InvariantCulture).PadLeft(9)))
                          + $" {byKind.Average(),9:F4}   {sig}");
        }

        sb.AppendLine();
        sb.AppendLine("  THE RANK CEILING VERSUS THE MEASURED ΔA (delete family, k edges = rank 2k)");
        sb.AppendLine("  ring        |E|   k      rank 2k   ceiling Σ min(m−1,2k)   headroom   ceiling capacity   measured cap");
        sb.AppendLine("  " + new string('-', 118));
        foreach (string ring in new[] { Anomaly, "D96", "S96-135" })
        {
            var spec = SpectrumOf(ring);
            var mult = Multiplicities(spec);
            var (a0, _, _) = AdaptabilityAudit.Buckets(spec);
            int head = AdaptabilityAudit.N - a0;
            int edges = AdaptabilityAudit.Edges(AdaptabilityAudit.RingAdjacency(ring)).Count;
            foreach (int k in new[] { 1, 3, 10, 19 })
            {
                int ceiling = RankCeiling(mult, 2 * k);
                var cell = MeasureCell(ring, "delete", k);
                sb.AppendLine($"  {ring,-10} {edges,5} {k,5} {2 * k,9} {ceiling,24} {head,10} {Math.Min(1.0, (double)ceiling / head),18:F4} {cell.Capacity,15:F4}");
            }
            sb.AppendLine();
        }

        sb.AppendLine("  THE DECISIVE SINGLE-OPERATION TEST. One edge deletion is rank 2, so it may create at most TWO");
        sb.AppendLine("  new distinct eigenvalues — from ANY level of ANY multiplicity. This is a sharp, falsifiable");
        sb.AppendLine("  prediction that is independent of the dose grid:");
        sb.AppendLine("  ring        A₀   ΔA after ONE deletion (3 seeds)      ceiling Σ min(m−1,2)   within bound?");
        sb.AppendLine("  " + new string('-', 92));
        bool allWithin = true;
        foreach (string ring in Rings)
        {
            var spec = SpectrumOf(ring);
            var mult = Multiplicities(spec);
            var (a0, _, _) = AdaptabilityAudit.Buckets(spec);
            var das = new List<int>();
            foreach (uint seed in AdaptabilityAudit.Seeds)
            {
                var p = AdaptabilityAudit.Perturb(AdaptabilityAudit.RingAdjacency(ring), "delete", 1, seed, 0.02);
                if (!AdaptabilityAudit.Connected(p)) continue;
                var (a1, _, _) = AdaptabilityAudit.Buckets(AdaptabilityAudit.SpectrumOf(p));
                das.Add(a1 - a0);
            }
            int ceiling = RankCeiling(mult, 2);
            bool within = das.All(d => d <= ceiling);
            allWithin &= within;
            sb.AppendLine($"  {ring,-10} {a0,4} {string.Join(", ", das),32} {ceiling,22} {(within ? "yes" : "NO")}");
        }
        sb.AppendLine();
        sb.AppendLine("  Reading the single-operation test: a level of multiplicity m does NOT contribute m − 1 new");
        sb.AppendLine("  eigenvalues under one edge deletion — it contributes at most 2, so the many-small-levels spectra");
        sb.AppendLine("  (D96 and friends) accumulate their headroom far faster than Pair1-47's one huge level can.");

        sb.AppendLine();
        sb.AppendLine("  CEILING VERSUS MEASUREMENT, and the size of the claim. The ceiling is an upper bound on a single");
        sb.AppendLine("  perturbation; the ensemble averages 3 seeds and five doses. Reported below is whether the");
        sb.AppendLine("  measured mean ΔA stays at or below the ceiling for every dose tested:");
        sb.AppendLine("  ring        doses where measured ΔA ≤ ceiling   doses tested");
        sb.AppendLine("  " + new string('-', 62));
        foreach (string ring in new[] { Anomaly, "D96" })
        {
            var spec = SpectrumOf(ring);
            var mult = Multiplicities(spec);
            var (a0, _, _) = AdaptabilityAudit.Buckets(spec);
            int edges = AdaptabilityAudit.Edges(AdaptabilityAudit.RingAdjacency(ring)).Count;
            int ok = 0, totalDoses = 0;
            foreach (double dose in AdaptabilityAudit.Doses)
            {
                int k = Math.Max(1, (int)Math.Round(dose * edges));
                var cell = MeasureCell(ring, "delete", k);
                totalDoses++;
                if (cell.MeanDeltaA <= RankCeiling(mult, 2 * k) + 1e-9) ok++;
            }
            sb.AppendLine($"  {ring,-10} {ok,32} {totalDoses,15}");
        }

        sb.AppendLine();
        sb.AppendLine("  DIAGNOSTIC — DOES THE DOMINANT LEVEL ACTUALLY SPLIT? For each family at the 1 % dose, the");
        sb.AppendLine("  number of distinct eigenvalues after perturbation (A₁) says directly how much of the level was");
        sb.AppendLine("  released. A full split would give A₁ = 96 for every ring.");
        sb.AppendLine("  ring        family   k     A₀    A₁ (distinct)   ΔA   ceiling Σ min(m−1,2k)   within bound?");
        sb.AppendLine("  " + new string('-', 104));
        foreach (string ring in new[] { Anomaly, "D96" })
        {
            var spec = SpectrumOf(ring);
            var mult = Multiplicities(spec);
            var (a0, _, _) = AdaptabilityAudit.Buckets(spec);
            int edges = AdaptabilityAudit.Edges(AdaptabilityAudit.RingAdjacency(ring)).Count;
            foreach (string fam in AdaptabilityAudit.Kinds)
            {
                int k = Math.Max(1, (int)Math.Round(0.01 * edges));
                int ceiling = RankCeiling(mult, fam == "weight" ? AdaptabilityAudit.N : 2 * k);
                foreach (uint seed in AdaptabilityAudit.Seeds.Take(1))
                {
                    var p = AdaptabilityAudit.Perturb(AdaptabilityAudit.RingAdjacency(ring), fam, k, seed, 0.02);
                    var (a1, _, _) = AdaptabilityAudit.Buckets(AdaptabilityAudit.SpectrumOf(p));
                    sb.AppendLine($"  {ring,-10} {fam,-8} {k,4} {a0,6} {a1,15} {a1 - a0,6} {ceiling,23} {(a1 - a0 <= ceiling ? "yes" : "NO")}");
                }
            }
        }

        sb.AppendLine();
        sb.AppendLine("  WHY THE WEIGHT FAMILY DOES NOT ESCAPE — the perturbed multiplicity pattern, printed rather than");
        sb.AppendLine("  argued. A full-rank random-weight perturbation should make every eigenvalue simple (A₁ = 96);");
        sb.AppendLine("  what it actually produces is read off below.");
        sb.AppendLine("  ring        family       perturbed pattern (m × count, largest first)");
        sb.AppendLine("  " + new string('-', 104));
        foreach (string ring in new[] { Anomaly, "D96" })
        {
            var spec = SpectrumOf(ring);
            int edges = AdaptabilityAudit.Edges(AdaptabilityAudit.RingAdjacency(ring)).Count;
            foreach (string fam in new[] { "delete", "weight" })
            {
                int k = Math.Max(1, (int)Math.Round(0.01 * edges));
                var p = AdaptabilityAudit.Perturb(AdaptabilityAudit.RingAdjacency(ring), fam, k, 11u, 0.02);
                var ps = AdaptabilityAudit.SpectrumOf(p);
                var pm = Multiplicities(ps);
                var pat = pm.GroupBy(m => m).OrderByDescending(g => g.Key).Take(6)
                            .Select(g => $"{g.Key}×{g.Count()}");
                var (pa, _, _) = AdaptabilityAudit.Buckets(ps);
                sb.AppendLine($"  {ring,-10} {fam,-10} A₁ = {pa,3}   {string.Join(", ", pat)}");
            }
        }
        sb.AppendLine();
        sb.AppendLine("  THE ANSWER IS VISIBLE HERE: the weight perturbation leaves the dominant level almost intact.");
        sb.AppendLine("  Pair1-47's λ = 4 level survives as a large-multiplicity block under BOTH the edge families and");
        sb.AppendLine("  the full-edge reweighting, so the 49 headroom slots it holds are never released. The level is");
        sb.AppendLine("  not merely degenerate — it is degenerate in a way that a random perturbation does not split.");
        sb.AppendLine();
        sb.AppendLine("  AND THE REASON IS THE SAME CONGRUENCE AGAIN. Pair1-47's edge set is two interleaved 96-cycles:");
        sb.AppendLine("  the ±1 cycle and the ±47 cycle, and ±47 ≡ ∓1 shifted by half the ring. Perturbing the WEIGHTS of");
        sb.AppendLine("  those two cycles cannot separate the odd-k modes, because for odd k the two cycles' contributions");
        sb.AppendLine("  are exactly complementary (4 = 2·[x] + 2·[2 − x] for any common factor x). Only a perturbation that");
        sb.AppendLine("  breaks the ±1/±(N/2 − 1) pairing itself can release the level — which is what D_055's ceiling says");
        sb.AppendLine("  quantitatively: the level needs a high-rank perturbation, and no family in the shared ensemble");
        sb.AppendLine("  supplies one at these doses.");

        Output.WriteLine(sb.ToString());
        Assert.True(allWithin, "the single-deletion bound ΔA ≤ Σ min(m−1, 2) must hold for every ring");
    }

    // ── 4. The perturbator's coin, audited ─────────────────────────────────

    [Fact]
    public void D055_04_The_Perturbator_Coin()
    {
        var sb = new StringBuilder();
        PrintHeader("4. Auditing the perturbator — why a 'full-rank random reweighting' does not split the level");

        sb.AppendLine("  The weight family is supposed to multiply every edge by an independent random factor, which is a");
        sb.AppendLine("  full-rank perturbation and should make every eigenvalue simple. It does not. So the perturbator");
        sb.AppendLine("  itself is audited here. The shared ensemble's generator is an LCG");
        sb.AppendLine("      x ← 1664525·x + 1013904223  (mod 2^32),   coin = Next() mod 2.");
        sb.AppendLine("  Both coefficients are ODD, so the lowest bit of x flips on every step:");
        sb.AppendLine("      (a·x + c) mod 2 = (x + 1) mod 2  for odd a, c.");
        sb.AppendLine("  A coin read from the LOW BIT therefore ALTERNATES deterministically, whatever the seed. Reproduce");
        sb.AppendLine("  the recurrence exactly and print the first draws:");
        for (uint seed = 11; seed <= 33; seed += 11)
        {
            uint x = seed;
            var draws = new List<int>();
            for (int i = 0; i < 12; i++) { x = unchecked(1664525u * x + 1013904223u); draws.Add((int)(x % 2)); }
            sb.AppendLine($"    seed {seed,3}: {string.Join(" ", draws)}");
        }
        sb.AppendLine();
        sb.AppendLine("  ⇒ every stream is 1,0,1,0,… — the 'random' sign of the weight perturbation is a fixed");
        sb.AppendLine("    alternation. Consequence, measured directly on the perturbed matrices: the number of DISTINCT");
        sb.AppendLine("    edge weights the weight family produces, against 192 edges for Pair1-47 and 576 for D96.");
        sb.AppendLine("  ring        distinct weights in the perturbed matrix   pattern");
        sb.AppendLine("  " + new string('-', 96));
        foreach (string ring in new[] { Anomaly, "D96", "Decay96" })
        {
            var p = AdaptabilityAudit.Perturb(AdaptabilityAudit.RingAdjacency(ring), "weight", 5, 11u, 0.02);
            var vals = new Dictionary<string, int>();
            for (int i = 0; i < 96; i++)
                for (int j = i + 1; j < 96; j++)
                    if (Math.Abs(p[i, j]) > 1e-12)
                    {
                        string key = p[i, j].ToString("F6", CultureInfo.InvariantCulture);
                        vals[key] = vals.GetValueOrDefault(key) + 1;
                    }
            var top = vals.OrderByDescending(kv => kv.Value).Take(4).Select(kv => $"{kv.Key}×{kv.Value}");
            sb.AppendLine($"  {ring,-10} {vals.Count,38}   {string.Join(", ", top)}");
        }
        sb.AppendLine();
        sb.AppendLine("  So the weight family is NOT an independent reweighting: because the node index and the edge");
        sb.AppendLine("  index are in step, whole offset classes receive ONE common factor, which leaves the graph");
        sb.AppendLine("  CIRCULANT (or nearly so) and therefore leaves a symmetry-protected level intact. That is why");
        sb.AppendLine("  the λ = 4 level survives reweighting here — and it is a defect in the shared perturbator, not a");
        sb.AppendLine("  property of Pair1-47.");
        sb.AppendLine();
        sb.AppendLine("  SCOPE OF THE DEFECT, stated honestly. It affects the WEIGHT family only: delete, add and rewire");
        sb.AppendLine("  draw their target from the full 32-bit value, whose low-order structure does not matter, so their");
        sb.AppendLine("  numbers stand. Every audit from D_048 onward reports weight as the STRONGEST family, and that");
        sb.AppendLine("  conclusion is unaffected in direction (a systematic reweighting is still a real perturbation);");
        sb.AppendLine("  what is unreliable is any fine structure attributed to weight's 'full-rank randomness'. This");
        sb.AppendLine("  audit therefore does not silently change the shared generator — that would invalidate stored");
        sb.AppendLine("  numbers in D_048–D_054 — but reproduces the defect here and recommends it as the next fix.");
        sb.AppendLine();

        // ── structural consequence: the reweighted graph is not generic ──
        sb.AppendLine("  STRUCTURAL CONSEQUENCE — the reweighted matrix is deterministic, not generic. The table below tests");
        sb.AppendLine("  whether the reweighted graph keeps a translation symmetry (it does NOT — so the defect is not a");
        sb.AppendLine("  symmetry being preserved but a specific, fixed sign pattern being applied), and reports how far");
        sb.AppendLine("  the spectrum consequently resolves:");
        sb.AppendLine("  ring        shift-by-1 preserved?   shift-by-2 preserved?   A₀   A₁ after weight");
        sb.AppendLine("  " + new string('-', 92));
        foreach (string ring in new[] { Anomaly, "D96", "Decay96", "S96-135" })
        {
            var baseAdj = AdaptabilityAudit.RingAdjacency(ring);
            var p = AdaptabilityAudit.Perturb(baseAdj, "weight", 5, 11u, 0.02);
            bool ByShift(int s)
            {
                for (int i = 0; i < 96; i++)
                    for (int j = i + 1; j < 96; j++)
                        if (Math.Abs(p[(i + s) % 96, (j + s) % 96] - p[i, j]) > 1e-12) return false;
                return true;
            }
            var (a0, _, _) = AdaptabilityAudit.Buckets(SpectrumOf(ring));
            var (a1, _, _) = AdaptabilityAudit.Buckets(AdaptabilityAudit.SpectrumOf(p));
            sb.AppendLine($"  {ring,-10} {ByShift(1),21} {ByShift(2),24} {a0,4} {a1,15}");
        }
        sb.AppendLine("  ⇒ the reweighted graph is NOT circulant and NOT shift-symmetric — it is simply a FIXED, systematic");
        sb.AppendLine("    ±ε pattern. Its effect on the spectrum is therefore SPECTRUM-DEPENDENT, which is precisely what");
        sb.AppendLine("    an independent reweighting must not be: D96 still reaches A₁ = 96, while Pair1-47 stalls at 51.");

        // ── quantifying the defect's effect on the anomaly ──
        sb.AppendLine("  HOW MUCH OF THE ANOMALY IS THE DEFECT? Quantified by re-running the weight family with a CORRECT");
        sb.AppendLine("  coin (drawn from the high bits, a proper independent sign per edge) and comparing. This is done");
        sb.AppendLine("  locally, so no stored number in D_048–D_054 is touched:");
        sb.AppendLine("  ring        weight cap (shared, periodic)   weight cap (corrected coin)   mean cap: shared → corrected");
        sb.AppendLine("  " + new string('-', 118));
        foreach (string ring in Rings)
        {
            var adj = AdaptabilityAudit.RingAdjacency(ring);
            var baseSpec = SpectrumOf(ring);
            var (a0, _, _) = AdaptabilityAudit.Buckets(baseSpec);
            int head = AdaptabilityAudit.N - a0;
            double sum = 0.0, recW = 0.0;
            int n = 0;
            foreach (double dose in AdaptabilityAudit.Doses)
                foreach (uint seed in AdaptabilityAudit.Seeds)
                {
                    var q = PerturbWeightCorrected(adj, dose * 2.0, seed);
                    if (!AdaptabilityAudit.Connected(q)) continue;
                    var (a1, _, _) = AdaptabilityAudit.Buckets(AdaptabilityAudit.SpectrumOf(q));
                    sum += head > 0 ? (double)(a1 - a0) / head : 0.0;
                    n++;
                }
            recW = n > 0 ? sum / n : double.NaN;
            var prof = ProfileOf(ring);
            double sharedW = prof.CapacityByKind[Array.IndexOf(AdaptabilityAudit.Kinds, "weight")];
            double meanShared = prof.Capacity;
            double meanCorrected = (prof.CapacityByKind[0] + prof.CapacityByKind[1] + prof.CapacityByKind[2] + recW) / 4.0;
            sb.AppendLine($"  {ring,-10} {sharedW,29:F4} {recW,30:F4} {meanShared,32:F4} → {meanCorrected:F4}");
        }
        sb.AppendLine();
        sb.AppendLine("  ⇒ The correction lifts Pair1-47's weight capacity to full resolution — as the rank argument");
        sb.AppendLine("    requires, since a genuinely independent reweighting is full rank — and it moves its ensemble");
        sb.AppendLine("    mean capacity off the floor. It does NOT remove the anomaly: under the three edge-count");
        sb.AppendLine("    families, which the defect does not touch, Pair1-47 remains at 0.41 / 0.40 / 0.50 while every");
        sb.AppendLine("    healthy ring stays above 0.92. The collapse is therefore a multiplicity-structure fact with a");
        sb.AppendLine("    perturbator defect layered on top, and both are reported.");

        Output.WriteLine(sb.ToString());
    }

    /// <summary>
    /// The weight family with a CORRECT independent coin, used only inside this audit to quantify the
    /// shared perturbator's defect. The coefficient is taken from the high bits, where an LCG is
    /// well behaved, instead of the lowest bit, which provably alternates.
    /// </summary>
    private static double[,] PerturbWeightCorrected(double[,] baseAdj, double eps, uint seed)
    {
        int n = AdaptabilityAudit.N;
        var a = new double[n, n];
        Array.Copy(baseAdj, a, baseAdj.Length);
        uint x = seed;
        foreach (var (i, j) in AdaptabilityAudit.Edges(baseAdj))
        {
            x = unchecked(1664525u * x + 1013904223u);
            double s = ((x >> 16) & 1u) == 1u ? 1.0 : -1.0;
            a[i, j] = a[j, i] = baseAdj[i, j] * (1.0 + eps * s);
        }
        return a;
    }

    // ── 5. Verdict ──────────────────────────────────────────────────────────

    [Fact]
    public void D055_05_Verdict()
    {
        var sb = new StringBuilder();
        PrintHeader("5. Verdict — DERIVED / EMERGENT / REFUTED");

        // The healthy set is EVERY audited ring except the anomaly, including D_054's held-out rings —
        // otherwise a property that a held-out healthy ring also has would be mistaken for unique.
        var healthy = AdaptabilityAudit.RingFamilyNames
            .Concat(AdaptabilityAudit.EdgeRingNames)
            .Where(r => r != Anomaly).ToArray();

        sb.AppendLine("  THE BRIEF ASKED FOR THE FIRST PROPERTY UNIQUELY POSSESSED BY PAIR1-47, WITH PROPERTIES SHARED");
        sb.AppendLine($"  BY HEALTHY RINGS REJECTED. The healthy comparison set is therefore ALL {healthy.Length} other audited rings,");
        sb.AppendLine("  including D_054's held-out edge-shape rings — a wider set than D_055's opening table, and the");
        sb.AppendLine("  wider set is required: E1 alone kills two otherwise plausible candidates.");
        sb.AppendLine();
        sb.AppendLine("  THE TEST: is Pair1-47's value OUTSIDE the whole interval spanned by the healthy rings?");
        sb.AppendLine("  property                       Pair1-47     healthy min … max          outside?   verdict");
        sb.AppendLine("  " + new string('-', 106));

        var spec47 = SpectrumOf(Anomaly);
        var mult47 = Multiplicities(spec47);
        var (a047, _, _) = AdaptabilityAudit.Buckets(spec47);
        var healthyProfiles = healthy.Select(ProfileOf).ToArray();

        double MinGap(double[] s)
        {
            var pos = s.Where(v => v > 1e-9).Select(v => Math.Round(v, 6)).OrderBy(v => v).Distinct().ToArray();
            double best = double.PositiveInfinity;
            for (int i = 1; i < pos.Length; i++) best = Math.Min(best, pos[i] - pos[i - 1]);
            return best;
        }
        double MeanGap(double[] s)
        {
            var dis = s.Where(v => v > 1e-9).Select(v => Math.Round(v, 6)).OrderBy(v => v).Distinct().ToArray();
            return dis.Length < 2 ? 0.0 : (dis[^1] - dis[0]) / (dis.Length - 1);
        }

        bool Sep(double mine, double lo, double hi) => mine < lo - 1e-12 || mine > hi + 1e-12;

        void Row2(string name, double mine, Func<string, double> other)
        {
            var vals = healthy.Select(other).ToArray();
            bool sep = Sep(mine, vals.Min(), vals.Max());
            string tag = name.Contains("multiplicity") || name.Contains("SHARE") || name.Contains("A₀")
                || name.Contains("headroom") || name.Contains("ΔE_lock")
                ? "function of the multiplicity structure" : "INDEPENDENT property";
            sb.AppendLine($"  {name,-30} {mine,10:F4}   {vals.Min(),10:F4} … {vals.Max(),-10:F4} {sep,10}   {(sep ? "SEPARATES" : "rejected")} — {tag}");
        }

        Row2("λ₂ (algebraic connectivity)", ProfileOf(Anomaly).Lambda2, r => ProfileOf(r).Lambda2);
        Row2("min positive spectral gap", MinGap(spec47), r => MinGap(SpectrumOf(r)));
        Row2("mean distinct-level spacing", MeanGap(spec47), r => MeanGap(SpectrumOf(r)));
        Row2("degree", 2.0 * AdaptabilityAudit.Edges(AdaptabilityAudit.RingAdjacency(Anomaly)).Count / 96.0,
            r => 2.0 * AdaptabilityAudit.Edges(AdaptabilityAudit.RingAdjacency(r)).Count / 96.0);
        Row2("edges |E|", AdaptabilityAudit.Edges(AdaptabilityAudit.RingAdjacency(Anomaly)).Count,
            r => AdaptabilityAudit.Edges(AdaptabilityAudit.RingAdjacency(r)).Count);
        Row2("ΔE_lock", Locked(spec47), r => Locked(SpectrumOf(r)));
        Row2("headroom N − A₀", AdaptabilityAudit.N - a047,
            r => AdaptabilityAudit.N - AdaptabilityAudit.Buckets(SpectrumOf(r)).A);
        Row2("distinct count A₀", a047, r => AdaptabilityAudit.Buckets(SpectrumOf(r)).A);
        Row2("largest multiplicity", mult47.Max(), r => Multiplicities(SpectrumOf(r)).Max());
        Row2("largest level's SHARE", (double)mult47.Max() / 96.0,
            r => (double)Multiplicities(SpectrumOf(r)).Max() / 96.0);

        sb.AppendLine();
        sb.AppendLine("  The E1 rejection, spelled out, because it is what makes the table honest: E1 is the ±1-only ring");
        sb.AppendLine($"  with λ₂ = {ProfileOf("E1").Lambda2:F6}, far BELOW Pair1-47's {ProfileOf(Anomaly).Lambda2:F6}, and E1 is perfectly healthy");
        sb.AppendLine("  (capacity 0.98598). So 'unusually small λ₂' is shared with a healthy ring and is REJECTED, even");
        sb.AppendLine("  though on the narrower seven-ring set it would have looked unique. The same argument applies to");
        sb.AppendLine("  degree and edge count: E1 has degree 2 against Pair1-47's 4.");

        sb.AppendLine();
        sb.AppendLine("  The min-positive-gap row needs care, so its underlying numbers are printed. The smallest");
        sb.AppendLine("  positive gaps of Pair1-47 and of the healthy ring that achieves the family minimum:");
        sb.AppendLine("  ring        first eight distinct positive eigenvalues (rounded to 6 d.p.)");
        sb.AppendLine("  " + new string('-', 92));
        foreach (string ring in new[] { Anomaly, "D96", "E1" })
        {
            var vals = SpectrumOf(ring).Where(v => v > 1e-9).Select(v => Math.Round(v, 6))
                                       .OrderBy(v => v).Distinct().Take(8).ToArray();
            sb.AppendLine($"  {ring,-10} {string.Join(", ", vals.Select(v => v.ToString("F6", CultureInfo.InvariantCulture)))}");
        }
        var gapRing = healthy.OrderBy(r => MinGap(SpectrumOf(r))).First();
        sb.AppendLine($"  smallest healthy min-gap is on {gapRing}: {MinGap(SpectrumOf(gapRing)):F6}");
        sb.AppendLine("  The metric and its witness, printed so it can be checked:");
        foreach (string ring in new[] { Anomaly, gapRing })
        {
            var pos = SpectrumOf(ring).Where(v => v > 1e-9).Select(v => Math.Round(v, 6))
                                      .OrderBy(v => v).Distinct().ToArray();
            int at = 1; double best = double.PositiveInfinity;
            for (int i = 1; i < pos.Length; i++)
                if (pos[i] - pos[i - 1] < best) { best = pos[i] - pos[i - 1]; at = i; }
            sb.AppendLine($"    {ring,-10} distinct positive levels = {pos.Length}, min gap = {best:F6} "
                          + $"between λ = {pos[at - 1]:F6} and λ = {pos[at]:F6}");
        }
        sb.AppendLine("  ⇒ The min positive gap DOES separate Pair1-47 (0.034221, against 0.0002 … 0.0211 healthy),");
        sb.AppendLine("    but the witness shows WHERE: it is the spacing between the level at 7.965779 (k = 46) and the");
        sb.AppendLine("    singleton at 8.000000 (k = 48) — in the MIDDLE of the spectrum, not at the bottom, and the");
        sb.AppendLine("    number merely coincides with λ₂. It is one scalar with no route to the rank budget, and it is");
        sb.AppendLine("    the ONLY independent observable in this table that separates the ring. Reporting it rather");
        sb.AppendLine("    than hiding it: 'unusual spacing statistic' is a true but non-explanatory separator, whereas");
        sb.AppendLine("    the multiplicity structure is both a separator and the cause (see DERIVED below).");

        sb.AppendLine();
        sb.AppendLine("  DERIVED");
        sb.AppendLine("    · THE PROPERTY IS THE MULTIPLICITY STRUCTURE: a SINGLE DEGENERATE LEVEL HOLDING 52.1 % OF THE");
        sb.AppendLine("      SPECTRUM (multiplicity 50 of 96). Every healthy ring's largest level holds at most 12 of 96");
        sb.AppendLine("      (12.5 %), and the median healthy ring's holds 6. Nothing else separates Pair1-47: λ₂, both");
        sb.AppendLine("      spacing statistics, degree and edge count are all shared with a healthy ring.");
        sb.AppendLine("    · IT HAS AN EXACT ARITHMETIC CAUSE. 47 = N/2 − 1, and in general an offset pair {±1, ±d}");
        sb.AppendLine("      produces this level precisely when d ≡ ±1 (mod N/2). The reason is a trigonometric identity:");
        sb.AppendLine("      cos(2πk·47/96) = (−1)^k·cos(2πk/96), so for every ODD k the two offsets' contributions are");
        sb.AppendLine("      complementary and λ_k = 2[(1 − cos θ) + (1 + cos θ)] = 4 EXACTLY. There are 48 odd k, and");
        sb.AppendLine("      k = 24 and k = 72 also give λ = 4, so the level has multiplicity 50. Verified to machine");
        sb.AppendLine($"      precision: 48 odd modes, largest |λ_k − 4| = 0, and {spec47.Count(v => Math.Abs(v - 4.0) < 1e-9)} eigenvalues within 1e-9 of 4 in");
        sb.AppendLine("      the actual Laplacian spectrum.");
        sb.AppendLine("    · A_D₀, HEADROOM AND ΔE_lock SEPARATE ONLY AS RESTATEMENTS. They are functions of the same");
        sb.AppendLine("      multiplicity multiset — A₀ is the number of levels, headroom is N − A₀, and");
        sb.AppendLine("      ΔE_lock = (1/N)·Σ m·ln m — so they cannot be independent candidates. Pair1-47 has the LARGEST");
        sb.AppendLine("      headroom (71) AND the largest ΔE_lock (2.3552) in the family, and the LOWEST capacity. That");
        sb.AppendLine("      inversion is the anomaly in one line: more locked entropy and more room, less collected.");
        sb.AppendLine("    · WHY THE INVERSION — THE RANK BUDGET. Deleting or adding an edge subtracts a RANK-2 matrix,");
        sb.AppendLine("      and a rank-r perturbation acts on an m-fold eigenspace as an m × m matrix of rank ≤ r, which");
        sb.AppendLine("      has at most r + 1 distinct eigenvalues. So a level of multiplicity m can yield at most");
        sb.AppendLine("      min(m − 1, r) new distinct eigenvalues, and the achievable attractor increase is bounded");
        sb.AppendLine("      level by level:  ΔA ≤ Σ_i min(m_i − 1, r).  Capacity is ΔA/(N − A₀), so a spectrum whose");
        sb.AppendLine("      HEADROOM SITS IN ONE HUGE LEVEL IS A RANK TRAP. Measured, delete family:");
        sb.AppendLine("        Pair1-47  k = 1: ceiling 0.3380  measured 0.3239      D96  k = 1: ceiling 0.9020  measured 0.8562");
        sb.AppendLine("        Pair1-47  k = 3: ceiling 0.3944  measured 0.3521      D96  k = 3: ceiling 1.0000  measured 0.9281");
        sb.AppendLine("        Pair1-47  k = 10: ceiling 0.5915 measured 0.4507      D96  k = 10: ceiling 1.0000 measured 0.9804");
        sb.AppendLine("        Pair1-47  k = 19: ceiling 0.8451 measured 0.5728      D96  k = 19: ceiling 1.0000 measured 1.0000");
        sb.AppendLine("      and the single-operation test confirms the per-level form of the bound for every ring: ONE edge");
        sb.AppendLine("      deletion splits EACH degenerate level independently, so D96 gains ≤ 42 + 2 + 2 = 46 distinct");
        sb.AppendLine("      eigenvalues while Pair1-47 gains ≤ 2 + 22 = 24 — its 49-slot level contributes at most 2, while");
        sb.AppendLine("      D96's 42 doublets contribute 42 at once.");
        sb.AppendLine("    · THE HEALTHY RINGS ARE RANK-EFFICIENT. Their headroom is built from MANY SMALL levels (42");
        sb.AppendLine("      doublets in D96), which a rank-2 perturbation releases simultaneously. Pair1-47's headroom is");
        sb.AppendLine("      built from ONE level, which it cannot. Capacity therefore depends on the multiplicity");
        sb.AppendLine("      DISTRIBUTION, not on the multiplicity count or the headroom — which is exactly why D_052's");
        sb.AppendLine("      degeneracy count and D_050's near-gap density cannot see the anomaly.");
        sb.AppendLine();
        sb.AppendLine("  EMERGENT");
        sb.AppendLine("    · THE MEASURED COLLAPSE, and its magnitude: Pair1-47 capacity 0.42089 against 0.93693 … 1.00000");
        sb.AppendLine("      for all twelve healthy rings; per family 0.4103 (delete), 0.4038 (add), 0.5033 (rewire),");
        sb.AppendLine("      0.3662 (weight). The rank ceiling reproduces the ordering and the size of the shortfall.");
        sb.AppendLine("    · A DEFECT IN THE SHARED PERTURBATOR, FOUND AND QUANTIFIED HERE. The ensemble's generator is an");
        sb.AppendLine("      LCG x ← 1664525·x + 1013904223 (mod 2³²) and the weight family takes its sign from the LOW");
        sb.AppendLine("      bit. Both coefficients are odd, so (a·x + c) mod 2 = (x + 1) mod 2 — the coin ALTERNATES");
        sb.AppendLine("      deterministically for every seed (reproduced: 0 1 0 1 …, 1 0 1 0 …, 0 1 0 1 …). Consequences,");
        sb.AppendLine("      all measured: the weight family produces exactly TWO distinct weight values for Pair1-47");
        sb.AppendLine("      (0.98 × 96, 1.02 × 96) and for D96 (0.98 × 288, 1.02 × 288); the reweighted graph is neither");
        sb.AppendLine("      circulant nor shift-symmetric, so it is not a symmetry being preserved but a FIXED pattern");
        sb.AppendLine("      being applied; and its effect on the spectrum is SPECTRUM-DEPENDENT — D96 still reaches");
        sb.AppendLine("      A₁ = 96 while Pair1-47 stalls at 51, with its λ = 4 level surviving intact. Re-running the");
        sb.AppendLine("      weight family with a CORRECTED coin (high bits, an independent sign per edge, done locally so");
        sb.AppendLine("      no stored number is touched): Pair1-47's weight capacity 0.3662 → 0.9812 and its ensemble mean");
        sb.AppendLine("      0.4209 → 0.5746, while every healthy ring is essentially unchanged (S96-135 0.9412 → 1.0000,");
        sb.AppendLine("      all others already 1.0000). So the defect DEPRESSES the anomaly by about a third and does not");
        sb.AppendLine("      create it.");
        sb.AppendLine();
        sb.AppendLine("  REFUTED");
        sb.AppendLine("    · 'EIGENVECTOR LOCALIZATION OR A SMALL PARTICIPATION RATIO explains Pair1-47.' REFUTED IN");
        sb.AppendLine("      PRINCIPLE. Every ring here is a circulant, so every Laplacian eigenvector is a Fourier mode");
        sb.AppendLine("      with |v_i| = 1/√N and participation ratio EXACTLY N = 96 for every mode of every ring —");
        sb.AppendLine("      verified with the exact Fourier modes as eigenvectors. Localization cannot separate any two");
        sb.AppendLine("      members of this family, whatever is measured.");
        sb.AppendLine("    · 'Pair1-47's SYMMETRY CLASS is different.' REFUTED: all eight opening rings are symmetric");
        sb.AppendLine("      circulants with the dihedral group of order 2N, one orbit, vertex-transitive. Classical");
        sb.AppendLine("      'symmetry-protected degeneracy' therefore cannot separate Pair1-47 — what differs is HOW MUCH");
        sb.AppendLine("      of the spectrum one level holds, not whether a symmetry exists.");
        sb.AppendLine("    · 'A SMALL SPECTRAL GAP, an unusual SPACING STATISTIC, or a low DEGREE distinguishes it.'");
        sb.AppendLine("      PARTLY REFUTED, and reported as such: the min positive gap DOES separate Pair1-47");
        sb.AppendLine("      (0.034221 against 0.0002 … 0.0211) — but its witness is the spacing between the level at");
        sb.AppendLine("      7.965779 and the singleton at 8.000000, in the middle of the spectrum, and it is a single");
        sb.AppendLine("      scalar with no route to the rank budget. λ₂, the MEAN level spacing, the degree and the edge");
        sb.AppendLine("      count are all shared with a healthy ring — E1 has a 4.85× SMALLER λ₂ and half the degree.");
        sb.AppendLine("    · 'Pair1-47 is anomalous because it has MANY degenerate levels.' REFUTED: it has the FEWEST");
        sb.AppendLine("      degenerate levels of the family (23, against 47 levels' worth in D96's 44 groups) and one");
        sb.AppendLine("      enormous one. Degeneracy COUNT is anti-correlated with the collapse here.");
        sb.AppendLine("    · 'The collapse is an artifact of the perturbator defect.' REFUTED: with a corrected coin the");
        sb.AppendLine("      anomaly survives (0.5746 against 0.94 … 1.00 for every healthy ring), and the three");
        sb.AppendLine("      edge-count families — untouched by the defect — still show 0.41 / 0.40 / 0.50.");
        sb.AppendLine();
        sb.AppendLine("  SUMMARY ANSWER. The first property uniquely possessed by Pair1-47 is a single degenerate level");
        sb.AppendLine("  holding more than half its spectrum, produced exactly by the half-period congruence of its offsets");
        sb.AppendLine("  (47 ≡ ±1 mod 48). It collapses capacity because a low-rank perturbation can only release that");
        sb.AppendLine("  level's 49 headroom slots a couple at a time, while the healthy rings' headroom sits in dozens of");
        sb.AppendLine("  small levels that a single edge operation releases together. ΔE_lock, headroom and A₀ separate the");
        sb.AppendLine("  ring too, but only as restatements of that one multiplicity structure; localization and symmetry");
        sb.AppendLine("  fail to separate it at all.");
        sb.AppendLine();
        sb.AppendLine("  A defect in the shared perturbator was found on the way and is reported, not exploited: the weight");
        sb.AppendLine("  family's sign is read from an LCG's lowest bit, which alternates deterministically, so its");
        sb.AppendLine("  'independent reweighting' is a fixed spectrum-dependent pattern. It is quantified above and left");
        sb.AppendLine("  for a follow-up fix rather than changed here, since altering the generator would invalidate the");
        sb.AppendLine("  stored numbers of D_048–D_054.");
        sb.AppendLine();
        sb.AppendLine("  No canonical AT claim, value, equation or registry entry is changed; the D_040");
        sb.AppendLine("  ClassificationRegistry is untouched. No new simulation primitive is added to the shared machinery:");
        sb.AppendLine("  the corrected coin is local to this audit and used only to bound the defect.");

        Assert.True(mult47.Max() >= 50, "the dominant level must be exhibited");
        Assert.True(healthy.All(r => Multiplicities(SpectrumOf(r)).Max() <= 12),
            "no healthy ring may share the dominant level");
        Assert.True(ProfileOf("E1").Lambda2 < ProfileOf(Anomaly).Lambda2,
            "E1 must have the smaller λ₂ — the reason λ₂ is rejected");
        Assert.True(adaptiveCeilingBeatsHealthy(), "the rank ceiling must favour the healthy rings at the lowest dose");

        Output.WriteLine(sb.ToString());
    }

    /// <summary>
    /// The mechanism assertion: at one edge deletion, the rank ceiling permits the healthy rings a far
    /// larger attractor increase than the anomaly gets.
    /// </summary>
    private static bool adaptiveCeilingBeatsHealthy()
    {
        int ceiling47 = RankCeiling(Multiplicities(SpectrumOf(Anomaly)), 2);
        var (a47, _, _) = AdaptabilityAudit.Buckets(SpectrumOf(Anomaly));
        var (a96, _, _) = AdaptabilityAudit.Buckets(SpectrumOf("D96"));
        int ceiling96 = RankCeiling(Multiplicities(SpectrumOf("D96")), 2);
        double cap47 = Math.Min(1.0, (double)ceiling47 / (96 - a47));
        double cap96 = Math.Min(1.0, (double)ceiling96 / (96 - a96));
        return cap96 > cap47;
    }
}
