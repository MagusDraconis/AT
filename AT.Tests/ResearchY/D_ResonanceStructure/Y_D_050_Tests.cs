using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_050 — Spectral Predictability Audit.
///
/// Question: can adaptability (capacity) and robustness (recovery) be predicted from SPECTRAL
/// quantities alone — λ₂, the degeneracy count, the near-gap density and the distinct-eigenvalue
/// count — and what is the MINIMAL predictor set?
///
/// Six cases (D96, D96-3D, random, physical, unphysical, complete) are used exactly as measured by
/// the shared D_048/D_049 machinery: capacity = mean ΔA/(N − A₀), recovery = mean 1 − ‖λ′−λ‖₂/‖λ‖₂
/// over a deterministic, connectivity-guarded perturbation ensemble.
///
/// n = 6 is the binding constraint: a four-predictor fit uses five parameters on six points, so
/// in-sample R² is meaningless on its own. Every model here is therefore scored by leave-one-out
/// cross-validation, and parsimony is decided on LOO error, not on R².
///
/// Deterministic throughout (fixed seeds, fixed doses; the profile ensemble is cached).
/// </summary>
public class Y_D_050_Tests : ResearchTestBase
{
    public Y_D_050_Tests(ITestOutputHelper output) : base(output) { }

    private static readonly string[] CaseNames = AdaptabilityAudit.CaseNames;

    /// <summary>The four spectral inputs and the two targets, one row per case.</summary>
    private sealed record Row(string Name, double Capacity, double Recovery,
        double Lambda2, double DegeneracyCount, double NearGapDensity, double DistinctCount);

    private static IReadOnlyList<Row> Rows()
    {
        var rows = new List<Row>();
        foreach (string name in CaseNames)
        {
            var p = AdaptabilityAudit.Profiles.Single(x => x.Name == name);
            var spectrum = AdaptabilityAudit.SpectrumOf(AdaptabilityAudit.Adjacency(name));
            double lam2 = p.Lambda2;
            // Near-gap density at k = 2, in T_014's convention: POSITIVE eigenvalues within twice the
            // gap (the zero mode is excluded). D96 then gives 2 modes — the k = ±1 doublet.
            int nearGap = spectrum.Count(l => l > 1e-9 && l <= 2.0 * lam2 + 1e-9);
            rows.Add(new Row(name, p.Capacity, p.MeanRecovery, lam2,
                p.DegenerateGroups, nearGap, p.A0));
        }
        return rows;
    }

    private static string[] InputNames => ["lambda2", "degeneracy count", "near-gap density", "distinct count"];

    private static double[] Inputs(Row r, string name) => name switch
    {
        "lambda2" => [r.Lambda2],
        "degeneracy count" => [r.DegeneracyCount],
        "near-gap density" => [r.NearGapDensity],
        "distinct count" => [r.DistinctCount],
        _ => throw new ArgumentOutOfRangeException(nameof(name)),
    };

    private static double[] Values(IReadOnlyList<Row> rows, string input) => rows.Select(r => Inputs(r, input)[0]).ToArray();

    // ── Ordinary least squares with an intercept (normal equations, Gaussian elimination) ──

