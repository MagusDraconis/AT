using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_049 — Adaptability–Robustness Frontier Audit.
///
/// Question: is there a UNIVERSAL tradeoff  Adaptability × Robustness = const ?
///
/// Measures (from <see cref="AdaptabilityAudit"/>, the shared D_048/D_049 machinery):
///   capacity  = mean ΔA/(N − A₀)  — the adaptive capacity (fraction of latent capacity realized)
///   recovery  = mean 1 − ‖λ′−λ‖₂/‖λ‖₂ — spectral retention (robustness)
///   λ₂        = the spectral gap
///   degeneracy= the number of degenerate eigenspaces (multiplicity &gt; 1), and L = (N − A₀)/N
///
/// Cases: D96, D96^3 (the 3-dimensional D96 variant, the 4×4×6 periodic torus, N = 96),
/// random, complete, physical, unphysical.
///
/// Fit: capacity vs recovery — a linear OLS fit C = a + b·R, plus the power law C = k·Δ^β in
/// the damage Δ = 1 − recovery, with the free fit compared against the CONSERVED-PRODUCT law
/// C·Δ = const (slope −1) forced through the same data.
///
/// VERDICTS FOUND:
///   REFUTED  (universal constant product): the product C·R spans 0 → 0.956 (the null gives
///            exactly 0) and, among the five non-null cases, 0.282 → 0.956 — a 3.39× span
///            with CV 0.33. The conserved-product law has the WRONG SIGN: capacity RISES with
///            damage (fitted exponent +0.74, sub-linear), whereas C·Δ = const demands −1;
///            forcing −1 gives R² = −3.28 against R² = +0.72 for the free power law — worse
///            than predicting the mean.
///   DERIVED  (the frontier): the (capacity, recovery) Pareto frontier of the six cases has
///            exactly TWO members — D96 (maximum adaptability) and complete (maximum
///            robustness). The other four graphs are strictly DOMINATED: they give up
///            adaptability without buying robustness. A tradeoff therefore exists, but it is
///            a 2-point frontier, not a constant.
///   DERIVED  (the mechanism): ONE structural quantity governs both axes with opposite signs —
///            the degeneracy COUNT (ρ(capacity) = +0.89, ρ(recovery) = −0.54), equivalently
///            the inverse gap (ρ(capacity) = −0.84, ρ(recovery) = +0.46). The degeneracy
///            FRACTION L is a far weaker predictor (ρ = −0.03): the count indexes the tradeoff.
///   EMERGENT : the fitted exponent β ≈ 0.74, the constant k ≈ 8.0, the particular dominated
///            set, and the frontier INSTABILITY across perturbation families — under edge
///            deletion the frontier collapses to a single case (no tradeoff at all), under
///            edge addition it has four members.
///
/// Quantitative only: no AT theoretical assumptions are used anywhere in this audit.
/// Deterministic: the shared fixed LCG ensemble (4 perturbation types × 5 doses × 3 seeds).
/// </summary>
public class Y_D_049_Tests : ResearchTestBase
{
    public Y_D_049_Tests(ITestOutputHelper output) : base(output) { }

    private static IReadOnlyList<AdaptabilityProfile> Profiles => AdaptabilityAudit.Profiles;
    private static AdaptabilityProfile P(string name) => AdaptabilityAudit.P(name);

    /// <summary>Pareto frontier for "maximize both" — returns the non-dominated case names.</summary>
    private static List<string> Pareto(IEnumerable<(string Name, double C, double R)> pts)
    {
        var list = pts.ToList();
        return list.Where(a => !list.Any(b =>
                b.Name != a.Name && b.C >= a.C && b.R >= a.R && (b.C > a.C || b.R > a.R)))
            .Select(a => a.Name)
            .ToList();
    }

    private static List<string> AggregateFrontier()
        => Pareto(Profiles.Select(p => (p.Name, p.Capacity, p.MeanRecovery)));

    private static List<string> FamilyFrontier(string kind)
    {
        int ki = Array.IndexOf(AdaptabilityAudit.Kinds, kind);
        return Pareto(Profiles.Select(p => (p.Name, p.CapacityByKind[ki], p.RecoveryByKind[ki])));
    }

    /// <summary>The non-null cases, in damage coordinates: x = ln(1 − R), y = ln(capacity).</summary>
    private static (double[] X, double[] Y, List<string> Names) LogDamagePoints()
    {
        var names = new List<string>();
        var xs = new List<double>();
        var ys = new List<double>();
        foreach (var p in Profiles)
        {
            if (p.Capacity <= 0) continue;          // log of zero is undefined (the null case)
            names.Add(p.Name);
            xs.Add(Math.Log(p.Damage));
            ys.Add(Math.Log(p.Capacity));
        }
        return (xs.ToArray(), ys.ToArray(), names);
    }

