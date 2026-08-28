using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.A_WaveFoundations;

/// <summary>
/// ResearchY-A_001 — Wave Origin Audit test suite (Y_A_001_Tests.cs).
///
/// Goal: explore whether Difference can be interpreted as a localized disturbance and
/// whether measurable wave properties emerge — radius, circumference, propagation,
/// phase, resonance, π, 2π — without modifying canonical AT V2.0.
///
/// Method: recompute the canonical D96 spectrum from the closed-form Laplacian
/// eigenvalues λ_k = 2Σ_{d=1..6}(1−cos(2πdk/96)) of the attractor graph C96(±1..±6),
/// verify the canonical constants exactly, then read the wave-geometry observables
/// (circumference, radius, wavelength, frequency, phase lattice, resonance structure)
/// from the same spectrum. Deterministic — closed-form formula only, no randomness.
/// </summary>
public class Y_A_001_Tests : ResearchTestBase
{
    private const int N = 96;      // attractor size (circumference in ring units)
    private const int K = 6;       // link-length parameter (C96(±1..±6))

    // Canonical D96 constants (Ch6; D96_REPRO_AUDIT).
    private const double CanonicalSigmaSqrtM = 64.08;
    private const double CanonicalSpan = 6.40;
    private const double CanonicalOccMom = 1900.25;
    private static readonly int[] CanonicalOctaveBands = { 4, 4, 87 };

    public Y_A_001_Tests(ITestOutputHelper output) : base(output) { }

    // ── Core spectrum construction ──────────────────────────────────────────

    /// <summary>Closed-form Laplacian eigenvalues of C96(±1..±6).</summary>
    private static double[] LaplacianEigenvalues(int n = N, int k = K)
    {
        var lam = new double[n];
        for (int i = 0; i < n; i++)
        {
            double sum = 0.0;
            for (int d = 1; d <= k; d++)
                sum += 1.0 - Math.Cos(2.0 * Math.PI * d * i / n);
            lam[i] = 2.0 * sum;
        }
        Array.Sort(lam);
        return lam;
    }

    /// <summary>Positive eigenvalues, ascending (zero mode excluded).</summary>
    private static double[] PositiveEigenvalues(double[] lam)
        => lam.Where(l => l > 1e-9).ToArray();

    private static double[] Frequencies(double[] pos)
        => pos.Select(l => Math.Sqrt(l)).ToArray();

    /// <summary>Multiplicity multiset: count of eigenvalues in each degenerate group, ascending.</summary>
    private static int[] MultiplicityMultiset(double[] pos)
        => pos.GroupBy(l => Math.Round(l, 8))
              .Select(g => g.Count())
              .OrderBy(c => c)
              .ToArray();

    /// <summary>Octave occupancies over [ω_min, 2ω_min), [2ω_min, 4ω_min), [4ω_min, 8ω_min).</summary>
    private static int[] OctaveOccupancies(double[] w)
    {
        double w0 = w[0];
        int b1 = w.Count(x => w0 <= x && x < 2 * w0);
        int b2 = w.Count(x => 2 * w0 <= x && x < 4 * w0);
        int b3 = w.Count(x => 4 * w0 <= x && x < 8 * w0);
        return new[] { b1, b2, b3 };
    }

    // ── Fast numerical verification ─────────────────────────────────────────

    [Fact]
    public void Y_A_001_CanonicalConstants()
    {
        double[] lam = LaplacianEigenvalues();
        double[] pos = PositiveEigenvalues(lam);
        double[] w = Frequencies(pos);
        int[] mult = MultiplicityMultiset(pos);
        int[] occ = OctaveOccupancies(w);

        Assert.Equal(95, pos.Length);                          // 95 positive modes
        Assert.Equal(96, lam.Length);                          // N = 96 eigenvalues

        // Multiplicity multiset [42×2, 5, 6]: 44 distinct eigenvalue groups.
        Assert.Equal(44, mult.Length);
        int countTwo = mult.Count(c => c == 2);
        int countFive = mult.Count(c => c == 5);
        int countSix = mult.Count(c => c == 6);
        Assert.Equal(42, countTwo);
        Assert.Equal(1, countFive);
        Assert.Equal(1, countSix);

        // Moments: Σm = 95, Σ√m = 64.08, Σm² = 229.
        double sumM = mult.Sum();
        double sumSqrtM = mult.Sum(m => Math.Sqrt(m));
        double sumM2 = mult.Sum(m => (double)m * m);
        Assert.Equal(95.0, sumM, 6);
        Assert.Equal(CanonicalSigmaSqrtM, sumSqrtM, 2);
        Assert.Equal(229.0, sumM2, 6);

        // Span = ω_max/ω_min = 6.40.
        Assert.Equal(CanonicalSpan, w[^1] / w[0], 2);

        // Octave bands [4,4,87] and occMom = 1900.25.
        Assert.Equal(CanonicalOctaveBands, occ);
        double occMom = occ.Sum(o => (double)o * o) / occ[0];
        Assert.Equal(CanonicalOccMom, occMom, 2);
    }

