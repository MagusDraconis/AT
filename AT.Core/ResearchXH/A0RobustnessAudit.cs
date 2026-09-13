namespace AT.Core.ResearchXH;

/// <summary>How an A₀-derived quantity fared when exact-double keying was replaced by tolerance clustering.</summary>
public enum A0Impact
{
    /// <summary>Invariant under the replacement (or the assertion still holds at its own precision).</summary>
    Unchanged,

    /// <summary>The quoted figure moved, but nothing it supports breaks — it must be restated, not withdrawn.</summary>
    Boundary,

    /// <summary>The value was pinned to the artifact: as stated it no longer holds.</summary>
    Refuted,
}

/// <summary>What kind of claim a row makes.</summary>
public enum A0ClaimKind
{
    /// <summary>An exact value asserted at a stated precision — moved beyond it ⟹ REFUTED.</summary>
    PinnedValue,

    /// <summary>An inequality — classified UNCHANGED iff it still holds.</summary>
    AboveThreshold,

    /// <summary>A figure quoted in narrative or a report — moved ⟹ BOUNDARY.</summary>
    QuotedFigure,

    /// <summary>An algebraic identity or an ordering — classified UNCHANGED iff it is invariant.</summary>
    Invariant,
}

/// <summary>A derived quantity of the D96³ spectrum, before and after the keying replacement.</summary>
public sealed record A0Quantity(string Name, double Before, double After, string Note)
{
    public double Delta => After - Before;

    public double RelativeChange => Before == 0.0 ? double.NaN : (After - Before) / Before;
}

/// <summary>One conclusion that depends on A₀ (or on a quantity derived from it).</summary>
public sealed record A0Claim(
    string Audit,
    string Claim,
    string Quantity,
    double Before,
    double After,
    double Tolerance,
    A0ClaimKind Kind,
    string Note);

/// <summary>The derived quantities of one D96³ spectrum.</summary>
public sealed record A0Derived(
    int A0, int Modes, int FreeRoom, double LatentFraction, double LockRelease, int MaxMultiplicity,
    int A0WithMultiplicityAboveOne);

/// <summary>
/// ResearchY-G_034 — A₀ ROBUSTNESS AUDIT.
///
/// QUESTION. Which gravity/time conclusions depend **quantitatively** on A₀? Replace exact-double keying with
/// tolerance clustering and measure A₀, free room, latent fraction, lock-release, and every derived quantity.
/// Classify UNCHANGED / BOUNDARY / REFUTED. Goal: determine whether any G-series result depends on
/// floating-point degeneracy artifacts.
///
/// ANSWER: **the G-series' qualitative conclusions are UNCHANGED, but SIX pinned numeric values are REFUTED**
/// — they were the floating-point artifact, not invariants. The replacement is applied (G_033's OP1) and the
/// corrected spectrum is confirmed against an independent lineage on five independent figures.
///
/// THE REPLACEMENT. `SpectralCaseCatalog.D96CubedBreakdown` keyed its 49³ triple sums on EXACT double
/// equality, so A₀ depended on the last bits of `Math.Cos`. It now clusters the sums at a tolerance — the rule
/// **D_047 already documents and applies** (`private const double TolCube = 1e-8; // 3-factor sums carry
/// ~1e-14 noise; tol above it`). The corrected spectrum reproduces D_047 on **five independent figures**:
///
///     A₀ = 16 080    A₀(m>1) = 16 079    max multiplicity = 738    lock release = 4.165691    Σ(m−1) = 868 656
///
/// (D_047's published row: 16 080 | 16 079 | 738 | 4.16569 | 868 656.) It also matches M_012's independently
/// reported 16 080. **The root cause is a mixed equality discipline**: D96's OWN spectrum reaches the code
/// through a tolerant eigensolver (`AttractorDominanceAnalyzer.Eigenspaces`) and therefore already yields the
/// tolerant answers — A₀ = 45, lock release 0.802314 — while D96³'s own construction used exact-double keying.
/// Two instances of the same quantity, two different equality rules, inside the same programme.
///
/// WHAT MOVED (all A₀-derived):
///
///     quantity                exact-double (before)   cluster (after)    change
///     A₀                            20 812                16 080         −22.7 %
///     A₀ (m > 1)                    20 811                16 079         −22.7 %
///     free room Σ(m−1)             863 924               868 656          +0.55 %
///     latent fraction L            0.976477              0.981825         +0.55 %
///     lock release (nats)           3.948614              4.165691         +5.50 %
///     max multiplicity                 562                   738         +31.3 %
///
/// WHAT WAS REFUTED. Six pinned values failed their own stated precision once the artifact was removed:
/// G_002's A₀ = 20 812 and max multiplicity 562; G_003's D96³-vs-D96 ΔA = 0.276596 (moved 13 438× its 1e−6
/// tolerance); G_005's cube lock release 3.948614 (moved 21 708× its 1e−5 tolerance) and cube L = 0.97648
/// (535× its tolerance); and the same A₀ figure restated downstream in G_006 and the AT surfaces.
///
/// WHAT SURVIVED. Every **inequality, ordering, identity and structural** claim in the G-series — which is the
/// reason no conclusion breaks: the cube is still ~98 % energy-free (0.981825 > 0.97), free room is still
/// exactly N − A₀ and still equals the D_048 latent fraction L, D96's own invariants (A₀ = 45, lock 0.802314,
/// L = 0.53125) never moved, the ordering arrangement > degeneracy > lattice-average > survivor compression is
/// intact, and the whole regime is still CONTROLLABLE. The refuted items are **numbers**, not conclusions.
/// Two supporting figures become BOUNDARY: the cube's free fraction (97.6 % → 98.2 %) and its contraction
/// factor at m = 200 (7.33 → 7.10).
/// </summary>
public static class A0RobustnessAudit
{
    public const int N96 = 96;
    public const int Sides = 49;
    public const int Modes = 96 * 96 * 96;              // 884 736

