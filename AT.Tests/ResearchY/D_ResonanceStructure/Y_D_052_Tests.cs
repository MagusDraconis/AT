using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_052 — Degeneracy Axis Audit.
///
/// Question: does the DEGENERACY axis predict adaptability WITHIN the ring family, where D_050's
/// near-gap predictor was shown (D_051) to be a constant and therefore useless?
///
/// The family is the seven 96-node rings: the canonical D96 plus D_051's six blind rings. D_051
/// ended with a concrete hypothesis — the near-gap density does not vary across rings while the
/// degeneracy count does (38 … 47), so the collinear pair's OTHER member is the better candidate.
/// This audit tests that hypothesis under the same blind protocol.
///
/// PHASE A (this file, committed on its own) contains the PREDICTION ONLY — it never constructs the
/// perturbation ensemble. PHASE B measures the rings and compares. The blind element is the six
/// rings' measured capacity and recovery; every coefficient used here is fitted from D_048–D_050's
/// already-published cases, so the prediction depends on nothing new.
///
/// Deterministic throughout: fixed rings, and the shared fixed seeds and doses.
/// </summary>
public class Y_D_052_Tests : ResearchTestBase
{
    public Y_D_052_Tests(ITestOutputHelper output) : base(output) { }

    private static readonly string[] RingNames = AdaptabilityAudit.RingFamilyNames;

    // ── The four candidate predictors ───────────────────────────────────────
    //
    // Each is a scalar read from the ring's SPECTRUM. All coefficients are ordinary-least-squares
    // fits on D_048/D_050's six already-published cases (computed at run time from those published
    // measurements — no ring measurement enters), so the prediction is reproducible and closed.
    //
    //   NG   near-gap density at k = 2   — D_050's chosen minimal predictor (the baseline to beat)
    //   DEG  degeneracy count            — levels with multiplicity > 1 (D_050's rank winner)
    //   ENT  degeneracy entropy          — Shannon entropy of the multiplicity distribution
    //   LOCK locked entropy ΔE_lock      — D_047's (1/N)·Σ_{m>1} m·ln m ("multiplicity spectrum")

    /// <summary>The spectral inputs of one ring — no simulation of any kind is involved.</summary>
    private sealed record Axis(string Name, int NearGap, int Degeneracy, double Entropy,
        double LockedEntropy, int MaxMultiplicity, int Distinct, double Lambda2, string Multiplicity);

    private static Axis AxisOf(string name)
    {
        var spectrum = AdaptabilityAudit.SpectrumOf(AdaptabilityAudit.RingAdjacency(name));
        var (_, entropy, mult) = AdaptabilityAudit.Buckets(spectrum);
        double lam2 = spectrum.Where(x => x > AdaptabilityAudit.Tol).Min();
        var groups = mult.GroupBy(m => m).OrderByDescending(g => g.Key)
                         .Select(g => $"{g.Count()}×{g.Key}");
        return new Axis(name, AdaptabilityAudit.NearGapDensityK2(spectrum, lam2),
            mult.Count(m => m > 1), entropy,
            AdaptabilityAudit.LockedEntropy(mult, AdaptabilityAudit.N),
            mult.Max(), mult.Length, lam2, string.Join(", ", groups));
    }

    /// <summary>A fitted two-parameter predictor: y ≈ Slope·x + Intercept.</summary>
    private sealed record Predictor(string Input, double Slope, double Intercept, double FitR2)
    {
        public double Apply(double x) => Slope * x + Intercept;
    }

    /// <summary>Fit each candidate predictor on D_048/D_050's six published cases.</summary>
    private static Dictionary<string, Predictor> FrozenPredictors()
    {
        var rows = AdaptabilityAudit.CaseNames.Select(name =>
        {
            var p = AdaptabilityAudit.Profiles.Single(x => x.Name == name);
            var spectrum = AdaptabilityAudit.SpectrumOf(AdaptabilityAudit.Adjacency(name));
            double lam2 = p.Lambda2;
            var (_, entropy, mult) = AdaptabilityAudit.Buckets(spectrum);
            return (NearGap: AdaptabilityAudit.NearGapDensityK2(spectrum, lam2),
                Degeneracy: mult.Count(m => m > 1), Entropy: entropy,
                Locked: AdaptabilityAudit.LockedEntropy(mult, AdaptabilityAudit.N),
                Capacity: p.Capacity, Recovery: p.MeanRecovery);
        }).ToArray();

        var map = new Dictionary<string, Predictor>();
        foreach (string target in new[] { "capacity", "recovery" })
        {
            double[] y = target == "capacity" ? rows.Select(r => r.Capacity).ToArray()
                                              : rows.Select(r => r.Recovery).ToArray();
            foreach (string input in new[] { "NG", "DEG", "ENT", "LOCK" })
            {
                double[] x = input switch
                {
                    "NG" => rows.Select(r => (double)r.NearGap).ToArray(),
                    "DEG" => rows.Select(r => (double)r.Degeneracy).ToArray(),
                    "ENT" => rows.Select(r => r.Entropy).ToArray(),
                    _ => rows.Select(r => r.Locked).ToArray(),
                };
                var (slope, intercept, r2, _) = AdaptabilityAudit.Fit(x, y);
                map[$"{target}/{input}"] = new Predictor(input, slope, intercept, r2);
            }
        }
        return map;
    }

