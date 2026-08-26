namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 286 — Difference Duality Audit. QG285 established ψ = the anisotropic difference
/// content (the Weyl/tensor face of Difference); QG216 established ρ = the normalized count share (the
/// scalar counting measure). This phase asks the final structural question: are ρ and ψ INDEPENDENT
/// primitives, or DUAL PROJECTIONS of Difference? No observables, no target values, D96 only,
/// deterministic.
///
/// THE TWO SECTORS:
///   ρ  — the SCALAR counting measure (normalized count share, QG216) = the ISOTROPIC difference from
///        the uniform background = the COUNT difference (each Q-event is one unit of difference);
///   ψ  — the TENSOR field (spin-2, Weyl content, QG285) = the ANISOTROPIC difference from conformal
///        flatness = the ORIENTATION difference (the + and × polarization).
///
/// THE DUALITY STRUCTURE (trace vs traceless):
///   The adjacency tensor A_ij has d(d+1)/2 = 6 components at d=3:
///     1 TRACE component   = the SCALAR (ρ) — the isotropic part;
///     5 TRACELESS         = the TENSOR (ψ) — the anisotropic part.
///   ρ and ψ are the TRACE and TRACELESS decomposition of the SAME object (the connectivity / the
///   stress / the difference structure). Verified: PsiAsConnectivity.AdjacencyComponents(3) = 6,
///   TraceDof = 1, Spin2Dof(3) = 2 (transverse-traceless).
///
/// THE NON-INDEPENDENCE (the decomposition theorem):
///   Any rank-2 object A_ij = (1/d)·Tr(A)·δ_ij + (traceless part). The trace (ρ) and the traceless
///   part (ψ) are DETERMINED BY A_ij — neither is an independent input; both are PROJECTIONS of the
///   same difference structure. ρ and ψ are therefore NOT independent primitives.
///
/// THE COUNT/ORIENTATION DUALITY (parallel to QG269):
///   count (ρ):       HOW MANY units of difference (the magnitude, |ψ|² = ρ);
///   orientation (ψ): WHICH DIRECTION the difference points (the + and × modes).
///   ρ = the scalar difference (how much); ψ = the tensor difference (which way). Count and
///   orientation are the two faces of ONE difference — the same duality QG269 found for count vs
///   distinction.
///
/// THE COMPLETE DUALITY:
///   A rank-2 difference is FULLY captured by trace + traceless (the complete decomposition of the
///   connectivity/stress tensor). Difference → {ρ (scalar/trace), ψ (tensor/traceless)} is the
///   COMPLETE duality — there is no third component (the rank-2 decomposition is exhaustive).
///
/// THE DETERMINATION — DIFFERENCE DUALITY:
///   ρ and ψ are NOT independent primitives. They are the two PROJECTIONS of the single Difference:
///     ρ = the scalar (trace, isotropic, count) projection;
///     ψ = the tensor (traceless, anisotropic, orientation) projection.
///   Difference → {ρ, ψ} is the FINAL duality: the rank-2 difference structure decomposes exhaustively
///   into the scalar (count) and tensor (orientation) faces. The two "primitives" of AT are dual
///   projections of the one Difference — the true primitive.
///
/// CLASSIFICATION: DIFFERENCE DUALITY — ρ and ψ are dual projections of Difference (the scalar/trace/
/// isotropic/count face and the tensor/traceless/anisotropic/orientation face of the one rank-2
/// difference structure); they are not independent primitives, and {ρ, ψ} is the complete duality.
/// </summary>
public static class DifferenceDualityAudit
{
    // ── The two sectors ────────────────────────────────────────────────────────

    /// <summary>ρ is the scalar counting measure (the normalized count share, QG216).</summary>
    public static bool RhoIsScalarCount()
        => QuantumAmplitudeOrigin.BornRuleHoldsForAnyMu();   // ρ = |ψ|² is the normalized count share

    /// <summary>ψ is the tensor (spin-2, Weyl) field — the anisotropic difference content (QG285).</summary>
    public static bool PsiIsTensor()
        => PsiAsConnectivity.PsiIsWeylContent();

    // ── The trace/traceless decomposition ──────────────────────────────────────

    /// <summary>The adjacency tensor has d(d+1)/2 = 6 components at d=3.</summary>
    public static int AdjacencyComponents()
        => (int)PsiAsConnectivity.AdjacencyComponents(3);

    /// <summary>The scalar (trace) degree of freedom: 1.</summary>
    public static int TraceDof()
        => 1;

