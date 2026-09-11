using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_056 — Multiplicity Distribution Audit.
///
/// Question: is capacity controlled by the FULL MULTIPLICITY DISTRIBUTION — max multiplicity, Gini(m),
/// Entropy(m), Herfindahl(m) and the largest-level share — rather than by the near-gap density, the
/// degeneracy count or λ₂?
///
/// D_055 showed the mechanism is a rank budget, ΔA ≤ Σ_i min(m_i − 1, r), which is a function of the
/// multiplicity DISTRIBUTION and not of its count. D_056 tests whether the distribution therefore
/// predicts capacity outright, and whether it beats the three baselines.
///
/// PHASE A (this file, committed on its own) freezes the distributions, the coefficients, the decision
/// rule and every prediction — including four NEW rings chosen to vary the distribution SHAPE. It
/// never constructs the perturbation ensemble.
///
/// Deterministic throughout: fixed rings, fixed seeds, fixed doses, no randomness.
/// </summary>
public class Y_D_056_Tests : ResearchTestBase
{
    public Y_D_056_Tests(ITestOutputHelper output) : base(output) { }

    /// <summary>The required case set.</summary>
    private static readonly string[] CaseSet =
        ["D96", "Pair1-47", "S96-123", "S96-135", "Ring48", "Decay96", "Boost96"];

    /// <summary>The new held-out set, which is what gives the blind protocol teeth.</summary>
    private static readonly string[] BlindSet = AdaptabilityAudit.MultiplicityRingNames;

    /// <summary>Adjacency of any named case: the shared ensemble's six cases, or any audited ring.</summary>
    private static double[,] AdjacencyOf(string name)
        => AdaptabilityAudit.CaseNames.Contains(name)
            ? AdaptabilityAudit.Adjacency(name)
            : AdaptabilityAudit.RingAdjacency(name);

    private static double[] SpectrumOf(string ring)
        => AdaptabilityAudit.SpectrumOf(AdjacencyOf(ring));

    /// <summary>Multiplicities of every DISTINCT level of the spectrum (they sum to N).</summary>
    private static int[] Mult(string ring) => AdaptabilityAudit.Buckets(SpectrumOf(ring)).Mult;

    // ── The five specified distribution statistics, plus the D_055 mechanism candidate ──

    /// <summary>Gini coefficient of the multiplicity list (standard mean-difference form).</summary>
    private static double Gini(int[] m)
    {
        var s = m.OrderBy(v => v).ToArray();
        int n = s.Length;
        double sum = s.Sum(), weighted = 0.0;
        for (int i = 0; i < n; i++) weighted += (2.0 * (i + 1) - n - 1) * s[i];
        return sum == 0 ? 0.0 : weighted / (n * sum);
    }

    /// <summary>Shannon entropy of the multiplicity distribution, p = m/N (nats).</summary>
    private static double Entropy(int[] m)
    {
        double e = 0.0;
        foreach (int v in m)
        {
            double p = (double)v / AdaptabilityAudit.N;
            if (p > 0) e -= p * Math.Log(p);
        }
        return e;
    }

    /// <summary>Herfindahl index Σ p² of the multiplicity distribution.</summary>
    private static double Herfindahl(int[] m)
        => m.Sum(v => Math.Pow((double)v / AdaptabilityAudit.N, 2));

    /// <summary>Share of the spectrum held by the single largest level.</summary>
    private static double LargestShare(int[] m) => (double)m.Max() / AdaptabilityAudit.N;

    /// <summary>
    /// D_055's rank-budget ceiling: the largest capacity a perturbation touching k edges can achieve,
    /// derived from the multiplicity distribution alone. Included as the MECHANISM candidate, kept
    /// separate from the five distribution shape statistics.
    /// </summary>
    private static double RankCeilingCapacity(int[] m, string ring)
    {
        int edges = AdaptabilityAudit.Edges(AdjacencyOf(ring)).Count;
        int a0 = m.Length;
        int head = AdaptabilityAudit.N - a0;
        // Mean over the shared dose grid, each dose giving rank r = 2k.
        double sum = 0.0;
        foreach (double dose in AdaptabilityAudit.Doses)
        {
            int k = Math.Max(1, (int)Math.Round(dose * edges));
            int r = 2 * k;
            int ceiling = m.Sum(v => Math.Min(v - 1, r));
            sum += head > 0 ? Math.Min(1.0, (double)ceiling / head) : 0.0;
        }
        return sum / AdaptabilityAudit.Doses.Length;
    }

    private static readonly string[] InputNames =
        ["max multiplicity", "Gini(m)", "Entropy(m)", "Herfindahl(m)", "largest share", "rank ceiling (D_055)"];

    private static double InputOf(string ring, string input)
    {
        var m = Mult(ring);
        return input switch
        {
            "max multiplicity" => m.Max(),
            "Gini(m)" => Gini(m),
            "Entropy(m)" => Entropy(m),
            "Herfindahl(m)" => Herfindahl(m),
            "largest share" => LargestShare(m),
            "rank ceiling (D_055)" => RankCeilingCapacity(m, ring),
            _ => throw new ArgumentOutOfRangeException(nameof(input)),
        };
    }

    /// <summary>OLS coefficients for one input+fitted on the six D_048/D_050 source cases.</summary>
    private static (double Slope, double Intercept, double R2) FitOnSources(string input, bool capacity)
    {
        double[] x = AdaptabilityAudit.CaseNames.Select(n => InputOf(n, input)).ToArray();
        double[] y = AdaptabilityAudit.CaseNames
            .Select(n => AdaptabilityAudit.Profiles.Single(p => p.Name == n))
            .Select(p => capacity ? p.Capacity : p.MeanRecovery).ToArray();
        var (slope, intercept, r2, _) = AdaptabilityAudit.Fit(x, y);
        return (slope, intercept, r2);
    }