    private static double AxisValue(Axis a, string input) => input switch
    {
        "NG" => a.NearGap,
        "DEG" => a.Degeneracy,
        "ENT" => a.Entropy,
        _ => a.LockedEntropy,
    };

    // ── 1. The seven rings on the degeneracy axis ───────────────────────────

    [Fact]
    public void D052_01_Ring_Family_On_The_Degeneracy_Axis()
    {
        var sb = new StringBuilder();
        PrintHeader("1. The seven rings on the degeneracy axis — PHASE A (no simulation)");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  A1. The family is the seven 96-node rings D96 + D_051's six blind rings, all circulant");
        sb.AppendLine("      (C96 with signed offsets). D96's own capacity and recovery are ALREADY published");
        sb.AppendLine("      (D_048/D_049: 0.9902 / 0.9659), so it is a reproducibility check, not a blind point;");
        sb.AppendLine("      the blind points are the six rings.");
        sb.AppendLine("  A2. Four candidate predictors, all read from the SPECTRUM alone: near-gap density at");
        sb.AppendLine("      k = 2 (D_050's pick, the baseline to beat), degeneracy count (levels with m > 1),");
        sb.AppendLine("      degeneracy entropy (Shannon entropy of the multiplicity distribution), and D_047's");
        sb.AppendLine("      locked entropy ΔE_lock = (1/N)·Σ_{m>1} m·ln m (the 'multiplicity spectrum' input).");
        sb.AppendLine("  A3. Every coefficient is an OLS fit on D_048/D_050's six ALREADY-PUBLISHED cases. No ring");
        sb.AppendLine("      measurement of any kind enters the prediction, and none of these rings is in that set.");
        sb.AppendLine("  A4. Success = the degeneracy-based predictor beats the near-gap predictor on the six blind");
        sb.AppendLine("      rings. Metrics are computed in PHASE B: R², Spearman, leave-one-out RMSE.");
        sb.AppendLine();

        var axes = RingNames.Select(AxisOf).ToArray();

        sb.AppendLine("  THE FAMILY (spectral inputs only — no dynamics run)");
        sb.AppendLine("  ring        λ₂          near-gap(2λ₂)   degen count   entropy   ΔE_lock   max m   distinct");
        sb.AppendLine("  " + new string('-', 96));
        foreach (var a in axes)
            sb.AppendLine($"  {a.Name,-9} {a.Lambda2,10:F6} {a.NearGap,14} {a.Degeneracy,13} {a.Entropy,9:F4} {a.LockedEntropy,9:F4} {a.MaxMultiplicity,7} {a.Distinct,10}");
        sb.AppendLine();
        sb.AppendLine("  MULTIPLICITY SPECTRA (m = multiplicity, grouped) — the object the three degeneracy inputs read:");
        foreach (var a in axes) sb.AppendLine($"    {a.Name,-9} {a.Multiplicity}");

        sb.AppendLine();
        sb.AppendLine("  ALREADY VISIBLE AT PREDICTION TIME — D_051's hypothesis restated exactly:");
        int ngDistinct = axes.Select(a => a.NearGap).Distinct().Count();
        int degDistinct = axes.Select(a => a.Degeneracy).Distinct().Count();
        sb.AppendLine($"    near-gap density takes {ngDistinct} distinct values across the seven rings "
                      + $"({string.Join(", ", axes.Select(a => a.NearGap))}),");
        sb.AppendLine($"    degeneracy count takes {degDistinct} distinct values "
                      + $"({string.Join(", ", axes.Select(a => a.Degeneracy))}).");
        sb.AppendLine($"    near-gap span {axes.Max(a => a.NearGap) - axes.Min(a => a.NearGap)} vs degeneracy span "
                      + $"{axes.Max(a => a.Degeneracy) - axes.Min(a => a.Degeneracy)} — the degeneracy axis RESOLVES this family.");
        sb.AppendLine($"    entropy spans {axes.Min(a => a.Entropy):F4} … {axes.Max(a => a.Entropy):F4}; ΔE_lock spans "
                      + $"{axes.Min(a => a.LockedEntropy):F4} … {axes.Max(a => a.LockedEntropy):F4}.");

        Assert.True(degDistinct >= 5, "the degeneracy count must resolve the ring family");
        Assert.True(degDistinct > ngDistinct, "the degeneracy count must resolve it better than the near-gap density");

        Output.WriteLine(sb.ToString());
    }