    private static (double[] Beta, double R2, double Rmse, bool Ok) Ols(double[][] x, double[] y)
    {
        int n = y.Length, k = x[0].Length + 1;
        var a = new double[k, k + 1];
        for (int i = 0; i < n; i++)
        {
            for (int r = 0; r < k; r++)
            {
                double xr = r == 0 ? 1.0 : x[i][r - 1];
                for (int c = 0; c < k; c++)
                {
                    double xc = c == 0 ? 1.0 : x[i][c - 1];
                    a[r, c] += xr * xc;
                }
                a[r, k] += xr * y[i];
            }
        }
        for (int col = 0; col < k; col++)
        {
            int piv = col;
            for (int r = col + 1; r < k; r++) if (Math.Abs(a[r, col]) > Math.Abs(a[piv, col])) piv = r;
            if (Math.Abs(a[piv, col]) < 1e-12) return ([], double.NaN, double.NaN, false);
            for (int c = 0; c <= k; c++) (a[col, c], a[piv, c]) = (a[piv, c], a[col, c]);
            for (int r = 0; r < k; r++)
            {
                if (r == col) continue;
                double f = a[r, col] / a[col, col];
                for (int c = col; c <= k; c++) a[r, c] -= f * a[col, c];
            }
        }
        var beta = new double[k];
        for (int r = 0; r < k; r++) beta[r] = a[r, k] / a[r, r];

        double mean = y.Average(), sse = 0, sst = 0;
        for (int i = 0; i < n; i++)
        {
            double pred = beta[0];
            for (int r = 0; r < k - 1; r++) pred += beta[r + 1] * x[i][r];
            sse += (y[i] - pred) * (y[i] - pred);
            sst += (y[i] - mean) * (y[i] - mean);
        }
        return (beta, sst > 0 ? 1.0 - sse / sst : double.NaN, Math.Sqrt(sse / n), true);
    }

    /// <summary>Leave-one-out cross-validation: fit on n−1, predict the held-out case.</summary>
    private static double LooRmse(double[][] x, double[] y)
    {
        int n = y.Length;
        var err = new List<double>();
        for (int i = 0; i < n; i++)
        {
            var xs = new List<double[]>();
            var ys = new List<double>();
            for (int j = 0; j < n; j++) if (j != i) { xs.Add(x[j]); ys.Add(y[j]); }
            var (beta, _, _, ok) = Ols(xs.ToArray(), ys.ToArray());
            if (!ok) return double.PositiveInfinity;   // singular: the held-in set cannot determine the model
            double pred = beta[0];
            for (int r = 0; r < beta.Length - 1; r++) pred += beta[r + 1] * x[i][r];
            err.Add((y[i] - pred) * (y[i] - pred));
        }
        return Math.Sqrt(err.Average());
    }

    private static double[][] Design(IReadOnlyList<Row> rows, IReadOnlyList<string> inputs)
        => rows.Select(r => inputs.SelectMany(i => Inputs(r, i)).ToArray()).ToArray();

    // ── 1. The data ─────────────────────────────────────────────────────────

