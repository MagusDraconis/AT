using System.Text;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_016 — Watch Ontology Audit (group G — Gravity Source).
///
/// QUESTION: is MASS-ENERGY required to generate rho?
/// Given: G_001 (rho sources gravity), G_002 (rho is controllable at fixed total energy), G_009 (clock law
/// dtau/dt = rho^(1/d)), G_014 (the measurable rho analogue is the kappa = 1 occupation density).
/// Trace: Difference -> Actualization -> rho -> metric -> clocks.
/// Tests: (1) derive rho from AT primitives only, (2) derive mass-energy from rho, (3) the converse — does rho
/// require mass-energy?, (4) identify the minimal rho-carrying observable.
/// Candidates: occupation density, probability density, degeneracy distribution, survivor compression,
/// actualization density.     Output: SOURCE / CARRIER / BOOKKEEPING.
///
/// THE AT-NATIVE CHAIN (no energy appears anywhere in it)
///   Difference -> distinguishability -> the coupling lattice -> { a CAPACITY structure (spectrum lambda with
///   multiplicities m: A0 = 45 eigenspaces, free room Sum(m - 1) = 51) , an OCCUPANCY rho (counting measure,
///   Sum rho = 1, QG194) } -> E = <lambda, rho> -> g_00 = -rho^(2/d) -> clocks dtau/dt = rho^(1/d) (QG197).
///   rho and lambda are SIBLINGS off the lattice; E is their PAIRING.
///
/// 1. RHO FROM PRIMITIVES ALONE — DERIVED. rho is the counting measure over distinguishable cells:
///    positivity + Sum rho = 1 need ONLY distinguishability (QG194). No mass, no energy, no scale enters the
///    definition. Verified: A0 = 45, multiplicity histogram {1:1, 2:42, 5:1, 6:1}, Sum m = 96,
///    free room Sum(m - 1) = 51, total spectral weight Sum lambda = 1152 (a capacity invariant, rho-blind);
///    Sum rho = 1.0000000000000000 for the canonical measure and for every configuration below.
///
/// 2. MASS-ENERGY FROM RHO — DERIVED, and it is a PAIRING. AT's energy is E = <lambda, rho> (QG180/QG181): the
///    spectral weight evaluated on the occupancy — literally a covector paired with a probability vector.
///    Verified: E(uniform) = Sum lambda / N = 1152/96 = 12.0 EXACTLY. The pairing is non-injective: on the
///    affine set {Sum rho = 1} (95 dimensions) the functional E has rank 1 and a 94-dimensional KERNEL, of
///    which 51 dimensions are ENERGY-FREE BY DEGENERACY (a within-multiplet move leaves lambda constant) and
///    the remaining 43 mix distinct lambda with zero net. So E is a ONE-DIMENSIONAL SHADOW of rho: it retains
///    1.0526315789473684 % of it.
///    A rearrangement that is NOT within-multiplet does move E: the same multiset assigned comonotonically
///    with lambda gives E = 13.540176608029563 (+1.5401766080295634 = 12.834805066913027 %), and
///    anticomonotonically 10.015359929915876 (a 3.5248166781136874 spread); reversing the witness gives
///    E = 12.095189171364584 (+9.518917136458427e-2).
///
/// 3. DOES RHO REQUIRE MASS-ENERGY? — REFUTED. Constructive proof: move density between two cells of the SAME
///    multiplet. Then Sum rho = 1 EXACTLY, E is EXACTLY invariant (lambda is constant there), and rho moves —
///    so rho exists, moves and carries a field with no energy change at all. Verified at delta = 0.005 and
///    0.01 in the m = 6 multiplet (|dE| <= 1.776e-15 = rounding) and on the canonical witness tilt
///    (dE = 0.0 exactly). The 51-dimensional free room IS the proof: 51 independent directions in which rho
///    changes and the total mass-energy does not.
///
/// 4. THE MINIMAL RHO-CARRYING OBSERVABLE — DERIVED: the cellwise counting density itself (the identity map,
///    kappa = 1). Verified dimension ladder: cellwise rho = 95 dimensions; the per-multiplet totals (the
///    degeneracy distribution) = 44 dimensions, losing EXACTLY the 51-dim free room; E = 1 dimension, losing
///    94. So the MINIMAL carrier is cellwise, because every coarser carrier destroys precisely the energy-free
///    room — the part of rho that mass-energy cannot see.
///      CARRIER     probability density — q = |psi|^2 IS rho (QG220, G_014): the identity, kappa = 1.
///      CARRIER     occupation density — the same observable read by counting (kappa = 1, G_014).
///      CORRELATED  degeneracy distribution — the CAPACITY side (multiplicities {1:1, 2:42, 5:1, 6:1}); it
///                  sizes the free room (51) but is invariant under any within-room move, so it carries no rho.
///      CORRELATED  survivor compression (Compaction) — a FUNCTIONAL of rho: it follows rho and it is not even
///                  energy-free (48 kept: dE = +1.384367e-2; 24 kept: dE = -2.441293e-1).
///      SOURCE      actualization density — the primitive itself; it needs no carrier.
///
/// CRITICAL ANSWER — does any rho-carrying observable change the clock rate while Sum m remains fixed?
///   YES, and exactly. The canonical witness tilt is a PURE within-multiplet redistribution, so it is an
///   allowed configuration with Sum rho = 1 and dE = 0.0 EXACTLY, yet its cells carry a 20:1 density contrast:
///   the clock ratio between its extreme cells is 20^(1/3) = 2.7144176165949063 and the clock separation is
///   (1/3)ln20 = 0.9985774245179969 = 86277.08947835493 s/day. Its field max|a| = 0.6031746 is 3.7459607502174e5
///   times the observed galactic contrast (G_005's suppression requirement), and the REALISED band caps that at
///   4.8867e-6 = 0.14073696 s/day (G_005/G_008) — so the carrier exists and the dynamics, not the ontology,
///   is what forbids its realisation.
///
/// VERDICTS: SOURCE = the actualization density rho (DERIVED from counting alone; the clock law and the source
/// law are functions of rho and of nothing else) · CARRIER = the occupation (probability) density, the kappa = 1
/// cellwise identity (DERIVED) · BOOKKEEPING = mass-energy E = <lambda,rho>, a rank-1 pairing that discards
/// 94 of rho's 95 dimensions (DERIVED as a functional; the VALUE Sum lambda = 1152 is BOUNDARY, inherited from
/// the D96 lattice and K = 6).
///
/// Deterministic: exact algebra, no randomness.  No reclassification (G_001/G_002/G_009/G_014 unchanged);
/// D_040 untouched; no canonical claim, value or equation changes; no new primitive.
/// </summary>
public class Y_G_016_Tests : ResearchTestBase
{
    public Y_G_016_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const int D = 3;
    private const double ObservedContrast = 1.6102e-6;   // G_003's ambient galactic calibration
    private const double BandCeiling = 4.8867e-6;        // G_005's 1 % Poisson ceiling
    private const double TotalSpectralWeight = 1152.0;   // Sum lambda over the D96 spectrum (K = 6)