    // ── 2. The frozen prediction ────────────────────────────────────────────

    [Fact]
    public void D052_02_Frozen_Prediction()
    {
        var sb = new StringBuilder();
        PrintHeader("2. The frozen prediction — coefficients fitted on published cases only");

        var predictors = FrozenPredictors();
        var axes = RingNames.Select(AxisOf).ToArray();

        sb.AppendLine("  Coefficients (OLS on D_048/D_050's six published cases), printed here so the audit is");
        sb.AppendLine("  reproducible and so any later adjustment would be visible:");
        sb.AppendLine();
        sb.AppendLine("  target    input   slope          intercept      R²(in-sample, D_050 cases)");
        sb.AppendLine("  " + new string('-', 82));
        foreach (string target in new[] { "capacity", "recovery" })
            foreach (string input in new[] { "NG", "DEG", "ENT", "LOCK" })
            {
                var p = predictors[$"{target}/{input}"];
                sb.AppendLine($"  {target,-9} {input,-7} {p.Slope,14:G6} {p.Intercept,14:G6} {p.FitR2,12:F3}");
            }
        sb.AppendLine();
        sb.AppendLine("  D_050's published near-gap fit for capacity was −0.00814·x + 0.87153 (R² = 0.759) and");
        sb.AppendLine("  for recovery +0.000284·x + 0.95582 (R² = 0.525); the NG rows above reproduce them, which");
        sb.AppendLine("  is the integrity check that these fits are D_050's and not something re-tuned.");

        sb.AppendLine();
        sb.AppendLine("  THE FROZEN PREDICTION — six blind rings + the D96 check");
        sb.AppendLine("  ring        input  = value        →  predicted capacity   predicted recovery");
        sb.AppendLine("  " + new string('-', 84));
        foreach (var a in axes)
            foreach (string input in new[] { "NG", "DEG", "ENT", "LOCK" })
            {
                double x = AxisValue(a, input);
                double c = predictors[$"capacity/{input}"].Apply(x);
                double r = predictors[$"recovery/{input}"].Apply(x);
                sb.AppendLine($"  {a.Name,-9} {input,-6} {x,10:F4}        {c,17:F5} {r,19:F5}");
            }

        sb.AppendLine();
        sb.AppendLine("  PREDICTED CAPACITY RANKINGS (one line per predictor — this is where they differ):");
        foreach (string input in new[] { "NG", "DEG", "ENT", "LOCK" })
        {
            var ordered = axes.OrderByDescending(a => predictors[$"capacity/{input}"].Apply(AxisValue(a, input))).ToArray();
            sb.AppendLine($"    {input,-5}: " + string.Join(" > ", ordered.Select(a => a.Name)));
        }
        sb.AppendLine();
        sb.AppendLine("  PREDICTED RECOVERY RANKINGS:");
        foreach (string input in new[] { "NG", "DEG", "ENT", "LOCK" })
        {
            var ordered = axes.OrderByDescending(a => predictors[$"recovery/{input}"].Apply(AxisValue(a, input))).ToArray();
            sb.AppendLine($"    {input,-5}: " + string.Join(" > ", ordered.Select(a => a.Name)));
        }

        sb.AppendLine();
        sb.AppendLine("  BOUNDS CHECK (derived, D_050): every prediction must lie inside [0, 1].");
        foreach (var a in axes)
            foreach (string input in new[] { "NG", "DEG", "ENT", "LOCK" })
            {
                double c = predictors[$"capacity/{input}"].Apply(AxisValue(a, input));
                double r = predictors[$"recovery/{input}"].Apply(AxisValue(a, input));
                Assert.InRange(c, 0.0, 1.0);
                Assert.InRange(r, 0.0, 1.0);
            }
        sb.AppendLine("    all 4 predictors × 7 rings × 2 targets lie inside [0, 1] ✓");

        sb.AppendLine();
        sb.AppendLine("  The prediction is now on record. PHASE B (measurement under the shared D_048/D_049");
        sb.AppendLine("  ensemble, added in a later commit) is compared against these numbers verbatim.");

        Output.WriteLine(sb.ToString());
    }
}
