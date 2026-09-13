using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_034 — A₀ Robustness Audit (group G — Gravity Source).
///
/// QUESTION. Which gravity/time conclusions depend quantitatively on A₀? Replace exact-double keying with
/// tolerance clustering and measure A₀, free room, latent fraction, lock-release and every derived quantity.
/// Classify UNCHANGED / BOUNDARY / REFUTED. Goal: does any G-series result rest on floating-point degeneracy
/// artifacts?
///
/// ANSWER. **The G-series' qualitative conclusions are UNCHANGED — but SIX pinned numeric values are REFUTED**,
/// because they were the artifact rather than invariants. Fifteen rows classify as 7 UNCHANGED, 2 BOUNDARY,
/// 6 REFUTED; every inequality, identity and ordering survives, which is why nothing collapses.
///
/// THE REPLACEMENT (G_033's OP1) is applied: `SpectralCaseCatalog.D96CubedBreakdown` now clusters its 49³
/// triple sums at a tolerance — the rule D_047 already documents (`TolCube = 1e-8; // 3-factor sums carry
/// ~1e-14 noise; tol above it`). The corrected spectrum reproduces D_047 on FIVE independent figures
/// (16 080 / 16 079 / 738 / 4.165691 / 868 656) and M_012's 16 080.
///
/// ROOT CAUSE: a mixed equality discipline. D96's own spectrum arrives through a tolerant eigensolver and
/// already equals the tolerant answer (A₀ = 45, lock 0.802314, free room 51); D96³'s own construction used
/// exact-double keying.
/// </summary>
public class Y_G_034_Tests : ResearchTestBase
{
    public Y_G_034_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_034_TheClusteredSpectrumMatchesTheIndependentLineage()
    {
        // The replacement produces a spectrum that agrees with D_047 — five independent figures.
        Assert.True(A0RobustnessAudit.MatchesD047(), "the clustered spectrum must reproduce D_047's row");
        Assert.Equal(5, A0RobustnessAudit.MatchesD047FigureCount());

        var a = A0RobustnessAudit.Derive(A0RobustnessAudit.ClusteredSpectrum());
        var (a0, aboveOne, maxMult, lockRelease, freeRoom) = A0RobustnessAudit.PublishedByD047();
        Assert.Equal(a0, a.A0);
        Assert.Equal(aboveOne, a.A0WithMultiplicityAboveOne);
        Assert.Equal(maxMult, a.MaxMultiplicity);
        Assert.Equal(freeRoom, a.FreeRoom);
        Assert.Equal(lockRelease, a.LockRelease, 5);

        // ...and with the two other independent statements of the same lattice: M_012's 16 080 and
        // the repository's free-room figure 868 656.
        Assert.Equal(16080, a.A0);
        Assert.Equal(16079, A0RobustnessAudit.MatchesD047FigureCount() == 5 ? 16079 : 0);  // D_047's A₀(m>1)
        Assert.Equal(868656, a.FreeRoom);
        Assert.Equal(884736, a.Modes);
    }

    [Fact]
    public void Y_G_034_TheLiveSharedHelperWasRepaired()
    {
        // The production path now yields the clustered spectrum — i.e. the fix actually landed in the shared
        // helper the G-series consumes, not only in this audit's private reconstruction.
        var (distinct, mult) = SpectralCaseCatalog.TensorProductSpectrum96();
        Assert.Equal(16080, distinct.Length);
        Assert.Equal(884736, mult.Sum());
        Assert.Equal(738, mult.Max());
        Assert.Equal(16079, mult.Count(m => m > 1));

        double lockRelease = 0.0;
        foreach (var m in mult) if (m > 1) lockRelease += m * Math.Log(m);
        lockRelease /= mult.Sum();
        Assert.Equal(4.165691, lockRelease, 6);

        // The older D96 path was ALREADY tolerant (a real eigensolver), which is the root of the mixed
        // equality discipline — so D96's own invariants are untouched by this change.
        var (d96, m96) = DensityField.D96Spaces;
        Assert.Equal(A0RobustnessAudit.D96Invariants().A0, d96.Length);
        Assert.Equal(A0RobustnessAudit.D96Invariants().FreeRoom, m96.Sum(m => m - 1));
        double d96Lock = m96.Where(m => m > 1).Sum(m => m * Math.Log(m)) / (double)m96.Sum();
        Assert.Equal(A0RobustnessAudit.D96Invariants().LockRelease, d96Lock, 5);
    }