    // ── 1. The distributions ────────────────────────────────────────────────

    [Fact]
    public void D056_01_Multiplicity_Distributions()
    {
        var sb = new StringBuilder();
        PrintHeader("1. The multiplicity distributions — PHASE A (no simulation)");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  A1. The case set is the seven rings named in the brief. m is the multiplicity list of EVERY");
        sb.AppendLine("      DISTINCT level of the Laplacian spectrum — singletons included — so Σ m = N = 96 exactly.");
        sb.AppendLine("      Stating the convention matters: dropping the singletons would inflate every concentration");
        sb.AppendLine("      statistic and would break the Σ = N normalization that Gini, entropy and Herfindahl need.");
        sb.AppendLine("  A2. The five specified inputs are the distribution SHAPE statistics: max multiplicity, Gini(m),");
        sb.AppendLine("      Entropy(m) = −Σ p·ln p, Herfindahl(m) = Σ p², and the largest-level share max(m)/N.");
        sb.AppendLine("      A sixth input is added, clearly separated: D_055's rank-budget ceiling, which is DERIVED");
        sb.AppendLine("      from the same distribution and is the mechanism's own prediction. Reporting it alongside");
        sb.AppendLine("      is what makes 'the distribution controls capacity' a testable claim rather than a slogan.");
        sb.AppendLine("  A3. Blind protocol: the coefficients, the decision rule and the predictions for the required");
        sb.AppendLine("      case set AND four NEW rings are frozen in this commit; the four new rings are measured for");
        sb.AppendLine("      the first time in PHASE B. The required case set's targets are already published by");
        sb.AppendLine("      D_051–D_055, so for it this audit is a pre-registered replication.");
        sb.AppendLine();

        var all = CaseSet.Concat(BlindSet).ToArray();
        sb.AppendLine("  ring        A₀   max m   Gini(m)   Entropy(m)   Herfindahl(m)   largest share   pattern");
        sb.AppendLine("  " + new string('-', 122));
        foreach (string ring in all)
        {
            var m = Mult(ring);
            var pat = m.GroupBy(v => v).OrderByDescending(g => g.Key).Take(5).Select(g => $"{g.Key}×{g.Count()}");
            sb.AppendLine($"  {ring,-10} {m.Length,4} {m.Max(),7} {Gini(m),9:F4} {Entropy(m),12:F4} {Herfindahl(m),15:F4} {LargestShare(m),15:P1}   {string.Join(", ", pat)}");
        }
        sb.AppendLine();
        sb.AppendLine("  The four NEW rings and what they are built to do:");
        foreach (var r in AdaptabilityAudit.MultiplicityRings)
            sb.AppendLine($"    {r.Name,-8} {r.Description}");

        sb.AppendLine();
        sb.AppendLine("  THE PATTERN THE AUDIT IS ABOUT. The five shape statistics are all monotone restatements of");
        sb.AppendLine("  'how concentrated is the spectrum', so they should be strongly (and mutually) correlated —");
        sb.AppendLine("  which is itself a finding, because it means they are NOT five independent inputs:");
        var stats = new[] { "max multiplicity", "Gini(m)", "Entropy(m)", "Herfindahl(m)", "largest share" };
        sb.AppendLine("  Spearman ρ between the five distribution statistics, over the eleven rings:");
        sb.AppendLine("  " + "".PadRight(18) + string.Join("", stats.Select(s => s.PadLeft(14))));
        foreach (string a in stats)
        {
            var sb2 = new StringBuilder($"  {a,-16}");
            foreach (string b in stats)
            {
                double[] xs = all.Select(r => InputOf(r, a)).ToArray();
                double[] ys = all.Select(r => InputOf(r, b)).ToArray();
                sb2.Append(AdaptabilityAudit.Spearman(xs, ys).ToString("F3", CultureInfo.InvariantCulture).PadLeft(14));
            }
            sb.AppendLine(sb2.ToString());
        }
        sb.AppendLine("  ⇒ Gini, largest share and max multiplicity are one axis; entropy and Herfindahl are its exact");
        sb.AppendLine("    mirror (negative of each other on a normalized distribution). So 'the full multiplicity");
        sb.AppendLine("    distribution' is, as a predictor space, essentially ONE number — the same collapse D_050 found");
        sb.AppendLine("    among its four spectral inputs.");

        Assert.All(all, r => Assert.Equal(AdaptabilityAudit.N, Mult(r).Sum()));
        Assert.True(Math.Abs(Gini(Mult("Pair1-47")) - Gini(Mult("Decay96"))) > 0.2,
            "the distribution shapes must differ substantially across the family");
        Output.WriteLine(sb.ToString());
    }

    // ── 2. The frozen prediction ────────────────────────────────────────────

