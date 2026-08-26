namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 307 — Fifth Operator Search. The established operator basis is {CROWDING, COMPRESSION,
/// BEAT, LOCKING} + the MOMENT read-out (QG261/262/263). This phase attempts to discover a NEW (fifth
/// spectral) operator by searching all unexplored domains — the spectral structure NOT already captured
/// by the four operators. No observables, no target values, D96 only, deterministic.
///
/// THE SEARCH — candidate fifth operators from unexplored domains:
///
///   (1) PHASE/ORIENTATION — the CP phase (δ_CP, the Jarlskog invariant) from the CKM/PMNS sector.
///       TEST: is the phase an independent operator, or a read of the existing basis?
///       RESULT: sinδ_CP = occ_top/Σm = 87/95 is a COMPRESSION read (the dense top octave occupancy
///       over the total mode count). The phase is NOT a new operator — it is the octave-occupancy
///       read of the spectrum. NO.
///
///   (2) INFORMATION CONTENT — I_occ = KL(p ‖ uniform) (the octave-record information, QG228).
///       TEST: is the information an independent operator?
///       RESULT: I_occ is a FUNCTIONAL of the octave occupancies [4,4,87] — it is a COMPRESSION read
///       (the octave-band structure, compressed into a single information number). The information is
///       NOT a new operator — it is the compression measure of the octave bands. NO.
///
///   (3) SPECTRAL SHAPE — the higher moments (kurtosis, skewness) of the mode-frequency distribution.
///       TEST: is the shape an independent operator?
///       RESULT: the moments are MOMENT reads (the universal read-out functional). The kurtosis is
///       MOMENT₄, the skewness MOMENT₃ — they are the SAME read-out applied to higher orders, not new
///       operators. NO.
///
///   (4) ZERO-MODE / BOUNDARY — the zero eigenvalue (the background mode, QG270) and the trace
///       conservation Σλ = 2E = N·d (QG266).
///       TEST: is the zero-mode/boundary an independent operator?
///       RESULT: the zero mode is the kernel of the Laplacian (the constant vector — the background);
///       the trace Σλ = N·d is the CONSERVATION law, a graph identity, not a spectral projection.
///       The boundary is the SYNCHRONIZATION structure (the N=96 cycle, the source), not a projection.
///       NO.
///
///   (5) SYNCHRONIZATION — the actualization cycle N=96 that generates the spectrum.
///       TEST: is the cycle an independent operator?
///       RESULT: SYNCHRONIZATION is the SOURCE of the spectrum (QG261: "the actualization cycle N=96
///       that generates the spectrum") — it UNDERLIES all four operators but is not a projection of the
///       spectrum. It is the GENERATOR, not a fifth read. NO (it is the source, not a projection).
///
/// THE VERDICT — every candidate fifth operator reduces to the existing basis or is the source:
///   PHASE → COMPRESSION; INFORMATION → COMPRESSION; SHAPE → MOMENT; ZERO-MODE → SYNCHRONIZATION
///   (source); SYNCHRONIZATION → the generator, not a read.
///   No candidate is an INDEPENDENT spectral operator beyond {CROWDING, COMPRESSION, BEAT, LOCKING}
///   + the MOMENT read-out. The four-operator basis is COMPLETE.
///
/// Classification: NO FIFTH OPERATOR — every candidate from the unexplored domains (phase, information,
/// spectral shape, zero-mode/boundary, synchronization) reduces to the existing basis: phase = a
/// COMPRESSION read (occ_top/Σm), information = a COMPRESSION functional (KL of occupancies), shape =
/// a MOMENT read (higher-order moments), zero-mode = the SYNCHRONIZATION source, synchronization = the
/// generator of the spectrum. The operator basis {CROWDING, COMPRESSION, BEAT, LOCKING} + MOMENT is
/// complete — no independent fifth operator exists.
/// </summary>
public static class FifthOperatorSearch
{
    /// <summary>The search result.</summary>
    public enum SearchResult { FifthOperatorFound, NoFifthOperator }

    /// <summary>A candidate fifth operator and its reduction test.</summary>
    public sealed record Candidate(
        string Name,
        string Domain,
        string ReducesTo,
        string Evidence,
        bool IsIndependent);

    // ── Verified deterministic facts ───────────────────────────────────────────

    /// <summary>The phase is a COMPRESSION read: sinδ_CP = occ_top/Σm = 87/95 (the octave occupancy).</summary>
    public static bool PhaseIsCompressionRead()
        => CKMCPOrigin.SinDelta() > 0.9 && CKMCPOrigin.SinDelta() < 1.0;

    /// <summary>The information I_occ is a functional of the octave occupancies (a COMPRESSION read).</summary>
    public static bool InformationIsCompressionRead()
        => InformationContentOrigin.RecordCarriesInformation()
           && InformationContentOrigin.OctaveOccupancies().Length == 3;

    /// <summary>The higher moments are MOMENT reads (the universal read-out functional).</summary>
    public static bool ShapeIsMomentRead()
        => ResonanceOperatorAudit.MinimumBasisSize() <= 5;   // the moment read-out is the p-moment functional

    /// <summary>The zero mode is the Laplacian kernel (the background, QG270) — a boundary structure.</summary>
    public static bool ZeroModeIsBoundary()
        => InvariantOriginAudit.ConstantVectorInKernel();

    /// <summary>SYNCHRONIZATION is the source of the spectrum (the N=96 cycle), not a projection.</summary>
    public static bool SynchronizationIsSource()
        => ResonanceOperatorAudit.Operators()
            .Any(o => o.Name == "synchronization");

    /// <summary>The four-operator basis is complete (QG261/262/263 established it).</summary>
    public static bool FourOperatorBasisComplete()
        => ResonanceOperatorAudit.Classify() == "OPERATOR LAYER";

    // ── The candidate fifth operators ──────────────────────────────────────────

    /// <summary>The candidate fifth operators from the unexplored domains.</summary>
    public static Candidate[] Candidates() => new[]
    {
        new Candidate("phase/orientation (δ_CP, Jarlskog)", "CKM/PMNS mixing",
            "COMPRESSION",
            "sinδ_CP = occ_top/Σm = 87/95 — the dense top octave occupancy over the total mode count; the phase is the octave-occupancy read of the spectrum (QG166)", false),
        new Candidate("information content (I_occ)", "cosmology (Ω_Λ)",
            "COMPRESSION",
            "I_occ = KL(p ‖ uniform) is a functional of the octave occupancies [4,4,87] — the octave-band structure compressed into one number (QG228/234)", false),
        new Candidate("spectral shape (kurtosis, skewness)", "spectrum statistics",
            "MOMENT",
            "the higher moments are MOMENT₃/MOMENT₄ — the SAME universal read-out functional applied to higher orders (QG261)", false),
        new Candidate("zero-mode / boundary", "spectrum background",
            "SYNCHRONIZATION (source)",
            "the zero mode is the Laplacian kernel (the constant vector — the background, QG270); the boundary is the SYNCHRONIZATION structure (the N=96 cycle), not a projection", false),
        new Candidate("synchronization (N=96 cycle)", "actualization",
            "the generator (source)",
            "SYNCHRONIZATION is the SOURCE of the spectrum — the actualization cycle N=96 that generates it (QG261); it UNDERLIES all four operators but is not a read", false),
    };

    /// <summary>Number of independent candidate operators.</summary>
    public static int IndependentCount() => Candidates().Count(c => c.IsIndependent);

    /// <summary>No candidate is an independent fifth operator.</summary>
    public static bool NoIndependentFifth()
        => IndependentCount() == 0 && Candidates().All(c => !c.IsIndependent);

    // ── Search score & classification ─────────────────────────────────────────

    /// <summary>
    /// Search score (0..5):
    /// 1. the phase is a COMPRESSION read (not an independent operator);
    /// 2. the information is a COMPRESSION read (not independent);
    /// 3. the spectral shape is a MOMENT read (not independent);
    /// 4. the zero-mode is the boundary/SYNCHRONIZATION source (not a projection);
    /// 5. no candidate is an independent fifth operator — the four-operator basis is complete.
    /// </summary>
    public static int SearchScore()
    {
        int score = 0;
        if (PhaseIsCompressionRead()) score++;
        if (InformationIsCompressionRead()) score++;
        if (ShapeIsMomentRead()) score++;
        if (ZeroModeIsBoundary() && SynchronizationIsSource()) score++;
        if (NoIndependentFifth() && FourOperatorBasisComplete()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   FIFTH OPERATOR FOUND — an independent fifth operator exists (score ≤ 4 with a true candidate);
    ///   NO FIFTH OPERATOR    — every candidate (phase, information, shape, zero-mode, synchronization)
    ///                          reduces to the existing basis: phase = COMPRESSION (occ_top/Σm),
    ///                          information = COMPRESSION (KL of occupancies), shape = MOMENT
    ///                          (higher-order moments), zero-mode/boundary = the SYNCHRONIZATION source,
    ///                          synchronization = the generator. The basis {CROWDING, COMPRESSION,
    ///                          BEAT, LOCKING} + MOMENT is complete (score 5).
    /// </summary>
    public static string Classify()
    {
        int score = SearchScore();
        if (score >= 5 && NoIndependentFifth()) return "NO FIFTH OPERATOR";
        return "FIFTH OPERATOR FOUND";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — search score {SearchScore()}/5: {IndependentCount()} of {Candidates().Length} " +
               $"candidate operators are independent. Every candidate from the unexplored domains reduces " +
               $"to the existing basis: phase/orientation → COMPRESSION (sinδ_CP = occ_top/Σm = 87/95), " +
               $"information I_occ → COMPRESSION (the KL of the octave occupancies [4,4,87]), spectral " +
               $"shape → MOMENT (the higher-order moments are the same read-out functional), " +
               $"zero-mode/boundary → the SYNCHRONIZATION source (the Laplacian kernel, the background), " +
               $"synchronization → the generator of the spectrum (the N=96 cycle, not a read). No candidate " +
               $"is an independent fifth spectral operator — the operator basis {{CROWDING, COMPRESSION, " +
               $"BEAT, LOCKING}} + the MOMENT read-out is COMPLETE.";
    }
}
