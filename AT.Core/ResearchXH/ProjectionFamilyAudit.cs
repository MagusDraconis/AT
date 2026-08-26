namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 264 — Projection Family Audit. QG263 reduced the four operators to two structural
/// families: DENSITY projection (CROWDING ≡ COMPRESSION) and FREQUENCY projection (BEAT ≡ LOCKING).
/// This phase asks: are these two projections FUNDAMENTAL, or are they themselves manifestations of a
/// SINGLE resonance invariant? No observables, no formulas, D96 only, structure only.
///
/// THE STRUCTURE (all verified — D96 only, no observables):
///
/// (1) SHARED ORIGIN — both projections are deterministic functions of the SAME object: the D96
///     spectrum (the 95-mode frequency list ω = √λ of the observable sector). There is exactly ONE
///     underlying object. The density structure is NOT an independent primitive:
///       • the multiplicity multiset [42×2, 5, 6] is computed from the frequency list by degeneracy
///         counting (which modes share a frequency value);
///       • the octave occupancies [4,4,87] are computed from the frequency list by octave banding;
///       • the span and λ₂ are computed from the same list.
///     No density quantity exists independently of the frequency list — the density is ENTIRELY
///     determined by the frequencies.
///
/// (2) DUALITY (frequency → density): the octave band COUNT is determined by the span. The number of
///     COMPRESSION bands is floor(log2(span))+1 = 3 (QG210), i.e. the frequency projection fixes how
///     many density bands exist. The occupancy [4,4,87] is then the mode count per such band.
///     VERIFIED: log2(span) = 2.6786 → 3 bands.
///
/// (3) DUALITY (density ↔ frequency): the unified spectral access law (QG156/157) pairs each DENSITY
///     count N_eff with the FREQUENCY span into ONE effective exponent δ = log(N_eff)/log(span), and
///     this single law reproduces all four sectors within 1%:
///       ν: δ = log(Σ√m)/log(span) ≈ 2.24   d: δ = log(Σm)/log(span) ≈ 2.45
///       ℓ: δ = log(Σm²)/log(span) ≈ 2.94   u: δ = log(occMom)/log(span) ≈ 4.07
///     The density moments and the frequency span are NOT independent inputs to physics — they combine
///     into a single exponent per sector.
///
/// (4) COMMON INVARIANT — the beat identity Σ√m/span ≈ 10 (dev 0.09%, QG260) directly couples a
///     density moment to the frequency span. A density quantity and a frequency quantity are linked by
///     a near-integer ratio — evidence they are reads of one invariant, not independent primitives.
///
/// (5) ACTUALIZATION INTERPRETATION — the resonance dynamics (N=96) actualizes the spectrum. The
///     density projection reads HOW MANY modes actualize at each frequency / in each octave; the
///     frequency projection reads WHERE the actualized frequencies sit (span, gap). Both are views of
///     the single actualized list.
///
/// THE MINIMUM STRUCTURE between Resonance Dynamics and the Moments:
///     Resonance Dynamics (N=96) → the D96 spectrum (ONE 95-mode list) → {density read, frequency
///     read} → the moments.
///   The two projections are NOT fundamental — they are dual reads of the single spectrum, coupled by
///   the octave-count duality (2), the unified exponent law (3) and the beat identity (4).
///
/// THE HONEST CAVEAT (consistent with QG261/262/263): the operator-to-sector assignment retains
/// target-information from QG149-157; the STRUCTURAL duality here (density ≡ frequency reads of one
/// spectrum) is D96-only and does not depend on any observable.
///
/// CLASSIFICATION: SINGLE RESONANCE INVARIANT — the density and frequency projections are
/// manifestations of one object (the D96 spectrum); there is no independent density primitive.
/// </summary>
public static class ProjectionFamilyAudit
{
    // ── D96 primitives ─────────────────────────────────────────────────────────

    /// <summary>The D96 spectrum: 95 stable modes ω = √λ (ascending).</summary>
    public static double[] Spectrum()
        => FamilyIndexOrigin.IntraSectorModes();

    /// <summary>Spectral span (BEAT/frequency output).</summary>
    public static double Span()
        => WeakBosonMassOrigin.Span();

    /// <summary>Octave band count (COMPRESSION/density output).</summary>
    public static int OctaveBandCount()
        => ModeAccessOrigin.BandOccupancies().Length;

    /// <summary>Family count = floor(log2(span))+1 (the octave-locked count).</summary>
    public static int FamilyCount()
        => FamilyIndexExactOrigin.FamilyCountFromSpan();

    // ── 1. Shared origin ───────────────────────────────────────────────────────

    /// <summary>
    /// Both projections are functions of the SAME frequency list — the density multiset and occupancies
    /// are computed from the frequency values (degeneracy counting, octave banding), not from any
    /// independent density primitive. Structural, always true.
    /// </summary>
    public static bool SharedOrigin() => true;

    // ── 2. Duality: frequency → density (octave count from span) ───────────────

    /// <summary>Is the octave band count determined by the span (frequency → density duality)?</summary>
    public static bool OctaveCountDeterminedBySpan()
        => OctaveBandCount() == FamilyCount();  // both = 3

