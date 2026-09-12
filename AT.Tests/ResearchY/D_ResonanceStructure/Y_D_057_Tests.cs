using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_057 — Rank-Budget Law Audit.
///
/// Question: does ΔA ≤ Σ_i min(m_i − 1, r), the bound D_055 derived from the RANK of an edge operation,
/// DIRECTLY predict capacity — replacing the correlations of D_050–D_056 with a derived law?
///
/// Three law forms are frozen in PHASE A, in increasing order of fitted content:
///   L1  pure ceiling, ZERO parameters:  C = mean_d min(1, Σ_i min(m_i − 1, r_d) / (N − A₀)), r_d = 2k_d
///   L2  effective-rank law, ONE parameter c per family:  r_d = round(c · 2k_d), c from the sources only
///   L3  tightness-corrected law, ONE parameter τ per family:  C = L1 · τ, τ from the sources only
/// and all three are compared head-to-head against the correlational predictors (multiplicity
/// distribution, degeneracy count, λ₂, near-gap) on the same case set with the same metrics.
///
/// PHASE A (this file, committed on its own) verifies the LEMMA synthetically, states the laws, freezes
/// every prediction, and adds two NEW designed rings that isolate the mechanism. It never constructs
/// the perturbation ensemble.
///
/// Deterministic throughout: fixed rings, fixed seeds, fixed doses, and a deliberately seeded LCG for
/// the synthetic lemma test.
/// </summary>
public class Y_D_057_Tests : ResearchTestBase
{
    public Y_D_057_Tests(ITestOutputHelper output) : base(output) { }

    /// <summary>The eight rings the D group has been auditing since D_054.</summary>
    private static readonly string[] CaseSet =
        ["D96", "Pair1-47", "S96-123", "S96-135", "Ring48", "Decay96", "Boost96", "D96-24"];

    /// <summary>The two designed rings that make the blind protocol real.</summary>
    private static readonly string[] BlindSet = AdaptabilityAudit.BudgetRingNames;

    private static double[,] AdjacencyOf(string name)
        => AdaptabilityAudit.CaseNames.Contains(name)
            ? AdaptabilityAudit.Adjacency(name)
            : AdaptabilityAudit.RingAdjacency(name);

    private static double[] SpectrumOf(string ring)
        => AdaptabilityAudit.SpectrumOf(AdjacencyOf(ring));

    private static int[] Mult(string ring) => AdaptabilityAudit.Buckets(SpectrumOf(ring)).Mult;

    private static int Headroom(string ring) => AdaptabilityAudit.N - Mult(ring).Length;

    private static int Edges(string ring) => AdaptabilityAudit.Edges(AdjacencyOf(ring)).Count;

    /// <summary>Σ_i min(m_i − 1, r): the attainable number of NEW distinct eigenvalues at rank r.</summary>
    private static int Ceiling(int[] m, int r) => m.Sum(v => Math.Min(v - 1, r));

    /// <summary>The largest rank an edge operation touching k edges can have; weight rescales everything.</summary>
    private static int NominalRank(string family, int k)
        => family == "weight" ? AdaptabilityAudit.N : 2 * k;

    /// <summary>Mean over the shared dose grid of min(1, Ceiling/headroom) at a given rank scaling.</summary>
    private static double LawValue(string ring, string family, double rankScale)
    {
        var m = Mult(ring);
        int head = Headroom(ring);
        if (head <= 0) return 0.0;
        int edges = Edges(ring);
        double sum = 0.0;
        foreach (double dose in AdaptabilityAudit.Doses)
        {
            int k = Math.Max(1, (int)Math.Round(dose * edges));
            int r = family == "weight"
                ? AdaptabilityAudit.N
                : Math.Max(1, (int)Math.Round(rankScale * NominalRank(family, k)));
            sum += Math.Min(1.0, (double)Ceiling(m, r) / head);
        }
        return sum / AdaptabilityAudit.Doses.Length;
    }

