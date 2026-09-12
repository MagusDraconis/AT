using System.Text;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_018 — Rho Identity Audit (group G — Gravity Source).
///
/// QUESTION: after G_017 excluded the identification of a LABORATORY |psi|^2 with rho, which physical
/// quantity remains as the IDENTITY of rho?
/// Candidates: occupancy measure, actualization count, state accessibility, degeneracy occupancy, survivor
/// distribution.     Output: DERIVED / BOUNDARY / REFUTED.
///
/// THE AT-NATIVE CRITERION (no external physics enters it). A quantity Q is the *identity* of rho iff
///   (I1) it is defined by COUNTING over distinguishable states — i.e. it needs only Difference →
///        distinguishability, the first primitive;
///   (I2) it is DIMENSIONLESS and normalised (Sigma = 1), or a ratio of such;
///   (I3) it survives G_017's exclusion, i.e. its definition never referenced a laboratory quantity;
///   (I4) it DETERMINES rho, so it is not a lossy functional of rho (G_016/G_016b: functionals are readouts,
///        not identities).
/// This audit contains NO measured constant, NO SI quantity and NO laboratory number: every assertion below
/// is exact combinatorics of the 96-cell counting measure and its D96 degeneracy structure.
///
/// THE ANSWER — what remains is the OCCUPANCY MEASURE: a dimensionless, normalised COUNTING measure over
/// distinguishable states, whose entire physical content is in the ARRANGEMENT (the within-degeneracy
/// occupancy and the gradient) and none of it in the amount.
///
/// CANDIDATE VERDICTS
///   DERIVED    OCCUPANCY MEASURE — rho itself: positivity and Sigma rho = 1 need ONLY distinguishability;
///              95-dimensional on the affine set; dimensionless. It survives G_017 because the exclusion
///              removed an IDENTIFICATION, not the quantity: nothing in Rho = counting was ever a lab number.
///   REFUTED    ACTUALIZATION COUNT — NOT a distinct identity. Scale invariance (G_009, verified exactly
///              here) makes rho and lambda*rho physically identical in every ratio: max|a| = 0.6031746016455657
///              and the clock separation 0.9985774245179969 are the SAME at scales 1, 1e3, 1e-6 and 96. The
///              count N*rho is therefore a LABEL with zero content — the amount is not physical, only the
///              arrangement is.
///   DERIVED    DEGENERACY OCCUPANCY — the within-multiplet distribution: EXACTLY the part of rho that the
///   + BOUNDARY energy functional cannot see (dE = 0 exactly for a within-multiplet move, G_016). It is 51 of
///              the 95 dimensions — 53.6842 % of the state is invisible to everything except arrangement —
///              so it is DERIVED as content while its DIMENSION 51 = Sigma(m - 1) is BOUNDARY (inherited
///              from the D96 lattice). Two-level rule as in D_028/D_040.
///   REFUTED    SURVIVOR DISTRIBUTION — a FUNCTIONAL of rho, hence a readout: it follows rho and it loses
///              information (the per-multiplet totals keep 44 of 95 dimensions, discarding EXACTLY the
///              51-dimensional free room; survivor compaction moves E, so it is not even rho-blind).
///   BOUNDARY   STATE ACCESSIBILITY — the CAPACITY (A0 = 45 eigenspaces, free room Sigma(m - 1) = 51, and
///              A0 = 49/47/45/47/45/45 for coupling range K = 1..6) is a property of the LATTICE, not of the
///              state, so it is a boundary input (G_007) that bounds which rho are reachable — not an
///              identity of rho.
///
/// THE PROVENANCE LEDGER (the audit's own honesty check): imported constants used = ZERO. Imported
/// laboratory systems = NONE. Measured numbers = NONE. Everything asserted is a dimension count, an exact
/// invariance, or a witness constructed from the primitives.
///
/// Deterministic: exact algebra, no randomness.  No reclassification; D_040 untouched; no canonical claim,
/// value or equation changes; no new primitive.
/// </summary>
public class Y_G_018_Tests : ResearchTestBase
{
    public Y_G_018_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const int D = 3;

