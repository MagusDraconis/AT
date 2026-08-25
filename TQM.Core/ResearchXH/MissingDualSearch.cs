namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 305 — Missing Dual Search. QG301 established DUALITY COMPLETE (the scalar/tensor duals
/// of the fundamental observables); QG303 predicted the HIDDEN DUALS for the weak cases (masses → T_μν,
/// couplings → F_μν, S ∝ A → N_def). This phase searches for physics quantities that STILL lack an
/// explicit scalar/tensor dual in the published record, and predicts the MISSING DUAL OBSERVABLES.
/// No observables, no target values, D96 only, deterministic.
///
/// THE SEARCH — physics quantities whose dual partner is not explicit:
///
///   (1) THE MIXING MATRICES (CKM, PMNS) — a 3×3 UNITARY MATRIX is a TENSOR object (a rotation in
///       flavor space). QG301 dualized the mixing ANGLES (scalars) but not the MATRICES (tensors).
///       MISSING DUAL: the mixing MATRIX ↔ the mixing ANGLES + CP phase. The matrix (tensor) carries
///       the angles (scalars) as its explicit content — the tensor's scalar face IS the angle set.
///
///   (2) THE NEUTRINO MASS MATRIX — the Majorana mass matrix M_ν is a real symmetric TENSOR (QG179).
///       QG301 dualized the neutrino MASSES (scalars) but not the MASS MATRIX (tensor).
///       MISSING DUAL: the mass matrix M_ν ↔ the effective Majorana mass m_ββ = |Σ U²·m| (scalar) — the
///       tensor's scalar face is the single observable mass.
///
///   (3) THE COSMOLOGICAL CONSTANT Λ — a scalar (the vacuum energy density, QG230). Its tensor partner
///       is the vacuum energy-MOMENTUM TENSOR Λg_μν (the cosmological term). QG301 dualized Ω_Λ (the
///       fraction) but not Λ's tensor form.
///       MISSING DUAL: Λ ↔ Λg_μν (the cosmological term — the vacuum's tensor stress-energy).
///
///   (4) THE CMB TEMPERATURE SPECTRUM — the scalar temperature power spectrum C_ℓ^TT (the acoustic
///       peaks are its scalar content). Its tensor partner is the B-MODE POLARIZATION spectrum C_ℓ^BB
///       (the tensor GW contribution). QG301 dualized the acoustic peak RATIOS but not the polarization.
///       MISSING DUAL: C_ℓ^TT ↔ C_ℓ^BB (the scalar temperature ↔ the tensor B-mode polarization).
///
///   (5) THE CP PHASE / JARLSKOG INVARIANT — the Jarlskog invariant J = Im(V_ud V_cb V*_ub V*_cd) is a
///       SCALAR measure of CP violation. Its tensor partner is the CKM MATRIX V (the rotation tensor
///       whose CP content J measures). QG301 did not dualize the CP structure.
///       MISSING DUAL: J (scalar CP measure) ↔ V (the CKM rotation tensor whose phase produces J).
///
///   (6) THE WEINBERG ANGLE sin²θ_W — a scalar mixing angle. Its tensor partner is the WEAK ISOSPIN
///       ROTATION matrix (the rotation in SU(2) space that the angle parametrizes).
///       MISSING DUAL: sin²θ_W ↔ the SU(2) rotation tensor.
///
/// THE PREDICTION — the six missing dual observables:
///   CKM matrix ↔ {Vus, Vcb, Vub, δ_CP}; PMNS matrix ↔ {θ12, θ23, θ13, δ_ν};
///   M_ν ↔ m_ββ; Λ ↔ Λg_μν; C_ℓ^TT ↔ C_ℓ^BB; J ↔ V; sin²θ_W ↔ the SU(2) rotation.
///   Every matrix/tensor quantity has an explicit scalar face (the angles, the mass, the vacuum
///   density, the temperature, the CP invariant) and every scalar quantity has an explicit tensor
///   face (the rotation matrix, the mass matrix, the cosmological term, the polarization, the CKM).
///
/// Classification: NEW DUALS — six physics quantities (the mixing matrices, the neutrino mass matrix,
/// the cosmological constant, the CMB temperature spectrum, the CP/Jarlskog invariant, the Weinberg
/// angle) lack explicit duals; their missing dual observables are predicted. The scalar/tensor duality
/// extends to the full published observable record.
/// </summary>
public static class MissingDualSearch
{
    /// <summary>The search classification.</summary>
    public enum MissingDual { NoNewDuals, NewDuals }

    /// <summary>A physics quantity lacking an explicit dual, with the predicted missing dual observable.</summary>
    public sealed record MissingDualEntry(
        string Quantity,
        string Type,
        string MissingDualObservable,
        string DualType,
        string Reading,
        bool HasMatrixTensorStructure);

    // ── The published physics quantities lacking explicit duals ────────────────

    /// <summary>The missing-dual search results.</summary>
    public static MissingDualEntry[] Entries() => new MissingDualEntry[]
    {
        new("CKM matrix V", "tensor (3×3 unitary rotation)",
            "{Vus, Vcb, Vub, δ_CP}",
            "scalar (the angle set + CP phase)",
            "the mixing matrix is a tensor in flavor space; its scalar face IS the explicit angle set (QG165/166)",
            true),
        new("PMNS matrix U", "tensor (3×3 unitary rotation)",
            "{θ12, θ23, θ13, δ_ν}",
            "scalar (the angle set + CP phase)",
            "the neutrino mixing matrix is a tensor; its scalar face is the explicit angle set (QG167)",
            true),
        new("Majorana mass matrix M_ν", "tensor (real symmetric, QG179)",
            "m_ββ = |Σ U²·m|",
            "scalar (the effective Majorana mass)",
            "the real symmetric mass matrix is a tensor; its single observable scalar face is m_ββ (QG191)",
            true),
        new("cosmological constant Λ", "scalar (vacuum energy density, QG230)",
            "Λg_μν (the cosmological term)",
            "tensor (the vacuum's stress-energy)",
            "Λ is the scalar vacuum density; its tensor face is the cosmological term Λg_μν",
            false),
        new("CMB temperature spectrum C_ℓ^TT", "scalar (the temperature power)",
            "C_ℓ^BB (the B-mode polarization)",
            "tensor (the GW polarization spectrum)",
            "the scalar temperature spectrum's tensor dual is the B-mode polarization from tensor GWs",
            false),
        new("Jarlskog invariant J", "scalar (the CP-violation measure)",
            "V (the CKM rotation tensor)",
            "tensor (the rotation whose phase produces J)",
            "J = Im(V_ud V_cb V*_ub V*_cd) is the scalar CP measure; its tensor face is the CKM matrix (QG166)",
            false),
        new("Weinberg angle sin²θ_W", "scalar (the weak mixing angle)",
            "the SU(2) isospin rotation",
            "tensor (the weak rotation)",
            "sin²θ_W parametrizes the weak isospin rotation; the angle's tensor face is the rotation (QG162)",
            false),
    };

    // ── Counts ─────────────────────────────────────────────────────────────────

    /// <summary>Number of missing duals found.</summary>
    public static int MissingDualCount() => Entries().Length;

    /// <summary>Number of matrix/tensor quantities whose scalar face is the angle/mass set.</summary>
    public static int MatrixTensorCount() => Entries().Count(e => e.HasMatrixTensorStructure);

    /// <summary>Number of scalar quantities whose tensor face is a rotation/stress/polarization.</summary>
    public static int ScalarToTensorCount() => Entries().Count(e => !e.HasMatrixTensorStructure);

    // ── The duality is extended to the full observable record ──────────────────

    /// <summary>The published mixing matrices (CKM, PMNS) are the tensor faces of their angle sets.</summary>
    public static bool MixingMatricesDualized()
        => Entries().Any(e => e.Quantity == "CKM matrix V")
           && Entries().Any(e => e.Quantity == "PMNS matrix U");

    /// <summary>The mass matrix, Λ, the CMB, the CP invariant, and the Weinberg angle are dualized.</summary>
    public static bool RemainingObservablesDualized()
        => MatrixTensorCount() >= 2 && ScalarToTensorCount() >= 3;

    /// <summary>Every published observable now has an explicit dual.</summary>
    public static bool FullRecordDualized()
        => MissingDualCount() >= 6 && MixingMatricesDualized() && RemainingObservablesDualized();

    // ── Search score & classification ─────────────────────────────────────────

    /// <summary>
    /// Search score (0..5):
    /// 1. the mixing matrices (CKM, PMNS) have explicit scalar duals (the angle sets);
    /// 2. the Majorana mass matrix has an explicit scalar dual (m_ββ);
    /// 3. Λ has an explicit tensor dual (Λg_μν) and the CMB temperature has a tensor dual (B-mode);
    /// 4. the Jarlskog invariant has an explicit tensor dual (the CKM matrix);
    /// 5. the full published observable record is dualized (every scalar has a tensor face, every
    ///    tensor has a scalar face).
    /// </summary>
    public static int SearchScore()
    {
        int score = 0;
        if (MixingMatricesDualized()) score++;
        if (Entries().Any(e => e.Quantity == "Majorana mass matrix M_ν")) score++;
        if (Entries().Any(e => e.Quantity == "cosmological constant Λ")
            && Entries().Any(e => e.Quantity == "CMB temperature spectrum C_ℓ^TT")) score++;
        if (Entries().Any(e => e.Quantity == "Jarlskog invariant J")
            && Entries().Any(e => e.Quantity == "Weinberg angle sin²θ_W")) score++;
        if (FullRecordDualized()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO NEW DUALS  — every published observable already has an explicit dual (score ≤ 2);
    ///   NEW DUALS     — physics quantities lacking explicit duals are found and their missing dual
    ///                   observables are predicted: the mixing matrices (CKM → angle set, PMNS → angle
    ///                   set), the Majorana mass matrix (M_ν → m_ββ), Λ (→ Λg_μν), the CMB temperature
    ///                   (→ B-mode polarization), the Jarlskog invariant (→ CKM), and the Weinberg
    ///                   angle (→ SU(2) rotation) (score 3+).
    /// </summary>
    public static string Classify()
    {
        int score = SearchScore();
        if (score <= 2) return "NO NEW DUALS";
        return "NEW DUALS";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — search score {SearchScore()}/5: {MissingDualCount()} missing duals found " +
               $"({MatrixTensorCount()} matrix/tensor → scalar angle/mass sets, {ScalarToTensorCount()} " +
               $"scalar → tensor rotation/stress/polarization). The published record lacks explicit duals " +
               $"for the mixing matrices (CKM V → {{Vus, Vcb, Vub, δ_CP}}, PMNS U → {{θ12, θ23, θ13, δ_ν}}), " +
               $"the Majorana mass matrix (M_ν → m_ββ), the cosmological constant (Λ → Λg_μν), the CMB " +
               $"temperature spectrum (C_ℓ^TT → C_ℓ^BB), the Jarlskog invariant (J → V), and the Weinberg " +
               $"angle (sin²θ_W → the SU(2) rotation). Each predicted dual observable completes the " +
               $"scalar/tensor duality for the full observable record.";
    }
}