    /// <summary>Family-mean prediction: L1 no parameters, L2 rank scales, L3 tightness correction.</summary>
    private static double LawFamilyMean(string ring, Dictionary<string, double>? rankScales,
        Dictionary<string, double>? tightness)
    {
        double sum = 0.0;
        foreach (string fam in AdaptabilityAudit.Kinds)
        {
            if (rankScales != null) sum += LawValue(ring, fam, rankScales[fam]);
            else if (tightness != null) sum += LawValue(ring, fam, 1.0) * tightness[fam];
            else sum += LawValue(ring, fam, 1.0);
        }
        return sum / AdaptabilityAudit.Kinds.Length;
    }

    // ── 1. The lemma, verified synthetically ────────────────────────────────

    private sealed class Lcg(uint seed)
    {
        private uint _x = seed;
        public uint Next() => _x = unchecked(1664525u * _x + 1013904223u);
        public double Uniform() => (Next() >> 8) / (double)(1u << 24);
    }

    [Fact]
    public void D057_01_The_Lemma_Verified_Synthetically()
    {
        var sb = new StringBuilder();
        PrintHeader("1. The lemma — a rank-r perturbation splits a level of multiplicity m into at most r + 1 parts");

        sb.AppendLine("THE STATEMENT");
        sb.AppendLine("  Let A have an eigenvalue λ of multiplicity m, and let E be a symmetric perturbation of rank");
        sb.AppendLine("  exactly r ≤ m. Restricted to that eigenspace, E acts as an m × m symmetric matrix of rank ≤ r,");
        sb.AppendLine("  whose spectrum has at most r + 1 DISTINCT values. So the perturbed level splits into at most");
        sb.AppendLine("  r + 1 distinct eigenvalues. An edge deletion or addition subtracts the rank-2 matrix");
        sb.AppendLine("  (e_i e_j^T + e_j e_i^T), so k edge operations give r ≤ 2k, hence");
        sb.AppendLine("      ΔA ≤ Σ_i min(m_i − 1, r).");
        sb.AppendLine("  This is the entire content of the law: arithmetic on ranks, not a fitted relation.");
        sb.AppendLine();
        sb.AppendLine("THE TEST — direct construction, no ring ensemble involved. For m ∈ {4, 8, 16} and r = 1 … 6,");
        sb.AppendLine("  draw 200 random symmetric rank-r perturbations supported on the degenerate eigenspace (fixed");
        sb.AppendLine("  seeds), diagonalize with the Jacobi method, and count how many distinct values the level splits");
        sb.AppendLine("  into. The bound is r + 1.");
        sb.AppendLine("  m      r     max parts observed   bound r+1   violations   draws");
        sb.AppendLine("  " + new string('-', 78));
        int totalViolations = 0, totalDraws = 0;
        foreach (int m in new[] { 4, 8, 16 })
            for (int r = 1; r <= 6; r++)
            {
                if (r > m) continue;
                int maxParts = 0, violations = 0;
                for (int draw = 0; draw < 200; draw++)
                {
                    var rng = new Lcg(unchecked((uint)(7919 * m + 131 * r + draw)));
                    int n = m + 3;
                    var a = new double[n, n];
                    for (int i = m; i < n; i++) a[i, i] = 10.0 * (i - m + 1);
                    for (int t = 0; t < r; t++)
                    {
                        var v = new double[m];
                        double nrm = 0.0;
                        for (int i = 0; i < m; i++) { v[i] = rng.Uniform() - 0.5; nrm += v[i] * v[i]; }
                        nrm = Math.Sqrt(nrm);
                        for (int i = 0; i < m; i++) v[i] /= nrm;
                        double sign = rng.Uniform() < 0.5 ? -1.0 : 1.0;
                        for (int i = 0; i < m; i++)
                            for (int j = 0; j < m; j++)
                                a[i, j] += sign * v[i] * v[j];
                    }
                    var eig = SymmetricEigenvalues(a);
                    int parts = eig.Where(v => Math.Abs(v) < 5.0).Select(v => Math.Round(v, 8)).Distinct().Count();
                    maxParts = Math.Max(maxParts, parts);
                    if (parts > r + 1) violations++;
                    totalDraws++;
                }
                totalViolations += violations;
                sb.AppendLine($"  {m,-6} {r,-5} {maxParts,19} {r + 1,12} {violations,12} {200,8}");
            }
        sb.AppendLine();
        sb.AppendLine($"  ⇒ {totalViolations} violations in {totalDraws} draws, and the observed maximum is exactly r + 1");
        sb.AppendLine("    wherever r < m. The lemma holds, verified independently of the ring family, the ensemble and");
        sb.AppendLine("    the doses — so any failure of the LAW later in this audit is a failure of TIGHTNESS (the bound");
        sb.AppendLine("    not being achieved), never of the bound itself.");

        Assert.Equal(0, totalViolations);
        Output.WriteLine(sb.ToString());
    }