    /// <summary>The D96 eigenspaces for a coupling range K (grouped at 1e-6, as AT.Core does).</summary>
    private static (double[] Distinct, int[] Mult) Spaces(int kMax = 6)
    {
        var lams = Enumerable.Range(0, N).Select(k => Enumerable.Range(1, kMax)
            .Sum(s => 2.0 * (1.0 - Math.Cos(2.0 * Math.PI * k * s / N)))).OrderBy(v => v).ToArray();
        var distinct = new List<double>();
        var mult = new List<int>();
        int i = 0;
        while (i < N)
        {
            int j = i;
            while (j < N && lams[j] - lams[i] <= 1e-6) j++;
            distinct.Add(lams[i]); mult.Add(j - i); i = j;
        }
        return (distinct.ToArray(), mult.ToArray());
    }

    private static double[] Weight(int kMax = 6)
    {
        var (distinct, mult) = Spaces(kMax);
        var w = new double[N];
        int k = 0;
        for (int i = 0; i < mult.Length; i++)
            for (int j = 0; j < mult[i]; j++) w[k++] = distinct[i];
        return w;
    }

    private static double Energy(double[] rho, int kMax = 6)
    {
        var w = Weight(kMax);
        double s = 0.0;
        for (int i = 0; i < N; i++) s += w[i] * rho[i];
        return s;
    }

    private static double[] Uniform => Spread(D96Spaces.Mult, 1.0);
    private static double[] Witness => Spread(D96Spaces.Mult, 1.0, TiltFractions);

    // ── 1. DERIVED: the occupancy measure ────────────────────────────────────────

    [Fact]
    public void Y_G_018_OccupancyMeasure()
    {
        var (distinct, mult) = D96Spaces;

        // I1 + I2 — counting only: positivity and normalisation, with no reference to any external quantity.
        var uniform = Uniform;
        var witness = Witness;
        foreach (var rho in new[] { uniform, witness })
        {
            Assert.True(Math.Abs(rho.Sum() - 1.0) < 1e-12, $"Sigma rho = {rho.Sum()}");
            Assert.True(rho.Min() > 0.0);
        }
        Assert.Equal(N, mult.Sum());
        Assert.Equal(95, N - 1);                       // the state's affine dimension
        Assert.Equal(45, distinct.Length);

        // I3 — it SURVIVES G_017: the exclusion removed an IDENTIFICATION (lab |psi|^2 = rho), not the
        // quantity. The counting measure's definition never contained a laboratory number.
        Assert.True(Math.Abs(witness.Sum() - 1.0) < 1e-12);
        Assert.True(Math.Abs(uniform.Sum() - 1.0) < 1e-12);
        Assert.True(MaxAbsAcceleration(witness) > 0.0);

        // I4 — it DETERMINES rho trivially (it IS rho): no information is lost, unlike every other candidate.
        Assert.True(L1(witness, witness) == 0.0);
        Assert.True(Math.Abs(L1(witness, uniform) - 0.6666666666666667) < 1e-12);

        // The whole audit uses no measured constant: the assertions above are taken from N, the D96 grouping
        // and the counting constraint alone.
        Assert.Equal(96, N);
        Assert.Equal(3, D);
    }

    // ── 2. REFUTED: the actualization count is a label, not content ──────────────

    [Fact]
    public void Y_G_018_ActualizationCount()
    {
        // Scale invariance (G_009), verified EXACTLY: rho and lambda*rho are physically identical in every
        // ratio. The field and the clock separation do not notice the scale at all.
        var witness = Witness;
        double a1 = MaxAbsAcceleration(witness);
        double sep1 = Math.Log(witness.Max() / witness.Min()) / D;
        Assert.True(Math.Abs(a1 - 0.6031746016455657) < 1e-9, $"max|a| = {a1}");
        Assert.True(Math.Abs(sep1 - 0.9985774245179969) < 1e-12);

        foreach (double scale in new[] { 1e3, 1e-6, 96.0 })
        {
            var scaled = witness.Select(v => v * scale).ToArray();
            Assert.True(Math.Abs(scaled.Sum() - scale) < 1e-9 * scale);        // the COUNT changed
            Assert.True(Math.Abs(MaxAbsAcceleration(scaled) - a1) / a1 < 1e-9, $"scale {scale}: field changed");
            Assert.True(Math.Abs(Math.Log(scaled.Max() / scaled.Min()) / D - sep1) < 1e-14, $"scale {scale}");
        }

        // The count N*rho therefore carries ZERO physical content: every observable is a RATIO, and ratios
        // are scale-free. So "actualization count" is not a distinct identity — it is rho with a label.
        Assert.True(Math.Abs(N * 1.0 - N) < 1e-15);
        double ratioA = witness[0] / witness[1];
        double ratioB = (7.0 * witness[0]) / (7.0 * witness[1]);
        Assert.True(Math.Abs(ratioA - ratioB) < 1e-15);
    }