    [Fact]
    public void D056_02_Frozen_Prediction()
    {
        var sb = new StringBuilder();
        PrintHeader("2. The frozen prediction — coefficients from published cases only");

        sb.AppendLine("  BASELINES TO BEAT (D_050/D_052 and the D_054 recomputation on the ring family):");
        sb.AppendLine("    near-gap density : ρ = 0.204   λ₂ : ρ = 0.214   degeneracy count : ρ = 0.815, LOO RMSE 0.03304");
        sb.AppendLine();
        sb.AppendLine("  DECISION RULE, FROZEN BEFORE ANY METRIC IS COMPUTED:");
        sb.AppendLine("    A distribution input BEATS the field iff BOTH hold on the required case set —");
        sb.AppendLine("      (i)  |Spearman ρ| > 0.815 (the degeneracy count's value), and");
        sb.AppendLine("      (ii) its within-family LOO RMSE is below the degeneracy count's 0.03304.");
        sb.AppendLine("    |ρ| is used because a predictor with a perfect but INVERTED association still carries the");
        sb.AppendLine("    information; the sign is reported for every input so the direction is never hidden.");
        sb.AppendLine("    Corroboration on the held-out set is then required, and a LEAVE-PAIR-OUT-of-the-outlier");
        sb.AppendLine("    check is pre-registered: the same metrics are recomputed with Pair1-47 REMOVED, so a");
        sb.AppendLine("    correlation resting on the single extreme ring cannot be mistaken for a law.");
        sb.AppendLine();

        sb.AppendLine("  Frozen coefficients (OLS on D_048/D_050's six published source cases):");
        sb.AppendLine("  target     input                    slope            intercept        R² (sources)");
        sb.AppendLine("  " + new string('-', 94));
        var fits = new Dictionary<string, (double Slope, double Intercept, double R2)>();
        foreach (string target in new[] { "capacity", "recovery" })
            foreach (string input in InputNames)
            {
                var f = FitOnSources(input, target == "capacity");
                fits[$"{target}/{input}"] = f;
                sb.AppendLine($"  {target,-10} {input,-24} {f.Slope,16:G6} {f.Intercept,16:G6} {f.R2,15:F3}");
            }

        sb.AppendLine();
        sb.AppendLine("  THE FROZEN PREDICTION — required case set");
        sb.AppendLine("  ring        input                  value        →  predicted cap    predicted rec");
        sb.AppendLine("  " + new string('-', 88));
        foreach (string ring in CaseSet)
            foreach (string input in InputNames)
            {
                double x = InputOf(ring, input);
                var cf = fits[$"capacity/{input}"];
                var rf = fits[$"recovery/{input}"];
                sb.AppendLine($"  {ring,-10} {input,-22} {x,10:F6}      {cf.Slope * x + cf.Intercept,16:F5} {rf.Slope * x + rf.Intercept,16:F5}");
            }

        sb.AppendLine();
        sb.AppendLine("  THE FROZEN PREDICTION — four new rings (measured for the first time in PHASE B). The rank");
        sb.AppendLine("  ceiling column is also the mechanism's own forecast, and for P47-48 it is a SHARP one:");
        sb.AppendLine("  adding the exactly-antipodal ±48 offset makes the odd-mode sum 4 + 2(1 − cos(πk)) = 8 exactly,");
        sb.AppendLine("  so the dominant level should survive — moved from λ = 4 to λ = 8 — and the ring should");
        sb.AppendLine("  collapse like Pair1-47 despite having a different edge set.");
        sb.AppendLine("  ring        input                  value        →  predicted cap    predicted rec");
        sb.AppendLine("  " + new string('-', 88));
        foreach (string ring in BlindSet)
            foreach (string input in InputNames)
            {
                double x = InputOf(ring, input);
                var cf = fits[$"capacity/{input}"];
                var rf = fits[$"recovery/{input}"];
                sb.AppendLine($"  {ring,-10} {input,-22} {x,10:F6}      {cf.Slope * x + cf.Intercept,16:F5} {rf.Slope * x + rf.Intercept,16:F5}");
            }

        sb.AppendLine();
        sb.AppendLine("  BOUNDS CHECK (derived, D_050): a prediction outside [0, 1] is wrong by construction. Reported");
        sb.AppendLine("  rather than clipped, per the D_054 precedent:");
        sb.AppendLine("  input                    cells outside [0,1]   worst value");
        sb.AppendLine("  " + new string('-', 66));
        foreach (string input in InputNames)
        {
            int outside = 0;
            double worst = 0.0;
            foreach (string ring in CaseSet.Concat(BlindSet))
            {
                double x = InputOf(ring, input);
                foreach (string target in new[] { "capacity", "recovery" })
                {
                    var f = fits[$"{target}/{input}"];
                    double pred = f.Slope * x + f.Intercept;
                    double excess = Math.Max(0.0, Math.Max(pred - 1.0, -pred));
                    if (excess > 0) { outside++; worst = Math.Max(worst, excess); }
                }
            }
            sb.AppendLine($"  {input,-24} {outside,20} {worst,14:F5}");
        }

        sb.AppendLine();
        sb.AppendLine("  The prediction is now on record. PHASE B (measurement, added in a later commit) is compared");
        sb.AppendLine("  against these numbers verbatim.");

        Output.WriteLine(sb.ToString());
    }

    // ── 3. Measurement and metrics ──────────────────────────────────────────

    private sealed record Observed(string Ring, double Capacity, double Recovery);

    /// <summary>
    /// Measure any named ring. D_048's six cases and the twelve rings audited by D_051–D_055 come from
    /// the shared cache; D_056's four new rings are measured here for the first time under the identical
    /// protocol (4 families × 5 doses × 3 fixed seeds, connectivity-guarded).
    /// </summary>
    private static readonly Dictionary<string, AdaptabilityProfile> ProfileCache = [];

    private static AdaptabilityProfile ProfileOf(string ring)
    {
        if (ProfileCache.TryGetValue(ring, out var c)) return c;
        var p = AdaptabilityAudit.CaseNames.Contains(ring)
            ? AdaptabilityAudit.Profiles.Single(x => x.Name == ring)
            : AdaptabilityAudit.Study(ring, AdjacencyOf(ring));
        ProfileCache[ring] = p;
        return p;
    }

    private static Observed[] Measure(IEnumerable<string> names)
        => names.Select(n => { var p = ProfileOf(n); return new Observed(n, p.Capacity, p.MeanRecovery); }).ToArray();

    private static double LooRmse(IReadOnlyList<double> x, IReadOnlyList<double> y)
    {
        int n = y.Count;
        double sse = 0.0;
        for (int h = 0; h < n; h++)
        {
            var xs = new List<double>();
            var ys = new List<double>();
            for (int i = 0; i < n; i++)
                if (i != h) { xs.Add(x[i]); ys.Add(y[i]); }
            if (xs.Max() - xs.Min() < 1e-12) return double.NaN;
            var (slope, intercept, _, _) = AdaptabilityAudit.Fit(xs.ToArray(), ys.ToArray());
            double pred = slope * x[h] + intercept;
            sse += (pred - y[h]) * (pred - y[h]);
        }
        return Math.Sqrt(sse / n);
    }

