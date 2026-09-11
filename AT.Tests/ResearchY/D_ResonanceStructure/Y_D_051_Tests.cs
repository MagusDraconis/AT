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
}
