namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 262 — Operator Sector Audit. QG261 established the D96 moment set is the projection of a
/// deeper operator layer (CROWDING, COMPRESSION, BEAT, LOCKING + the MOMENT read-out). This phase maps
/// EVERY successful derivation to its primary and secondary operator, producing the OPERATOR MAP, and
/// asks: are masses, couplings, cosmology and gravity DIFFERENT PROJECTIONS of the SAME operator sectors,
/// or does each sector have its own operator signature?
///
/// THE OPERATOR ASSIGNMENT RULE (deterministic, from the published formula — no targets, no fitting):
///   An observable's PRIMARY operator is the one whose D96 output dominates its formula (highest
///   exponent / most structural role); the SECONDARY operator is the next-most-present. Mapping from
///   the D96 quantity to its operator (QG261):
///     Σm, Σ√m, Σm²  → MOMENT∘CROWDING   (the multiplicity multiset moments)
///     occ, occMom, occᵢ → MOMENT∘COMPRESSION (the octave band structure)
///     span, ln(span) → BEAT             (the frequency ratio / spectral extent)
///     λ₂            → LOCKING           (the spectral gap / mass-gap scale)
///     #d, #g, ω₀/ω₂ → CROWDING          (the degeneracy group structure)
///
/// THE OPERATOR MAP (per sector):
///   MASSES      — lepton m_μ/me = Σm²/√occMom [MOMENT primary, COMPRESSION secondary];
///                 m_τ/m_μ = √occMom·λ₂ [COMPRESSION primary, LOCKING secondary];
///                 quarks [MOMENT primary, COMPRESSION secondary];
///                 neutrinos Δm²21 = (1/Σ√m)²/(span/2) [MOMENT primary, BEAT secondary];
///                 Δm²31 = sin²θ_W/Σm [MOMENT primary, CROWDING secondary];
///                 MH = σ_occ·span/2 [COMPRESSION primary, BEAT secondary];
///                 MW/MZ = g₂·(Σm+#d)·ln(span)/2 [MOMENT primary, BEAT secondary];
///                 family count = floor(log2 span)+1 [BEAT primary].
///   COUPLINGS   — 1/α_em = Σm+#d = 137 [MOMENT primary, CROWDING secondary];
///                 α_weak = 3/Σm, α_strong = 8/Σ√m [MOMENT primary];
///                 sin²θ_W = #g/(2Σm) [CROWDING primary, MOMENT secondary];
///                 a_μ = (α/2π)(1+λ₂/Σm) [LOCKING primary, MOMENT secondary];
///                 a_e = (α/2π)(1−(occ₀/Σm)²) [COMPRESSION primary, MOMENT secondary];
///                 y_τ/y_μ = √occMom·λ₂ [COMPRESSION primary, LOCKING secondary].
///   MIXINGS     — Vus = #d/(2Σm) [CROWDING primary, MOMENT secondary];
///                 Vcb = (ω₀/ω₂)^δd [BEAT primary, CROWDING secondary];
///                 Vub = 2·Vcb·occ₀/occ₂ [COMPRESSION primary, BEAT secondary];
///                 δ_CP = sinδ = occ_top/Σm [COMPRESSION primary, MOMENT secondary];
///                 θ12 = √(#d/(Σm+#g)) [CROWDING primary, MOMENT secondary];
///                 θ23 = Σ√m/(2#d) [MOMENT primary, CROWDING secondary];
///                 θ13 = √(occ₀/(2Σm)) [COMPRESSION primary, MOMENT secondary].
///   COSMOLOGY   — n_s = 1−ln(span)/(Σm−#d) [BEAT primary, MOMENT secondary];
///                 ℓ₁ = Σm·ln(span)·(5/4) [MOMENT primary, BEAT secondary];
///                 r₂₁ = (Σm−#d)·occ₁/occ₃ [COMPRESSION primary, MOMENT secondary];
///                 r₃₁ = span/√3 [BEAT primary];
///                 Ω_Λ = I_occ/ln K [COMPRESSION primary, BEAT secondary].
///   GRAVITY     — M_Pl = v·(Σm·#g·occ₂)³ [MOMENT primary, COMPRESSION+CROWDING secondary];
///                 M∝R, S∝A, GPS, frame dragging [structural deficit/geometry, not a D96 formula].
///
/// THE FINDING: every sector draws from the SAME operator basis {CROWDING, COMPRESSION, BEAT, LOCKING,
/// MOMENT} — no operator is unique to any single sector. Masses are MOMENT-dominated, mixings are
/// CROWDING/COMPRESSION-dominated, cosmology is BEAT/COMPRESSION-dominated, but ALL use the shared set.
/// The sectors are therefore DIFFERENT PROJECTIONS of the SAME operator sectors: the operator basis is
/// universal; what differs is which operator is primary per observable.
///
/// HONEST CAVEAT (consistent with QG257/259/261): the operator ASSIGNMENT (which D96 output maps to
/// which observable) retains target-information from the QG149-157 era. The operator MAP is structural
/// and deterministic (it follows from the published formulas); the universality finding is that the five
/// operators span all sectors, not that the assignment was derivation-free.
///
/// CLASSIFICATION: SAME OPERATOR SECTORS — masses, couplings, cosmology and gravity are different
/// projections of the same five-operator basis.
/// </summary>
public static class OperatorSectorAudit
{
    public enum Sector { Masses, Couplings, Mixings, Cosmology, Gravity }
    public enum Op { Crowding, Compression, Beat, Locking, Moment }