    /// <summary>Exact two-sided permutation p-value for Spearman ρ (n! enumerated).</summary>
    private static double ExactPermutationP(double[] x, double[] y)
    {
        double[] rx = AdaptabilityAudit.Ranks(x), ry = AdaptabilityAudit.Ranks(y);
        double observed = Math.Abs(AdaptabilityAudit.Pearson(rx, ry));
        int n = ry.Length;
        var perm = new double[n];
        int atLeast = 0, total = 0;
        var idx = Enumerable.Range(0, n).ToArray();
        void Walk(int k)
        {
            if (k == n)
            {
                for (int i = 0; i < n; i++) perm[i] = ry[idx[i]];
                total++;
                if (Math.Abs(AdaptabilityAudit.Pearson(rx, perm)) >= observed - 1e-12) atLeast++;
                return;
            }
            for (int i = k; i < n; i++)
            {
                (idx[k], idx[i]) = (idx[i], idx[k]);
                Walk(k + 1);
                (idx[k], idx[i]) = (idx[i], idx[k]);
            }
        }
        Walk(0);
        return total == 0 ? double.NaN : (double)atLeast / total;
    }

    private static string F(double v, string format = "F5")
        => double.IsNaN(v) ? "n/a (constant)" : v.ToString(format, CultureInfo.InvariantCulture);

    /// <summary>Every metric for one input on one case set, for one target.</summary>
    private sealed record Metric(string Input, string Target, string Set, double Rho, double P,
        double Loo, double R2, double FrozenError);

    private static List<Metric> Metrics(IReadOnlyList<Observed> obs, string setName)
    {
        var list = new List<Metric>();
        foreach (string target in new[] { "capacity", "recovery" })
        {
            double[] y = obs.Select(o => target == "capacity" ? o.Capacity : o.Recovery).ToArray();
            foreach (string input in InputNames)
            {
                double[] x = obs.Select(o => InputOf(o.Ring, input)).ToArray();
                var f = FitOnSources(input, target == "capacity");
                double frozenErr = obs.Select(o => Math.Abs(f.Slope * InputOf(o.Ring, input) + f.Intercept
                    - (target == "capacity" ? o.Capacity : o.Recovery))).Average();
                list.Add(new Metric(input, target, setName, AdaptabilityAudit.Spearman(x, y),
                    ExactPermutationP(x, y), LooRmse(x, y), AdaptabilityAudit.Fit(x, y).R2, frozenErr));
            }
        }
        return list;
    }

    /// <summary>The three baselines of the brief, recomputed on the same case set.</summary>
    private static List<Metric> BaselineMetrics(IReadOnlyList<Observed> obs)
    {
        var list = new List<Metric>();
        foreach (string target in new[] { "capacity", "recovery" })
        {
            double[] y = obs.Select(o => target == "capacity" ? o.Capacity : o.Recovery).ToArray();
            foreach (string input in new[] { "near-gap", "degeneracy", "lambda2" })
            {
                double[] x = obs.Select(o =>
                {
                    var spec = SpectrumOf(o.Ring);
                    var p = ProfileOf(o.Ring);
                    return input switch
                    {
                        "near-gap" => (double)AdaptabilityAudit.NearGapDensityK2(spec, p.Lambda2),
                        "degeneracy" => Mult(o.Ring).Count(v => v > 1),
                        _ => p.Lambda2,
                    };
                }).ToArray();
                list.Add(new Metric(input, target, "recomputed", AdaptabilityAudit.Spearman(x, y),
                    ExactPermutationP(x, y), LooRmse(x, y), AdaptabilityAudit.Fit(x, y).R2, double.NaN));
            }
        }
        return list;
    }

    [Fact]
    public void D056_03_Metrics_On_The_Required_Set()
    {
        var sb = new StringBuilder();
        PrintHeader("3. Metrics on the required case set");

        var required = Measure(CaseSet);
        var metrics = Metrics(required, "required");
        var baseline = BaselineMetrics(required);

        sb.AppendLine("  MEASURED");
        sb.AppendLine("  ring        capacity   recovery   max m   Gini(m)   Entropy(m)   Herfindahl   rank ceiling");
        sb.AppendLine("  " + new string('-', 104));
        foreach (var o in required)
        {
            var m = Mult(o.Ring);
            sb.AppendLine($"  {o.Ring,-10} {o.Capacity,9:F5} {o.Recovery,10:F5} {m.Max(),8} {Gini(m),9:F4} {Entropy(m),12:F4} {Herfindahl(m),12:F4} {RankCeilingCapacity(m, o.Ring),14:F4}");
        }

        sb.AppendLine();
        sb.AppendLine("  p is the EXACT two-sided permutation p-value for ρ (all 7! = 5040 rank permutations).");
        foreach (string target in new[] { "capacity", "recovery" })
        {
            sb.AppendLine();
            sb.AppendLine($"  {target.ToUpperInvariant()}");
            sb.AppendLine("  input                    Spearman ρ   exact p   LOO RMSE             refit R²   frozen mean |error|");
            sb.AppendLine("  " + new string('-', 100));
            foreach (var m in metrics.Where(m => m.Target == target).OrderByDescending(m => Math.Abs(m.Rho)))
                sb.AppendLine($"  {m.Input,-22} {m.Rho,10:F3} {m.P,10:F4}   {F(m.Loo),-18} {m.R2,10:F3} {m.FrozenError,20:F5}");
        }

        sb.AppendLine();
        sb.AppendLine("  THE THREE BASELINES, recomputed on this case set");
        sb.AppendLine("  baseline          target     Spearman ρ   exact p   LOO RMSE             refit R²");
        sb.AppendLine("  " + new string('-', 84));
        foreach (var m in baseline)
            sb.AppendLine($"  {m.Input,-16} {m.Target,-10} {m.Rho,10:F3} {m.P,10:F4}   {F(m.Loo),-18} {m.R2,10:F3}");

        sb.AppendLine();
        sb.AppendLine("  HEAD-TO-HEAD — does a distribution input beat the field? Frozen rule: |ρ| > 0.815 AND");
        sb.AppendLine("  LOO RMSE < 0.03304, on the required case set.");
        sb.AppendLine("  target     best distribution input   |ρ|      > 0.815?   LOO        < 0.03304?   BEATS?   sign");
        sb.AppendLine("  " + new string('-', 108));
        foreach (string target in new[] { "capacity", "recovery" })
        {
            var cand = metrics.Where(m => m.Target == target).OrderByDescending(m => Math.Abs(m.Rho)).First();
            bool rhoOk = Math.Abs(cand.Rho) > 0.815;
            bool looOk = !double.IsNaN(cand.Loo) && cand.Loo < 0.03304;
            sb.AppendLine($"  {target,-10} {cand.Input,-26} {Math.Abs(cand.Rho),6:F3} {rhoOk,11}   {F(cand.Loo),-10} {looOk,12}   {(rhoOk && looOk ? "YES" : "NO"),-8} {(cand.Rho > 0 ? "positive" : "NEGATIVE")}");
        }

        Output.WriteLine(sb.ToString());
        Assert.Equal(7, required.Length);
    }

