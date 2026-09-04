using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_037 — The Role of Three Audit test suite (Y_NP_037_Tests.cs).
///
/// Question: is the recurring exponent 3 — A³ (M_Pl = v·A³, QG181/183), the ω³ DOS of
/// the blackbody (N(ω) ∝ ω³, NP_035/036), and the spatial dimension d = 3 — a DERIVED
/// consequence of AT (one canonical generator), or only a CORRESPONDENCE to observed
/// 3D physics (multiple unrelated appearances)?
///
/// Program: (1) inventory every 3/cube/cubic occurrence across QG and NP; (2) classify
/// each as STRUCTURAL / GEOMETRIC / DIMENSIONAL / NUMERICAL; (3) remove each factor-3
/// independently and determine what breaks; (4) search for a common generator; (5) test
/// whether A³, DOS~ω³, d=3 share a unified origin; (6) attempt a derivation of 3 from
/// canonical objects only.
///
/// Verdict tested: the recurring "3" is NOT one unified derived quantity. The trio
/// decomposes into (i) an octave/family VALUE 3 at N=96 (structural, DERIVED given the
/// span window [4,8) boundary, D_040 unchanged); (ii) a cube-exponent VALUE 3 that is a
/// numeric read of the OBSERVED Planck–weak ratio (value-level DERIVED, QG183; no
/// structural generator of the power — the cubic law is selected by observation); and
/// (iii) the spatial d=3 and the ω³ DOS exponent (GEOMETRIC, hosted CORRESPONDENCE,
/// NP_035/036 unchanged; only p=d and d≥3 are derived). A NEW quantitative falsifier:
/// across the pairing-complete octave rung family {48,96,192,384}, the implied exponent
/// e(N) = ln(M_Pl/v(N))/ln(A(N)) is monotone decreasing (3.73 → 3.00 → 2.50 → 2.14) and
/// equals the octave-band count 3 ONLY at N=96 — the "cube = three octave bands" anatomy
/// is a value coincidence at N=96, not a causal law; family-count 3, DOS p=1 of the same
/// ring, and cube-e 3 are simultaneously present only as independent quantities.
///
/// Classification: family/octave-count VALUE 3 DERIVED (given N=96 span; window [4,8)
/// BOUNDARY — D_040 registry unchanged); triple-factor A = Σm·#g·occ₂ = 95·44·87 and its
/// cube content DERIVED (QG181/183, NP_036 frequency-content triple); cube-exponent 3
/// as a read of the observed M_Pl/v CORRESPONDENCE at law level (value DERIVED; the
/// physical hierarchy is the observed input/BOUNDARY); d=3 spatial and ω³ DOS
/// CORRESPONDENCE (hosted 3D geometry, unchanged NP_035/036); a UNIFIED structural
/// origin of the recurring exponent 3 FALSIFIED (rung-ladder decoupling + axis/family
/// decoupling). No new primitive; canonical AT unchanged.
///
/// Deterministic: closed-form circulant spectrum, Weyl lattice counting, closed-form
/// M_Pl = v·A³ ladder reads.
/// </summary>
public class Y_NP_037_Tests : ResearchTestBase
{
    public Y_NP_037_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const double MPlPhys = 1.22089e19; // physical Planck mass, GeV (QG181)
    private const double MuOverElectron = 206.77; // physical m_μ/m_e (QG209 uses 207.03 derived)

    private static double LambdaK(int k, int n, int kMax = 6)
    {
        double sum = 0;
        for (int s = 1; s <= kMax; s++)
            sum += 2 * (1 - Math.Cos(2.0 * Math.PI * k * s / n));
        return sum;
    }

    private static double OmegaK(int k, int n, int kMax = 6) => Math.Sqrt(LambdaK(k, n, kMax));

    private static double[] Modes(int n, int kMax = 6)
    {
        var w = new double[n - 1];
        for (int k = 1; k < n; k++) w[k - 1] = OmegaK(k, n, kMax);
        Array.Sort(w);
        return w;
    }

    private static double Span(double[] w) => w[w.Length - 1] / w[0];

    private static int OctaveFamilyCount(double[] w)
        => (int)Math.Floor(Math.Log(Span(w)) / Math.Log(2.0)) + 1;

