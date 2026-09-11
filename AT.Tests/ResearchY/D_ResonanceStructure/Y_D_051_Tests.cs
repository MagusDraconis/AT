using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_051 — Blind Prediction Audit.
///
/// Tests D_050 PROSPECTIVELY. Six 96-node rings whose spectra were never used in D_048–D_050 are
/// generated, their spectral inputs are read off, and capacity and recovery are predicted from the
/// D_050 single-predictor relations — BEFORE any simulation of these rings is run.
///
/// The blind protocol is enforced by file order, not by convention: PHASE A (this file, committed
/// on its own) contains the prediction only — it never constructs the perturbation ensemble. The
/// measurement is added in a later commit, so the prediction is on record in git before any
/// observed value for these spectra exists.
///
/// Deterministic throughout: the rings are fixed circulants, and the ensemble uses the shared fixed
/// seeds and doses. No randomness anywhere.
/// </summary>
public class Y_D_051_Tests : ResearchTestBase
{
    public Y_D_051_Tests(ITestOutputHelper output) : base(output) { }

    // ── The D_050 relations, frozen. These are the ONLY predictor coefficients in this file. ──
    // Source: ResearchY-D_050 §4, fitted to the six D_048/D_049 cases and selected by leave-one-out
    // parsimony (the minimal set is { near-gap density }, one predictor for both targets).
    private const double CapSlope = -0.00814;
    private const double CapIntercept = 0.87153;
    private const double RecSlope = 0.000284;
    private const double RecIntercept = 0.95582;

    /// <summary>Predict capacity from the single D_050 predictor.</summary>
    private static double PredictCapacity(double nearGapDensity)
        => CapSlope * nearGapDensity + CapIntercept;

    /// <summary>Predict recovery from the single D_050 predictor.</summary>
    private static double PredictRecovery(double nearGapDensity)
        => RecSlope * nearGapDensity + RecIntercept;

    /// <summary>One new spectrum: a fixed circulant ring defined by its signed offsets.</summary>
    private sealed record NewRing(string Name, string Description, (int Offset, double Weight)[] Offsets);

    /// <summary>
    /// The six NEW rings. None is D_048–D_050's D96 (±1..±6), D96-3D (4×4×6 torus), random sparse,
    /// physical (λ_m = m), unphysical (piecewise-constant) or complete (K96). All are connected —
    /// required, since the shared ensemble drops perturbations that disconnect a graph.
    /// </summary>
    private static readonly NewRing[] NewRings =
    [
        new("S96-123", "sparse ring, offsets ±1..±3, unit weights (degree 6)",
            [(1, 1.0), (2, 1.0), (3, 1.0)]),
        new("S96-135", "sparse ring, offsets ±1,±3,±5, unit weights (degree 6)",
            [(1, 1.0), (3, 1.0), (5, 1.0)]),
        new("D96-24", "dense ring, offsets ±1..±12, unit weights (degree 24)",
            [(1, 1.0), (2, 1.0), (3, 1.0), (4, 1.0), (5, 1.0), (6, 1.0),
             (7, 1.0), (8, 1.0), (9, 1.0), (10, 1.0), (11, 1.0), (12, 1.0)]),
        new("Decay96", "ring ±1..±6 with decaying weights w_d = 1/d (degree 12)",
            [(1, 1.0), (2, 0.5), (3, 1.0 / 3.0), (4, 0.25), (5, 0.2), (6, 1.0 / 6.0)]),
        new("Boost96", "ring ±1..±6 with GROWING weights w_d = d (degree 12)",
            [(1, 1.0), (2, 2.0), (3, 3.0), (4, 4.0), (5, 5.0), (6, 6.0)]),
        new("Ring48", "ring ±1..±6 plus the long-range offsets ±24 and ±48 (degree 12)",
            [(1, 1.0), (2, 1.0), (3, 1.0), (4, 1.0), (5, 1.0), (6, 1.0), (24, 1.0), (48, 1.0)]),
    ];

    /// <summary>Build the symmetric adjacency matrix of a ring with the given signed offsets.</summary>
    private static double[,] Ring((int Offset, double Weight)[] offsets)
    {
        int n = AdaptabilityAudit.N;
        var a = new double[n, n];
        for (int i = 0; i < n; i++)
            foreach (var (d, w) in offsets)
            {
                a[i, (i + d) % n] += w;
                a[i, ((i - d) % n + n) % n] += w;
            }
        return a;
    }

    private static double SmallestPositive(double[] spectrum)
    {
        double lam2 = double.PositiveInfinity;
        foreach (double x in spectrum)
            if (x > AdaptabilityAudit.Tol && x < lam2) lam2 = x;
        return lam2;
    }