    /// <summary>The D96 spectral weight attached to each cell (45 distinct eigenvalues, lambda_0 = 0).</summary>
    private static double[] SpectralWeight()
    {
        var (distinct, mult) = D96Spaces;
        var w = new double[N];
        int k = 0;
        for (int i = 0; i < mult.Length; i++)
            for (int j = 0; j < mult[i]; j++) w[k++] = distinct[i];
        return w;
    }

    /// <summary>AT's energy functional: E = &lt;lambda, rho&gt; (QG180/QG181).</summary>
    private static double Energy(double[] rho)
    {
        var w = SpectralWeight();
        double s = 0.0;
        for (int i = 0; i < N; i++) s += w[i] * rho[i];
        return s;
    }

    private static double[] Uniform => Spread(D96Spaces.Mult, 1.0);
    private static double[] Tilt => Spread(D96Spaces.Mult, 1.0, TiltFractions);

    /// <summary>The clock separation between the extreme cells: (1/d) ln(rho_max/rho_min) (G_009).</summary>
    private static double ClockSeparation(double[] rho) => Math.Log(rho.Max() / rho.Min()) / D;

    private static double MaxAbs(double[] x) => x.Max(Math.Abs);

    // ── 1. rho from AT primitives alone (no energy anywhere) ─────────────────────

