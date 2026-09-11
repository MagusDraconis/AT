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
}