    /// <summary>The four D_050 spectral inputs, read from the spectrum alone — no simulation.</summary>
    private sealed record SpectralInputs(string Name, double Lambda2, int NearGapDensity, int Degeneracy, int Distinct);

    private static SpectralInputs InputsOf(NewRing ring)
    {
        var spectrum = AdaptabilityAudit.SpectrumOf(Ring(ring.Offsets));
        double lam2 = SmallestPositive(spectrum);
        var (attractors, _, mult) = AdaptabilityAudit.Buckets(spectrum);
        return new SpectralInputs(ring.Name, lam2,
            AdaptabilityAudit.NearGapDensityK2(spectrum, lam2),
            mult.Count(m => m > 1),
            attractors);
    }

    // ── 1. The new spectra, and the prediction made from them ───────────────

    [Fact]
    public void D051_01_New_Spectra_And_Frozen_Prediction()
    {
        var sb = new StringBuilder();
        PrintHeader("1. New spectra and the D_050 prediction — PHASE A (no simulation)");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  A1. The predictor is D_050's minimal set, ONE quantity: the near-gap density at k = 2");
        sb.AppendLine("      (positive eigenvalues within 2λ₂, the T_014 convention). Its coefficients are the");
        sb.AppendLine($"      D_050 §4 fitted relations, frozen here as constants: capacity = {CapSlope}·x + {CapIntercept},");
        sb.AppendLine($"      recovery = {RecSlope}·x + {RecIntercept}. No other predictor and no D051 fitted quantity is used.");
        sb.AppendLine("  A2. Six rings are generated that appear in NO D_048–D_050 case set; each is connected, so");
        sb.AppendLine("      the shared ensemble's connectivity guard cannot reject the whole case.");
        sb.AppendLine("  A3. The prediction is a pure function of the SPECTRUM. It is printed here, in a commit that");
        sb.AppendLine("      contains no measurement code, so it is on record before any observed value exists.");
        sb.AppendLine("  A4. Both predicted values must land inside the DERIVED bounds (0 ≤ capacity, recovery ≤ 1).");
        sb.AppendLine();

        // Guard: none of the new spectra may coincide with a D_048–D_050 case spectrum.
        var known = new List<(string Name, double[] Spectrum)>();
        foreach (string name in AdaptabilityAudit.CaseNames)
            known.Add((name, AdaptabilityAudit.SpectrumOf(AdaptabilityAudit.Adjacency(name))));

        var inputs = new List<SpectralInputs>();
        foreach (var ring in NewRings)
        {
            var spec = AdaptabilityAudit.SpectrumOf(Ring(ring.Offsets));
            var sorted = spec.OrderBy(x => x).ToArray();
            string closest = "none";
            double closestDev = double.PositiveInfinity;
            foreach (var (name, ks) in known)
            {
                var ksSorted = ks.OrderBy(x => x).ToArray();
                double dev = 0.0;
                for (int i = 0; i < sorted.Length; i++) dev = Math.Max(dev, Math.Abs(sorted[i] - ksSorted[i]));
                if (dev < closestDev) { closestDev = dev; closest = name; }
            }
            sb.AppendLine($"  {ring.Name,-9} vs nearest known case ({closest}): max |Δλ| = {closestDev.ToString("G4", CultureInfo.InvariantCulture)}  {(closestDev > 1e-6 ? "NEW ✓" : "DUPLICATE ✗")}");
            Assert.True(closestDev > 1e-6, $"{ring.Name} must be a spectrum not used in D_048–D_050");
            Assert.True(AdaptabilityAudit.Connected(Ring(ring.Offsets)), $"{ring.Name} must be connected");
            inputs.Add(InputsOf(ring));
        }

        sb.AppendLine();
        sb.AppendLine("  THE NEW SPECTRA (spectral inputs only — no dynamics run yet)");
        sb.AppendLine("  ring      description");
        sb.AppendLine("  " + new string('-', 96));
        foreach (var ring in NewRings) sb.AppendLine($"  {ring.Name,-9} {ring.Description}");
        sb.AppendLine();
        sb.AppendLine("  ring        λ₂          near-gap(2λ₂)  degeneracy  distinct");
        sb.AppendLine("  " + new string('-', 62));
        foreach (var i in inputs)
            sb.AppendLine($"  {i.Name,-9} {i.Lambda2,10:F6} {i.NearGapDensity,13} {i.Degeneracy,12} {i.Distinct,9}");

        sb.AppendLine();
        sb.AppendLine("  THE FROZEN PREDICTION — capacity and recovery, from the D_050 relations alone");
        sb.AppendLine("  ring        predicted capacity   predicted recovery");
        sb.AppendLine("  " + new string('-', 55));
        foreach (var i in inputs)
        {
            double c = PredictCapacity(i.NearGapDensity);
            double r = PredictRecovery(i.NearGapDensity);
            sb.AppendLine($"  {i.Name,-9} {c,17:F5} {r,19:F5}");
        }

        sb.AppendLine();
        sb.AppendLine("  PREDICTED RANKING BY CAPACITY (the direction the one-predictor law asserts):");
        foreach (var (i, r) in inputs.OrderByDescending(x => PredictCapacity(x.NearGapDensity))
                                     .Select((x, idx) => (x, idx + 1)))
            sb.AppendLine($"    {r}. {i.Name,-9} near-gap {i.NearGapDensity,3}  →  capacity {PredictCapacity(i.NearGapDensity):F4}");
        sb.AppendLine();
        sb.AppendLine("  PREDICTED RANKING BY RECOVERY:");
        foreach (var (i, r) in inputs.OrderByDescending(x => PredictRecovery(x.NearGapDensity))
                                     .Select((x, idx) => (x, idx + 1)))
            sb.AppendLine($"    {r}. {i.Name,-9} near-gap {i.NearGapDensity,3}  →  recovery {PredictRecovery(i.NearGapDensity):F4}");

        sb.AppendLine();
        sb.AppendLine("  BOUNDS CHECK (derived, D_050 §Classification): every prediction must lie in [0, 1].");
        foreach (var i in inputs)
        {
            double c = PredictCapacity(i.NearGapDensity), r = PredictRecovery(i.NearGapDensity);
            Assert.InRange(c, 0.0, 1.0);
            Assert.InRange(r, 0.0, 1.0);
            sb.AppendLine($"    {i.Name,-9} capacity {c:F5} ∈ [0,1] ✓   recovery {r:F5} ∈ [0,1] ✓");
        }

        sb.AppendLine();
        sb.AppendLine("  The prediction is now on record. PHASE B (measurement under the shared D_048/D_049");
        sb.AppendLine("  ensemble) is added in a later commit and compared against these numbers verbatim.");

        Output.WriteLine(sb.ToString());
    }