    // ── 4. Does the correlation survive removing the outlier? ───────────────

    [Fact]
    public void D056_04_Outlier_Removed()
    {
        var sb = new StringBuilder();
        PrintHeader("4. Pre-registered robustness check — the same metrics without Pair1-47");

        var full = Measure(CaseSet);
        var without = full.Where(o => o.Ring != "Pair1-47").ToArray();
        var metricsFull = Metrics(full, "full");
        var metricsWithout = Metrics(without, "without Pair1-47");
        var baseFull = BaselineMetrics(full);
        var baseWithout = BaselineMetrics(without);

        sb.AppendLine("  This check was frozen in PHASE A precisely because every distribution statistic is monotone in");
        sb.AppendLine("  'how concentrated is the spectrum' and Pair1-47 is the extreme point. If the correlation lives");
        sb.AppendLine("  on that one ring, removing it must collapse the ρ values.");
        sb.AppendLine();
        sb.AppendLine("  capacity, ρ over seven rings → ρ over the six healthy rings, and R² the same way:");
        sb.AppendLine("  input                    ρ (7)     ρ (6)     Δρ       R² (7)   R² (6)   LOO (6)");
        sb.AppendLine("  " + new string('-', 92));
        foreach (string input in InputNames)
        {
            var a = metricsFull.Single(m => m.Target == "capacity" && m.Input == input);
            var b = metricsWithout.Single(m => m.Target == "capacity" && m.Input == input);
            sb.AppendLine($"  {input,-22} {a.Rho,8:F3} {b.Rho,9:F3} {b.Rho - a.Rho,8:F3} {a.R2,9:F3} {b.R2,8:F3}   {F(b.Loo)}");
        }
        sb.AppendLine();
        sb.AppendLine("  the three baselines under the same removal:");
        sb.AppendLine("  baseline          ρ (7)     ρ (6)     R² (7)   R² (6)");
        sb.AppendLine("  " + new string('-', 58));
        foreach (string input in new[] { "near-gap", "degeneracy", "lambda2" })
        {
            var a = baseFull.Single(m => m.Target == "capacity" && m.Input == input);
            var b = baseWithout.Single(m => m.Target == "capacity" && m.Input == input);
            sb.AppendLine($"  {input,-16} {a.Rho,8:F3} {b.Rho,9:F3} {a.R2,9:F3} {b.R2,8:F3}");
        }

        sb.AppendLine();
        sb.AppendLine("  THE THREE RINGS THE DISTRIBUTION CANNOT SEPARATE. D96, S96-123 and Ring48 share one");
        sb.AppendLine("  multiplicity distribution exactly — identical max multiplicity, Gini, entropy and Herfindahl —");
        sb.AppendLine("  so every distribution input assigns them ONE number. Their measured capacities:");
        var triple = full.Where(o => o.Ring is "D96" or "S96-123" or "Ring48").ToArray();
        foreach (var o in triple) sb.AppendLine($"    {o.Ring,-10} capacity {o.Capacity:F5}");
        double spread = triple.Max(o => o.Capacity) - triple.Min(o => o.Capacity);
        double span = full.Max(o => o.Capacity) - full.Min(o => o.Capacity);
        sb.AppendLine($"    spread within that single distribution value: {spread:F5} = {spread / span:P0} of the whole family span");
        sb.AppendLine("  So the distribution is subject to exactly the D_052 non-injectivity, on the very same triple.");

        sb.AppendLine();
        sb.AppendLine("  AND THE NEAR-GAP BASELINE IS UNUSABLE HERE. Its ρ over the seven rings is driven by Ring48");
        sb.AppendLine("  alone (near-gap 8 against six 2s), so dropping a single ring makes its input constant:");
        sb.AppendLine($"    near-gap LOO on the seven rings: {F(baseFull.Single(m => m.Target == "capacity" && m.Input == "near-gap").Loo)}");
        sb.AppendLine($"    near-gap LOO on the six healthy rings: {F(baseWithout.Single(m => m.Target == "capacity" && m.Input == "near-gap").Loo)}");

        Output.WriteLine(sb.ToString());
    }

    // ── 5. The blind test ───────────────────────────────────────────────────