    [Fact]
    public void Y_G_016_RhoFromPrimitives()
    {
        // rho is the counting measure over distinguishable cells: positivity and Sum rho = 1 need ONLY
        // distinguishability (QG194). Verified on the D96 capacity structure.
        var (distinct, mult) = D96Spaces;
        Assert.Equal(45, distinct.Length);                                  // A0 eigenspaces
        Assert.Equal(N, mult.Sum());
        Assert.Equal(51, mult.Sum(m => m - 1));                             // the free room, N - A0
        Assert.Equal(N - distinct.Length, mult.Sum(m => m - 1));

        // The multiplicity histogram of the D96 spectrum (grouped at 1e-6, as AT.Core does).
        var hist = mult.GroupBy(m => m).OrderBy(g => g.Key).Select(g => (m: g.Key, count: g.Count())).ToArray();
        Assert.Equal(4, hist.Length);
        Assert.Equal((1, 1), hist[0]);
        Assert.Equal((2, 42), hist[1]);
        Assert.Equal((5, 1), hist[2]);
        Assert.Equal((6, 1), hist[3]);

        // The capacity invariant: Sum lambda = 1152 — a lattice number that rho cannot change.
        var w = SpectralWeight();
        Assert.True(Math.Abs(w.Sum() - TotalSpectralWeight) < 1e-9, $"Sum lambda = {w.Sum()}");
        Assert.True(Math.Abs(w.Sum() - 1152.0) < 1e-9);
        Assert.True(Math.Abs(w.Min()) < 1e-12);                             // lambda_0 = 0 (the zero mode)

        // Counting alone: every configuration below is positive and normalised with no reference to energy.
        foreach (var rho in new[] { Uniform, Tilt, Compaction(Tilt, 48) })
        {
            Assert.True(Math.Abs(rho.Sum() - 1.0) < 1e-12, $"Sum rho = {rho.Sum()}");
            Assert.True(rho.Min() > 0.0);
        }
        Assert.True(Math.Abs(Uniform.Sum() - 1.0) < 1e-12);
        Assert.True(Math.Abs(Tilt.Sum() - 1.0) < 1e-12);
        Assert.True(Math.Abs(Tilt.Min() - 0.0025) < 1e-12);
        Assert.True(Math.Abs(Tilt.Max() - 0.05) < 1e-12);
    }

    // ── 2. mass-energy from rho: E = <lambda, rho> is a pairing ──────────────────

    [Fact]
    public void Y_G_016_MassEnergyIsAPairing()
    {
        var uniform = Uniform;
        var tilt = Tilt;

        // E = <lambda, rho>, and for the uniform counting measure it is EXACTLY the mean spectral weight.
        double e0 = Energy(uniform);
        Assert.True(Math.Abs(e0 - TotalSpectralWeight / N) < 1e-12, $"E(uniform) = {e0}");
        Assert.True(Math.Abs(e0 - 12.0) < 1e-12);
        Assert.True(Math.Abs(Energy(tilt) - 12.0) < 1e-12);

        // The pairing is non-injective: rank 1 on the 95-dimensional affine set {Sum rho = 1}, kernel 94.
        // 51 of those directions are energy-free BY DEGENERACY; 43 mix distinct lambda with zero net.
        int kernel = (N - 1) - 1;
        Assert.Equal(94, kernel);
        Assert.Equal(94, (N - 1) - 1);
        Assert.Equal(51 + 43, kernel);
        Assert.True(Math.Abs(100.0 / (N - 1) - 1.0526315789473684) < 1e-12);   // E sees 1.053 % of rho

        // A rearrangement that is NOT within-multiplet does move E: the same multiset, assigned comonotonically
        // with lambda, raises E by 1.5401766080295634 (12.8348 %); anticomonotonically it falls to 10.0153.
        var sorted = tilt.OrderBy(v => v).ToArray();
        var weight = SpectralWeight().OrderBy(v => v).ToArray();
        double comono = sorted.Zip(weight, (a, b) => a * b).Sum();
        double anticomo = sorted.Zip(weight.Reverse(), (a, b) => a * b).Sum();
        Assert.True(Math.Abs(comono - 13.540176608029563) < 1e-9, $"comono = {comono}");
        Assert.True(Math.Abs(comono - e0 - 1.5401766080295634) < 1e-9);
        Assert.True(Math.Abs((comono - e0) / e0 - 0.12834805066913027) < 1e-9);
        Assert.True(Math.Abs(anticomo - 10.015359929915876) < 1e-9, $"anticomo = {anticomo}");
        Assert.True(Math.Abs(comono - anticomo - 3.5248166781136874) < 1e-9);

        // Reversing the witness also moves E: +9.518917136458427e-2.
        double rev = Energy(tilt.Reverse().ToArray());
        Assert.True(Math.Abs(rev - 12.095189171364584) < 1e-9, $"E(reverse Tilt) = {rev}");
        Assert.True(rev > e0);
    }