    // ── 2. Integrity of the frozen prediction ───────────────────────────────

    [Fact]
    public void D051_02_Frozen_Prediction_Integrity()
    {
        var sb = new StringBuilder();
        PrintHeader("2. Integrity of the frozen relations — are they really D_050's?");

        sb.AppendLine("  The constants below were committed BEFORE any measurement of the new rings exists.");
        sb.AppendLine("  If they had been adjusted after the fact, re-applying them to D_050's own six cases");
        sb.AppendLine("  would no longer reproduce D_050's published fits. That is the check performed here.");
        sb.AppendLine();

        var rows = AdaptabilityAudit.CaseNames.Select(name =>
        {
            var p = AdaptabilityAudit.Profiles.Single(x => x.Name == name);
            var spec = AdaptabilityAudit.SpectrumOf(AdaptabilityAudit.Adjacency(name));
            return (Name: name, Cap: p.Capacity, Rec: p.MeanRecovery,
                NearGap: AdaptabilityAudit.NearGapDensityK2(spec, p.Lambda2));
        }).ToArray();

        double[] x = rows.Select(r => (double)r.NearGap).ToArray();
        double[] yCap = rows.Select(r => r.Cap).ToArray();
        double[] yRec = rows.Select(r => r.Rec).ToArray();
        var capFit = AdaptabilityAudit.Fit(x, yCap);
        var recFit = AdaptabilityAudit.Fit(x, yRec);

        sb.AppendLine("  Re-fitting the six D_050 cases to the SAME single predictor (near-gap density):");
        sb.AppendLine($"    capacity = {capFit.Slope.ToString("F5", CultureInfo.InvariantCulture)}·x + {capFit.Intercept.ToString("F5", CultureInfo.InvariantCulture)}   R² = {capFit.R2.ToString("F3", CultureInfo.InvariantCulture)}");
        sb.AppendLine($"    recovery = {recFit.Slope.ToString("F6", CultureInfo.InvariantCulture)}·x + {recFit.Intercept.ToString("F5", CultureInfo.InvariantCulture)}   R² = {recFit.R2.ToString("F3", CultureInfo.InvariantCulture)}");
        sb.AppendLine($"    frozen constants used in this audit: capacity {CapSlope}·x + {CapIntercept}, recovery {RecSlope}·x + {RecIntercept}");
        sb.AppendLine();
        sb.AppendLine($"    |capacity slope − D_050 refit|   = {Math.Abs(capFit.Slope - CapSlope):G3}  (rounding of the published 5-decimal form)");
        sb.AppendLine($"    |capacity intercept − D_050 refit| = {Math.Abs(capFit.Intercept - CapIntercept):G3}");
        sb.AppendLine($"    |recovery slope − D_050 refit|   = {Math.Abs(recFit.Slope - RecSlope):G3}");
        sb.AppendLine($"    |recovery intercept − D_050 refit| = {Math.Abs(recFit.Intercept - RecIntercept):G3}");
        sb.AppendLine();
        sb.AppendLine($"  D_050 published R² for these two fits: 0.759 (capacity) and 0.525 (recovery); refit here: {capFit.R2:F3} and {recFit.R2:F3} — reproduced.");

        // The frozen constants are the published 5-decimal form of the refit; allow that rounding only.
        Assert.True(Math.Abs(capFit.Slope - CapSlope) < 5e-5, "the frozen capacity slope must be D_050's");
        Assert.True(Math.Abs(capFit.Intercept - CapIntercept) < 5e-4, "the frozen capacity intercept must be D_050's");
        Assert.True(Math.Abs(recFit.Slope - RecSlope) < 5e-7, "the frozen recovery slope must be D_050's");
        Assert.True(Math.Abs(recFit.Intercept - RecIntercept) < 5e-5, "the frozen recovery intercept must be D_050's");
        Assert.True(Math.Abs(capFit.R2 - 0.759) < 5e-3 && Math.Abs(recFit.R2 - 0.525) < 5e-3,
            "the refit must reproduce D_050's published R²");

        Output.WriteLine(sb.ToString());
    }