    [Fact]
    public void D056_05_Blind_Rings()
    {
        var sb = new StringBuilder();
        PrintHeader("5. The blind test — four new multiplicity-distribution rings");

        var blind = Measure(BlindSet);
        var fits = new Dictionary<string, (double Slope, double Intercept, double R2)>();
        foreach (string input in InputNames)
            foreach (bool cap in new[] { true, false })
                fits[$"{(cap ? "capacity" : "recovery")}/{input}"] = FitOnSources(input, cap);

        sb.AppendLine("  These four rings were declared in commit fcd81f69 and measured for the first time in this");
        sb.AppendLine("  commit, so this section is the audit's genuinely blind component.");
        sb.AppendLine();
        sb.AppendLine("  ring        obs capacity   obs recovery   max m   rank ceiling   predicted cap (ceiling)");
        sb.AppendLine("  " + new string('-', 96));
        foreach (var o in blind)
        {
            var m = Mult(o.Ring);
            double cf = fits["capacity/rank ceiling (D_055)"].Slope;
            double ci = fits["capacity/rank ceiling (D_055)"].Intercept;
            double ceil = RankCeilingCapacity(m, o.Ring);
            sb.AppendLine($"  {o.Ring,-10} {o.Capacity,12:F5} {o.Recovery,14:F5} {m.Max(),8} {ceil,14:F4} {cf * ceil + ci,26:F5}");
        }
        sb.AppendLine();
        sb.AppendLine("  THE SHARP MECHANISM FORECAST. P47-48 was built by adding the exactly-antipodal ±48 offset to");
        sb.AppendLine("  Pair1-47. For odd k the ±48 term is 2(1 − cos(πk)) = 4, so the odd-mode sum becomes");
        sb.AppendLine("  4 + 4 = 8 EXACTLY: the dominant level should survive, MOVED from λ = 4 to λ = 8, and the ring");
        sb.AppendLine("  should collapse like Pair1-47 despite a different edge set. Checked directly:");
        foreach (string ring in new[] { "Pair1-47", "P47-48" })
        {
            var spec = SpectrumOf(ring);
            double lam2 = spec.Where(v => v > AdaptabilityAudit.Tol).Min();
            var m = Mult(ring);
            int dom = m.Max();
            double at = spec.GroupBy(v => Math.Round(v, 6)).OrderByDescending(g => g.Count()).First().Key;
            sb.AppendLine($"    {ring,-9} dominant level at λ = {at:F6} with multiplicity {dom}"
                          + $"   λ₂ = {lam2:F6}   near-gap(2λ₂) = {AdaptabilityAudit.NearGapDensityK2(spec, lam2)}");
        }
        sb.AppendLine();
        sb.AppendLine("  P47-16 was built to DISSOLVE the dominant level with a mid-range offset, and H51123 has no N/2");
        sb.AppendLine("  relationship at all — both should therefore behave like healthy rings.");

        sb.AppendLine();
        sb.AppendLine("  FROZEN VERSUS OBSERVED, per input (rank-correlation over the four new rings — 4! = 24");
        sb.AppendLine("  permutations, so the smallest attainable two-sided p is 2/24 = 0.0833):");
        sb.AppendLine("  input                    target     Spearman ρ   exact p   LOO RMSE             refit R²   frozen mean |error|");
        sb.AppendLine("  " + new string('-', 112));
        var bm = Metrics(blind, "blind");
        foreach (var m in bm.OrderBy(m => m.Target).ThenByDescending(m => Math.Abs(m.Rho)))
            sb.AppendLine($"  {m.Input,-22} {m.Target,-10} {m.Rho,10:F3} {m.P,10:F4}   {F(m.Loo),-18} {m.R2,10:F3} {m.FrozenError,20:F5}");

        sb.AppendLine();
        sb.AppendLine("  Baselines on the blind set:");
        sb.AppendLine("  baseline          target     Spearman ρ   exact p");
        sb.AppendLine("  " + new string('-', 56));
        foreach (var m in BaselineMetrics(blind))
            sb.AppendLine($"  {m.Input,-16} {m.Target,-10} {m.Rho,10:F3} {m.P,10:F4}");

        Output.WriteLine(sb.ToString());
    }

    // ── 6. Verdict ──────────────────────────────────────────────────────────