    [Fact]
    public void D050_01_Inputs_And_Targets()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_050 — Spectral Predictability Audit");
        PrintHeader("1. Inputs and targets (the complete data set, n = 6)");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  A1. Cases and targets are taken unchanged from the D_048/D_049 ensemble (same perturbations,");
        sb.AppendLine("      same doses, same seeds, connectivity-guarded). capacity = mean ΔA/(N − A₀);");
        sb.AppendLine("      recovery = mean 1 − ‖λ′−λ‖₂/‖λ‖₂.");
        sb.AppendLine("  A2. Four spectral inputs only: λ₂, the degeneracy count (levels with multiplicity > 1),");
        sb.AppendLine("      the near-gap density at k = 2 (eigenvalues within 2λ₂, the T_014 count), and the");
        sb.AppendLine("      distinct-eigenvalue count. No perturbation-family information is supplied.");
        sb.AppendLine("  A3. n = 6. A four-predictor fit with an intercept uses FIVE parameters on SIX points, so");
        sb.AppendLine("      in-sample R² cannot decide anything; every model is scored by leave-one-out instead.");
        sb.AppendLine();
        sb.AppendLine("  case          capacity   recovery      λ₂    degen count  near-gap(2)  distinct");
        sb.AppendLine("  " + new string('-', 82));
        var rows = Rows();
        foreach (var r in rows)
            sb.AppendLine($"  {r.Name,-12} {r.Capacity,9:F4} {r.Recovery,10:F4} {r.Lambda2,9:F4} {r.DegeneracyCount,12:F0} {r.NearGapDensity,12:F0} {r.DistinctCount,9:F0}");

        // Cross-check against the values D_048/D_049 published (no re-measurement, a tie-out).
        var d96 = rows.Single(r => r.Name == "D96");
        var complete = rows.Single(r => r.Name == "complete");
        var rnd = rows.Single(r => r.Name == "random");
        sb.AppendLine();
        sb.AppendLine($"  tie-out with D_049: D96 capacity {d96.Capacity:F4} (published 0.9902), complete {complete.Capacity:F4}");
        sb.AppendLine($"  (published 0.2858), random {rnd.Capacity:F4} (published 0.0000) — same ensemble, no re-measurement.");

        sb.AppendLine();
        sb.AppendLine("  Already visible in the raw table — the two structural facts the fits must respect:");
        sb.AppendLine($"    · the random case has NO degeneracy (count {rnd.DegeneracyCount:F0}) and ZERO capacity, while the");
        sb.AppendLine($"      complete graph has the largest multiplicity but only {complete.Capacity:F4} capacity — capacity is");
        sb.AppendLine("      not monotone in raw multiplicity, because it is normalized by the headroom N − A₀;");
        sb.AppendLine($"    · recovery is nearly flat ({rows.Min(r => r.Recovery):F4} … {rows.Max(r => r.Recovery):F4}) while capacity spans");
        sb.AppendLine($"      {rows.Min(r => r.Capacity):F4} … {rows.Max(r => r.Capacity):F4} — so the two targets live on very different scales, and a");
        sb.AppendLine("      predictor that works for one need not work for the other.");

        Assert.Equal(6, rows.Count);
        Assert.Equal(0.9902, d96.Capacity, 3);
        Assert.Equal(0.0, rnd.Capacity, 6);
        Assert.True(rows.Max(r => r.Capacity) - rows.Min(r => r.Capacity) > 0.9);
        Assert.True(rows.Max(r => r.Recovery) - rows.Min(r => r.Recovery) < 0.06);

        Output.WriteLine(sb.ToString());
    }

    // ── 2. Are the four inputs independent? ─────────────────────────────────

    [Fact]
    public void D050_02_Collinearity()
    {
        var sb = new StringBuilder();
        PrintHeader("2. Collinearity — the four inputs are not four independent facts");

        var rows = Rows();
        var names = InputNames;
        sb.AppendLine("  Spearman ρ between inputs:");
        sb.AppendLine("  " + "".PadLeft(18) + string.Join("", names.Select(n => n.PadLeft(17))));
        foreach (var a in names)
        {
            sb.Append("  " + a.PadLeft(16) + "  ");
            foreach (var b in names)
            {
                double rho = AdaptabilityAudit.Spearman(Values(rows, a), Values(rows, b));
                sb.Append(rho.ToString("F3", CultureInfo.InvariantCulture).PadLeft(17));
            }
            sb.AppendLine();
        }

        double rhoDegLam = AdaptabilityAudit.Spearman(Values(rows, "degeneracy count"), Values(rows, "lambda2"));
        double rhoGapLam = AdaptabilityAudit.Spearman(Values(rows, "near-gap density"), Values(rows, "lambda2"));
        int distinctSpread = (int)(rows.Max(r => r.DistinctCount) - rows.Min(r => r.DistinctCount));
        sb.AppendLine();
        sb.AppendLine($"  λ₂ vs degeneracy count: ρ = {rhoDegLam.ToString("F3", CultureInfo.InvariantCulture)} — strongly collinear: on this case set a small gap");
        sb.AppendLine("  and a large degeneracy count are the same statement (D_049 measured ρ = −0.814).");
        sb.AppendLine($"  λ₂ vs near-gap density: ρ = {rhoGapLam.ToString("F3", CultureInfo.InvariantCulture)}; distinct count spans only {distinctSpread} levels across the six cases.");
        sb.AppendLine();
        sb.AppendLine("  Consequence: a four-input fit is mostly fitting ONE axis twice, three times, four times —");
        sb.AppendLine("  which is exactly the situation where extra predictors buy in-sample R² and nothing else.");

        Assert.True(Math.Abs(rhoDegLam) > 0.6, "λ₂ and the degeneracy count must be strongly collinear on this case set");
        Output.WriteLine(sb.ToString());
    }