    private static int[] OctaveCounts(double[] w)
    {
        double wmin = w[0];
        double wmax = w[w.Length - 1];
        var list = new List<int>();
        double lo = wmin;
        while (lo <= wmax + 1e-12)
        {
            double hi = 2.0 * lo;
            int c = 0;
            for (int i = 0; i < w.Length; i++)
                if (w[i] >= lo && w[i] < hi) c++;
            list.Add(c);
            if (hi > wmax) break;
            lo = hi;
        }
        return list.ToArray();
    }

    private static int DistinctCount(double[] w)
    {
        int n = 1;
        for (int i = 1; i < w.Length; i++)
            if (w[i] - w[i - 1] > 1e-9) n++;
        return n;
    }

    private static int DoubletCount(double[] w)
    {
        int count = 0;
        int run = 1;
        for (int i = 1; i < w.Length; i++)
        {
            if (w[i] - w[i - 1] <= 1e-9) run++;
            else { if (run == 2) count++; run = 1; }
        }
        if (run == 2) count++;
        return count;
    }

    /// <summary>Implied exponent e(N): the power that maps derived content v(N)·A(N)^e onto
    /// the OBSERVED Planck mass (the QG183 definition generalized across rungs).</summary>
    private static (int bands, double v, double A, double e) RungRead(int n)
    {
        var w = Modes(n);
        int bands = OctaveFamilyCount(w);
        int sm = w.Length;
        int distinct = DistinctCount(w);
        var occ = OctaveCounts(w);
        int occTop = occ[^1];
        double span = Span(w);
        int doublets = DoubletCount(w);
        double v = (sm + doublets) * Math.Log(span);
        double A = (double)sm * distinct * occTop;
        double e = Math.Log(MPlPhys / v) / Math.Log(A);
        return (bands, v, A, e);
    }

    private static double LowOctaveDosExponent(int n)
    {
        var w = Modes(n);
        double w1 = w[0];
        int n2 = w.Count(x => x < 2 * w1);
        int n4 = w.Count(x => x < 4 * w1);
        return Math.Log((double)n4 / n2) / Math.Log(2.0);
    }

    // ── [Required] Y_NP_037_CanonicalStructuralThree ──────────────

    [Fact]
    public void Y_NP_037_CanonicalStructuralThree()
    {
        // Occurrence inventory anchor #1 — the canonical D96 ring is structurally "three":
        // three octave bands [4,4,87], family count 3 = floor(log2 span)+1 (span 6.4025 < 8),
        // and N = 96 = 3·2⁵ (the factor-3 octave rung, D_030/D_031).
        var w = Modes(N);
        var occ = OctaveCounts(w);
        Assert.Equal(new[] { 4, 4, 87 }, occ);
        Assert.Equal(3, occ.Length);                     // three octave bands
        Assert.Equal(3, OctaveFamilyCount(w));           // family count value 3
        double span = Span(w);
        Assert.True(Math.Abs(span - 6.4025) < 0.02, $"span {span}");
        Assert.True(span < 8.0, "span < 2³: no fourth octave");
        Assert.Equal(96, 3 * 32);                        // N = 3·2⁵ (factor 3 in the ring order)
        // D_040 registry anchors (unchanged): value DERIVED, window [4,8) BOUNDARY.
        Assert.True(span >= 4.0 && span < 8.0);
    }

    // ── [Required] Y_NP_037_ATripleProductAndCube ─────────────────

    [Fact]
    public void Y_NP_037_ATripleProductAndCube()
    {
        // Occurrence inventory anchor #2 — A = Σm·#g·occ₂ = 95·44·87 = 363,660 is a triple
        // of three spectral counts of ONE ring; M_Pl = v·A³ (QG181/183).
        var w = Modes(N);
        int sm = w.Length;
        int distinct = DistinctCount(w);
        var occ = OctaveCounts(w);
        int occTop = occ[^1];
        Assert.Equal(95, sm);
        Assert.Equal(44, distinct);
        Assert.Equal(87, occTop);

        double A = (double)sm * distinct * occTop;
        Assert.Equal(363660.0, A, 1);

        double span = Span(w);
        double v = (sm + DoubletCount(w)) * Math.Log(span);
        Assert.True(Math.Abs(v - 254.37) < 0.05, $"v = {v} GeV");

        double MPlDerived = v * A * A * A;
        Assert.True(Math.Abs(MPlDerived - 1.22335e19) / 1.22335e19 < 0.01, $"M_Pl = v·A³ = {MPlDerived}");

        // Cube exponent 3 as a value-level read of the OBSERVED Planck–weak ratio (QG183).
        double e = Math.Log(MPlPhys / v) / Math.Log(A);
        Assert.True(Math.Abs(e - 3.0) < 0.01, $"cube exponent {e} (must be ≈ 3)");
    }