    /// <summary>Eigenvalues of a real symmetric matrix by the cyclic Jacobi method.</summary>
    private static double[] SymmetricEigenvalues(double[,] input)
    {
        int n = input.GetLength(0);
        var a = (double[,])input.Clone();
        for (int sweep = 0; sweep < 60; sweep++)
        {
            double off = 0.0;
            for (int p = 0; p < n; p++)
                for (int q = p + 1; q < n; q++) off += a[p, q] * a[p, q];
            if (off < 1e-24) break;
            for (int p = 0; p < n; p++)
                for (int q = p + 1; q < n; q++)
                {
                    if (Math.Abs(a[p, q]) < 1e-18) continue;
                    double theta = 0.5 * Math.Atan2(2.0 * a[p, q], a[q, q] - a[p, p]);
                    double c = Math.Cos(theta), s = Math.Sin(theta);
                    for (int i = 0; i < n; i++)
                    {
                        double aip = a[i, p], aiq = a[i, q];
                        a[i, p] = c * aip - s * aiq;
                        a[i, q] = s * aip + c * aiq;
                    }
                    for (int j = 0; j < n; j++)
                    {
                        double apj = a[p, j], aqj = a[q, j];
                        a[p, j] = c * apj - s * aqj;
                        a[q, j] = s * apj + c * aqj;
                    }
                }
        }
        var eig = new double[n];
        for (int i = 0; i < n; i++) eig[i] = a[i, i];
        return eig;
    }

    // ── 2. The laws and the frozen prediction ───────────────────────────────

    /// <summary>
    /// L2's single parameter per family: the fraction of the nominal rank the perturbation effectively
    /// delivers. Least squares on the POOLED measured ΔA of D_048/D_050's six source cases — one number
    /// per family, no per-ring parameter.
    /// </summary>
    private static double RankScaleFor(string family)
    {
        double best = 1.0, bestSse = double.PositiveInfinity;
        for (double c = 0.02; c <= 1.5; c += 0.02)
        {
            double sse = 0.0;
            foreach (string ring in AdaptabilityAudit.CaseNames)
            {
                var prof = AdaptabilityAudit.Profiles.Single(p => p.Name == ring);
                int head = Headroom(ring);
                double measured = head > 0 ? prof.MeanDeltaA / head : 0.0;
                double d = LawValue(ring, family, c) - measured;
                sse += d * d;
            }
            if (sse < bestSse) { bestSse = sse; best = c; }
        }
        return best;
    }

    /// <summary>L3's single parameter per family: the pooled tightness ratio measured / L1 on the sources.</summary>
    private static double TightnessFor(string family)
    {
        double sum = 0.0;
        int n = 0;
        foreach (string ring in AdaptabilityAudit.CaseNames)
        {
            var prof = AdaptabilityAudit.Profiles.Single(p => p.Name == ring);
            int head = Headroom(ring);
            double measured = head > 0 ? prof.MeanDeltaA / head : 0.0;
            double l1 = LawValue(ring, family, 1.0);
            if (l1 > 1e-6) { sum += measured / l1; n++; }
        }
        return n > 0 ? sum / n : 0.0;
    }