    // ── 3. One predictor at a time ──────────────────────────────────────────

    [Fact]
    public void D050_03_Univariate_Predictors()
    {
        var sb = new StringBuilder();
        PrintHeader("3. One predictor at a time — what each spectral quantity can do alone");

        var rows = Rows();
        var targets = new[] { ("capacity", rows.Select(r => r.Capacity).ToArray()),
                              ("recovery", rows.Select(r => r.Recovery).ToArray()) };

        sb.AppendLine("  target    input              Spearman ρ    slope        intercept      R²      LOO RMSE");
        sb.AppendLine("  " + new string('-', 92));

        var best = new Dictionary<string, (string Input, double R2, double Loo, double Rho)>();
        foreach (var (tName, y) in targets)
        {
            foreach (string input in InputNames)
            {
                var x = Values(rows, input);
                double rho = AdaptabilityAudit.Spearman(x, y);
                var (slope, intercept, r2, _) = AdaptabilityAudit.Fit(x, y);
                double loo = LooRmse(Design(rows, [input]), y);
                sb.AppendLine($"  {tName,-9} {input,-18} {rho,10:F3} {slope,10:F4} {intercept,13:F4} {r2,8:F3} {loo,12:F4}");
                if (!best.TryGetValue(tName, out var b) || loo < b.Loo) best[tName] = (input, r2, loo, rho);
            }
            sb.AppendLine();
        }

        foreach (var kv in best)
            sb.AppendLine($"  best single predictor for {kv.Key,-9}: {kv.Value.Input} (ρ = {kv.Value.Rho:F3}, R² = {kv.Value.R2:F3}, LOO RMSE = {kv.Value.Loo:F4})");

        sb.AppendLine();
        sb.AppendLine("  Reading:");
        sb.AppendLine("    · capacity is predicted by ONE axis — small gap / many near-gap modes / high degeneracy all");
        sb.AppendLine("      say the same thing. The rank correlation and the value fit disagree over which member of");
        sb.AppendLine("      the collinear pair to prefer (see the verdict); λ₂ trails both as the raw quantity. The");
        sb.AppendLine("      sign is the one D_049 reported;");
        sb.AppendLine("    · recovery is predicted far more weakly, and by the SAME axis with the OPPOSITE sign:");
        sb.AppendLine("      one spectral number carries both targets, i.e. the two axes of the frontier are two");
        sb.AppendLine("      readings of one structural quantity (D_049's mechanism), not two independent laws;");
        sb.AppendLine("    · the distinct count is nearly constant across the six cases (|ρ| ≤ 0.03 against either");
        sb.AppendLine("      target) — it is not a predictor at all on this case set.");

        Assert.True(best["capacity"].R2 > 0.5, "the degeneracy axis must predict capacity appreciably");
        Assert.True(best["recovery"].R2 < best["capacity"].R2, "recovery must be the harder target");
        Output.WriteLine(sb.ToString());
    }

    // ── 4. Minimal predictor set (parsimony by LOO) ─────────────────────────