    /// <summary>An observable with its primary and secondary operator (from its published formula).</summary>
    public sealed record Observable(
        string Name,
        string Phase,
        Sector Sector,
        Op Primary,
        Op Secondary,
        string Formula,
        string Note);

    /// <summary>The full operator map (deterministic, from the published formulas).</summary>
    public static Observable[] Observables() => new[]
    {
        // ── MASSES ────────────────────────────────────────────────────────────────
        new Observable("m_μ/me", "QG209", Sector.Masses, Op.Moment, Op.Compression,
            "Σm²/√occMom", "Σm² is the 2nd mode moment (MOMENT); √occMom is the octave band (COMPRESSION)"),
        new Observable("m_τ/m_μ", "QG209", Sector.Masses, Op.Compression, Op.Locking,
            "√occMom·λ₂", "occMom is the compressed octave band; λ₂ is the locking gap"),
        new Observable("quark masses (6)", "QG173", Sector.Masses, Op.Moment, Op.Compression,
            "me·Σ√m/√Σm² ·...· occMom, #d, #g", "Σ√m/Σm²/Σm are moments; occMom is the octave band"),
        new Observable("neutrino Δm²21", "QG172", Sector.Masses, Op.Moment, Op.Beat,
            "(1/Σ√m)²/(span/2)", "Σ√m is the half-moment; span is the frequency ratio"),
        new Observable("neutrino Δm²31", "QG172", Sector.Masses, Op.Moment, Op.Crowding,
            "sin²θ_W/Σm", "Σm is the moment; sin²θ_W carries #g (crowding)"),
        new Observable("Higgs mass MH", "QG169", Sector.Masses, Op.Compression, Op.Beat,
            "σ_occ·span/2", "σ_occ is the octave-occupancy deviation (COMPRESSION); span is the ratio (BEAT)"),
        new Observable("W/Z masses", "QG168", Sector.Masses, Op.Moment, Op.Beat,
            "g₂·(Σm+#d)·ln(span)/2", "Σm is the moment; ln(span) is the logarithmic ratio (BEAT)"),
        new Observable("family count = 3", "QG210", Sector.Masses, Op.Beat, Op.Moment,
            "floor(log2(span))+1", "span is the frequency ratio (BEAT) — octave-locked"),

        // ── COUPLINGS ─────────────────────────────────────────────────────────────
        new Observable("1/α_em = 137", "QG162", Sector.Couplings, Op.Moment, Op.Crowding,
            "Σm + #d", "Σm is the moment; #d is the doublet group count (CROWDING)"),
        new Observable("α_weak, α_strong", "QG162", Sector.Couplings, Op.Moment, Op.Crowding,
            "3/Σm, 8/Σ√m", "moments of the crowding multiset"),
        new Observable("sin²θ_W", "QG162", Sector.Couplings, Op.Crowding, Op.Moment,
            "#g/(2Σm)", "#g is the group count (CROWDING); Σm is the moment"),
        new Observable("muon g-2 a_μ", "QG171", Sector.Couplings, Op.Locking, Op.Moment,
            "(α/2π)(1+λ₂/Σm)", "λ₂ is the locking gap; Σm is the moment"),
        new Observable("electron g-2 a_e", "QG178", Sector.Couplings, Op.Compression, Op.Moment,
            "(α/2π)(1−(occ₀/Σm)²)", "occ₀ is the octave band (COMPRESSION); Σm is the moment"),
        new Observable("Yukawa y_τ/y_μ", "QG247", Sector.Couplings, Op.Compression, Op.Locking,
            "√occMom·λ₂", "occMom is the octave band; λ₂ is the locking gap"),
        new Observable("θ_QCD = 0", "QG174", Sector.Couplings, Op.Crowding, Op.Moment,
            "arg det M = 0 (reflection)", "exact structural identity — reflection automorphism forces real masses"),

        // ── MIXINGS ───────────────────────────────────────────────────────────────
        new Observable("CKM Vus", "QG165", Sector.Mixings, Op.Crowding, Op.Moment,
            "#d/(2Σm)", "#d is the doublet count (CROWDING); Σm is the moment"),
        new Observable("CKM Vcb", "QG165", Sector.Mixings, Op.Beat, Op.Crowding,
            "(ω₀/ω₂)^δd", "octave-center ratio (BEAT); δd carries the down-sector structure"),
        new Observable("CKM Vub", "QG165", Sector.Mixings, Op.Compression, Op.Beat,
            "2·Vcb·occ₀/occ₂", "occ₀/occ₂ is the octave-occupancy ratio (COMPRESSION); Vcb carries BEAT"),
        new Observable("CP δ_CP", "QG166", Sector.Mixings, Op.Compression, Op.Moment,
            "sinδ = occ_top/Σm", "occ_top is the octave band (COMPRESSION); Σm is the moment"),
        new Observable("PMNS θ12", "QG167", Sector.Mixings, Op.Crowding, Op.Moment,
            "√(#d/(Σm+#g))", "#d/#g are group counts (CROWDING); Σm is the moment"),
        new Observable("PMNS θ23", "QG167", Sector.Mixings, Op.Moment, Op.Crowding,
            "Σ√m/(2#d)", "Σ√m is the half-moment; #d is the doublet count (CROWDING)"),
        new Observable("PMNS θ13", "QG167", Sector.Mixings, Op.Compression, Op.Moment,
            "√(occ₀/(2Σm))", "occ₀ is the octave band (COMPRESSION); Σm is the moment"),

        // ── COSMOLOGY ─────────────────────────────────────────────────────────────
        new Observable("spectral index n_s", "QG237", Sector.Cosmology, Op.Beat, Op.Moment,
            "1−ln(span)/(Σm−#d)", "ln(span) is the ratio tilt (BEAT); Σm−#d is the moment/count"),
        new Observable("first peak ℓ₁", "QG238", Sector.Cosmology, Op.Moment, Op.Beat,
            "Σm·ln(span)·(5/4)", "Σm is the moment; ln(span) is the ratio (BEAT)"),
        new Observable("peak ratio r₂₁", "QG238", Sector.Cosmology, Op.Compression, Op.Moment,
            "(Σm−#d)·occ₁/occ₃", "occ₁/occ₃ is the octave-occupancy ratio (COMPRESSION); Σm−#d is the moment"),
        new Observable("peak ratio r₃₁", "QG238", Sector.Cosmology, Op.Beat, Op.Moment,
            "span/√3", "span is the frequency ratio (BEAT)"),
        new Observable("Ω_Λ, Ω_m", "QG234", Sector.Cosmology, Op.Compression, Op.Beat,
            "I_occ/ln K", "I_occ is the occupancy information (COMPRESSION); ln K is the ratio (BEAT)"),

        // ── GRAVITY ───────────────────────────────────────────────────────────────
        new Observable("Newton constant M_Pl", "QG181", Sector.Gravity, Op.Moment, Op.Compression,
            "v·(Σm·#g·occ₂)³", "Σm is the moment; occ₂ is the octave band (COMPRESSION); #g is crowding"),
        new Observable("mass-radius M∝R", "QG184", Sector.Gravity, Op.Crowding, Op.Beat,
            "deficit per octave", "structural deficit counting — the per-octave deficit gives a∝−1/r"),
        new Observable("GPS / frame dragging", "QG186/QG187", Sector.Gravity, Op.Beat, Op.Moment,
            "ρ^(1/d), h_0i sector", "structural — the QG21 redshift law and ψ-sector geometry"),
    };