    // ── 1. The constant-product hypothesis ──────────────────────────────────

    [Fact]
    public void Y_D_049_ProductIsNotConstant()
    {
        var products = Profiles.Select(p => (p.Name, P: p.Product, p.Capacity, p.MeanRecovery)).ToList();

        // The null case breaks "universal" outright: capacity 0 ⇒ product 0.
        var rnd = P("random");
        Assert.Equal(0.0, rnd.Capacity, 12);
        Assert.Equal(0.0, rnd.Product, 12);

        // Among the five non-null cases the product still spans a factor > 3.
        var nonNull = products.Where(x => x.Capacity > 0).Select(x => x.P).ToArray();
        Assert.Equal(5, nonNull.Length);
        double max = nonNull.Max(), min = nonNull.Min();
        Assert.True(max / min > 2.5, $"product span = {max / min:F2}×");

        // Coefficient of variation of the product — a constant law would give ≈ 0.
        double mean = nonNull.Average();
        double sd = Math.Sqrt(nonNull.Select(v => (v - mean) * (v - mean)).Sum() / nonNull.Length);
        double cv = sd / mean;
        Assert.True(cv > 0.25, $"product CV = {cv:F3}");

        // No single constant fits within ±10 %: the worst case is off by > 50 %.
        double worst = nonNull.Max(v => Math.Abs(v - mean) / mean);
        Assert.True(worst > 0.5, $"worst deviation from the mean product = {worst:P0}");

        // Even restricting to the Pareto frontier the product is not conserved.
        double pD96 = P("D96").Product, pCmp = P("complete").Product;
        Assert.True(Math.Abs(pD96 - pCmp) / ((pD96 + pCmp) / 2.0) > 0.5,
            $"frontier products {pD96:F4} vs {pCmp:F4}");
    }

    // ── 2. Fitting capacity against recovery ────────────────────────────────

    [Fact]
    public void Y_D_049_FitCapacityVsRecovery()
    {
        var r = Profiles.Select(p => p.MeanRecovery).ToArray();
        var c = Profiles.Select(p => p.Capacity).ToArray();

        // (a) Linear fit C = a + b·R over all six cases: the tradeoff slope is NEGATIVE.
        var lin = AdaptabilityAudit.Fit(r, c);
        Assert.True(lin.Slope < 0, $"linear slope = {lin.Slope:F2}");
        Assert.True(lin.R2 < 0.6, $"linear R² = {lin.R2:F3}");   // weak — not a tight law
        Assert.True(lin.R2 > 0.2, $"linear R² = {lin.R2:F3}");   // but the sign is systematic

        // (b) Power law in damage Δ = 1 − R, over the five non-null cases: C = k·Δ^β.
        var (x, y, _) = LogDamagePoints();
        var free = AdaptabilityAudit.Fit(x, y);
        double beta = free.Slope;
        double k = Math.Exp(free.Intercept);

        Assert.True(beta > 0.4, $"β = {beta:F3}");
        Assert.True(beta < 1.0, $"β = {beta:F3} — sub-linear, NOT the constant-product exponent −1");
        Assert.True(free.R2 > 0.6, $"free power-law R² = {free.R2:F3}");
        Assert.True(k > 3.0 && k < 20.0, $"k = {k:F2}");

        // (c) A conserved product C·Δ = const is the exponent −1 law. The data has the OPPOSITE
        // sign (capacity RISES with damage), so forcing −1 collapses the fit below the mean.
        var forced = AdaptabilityAudit.FitForcedSlope(x, y, -1.0);
        Assert.True(forced.R2 < 0.0, $"forced constant-product law gives R² = {forced.R2:F3}");
        Assert.True(free.R2 - forced.R2 > 0.5,
            $"free R² = {free.R2:F3} vs forced R² = {forced.R2:F3}");

        // A doubling of damage buys LESS than a doubling of capacity — the tradeoff is real
        // but sub-linear, so no constant can be conserved.
        double doubling = Math.Pow(2.0, beta);
        Assert.True(doubling < 2.0, $"2× damage ⇒ {doubling:F3}× capacity");
    }

    // ── 3. The frontier geometry ────────────────────────────────────────────