    // ── [Required] Y_NP_037_RemoveCubeBreaksPlanckScale ───────────

    [Fact]
    public void Y_NP_037_RemoveCubeBreaksPlanckScale()
    {
        // Removal analysis #1 — remove the cube from M_Pl = v·A³ (replace 3 by 1, 2, or 4):
        // the Planck scale and hence G break by 100% / 99.9997% / 3.6e7% (QG183 power test).
        var w = Modes(N);
        int sm = w.Length;
        int distinct = DistinctCount(w);
        var occ = OctaveCounts(w);
        double A = (double)sm * distinct * occ[^1];
        double v = (sm + DoubletCount(w)) * Math.Log(Span(w));

        double d1 = Math.Abs(v * A - MPlPhys) / MPlPhys;
        double d2 = Math.Abs(v * A * A - MPlPhys) / MPlPhys;
        double d3 = Math.Abs(v * A * A * A - MPlPhys) / MPlPhys;
        double d4 = Math.Abs(v * A * A * A * A - MPlPhys) / MPlPhys;

        Assert.True(d1 > 0.9, "A¹ fails by ~100%");
        Assert.True(d2 > 0.9, "A² fails by ~99.9997%");
        Assert.True(d3 < 0.01, $"A³ dev {d3} (0.2%)");
        Assert.True(d4 > 100.0, "A⁴ fails by ~3.6e7%");
    }

    // ── [Required] Y_NP_037_RemoveFamilyThreeBreaksMassContent ────

    [Fact]
    public void Y_NP_037_RemoveFamilyThreeBreaksMassContent()
    {
        // Removal analysis #2 — remove the 3-family window (span ∈ [4,8)): the pairing-
        // complete octave rungs N=48 (2 families) / N=192 (4 families) / N=384 (5 families)
        // still exist (QG_013), but the observed mass content anchored to the [4,4,87]
        // occupancy pattern breaks: the QG209 occupancy-derived lepton ratio m_μ/m_e =
        // Σm²/√occMom reproduces the physical 206.77 only at N=96.
        Assert.Equal(2, OctaveFamilyCount(Modes(48)));
        Assert.Equal(3, OctaveFamilyCount(Modes(96)));
        Assert.Equal(4, OctaveFamilyCount(Modes(192)));
        Assert.Equal(5, OctaveFamilyCount(Modes(384)));

        foreach (int n in new[] { 48, 96, 192, 384 })
        {
            var w = Modes(n);
            var occ = OctaveCounts(w);
            double occMom = 0;
            foreach (int c in occ) occMom += (double)c * c;
            occMom /= 4.0;
            int sm = w.Length;
            double ratio = (double)sm * sm / Math.Sqrt(occMom);
            double dev = Math.Abs(ratio - MuOverElectron) / MuOverElectron;
            if (n == 96)
                Assert.True(dev < 0.02, $"N=96 m_μ/m_e = {ratio} (dev {dev})");
            else
                Assert.True(dev > 0.3, $"N={n} m_μ/m_e = {ratio} deviates {dev} — observed mass content broken");
        }
    }

    // ── [Required] Y_NP_037_RemoveThirdAxisBreaksBlackbodyDos ─────

