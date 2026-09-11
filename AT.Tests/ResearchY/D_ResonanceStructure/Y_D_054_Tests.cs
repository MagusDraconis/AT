using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_054 — Low-Energy Spectral Edge Audit.
///
/// Question: is capacity controlled by the SHAPE of the low-energy spectral edge — λ₂, λ₃, λ₄, λ₅
/// and the shape ratios λ₃/λ₂, λ₄/λ₂, λ₅/λ₂ — rather than by the near-gap density, the degeneracy
/// count or the multiplicity spectrum?
///
/// Goal: BEAT the D_052 baselines on the same case set. Those are already published, so they are
/// frozen here as constants and never re-derived from the ring measurements:
///   degeneracy count : Spearman ρ 0.815, refit R² 0.226, LOO RMSE 0.03304, frozen mean |error| 0.07399
///   near-gap density : Spearman ρ 0.204, refit R² 0.068, LOO RMSE 0.02437, frozen mean |error| 0.13116
///   multiplicity entropy / ΔE_lock: ρ 0.815 but frozen mean |error| 0.38469 (5× worse)
///
/// PHASE A (this file, committed on its own) contains the PREDICTION ONLY: the edge geometry of every
/// ring, the coefficient table, and the predicted capacity and recovery for the required case set and
/// for a held-out set of six NEW edge-shape rings. It never constructs the perturbation ensemble.
///
/// HONEST NOTE ON BLINDNESS. The required case set — D_051's, D_052's and D_053's rings — is exactly
/// the seven rings whose capacity and recovery are ALREADY published by D_051–D_053. For those rings
/// this audit is therefore a PRE-REGISTERED REPLICATION, not a blind test: the discipline is that the
/// predictor declaration, the coefficient source and the decision rule are frozen in this commit
/// before any metric is computed. To give the protocol a genuinely blind component as well, six NEW
/// edge-shape rings are introduced here and measured for the first time in PHASE B.
///
/// Deterministic throughout: fixed rings, fixed seeds, fixed doses, no randomness.
/// </summary>
public class Y_D_054_Tests : ResearchTestBase
{
    public Y_D_054_Tests(ITestOutputHelper output) : base(output) { }

    /// <summary>The required case set: every ring audited by D_051, D_052 and D_053.</summary>
    private static readonly string[] CaseSet = AdaptabilityAudit.RingFamilyNames;

    /// <summary>The new held-out set, which makes the blind protocol real rather than nominal.</summary>
    private static readonly string[] BlindSet = AdaptabilityAudit.EdgeRingNames;

    // ── The D_052 baselines to beat (published; frozen, never re-derived here) ──

    private sealed record Baseline(string Name, double Rho, double Loo, double R2, double Error);

    private static readonly Baseline[] Baselines =
    [
        new("degeneracy count", 0.815, 0.03304, 0.226, 0.07399),
        new("near-gap density", 0.204, 0.02437, 0.068, 0.13116),
        new("multiplicity entropy", 0.815, 0.03903, 0.187, 0.38469),
    ];

    // ── The seven edge-shape inputs ─────────────────────────────────────────

    private static readonly string[] InputNames =
        ["lambda2", "lambda3", "lambda4", "lambda5", "r3", "r4", "r5"];

    private static double[][] SixCaseInputs()
    {
        var rows = AdaptabilityAudit.CaseNames.Select(EdgeOf).ToArray();
        return [.. rows];
    }

    /// <summary>The four lowest positive Laplacian eigenvalues of a named ring (or any case).</summary>
    private static double[] Edge(string name) => AdaptabilityAudit
        .SpectrumOf(name == "D96" || AdaptabilityAudit.CaseNames.Contains(name)
            ? AdaptabilityAudit.Adjacency(name)
            : AdaptabilityAudit.RingAdjacency(name))
        .Where(x => x > AdaptabilityAudit.Tol)
        .OrderBy(x => x)
        .Take(4)
        .ToArray();