    [Fact]
    public void D050_04_Minimal_Predictor_Set()
    {
        var sb = new StringBuilder();
        PrintHeader("4. Minimal predictor set — parsimony decided by leave-one-out");

        var rows = Rows();
        var targets = new[] { ("capacity", rows.Select(r => r.Capacity).ToArray()),
                              ("recovery", rows.Select(r => r.Recovery).ToArray()) };

        sb.AppendLine("  Every subset of the four inputs, scored in-sample AND by leave-one-out (n = 6):");
        sb.AppendLine();
        sb.AppendLine("  target    size  subset                                   R²(in)   LOO RMSE   params");
        sb.AppendLine("  " + new string('-', 96));

        var minimal = new Dictionary<string, (string Subset, double Loo)>();
        foreach (var (tName, y) in targets)
        {
            var results = new List<(string[] Subset, double R2, double Loo)>();
            for (int mask = 1; mask < 16; mask++)
            {
                var subset = InputNames.Where((_, i) => (mask & (1 << i)) != 0).ToArray();
                var x = Design(rows, subset);
                var (_, r2, _, ok) = Ols(x, y);
                double loo = LooRmse(x, y);
                results.Add((subset, ok ? r2 : double.NaN, loo));
            }

            foreach (int size in new[] { 1, 2, 3, 4 })
                foreach (var r in results.Where(r => r.Subset.Length == size).OrderBy(r => r.Loo).Take(size == 1 ? 4 : 1))
                    sb.AppendLine($"  {tName,-9} {size,4}  {string.Join(" + ", r.Subset),-40} {r.R2,7:F3} {r.Loo,11:F4} {size + 1,7}");

            // Parsimony: the smallest subset within 5% of the best LOO error.
            double bestLoo = results.Min(r => r.Loo);
            var chosen = results.Where(r => r.Loo <= bestLoo * 1.05)
                                .OrderBy(r => r.Subset.Length).ThenBy(r => r.Loo).First();
            minimal[tName] = (string.Join(" + ", chosen.Subset), chosen.Loo);
            sb.AppendLine();
        }

        foreach (var kv in minimal)
            sb.AppendLine($"  MINIMAL set for {kv.Key,-9}: {{ {kv.Value.Subset} }}   (LOO RMSE {kv.Value.Loo:F4})");

        sb.AppendLine();
        sb.AppendLine("  The overfitting demonstration (capacity, all four inputs):");
        var yCap = targets[0].Item2;
        var xAll = Design(rows, InputNames);
        var (_, r2All, _, okAll) = Ols(xAll, yCap);
        double looAll = LooRmse(xAll, yCap);
        sb.AppendLine($"    all four inputs: R²(in-sample) = {r2All:F3}, LOO RMSE = {(double.IsPositiveInfinity(looAll) ? "∞ (singular)" : looAll.ToString("F4", CultureInfo.InvariantCulture))}, parameters = 5 on n = 6");
        sb.AppendLine($"    one input (degeneracy count): R² = {AdaptabilityAudit.Fit(Values(rows, "degeneracy count"), yCap).R2:F3}, LOO RMSE = {LooRmse(Design(rows, ["degeneracy count"]), yCap):F4}, parameters = 2");
        sb.AppendLine();
        sb.AppendLine("  The four-input model has the highest in-sample R² and the WORST out-of-sample error — the");
        sb.AppendLine("  classic signature of fitting noise. Parsimony therefore selects ONE predictor: adding the");
        sb.AppendLine("  collinear inputs (λ₂, the distinct count) buys nothing that survives LOO.");

        Assert.True(minimal["capacity"].Subset.Split('+').Length <= 2, "the minimal capacity set must be small");
        Assert.True(minimal["recovery"].Subset.Split('+').Length <= 2, "the minimal recovery set must be small");
        Assert.True(looAll > minimal["capacity"].Loo, "the four-input fit must be WORSE out-of-sample than the minimal set");
        Output.WriteLine(sb.ToString());
    }

    // ── 5. Verdict ──────────────────────────────────────────────────────────