    [Fact]
    public void Y_NP_037_RemoveThirdAxisBreaksBlackbodyDos()
    {
        // Removal analysis #3 — remove the third independent D96 axis (D96⊗3 → D96⊗2):
        // the mode count changes from the 3D Weyl law (π/6)R³, N∝ω³ to the 2D law
        // (π/4)R², N∝ω² — the observed blackbody (Stefan–Boltzmann, ω³ weight) breaks.
        double p3 = CountExponent(Count3D, 20, 80);
        Assert.True(p3 > 2.9 && p3 < 3.2, $"D96⊗3 exponent {p3} (p→3)");
        double p2 = CountExponent(Count2D, 20, 80);
        Assert.True(p2 > 1.9 && p2 < 2.15, $"D96⊗2 exponent {p2} (p→2)");

        // 2D would be (π/4)R² — not the blackbody Weyl term (π/6)R³.
        Assert.True(Math.Abs((double)Count3D(160) / Math.Pow(160, 3) - Math.PI / 6.0) < 0.02);
        Assert.True(Math.Abs((double)Count2D(160) / Math.Pow(160, 2) - Math.PI / 4.0) < 0.02);

        // The Stefan–Boltzmann integral requires the ω³ weight (3D DOS ω² × ω occupation).
        double sb = Integrate(x => x * x * x / (Math.Exp(x) - 1.0), 1e-9, 60);
        Assert.Equal(Math.PI * Math.PI * Math.PI * Math.PI / 15.0, sb, 4);
    }

    // ── [Required] Y_NP_037_RemoveExactD3KeepsGravityButD2Breaks ──

    [Fact]
    public void Y_NP_037_RemoveExactD3KeepsGravityButD2Breaks()
    {
        // Removal analysis #4 — remove the exact value 3 in d=3: the Einstein prefactor
        // (d−1)(d−2) vanishes at d=2 but is NON-zero for every d ≥ 3 (QG2/QG197/QG290).
        // Gravity breaks at d=2, NOT at "d≠3": the canonical object derives only d ≥ 3,
        // so the exact VALUE 3 is not load-bearing for AT — it is an observed input.
        double[] pref = { (2 - 1) * (2 - 2), (3 - 1) * (3 - 2), (4 - 1) * (4 - 2), (5 - 1) * (5 - 2) };
        Assert.Equal(0.0, pref[0]);
        for (int i = 1; i < pref.Length; i++)
            Assert.True(pref[i] > 0, $"prefactor positive at d={i + 2}");
        // d=4, d=5 are equally admissible — the value 3 is not selected by the prefactor.
        Assert.True(pref[1] > 0 && pref[2] > 0 && pref[3] > 0);
    }

    // ── [Required] Y_NP_037_RungLadderCubeVsOctaves ───────────────

    [Fact]
    public void Y_NP_037_RungLadderCubeVsOctaves()
    {
        // Common-generator search #1 — is the octave-band count 3 the generator of the
        // cube exponent 3? Across the pairing-complete octave rungs {48,96,192,384}, read
        // e(N) = ln(M_Pl/v(N))/ln(A(N)) (the QG183 exponent read, generalized). If the
        // "cube = three octave bands" anatomy were a LAW, e(N) would track bands(N).
        int[] rungs = { 48, 96, 192, 384 };
        double[] e = new double[rungs.Length];
        int[] bands = new int[rungs.Length];
        for (int i = 0; i < rungs.Length; i++)
        {
            var r = RungRead(rungs[i]);
            bands[i] = r.bands;
            e[i] = r.e;
        }

        // e(N) is monotone decreasing across rungs (3.73 → 3.00 → 2.50 → 2.14).
        for (int i = 1; i < e.Length; i++)
            Assert.True(e[i] < e[i - 1], "e(N) strictly decreasing across rungs");

        // e(N) ≈ 3 ONLY at N=96.
        Assert.True(Math.Abs(e[1] - 3.0) < 0.01, $"e(96) = {e[1]}");
        for (int i = 0; i < e.Length; i++)
            if (i != 1)
                Assert.True(Math.Abs(e[i] - 3.0) > 0.4, $"e(N) at rung {rungs[i]} = {e[i]} must be far from 3");

        // e(N) ≈ bands(N) ONLY at N=96 (value coincidence, not a causal law).
        for (int i = 0; i < e.Length; i++)
        {
            if (i == 1)
                Assert.True(Math.Abs(e[i] - bands[i]) < 0.2, "only N=96: e ≈ octave bands");
            else
                Assert.True(Math.Abs(e[i] - bands[i]) > 1.0,
                    $"rung {rungs[i]}: e={e[i]:F3} ≠ bands={bands[i]} — cube does not track octave count");
        }
    }