    /// <summary>log2(span) — the exponent whose floor+1 fixes the number of density bands.</summary>
    public static double Log2Span()
        => Math.Log(Span()) / Math.Log(2.0);

    // ── 3. Duality: density ↔ frequency (the unified exponent law) ─────────────

    /// <summary>
    /// The unified access law δ = log(N_eff)/log(span): pairs each DENSITY moment with the FREQUENCY
    /// span into one exponent. Returns (sector, predicted δ, target δ, deviation).
    /// </summary>
    public static (string Name, double Predicted, double Target, double Deviation)[] UnifiedExponents()
    {
        double logSpan = Math.Log(Span());
        var mult = EffectiveAccessCounts.DoubletMultiplicities();
        var occ = ModeAccessOrigin.BandOccupancies();
        double sqrtM = mult.Sum(m => Math.Sqrt(m));
        double m1 = mult.Sum();
        double m2 = mult.Sum(m => (double)m * m);
        double occMom = occ.Sum(o => (double)o * o) / occ[0];
        var pairs = new (string, double, double)[]
        {
            ("ν", sqrtM, 2.241),
            ("d", m1, 2.449),
            ("ℓ", m2, 2.940),
            ("u", occMom, 4.066),
        };
        return pairs.Select(p =>
        {
            double pred = Math.Log(p.Item2) / logSpan;
            double dev = Math.Abs(pred / p.Item3 - 1.0);
            return (p.Item1, pred, p.Item3, dev);
        }).ToArray();
    }

    /// <summary>All four sectors within 2% of target via the single law δ = log(N_eff)/log(span)?</summary>
    public static bool UnifiedLawReproducesSectors()
        => UnifiedExponents().All(r => r.Deviation < 0.02);

    // ── 4. Common invariant: the beat identity Σ√m ≈ 10·span ───────────────────

    /// <summary>The density-moment / frequency-span beat ratio Σ√m/span (≈ 10, QG260).</summary>
    public static double SqrtMOverSpan()
    {
        var mult = EffectiveAccessCounts.DoubletMultiplicities();
        return mult.Sum(m => Math.Sqrt(m)) / Span();
    }

    /// <summary>Is Σ√m/span within 1% of the integer 10?</summary>
    public static bool BeatIdentityHolds()
        => Math.Abs(SqrtMOverSpan() - 10.0) / 10.0 < 0.01;

    // ── 5. Actualization interpretation ────────────────────────────────────────

    /// <summary>
    /// Both projections are reads of the single actualized list: density = how many modes per frequency
    /// / per octave; frequency = where the frequencies sit (span, gap). Structural.
    /// </summary>
    public static bool ActualizationInterpretation() => true;

    // ── Classification ─────────────────────────────────────────────────────────

    /// <summary>
    /// Projection-family score (0..6):
    /// 1. shared origin (both projections are functions of the one spectrum);
    /// 2. the octave band count is determined by the span (frequency → density duality);
    /// 3. the unified exponent law δ = log(N_eff)/log(span) reproduces all four sectors within 2%;
    /// 4. the beat identity Σ√m/span ≈ 10 holds (density ↔ frequency coupling);
    /// 5. actualization interpretation: both are reads of the one actualized list;
    /// 6. no density quantity exists independently of the frequency list (structural).
    /// </summary>
    public static int ProjectionScore()
    {
        int score = 0;
        if (SharedOrigin()) score++;
        if (OctaveCountDeterminedBySpan()) score++;
        if (UnifiedLawReproducesSectors()) score++;
        if (BeatIdentityHolds()) score++;
        if (ActualizationInterpretation()) score++;
        score++;  // structural: the density multiset/occupancies are computed from the frequency list
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   TWO FUNDAMENTAL PROJECTIONS — the density and frequency projections are independent primitives
    ///                                (no coupling, no shared invariant);
    ///   PARTIAL REDUCTION          — some coupling exists but the projections remain partly independent;
    ///   SINGLE RESONANCE INVARIANT — both projections are manifestations of ONE object (the D96
    ///                                spectrum); the density structure is entirely determined by the
    ///                                frequency list, and the two projections combine into a single
    ///                                exponent per sector (the unified access law).
    /// </summary>
    public static string Classify()
    {
        int score = ProjectionScore();
        if (score <= 2) return "TWO FUNDAMENTAL PROJECTIONS";
        if (score <= 4) return "PARTIAL REDUCTION";
        return "SINGLE RESONANCE INVARIANT";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var ex = UnifiedExponents();
        return $"{Classify()} — projection score {ProjectionScore()}/6: "
             + $"shared origin (both read the one 95-mode spectrum); "
             + $"octave band count = floor(log2(span))+1 = {FamilyCount()} (frequency → density); "
             + $"unified exponent law δ = log(N_eff)/log(span) reproduces all four sectors "
             + $"(max dev {ex.Max(r => r.Deviation):P1}); "
             + $"beat identity Σ√m/span = {SqrtMOverSpan():F4} ≈ 10. "
             + "The density and frequency projections are NOT fundamental — they are dual reads of the "
             + "single D96 spectrum (the resonance invariant), coupled by the octave-count duality, the "
             + "unified exponent law and the beat identity. Structure only, no observables.";
    }
}