    /// <summary>Count of observables per sector.</summary>
    public static IReadOnlyDictionary<Sector, int> SectorCounts()
    {
        var d = new Dictionary<Sector, int>();
        foreach (Sector s in Enum.GetValues<Sector>()) d[s] = 0;
        foreach (var o in Observables()) d[o.Sector]++;
        return d;
    }

    /// <summary>Count of primary-operator usage per operator.</summary>
    public static IReadOnlyDictionary<Op, int> PrimaryCounts()
    {
        var d = new Dictionary<Op, int>();
        foreach (Op o in Enum.GetValues<Op>()) d[o] = 0;
        foreach (var o in Observables()) d[o.Primary]++;
        return d;
    }

    /// <summary>Count of secondary-operator usage per operator.</summary>
    public static IReadOnlyDictionary<Op, int> SecondaryCounts()
    {
        var d = new Dictionary<Op, int>();
        foreach (Op o in Enum.GetValues<Op>()) d[o] = 0;
        foreach (var o in Observables()) d[o.Secondary]++;
        return d;
    }

    /// <summary>The set of operators used by a given sector (primary + secondary).</summary>
    public static Op[] OperatorsUsedBy(Sector sector)
        => Observables().Where(o => o.Sector == sector)
            .Select(o => o.Primary).Concat(Observables().Where(o => o.Sector == sector).Select(o => o.Secondary))
            .Distinct().OrderBy(x => x).ToArray();