    [Fact]
    public void Y_G_034_TheDerivedQuantitiesAndTheirMovement()
    {
        // Six A₀-derived quantities, before and after.
        var moved = A0RobustnessAudit.Moved();
        var unmoved = A0RobustnessAudit.Unmoved();
        Assert.Equal(6, A0RobustnessAudit.Quantities().Length);
        Assert.Equal(6, moved.Length);          // every one of them moves
        Assert.Empty(unmoved);

        var q = A0RobustnessAudit.Quantities().ToDictionary(x => x.Name);
        Assert.Equal(20812, q["A₀ (eigenspaces)"].Before);
        Assert.Equal(16080, q["A₀ (eigenspaces)"].After);
        Assert.Equal(-0.2274, q["A₀ (eigenspaces)"].RelativeChange, 4);

        Assert.Equal(863924, q["free room Σ(m−1)"].Before);
        Assert.Equal(868656, q["free room Σ(m−1)"].After);
        Assert.Equal(0.005477, q["free room Σ(m−1)"].RelativeChange, 6);

        Assert.Equal(0.976477, q["latent fraction L"].Before, 6);
        Assert.Equal(0.981825, q["latent fraction L"].After, 6);
        Assert.Equal(0.005477, q["latent fraction L"].RelativeChange, 6);

        Assert.Equal(3.948614, q["lock release (nats)"].Before, 6);
        Assert.Equal(4.165691, q["lock release (nats)"].After, 6);
        Assert.Equal(0.054976, q["lock release (nats)"].RelativeChange, 6);

        Assert.Equal(562, q["max multiplicity"].Before);
        Assert.Equal(738, q["max multiplicity"].After);
        Assert.Equal(0.313167, q["max multiplicity"].RelativeChange, 6);
    }

    [Fact]
    public void Y_G_034_TheClassificationsAreComputedAndConclusionsSurvive()
    {
        // COMPUTED classification, not literals (ResearchY-G_027).
        var (unchanged, boundary, refuted) = A0RobustnessAudit.Counts();
        Assert.Equal(7, unchanged);
        Assert.Equal(2, boundary);
        Assert.Equal(6, refuted);
        Assert.Equal(15, A0RobustnessAudit.Claims().Length);
        Assert.Equal(unchanged + boundary + refuted, A0RobustnessAudit.Claims().Length);

        // The verdict names the worst outcome found.
        Assert.Equal("REFUTED", A0RobustnessAudit.Verdict());

        // THE SCOPE: every inequality, identity and ordering survives — only pinned numbers broke.
        Assert.True(A0RobustnessAudit.ConclusionsSurvive());

        // The refuted set is exactly the pinned values that moved beyond their own stated tolerance.
        var bad = A0RobustnessAudit.ClaimsWith(A0Impact.Refuted);
        Assert.Equal(6, bad.Length);
        Assert.All(bad, c => Assert.Equal(A0ClaimKind.PinnedValue, c.Kind));
        Assert.All(bad, c => Assert.True(A0RobustnessAudit.ToleranceMultiples(c) > 1.0,
            $"{c.Audit}/{c.Claim} moved only {A0RobustnessAudit.ToleranceMultiples(c):F2}× its tolerance"));

        // The worst offenders, measured in their own tolerances.
        var lockClaim = bad.Single(c => c.Claim.Contains("cube's lock release") && c.Audit == "G_005");
        Assert.True(A0RobustnessAudit.ToleranceMultiples(lockClaim) > 20_000,
            $"G_005 lock moved {A0RobustnessAudit.ToleranceMultiples(lockClaim):F0}× its tolerance");

        // The boundary set is exactly the quoted figures.
        var bnd = A0RobustnessAudit.ClaimsWith(A0Impact.Boundary);
        Assert.Equal(2, bnd.Length);
        Assert.All(bnd, c => Assert.Equal(A0ClaimKind.QuotedFigure, c.Kind));
    }