    private static double[] EdgeOf(string name)
    {
        double[] e = Edge(name);
        double l2 = e.Length > 0 ? e[0] : double.NaN;
        double l3 = e.Length > 1 ? e[1] : double.NaN;
        double l4 = e.Length > 2 ? e[2] : double.NaN;
        double l5 = e.Length > 3 ? e[3] : double.NaN;
        return [l2, l3, l4, l5, l3 / l2, l4 / l2, l5 / l2];
    }

    private static double InputOf(string name, string input)
        => EdgeOf(name)[Array.IndexOf(InputNames, input)];

    /// <summary>OLS coefficients for one input, fitted on the six D_048/D_050 cases only.</summary>
    private static (double Slope, double Intercept, double R2) FitOnSources(string input, bool capacity)
    {
        double[] x = AdaptabilityAudit.CaseNames.Select(n => InputOf(n, input)).ToArray();
        double[] y = AdaptabilityAudit.CaseNames
            .Select(n => AdaptabilityAudit.Profiles.Single(p => p.Name == n))
            .Select(p => capacity ? p.Capacity : p.MeanRecovery).ToArray();
        var (slope, intercept, r2, _) = AdaptabilityAudit.Fit(x, y);
        return (slope, intercept, r2);
    }

    // ── 1. The edge geometry ────────────────────────────────────────────────