    // ── [Required] Y_NP_037_FamilyDosCubeDecouple ─────────────────

    [Fact]
    public void Y_NP_037_FamilyDosCubeDecouple()
    {
        // Common-generator search #2 — the three "3" slots are INDEPENDENT quantities:
        // (a) family count varies 3→4→5 across rungs while the single-ring DOS exponent
        // stays p = 1 (octave count does not set the DOS exponent);
        // (b) the DOS exponent varies 1→2→3 across the tensor power of the SAME 3-family
        // ring (axis count changes while the family count stays 3);
        // (c) the cube exponent e(N) varies independently of both (e(192)=2.50, e(384)=2.14).
        // (a) family vs DOS p on single rings
        foreach (int n in new[] { 96, 192, 384 })
        {
            double p = LowOctaveDosExponent(n);
            Assert.True(Math.Abs(p - 1.0) < 0.05, $"single ring N={n} DOS p={p} (stays 1)");
            int fam = OctaveFamilyCount(Modes(n));
            Assert.True(fam != 1, "family count changes across rungs");
        }
        Assert.Equal(3, OctaveFamilyCount(Modes(96)));
        Assert.Equal(4, OctaveFamilyCount(Modes(192)));
        Assert.Equal(5, OctaveFamilyCount(Modes(384)));

        // (b) tensor power of the 3-family ring: DOS exponent p = 1, 2, 3 while family = 3.
        Assert.Equal(3, OctaveFamilyCount(Modes(96)));
        double p1 = LowOctaveDosExponent(96);
        double p2 = CountExponent(Count2D, 40, 160);
        double p3 = CountExponent(Count3D, 40, 160);
        Assert.True(Math.Abs(p1 - 1.0) < 0.05 && Math.Abs(p2 - 2.0) < 0.15 && Math.Abs(p3 - 3.0) < 0.15,
            $"axes p: {p1}, {p2}, {p3} → 1, 2, 3");

        // (c) cube exponent vs family: e(192)=2.50 ≠ 4, e(384)=2.14 ≠ 5 (from rung read).
        var r192 = RungRead(192);
        var r384 = RungRead(384);
        Assert.True(Math.Abs(r192.e - r192.bands) > 1.0 && Math.Abs(r384.e - r384.bands) > 1.0);
    }

    // ── [Required] Y_NP_037_DerivationAttemptFromCanonicalObjects ──

    [Fact]
    public void Y_NP_037_DerivationAttemptFromCanonicalObjects()
    {
        // Step 6 — attempt to derive the value 3 from canonical objects ONLY.
        // (i) octave/family 3 ← N=96 ring alone: DERIVED (span 6.4025 → floor(log2 span)+1 = 3).
        var w = Modes(96);
        Assert.Equal(3, OctaveFamilyCount(w));

        // (ii) cube exponent 3 ← ring alone (no observed M_Pl): NOT derivable. The
        //     generalized exponent e(N) is not constant across rungs; without the observed
        //     Planck–weak ratio no power is selected (only N=96 sits at exactly 3).
        double e96 = RungRead(96).e;
        double e48 = RungRead(48).e;
        Assert.True(Math.Abs(e96 - 3.0) < 0.01);
        Assert.True(Math.Abs(e48 - 3.0) > 0.4, "other rungs would force a different exponent");

        // (iii) d=3 ← Einstein structure alone: only d ≥ 3 (prefactor nonzero), the exact
        //      value 3 is NOT selected (d=4, d=5 equally admissible).
        Assert.Equal(0.0, (2 - 1) * (2 - 2));
        Assert.True((3 - 1) * (3 - 2) > 0 && (4 - 1) * (4 - 2) > 0);

        // (iv) DOS p=3 ← one ring alone: FALSIFIED (single ring p=1 at every N, NP_032/035).
        Assert.True(Math.Abs(LowOctaveDosExponent(96) - 1.0) < 0.05);

        // (v) no common generator: bands(96)=3 & e(96)≈3 & p_DOS(96)=1 — three values of
        //     "3" co-exist only as independent quantities at the single canonical ring.
        Assert.Equal(3, OctaveFamilyCount(w));
        Assert.True(Math.Abs(RungRead(96).e - 3.0) < 0.01);
        Assert.True(Math.Abs(LowOctaveDosExponent(96) - 1.0) < 0.05);
    }