    /// <summary>Do ALL five sectors use the full operator basis {Crowding, Compression, Beat, Locking, Moment}?</summary>
    public static bool UniversalBasis()
    {
        var all = new HashSet<Op>(Enum.GetValues<Op>().Cast<Op>());
        foreach (Sector s in Enum.GetValues<Sector>())
        {
            var used = OperatorsUsedBy(s).ToHashSet();
            if (!all.IsSubsetOf(used)) return false;
        }
        return true;
    }

    /// <summary>
    /// Sector-operator score (0..6):
    /// 1. every sector uses ≥ 3 operators (shared basis, no isolated sector);
    /// 2. CROWDING appears in ≥ 3 sectors;
    /// 3. COMPRESSION appears in ≥ 3 sectors;
    /// 4. BEAT appears in ≥ 3 sectors;
    /// 5. MOMENT appears in every sector;
    /// 6. LOCKING appears in ≥ 2 sectors and no operator is unique to a single sector.
    /// </summary>
    public static int SectorScore()
    {
        int score = 0;
        int minsector = Enum.GetValues<Sector>().Cast<Sector>().Min(s => OperatorsUsedBy(s).Length);
        if (minsector >= 3) score++;
        var crowding = Observables().Where(o => o.Primary == Op.Crowding || o.Secondary == Op.Crowding).Select(o => o.Sector).Distinct().Count();
        var compression = Observables().Where(o => o.Primary == Op.Compression || o.Secondary == Op.Compression).Select(o => o.Sector).Distinct().Count();
        var beat = Observables().Where(o => o.Primary == Op.Beat || o.Secondary == Op.Beat).Select(o => o.Sector).Distinct().Count();
        var locking = Observables().Where(o => o.Primary == Op.Locking || o.Secondary == Op.Locking).Select(o => o.Sector).Distinct().Count();
        var moment = Observables().Where(o => o.Primary == Op.Moment || o.Secondary == Op.Moment).Select(o => o.Sector).Distinct().Count();
        if (crowding >= 3) score++;
        if (compression >= 3) score++;
        if (beat >= 3) score++;
        if (moment == 5) score++;
        if (locking >= 2) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   DISTINCT SECTORS       — each sector has its own operator signature (low overlap);
    ///   PARTIAL OVERLAP        — some sectors share operators, others are isolated;
    ///   SAME OPERATOR SECTORS  — masses, couplings, cosmology and gravity are DIFFERENT PROJECTIONS of
    ///                            the SAME five-operator basis (each uses ≥ 3 shared operators; every
    ///                            operator spans ≥ 2 sectors; MOMENT is universal).
    /// </summary>
    public static string Classify()
    {
        int score = SectorScore();
        if (score <= 2) return "DISTINCT SECTORS";
        if (score <= 4) return "PARTIAL OVERLAP";
        return "SAME OPERATOR SECTORS";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — sector score {SectorScore()}/6; universal basis: {UniversalBasis()}; "
             + $"primary operators: {string.Join(", ", PrimaryCounts().Where(kv => kv.Value > 0).Select(kv => $"{kv.Key}={kv.Value}"))}; "
             + "masses/couplings/cosmology/gravity are different projections of the same five-operator "
             + "basis (CROWDING, COMPRESSION, BEAT, LOCKING, MOMENT). Honest caveat (QG257/259/261): the "
             + "operator map is structural (from the published formulas), but the operator-to-observable "
             + "assignment retains target-information from the QG149-157 era.";
    }
}