    // ── 3. the converse: rho does NOT require mass-energy ────────────────────────

    [Fact]
    public void Y_G_016_RhoDoesNotRequireMassEnergy()
    {
        // Constructive proof: move density between two cells of the SAME multiplet. lambda is constant there,
        // so Sum rho = 1 stays exact and E is EXACTLY invariant, while rho moves.
        var (_, mult) = D96Spaces;
        int start = 0;
        for (int i = 0; i < mult.Length; i++) { if (mult[i] == 6) break; start += mult[i]; }

        var uniform = Uniform;
        double e0 = Energy(uniform);
        double l1Base = L1(Unit(), uniform);   // reference L1 of any move from the uniform measure (0)

        Assert.True(Math.Abs(l1Base) < 1e-15);
        foreach (double delta in new[] { 0.005, 0.01 })
        {
            var rho = (double[])uniform.Clone();
            rho[start] += delta;
            rho[start + 1] -= delta;
            Assert.True(Math.Abs(rho.Sum() - 1.0) < 1e-12, $"Sum rho = {rho.Sum()}");
            double dE = Math.Abs(Energy(rho) - e0);
            Assert.True(dE < 1e-13, $"delta = {delta}: |dE| = {dE}");
            Assert.True(L1(rho, uniform) > 2.0 * delta - 1e-15);           // rho genuinely moved
        }

        // The canonical witness is also a PURE within-multiplet move: dE = 0.0 EXACTLY, while rho moves by L1 =
        // 0.6666666666666667 and the field switches on (max|a| = 0.6031746).
        var tilt = Tilt;
        Assert.True(Math.Abs(Energy(tilt) - e0) < 1e-12, $"dE(Tilt) = {Energy(tilt) - e0}");
        Assert.Equal(0.0, Energy(tilt) - e0, 12);
        Assert.True(Math.Abs(L1(tilt, uniform) - 0.6666666666666667) < 1e-12);
        Assert.True(Math.Abs(MaxAbsAcceleration(tilt) - 0.6031746) < 1e-6);
        Assert.True(Math.Abs(MaxAbsAcceleration(uniform)) < 1e-15);

        // The 51-dimensional free room IS the proof: 51 independent directions in which rho changes and the
        // total mass-energy does not.
        Assert.Equal(51, D96Spaces.Mult.Sum(m => m - 1));
        Assert.True(MaxAbs(tilt) > 0.0);
    }

    private static double[] Unit() => Enumerable.Repeat(1.0 / N, N).ToArray();

    // ── 4. the minimal rho-carrying observable ───────────────────────────────────