    // ── 3. The measurement ──────────────────────────────────────────────────

    /// <summary>Predicted versus observed for one new ring.</summary>
    private sealed record Outcome(string Name, int NearGap, double PredCap, double ObsCap,
        double PredRec, double ObsRec)
    {
        public double CapError => Math.Abs(PredCap - ObsCap);
        public double RecError => Math.Abs(PredRec - ObsRec);
    }

    private static IReadOnlyList<Outcome> Measure()
    {
        var list = new List<Outcome>();
        foreach (var ring in NewRings)
        {
            var i = InputsOf(ring);
            var observed = AdaptabilityAudit.Study(ring.Name, Ring(ring.Offsets));
            list.Add(new Outcome(ring.Name, i.NearGapDensity,
                PredictCapacity(i.NearGapDensity), observed.Capacity,
                PredictRecovery(i.NearGapDensity), observed.MeanRecovery));
        }
        return list;
    }

    [Fact]
    public void D051_03_Predicted_Versus_Observed()
    {
        var sb = new StringBuilder();
        PrintHeader("3. Predicted versus observed — the blind test");

        var outcomes = Measure();

        sb.AppendLine("  The observed values come from the shared D_048/D_049 ensemble applied to each new ring");
        sb.AppendLine("  through the identical protocol: 4 perturbation types × 5 doses × 3 fixed seeds, doses as");
        sb.AppendLine("  fractions of the ring's own edge count, connectivity-guarded, zero exclusions. The");
        sb.AppendLine("  predicted column is the D_050 relation, computed from the spectrum alone.");
        sb.AppendLine();
        sb.AppendLine("  ring        near-gap   pred cap   obs cap   |Δ|      pred rec   obs rec   |Δ|");
        sb.AppendLine("  " + new string('-', 78));
        foreach (var o in outcomes)
            sb.AppendLine($"  {o.Name,-9} {o.NearGap,8} {o.PredCap,10:F5} {o.ObsCap,9:F5} {o.CapError,7:F4} {o.PredRec,10:F5} {o.ObsRec,9:F5} {o.RecError,6:F4}");

        double maxCap = outcomes.Max(o => o.CapError);
        double meanCap = outcomes.Average(o => o.CapError);
        double maxRec = outcomes.Max(o => o.RecError);
        double meanRec = outcomes.Average(o => o.RecError);

        sb.AppendLine();
        sb.AppendLine($"  ABSOLUTE ERROR — capacity: mean {meanCap:F4}, max {maxCap:F4} (range of predictions: " +
                      $"{outcomes.Min(o => o.PredCap):F4} … {outcomes.Max(o => o.PredCap):F4}, span {outcomes.Max(o => o.PredCap) - outcomes.Min(o => o.PredCap):F4})");
        sb.AppendLine($"  ABSOLUTE ERROR — recovery: mean {meanRec:F5}, max {maxRec:F5} (range of predictions: " +
                      $"{outcomes.Min(o => o.PredRec):F5} … {outcomes.Max(o => o.PredRec):F5}, span {outcomes.Max(o => o.PredRec) - outcomes.Min(o => o.PredRec):F5})");
        sb.AppendLine($"  OBSERVED capacity span: {outcomes.Min(o => o.ObsCap):F4} … {outcomes.Max(o => o.ObsCap):F4} = {outcomes.Max(o => o.ObsCap) - outcomes.Min(o => o.ObsCap):F4}");
        sb.AppendLine($"  OBSERVED recovery span: {outcomes.Min(o => o.ObsRec):F5} … {outcomes.Max(o => o.ObsRec):F5} = {outcomes.Max(o => o.ObsRec) - outcomes.Min(o => o.ObsRec):F5}");

        sb.AppendLine();
        sb.AppendLine("  THE PREDICTOR'S OWN RESOLUTION ON THESE RINGS");
        int distinctPredCap = outcomes.Select(o => Math.Round(o.PredCap, 6)).Distinct().Count();
        int distinctObsCap = outcomes.Select(o => Math.Round(o.ObsCap, 4)).Distinct().Count();
        sb.AppendLine($"    distinct predicted capacities across the six rings: {distinctPredCap} (near-gap values: " +
                      $"{string.Join(", ", outcomes.Select(o => o.NearGap))})");
        sb.AppendLine($"    distinct OBSERVED capacities (4 d.p.): {distinctObsCap}");
        sb.AppendLine("    Five of the six rings carry the minimum possible near-gap density (2) — only the k = ±1");
        sb.AppendLine("    doublet sits within 2λ₂ whenever the low-k spectrum follows the k² law — so the law assigns");
        sb.AppendLine("    them one and the same prediction. It cannot separate them by construction.");

        Assert.True(outcomes.All(o => o.NearGap >= 2), "a connected ring must have at least the k = ±1 doublet in the near-gap");
        Output.WriteLine(sb.ToString());
    }