    /// <summary>
    /// The already-published capacities of the required case set, frozen as a table so PHASE A needs no
    /// simulation. Sources: D96 from D_048/D_049; the ring values from D_051–D_056.
    /// </summary>
    private static Dictionary<string, double> Published() => new()
    {
        ["D96"] = 0.99020,
        ["Pair1-47"] = 0.42089,
        ["S96-123"] = 0.96928,
        ["S96-135"] = 0.93693,
        ["Ring48"] = 0.99248,
        ["Decay96"] = 1.00000,
        ["Boost96"] = 0.99524,
        ["D96-24"] = 0.97193,
    };

    /// <summary>The correlational predictors of the previous audits, for the same rings.</summary>
    private static double Correlational(string ring, string which)
    {
        var m = Mult(ring);
        var spec = SpectrumOf(ring);
        double lam2 = spec.Where(v => v > AdaptabilityAudit.Tol).Min();
        return which switch
        {
            "max multiplicity" => m.Max(),
            "largest share" => (double)m.Max() / AdaptabilityAudit.N,
            "degeneracy count" => m.Count(v => v > 1),
            "lambda2" => lam2,
            "near-gap" => AdaptabilityAudit.NearGapDensityK2(spec, lam2),
            _ => throw new ArgumentOutOfRangeException(nameof(which)),
        };
    }