    [Fact]
    public void Y_D_049_ParetoFrontier()
    {
        var front = AggregateFrontier();

        // Exactly two cases are non-dominated.
        Assert.Equal(2, front.Count);
        Assert.Contains("D96", front);
        Assert.Contains("complete", front);

        var d96 = P("D96");
        var cmp = P("complete");

        // The frontier is NON-TRIVIAL: each member leads on one axis and trails on the other.
        Assert.True(d96.Capacity > cmp.Capacity, $"{d96.Capacity} vs {cmp.Capacity}");
        Assert.True(cmp.MeanRecovery > d96.MeanRecovery, $"{cmp.MeanRecovery} vs {d96.MeanRecovery}");

        // D96 is the global adaptability maximum; complete the global robustness maximum.
        Assert.All(Profiles, p => Assert.True(d96.Capacity >= p.Capacity - 1e-12));
        Assert.All(Profiles, p => Assert.True(cmp.MeanRecovery >= p.MeanRecovery - 1e-12));

        // Every other case is STRICTLY dominated by at least one frontier member — it gives up
        // adaptability without buying robustness.
        foreach (var p in Profiles.Where(x => !front.Contains(x.Name)))
        {
            bool dominated = (d96.Capacity > p.Capacity && d96.MeanRecovery > p.MeanRecovery)
                          || (cmp.Capacity > p.Capacity && cmp.MeanRecovery > p.MeanRecovery);
            Assert.True(dominated, $"{p.Name} is not strictly dominated");
        }

        // The tradeoff is therefore REAL (two incomparable optima) but NOT a constant law.
        Assert.True(d96.Product > 3 * cmp.Product, $"{d96.Product:F4} vs {cmp.Product:F4}");
    }

    // ── 4. Is the frontier universal across perturbation families? ──────────

    [Fact]
    public void Y_D_049_FrontierAcrossPerturbationFamilies()
    {
        var fronts = AdaptabilityAudit.Kinds.ToDictionary(k => k, FamilyFrontier);

        // NOT UNIVERSAL: the frontier membership depends on the perturbation family, and under
        // edge deletion it COLLAPSES to a single case — one graph is best on both axes, so no
        // tradeoff exists under that family at all.
        Assert.Single(fronts["delete"]);
        Assert.True(fronts.Values.Select(f => string.Join(",", f)).Distinct().Count() >= 3,
            "the frontier must change with the perturbation family");
        Assert.True(fronts["delete"].Count < fronts["add"].Count,
            "deletion dissolves the tradeoff that addition preserves");

        // The tradeoff is preserved under the other three families.
        foreach (string kind in new[] { "add", "rewire", "weight" })
            Assert.True(fronts[kind].Count >= 2 || fronts[kind].Count == 0,
                $"{kind} frontier has {fronts[kind].Count} members");

        // Every family frontier is SMALL — the cases never become interchangeable.
        foreach (var (kind, f) in fronts)
            Assert.True(f.Count <= 4, $"{kind} frontier size = {f.Count} ({string.Join(",", f)})");

        // The AGGREGATE frontier is never simply one family's frontier: universality fails.
        var agg = string.Join(",", AggregateFrontier());
        Assert.True(fronts.Values.Any(f => string.Join(",", f) != agg),
            "no single family reproduces the aggregate frontier");

        // The degenerate-free null is NOT on the aggregate frontier (it is dominated), even
        // though weight perturbation alone can put it on a frontier.
        Assert.DoesNotContain("random", AggregateFrontier());
    }

    // ── 5. The mechanism: the gap controls the tradeoff ─────────────────────

    [Fact]
    public void Y_D_049_GapIsTheControlParameter()
    {
        double[] gap = Profiles.Select(p => p.Lambda2).ToArray();
        double[] cap = Profiles.Select(p => p.Capacity).ToArray();
        double[] rec = Profiles.Select(p => p.MeanRecovery).ToArray();
        double[] deg = Profiles.Select(p => (double)p.DegenerateGroups).ToArray();
        double[] lat = Profiles.Select(p => p.L).ToArray();

        double rhoCapGap = AdaptabilityAudit.Spearman(gap, cap);
        double rhoRecGap = AdaptabilityAudit.Spearman(gap, rec);
        double rhoCapDeg = AdaptabilityAudit.Spearman(deg, cap);
        double rhoRecDeg = AdaptabilityAudit.Spearman(deg, rec);

        // One structural quantity — the degeneracy count, equivalently the inverse gap —
        // governs BOTH axes with OPPOSITE signs. That is the tradeoff.
        Assert.True(rhoCapDeg > 0.6, $"ρ(capacity, degeneracy count) = {rhoCapDeg:F3}");
        Assert.True(rhoRecDeg < -0.3, $"ρ(recovery, degeneracy count) = {rhoRecDeg:F3}");
        Assert.True(rhoCapGap < -0.6, $"ρ(capacity, λ₂) = {rhoCapGap:F3}");
        Assert.True(rhoRecGap > 0.3, $"ρ(recovery, λ₂) = {rhoRecGap:F3}");

        // The two predictors are the same axis seen two ways.
        Assert.True(AdaptabilityAudit.Spearman(deg, gap) < -0.6,
            $"ρ(degeneracy count, λ₂) = {AdaptabilityAudit.Spearman(deg, gap):F3}");

        // The degeneracy FRACTION L (D_048's latent fraction) is a far weaker predictor than
        // the degeneracy COUNT — the counting measure is what indexes the tradeoff.
        Assert.True(Math.Abs(AdaptabilityAudit.Spearman(lat, cap)) < 0.3,
            $"ρ(capacity, L) = {AdaptabilityAudit.Spearman(lat, cap):F3}");
        Assert.True(Math.Abs(rhoCapDeg) > 3 * Math.Abs(AdaptabilityAudit.Spearman(lat, cap)),
            "the count beats the fraction");
    }