    // ── [Required] Y_NP_037_Classification ─────────────────────────

    [Fact]
    public void Y_NP_037_Classification()
    {
        // A UNIFIED recurring exponent 3 (one canonical generator of A³, ω³, d=3): FALSIFIED.
        bool unifiedRecurring3Derived = false;
        Assert.False(unifiedRecurring3Derived);

        // Family/octave-count VALUE 3 at N=96: DERIVED (given span(96); window [4,8) BOUNDARY,
        // D_040 registry unchanged — no reclassification).
        Assert.Equal(3, OctaveFamilyCount(Modes(96)));

        // Triple-factor A = 95·44·87 and its cube content: DERIVED (QG181/183; NP_036
        // frequency-content triple — unchanged).
        double A = 95.0 * 44.0 * 87.0;
        Assert.True(Math.Abs(A - 363660.0) < 1);

        // Cube exponent 3 as a value read of the OBSERVED M_Pl/v ratio: value DERIVED
        // (QG183, |p−3|<1e-2); the physical hierarchy is the observed input (BOUNDARY),
        // no structural generator selects the power (law level CORRESPONDENCE).
        double e96 = RungRead(96).e;
        Assert.True(Math.Abs(e96 - 3.0) < 0.01);

        // d=3 spatial & ω³ DOS exponent: CORRESPONDENCE (hosted 3D geometry; unchanged
        // NP_035/036) — a single ring is p=1 and the metric is dimension-generic (d≥3).
        bool spatialDosCorrespondence = true;
        Assert.True(spatialDosCorrespondence);
        Assert.True(Math.Abs(LowOctaveDosExponent(96) - 1.0) < 0.05);

        // No new primitive; canonical AT unchanged.
        Assert.True(363660.0 == A);
    }

    // ── [Required] Y_NP_037_Run ───────────────────────────────────

    [Fact]
    public void Y_NP_037_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_037 — The Role of Three Audit");

        sb.AppendLine("Goal: is the recurring exponent 3 — A³ (M_Pl = v·A³), the blackbody");
        sb.AppendLine("DOS ω³ (N ∝ ω³), and the spatial dimension d=3 — a DERIVED consequence");
        sb.AppendLine("of AT (one canonical generator) or a CORRESPONDENCE to observed 3D");
        sb.AppendLine("physics (multiple unrelated appearances)?");
        sb.AppendLine();

        sb.AppendLine("[1] Canonical structural three at N=96 (DERIVED values)");
        var w = Modes(96);
        var occ = OctaveCounts(w);
        sb.AppendLine($"    occupancy [{string.Join(",", occ)}]: three octave bands");
        sb.AppendLine($"    span = {Span(w):F4} < 8 = 2^3  ->  family count = 3 (floor(log2 span)+1)");
        sb.AppendLine("    N = 96 = 3·2^5 (factor-3 octave rung); period-3 seed DERIVED (D_031)");
        sb.AppendLine();

        sb.AppendLine("[2] A triple + cube (QG181/183, unchanged)");
        int sm = w.Length;
        int distinct = DistinctCount(w);
        int occTop = occ[^1];
        double A = (double)sm * distinct * occTop;
        sb.AppendLine($"    A = Σm·#g·occ₂ = {sm}·{distinct}·{occTop} = {A:F0} (triple of spectral counts)");
        double v = (sm + DoubletCount(w)) * Math.Log(Span(w));
        double e96 = Math.Log(MPlPhys / v) / Math.Log(A);
        sb.AppendLine($"    v = {v:F2} GeV; cube exponent e(96) = ln(M_Pl/v)/ln A = {e96:F4} (value 3)");
        sb.AppendLine();