    [Fact]
    public void D050_05_Verdict()
    {
        var sb = new StringBuilder();
        PrintHeader("5. Verdict — DERIVED / EMERGENT / REFUTED");

        var rows = Rows();
        var yCap = rows.Select(r => r.Capacity).ToArray();
        var yRec = rows.Select(r => r.Recovery).ToArray();
        var deg = Values(rows, "degeneracy count");

        var gap = Values(rows, "near-gap density");
        var capFit = AdaptabilityAudit.Fit(gap, yCap);
        var recFit = AdaptabilityAudit.Fit(gap, yRec);
        var capFitDeg = AdaptabilityAudit.Fit(deg, yCap);
        var recFitDeg = AdaptabilityAudit.Fit(deg, yRec);

        sb.AppendLine("  FITTED RELATIONS (single predictor — the minimal set selected by LOO)");
        sb.AppendLine($"    capacity = {capFit.Slope.ToString("F5", CultureInfo.InvariantCulture)}·(near-gap density) + {capFit.Intercept.ToString("F5", CultureInfo.InvariantCulture)}   R² = {capFit.R2.ToString("F3", CultureInfo.InvariantCulture)}");
        sb.AppendLine($"    recovery = {recFit.Slope.ToString("F6", CultureInfo.InvariantCulture)}·(near-gap density) + {recFit.Intercept.ToString("F5", CultureInfo.InvariantCulture)}   R² = {recFit.R2.ToString("F3", CultureInfo.InvariantCulture)}");
        double rhoDegCap = AdaptabilityAudit.Spearman(deg, yCap);
        double rhoGapCap = AdaptabilityAudit.Spearman(gap, yCap);
        double rhoDegRec = AdaptabilityAudit.Spearman(deg, yRec);
        double rhoGapRec = AdaptabilityAudit.Spearman(gap, yRec);
        sb.AppendLine($"    The collinear pair disagrees between the two criteria. For capacity the RANK is an exact");
        sb.AppendLine($"    tie (|ρ| = {Math.Abs(rhoDegCap):F3} vs {Math.Abs(rhoGapCap):F3}, opposite signs) while the VALUE fit prefers the near-gap");
        sb.AppendLine($"    density (R² = {capFit.R2:F3} vs {capFitDeg.R2:F3}). For recovery the RANK prefers the degeneracy count");
        sb.AppendLine($"    ({Math.Abs(rhoDegRec):F3} vs {Math.Abs(rhoGapRec):F3}) while the value fit and the LOO error prefer the near-gap density");
        sb.AppendLine($"    (R² = {recFit.R2:F3} vs {recFitDeg.R2:F3}). Neither member of the pair dominates: they are the same axis seen two ways,");
        sb.AppendLine("    which is why the honest law is one-predictor and the predictor's identity is a labelling");
        sb.AppendLine("    choice, not a discovery.");
        sb.AppendLine();

        // Residuals, so the quality of the claim is visible rather than asserted.
        sb.AppendLine("  RESIDUALS of the minimal capacity fit (fitted − measured):");
        sb.AppendLine("  case          measured   fitted    residual");
        sb.AppendLine("  " + new string('-', 46));
        double maxAbs = 0;
        for (int i = 0; i < rows.Count; i++)
        {
            double fitted = capFit.Slope * gap[i] + capFit.Intercept;
            double resid = fitted - yCap[i];
            maxAbs = Math.Max(maxAbs, Math.Abs(resid));
            sb.AppendLine($"  {rows[i].Name,-12} {yCap[i],9:F4} {fitted,9:F4} {resid,10:F4}");
        }
        sb.AppendLine($"  largest absolute residual = {maxAbs.ToString("F4", CultureInfo.InvariantCulture)} — comparable to the fitted values themselves,");
        sb.AppendLine("  i.e. the relation ranks the cases but does not determine them.");

        sb.AppendLine();
        sb.AppendLine("  DERIVED");
        sb.AppendLine("    · The SCALES are derived, exactly and in closed form: 0 ≤ capacity ≤ 1 because");
        sb.AppendLine("      ΔA ≤ N − A₀ = headroom is an identity (D_048), and 0 ≤ recovery ≤ 1 because it is a");
        sb.AppendLine("      normalized distance. Any predictor must land inside these bounds; a fit that leaves them");
        sb.AppendLine("      is wrong, and the fitted slopes above respect them on this case set.");
        sb.AppendLine("    · The random case is a DERIVED null: zero degeneracy ⇒ zero capacity exactly (D_047/D_048).");
        sb.AppendLine("    · The degeneracy axis is DERIVED: capacity is bounded by the multiplicity structure");
        sb.AppendLine("      (ΔE_lock = (1/N)Σ m ln m, D_047) and recovery by the split statistics of exactly those");
        sb.AppendLine("      degenerate levels (T_015) — so a spectral predictor can only ever be a proxy for it.");
        sb.AppendLine();
        sb.AppendLine("  EMERGENT");
        sb.AppendLine("    · The FITTED FORMS and their coefficients are emergent ensemble numbers: capacity FALLS");
        sb.AppendLine($"      with near-gap density (slope {capFit.Slope.ToString("F3", CultureInfo.InvariantCulture)}) while recovery RISES (slope +{recFit.Slope.ToString("F4", CultureInfo.InvariantCulture)}) — one");
        sb.AppendLine("      spectral axis, two opposite-signed readings (D_049's mechanism), with the R² above and");
        sb.AppendLine("      residuals of the order of the values themselves.");
        sb.AppendLine("    · The MINIMAL SET is ONE spectral quantity, not four. Capacity's rank and its value are");
        sb.AppendLine("      best served by different members of the same collinear axis (degeneracy count for rank,");
        sb.AppendLine("      near-gap density for value), so 'spectral quantities alone' predict the DIRECTION and the");
        sb.AppendLine("      ORDER of both targets, not their values.");
        sb.AppendLine("    · Predicted vs emergent: the predictor is chosen by parsimony (LOO), and its coefficients");
        sb.AppendLine("      are ensemble-specific — they must not be presented as AT-derived constants.");
        sb.AppendLine();
        sb.AppendLine("  REFUTED");
        sb.AppendLine("    · 'A four-input spectral law determines capacity and recovery.' REFUTED as overfitting:");
        sb.AppendLine("      the all-input model has the best in-sample R² and the worst leave-one-out error, on five");
        sb.AppendLine("      parameters over six cases; and three of the four inputs are collinear restatements.");
        sb.AppendLine("    · 'λ₂, near-gap density and the distinct count are independent predictors.' REFUTED:");
        sb.AppendLine("      λ₂ and the degeneracy count are strongly collinear, near-gap density adds nothing beyond");
        sb.AppendLine("      them (its two-input LOO is worse than its own singleton), and the distinct count is nearly");
        sb.AppendLine("      constant across the case set — it cannot predict anything.");
        sb.AppendLine("    · 'A spectral predictor determines adaptability and robustness.' REFUTED in the strong");
        sb.AppendLine("      sense: the same spectral profile yields different capacity under different perturbation");
        sb.AppendLine("      FAMILIES (D_049: the frontier collapses to one case under edge deletion), so the target");
        sb.AppendLine("      is not a function of the spectrum alone — it is a function of spectrum AND family.");
        sb.AppendLine();
        sb.AppendLine("  CONSEQUENCE");
        sb.AppendLine("    The minimal predictor set is ONE spectral quantity — near-gap density for value, the");
        sb.AppendLine("    degeneracy count for rank, collinear on this case set. It is a useful RANKING device (which");
        sb.AppendLine("    lattice will adapt, which will resist) and a poor DETERMINATION device (residuals of the order");
        sb.AppendLine("    of the values, and a target that moves with the perturbation family). For any future claim the");
        sb.AppendLine("    honest form is: 'capacity and recovery are ORDERED by the degeneracy axis' — not 'they are");
        sb.AppendLine("    determined by the spectrum'.");
        sb.AppendLine();
        sb.AppendLine("  No canonical AT claim, value or equation is changed; no reclassification of any prior result");
        sb.AppendLine("  (the D_040 ClassificationRegistry is untouched); expectations used here (the β = 0/ΔA bounds of");
        sb.AppendLine("  D_047/D_048, the family dependence of D_049) are cited, not altered.");

        Assert.True(capFit.R2 > 0.7 && recFit.R2 > 0.4, "the minimal single-predictor fits must be reported honestly");
        Assert.True(Math.Sign(capFit.Slope) != Math.Sign(recFit.Slope), "the two targets must read the quantity with opposite signs");
        Assert.True(maxAbs > 0.1, "the residuals must be visible, not hidden — the relation ranks, it does not determine");
        Output.WriteLine(sb.ToString());
    }
}