    // ── 6. Classification output ────────────────────────────────────────────

    [Fact]
    public void Y_D_049_Classification()
    {
        var front = AggregateFrontier();
        var (x, y, _) = LogDamagePoints();
        var free = AdaptabilityAudit.Fit(x, y);
        var forced = AdaptabilityAudit.FitForcedSlope(x, y, -1.0);

        // DERIVED — the frontier structure and the mechanism (sign-stable relations).
        Assert.Equal(2, front.Count);
        Assert.True(AdaptabilityAudit.Spearman(
            Profiles.Select(p => p.Lambda2).ToArray(),
            Profiles.Select(p => p.Capacity).ToArray()) < -0.6);

        // EMERGENT — the fitted constants and the particular dominated set have no derivation;
        // they are numbers read off this six-case ensemble.
        Assert.True(free.Slope > 0.4 && free.Slope < 1.0);
        Assert.True(free.R2 > forced.R2);

        // REFUTED — "Adaptability × Robustness = const" as a universal law.
        Assert.True(forced.R2 < 0.0);
        Assert.Equal(0.0, P("random").Product, 12);
    }

    // ── 7. Report ───────────────────────────────────────────────────────────

    [Fact]
    public void Y_D_049_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_049 — Adaptability–Robustness Frontier Audit");

        sb.AppendLine("Question: is there a universal tradeoff  Adaptability × Robustness = const ?");
        sb.AppendLine("capacity = mean ΔA/(N − A₀) ;  recovery = mean 1 − ‖λ′−λ‖₂/‖λ‖₂  (shared D_048 ensemble).");
        sb.AppendLine("Quantitative only — no AT assumptions.");
        sb.AppendLine();

        sb.AppendLine("[1] The two axes");
        sb.AppendLine("     case            λ₂      degen  L       capacity  recovery   product   frontier");
        var front = AggregateFrontier();
        foreach (var p in Profiles)
            sb.AppendLine($"     {p.Name,-13} {p.Lambda2,7:F4} {p.DegenerateGroups,6} {p.L,7:F4} " +
                          $"{p.Capacity,9:F4} {p.MeanRecovery,9:F4} {p.Product,9:F4}   " +
                          $"{(front.Contains(p.Name) ? "PARETO" : "dominated")}");
        sb.AppendLine();

        sb.AppendLine("[2] Is the product constant?");
        var nonNull = Profiles.Where(p => p.Capacity > 0).Select(p => p.Product).ToArray();
        double mean = nonNull.Average();
        double sd = Math.Sqrt(nonNull.Select(v => (v - mean) * (v - mean)).Sum() / nonNull.Length);
        sb.AppendLine($"     non-null products : {string.Join(", ", nonNull.Select(v => v.ToString("F4")))}");
        sb.AppendLine($"     mean = {mean:F4}   sd = {sd:F4}   CV = {sd / mean:F3}   span = {nonNull.Max() / nonNull.Min():F2}×");
        sb.AppendLine($"     the null case (random) has capacity 0 ⇒ product 0 ⇒ 'universal' fails outright.");
        sb.AppendLine();