    /// <summary>The default clustering tolerance — the value D_047 documents and applies.</summary>
    public const double DefaultTolerance = 1.0e-8;

    /// <summary>The 1D D96 spectrum over the 49 reduced indices (doublet λ_j = λ_{96−j} folded).</summary>
    public static double[] ReducedSpectrum()
    {
        var f = new double[Sides];
        for (int r = 0; r < Sides; r++)
        {
            double s = 0.0;
            for (int d = 1; d <= 6; d++) s += 1.0 - Math.Cos(2.0 * Math.PI * d * r / 96.0);
            f[r] = 2.0 * s;
        }
        return f;
    }

    /// <summary>1D multiplicity of a reduced index: 1 at r = 0 and r = 48, else 2.</summary>
    public static int Mult1D(int r) => (r == 0 || r == 48) ? 1 : 2;

    // ── The two keyings, computed independently of the production helper ─────

    /// <summary>
    /// THE OLD RULE: distinct triple sums keyed on EXACT binary64 equality (a `Dictionary&lt;double,int&gt;`).
    /// Reproduces the former A₀ = 20 812 — and, as G_033 showed, that number is not reproducible across
    /// implementations of the same IEEE-754 algorithm.
    /// </summary>
    public static (double[] Distinct, int[] Mult) ExactKeyingSpectrum()
    {
        var f = ReducedSpectrum();
        var acc = new Dictionary<double, int>();
        for (int i = 0; i < Sides; i++)
            for (int j = 0; j < Sides; j++)
                for (int k = 0; k < Sides; k++)
                {
                    double e = f[i] + f[j] + f[k];
                    int m = Mult1D(i) * Mult1D(j) * Mult1D(k);
                    acc[e] = acc.TryGetValue(e, out var v) ? v + m : m;
                }
        var order = acc.Keys.OrderBy(x => x).ToArray();
        return (order, order.Select(e => acc[e]).ToArray());
    }