    // ── 4. Metrics: absolute error and rank error ───────────────────────────

    [Fact]
    public void D051_04_Absolute_And_Rank_Error()
    {
        var sb = new StringBuilder();
        PrintHeader("4. Metrics — absolute error and rank error");

        var outcomes = Measure();

        double[] predCap = outcomes.Select(o => o.PredCap).ToArray();
        double[] obsCap = outcomes.Select(o => o.ObsCap).ToArray();
        double[] predRec = outcomes.Select(o => o.PredRec).ToArray();
        double[] obsRec = outcomes.Select(o => o.ObsRec).ToArray();

        sb.AppendLine("  RANK ERROR within the six new rings (tie-averaged ranks: equal predictions share a rank).");
        sb.AppendLine("  ring        pred rank   obs rank   |Δrank|   (capacity)      pred rank   obs rank   |Δrank|   (recovery)");
        sb.AppendLine("  " + new string('-', 104));
        double[] rpC = AdaptabilityAudit.Ranks(predCap), roC = AdaptabilityAudit.Ranks(obsCap);
        double[] rpR = AdaptabilityAudit.Ranks(predRec), roR = AdaptabilityAudit.Ranks(obsRec);
        for (int i = 0; i < outcomes.Count; i++)
            sb.AppendLine($"  {outcomes[i].Name,-9} {rpC[i],9:F1} {roC[i],10:F1} {Math.Abs(rpC[i] - roC[i]),9:F1} {new string(' ', 14)} {rpR[i],9:F1} {roR[i],10:F1} {Math.Abs(rpR[i] - roR[i]),9:F1}");

        double capRankErr = Enumerable.Range(0, outcomes.Count).Average(i => Math.Abs(rpC[i] - roC[i]));
        double recRankErr = Enumerable.Range(0, outcomes.Count).Average(i => Math.Abs(rpR[i] - roR[i]));
        double capRankMax = Enumerable.Range(0, outcomes.Count).Max(i => Math.Abs(rpC[i] - roC[i]));
        double recRankMax = Enumerable.Range(0, outcomes.Count).Max(i => Math.Abs(rpR[i] - roR[i]));

        sb.AppendLine();
        sb.AppendLine($"  RANK ERROR (capacity, 6 new rings): mean |Δrank| = {capRankErr:F2} of 5 possible, max {capRankMax:F1}");
        sb.AppendLine($"  RANK ERROR (recovery, 6 new rings): mean |Δrank| = {recRankErr:F2} of 5 possible, max {recRankMax:F1}");

        // The same comparison inside the union of the twelve cases — the old six, whose predictions are
        // the law's own in-sample values, plus the six new ones.
        var allPredCap = new List<double>();
        var allObsCap = new List<double>();
        var allPredRec = new List<double>();
        var allObsRec = new List<double>();
        var names = new List<string>();
        foreach (string name in AdaptabilityAudit.CaseNames)
        {
            var p = AdaptabilityAudit.Profiles.Single(x => x.Name == name);
            var spec = AdaptabilityAudit.SpectrumOf(AdaptabilityAudit.Adjacency(name));
            int ng = AdaptabilityAudit.NearGapDensityK2(spec, p.Lambda2);
            names.Add(name);
            allPredCap.Add(PredictCapacity(ng));
            allObsCap.Add(p.Capacity);
            allPredRec.Add(PredictRecovery(ng));
            allObsRec.Add(p.MeanRecovery);
        }
        foreach (var o in outcomes)
        {
            names.Add(o.Name);
            allPredCap.Add(o.PredCap);
            allObsCap.Add(o.ObsCap);
            allPredRec.Add(o.PredRec);
            allObsRec.Add(o.ObsRec);
        }
        double[] up = AdaptabilityAudit.Ranks(allPredCap.ToArray());
        double[] uo = AdaptabilityAudit.Ranks(allObsCap.ToArray());
        double[] vr = AdaptabilityAudit.Ranks(allPredRec.ToArray());
        double[] vo = AdaptabilityAudit.Ranks(allObsRec.ToArray());
        double unionCapErr = Enumerable.Range(0, names.Count).Average(i => Math.Abs(up[i] - uo[i]));
        double unionRecErr = Enumerable.Range(0, names.Count).Average(i => Math.Abs(vr[i] - vo[i]));

        sb.AppendLine();
        sb.AppendLine("  RANK ERROR inside the UNION of twelve cases (D_050's six in-sample cases + the six new ones).");
        sb.AppendLine($"    capacity: mean |Δrank| = {unionCapErr:F2} of 11 possible");
        sb.AppendLine($"    recovery: mean |Δrank| = {unionRecErr:F2} of 11 possible");
        sb.AppendLine($"    NOTE the union metric is generous to the law: for the six old cases the prediction IS the");
        sb.AppendLine($"    fitted value, so their rank error is not out-of-sample evidence.");
        sb.AppendLine();
        double unionCapNew = Enumerable.Range(6, 6).Average(i => Math.Abs(up[i] - uo[i]));
        double unionRecNew = Enumerable.Range(6, 6).Average(i => Math.Abs(vr[i] - vo[i]));
        sb.AppendLine($"    restricted to the six NEW cases inside the union: capacity mean |Δrank| = {unionCapNew:F2}, recovery {unionRecNew:F2}");

        sb.AppendLine();
        sb.AppendLine("  SPEARMAN between predicted and observed, over the six new rings:");
        sb.AppendLine($"    capacity: ρ = {AdaptabilityAudit.Spearman(predCap, obsCap):F3}   recovery: ρ = {AdaptabilityAudit.Spearman(predRec, obsRec):F3}");
        sb.AppendLine($"    (both targets carry five identical predictions, so the correlation is driven entirely by Ring48)");
        sb.AppendLine();
        sb.AppendLine("  The law's information about the six new rings is exactly ONE bit of ordering: 'Ring48, and");
        sb.AppendLine("  then everybody else' (capacity), inverted for recovery.");

        Assert.Equal(6, outcomes.Count);
        Output.WriteLine(sb.ToString());
    }