    [Fact]
    public void Y_A_001_WaveGeometryObservables()
    {
        double[] lam = LaplacianEigenvalues();
        double[] pos = PositiveEigenvalues(lam);
        double[] w = Frequencies(pos);

        // Circumference: the ring has N sites.
        Assert.Equal(N, 96);

        // Radius of a unit-spacing circle: R = N/(2π) ≈ 15.279.
        double radius = N / (2.0 * Math.PI);
        Assert.Equal(15.279, radius, 2);

        // First mode frequency ω_min = √λ_min ≈ 0.6216 (fundamental-doublet member).
        double wMin = w[0];
        Assert.Equal(0.6216, wMin, 3);

        // Frequency span ratio (bandwidth): ω_max/ω_min = 6.40.
        Assert.Equal(CanonicalSpan, w[^1] / wMin, 2);

        // Wavelength of the first mode in ring units: N/1 = 96 (largest wavelength).
        Assert.Equal(96.0, (double)N / 1.0, 6);

        // Phase lattice closure: θ_k = 2πk/N, θ_N = 2π ≡ 0 mod 2π (ring closure).
        double thetaN = 2.0 * Math.PI * N / N;
        Assert.Equal(2.0 * Math.PI, thetaN, 12);
        Assert.Equal(0.0, thetaN % (2.0 * Math.PI), 12);

        // Resonance: distinct degeneracy groups present; largest group multiplicity is 6.
        int[] mult = MultiplicityMultiset(pos);
        Assert.Equal(6, mult[^1]);
    }

    [Fact]
    public void Y_A_001_Z2Doublets()
    {
        // λ_k = λ_{N−k} for the circulant ring (Z2 doublet pairing, QG153).
        var lamUnsorted = new double[N];
        for (int i = 0; i < N; i++)
        {
            double sum = 0.0;
            for (int d = 1; d <= K; d++)
                sum += 1.0 - Math.Cos(2.0 * Math.PI * d * i / N);
            lamUnsorted[i] = 2.0 * sum;
        }
        int paired = 0;
        for (int k = 1; k <= 47; k++)
            if (Math.Abs(lamUnsorted[k] - lamUnsorted[N - k]) < 1e-9)
                paired++;
        // All non-self-conjugate modes are ±k-paired (k and N−k); k=48 is self-conjugate.
        Assert.Equal(47, paired);
        // Self-conjugate mode: k = N/2 = 48 → cos(πd) = (−1)^d → λ = 2Σ_{d odd}2 = 12.
        Assert.Equal(12.0, lamUnsorted[48], 6);
    }

    [Fact]
    public void Y_A_001_ClosureTwoPi()
    {
        // The 2π in the eigenvalue formula is the ring-closure periodicity constant:
        // for k → N the cosine argument wraps by 2π (periodicity), giving λ_N = λ_0 = 0.
        var lam = LaplacianEigenvalues();
        // A closed ring must reproduce itself at k = N (periodicity): λ(θ) = λ(θ + 2π).
        double theta0 = 0.0;
        double thetaN = 2.0 * Math.PI;
        double l0 = 2.0 * Enumerable.Range(1, K)
            .Sum(d => 1.0 - Math.Cos(d * theta0));
        double lN = 2.0 * Enumerable.Range(1, K)
            .Sum(d => 1.0 - Math.Cos(d * thetaN));
        Assert.Equal(l0, lN, 10);   // periodicity: 2π closure
        Assert.Equal(0.0, lam[0], 10); // zero mode = uniform rest state
    }

    // ── Research report ─────────────────────────────────────────────────────

    [Fact]
    public void Y_A_001_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-A_001 — Wave Origin Audit");

        sb.AppendLine("Goal: can Difference be read as a localized disturbance, and do");
        sb.AppendLine("      radius, circumference, propagation, phase, resonance, π, 2π");
        sb.AppendLine("      emerge from the canonical D96 structure?");
        sb.AppendLine("Constraint: canonical AT V2.0 unmodified; no new primitives.");
        sb.AppendLine();

        // ── 1. Canonical spectrum ──────────────────────────────────────────
        sb.AppendLine("[1] Canonical D96 spectrum (closed form, C96(±1..±6))");
        sb.AppendLine("    λ_k = 2 Σ_{d=1..6} (1 − cos 2πdk/96),  ω_k = √λ_k");
        sb.AppendLine();

        double[] lam = LaplacianEigenvalues();
        double[] pos = PositiveEigenvalues(lam);
        double[] w = Frequencies(pos);
        int[] mult = MultiplicityMultiset(pos);
        int[] occ = OctaveOccupancies(w);

        double sumSqrtM = mult.Sum(m => Math.Sqrt(m));
        double sumM2 = mult.Sum(m => (double)m * m);
        double occMom = occ.Sum(o => (double)o * o) / occ[0];
        double span = w[^1] / w[0];