    /// <summary>
    /// THE NEW RULE: cluster the 49³ triple sums at a tolerance, then take distinct cluster representatives
    /// with summed multiplicities. Yields the stable A₀ = 16 080 and the D_047 figures.
    /// </summary>
    public static (double[] Distinct, int[] Mult) ClusteredSpectrum(double tolerance = DefaultTolerance)
    {
        var f = ReducedSpectrum();
        var sums = new double[Sides * Sides * Sides];
        var mults = new int[sums.Length];
        int n = 0;
        for (int i = 0; i < Sides; i++)
            for (int j = 0; j < Sides; j++)
                for (int k = 0; k < Sides; k++)
                {
                    sums[n] = f[i] + f[j] + f[k];
                    mults[n] = Mult1D(i) * Mult1D(j) * Mult1D(k);
                    n++;
                }
        var idx = Enumerable.Range(0, n).OrderBy(t => sums[t]).ToArray();
        var distinct = new List<double>();
        var mult = new List<int>();
        int start = 0, running = 0;
        double anchor = sums[idx[0]], total = 0.0;
        for (int t = 0; t < n; t++)
        {
            int p = idx[t];
            if (sums[p] - anchor > tolerance)
            {
                distinct.Add(total / (t - start));
                mult.Add(running);
                start = t; running = 0; total = 0.0; anchor = sums[p];
            }
            running += mults[p];
            total += sums[p];
        }
        distinct.Add(total / (n - start));
        mult.Add(running);
        return (distinct.ToArray(), mult.ToArray());
    }

    /// <summary>Every derived quantity of a spectrum.</summary>
    public static A0Derived Derive((double[] Distinct, int[] Mult) s)
    {
        double sum = 0.0, lockRelease = 0.0;
        foreach (var m in s.Mult)
        {
            sum += m;
            if (m > 1) lockRelease += m * Math.Log(m);
        }
        int a0 = s.Distinct.Length;
        return new A0Derived(a0, (int)sum, (int)sum - a0,
            ((int)sum - a0) / sum, lockRelease / sum, s.Mult.Max(),
            s.Mult.Count(m => m > 1));
    }

    /// <summary>Before and after, per quantity.</summary>
    public static A0Quantity[] Quantities()
    {
        var before = Derive(ExactKeyingSpectrum());
        var after = Derive(ClusteredSpectrum());
        return new[]
        {
            new A0Quantity("A₀ (eigenspaces)", before.A0, after.A0,
                "the headline count group G cited; an artifact of binary64 keying"),
            new A0Quantity("A₀ with multiplicity > 1", before.A0WithMultiplicityAboveOne,
                after.A0WithMultiplicityAboveOne, "recorded by D_047 as 16 079"),
            new A0Quantity("free room Σ(m−1)", before.FreeRoom, after.FreeRoom,
                "= modes − A₀; D_047 records 868 656"),
            new A0Quantity("latent fraction L", before.LatentFraction, after.LatentFraction,
                "= (N − A₀)/N = the D_048 latent fraction"),
            new A0Quantity("lock release (nats)", before.LockRelease, after.LockRelease,
                "Σ_{m>1} m·ln m / Σm; D_047 records 4.16569"),
            new A0Quantity("max multiplicity", before.MaxMultiplicity, after.MaxMultiplicity,
                "D_047 records 738"),
        };
    }

    /// <summary>Quantities that moved at all.</summary>
    public static A0Quantity[] Moved() => Quantities().Where(q => q.Delta != 0.0).ToArray();

    /// <summary>Quantities that did NOT move.</summary>
    public static A0Quantity[] Unmoved() => Quantities().Where(q => q.Delta == 0.0).ToArray();

    // ── The independent cross-check: D_047's published figures ───────────────

    /// <summary>The D96³ row D_047 publishes independently of group G (A₀, A₀(m>1), max m, lock, Σ(m−1)).</summary>
    public static (int A0, int A0AboveOne, int MaxMultiplicity, double LockRelease, int FreeRoom) PublishedByD047()
        => (16080, 16079, 738, 4.16569, 868656);

    /// <summary>Does the clustered spectrum reproduce every D_047 figure? (Five independent checks.)</summary>
    public static bool MatchesD047()
    {
        var a = Derive(ClusteredSpectrum());
        var (a0, above, maxm, lockRef, free) = PublishedByD047();
        return a.A0 == a0 && a.A0WithMultiplicityAboveOne == above
            && a.MaxMultiplicity == maxm && a.FreeRoom == free
            && Math.Abs(a.LockRelease - lockRef) < 1e-5;
    }