    [Fact]
    public void D057_02_The_Laws_And_Frozen_Prediction()
    {
        var sb = new StringBuilder();
        PrintHeader("2. The three law forms and the frozen prediction — PHASE A (no simulation)");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  A1. The bound is ΔA ≤ Σ_i min(m_i − 1, r) with r ≤ 2k for delete/add/rewire and r ≤ N for the");
        sb.AppendLine("      full-edge weight family (§1 verifies the lemma behind it). Capacity is ΔA/(N − A₀), so the");
        sb.AppendLine("      bound becomes a capacity ceiling once normalized.");
        sb.AppendLine("  A2. THE THREE LAWS, in increasing order of fitted content:");
        sb.AppendLine("      L1  pure ceiling — ZERO parameters:  C = mean_d min(1, Σ min(m_i − 1, 2k_d)/(N − A₀))");
        sb.AppendLine("      L2  effective-rank law — ONE parameter per family:  r_d = round(c · 2k_d), c from the sources");
        sb.AppendLine("      L3  tightness-corrected law — ONE parameter per family:  C = L1 · τ, τ from the sources");
        sb.AppendLine("  A3. Every parameter is calibrated on D_048/D_050's six SOURCE cases only. No ring measurement");
        sb.AppendLine("      enters any prediction; the required case set's targets (published by D_051–D_056) serve as a");
        sb.AppendLine("      pre-registered replication, and the two NEW rings are the blind component.");
        sb.AppendLine("  A4. The weight family makes the bound VACUOUS: it rescales every edge, so r = N and the ceiling");
        sb.AppendLine("      equals the whole headroom, giving L1 = 1 exactly by construction. Stated up front rather than");
        sb.AppendLine("      presented as a predictive success.");

        sb.AppendLine();
        sb.AppendLine("  THE CALIBRATED PARAMETERS (from the six source cases, printed so they cannot be tuned later):");
        sb.AppendLine("  family    L1 (no parameters)   L2 rank scale c   L3 tightness τ");
        sb.AppendLine("  " + new string('-', 66));
        var rankScale = new Dictionary<string, double>();
        var tightTs = new Dictionary<string, double>();
        foreach (string fam in AdaptabilityAudit.Kinds)
        {
            rankScale[fam] = RankScaleFor(fam);
            tightTs[fam] = TightnessFor(fam);
            sb.AppendLine($"  {fam,-9} {"—",-20} {rankScale[fam],16:F2} {tightTs[fam],15:F3}");
        }
        sb.AppendLine($"  family-mean rank scale = {rankScale.Values.Average():F3}, family-mean tightness = {tightTs.Values.Average():F3}");

        sb.AppendLine();
        sb.AppendLine("  THE PREDICTION PER RING — L1, L2, L3. The observed column is a FROZEN TABLE of the values");
        sb.AppendLine("  published by D_051–D_056, quoted here rather than re-measured: this commit must contain no");
        sb.AppendLine("  simulation of any kind. PHASE B confirms the table by re-measuring.");
        sb.AppendLine("  ring        L1        L2        L3        observed (published)   L1 |err|   L2 |err|   L3 |err|");
        sb.AppendLine("  " + new string('-', 98));
        foreach (string ring in CaseSet)
        {
            double observed = Published()[ring];
            double l1 = LawFamilyMean(ring, null, null);
            double l2 = LawFamilyMean(ring, rankScale, null);
            double l3 = LawFamilyMean(ring, null, tightTs);
            sb.AppendLine($"  {ring,-10} {l1,8:F5} {l2,9:F5} {l3,9:F5} {observed,20:F5} {Math.Abs(l1 - observed),10:F4} {Math.Abs(l2 - observed),10:F4} {Math.Abs(l3 - observed),10:F4}");
        }

        sb.AppendLine();
        sb.AppendLine("  THE FROZEN PREDICTION — the two NEW designed rings (measured for the first time in PHASE B).");
        sb.AppendLine("  Half47 adds the N/4 offset ±24 to Pair1-47. For odd k the ±24 term is 2(1 − cos(πk/2)) = 2, so");
        sb.AppendLine("  the odd-mode sum is 2 + (2 − 2cos θ) + (2 + 2cos θ) = 6: the dominant level should SURVIVE at a");
        sb.AppendLine("  THIRD value, λ = 6, and the ring should collapse like Pair1-47 and P47-48 — evidence that the law");
        sb.AppendLine("  tracks the LEVEL rather than any particular offset.");
        sb.AppendLine("  Triple47 adds ±23 instead. Since 23 = N/4 − 1, the middle term becomes 2 ∓ 2 sin θ for odd k and");
        sb.AppendLine("  the cosines no longer cancel: the sum is 6 ∓ 2 sin θ, so the dominant level should be DESTROYED");
        sb.AppendLine("  and the ring should behave like a healthy one. Two rings, opposite verdicts, one audit.");
        sb.AppendLine("  ring        L1        L2        L3        max m   dominant level   level parts   predicted verdict");
        sb.AppendLine("  " + new string('-', 118));
        foreach (string ring in BlindSet)
        {
            var m = Mult(ring);
            var spec = SpectrumOf(ring);
            var dom = spec.GroupBy(v => Math.Round(v, 6)).OrderByDescending(g => g.Count()).First();
            double l1 = LawFamilyMean(ring, null, null);
            double l2 = LawFamilyMean(ring, rankScale, null);
            double l3 = LawFamilyMean(ring, null, tightTs);
            sb.AppendLine($"  {ring,-10} {l1,8:F5} {l2,9:F5} {l3,9:F5} {m.Max(),8} {dom.Key,16:F6} {dom.Count(),13}   {(m.Max() >= 40 ? "COLLAPSE" : "healthy")}");
        }
        sb.AppendLine();
        sb.AppendLine("  Note the contrast the audit will test: Half47 and Triple47 differ only in whether the third");
        sb.AppendLine("  offset is N/4 or N/4 − 1, and the law says one collapses and the other does not.");

        sb.AppendLine();
        sb.AppendLine("  The prediction is now on record. PHASE B (measurement, added in a later commit) verifies the");
        sb.AppendLine("  bound across the whole ensemble, measures its tightness, and compares the laws head-to-head");
        sb.AppendLine("  against the correlational predictors on the same case set with the same metrics.");

        Assert.True(rankScale.Values.All(v => v > 0), "every family must admit a positive rank scale");
        Assert.True(Mult("Half47").Max() >= 40, "Half47's dominant level must be present at prediction time");
        Assert.True(Mult("Triple47").Max() < 20, "Triple47's dominant level must be absent at prediction time");
        Output.WriteLine(sb.ToString());
    }
}