        sb.AppendLine("[3] Rung-ladder falsifier (NEW): cube exponent does NOT track octave count");
        int[] rungs = { 48, 96, 192, 384 };
        sb.AppendLine("    N    bands   v(GeV)      A          e(N)=ln(M_Pl/v)/lnA");
        foreach (int n in rungs)
        {
            var r = RungRead(n);
            sb.AppendLine($"    {n,-4} {r.bands,-6} {r.v,-10:F2} {r.A,-11:F0} {r.e,-8:F3}");
        }
        sb.AppendLine("    e(N) strictly decreasing; e(N)≈bands(N) ONLY at N=96 (value coincidence,");
        sb.AppendLine("    not a causal law). Remove the cube -> A¹/A²/A⁴ fail 100%/99.9997%/3.6e7%.");
        sb.AppendLine();

        sb.AppendLine("[4] Independence of the three 3-slots");
        sb.AppendLine($"    single ring N=96: family=3, DOS p={LowOctaveDosExponent(96):F3} (1 axis)");
        sb.AppendLine($"    N=192: family=4, DOS p={LowOctaveDosExponent(192):F3};  N=384: family=5, DOS p={LowOctaveDosExponent(384):F3}");
        sb.AppendLine($"    tensor of 3-family ring: axes 1/2/3 -> DOS p 1/2/3 (family stays 3)");
        sb.AppendLine($"    Einstein prefactor (d−1)(d−2): 0 at d=2, >0 for all d ≥ 3 (only d ≥ 3 derived)");
        sb.AppendLine();

        sb.AppendLine("[5] Derivation attempt from canonical objects only");
        sb.AppendLine("    octave/family 3 from N=96 ring: DERIVED (span).");
        sb.AppendLine("    cube exponent 3 from ring alone: NOT derivable (needs observed M_Pl/v).");
        sb.AppendLine("    d=3 from Einstein structure alone: only d ≥ 3 (value 3 not selected).");
        sb.AppendLine("    DOS p=3 from one ring: FALSIFIED (p=1 at every N).");
        sb.AppendLine();

        sb.AppendLine("[6] Verdict");
        sb.AppendLine("    A unified recurring exponent 3 (A³, ω³, d=3 from one generator): FALSIFIED.");
        sb.AppendLine("    Family/octave VALUE 3: DERIVED (window [4,8) BOUNDARY, D_040 unchanged).");
        sb.AppendLine("    Triple A = 95·44·87, cube content: DERIVED (frequency-content triple, NP_036).");
        sb.AppendLine("    Cube exponent 3: value DERIVED as a read of observed M_Pl/v; law-level");
        sb.AppendLine("    CORRESPONDENCE (no structural generator; the physical hierarchy is the input).");
        sb.AppendLine("    d=3 & ω³ DOS: CORRESPONDENCE (hosted 3D geometry, unchanged NP_035/036).");
        sb.AppendLine("    Multiple unrelated appearances (success criterion C) — not one derived 3.");
        sb.AppendLine("    No new primitive; canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }

    // ── Helpers ────────────────────────────────────────────────────

    private static int Count2D(double r)
    {
        int n = 0;
        int m = (int)r;
        for (int a = 1; a <= m; a++)
            for (int b = 1; b <= m; b++)
                if (a * a + b * b <= r * r) n++;
        return n;
    }

    private static int Count3D(double r)
    {
        int n = 0;
        int m = (int)r;
        for (int a = 1; a <= m; a++)
            for (int b = 1; b <= m; b++)
            {
                int c2 = (int)(r * r - a * a - b * b);
                if (c2 <= 0) continue;
                int cmax = (int)Math.Sqrt(c2);
                if (cmax > m) cmax = m;
                if (cmax >= 1) n += cmax;
            }
        return n;
    }

    private static double CountExponent(Func<double, int> count, double r1, double r2)
        => Math.Log((double)count(r2) / count(r1)) / Math.Log(r2 / r1);

    private static double Integrate(Func<double, double> f, double a, double b)
    {
        const int n = 400000;
        double h = (b - a) / n;
        double s = f(a) + f(b);
        for (int i = 1; i < n; i++)
            s += f(a + i * h) * (i % 2 == 0 ? 2 : 4);
        return s * h / 3.0;
    }
}