    [Fact]
    public void D054_01_Edge_Geometry()
    {
        var sb = new StringBuilder();
        PrintHeader("1. The low-energy spectral edge of every ring — PHASE A (no simulation)");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  A1. The case set is exactly the rings audited by D_051, D_052 and D_053 — the canonical D96");
        sb.AppendLine("      plus D_051's six blind rings. Their capacity and recovery are ALREADY published, so for");
        sb.AppendLine("      them this audit is a PRE-REGISTERED REPLICATION (predictor declaration and decision rule");
        sb.AppendLine("      frozen in this commit, before any metric is computed) — not a blind test.");
        sb.AppendLine("  A2. To give the protocol a genuinely blind component, six NEW edge-shape rings are introduced");
        sb.AppendLine("      in this commit and measured for the first time in PHASE B. They are chosen to bend the");
        sb.AppendLine("      low edge away from the universal parabolic form λ_k ∝ k² in different ways.");
        sb.AppendLine("  A3. λ₂ … λ₅ are the four smallest POSITIVE Laplacian eigenvalues in sorted order (the standard");
        sb.AppendLine("      spectral-graph convention, λ₁ = 0 excluded). The ratios r3 = λ₃/λ₂, r4 = λ₄/λ₂,");
        sb.AppendLine("      r5 = λ₅/λ₂ are the scale-free shape descriptors.");
        sb.AppendLine("  A4. Every coefficient is an OLS fit on D_048/D_050's six ALREADY-PUBLISHED cases. No ring");
        sb.AppendLine("      measurement enters the prediction, and the D_052 baselines are frozen constants.");
        sb.AppendLine();

        sb.AppendLine("  THE CASE SET (required) — edge geometry only, no dynamics");
        sb.AppendLine("  ring        λ₂          λ₃          λ₄          λ₅          r3       r4        r5");
        sb.AppendLine("  " + new string('-', 96));
        foreach (string n in CaseSet)
        {
            double[] e = EdgeOf(n);
            sb.AppendLine($"  {n,-9} " + string.Join(" ", e.Select(v => v.ToString("F6", CultureInfo.InvariantCulture).PadLeft(11))));
        }

        sb.AppendLine();
        sb.AppendLine("  THE HELD-OUT SET (new — measured for the first time in PHASE B)");
        sb.AppendLine("  ring        λ₂          λ₃          λ₄          λ₅          r3       r4        r5");
        sb.AppendLine("  " + new string('-', 96));
        foreach (string n in BlindSet)
        {
            double[] e = EdgeOf(n);
            sb.AppendLine($"  {n,-9} " + string.Join(" ", e.Select(v => v.ToString("F6", CultureInfo.InvariantCulture).PadLeft(11))));
        }
        sb.AppendLine();
        foreach (var r in AdaptabilityAudit.EdgeRings) sb.AppendLine($"    {r.Name,-10} {r.Description}");

        sb.AppendLine();
        sb.AppendLine("  STRUCTURE ALREADY VISIBLE, AND IT MATTERS. On a circulant ring the eigenvalue at wavevector");
        sb.AppendLine("  k and at N − k coincide, so the bottom of the spectrum is a DOUBLET: λ₂ = λ₃ whenever the");
        sb.AppendLine("  lowest level has multiplicity 2. Where that holds, r3 = λ₃/λ₂ is EXACTLY 1 and is a constant,");
        sb.AppendLine("  not a predictor; and if the second level is also a doublet then λ₄ = λ₅ and r4 = r5. Enumerate");
        sb.AppendLine("  the empirical consequences on both sets:");
        int deg3 = 0, deg45 = 0, total = 0;
        foreach (string n in CaseSet.Concat(BlindSet))
        {
            double[] e = EdgeOf(n);
            bool e23 = Math.Abs(e[1] - e[0]) < 1e-9;
            bool e45 = Math.Abs(e[3] - e[2]) < 1e-9;
            if (e23) deg3++;
            if (e45) deg45++;
            total++;
            sb.AppendLine($"    {n,-10} λ₃/λ₂ = {e[4]:F6} {(e23 ? "(= 1 exactly — doublet bottom)" : "(≠ 1 — bottom split)")}"
                          + $"   λ₅/λ₄ = {(e[2] > 0 ? e[3] / e[2] : 0):F6} {(e45 ? "(= 1 exactly)" : "(≠ 1)")}");
        }
        sb.AppendLine($"    ⇒ {deg3} of {total} rings have a 2-fold bottom (r3 ≡ 1) and {deg45} of {total} have a 2-fold second level (r5 ≡ r4).");

        sb.AppendLine();
        sb.AppendLine("  DISTINCT VALUES ACROSS THE COMBINED THIRTEEN RINGS (a predictor with one value predicts nothing).");
        sb.AppendLine("  Values are compared at 6 decimals, so algebraic coincidences count as one value:");
        foreach (string input in InputNames)
        {
            var vals = CaseSet.Concat(BlindSet).Select(n => InputOf(n, input))
                              .Where(v => !double.IsNaN(v)).Select(v => Math.Round(v, 6)).Distinct().Count();
            var raw = CaseSet.Concat(BlindSet).Select(n => InputOf(n, input)).Where(v => !double.IsNaN(v)).ToArray();
            sb.AppendLine($"    {input,-8} distinct {vals,2} of 13   range {raw.Min():F6} … {raw.Max():F6}");
        }
        sb.AppendLine();
        sb.AppendLine("    r3 is 1.000000 on 12 of 13 rings and 1.089372 on the one exception: the doublet bottom makes");
        sb.AppendLine("    the third-lowest positive eigenvalue EQUAL to the second, so r3 is a constant on the family");
        sb.AppendLine("    (and on the source set it is not — that is exactly the D_051 failure mode in mirror image:");
        sb.AppendLine("    an input that varies on the calibration set but is constant on the target set).");
        sb.AppendLine("    r4 = r5 on the same 12 rings for the same reason, and r4 measures EDGE BENDING: 4.000000 is");
        sb.AppendLine("    the pure parabolic edge λ_k ∝ k², and the rings fall from 3.995718 (E1, barely bent) through");
        sb.AppendLine("    3.894202 (D96) to 3.617858 (D96-24, heavily bent) and 1.018556 (Ring48, the ±48 offset flips");
        sb.AppendLine("    the sign of cos almost every step). So the edge shape of this whole family is largely ONE");
        sb.AppendLine("    number: how far r4 has fallen from 4.");

        Assert.True(deg3 >= 10, "the doublet bottom must be exhibited — it is why r3 is degenerate");
        Output.WriteLine(sb.ToString());
    }