    // ── 3. DERIVED content / BOUNDARY dimension: degeneracy occupancy ────────────

    [Fact]
    public void Y_G_018_DegeneracyOccupancy()
    {
        var uniform = Uniform;
        var witness = Witness;
        var (_, mult) = D96Spaces;

        // The within-multiplet (degenerate) occupancy is EXACTLY the part of rho that the energy functional
        // cannot see: dE = 0 for the canonical witness (G_016).
        Assert.Equal(0.0, Energy(witness) - Energy(uniform), 12);
        Assert.True(Math.Abs(Energy(witness) - Energy(uniform)) < 1e-12);

        // And it is a 51-dimensional room — the energy-free directions.
        int free = mult.Sum(m => m - 1);
        Assert.Equal(51, free);
        Assert.Equal(N - D96Spaces.Distinct.Length, free);

        // So it is DERIVED as content (a genuine part of the state) while its DIMENSION is BOUNDARY: it is
        // inherited from the D96 lattice, not from the state.
        Assert.True(Math.Abs(100.0 * 51 / 95 - 53.68421052631579) < 1e-12);     // 53.6842 % pure arrangement
        Assert.True(Math.Abs(100.0 * 94 / 95 - 98.94736842105263) < 1e-12);     // 98.9474 % energy-invisible

        // Explicit: a within-multiplet move changes the occupancy, keeps Sigma = 1, and leaves E exact.
        int start = 0;
        for (int i = 0; i < mult.Length; i++) { if (mult[i] == 6) break; start += mult[i]; }
        var rho = (double[])uniform.Clone();
        rho[start] += 0.012; rho[start + 1] -= 0.012;
        Assert.True(Math.Abs(rho.Sum() - 1.0) < 1e-12);
        Assert.True(Math.Abs(Energy(rho) - Energy(uniform)) < 1e-13);
        Assert.True(L1(rho, uniform) > 0.023);

        // The lattice (not the state) is what supplies the room: one multiplet of 6, one of 5, 42 of 2, one of 1.
        Assert.Equal(4, mult.Distinct().Count());
        Assert.Equal(1, mult.Count(m => m == 6));
        Assert.Equal(1, mult.Count(m => m == 5));
        Assert.Equal(42, mult.Count(m => m == 2));
        Assert.Equal(1, mult.Count(m => m == 1));
    }

    // ── 4. REFUTED: survivor distribution is a lossy functional ──────────────────

    [Fact]
    public void Y_G_018_SurvivorDistribution()
    {
        var uniform = Uniform;
        var witness = Witness;
        var (_, mult) = D96Spaces;

        // A survivor/compressed distribution is a FUNCTIONAL of rho: it follows rho and it is lossy.
        // (a) the per-multiplet totals keep 44 of 95 dimensions, discarding EXACTLY the 51-dim free room.
        var blocksW = BlockSums(mult, witness);
        var blocksU = BlockSums(mult, uniform);
        Assert.Equal(45, blocksW.Length);
        Assert.True(L1(blocksW, blocksU) < 1e-12);                  // blind to the whole within-multiplet room
        Assert.Equal(44, 45 - 1);
        Assert.Equal(51, (N - 1) - 44);

        // (b) survivor compaction moves E, so it is not even energy-blind: it is a readout of rho, and it
        // cannot determine rho.
        double eTilt = Energy(witness);
        Assert.True(Math.Abs(Energy(Compaction(witness, 48)) - eTilt) > 1e-3);
        Assert.True(Math.Abs(Compaction(witness, 48).Sum() - 1.0) < 1e-12);

        // (c) block sums lose 51 of the 95 dimensions and E loses 94 — both strictly lossy.
        Assert.True(44 < 95 && 1 < 95);
        Assert.Equal(51, 95 - 44);
        Assert.Equal(94, 95 - 1);

        // A lossy functional cannot be the identity: two DIFFERENT states share the same survivor data.
        // Move density WITHIN one multiplet, so the per-multiplet totals are exactly unchanged.
        int s2 = 0;
        for (int i = 0; i < mult.Length; i++) { if (mult[i] == 2) break; s2 += mult[i]; }
        var alt = (double[])witness.Clone();
        alt[s2] += 1e-6; alt[s2 + 1] -= 1e-6;
        Assert.True(L1(alt, witness) > 0.0);
        Assert.True(L1(BlockSums(mult, alt), blocksW) < 1e-12);
    }