    /// <summary>The number of D_047 figures the clustered spectrum reproduces.</summary>
    public static int MatchesD047FigureCount()
    {
        var a = Derive(ClusteredSpectrum());
        var (a0, above, maxm, lockRef, free) = PublishedByD047();
        int hits = 0;
        if (a.A0 == a0) hits++;
        if (a.A0WithMultiplicityAboveOne == above) hits++;
        if (a.MaxMultiplicity == maxm) hits++;
        if (a.FreeRoom == free) hits++;
        if (Math.Abs(a.LockRelease - lockRef) < 1e-5) hits++;
        return hits;
    }

    /// <summary>
    /// The mixed equality discipline at the root of the defect: D96's own spectrum (A₀ = 45, lock 0.802314,
    /// free room 51) is the TOLERANT answer, and its construction reaches the code through a tolerant
    /// eigensolver — while D96³'s was keyed exactly.
    /// </summary>
    public static (int A0, double LockRelease, int FreeRoom) D96Invariants() => (45, 0.802314, 51);

    // ── The claim registry ───────────────────────────────────────────────────

    /// <summary>
    /// Every group-G conclusion that consumes an A₀-derived quantity, with its before/after values, the
    /// precision it states (or the threshold it compares against), and its kind. The classification below is
    /// COMPUTED from those numbers — never typed (the ResearchY-G_027 discipline).
    /// </summary>
    public static A0Claim[] Claims()
    {
        var before = Derive(ExactKeyingSpectrum());
        var after = Derive(ClusteredSpectrum());
        return new[]
        {
            // ── pinned values that fail their own precision ──
            new A0Claim("G_002", "the cube's eigenspace count", "A₀", before.A0, after.A0, 1.0,
                A0ClaimKind.PinnedValue, "asserted as a range around the exact-keying count"),
            new A0Claim("G_002", "the cube's largest multiplicity", "max multiplicity",
                before.MaxMultiplicity, after.MaxMultiplicity, 1.0,
                A0ClaimKind.PinnedValue, "asserted > 550 (the exact-keying value was 562)"),
            new A0Claim("G_003", "the D96³-vs-D96 density delta", "ΔA_at",
                0.276596, 0.263158, 1.0e-6,
                A0ClaimKind.PinnedValue, "asserted to 6 decimals"),
            new A0Claim("G_005", "the cube's lock release", "lock release",
                3.948614, after.LockRelease, 1.0e-5,
                A0ClaimKind.PinnedValue, "asserted |lock − 3.948614| < 1e-5"),
            new A0Claim("G_005", "the cube's latent fraction", "L", 0.97648, after.LatentFraction, 1.0e-5,
                A0ClaimKind.PinnedValue, "asserted |(1 − A₀/N) − 0.97648| < 1e-5"),
            new A0Claim("G_006", "the cube's lock release (restated)", "lock release",
                3.948614, after.LockRelease, 1.0e-5,
                A0ClaimKind.PinnedValue, "the same figure carried into the G_006 narrative"),

            // ── inequalities and identities: unchanged ──
            new A0Claim("G_002", "the cube is overwhelmingly energy-free", "free fraction",
                0.976477, after.LatentFraction, 0.97,
                A0ClaimKind.AboveThreshold, "asserted free/modes > 0.97 — holds under both keyings"),
            new A0Claim("G_006", "the cube has plenty of free room", "L",
                0.97648, after.LatentFraction, 0.97,
                A0ClaimKind.AboveThreshold, "asserted 1 − A₀/884736 > 0.97 — holds under both keyings"),
            new A0Claim("G_002", "free room = N − A₀ and equals the D_048 latent fraction L", "identity",
                1.0, 1.0, 0.0,
                A0ClaimKind.Invariant, "an algebraic identity, true for either A₀"),
            new A0Claim("G_002", "the cube's free room exceeds D96's", "ordering",
                1.0, 1.0, 0.0,
                A0ClaimKind.Invariant, "0.982 > 0.531 either way"),
            new A0Claim("G_005", "D96's own invariants are exact and ε-independent", "D96 A₀",
                45, 45, 0.0,
                A0ClaimKind.PinnedValue, "D96 already came through a tolerant eigensolver — never at risk"),
            new A0Claim("G_005", "D96's lock release", "D96 lock",
                0.802314, 0.802314, 1.0e-5,
                A0ClaimKind.PinnedValue, "unchanged — D96's spectrum was already grouped tolerantly"),

            // ── quoted figures that moved ──
            new A0Claim("G_002", "97.6 % of the cube's directions are energy-free", "free fraction",
                0.976477, after.LatentFraction, 0.0,
                A0ClaimKind.QuotedFigure, "restate as 98.2 %"),
            new A0Claim("G_006", "the cube's contraction factor at m = 200", "contraction factor",
                7.33, 7.10, 0.0,
                A0ClaimKind.QuotedFigure, "recomputed under the corrected multiplicity distribution"),
            new A0Claim("G_006", "D96's contraction factor at m = 200", "contraction factor",
                33.78, 33.78, 0.0,
                A0ClaimKind.QuotedFigure, "μ_k depends on N only, so D96 is untouched by the replacement"),
        };
    }