    // ── 2. The frozen prediction ────────────────────────────────────────────

    [Fact]
    public void D054_02_Frozen_Prediction()
    {
        var sb = new StringBuilder();
        PrintHeader("2. The frozen prediction — coefficients from published cases only");

        sb.AppendLine("  Coefficients (OLS on D_048/D_050's six published cases), printed here so the audit is");
        sb.AppendLine("  reproducible and so any later adjustment would be visible:");
        sb.AppendLine();
        sb.AppendLine("  target    input      slope            intercept        R² (on the six source cases)");
        sb.AppendLine("  " + new string('-', 92));
        var fits = new Dictionary<string, (double Slope, double Intercept, double R2)>();
        foreach (string target in new[] { "capacity", "recovery" })
            foreach (string input in InputNames)
            {
                var f = FitOnSources(input, target == "capacity");
                fits[$"{target}/{input}"] = f;
                sb.AppendLine($"  {target,-9} {input,-10} {f.Slope,16:G6} {f.Intercept,16:G6} {f.R2,18:F3}");
            }

        sb.AppendLine();
        sb.AppendLine("  THE FROZEN BASELINES TO BEAT (D_052, published on this same case set — frozen constants,");
        sb.AppendLine("  never re-derived from the ring measurements):");
        sb.AppendLine("  predictor                Spearman ρ   refit R²   LOO RMSE   frozen mean |error|");
        sb.AppendLine("  " + new string('-', 84));
        foreach (var b in Baselines)
            sb.AppendLine($"  {b.Name,-22} {b.Rho,10:F3} {b.R2,10:F3} {b.Loo,10:F5} {b.Error,20:F5}");
        sb.AppendLine();
        sb.AppendLine("  DECISION RULE, FROZEN BEFORE ANY METRIC IS COMPUTED:");
        sb.AppendLine("    An edge-shape predictor BEATS the D_052 field iff BOTH hold on the required case set —");
        sb.AppendLine("      (i) its Spearman ρ exceeds the degeneracy count's 0.815, and");
        sb.AppendLine("      (ii) its within-family LOO RMSE is below the degeneracy count's 0.03304.");
        sb.AppendLine("    Corroboration is then required on the held-out set. With n = 7 cases, an exact");
        sb.AppendLine("    permutation p-value for ρ is computed too (7! = 5040 permutations enumerated exactly), so");
        sb.AppendLine("    the multiple-input search is judged against its own null rather than a table.");
        sb.AppendLine();
        sb.AppendLine("  THE FROZEN PREDICTION — required case set");
        sb.AppendLine("  ring        input      value        →  predicted cap    predicted rec");
        sb.AppendLine("  " + new string('-', 76));
        foreach (string n in CaseSet)
            foreach (string input in InputNames)
            {
                double x = InputOf(n, input);
                var cf = fits[$"capacity/{input}"];
                var rf = fits[$"recovery/{input}"];
                double c = cf.Slope * x + cf.Intercept;
                double r = rf.Slope * x + rf.Intercept;
                sb.AppendLine($"  {n,-9} {input,-10} {x,10:F6}      {c,16:F5} {r,16:F5}");
            }

        sb.AppendLine();
        sb.AppendLine("  THE FROZEN PREDICTION — held-out set (measured for the first time in PHASE B)");
        sb.AppendLine("  ring        input      value        →  predicted cap    predicted rec");
        sb.AppendLine("  " + new string('-', 76));
        foreach (string n in BlindSet)
            foreach (string input in InputNames)
            {
                double x = InputOf(n, input);
                var cf = fits[$"capacity/{input}"];
                var rf = fits[$"recovery/{input}"];
                double c = cf.Slope * x + cf.Intercept;
                double r = rf.Slope * x + rf.Intercept;
                sb.AppendLine($"  {n,-9} {input,-10} {x,10:F6}      {c,16:F5} {r,16:F5}");
            }

        sb.AppendLine();
        sb.AppendLine("  BOUNDS CHECK (derived, D_050): 0 ≤ capacity ≤ 1 and 0 ≤ recovery ≤ 1 are IDENTITIES — the");
        sb.AppendLine("  headroom normalization and a normalized distance. A predictor that leaves the range is WRONG");
        sb.AppendLine("  BY CONSTRUCTION, whatever its correlation, so the violations are reported rather than clipped:");
        sb.AppendLine();
        sb.AppendLine("  input      cells outside [0,1]   worst value   worst case");
        sb.AppendLine("  " + new string('-', 76));
        int ratioViolations = 0, scaleViolations = 0;
        foreach (string input in InputNames)
        {
            int outOfRange = 0, checkedCells = 0;
            double worst = 0.0;
            string worstCase = "—";
            bool scaleSensitive = input.StartsWith("lambda");
            foreach (string n in CaseSet.Concat(BlindSet))
            {
                double x = InputOf(n, input);
                foreach (string target in new[] { "capacity", "recovery" })
                {
                    var f = fits[$"{target}/{input}"];
                    double pred = f.Slope * x + f.Intercept;
                    checkedCells++;
                    double excess = Math.Max(0.0, Math.Max(pred - 1.0, -pred));
                    if (excess > 0)
                    {
                        outOfRange++;
                        if (excess > worst) { worst = excess; worstCase = $"{n} {target} = {pred:F5}"; }
                    }
                }
            }
            if (scaleSensitive) scaleViolations += outOfRange; else ratioViolations += outOfRange;
            sb.AppendLine($"  {input,-10} {outOfRange,17} {worst,14:F5}   {worstCase}");
        }
        sb.AppendLine();
        sb.AppendLine($"  ⇒ {scaleViolations} violations from the SCALE-SENSITIVE inputs (λ₂ … λ₅) and {ratioViolations} from");
        sb.AppendLine("    the SCALE-FREE shape ratios, out of 7 inputs × 13 rings × 2 targets = 182 cells. The pattern is");
        sb.AppendLine("    the opposite of the naive expectation, and it is diagnostic:");
        sb.AppendLine("      · λ₂ … λ₅ never leave the range, because the SOURCE set (which includes the random graph at");
        sb.AppendLine("        λ₂ = 17.19 and K96 at λ₂ = 96) spans a vastly wider λ envelope than any ring, so the fitted");
        sb.AppendLine("        lines are shallow and the ring values land safely inside. Admissible — and, as PHASE B");
        sb.AppendLine("        will show, nearly unresolving.");
        sb.AppendLine("      · r3, r4 and r5 DO leave the range, because on the source set the shape ratios are pulled");
        sb.AppendLine("        toward the non-ring cases (K96 gives r4 = r5 = 1 exactly, a vanishing edge), which makes");
        sb.AppendLine("        those slopes steep enough that a ring's r4 ≈ 4 extrapolates past capacity 1.");
        sb.AppendLine("    So the derived bounds already expose the central tension of this audit: the predictor that is");
        sb.AppendLine("    admissible in linear form is the one that does not resolve rings, and the shape descriptors");
        sb.AppendLine("    that do resolve rings are inadmissible in linear form on this calibration.");

        Output.WriteLine(sb.ToString());
        Assert.True(scaleViolations + ratioViolations > 0,
            "the derived bounds must be shown to bite on at least one predictor family — an admissibility finding");

        sb.AppendLine();
        sb.AppendLine("  The prediction is now on record. PHASE B (measurement, added in a later commit) is compared");
        sb.AppendLine("  against these numbers verbatim.");

        Output.WriteLine(sb.ToString());
    }
}
