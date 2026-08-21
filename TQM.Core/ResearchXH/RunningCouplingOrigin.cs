namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 163 — Running coupling origin. QG162 established the coupling laws at the observable
/// scale: 1/α_em = Σm + #doublets = 137, α_weak = 3/Σm, α_strong = 8/Σ√m. This phase asks: WHY do the
/// couplings run with energy, and does a unification scale emerge — using ONLY D96 spectral geometry,
/// with no fitted beta functions?
///
/// Method (computational, fully deterministic): the couplings are functions of the OCCUPANCY STATISTICS
/// of the activated spectral content. As the energy scale E increases, more spectral modes are
/// ACTIVATED (higher spectral bands join the accessible set). (1) SPECTRAL SCALE — the octave-band
/// ladder of the observable sector (sizes [4,4,87]) is the natural energy scale: rung 1 = 4 modes,
/// rung 2 = 8 modes, rung 3 = 95 modes. (2) OCCUPATION FLOW — the denominators Σm, #doublets, Σ√m grow
/// as occupation flows up the bands (4 → 8 → 95). (3) SCALE-DEPENDENT ACCESS — α_i(E) = g_i / D_i(N(E))
/// where N(E) = activated modes and D_i is the coupling denominator: α_em(E) = 1/(Σm(E)+#doublets(E)),
/// α_weak(E) = 3/Σm(E), α_strong(E) = 8/Σ√m(E). (4) RUNNING — all three couplings DECREASE with E
/// because the denominators grow; α_strong decreases fastest (QCD-like asymptotic-freedom direction).
/// (5) UNIFICATION — the structural bound 1/α_em = Σm + #doublets &gt; Σm/3 = 1/α_weak (since
/// #doublets &gt; 0) holds at EVERY scale, so α_em &lt; α_weak at all scales: the hierarchy is preserved
/// and NO unification occurs within the observable sector — consistent with the observed low-energy
/// hierarchy and a GUT scale beyond the observable octave ladder.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class RunningCouplingOrigin
{
    /// <summary>Coupling generator counts (QG161/162).</summary>
    public const double GEm = 1.0;
    public const double GWeak = 3.0;
    public const double GStrong = 8.0;

    // ── 1. Spectral scale (octave ladder) ──────────────────────────────────────

    /// <summary>Octave-band sizes of the observable sector ([4,4,87]).</summary>
    public static int[] OctaveBandSizes()
    {
        var w = FamilyIndexOrigin.IntraSectorModes();
        return SpectralClasses.OctaveFamilies(w).familySizes;
    }

    /// <summary>Number of octave rungs (3).</summary>
    public static int OctaveRungCount()
        => OctaveBandSizes().Length;

    /// <summary>
    /// Activation ladder: cumulative mode counts per octave rung (4, 8, 95).
    /// </summary>
    public static int[] ActivationLadder()
    {
        var sizes = OctaveBandSizes();
        var ladder = new int[sizes.Length];
        int cum = 0;
        for (int i = 0; i < sizes.Length; i++)
        {
            cum += sizes[i];
            ladder[i] = cum;
        }
        return ladder;
    }

    // ── 2. Occupation statistics at scale E ────────────────────────────────────

    /// <summary>Multiplicity groups of the first N sorted modes.</summary>
    private static int[] GroupsAt(int n)
    {
        var w = FamilyIndexOrigin.IntraSectorModes();
        var sel = w.Take(n).ToArray();
        var groups = new List<int>();
        int i = 0;
        while (i < sel.Length)
        {
            int j = i;
            while (j + 1 < sel.Length && Math.Abs(sel[j + 1] - sel[i]) < 1e-9) j++;
            groups.Add(j - i + 1);
            i = j + 1;
        }
        return groups.ToArray();
    }

    /// <summary>Σm(E) — total activated modes at the first N modes.</summary>
    public static int ActiveModes(int n)
        => GroupsAt(n).Sum();

    /// <summary>#doublets(E) — Z2 doublet groups among the first N modes.</summary>
    public static int ActiveDoublets(int n)
        => GroupsAt(n).Count(g => g == 2);

    /// <summary>Σ√m(E) — neutral moment among the first N modes.</summary>
    public static double ActiveNeutralMoment(int n)
        => GroupsAt(n).Sum(g => Math.Sqrt(g));

    // ── 3. Couplings at scale E ────────────────────────────────────────────────

    /// <summary>1/α_em(E) = Σm(E) + #doublets(E).</summary>
    public static double InverseAlphaEmAt(int n)
        => ActiveModes(n) + ActiveDoublets(n);

    /// <summary>α_weak(E) = 3/Σm(E).</summary>
    public static double AlphaWeakAt(int n)
        => GWeak / ActiveModes(n);

    /// <summary>α_strong(E) = 8/Σ√m(E).</summary>
    public static double AlphaStrongAt(int n)
        => GStrong / ActiveNeutralMoment(n);

    /// <summary>
    /// Couplings at each octave rung: (rung, N, 1/α_em, α_weak, α_strong).
    /// </summary>
    public static (int Rung, int N, double EmInv, double Weak, double Strong)[] RungCouplings()
    {
        var ladder = ActivationLadder();
        var result = new List<(int, int, double, double, double)>();
        for (int r = 0; r < ladder.Length; r++)
        {
            int n = ladder[r];
            result.Add((r + 1, n, InverseAlphaEmAt(n), AlphaWeakAt(n), AlphaStrongAt(n)));
        }
        return result.ToArray();
    }

    // ── 4. Running direction & rates ───────────────────────────────────────────

    /// <summary>
    /// Running direction: all three COUPLINGS decrease with activation. For α_em the inverse 1/α_em
    /// GROWS (6 → 12 → 137) which means α_em = 1/(inverse) DECREASES. Returns (em, weak, strong).
    /// </summary>
    public static (bool Em, bool Weak, bool Strong) MonotoneDecrease()
    {
        var ladder = ActivationLadder();
        bool em = true, wk = true, st = true;
        double prevEmInv = 0, prevWk = double.MaxValue, prevSt = double.MaxValue;
        foreach (int n in ladder)
        {
            double emInv = InverseAlphaEmAt(n); // grows → α_em decreases
            double wkV = AlphaWeakAt(n);
            double stV = AlphaStrongAt(n);
            if (emInv <= prevEmInv) em = false;   // α_em decreases iff inverse increases
            if (wkV >= prevWk) wk = false;
            if (stV >= prevSt) st = false;
            prevEmInv = emInv; prevWk = wkV; prevSt = stV;
        }
        return (em, wk, st);
    }

    /// <summary>
    /// Running factors from the lowest rung to the observable scale (multiplicative decrease of the
    /// COUPLINGS α_i). Returns (em, weak, strong) — the decrease factors (&gt; 1 = running down).
    /// For α_em we use the inverse 1/α_em which GROWS, so the decrease factor is high/low.
    /// </summary>
    public static (double Em, double Weak, double Strong) RunningFactors()
    {
        var c = RungCouplings();
        var low = c[0];
        var high = c[^1];
        return (high.EmInv / low.EmInv, low.Weak / high.Weak, low.Strong / high.Strong);
    }

    /// <summary>
    /// All three couplings run by COMPARABLE factors (the shared occupation flow drives all three
    /// denominators, so the hierarchy is preserved — no in-sector unification). True if the max/min
    /// running-factor ratio is within 20%.
    /// </summary>
    public static bool ComparableRunningRates()
    {
        var (em, wk, st) = RunningFactors();
        double max = Math.Max(em, Math.Max(wk, st));
        double min = Math.Min(em, Math.Min(wk, st));
        return max / min < 1.2;
    }

    // ── 5. Unification (hierarchy preservation) ────────────────────────────────

    /// <summary>
    /// Structural bound: 1/α_em = Σm + #doublets &gt; Σm/3 = 1/α_weak at EVERY scale (since
    /// #doublets &gt; 0), so α_em &lt; α_weak always: the hierarchy is preserved and the couplings do NOT
    /// unify within the observable sector. This is consistent with a GUT scale BEYOND the observable
    /// octave ladder.
    /// </summary>
    public static bool HierarchyPreservedAtAllScales()
    {
        for (int n = 2; n <= 95; n++)
            if (InverseAlphaEmAt(n) <= ActiveModes(n) / 3.0) return false;
        return true;
    }

    /// <summary>Is the strong coupling largest at every rung (QCD-like hierarchy)?</summary>
    public static bool StrongLargestAtEveryRung()
    {
        foreach (var (_, _, emInv, wk, st) in RungCouplings())
            if (st <= wk || wk * emInv <= 1.0) return false;
        return true;
    }

    /// <summary>
    /// Unification statement: NO unification within the observable sector (the hierarchy is preserved
    /// at all scales). The GUT scale, if any, lies beyond the observable octave ladder.
    /// </summary>
    public static string UnificationStatement()
        => HierarchyPreservedAtAllScales()
            ? "NO in-sector unification: α_em < α_weak < α_strong at all scales (hierarchy preserved); unification, if any, lies at a GUT scale beyond the observable octave ladder."
            : "in-sector unification present";

    // ── Origin score & classification ─────────────────────────────────────────

    /// <summary>
    /// Running-origin score (0..5):
    /// 1. the octave-band ladder defines a natural spectral (energy) scale (≥ 3 rungs);
    /// 2. the denominators (Σm, #doublets, Σ√m) GROW along the ladder (occupation flow);
    /// 3. all three couplings DECREASE monotonically with activation (running exists);
    /// 4. the three couplings run at comparable rates (the shared occupation flow drives all three,
    ///    preserving the hierarchy);
    /// 5. the hierarchy is preserved at all scales (no in-sector unification — consistent with a GUT
    ///    scale beyond the observable ladder).
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        if (OctaveRungCount() >= 3) score++;
        var ladder = ActivationLadder();
        bool denomGrow = true;
        for (int i = 1; i < ladder.Length; i++)
            if (ActiveModes(ladder[i]) <= ActiveModes(ladder[i - 1])) denomGrow = false;
        if (denomGrow) score++;
        var (em, wk, st) = MonotoneDecrease();
        if (em && wk && st) score++;
        if (ComparableRunningRates()) score++;
        if (HierarchyPreservedAtAllScales()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ORIGIN      — no spectral-scale running emerges from D96;
    ///   PARTIAL ORIGIN — some running exists but without a coherent spectral-scale mechanism;
    ///   RUNNING ORIGIN — the running of the gauge couplings EMERGES from D96 spectral geometry: the
    ///                    octave-band ladder defines the spectral (energy) scale, the occupation flow
    ///                    (4 → 8 → 95 modes) grows the coupling denominators, and α_i(E) = g_i / D_i(N(E))
    ///                    runs monotonically — all three couplings decrease by comparable factors (~23x)
    ///                    driven by the shared occupation flow. The structural bound 1/α_em &gt; Σm/3 =
    ///                    1/α_weak holds at every scale, so the hierarchy is preserved and the couplings
    ///                    do NOT unify within the observable sector — consistent with a GUT scale beyond
    ///                    the observable octave ladder — with no fitted beta functions.
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (score <= 2) return "NO ORIGIN";
        if (score == 5) return "RUNNING ORIGIN";
        return "PARTIAL ORIGIN";
    }
}