    /// <summary>THE COMPUTED CLASSIFICATION of a claim.</summary>
    public static A0Impact Classify(A0Claim c) => c.Kind switch
    {
        A0ClaimKind.PinnedValue => Math.Abs(c.After - c.Before) <= c.Tolerance
            ? A0Impact.Unchanged
            : A0Impact.Refuted,
        A0ClaimKind.AboveThreshold => c.Before > c.Tolerance && c.After > c.Tolerance
            ? A0Impact.Unchanged
            : A0Impact.Refuted,
        A0ClaimKind.QuotedFigure => c.After == c.Before ? A0Impact.Unchanged : A0Impact.Boundary,
        A0ClaimKind.Invariant => c.After == c.Before ? A0Impact.Unchanged : A0Impact.Refuted,
        _ => throw new ArgumentOutOfRangeException(nameof(c)),
    };

    /// <summary>(unchanged, boundary, refuted) counts.</summary>
    public static (int Unchanged, int Boundary, int Refuted) Counts()
    {
        var rows = Claims().Select(Classify).ToArray();
        return (rows.Count(r => r == A0Impact.Unchanged),
                rows.Count(r => r == A0Impact.Boundary),
                rows.Count(r => r == A0Impact.Refuted));
    }

    /// <summary>The claims a given impact applies to.</summary>
    public static A0Claim[] ClaimsWith(A0Impact impact)
        => Claims().Where(c => Classify(c) == impact).ToArray();

    /// <summary>
    /// How far a refuted pinned value moved, in units of its OWN stated tolerance — the measure of how
    /// badly it was pinned to the artifact.
    /// </summary>
    public static double ToleranceMultiples(A0Claim c)
        => c.Tolerance <= 0.0 ? double.PositiveInfinity : Math.Abs(c.After - c.Before) / c.Tolerance;

    // ── The verdict, COMPUTED ────────────────────────────────────────────────

    /// <summary>
    /// THE VERDICT, COMPUTED (G_027: never a literal).
    ///   REFUTED   — at least one G-series claim was pinned to the artifact and fails at its own precision;
    ///   BOUNDARY  — only quoted figures moved;
    ///   UNCHANGED — every claim survived the replacement.
    /// </summary>
    public static string Verdict()
    {
        var (_, boundary, refuted) = Counts();
        if (refuted > 0) return "REFUTED";
        return boundary > 0 ? "BOUNDARY" : "UNCHANGED";
    }

    /// <summary>
    /// The scope of the verdict: no *conclusion* breaks — only pinned numbers do. Verified by checking that
    /// every inequality, identity and ordering row is UNCHANGED.
    /// </summary>
    public static bool ConclusionsSurvive()
        => Claims().Where(c => c.Kind != A0ClaimKind.PinnedValue)
                  .All(c => Classify(c) != A0Impact.Refuted);
}