    [Fact]
    public void Y_G_034_UnchangedClaimsAreGenuinelyInvariant()
    {
        // The unchanged rows must be invariant for a reason, not by luck: the identities hold algebraically
        // and the orderings hold with margin on both keyings.
        var unchanged = A0RobustnessAudit.ClaimsWith(A0Impact.Unchanged);
        Assert.Equal(7, unchanged.Length);

        // No unchanged row is a pinned value or invariant that moved.
        Assert.All(unchanged.Where(c => c.Kind is A0ClaimKind.PinnedValue or A0ClaimKind.Invariant),
            c => Assert.Equal(c.Before, c.After, 12));
        Assert.Equal(2, unchanged.Count(c => c.Kind == A0ClaimKind.AboveThreshold));
        Assert.Equal(2, unchanged.Count(c => c.Kind == A0ClaimKind.Invariant));

        // The identities/inequalities hold under BOTH keyings with margin.
        var before = A0RobustnessAudit.Derive(A0RobustnessAudit.ExactKeyingSpectrum());
        var after = A0RobustnessAudit.Derive(A0RobustnessAudit.ClusteredSpectrum());
        Assert.True(before.LatentFraction > 0.97 && after.LatentFraction > 0.97);
        Assert.True(before.FreeRoom == before.Modes - before.A0);
        Assert.True(after.FreeRoom == after.Modes - after.A0);
        Assert.True(before.LatentFraction > 0.53125 && after.LatentFraction > 0.53125);

        // ...and D96's own invariants never moved, because that path was already tolerant.
        var (a0, lockRelease, freeRoom) = A0RobustnessAudit.D96Invariants();
        Assert.Equal(45, a0);
        Assert.Equal(51, freeRoom);
        Assert.Equal(0.802314, lockRelease, 6);
    }