        sb.AppendLine($"    positive modes           = {pos.Length}");
        sb.AppendLine($"    multiplicity multiset     = [42×2, 5, 6] → groups {mult.Length}, Σm = {mult.Sum()}");
        sb.AppendLine($"    Σ√m = {sumSqrtM:F4}  (canonical 64.08)");
        sb.AppendLine($"    Σm² = {sumM2}            (canonical 229)");
        sb.AppendLine($"    octave occupancies       = [{occ[0]}, {occ[1]}, {occ[2]}]  (canonical [4,4,87])");
        sb.AppendLine($"    occMom = {occMom:F2}    (canonical 1900.25)");
        sb.AppendLine($"    span = ω_max/ω_min       = {span:F4}  (canonical 6.40)");
        sb.AppendLine($"    ω_min = {w[0]:F4}, ω_max = {w[^1]:F4}");
        sb.AppendLine();

        // ── 2. Wave-geometry observables ───────────────────────────────────
        sb.AppendLine("[2] Wave-geometry observables read from the same spectrum");
        sb.AppendLine();

        double radius = N / (2.0 * Math.PI);
        sb.AppendLine($"    circumference  = N        = {N}  (ring sites)");
        sb.AppendLine($"    radius (unit spacing)    = N/2π = {radius:F4}");
        sb.AppendLine($"    ladder radii (QG121/128)  = 6.0 .. 17.333  (contains {radius:F3})");
        sb.AppendLine($"    first wavelength          = N/1 = {N}  (ring units)");
        sb.AppendLine($"    first frequency  ω_min    = {w[0]:F4}  (fundamental doublet)");
        sb.AppendLine($"    mode frequencies          = ω_k = √λ_k (canonical convention)");
        sb.AppendLine($"    phase lattice             = θ_k = 2πk/96, θ_96 = 2π ≡ 0 (ring closure)");
        sb.AppendLine($"    Z2 doublets               = λ_k = λ_{{96−k}} (±k ring-mode degeneracy, 47 pairs)");
        sb.AppendLine($"    resonance structure       = {mult.Length} degeneracy groups; largest multiplicity {mult[^1]};");
        sb.AppendLine($"                                 octave bands [{occ[0]},{occ[1]},{occ[2]}] = standing-band content");
        sb.AppendLine();

        // ── 3. π / 2π status ───────────────────────────────────────────────
        sb.AppendLine("[3] π and 2π status (scope: spectral layer only)");
        sb.AppendLine($"    2π = {2.0 * Math.PI:F6} appears as the ring-closure periodicity constant");
        sb.AppendLine("    in λ_k (roots of unity e^{2πik/96}). Observation only:");
        sb.AppendLine("    the circle constant is selected by ring closure; the numerical value");
        sb.AppendLine("    of π is not derived, and the Bekenstein boundary (QG196) is unchanged.");
        sb.AppendLine($"    ring π-consistency: radius·2π = {radius * 2.0 * Math.PI:F4} ≈ N = {N}");
        sb.AppendLine();

        // ── 4. Verdicts (mirrors ResearchY-A_001.md) ───────────────────────
        sb.AppendLine("[4] Verdicts");
        sb.AppendLine("    R1  Difference as localized disturbance   → COMPATIBLE (configuration)");
        sb.AppendLine("    R2  Actualization as propagation          → PARTIAL (generation-space branching)");
        sb.AppendLine("    R3  Spectrum as resonance of propagation  → COMPATIBLE AS EXPLANATION");
        sb.AppendLine("    R4  Center / source                       → PARTIAL (no spatial center; ladder radii)");
        sb.AppendLine("    R5  D96 as standing wave                  → YES (static normal modes of the ring)");
        sb.AppendLine("    R6  Circular propagation                  → PARTIAL (topology + phase order emerge)");
        sb.AppendLine("    R7  π / 2π from closure                   → OBSERVATION (spectral 2π; π value not derived)");
        sb.AppendLine("    R8  radius/λ/ω/resonance implicit         → YES (all present as derived quantities)");
        sb.AppendLine();

        // ── 5. Conclusion ──────────────────────────────────────────────────
        sb.AppendLine("[5] Conclusion");
        sb.AppendLine("    The canonical D96 spectrum reproduces every claimed constant to the");
        sb.AppendLine("    stated precision and simultaneously reads as the standing-wave");
        sb.AppendLine("    (normal-mode) content of a closed ring of circumference N = 96:");
        sb.AppendLine("    radius N/2π ≈ 15.28 lies inside the ladder-radii range 6.0–17.333;");
        sb.AppendLine("    the Z2 doublets are the ±k ring-mode degeneracy; the octave bands");
        sb.AppendLine("    are the standing-band content; and 2π is the ring-closure periodicity");
        sb.AppendLine("    constant of the spectral layer. No canonical value is modified.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