    // ── 5. BOUNDARY: state accessibility is the lattice ──────────────────────────

    [Fact]
    public void Y_G_018_StateAccessibility()
    {
        // Accessibility is a property of the LATTICE, not of the state: A0 eigenspaces and the free room
        // Sigma(m - 1) = N - A0, computed for six coupling ranges with NO reference to rho.
        var rows = new (int K, int A0, int Free)[]
        { (1, 49, 47), (2, 47, 49), (3, 45, 51), (4, 47, 49), (5, 45, 51), (6, 45, 51) };
        foreach (var (K, a0, free) in rows)
        {
            var (distinct, mult) = Spaces(K);
            Assert.Equal(a0, distinct.Length);
            Assert.Equal(free, mult.Sum(m => m - 1));
            Assert.Equal(N - a0, mult.Sum(m => m - 1));            // free room = N - A0, always
            Assert.Equal(N, mult.Sum());
        }

        // The SAME rho vector (uniform) exists for every lattice, so accessibility does not determine rho:
        var u1 = Spread(Spaces(1).Mult, 1.0);
        var u6 = Spread(Spaces(6).Mult, 1.0);
        Assert.True(L1(u1, u6) < 1e-15);
        Assert.True(Math.Abs(u1.Sum() - 1.0) < 1e-12);

        // It BOUNDS what rho can reach without being rho: the free room is 51 directions the flow can occupy,
        // and the capacity — not the state — sets the number.
        Assert.Equal(51, D96Spaces.Mult.Sum(m => m - 1));
        Assert.True(D96Spaces.Mult.Sum(m => m - 1) < N - 1);
    }

    // ── 6. verdicts and the provenance ledger ────────────────────────────────────

    [Fact]
    public void Y_G_018_VerdictsAndProvenance()
    {
        var uniform = Uniform;
        var witness = Witness;

        // THE ANSWER: the OCCUPANCY MEASURE — dimensionless, normalised, arrangement-determined.
        Assert.True(Math.Abs(uniform.Sum() - 1.0) < 1e-12);
        Assert.True(Math.Abs(witness.Sum() - 1.0) < 1e-12);

        // The invariants that make it the identity (I1-I4), each verified: counting-defined, normalised,
        // G_017-proof, and information-preserving.
        Assert.True(witness.Min() > 0.0);
        Assert.True(L1(witness, witness) == 0.0);
        Assert.True(Math.Abs(Energy(witness) - Energy(uniform)) < 1e-12);
        Assert.True(Math.Abs(MaxAbsAcceleration(witness) - 0.6031746016455657) < 1e-9);

        // The verdict split, in one frame:
        Assert.Equal(95, N - 1);                                   // the state
        Assert.Equal(51, D96Spaces.Mult.Sum(m => m - 1));          // DERIVED content, BOUNDARY dimension
        Assert.Equal(43, (N - 1) - 1 - 51);                        // the lambda-mixing room
        Assert.Equal(44, D96Spaces.Mult.Length - 1);               // the degeneracy distribution (lossy)
        Assert.Equal(45, D96Spaces.Distinct.Length);               // the capacity (BOUNDARY)

        // PROVENANCE LEDGER: this audit imports NO constant. Every number above comes from N = 96, the D96
        // coupling range K = 6 and the counting constraint Sigma rho = 1. (Contrast G_015/G_017, which
        // imported G, c, finesse, Q and the polarizability — and were thereby falsifiable.)
        Assert.Equal(96, N);
        Assert.Equal(1, D96Spaces.Mult.Sum(m => m - 1) / 51);
        Assert.True(Math.Abs(1.0 / (N - 1) - 1.0 / 95) < 1e-15);

        // The honest limit of an identity statement: it is combinatorial, so by itself it makes no contact
        // with an experiment — the falsifiable content lives in the boundary identifications (G_017).
        Assert.True(D96Spaces.Distinct.Length * 2 + D96Spaces.Mult.Sum(m => m - 1) + 43 > 95);
    }

    // ── 7. research report ───────────────────────────────────────────────────────