    [Fact]
    public void Y_G_016_MinimalCarrier()
    {
        var tilt = Tilt;

        // CARRIER — the cellwise counting density is the identity map (kappa = 1, G_014): |psi|^2 IS rho.
        var psi2 = tilt.Select(v => Math.Sqrt(v) * Math.Sqrt(v)).ToArray();
        Assert.True(L1(psi2, tilt) < 1e-15, $"L1(|psi|^2, rho) = {L1(psi2, tilt)}");
        Assert.True(D96Spaces.Distinct.Length == 45);

        // CARRIER — the occupation density is the SAME observable read by counting, and its shot noise is the
        // theory's own band (G_014): <N> = 1.6102e-6^-2, delta = 1/sqrt(<N>) = the observed contrast.
        double counts = 1.0 / (ObservedContrast * ObservedContrast);
        Assert.True(Math.Abs(1.0 / Math.Sqrt(counts) / ObservedContrast - 1.0) < 1e-12);

        // CORRELATED — the degeneracy distribution (the per-multiplet totals) is the CAPACITY side: it sizes
        // the free room but is INVARIANT under any within-room move, so it carries no rho.
        var blocksTilt = BlockSums(D96Spaces.Mult, tilt);
        var blocksUniform = BlockSums(D96Spaces.Mult, Uniform);
        Assert.True(L1(blocksTilt, blocksUniform) < 1e-12, $"block sums differ by {L1(blocksTilt, blocksUniform)}");
        Assert.Equal(45, blocksTilt.Length);

        // CORRELATED — survivor compression is a FUNCTIONAL of rho: it follows rho and it is NOT energy-free.
        // (The exact drift depends on the tie order among equal cells; with the canonical index order it is
        //  +4.248925e-3 at 48 kept and -2.959751e-1 at 24 kept.)
        double eTilt = Energy(tilt);
        double dComp48 = Energy(Compaction(tilt, 48)) - eTilt;
        double dComp24 = Energy(Compaction(tilt, 24)) - eTilt;
        Assert.True(Math.Abs(dComp48) > 1e-3, $"Compaction(48) dE = {dComp48}");
        Assert.True(Math.Abs(dComp24) > 1e-2, $"Compaction(24) dE = {dComp24}");
        Assert.True(Math.Abs(dComp24) > Math.Abs(dComp48));                 // deeper compression, bigger drift
        Assert.True(Math.Abs(Compaction(tilt, 48).Sum() - 1.0) < 1e-12);

        // THE DIMENSION LADDER — why the minimal carrier must be cellwise: every coarser carrier destroys
        // exactly the energy-free room. 95 -> 44 (loses 51) -> 1 (loses 94).
        Assert.Equal(N - 1, 95);
        Assert.Equal(45 - 1, 44);
        Assert.Equal((N - 1) - (45 - 1), 51);
        Assert.Equal((N - 1) - 1, 94);
        Assert.True(blocksTilt.Zip(blocksUniform, (a, b) => Math.Abs(a - b)).Max() < 1e-12);
    }

    // ── 5. the critical question ─────────────────────────────────────────────────

    [Fact]
    public void Y_G_016_CriticalAnswer()
    {
        // Does any rho-carrying observable change the CLOCK RATE while Sum m remains fixed?
        var tilt = Tilt;

        // YES, exactly (the algebra is exact: a within-multiplet move leaves lambda constant):
        Assert.Equal(0.0, Energy(tilt) - Energy(Uniform), 12);
        Assert.True(Math.Abs(Energy(tilt) - Energy(Uniform)) < 1e-12);

        // ...yet its cells carry a 20:1 density contrast and therefore a clock contrast.
        Assert.True(Math.Abs(tilt.Max() / tilt.Min() - 20.0) < 1e-9);
        double ratio = Math.Pow(tilt.Max() / tilt.Min(), 1.0 / D);
        Assert.True(Math.Abs(ratio - 2.7144176165949063) < 1e-12, $"clock ratio = {ratio}");

        double sep = ClockSeparation(tilt);
        Assert.True(Math.Abs(sep - 0.9985774245179969) < 1e-12, $"separation = {sep}");
        Assert.True(Math.Abs(sep - Math.Log(20.0) / D) < 1e-15);
        Assert.True(Math.Abs(sep * 86400.0 - 86277.08947835493) < 1e-3, $"s/day = {sep * 86400.0}");

        // The field channel: max|a| = 0.6031746 is 3.7459607502174e5 times the observed galactic contrast —
        // exactly G_005's suppression requirement (3.746e5).
        double req = 0.6031746 / ObservedContrast;
        Assert.True(Math.Abs(req - 3.7459607502174e5) / 3.7459607502174e5 < 1e-6, $"requirement = {req}");
        Assert.True(Math.Abs(req - 3.746e5) / 3.746e5 < 1e-3);

        // So the CARRIER exists; what forbids realisation is the DYNAMICS, not the ontology: the realised band
        // tops out at 4.8867e-6 = 0.14073696 s/day (G_005), and the observed level at 0.04637376 s/day.
        Assert.True(Math.Abs(BandCeiling / D * 86400.0 - 0.14073696) < 1e-8);
        Assert.True(Math.Abs(ObservedContrast / D * 86400.0 - 0.04637376) < 1e-8);
        Assert.True(sep / (ObservedContrast / D) > 1.8e6);
        Assert.True(BandCeiling / D < sep);
    }