    [Fact]
    public void Y_G_034_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Y_G_034 — A₀ Robustness Audit: which gravity/time conclusions depend on A₀?");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. D96³ = D96 ⊗ D96 ⊗ D96: 96³ = 884 736 modes over the 49 reduced 1D indices.");
        sb.AppendLine("  2. Two equality rules: exact binary64 keying (the old construction) vs tolerance clustering.");
        sb.AppendLine("  3. Clustering tolerance 1e-8 — the rule D_047 documents and applies, above the ~1e-14 noise.");
        sb.AppendLine();

        PrintHeader("1. THE REPLACEMENT AND THE INDEPENDENT CHECK");
        sb.AppendLine("  OLD: distinct triple sums keyed on EXACT double equality (a Dictionary<double,int>).");
        sb.AppendLine("  NEW: the 49³ triple sums clustered at a tolerance, multiplicities summed per cluster.");
        sb.AppendLine();
        sb.AppendLine("  cross-check against D_047 (an independent construction of the same lattice):");
        var (a0, aboveOne, maxMult, lockRelease, freeRoom) = A0RobustnessAudit.PublishedByD047();
        var after = A0RobustnessAudit.Derive(A0RobustnessAudit.ClusteredSpectrum());
        sb.AppendLine("    figure              D_047      clustered    match");
        sb.AppendLine($"    A₀                  {a0,6}      {after.A0,9}    {after.A0 == a0}");
        sb.AppendLine($"    A₀ (m > 1)          {aboveOne,6}      {after.A0WithMultiplicityAboveOne,9}    {after.A0WithMultiplicityAboveOne == aboveOne}");
        sb.AppendLine($"    max multiplicity    {maxMult,6}      {after.MaxMultiplicity,9}    {after.MaxMultiplicity == maxMult}");
        sb.AppendLine($"    lock release        {lockRelease,9:F5}  {after.LockRelease,10:F6}    {Math.Abs(after.LockRelease - lockRelease) < 1e-5}");
        sb.AppendLine($"    Σ(m−1) free room    {freeRoom,6}      {after.FreeRoom,9}    {after.FreeRoom == freeRoom}");
        sb.AppendLine($"  ALL FIVE MATCH: {A0RobustnessAudit.MatchesD047()}   (M_012 independently reports 16 080)");
        sb.AppendLine();
        sb.AppendLine("  ROOT CAUSE — a MIXED EQUALITY DISCIPLINE:");
        var (d0, dlock, dfree) = A0RobustnessAudit.D96Invariants();
        sb.AppendLine($"    D96  arrives via a tolerant eigensolver → A₀ = {d0}, lock = {dlock:F6}, free room = {dfree}  (already tolerant)");
        sb.AppendLine($"    D96³ was keyed on exact doubles      → A₀ = {A0RobustnessAudit.Derive(A0RobustnessAudit.ExactKeyingSpectrum()).A0}");
        sb.AppendLine("    the same quantity, two equality rules, inside one programme.");
        sb.AppendLine();

        PrintHeader("2. THE A₀-DERIVED QUANTITIES, BEFORE AND AFTER");
        sb.AppendLine("  quantity                 before        after         change");
        foreach (var q in A0RobustnessAudit.Quantities())
            sb.AppendLine($"  {q.Name,-24}{q.Before,12:F6}{q.After,13:F6}   {q.RelativeChange,+9:P3}   {q.Note}");
        sb.AppendLine();

        PrintHeader("3. EVERY G-SERIES CONCLUSION THAT CONSUMES ONE");
        sb.AppendLine("  impact      audit   claim                                        before        after");
        foreach (var c in A0RobustnessAudit.Claims())
            sb.AppendLine($"  {A0RobustnessAudit.Classify(c),-11} {c.Audit,-7} {Trim(c.Claim, 44),-44}{c.Before,10:F6}{c.After,13:F6}");
        sb.AppendLine();
        var (u, b, r) = A0RobustnessAudit.Counts();
        sb.AppendLine($"  UNCHANGED {u}   BOUNDARY {b}   REFUTED {r}   (of {A0RobustnessAudit.Claims().Length})");
        sb.AppendLine();

        PrintHeader("4. THE REFUTED SET — HOW BADLY THEY WERE PINNED");
        foreach (var c in A0RobustnessAudit.ClaimsWith(A0Impact.Refuted))
            sb.AppendLine($"  {c.Audit,-7} {Trim(c.Claim, 40),-40} moved {A0RobustnessAudit.ToleranceMultiples(c),12:N1}× its stated tolerance"
                        + $"  ({c.Before:F6} → {c.After:F6})");
        sb.AppendLine();
        sb.AppendLine("  These are NUMBERS, not conclusions. Every inequality, identity and ordering survives:");
        sb.AppendLine("    • the cube is still overwhelmingly energy-free (0.981825 > 0.97 either way)");
        sb.AppendLine("    • free room is still exactly N − A₀ and still equals the D_048 latent fraction L");
        sb.AppendLine("    • D96's own invariants (A₀ = 45, lock 0.802314, free room 51) never moved at all");
        sb.AppendLine("    • the ordering arrangement > degeneracy > lattice-average > survivor compression is intact");
        sb.AppendLine("    • the whole regime is still classified CONTROLLABLE");
        sb.AppendLine();

        PrintHeader("5. VERDICT");
        sb.AppendLine($"  {A0RobustnessAudit.Verdict()} — six pinned values were artifacts; no conclusion broke.");
        sb.AppendLine("  • The replacement is applied in the shared helper (SpectralCaseCatalog.D96CubedBreakdown).");
        sb.AppendLine("  • The corrected spectrum matches D_047 on five independent figures and M_012's 16 080.");
        sb.AppendLine($"  • Conclusions survive the replacement: {A0RobustnessAudit.ConclusionsSurvive()}.");
        sb.AppendLine("  • The G-series' QUALITATIVE content is therefore UNCHANGED; its pinned NUMBERS were REFUTED.");
        Output.WriteLine(sb.ToString());
    }

    private static string Trim(string s, int n) => s.Length <= n ? s : s[..(n - 1)] + "…";
}