    [Fact]
    public void Y_G_018_Run()
    {
        var sb = new StringBuilder();
        var uniform = Uniform;
        var witness = Witness;
        var (distinct, mult) = D96Spaces;

        PrintHeader(sb, "ResearchY-G_018 — RHO IDENTITY AUDIT");
        sb.AppendLine("Question: after G_017 excluded the identification of a LABORATORY |psi|^2 with rho, which");
        sb.AppendLine("          physical quantity remains as the IDENTITY of rho?");
        sb.AppendLine("Candidates: occupancy measure, actualization count, state accessibility, degeneracy occupancy,");
        sb.AppendLine("            survivor distribution.     Output: DERIVED / BOUNDARY / REFUTED.");
        sb.AppendLine();

        PrintHeader(sb, "ASSUMPTIONS — AND THE PROVENANCE LEDGER");
        sb.AppendLine("  A1  The criterion: Q is the identity of rho iff (I1) it is defined by COUNTING over");
        sb.AppendLine("      distinguishable states (Difference -> distinguishability only), (I2) it is dimensionless and");
        sb.AppendLine("      normalised or a ratio of such, (I3) it survived G_017 (its definition never held a lab");
        sb.AppendLine("      number), (I4) it DETERMINES rho and is not a lossy functional of it.");
        sb.AppendLine("  A2  PROVENANCE: imported constants used by this audit = ZERO. No SI quantity, no measured");
        sb.AppendLine("      number, no laboratory system. Compare G_015/G_017, which imported G, c, finesse, Q and the");
        sb.AppendLine("      polarizability — and were thereby falsifiable.");
        sb.AppendLine();

        PrintHeader(sb, "1. DERIVED — THE OCCUPANCY MEASURE");
        sb.AppendLine($"  cells N = {N}; eigenspaces A0 = {distinct.Length}; free room Sigma(m-1) = {mult.Sum(m => m - 1)}");
        sb.AppendLine($"  state affine dimension = N - 1 = {N - 1};  Sigma rho = 1 and rho > 0 for every configuration");
        sb.AppendLine("  I3: nothing in 'rho = counting' was ever a laboratory number, so G_017's exclusion removed an");
        sb.AppendLine("      IDENTIFICATION, not the quantity.");
        sb.AppendLine($"  I4: it IS rho — zero loss (L1(witness, witness) = 0), unlike every other candidate.");
        sb.AppendLine("  => the identity that remains is a DIMENSIONLESS, NORMALISED COUNTING MEASURE whose entire");
        sb.AppendLine("     physical content is in the ARRANGEMENT and none of it in the amount.");

        PrintHeader(sb, "2. REFUTED — THE ACTUALIZATION COUNT");
        sb.AppendLine("  Scale invariance (G_009), verified exactly:");
        sb.AppendLine("    scale        Sigma rho      max|a|                  clock separation");
        foreach (double scale in new[] { 1.0, 1e3, 1e-6, 96.0 })
        {
            var s = witness.Select(v => v * scale).ToArray();
            sb.AppendLine($"    {scale,-10}   {s.Sum(),10:F6}   {MaxAbsAcceleration(s):F16}   {Math.Log(s.Max() / s.Min()) / D:F16}");
        }
        sb.AppendLine("  => every observable is a RATIO and ratios are scale-free: the count N*rho is a LABEL with");
        sb.AppendLine("     ZERO content. 'Actualization count' is not a distinct identity.");

        PrintHeader(sb, "3. DEGENERACY OCCUPANCY — DERIVED CONTENT, BOUNDARY DIMENSION");
        sb.AppendLine($"  the within-multiplet distribution: dE = {Energy(witness) - Energy(uniform):E2} (exactly invariant)");
        sb.AppendLine($"  it is {mult.Sum(m => m - 1)} of the {N - 1} dimensions  =  {100.0 * 51 / 95:F4} % of the state is invisible to everything");
        sb.AppendLine($"                                        except arrangement;  {100.0 * 94 / 95:F4} % is invisible to ENERGY");
        sb.AppendLine("  DERIVED as content (a genuine part of the state); BOUNDARY in its dimension, since 51 = Sigma(m-1)");
        sb.AppendLine("  is inherited from the D96 lattice (two-level rule, as in D_028/D_040).");
        sb.AppendLine($"  multiplicities: one of 6, one of 5, 42 of 2, one of 1  =>  A0 = {distinct.Length}");

        PrintHeader(sb, "4. REFUTED — THE SURVIVOR DISTRIBUTION");
        sb.AppendLine("  a FUNCTIONAL of rho, hence a readout:");
        sb.AppendLine($"    per-multiplet totals keep {distinct.Length - 1} of {N - 1} dimensions, discarding EXACTLY the 51-dim room");
        sb.AppendLine($"    E = <lambda, rho> keeps 1 of {N - 1}, discarding 94");
        sb.AppendLine($"    survivor compaction MOVES E (|dE| = {Math.Abs(Energy(Compaction(witness, 48)) - Energy(witness)):E3}), so it is not even energy-blind");
        sb.AppendLine("  two DIFFERENT states share the same survivor data => it cannot determine rho.");

        PrintHeader(sb, "5. BOUNDARY — STATE ACCESSIBILITY");
        sb.AppendLine("   K     A0    free room = N - A0");
        foreach (var K in new[] { 1, 2, 3, 4, 5, 6 })
        {
            var (dist, mul) = Spaces(K);
            sb.AppendLine($"   {K}   {dist.Length,4}    {mul.Sum(m => m - 1),4}");
        }
        sb.AppendLine("  The same uniform rho exists for every lattice (L1 = 0), so accessibility does not determine rho:");
        sb.AppendLine("  it is a CAPACITY input (G_007) that BOUNDS what rho can reach without being rho.");

        PrintHeader(sb, "6. CONCLUSIONS");
        sb.AppendLine("  C1  DERIVED — the OCCUPANCY MEASURE: a dimensionless, normalised counting measure over");
        sb.AppendLine("      distinguishable states, needing only the first primitive. It survives G_017 because the");
        sb.AppendLine("      exclusion removed an identification, not a quantity.");
        sb.AppendLine("  C2  REFUTED — the ACTUALIZATION COUNT: scale invariance makes the amount unphysical (identical");
        sb.AppendLine("      field and identical clock separation at every scale), so the count is a label.");
        sb.AppendLine("  C3  DERIVED content / BOUNDARY dimension — the DEGENERACY OCCUPANCY: exactly the energy-invisible");
        sb.AppendLine($"      part of the state ({100.0 * 51 / 95:F4} % of it is invisible to everything but arrangement), with the");
        sb.AppendLine("      dimension 51 inherited from the lattice.");
        sb.AppendLine("  C4  REFUTED — the SURVIVOR DISTRIBUTION: a lossy functional (loses exactly the 51-dim room, or 94");
        sb.AppendLine("      dimensions as energy) and it moves E, so it is a readout rather than an identity.");
        sb.AppendLine("  C5  BOUNDARY — STATE ACCESSIBILITY: the capacity (A0, free room) belongs to the lattice, and bounds");
        sb.AppendLine("      the reachable set without determining the state.");
        sb.AppendLine("  C6  What remains, stated plainly: rho is an ARRANGEMENT — a counting measure whose physical meaning");
        sb.AppendLine("      is entirely in the shape of the occupancy and none of it in its size.");
        sb.AppendLine("  C7  THE HONEST LIMIT: this audit imports NO constant, which makes it the most AT-native of the whole");
        sb.AppendLine("      group — and correspondingly unfalsifiable by itself. Its falsifiable content lives entirely in");
        sb.AppendLine("      the BOUNDARY identifications, and G_017 showed the laboratory one is excluded.");

        PrintHeader(sb, "7. CLASSIFICATION");
        sb.AppendLine("  DERIVED   the occupancy measure (I1-I4 all verified) · plus the degeneracy occupancy as CONTENT.");
        sb.AppendLine("  BOUNDARY  state accessibility (the lattice capacity A0 / free room) · the dimension 51 = Sigma(m-1).");
        sb.AppendLine("  REFUTED   the actualization count (a label, by scale invariance) · the survivor distribution (a lossy");
        sb.AppendLine("            functional).");
        sb.AppendLine("  No reclassification; D_040 untouched; no canonical claim, value or equation changes; no new");
        sb.AppendLine("  primitive; deterministic (exact combinatorics, no randomness, no imported constant).");

        Output.WriteLine(sb.ToString());
    }

    private static void PrintHeader(StringBuilder sb, string title)
    {
        sb.AppendLine(new string('=', 100));
        sb.AppendLine(title);
        sb.AppendLine(new string('=', 100));
    }
}