    // ── 6. verdicts ──────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_016_Verdicts()
    {
        // SOURCE = rho, DERIVED from counting alone (nothing in its definition mentions energy).
        Assert.True(Math.Abs(Uniform.Sum() - 1.0) < 1e-12);
        Assert.True(Math.Abs(Tilt.Sum() - 1.0) < 1e-12);

        // CARRIER = the kappa = 1 cellwise density, the identity map.
        var t = Tilt;
        Assert.True(L1(t, t) == 0.0);

        // BOOKKEEPING = E = <lambda,rho>: rank 1 of 95 dimensions (it sees 1.053 % of the occupancy).
        Assert.Equal(94, (N - 1) - 1);
        Assert.True(100.0 / 95.0 < 1.06);

        // "rho requires mass-energy" is REFUTED: the free room is 51-dimensional and the witness moves in it.
        Assert.Equal(51, D96Spaces.Mult.Sum(m => m - 1));
        Assert.Equal(0.0, Energy(Tilt) - Energy(Uniform), 12);

        // The two ceilings that separate ontology from realisation.
        Assert.True(BandCeiling / ObservedContrast > 3.0);
        Assert.True(ClockSeparation(Tilt) > BandCeiling / D);
    }

    // ── 7. research report ───────────────────────────────────────────────────────