        sb.AppendLine("[3] Fit capacity vs recovery");
        var r = Profiles.Select(p => p.MeanRecovery).ToArray();
        var c = Profiles.Select(p => p.Capacity).ToArray();
        var lin = AdaptabilityAudit.Fit(r, c);
        sb.AppendLine($"     (a) linear  C = {lin.Intercept:F2} + ({lin.Slope:F2})·R      R² = {lin.R2:F3}");
        var (x, y, names) = LogDamagePoints();
        var free = AdaptabilityAudit.Fit(x, y);
        var forced = AdaptabilityAudit.FitForcedSlope(x, y, -1.0);
        sb.AppendLine($"     (b) power   C = {Math.Exp(free.Intercept):F2}·Δ^({free.Slope:F3})      R² = {free.R2:F3}");
        sb.AppendLine($"     (c) forced  C = {Math.Exp(forced.Offset):F2}·Δ^(-1.000)      R² = {forced.R2:F3}  ← C·Δ = const (conserved product)");
        sb.AppendLine($"         free fit uses {names.Count} non-null cases (log 0 undefined for the null).");
        sb.AppendLine($"         2× damage buys {Math.Pow(2.0, free.Slope):F3}× capacity — sub-linear, and the SIGN is");
        sb.AppendLine($"         the OPPOSITE of a conserved product (capacity RISES with damage: exponent +{free.Slope:F3}).");
        sb.AppendLine();

        sb.AppendLine("[4] Pareto frontier per perturbation family");
        foreach (string kind in AdaptabilityAudit.Kinds)
            sb.AppendLine($"     {kind,-8} → {{{string.Join(", ", FamilyFrontier(kind))}}}");
        sb.AppendLine();

        sb.AppendLine("[5] What shape is the tradeoff? (Spearman ρ over the six cases)");
        sb.AppendLine("     axis          vs λ₂   vs degeneracy       vs L");
        {
            var gap = Profiles.Select(p => p.Lambda2).ToArray();
            var deg = Profiles.Select(p => (double)p.DegenerateGroups).ToArray();
            var lat = Profiles.Select(p => p.L).ToArray();
            foreach (var (name, v) in new (string, Func<AdaptabilityProfile, double>)[]
                     {
                         ("capacity", p => p.Capacity),
                         ("recovery", p => p.MeanRecovery),
                     })
            {
                var col = Profiles.Select(v).ToArray();
                sb.AppendLine($"     {name,-12} {AdaptabilityAudit.Spearman(gap, col),7:F3} " +
                              $"{AdaptabilityAudit.Spearman(deg, col),14:F3} {AdaptabilityAudit.Spearman(lat, col),9:F3}");
            }
        }
        sb.AppendLine();

        sb.AppendLine("[6] Classification");
        sb.AppendLine("  DERIVED : the (capacity, recovery) Pareto frontier has exactly TWO members —");
        sb.AppendLine("            D96 (max adaptability) and complete (max robustness); the other four");
        sb.AppendLine("            cases are strictly dominated (they give up adaptability for nothing).");
        sb.AppendLine("            A tradeoff therefore EXISTS — but it is a 2-point frontier, not a law.");
        sb.AppendLine("  DERIVED : the mechanism — ONE structural quantity, the degeneracy count (equivalently");
        var gapCol = Profiles.Select(p => p.Lambda2).ToArray();
        var degCol = Profiles.Select(p => (double)p.DegenerateGroups).ToArray();
        sb.AppendLine($"            the inverse gap), governs both axes with OPPOSITE signs: " +
                      $"ρ(capacity, degen) = {AdaptabilityAudit.Spearman(degCol, c):F3},");
        sb.AppendLine($"            ρ(recovery, degen) = {AdaptabilityAudit.Spearman(degCol, r):F3}; " +
                      $"ρ(capacity, λ₂) = {AdaptabilityAudit.Spearman(gapCol, c):F3},");
        sb.AppendLine($"            ρ(recovery, λ₂) = {AdaptabilityAudit.Spearman(gapCol, r):F3}. " +
                      $"The degeneracy FRACTION L is");
        sb.AppendLine("            a far weaker predictor (ρ = −0.03): the COUNT indexes the tradeoff, not the share.");
        sb.AppendLine("  EMERGENT: the fitted exponent and constant (β ≈ 0.74, k ≈ 8), the particular");
        sb.AppendLine("            dominated set, and the frontier instability across perturbation families");
        sb.AppendLine("            (deletion collapses the frontier to one case; addition gives four).");
        sb.AppendLine("  REFUTED : 'Adaptability × Robustness = const' as a UNIVERSAL law — the product");
        sb.AppendLine("            spans 0 → 0.96 (the null gives exactly 0), and the conserved-product law");
        sb.AppendLine($"            (exponent −1) gives R² = {forced.R2:F2} versus R² = {free.R2:F2} for the free");
        sb.AppendLine("            power law — worse than predicting the mean, and with the wrong SIGN.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