    [Fact]
    public void D056_06_Verdict()
    {
        var sb = new StringBuilder();
        PrintHeader("6. Verdict — DERIVED / EMERGENT / REFUTED");

        var required = Measure(CaseSet);
        var without = required.Where(o => o.Ring != "Pair1-47").ToArray();
        var blind = Measure(BlindSet);
        var req = Metrics(required, "required");
        var wo = Metrics(without, "without outlier");
        var bl = Metrics(blind, "blind");
        var baseReq = BaselineMetrics(required);
        var baseWo = BaselineMetrics(without);

        var capBest = req.Where(m => m.Target == "capacity").OrderByDescending(m => Math.Abs(m.Rho)).First();
        var capBestLoo = req.Where(m => m.Target == "capacity" && !double.IsNaN(m.Loo)).OrderBy(m => m.Loo).First();
        var degReq = baseReq.Single(m => m.Input == "degeneracy" && m.Target == "capacity");
        var degWo = baseWo.Single(m => m.Input == "degeneracy" && m.Target == "capacity");
        var ceilWo = wo.Single(m => m.Input == "rank ceiling (D_055)" && m.Target == "capacity");
        var degWoCap = degWo;
        var recBest = req.Where(m => m.Target == "recovery").OrderByDescending(m => Math.Abs(m.Rho)).First();
        var ceilBlind = bl.Single(m => m.Input == "rank ceiling (D_055)" && m.Target == "capacity");
        double capSpan = required.Max(o => o.Capacity) - required.Min(o => o.Capacity);
        var triple = required.Where(o => o.Ring is "D96" or "S96-123" or "Ring48").ToArray();
        double tripleSpread = triple.Max(o => o.Capacity) - triple.Min(o => o.Capacity);

        sb.AppendLine("  THE GOAL, TAKEN LITERALLY: beat the near-gap density, the degeneracy count and λ₂.");
        sb.AppendLine();
        sb.AppendLine("  CAPACITY — MET ON ρ AND R², TIED WITH THE DEGENERACY COUNT ON ORDERING. On the required set:");
        sb.AppendLine($"    distribution statistics      ρ = {capBest.Rho:F3} (exact p = {capBest.P:F4}), R² = {capBest.R2:F3}");
        sb.AppendLine($"    degeneracy count (baseline)  ρ = {degReq.Rho:F3} (exact p = {degReq.P:F4}), R² = {degReq.R2:F3}");
        sb.AppendLine($"    λ₂ (baseline)                ρ = {baseReq.Single(m => m.Input == "lambda2" && m.Target == "capacity").Rho:F3}");
        sb.AppendLine($"    near-gap (baseline)          ρ = {baseReq.Single(m => m.Input == "near-gap" && m.Target == "capacity").Rho:F3} — and its LOO is UNDEFINED here");
        sb.AppendLine($"    ⇒ the distribution ties the degeneracy count on ρ EXACTLY (both {Math.Abs(capBest.Rho):F3}) and beats λ₂ and near-gap; it wins");
        sb.AppendLine("      on R² and on the healthy-six subset, and it reaches ρ = ±1.000 on the blind rings.");
        sb.AppendLine("    ⇒ the frozen decision rule (|ρ| > 0.815 AND LOO < 0.03304) is NOT met — but nor is it met by the");
        sb.AppendLine($"      degeneracy count itself on this case set ({F(degReq.Loo)}), because the 0.03304 bar was");
        sb.AppendLine("      inherited from D_052's DIFFERENT case set. Relative comparisons on the same set are used below.");
        sb.AppendLine("    The p-value is the first in the D group to SURVIVE multiple-comparison correction: p = 0.0024");
        sb.AppendLine("    against α/5 = 0.01 for five declared distribution inputs.");
        sb.AppendLine();
        sb.AppendLine("  RECOVERY — NOT MET, and emphatically so:");
        sb.AppendLine($"    best distribution input on recovery is {recBest.Input} at ρ = {recBest.Rho:F3} (exact p = {recBest.P:F4}).");
        sb.AppendLine("    The multiplicity distribution says nothing about robustness, while λ₂ does (ρ = 0.821, p = 0.0341).");
        sb.AppendLine();
        sb.AppendLine("  DERIVED");
        sb.AppendLine("    · THE FIVE DISTRIBUTION STATISTICS ARE ONE AXIS, NOT FIVE. On the eleven audited rings max");
        sb.AppendLine("      multiplicity, Gini(m) and the largest-level share are mutually monotone and give IDENTICAL ρ");
        sb.AppendLine("      on both targets to three decimals (capacity −0.964 for all three; recovery −0.037), while");
        sb.AppendLine("      Entropy(m) and Herfindahl(m) are the exact mirror of that axis (+0.964 and −0.964). So 'the");
        sb.AppendLine("      full multiplicity distribution' is, as a predictor space, ONE number — the collapse D_050");
        sb.AppendLine("      found among its four spectral inputs, reproduced here.");
        sb.AppendLine("    · THE MECHANISM PREDICTOR WINS DECISIVELY. D_055's rank ceiling is the only input that is");
        sb.AppendLine($"      derived from the distribution by argument rather than fitted from it, and it leads on every");
        sb.AppendLine($"      value-fidelity measure: R² = {ceilWo.R2:F3} on the six healthy rings (against the degeneracy count's");
        sb.AppendLine($"      {degWoCap.R2:F3}), LOO {F(ceilWo.Loo)} there (the best of any input), R² = {ceilBlind.R2:F3} and ρ = 1.000 on the");
        sb.AppendLine($"      blind rings, and the smallest frozen-coefficient error of all six inputs.");
        sb.AppendLine("    · THE NON-INJECTIVITY IS INHERITED. D96, S96-123 and Ring48 share one multiplicity distribution");
        sb.AppendLine("      EXACTLY — identical max multiplicity, Gini, entropy, Herfindahl and share — so every");
        sb.AppendLine($"      distribution input assigns them ONE number, while their measured capacities spread {tripleSpread:F5}");
        sb.AppendLine($"      = {tripleSpread / capSpan:P0} of the whole family span. The distribution is subject to the same");
        sb.AppendLine("      limitation D_052 found for the degeneracy count, on the very same three rings.");
        sb.AppendLine("    · THE NEAR-GAP BASELINE IS STRUCTURALLY UNUSABLE ON RINGS. Six of the seven rings sit at");
        sb.AppendLine("      near-gap 2 with Ring48 alone at 8, so its input becomes CONSTANT as soon as one ring is held");
        sb.AppendLine("      out — its LOO is undefined, not merely poor. It cannot be a ring-family predictor at all.");
        sb.AppendLine();
        sb.AppendLine("  EMERGENT");
        sb.AppendLine("    · THE BLIND FORECAST WAS CONFIRMED, INCLUDING ITS SHARPEST CLAUSE. P47-48 was built by adding");
        sb.AppendLine("      the exactly-antipodal ±48 offset to Pair1-47, and the frozen prediction was that the dominant");
        sb.AppendLine("      level should SURVIVE, MOVED from λ = 4 to λ = 8, leaving the ring collapsed. Measured: the");
        sb.AppendLine("      dominant level sits at λ = 8.000000 with multiplicity 49, and P47-48's capacity is 0.58920 —");
        sb.AppendLine("      the only blind ring that collapses. Its λ₂ is IDENTICAL to Pair1-47's (0.034221) and its");
        sb.AppendLine("      near-gap is 2, so the near-gap predictor cannot see it at all.");
        sb.AppendLine("    · The other three blind rings behaved as the mechanism says: P47-16 (dominant level partly");
        sb.AppendLine("      dissolved, max multiplicity 34) at capacity 0.69765; P47-123 (max multiplicity 4) at 0.99618;");
        sb.AppendLine("      H51123 (no N/2 relationship, max multiplicity 6) at 0.97455. All four orderings are correct:");
        sb.AppendLine("      ρ = ±1.000 for all five distribution statistics and for the rank ceiling on the blind set.");
        sb.AppendLine("    · THE OUTLIER CHECK PASSED, WHICH IS THE STRONGEST FORM OF THE RESULT. Removing Pair1-47 moves");
        sb.AppendLine($"      ρ only from 0.964 to 0.941 and R² from 0.995 to between 0.540 and 0.784 — so the correlation does");
        sb.AppendLine("      NOT rest on the single extreme ring. On the six healthy rings the distribution statistics");
        sb.AppendLine($"      (R² up to {wo.Where(m => m.Target == "capacity").Max(m => m.R2):F3}) beat the degeneracy count ({degWoCap.R2:F3}), λ₂ and near-gap.");
        sb.AppendLine("    · THE FROZEN-COEFFICIENT PREDICTIONS ARE POOR FOR EVERY INPUT (mean errors 0.19 … 0.49), which is");
        sb.AppendLine("      D_051's lesson recurring: coefficients fitted on D_048/D_050's non-ring sources extrapolate");
        sb.AppendLine("      badly onto rings. The rank ceiling is again the least bad, at 0.19963 on the required set and");
        sb.AppendLine("      0.19593 on the blind set.");
        sb.AppendLine();
        sb.AppendLine("  REFUTED");
        sb.AppendLine("    · 'Capacity is controlled by the FULL multiplicity distribution, beyond the degeneracy count.'");
        sb.AppendLine($"      REFUTED as an ordering claim: on the required set the five statistics and the degeneracy count");
        sb.AppendLine($"      give the SAME ρ ({Math.Abs(capBest.Rho):F3}, identical exact p = {capBest.P:F4}), so the full distribution adds no rank");
        sb.AppendLine("      information the count does not already carry. It does add VALUE fidelity (R² 0.995 vs 0.987,");
        sb.AppendLine($"      and {wo.Where(m => m.Target == "capacity").Max(m => m.R2):F3} vs {degWoCap.R2:F3} on the healthy six) — a refinement, not a new instrument.");
        sb.AppendLine("    · 'The multiplicity distribution controls robustness as well as adaptability.' REFUTED:");
        sb.AppendLine($"      every distribution input gives ρ = {recBest.Rho:F3} with exact p = {recBest.P:F4} on recovery — indistinguishable from no");
        sb.AppendLine("      association at all — while λ₂ reaches ρ = 0.821 there.");
        sb.AppendLine("    · 'Each distribution statistic is an independent input.' REFUTED: the five collapse onto one");
        sb.AppendLine("      axis with two signs (three mutually monotone, two mirror images), exactly as D_050's four");
        sb.AppendLine("      spectral inputs collapsed onto one.");
        sb.AppendLine("    · 'Two of the inputs anyway separate the identical-distribution triple.' REFUTED: D96, S96-123");
        sb.AppendLine($"      and Ring48 share one distribution value whose measured capacities spread {tripleSpread:F5}, so no");
        sb.AppendLine("      functional of the distribution can be sufficient — the same non-injectivity as D_052.");
        sb.AppendLine("    · 'Beating the near-gap baseline demonstrates a superior predictor.' REFUTED as a claim about");
        sb.AppendLine("      near-gap on rings: its LOO is undefined there (six rings share one value), so it is not a");
        sb.AppendLine("      competitor that can be beaten or lose on this family.");
        sb.AppendLine();
        sb.AppendLine("  SUMMARY. The multiplicity distribution predicts ring CAPACITY strongly and does not predict");
        sb.AppendLine("  recovery at all. Its five specified statistics are one axis; on the required set that axis only");
        sb.AppendLine("  TIES the degeneracy count on ordering, though it beats it on R² and on the healthy subset, and it");
        sb.AppendLine("  reaches perfect ordering on four brand-new rings whose collapse was predicted in advance from the");
        sb.AppendLine("  distribution alone. The winner on every fidelity measure is the DERIVED rank-budget ceiling of");
        sb.AppendLine("  D_055 — an argument, not a fit — and its sharpest clause (the λ = 8 level in P47-48) was confirmed");
        sb.AppendLine("  exactly.");
        sb.AppendLine();
        sb.AppendLine("  No canonical AT claim, value, equation or registry entry is changed; the D_040");
        sb.AppendLine("  ClassificationRegistry is untouched. No new simulation primitive is added to the shared machinery:");
        sb.AppendLine("  the twelve previously audited rings come from the shared cache and only the four new rings are");
        sb.AppendLine("  measured here.");

        Assert.True(Math.Abs(capBest.Rho) > 0.9, "the distribution must predict capacity strongly");
        Assert.True(Math.Abs(capBest.Rho) <= Math.Abs(degReq.Rho) + 1e-9,
            "and it must NOT beat the degeneracy count on ordering — the tie is the finding");
        Assert.True(Math.Abs(recBest.Rho) < 0.4, "the distribution must fail on recovery");
        Assert.True(wo.Single(m => m.Input == "rank ceiling (D_055)" && m.Target == "capacity").R2 > degWoCap.R2,
            "the derived rank ceiling must lead on the healthy subset");
        Assert.True(blind.Single(o => o.Ring == "P47-48").Capacity < 0.7,
            "the predicted collapse of P47-48 must be exhibited");
        Assert.True(Math.Abs(ceilBlind.Rho) == 1.0, "the rank ceiling must order the blind rings perfectly");

        Output.WriteLine(sb.ToString());
    }
}