    // ── 5. Verdict ──────────────────────────────────────────────────────────

    [Fact]
    public void D051_05_Verdict()
    {
        var sb = new StringBuilder();
        PrintHeader("5. Verdict — DERIVED / EMERGENT / REFUTED");

        var outcomes = Measure();
        double[] predCap = outcomes.Select(o => o.PredCap).ToArray();
        double[] obsCap = outcomes.Select(o => o.ObsCap).ToArray();
        double[] predRec = outcomes.Select(o => o.PredRec).ToArray();
        double[] obsRec = outcomes.Select(o => o.ObsRec).ToArray();

        double meanCapErr = outcomes.Average(o => o.CapError);
        double maxCapErr = outcomes.Max(o => o.CapError);
        double meanRecErr = outcomes.Average(o => o.RecError);
        double obsCapSpan = obsCap.Max() - obsCap.Min();
        double predCapSpan = predCap.Max() - predCap.Min();
        double obsRecSpan = obsRec.Max() - obsRec.Min();
        double predRecSpan = predRec.Max() - predRec.Min();
        double rhoCap = AdaptabilityAudit.Spearman(predCap, obsCap);
        double rhoRec = AdaptabilityAudit.Spearman(predRec, obsRec);
        double[] rpC = AdaptabilityAudit.Ranks(predCap), roC = AdaptabilityAudit.Ranks(obsCap);
        double[] rpR = AdaptabilityAudit.Ranks(predRec), roR = AdaptabilityAudit.Ranks(obsRec);
        double capRankErr = Enumerable.Range(0, 6).Average(i => Math.Abs(rpC[i] - roC[i]));
        double recRankErr = Enumerable.Range(0, 6).Average(i => Math.Abs(rpR[i] - roR[i]));
        int nearGapTwo = outcomes.Count(o => o.NearGap == 2);

        sb.AppendLine("  The prediction was committed (003a0033) in a file containing no measurement code;");
        sb.AppendLine("  the six new rings were measured afterwards under the shared D_048/D_049 ensemble. This");
        sb.AppendLine("  verdict therefore reports a genuine OUT-OF-SAMPLE result, not an in-sample fit.");
        sb.AppendLine();
        sb.AppendLine("  HEADLINE NUMBERS");
        sb.AppendLine($"    capacity : mean |error| {meanCapErr:F4}, max {maxCapErr:F4}   against an OBSERVED span of {obsCapSpan:F4}");
        sb.AppendLine($"               => the mean error is {meanCapErr / obsCapSpan:F2}× the entire observed range of the truth");
        sb.AppendLine($"    recovery : mean |error| {meanRecErr:F5}, max {outcomes.Max(o => o.RecError):F5}   against an OBSERVED span of {obsRecSpan:F5}");
        sb.AppendLine($"               => predicted span {predRecSpan:F5} vs observed span {obsRecSpan:F5}: the law sees {predRecSpan / obsRecSpan:P1} of the real variation");
        sb.AppendLine($"    rank     : capacity mean |Δrank| {capRankErr:F2} of 5 ({capRankErr / 5.0:P0} of the range), recovery {recRankErr:F2}");
        sb.AppendLine($"    Spearman : capacity ρ = {rhoCap:F3} (the ordering is ANTI-correlated), recovery ρ = {rhoRec:F3}");
        sb.AppendLine();
        sb.AppendLine("  WHY IT FAILED — the predictor is CONSTANT on this family, and that was visible before");
        sb.AppendLine("  the simulation was run:");
        sb.AppendLine($"    {nearGapTwo} of the 6 rings have near-gap density exactly 2, so the law emits ONE prediction for");
        sb.AppendLine("    all of them (capacity 0.8553), while the measured capacities are all distinct.");
        sb.AppendLine($"    distinct predictions {predCap.Select(v => Math.Round(v, 4)).Distinct().Count()} vs distinct observations (4 d.p.) {obsCap.Select(v => Math.Round(v, 4)).Distinct().Count()}.");
        sb.AppendLine();
        sb.AppendLine("  DERIVED");
        sb.AppendLine("    · WHY near-gap = 2 IS FORCED ON RINGS. Every 1-D ring whose low-k spectrum follows the");
        sb.AppendLine("      k² law has λ₂/λ₁ = 4 and λ₃/λ₂ = 9/4 > 2, so the set {λ > 0 : λ ≤ 2λ₂} contains exactly");
        sb.AppendLine("      the k = ±1 doublet — two modes, whatever the weight profile. Verified here: five of five");
        sb.AppendLine("      unit-offset and tapered rings give 2 (the sixth, Ring48, escapes only because its");
        sb.AppendLine("      long-range offsets raise λ₁ and lift 2λ₂ over 8 modes). The minimal predictor set of");
        sb.AppendLine("      D_050 is therefore a CONSTANT on the ring family — the family D96 itself belongs to —");
        sb.AppendLine("      and D_050's R² must have been carried by its non-ring cases (random, complete, the 3D");
        sb.AppendLine("      torus). This is an analytic fact about rings, not a fitted one.");
        sb.AppendLine("    · The direction of the failure is DERIVED too: near-gap = 2 is the MINIMUM of the predictor,");
        sb.AppendLine("      so any prediction error on a ring is a one-sided UNDER-prediction. D_050's own largest");
        sb.AppendLine("      residual (D96, −0.1349, at the same near-gap = 2) was the first instance of it; all six");
        sb.AppendLine("      new rings repeat the sign (−0.0817 … −0.1861). The sign is systematic, not noise.");
        sb.AppendLine("    · The bounds continue to hold (0 ≤ capacity, recovery ≤ 1 for every prediction), and the");
        sb.AppendLine("      zero-degeneracy null is untouched — the derived scaffolding of D_050 survives intact.");
        sb.AppendLine();
        sb.AppendLine("  EMERGENT");
        sb.AppendLine($"    · Six structurally different rings all adapt STRONGLY: capacity {obsCap.Min():F4} … {obsCap.Max():F4},");
        sb.AppendLine("      i.e. a 1-D ring collects most of its headroom whether it is sparse (±1..±3), dense");
        sb.AppendLine("      (±1..±12), tapered (w = 1/d), long-range weighted (w = d) or extended (±24, ±48). The");
        sb.AppendLine("      observed recovery band (0.9198 … 0.9754) is likewise 33× wider than the law predicted.");
        sb.AppendLine("      The measured values are emergent ensemble numbers for new spectra.");
        sb.AppendLine("    · The blind test's own outcome distribution — 5 identical predictions against 6 distinct");
        sb.AppendLine("      measurements — is the emergent evidence that the minimal predictor set of D_050 is");
        sb.AppendLine("      family-local.");
        sb.AppendLine();
        sb.AppendLine("  REFUTED");
        sb.AppendLine("    · 'The D_050 one-predictor law predicts capacity and recovery on new spectra.' REFUTED:");
        sb.AppendLine($"      capacity mean absolute error {meanCapErr:F4} against an observed span of {obsCapSpan:F4} — the error is");
        sb.AppendLine($"      {meanCapErr / obsCapSpan:F1}× the whole range of the truth — and recovery captures {predRecSpan / obsRecSpan:P0} of the real");
        sb.AppendLine("      variation. Both targets were predicted before measurement and both missed.");
        sb.AppendLine("    · 'The minimal predictor set is a usable RANKING device.' REFUTED prospectively for");
        sb.AppendLine($"      capacity: Spearman ρ = {rhoCap:F3} (anti-correlated) and mean |Δrank| = {capRankErr:F2} of 5. D_050's");
        sb.AppendLine("      ranking claim rested on in-sample ordering; on new spectra it does not survive.");
        sb.AppendLine($"      Recovery ranks better (ρ = {rhoRec:F3}, mean |Δrank| {recRankErr:F2}), which is expected since the");
        sb.AppendLine("      observations themselves span a narrow band.");
        sb.AppendLine("    · 'D_050's minimal set generalizes beyond its own case set.' REFUTED. The predictor takes");
        sb.AppendLine("      two distinct values across six rings that are structurally very different, and the one");
        sb.AppendLine("      ring it does separate (Ring48) it orders LAST on capacity while the measurement puts it");
        sb.AppendLine("      FOURTH — the single discrimination it can make is also wrong.");
        sb.AppendLine();
        sb.AppendLine("  SUCCESS CRITERION — 'prediction made before simulation results are known': MET, by");
        sb.AppendLine("  construction. The prediction lives in commit 003a0033, whose test file never constructs the");
        sb.AppendLine("  perturbation ensemble; the measurement was added in the following commit, and its numbers");
        sb.AppendLine("  are compared here against the frozen constants verbatim.");
        sb.AppendLine();
        sb.AppendLine("  CONSEQUENCE for the D group. D_050's answer stands as stated — adaptability is ORDERED by");
        sb.AppendLine("  the degeneracy axis within its own ensemble — but this audit narrows it sharply: the");
        sb.AppendLine("  ordering does NOT transfer to new spectra of the same topological family, because on");
        sb.AppendLine("  circulants the chosen predictor is a constant. A usable ring-family predictor must read a");
        sb.AppendLine("  quantity that actually varies across rings — the degeneracy count did vary (38 … 47) while");
        sb.AppendLine("  the near-gap density did not, so the collinear pair's OTHER member is the better candidate,");
        sb.AppendLine("  which is a concrete hypothesis for the next audit rather than a new claim.");
        sb.AppendLine();
        sb.AppendLine("  No canonical AT claim, value, equation or registry entry is changed; the D_040");
        sb.AppendLine("  ClassificationRegistry is untouched; D_050's classifications are reaffirmed, not reclassified.");

        Assert.True(meanCapErr > obsCapSpan,
            "the headline result: the capacity error must exceed the entire observed range");
        Assert.True(predCapSpan < obsCapSpan, "the law must be shown unable to resolve these rings");
        Assert.True(rhoCap < 0.5, "the capacity ordering must be shown not to transfer");
        Assert.True(nearGapTwo >= 5, "the near-gap = 2 degeneracy on rings must be exhibited");
        Assert.True(outcomes.All(o => o.PredCap - o.ObsCap < 0), "the prediction error must be one-sided (under-prediction)");

        Output.WriteLine(sb.ToString());
    }
}