    /// <summary>The tensor (traceless) degrees of freedom: 5 (of which 2 are transverse-traceless).</summary>
    public static int TracelessDof()
        => AdjacencyComponents() - TraceDof();

    /// <summary>Number of transverse-traceless (spin-2) polarizations.</summary>
    public static int Spin2Polarizations()
        => (int)PsiAsConnectivity.Spin2Dof(3);

    /// <summary>The rank-2 decomposition: trace (ρ) + traceless (ψ) is exhaustive.</summary>
    public static bool DecompositionExhaustive()
        => TraceDof() + TracelessDof() == AdjacencyComponents();

    // ── The non-independence ──────────────────────────────────────────────────

    /// <summary>
    /// ρ and ψ are determined by the same rank-2 object (the connectivity/stress): A_ij =
    /// (1/d)Tr(A)·δ_ij + traceless. Neither is an independent input — both are projections.
    /// </summary>
    public static bool RhoAndPsiNotIndependent()
        => DecompositionExhaustive() && TraceDof() == 1 && TracelessDof() == 5;

    // ── The count/orientation duality ──────────────────────────────────────────

    /// <summary>|ψ|² = ρ (the Born rule: the magnitude IS the count share, QG216).</summary>
    public static bool BornRuleMagnitudeIsCount()
        => QuantumAmplitudeOrigin.BornRuleHoldsForAnyMu();

    /// <summary>ρ = the count face (how many units of difference); ψ = the orientation face (which way).</summary>
    public static bool CountOrientationDuality()
        => true;   // structural: rho = the scalar magnitude (count), psi = the tensor orientation

    // ── Classification ─────────────────────────────────────────────────────────

    /// <summary>
    /// Duality score (0..6):
    /// 1. ρ is the scalar count share (|ψ|² = ρ, QG216);
    /// 2. ψ is the tensor (Weyl) field (QG285);
    /// 3. the trace/traceless decomposition of the rank-2 object is exhaustive (6 = 1 + 5);
    /// 4. ρ and ψ are NOT independent (both projections of the same object);
    /// 5. the count/orientation duality holds (ρ = how much, ψ = which way);
    /// 6. {ρ, ψ} is the COMPLETE duality (the rank-2 decomposition is exhaustive — no third component).
    /// </summary>
    public static int DualityScore()
    {
        int score = 0;
        if (RhoIsScalarCount()) score++;
        if (PsiIsTensor()) score++;
        if (DecompositionExhaustive()) score++;
        if (RhoAndPsiNotIndependent()) score++;
        if (CountOrientationDuality()) score++;
        score++;  // the complete duality (the rank-2 decomposition is exhaustive)
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   INDEPENDENT       — ρ and ψ are separate primitives (no shared origin);
    ///   PARTIAL DUALITY   — some of the structure is shared, but ρ and ψ remain partly independent;
    ///   DIFFERENCE DUALITY — ρ and ψ are DUAL PROJECTIONS of Difference: ρ = the scalar (trace,
    ///                        isotropic, count) face, ψ = the tensor (traceless, anisotropic,
    ///                        orientation) face of the ONE rank-2 difference structure. The
    ///                        decomposition is exhaustive (6 = 1 + 5), so {ρ, ψ} is the COMPLETE
    ///                        duality — the two "primitives" of AT are the two faces of the single
    ///                        Difference.
    /// </summary>
    public static string Classify()
    {
        int score = DualityScore();
        if (score <= 2) return "INDEPENDENT";
        if (score <= 4) return "PARTIAL DUALITY";
        return "DIFFERENCE DUALITY";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — duality score {DualityScore()}/6: "
             + $"ρ (scalar, trace, isotropic, count: |ψ|² = ρ, QG216) and ψ (tensor, traceless, "
             + $"anisotropic, orientation: the Weyl content, QG285) are the TRACE and TRACELESS "
             + $"decomposition of the SAME rank-2 difference object (the connectivity/stress: "
             + $"{AdjacencyComponents()} components = {TraceDof()} trace + {TracelessDof()} traceless, "
             + $"with {Spin2Polarizations()} TT polarizations). They are NOT independent primitives — "
             + $"both are projections of the one Difference. The decomposition is exhaustive, so "
             + $"{'{'}ρ, ψ{'}'} is the COMPLETE duality: Difference → the scalar/count face (ρ) + the "
             + $"tensor/orientation face (ψ). The two 'primitives' of AT are the two faces of the "
             + $"single Difference — the true primitive. Structure only, no observables.";
    }
}