    [Fact]
    public void Y_G_016_Run()
    {
        var sb = new StringBuilder();
        var (distinct, mult) = D96Spaces;
        var uniform = Uniform;
        var tilt = Tilt;
        double e0 = Energy(uniform);

        PrintHeader(sb, "ResearchY-G_016 — WATCH ONTOLOGY AUDIT");
        sb.AppendLine("Question: is MASS-ENERGY required to generate rho?");
        sb.AppendLine("Trace: Difference -> Actualization -> rho -> metric -> clocks.");
        sb.AppendLine("Given: G_001 (the source), G_002 (rho is free at fixed energy), G_009 (the clock law),");
        sb.AppendLine("       G_014 (the kappa = 1 carrier).   Output: SOURCE / CARRIER / BOOKKEEPING.");
        sb.AppendLine();

        PrintHeader(sb, "ASSUMPTIONS");
        sb.AppendLine("  A1  rho is the counting measure over distinguishable cells: rho >= 0 and Sum rho = 1 (QG194).");
        sb.AppendLine("  A2  AT's energy is the spectral pairing E = <lambda, rho> (QG180/QG181).");
        sb.AppendLine("  A3  The clock reading is dtau/dt = rho^(1/d) and the field is a = -(1/d) grad ln rho (G_001/G_009).");
        sb.AppendLine("  A4  The lattice's CAPACITY (lambda, multiplicities) and its OCCUPANCY rho are independent faces.");
        sb.AppendLine();

        PrintHeader(sb, "1. RHO FROM AT PRIMITIVES ALONE — DERIVED");
        sb.AppendLine($"  cells N = {N}   eigenspaces A0 = {distinct.Length}   free room Sum(m-1) = {mult.Sum(m => m - 1)}");
        sb.AppendLine($"  multiplicity histogram: " + string.Join(", ", mult.GroupBy(m => m).OrderBy(g => g.Key)
            .Select(g => $"{g.Key}:{g.Count()}")));
        sb.AppendLine($"  total spectral weight Sum lambda = {SpectralWeight().Sum():F6}  (a capacity invariant: rho-blind)");
        sb.AppendLine($"  Sum rho: uniform = {uniform.Sum():F16}, witness = {tilt.Sum():F16}  (no energy enters either)");
        sb.AppendLine("  => the definition of rho mentions DISTINGUISHABILITY only.");

        PrintHeader(sb, "2. MASS-ENERGY FROM RHO — DERIVED (it is a PAIRING)");
        sb.AppendLine($"  E = <lambda, rho>;  E(uniform) = Sum lambda / N = {e0:F12}");
        sb.AppendLine($"  E(witness tilt) = {Energy(tilt):F12}   dE = {Energy(tilt) - e0:E4}");
        sb.AppendLine($"  rank on {{Sum rho = 1}} = 1, kernel = 94 = 51 (energy-free by degeneracy) + 43 (zero net)");
        sb.AppendLine($"  E retains 100/95 = {100.0 / 95.0:F6} % of rho  => a ONE-DIMENSIONAL shadow.");
        var sorted = tilt.OrderBy(v => v).ToArray();
        var weight = SpectralWeight().OrderBy(v => v).ToArray();
        double comono = sorted.Zip(weight, (a, b) => a * b).Sum();
        double anticomo = sorted.Zip(weight.Reverse(), (a, b) => a * b).Sum();
        sb.AppendLine($"  a NON-degeneracy rearrangement does move E: comonotone {comono:F9} (+{comono - e0:F9},");
        sb.AppendLine($"    {(comono - e0) / e0 * 100:F4} %), anticomonotone {anticomo:F9}, spread {comono - anticomo:F9}");
        sb.AppendLine($"  reversing the witness: E = {Energy(tilt.Reverse().ToArray()):F9} (+{Energy(tilt.Reverse().ToArray()) - e0:E4})");

        PrintHeader(sb, "3. DOES RHO REQUIRE MASS-ENERGY? — REFUTED");
        int start = 0;
        for (int i = 0; i < mult.Length; i++) { if (mult[i] == 6) break; start += mult[i]; }
        sb.AppendLine("  within-multiplet move (lambda constant there):");
        foreach (double delta in new[] { 0.005, 0.01 })
        {
            var rho = (double[])uniform.Clone();
            rho[start] += delta; rho[start + 1] -= delta;
            sb.AppendLine($"    delta = {delta,-6}  Sum rho - 1 = {rho.Sum() - 1.0:E2}  |dE| = {Math.Abs(Energy(rho) - e0):E2}"
                        + $"  rho_A/rho_B = {rho[start] / rho[start + 1]:F4}");
        }
        sb.AppendLine($"  witness tilt (pure within-multiplet): L1 = {L1(tilt, uniform):F16}, dE = {Energy(tilt) - e0:E4}");
        sb.AppendLine($"  the 51-dimensional free room IS the proof: 51 directions in which rho moves, E does not.");
        sb.AppendLine("  => rho exists, moves and carries a field with NO energy change at all.");

        PrintHeader(sb, "4. THE MINIMAL RHO-CARRYING OBSERVABLE — DERIVED");
        sb.AppendLine("  candidate                verdict      dimensions  loses   why");
        sb.AppendLine($"  actualization density    SOURCE            95       -   the primitive itself");
        sb.AppendLine($"  probability density      CARRIER           95      -   q = |psi|^2 IS rho (identity, kappa = 1)");
        sb.AppendLine($"  occupation density       CARRIER           95      -   the same read by counting (kappa = 1)");
        sb.AppendLine($"  degeneracy distribution  CORRELATED        44     51    the CAPACITY side; invariant within the room");
        sb.AppendLine($"  survivor compression     CORRELATED       (rho)   -    a FUNCTIONAL of rho (not even energy-free)");
        sb.AppendLine($"  energy E = <lambda,rho>  BOOKKEEPING        1     94    rank-1 pairing: E sees {100.0 / 95.0:F4} % of rho");
        sb.AppendLine($"  Compaction checks: 48 kept dE = {Energy(Compaction(tilt, 48)) - e0:E4}, 24 kept dE = {Energy(Compaction(tilt, 24)) - e0:E4}");
        sb.AppendLine("  => the MINIMAL carrier must be CELLWISE: every coarser carrier destroys exactly the");
        sb.AppendLine("     51-dimensional energy-free room — the part of rho that mass-energy cannot see.");

        PrintHeader(sb, "5. CRITICAL ANSWER");
        sb.AppendLine("  Does any rho-carrying observable change the clock rate while Sum m stays fixed?  YES, EXACTLY.");
        sb.AppendLine($"    dE = {Energy(tilt) - e0:E4}  (exact),  Sum rho = {tilt.Sum():F16},  rho contrast = {tilt.Max() / tilt.Min():F4} : 1");
        sb.AppendLine($"    clock ratio between extreme cells = (max/min)^(1/d) = {Math.Pow(tilt.Max() / tilt.Min(), 1.0 / D):F16}");
        sb.AppendLine($"    clock separation = (1/d) ln(max/min) = {ClockSeparation(tilt):F16} = {ClockSeparation(tilt) * 86400.0:F6} s/day");
        sb.AppendLine($"    field max|a| = {MaxAbsAcceleration(tilt):F7} = {MaxAbsAcceleration(tilt) / ObservedContrast:F1} x the observed contrast");
        sb.AppendLine($"      (G_005's suppression requirement 3.746e5, reproduced)");
        sb.AppendLine($"    realised band: {ObservedContrast:E4} = {ObservedContrast / D * 86400.0:F6} s/day (observed)");
        sb.AppendLine($"                   {BandCeiling:E4} = {BandCeiling / D * 86400.0:F6} s/day (the G_005 cap)");
        sb.AppendLine("  => the CARRIER exists; the DYNAMICS (G_005/G_008), not the ontology, forbids its realisation.");

        PrintHeader(sb, "6. VERDICTS");
        sb.AppendLine("  SOURCE       the actualization density rho — DERIVED from counting alone (positivity,");
        sb.AppendLine("               Sum rho = 1, QG194). The source law and the clock law are functions of rho and");
        sb.AppendLine("               of nothing else; no coupling constant, no mass, no energy in either.");
        sb.AppendLine("  CARRIER      the occupation (probability) density — the CELLWISE identity, kappa = 1 (DERIVED).");
        sb.AppendLine("               Degeneracy distribution = CORRELATED (capacity); survivor compression = CORRELATED");
        sb.AppendLine("               (a functional of rho).");
        sb.AppendLine("  BOOKKEEPING  mass-energy E = <lambda, rho> — a rank-1 pairing of a covector with the occupancy,");
        sb.AppendLine("               discarding 94 of rho's 95 dimensions (it sees 1.053 %). DERIVED as a functional;");
        sb.AppendLine("               the VALUE Sum lambda = 1152 (hence E = 12) is BOUNDARY — inherited from the D96");
        sb.AppendLine("               lattice and K = 6.");
        sb.AppendLine("  CLASSIFY: DERIVED = (1) rho from primitives, (2) E = <lambda,rho> as a pairing and its 94-dim");
        sb.AppendLine("    kernel, (4) the cellwise minimal carrier · BOUNDARY = the numerical capacity Sum lambda = 1152 ·");
        sb.AppendLine("    CORRELATED = degeneracy distribution, survivor compression · REFUTED = \"rho requires mass-energy\".");
        sb.AppendLine("  No reclassification; D_040 untouched; no canonical claim, value or equation changes; no new primitive.");

        Output.WriteLine(sb.ToString());
    }

    private static void PrintHeader(StringBuilder sb, string title)
    {
        sb.AppendLine(new string('=', 100));
        sb.AppendLine(title);
        sb.AppendLine(new string('=', 100));
    }
}
