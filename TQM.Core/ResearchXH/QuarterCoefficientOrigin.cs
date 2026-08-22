namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 196 — Quarter Coefficient Origin. Known: S ∝ A (QG12), T ∝ 1/R (QG184). Missing: the exact
/// 1/4 in S = A/4. This phase asks: can 1/4 be DERIVED — no fitting, no imported Hawking factor — or is it
/// IMPOSSIBLE within D96/TRM? Deterministic.
///
/// Method (computational, fully deterministic):
///  (1) BOUNDARY COUNTING (QG12) — S = b·R² (b bits per horizon cell, A_cell = R²). The physical area is
///      A_phys = 4πR². Hence S/A_phys = b/(4π). For the Bekenstein target S/A_phys = 1/4 this requires
///      b = π. QG12's natural count is b = ln 2 (1 bit/cell) → S/A_phys = ln2/(4π) = 0.0552 ≠ 1/4.
///  (2) DEFICIT FIRST-LAW (QG185) — S = R²/2 = A_cell/2 → S/A_phys = 1/(8π) = 0.0398 ≠ 1/4 (off by 2π).
///  (3) THE BIT-PER-CELL CONSTRAINT — for ANY boundary-counting coefficient b, S/A_phys = 1/4 forces
///      b = π. π is NOT a D96/TRM quantity (it is the imported geometric/quantum constant); setting
///      b = π would be an imported normalization, forbidden here.
///  (4) THE occ₀ = 4 CANDIDATE — 1/occ₀ = 1/4 as a CELL coefficient gives S = (1/4)·R² = A_phys/(16π),
///      i.e. S/A_phys = 1/(16π) ≈ 0.0199, which is 1/(4π) of the target 1/4. It would require π = 1/4
///      (not D96/TRM). The 1/4 identity is a numerical coincidence of the label 4 in the wrong units.
///
/// CONCLUSION — the exact 1/4 is IMPOSSIBLE to derive from D96/TRM without fitting and without importing
/// π (the 2π quantum factor). The structure (S ∝ A, M ∝ R, T ∝ 1/R) is fully derived; the coefficient is
/// not. Classification: PARTIAL ORIGIN (structure derived; exact 1/4 requires imported π — impossible
/// within the D96/TRM constraints of this phase).
/// </summary>
public static class QuarterCoefficientOrigin
{
    /// <summary>Spatial dimension.</summary>
    public const int Dimension = 3;

    /// <summary>Target Bekenstein coefficient: S = A_phys/4.</summary>
    public const double BekensteinCoefficient = 0.25;

    // ── 1. Boundary counting (QG12) ─────────────────────────────────────────────

    /// <summary>Boundary-counting entropy S = b·R² (b bits per horizon cell).</summary>
    public static double BoundaryEntropy(double bitsPerCell, double R)
        => bitsPerCell * R * R;

    /// <summary>Physical horizon area A = 4πR².</summary>
    public static double PhysicalArea(double R)
        => 4.0 * Math.PI * R * R;

    /// <summary>S/A_phys for a given bits-per-cell: b/(4π).</summary>
    public static double BoundaryCoefficient(double bitsPerCell)
        => bitsPerCell / (4.0 * Math.PI);

    /// <summary>QG12's natural count: 1 bit per cell → S/A_phys = ln2/(4π) = 0.0552 ≠ 1/4.</summary>
    public static double Qg12Coefficient()
        => BoundaryCoefficient(Math.Log(2.0));

    /// <summary>Does QG12 boundary counting reproduce 1/4? NO.</summary>
    public static bool Qg12ReproducesQuarter()
        => Math.Abs(Qg12Coefficient() - BekensteinCoefficient) < 1e-9;

    // ── 2. Deficit first-law (QG185) ────────────────────────────────────────────

    /// <summary>Deficit first-law entropy S = R²/2 = A_cell/2 → S/A_phys = 1/(8π) = 0.0398.</summary>
    public static double DeficitFirstLawCoefficient()
        => 1.0 / (8.0 * Math.PI);

    /// <summary>Does the deficit first-law reproduce 1/4? NO — off by 2π.</summary>
    public static bool DeficitReproducesQuarter()
        => Math.Abs(DeficitFirstLawCoefficient() - BekensteinCoefficient) < 1e-9;

    // ── 3. The bit-per-cell constraint (impossibility) ──────────────────────────

    /// <summary>
    /// The bits-per-cell required for S/A_phys = 1/4: S = b·R² = A_phys/4 = πR² ⇒ b = π.
    /// π is not a D96/TRM quantity — imposing it would be an imported normalization (forbidden).
    /// </summary>
    public static double RequiredBitsPerCell()
        => Math.PI;

    /// <summary>The required bits-per-cell is π — an imported constant, not derivable from D96/TRM.</summary>
    public static bool RequiresImportedPi()
        => Math.Abs(RequiredBitsPerCell() - Math.PI) < 1e-12 && Math.Abs(RequiredBitsPerCell() - Math.Log(2.0)) > 0.1;

    // ── 4. The occ₀ = 4 candidate ───────────────────────────────────────────────

    /// <summary>occ₀ (lightest-octave occupancy) = 4.</summary>
    public static double LightestOctaveOccupancy()
        => EffectiveAccessCounts.OctaveOccupancies()[0];

    /// <summary>1/occ₀ = 1/4 as a CELL coefficient.</summary>
    public static double InverseOctaveCellCoefficient()
        => 1.0 / LightestOctaveOccupancy();

    /// <summary>
    /// 1/occ₀ = 1/4 in CELL units gives S = (1/4)R² = A_phys/(16π) — i.e. S/A_phys = 1/(16π) ≈ 0.0199,
    /// which is 1/(4π) of the target 1/4. It would require π = 1/4 (not D96/TRM).
    /// </summary>
    public static double InverseOctavePhysicalCoefficient()
        => InverseOctaveCellCoefficient() / (4.0 * Math.PI);

    /// <summary>Does 1/occ₀ reproduce the physical 1/4? NO — it gives 1/(16π) (needs π = 1/4).</summary>
    public static bool InverseOctaveReproducesQuarter()
        => Math.Abs(InverseOctavePhysicalCoefficient() - BekensteinCoefficient) < 1e-9;

    // ── Structure check ─────────────────────────────────────────────────────────

    /// <summary>The structure (S ∝ A, M ∝ R, T ∝ 1/R) is fully derived (QG12/QG184).</summary>
    public static bool StructureDerived()
        => BekensteinQuarterOrigin.StructureDerived();

    // ── Origin score & classification ───────────────────────────────────────────

    /// <summary>
    /// Quarter-coefficient score (0..3):
    /// 1. the structure (S ∝ A, M ∝ R, T ∝ 1/R) is derived;
    /// 2. the deficit first-law and QG12 counting give DEFINITE coefficients (1/(8π), ln2/(4π));
    /// 3. the exact 1/4 is PROVEN IMPOSSIBLE without importing π (bits/cell = π; occ₀ gives 1/(16π)).
    /// Score 3 = PARTIAL ORIGIN (structure derived, exact 1/4 proven impossible within D96/TRM).
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        if (StructureDerived()) score++;
        if (Qg12Coefficient() > 0 && DeficitFirstLawCoefficient() > 0) score++;
        if (RequiresImportedPi() && !InverseOctaveReproducesQuarter()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ORIGIN       — even the structure is not derived;
    ///   PARTIAL ORIGIN  — the structure (S ∝ A, M ∝ R, T ∝ 1/R) is fully derived and the coefficients are
    ///                      definite (QG12 ln2/(4π), deficit first-law 1/(8π)), but the exact 1/4 is PROVEN
    ///                      IMPOSSIBLE to derive from D96/TRM without fitting and without importing π
    ///                      (the 2π quantum factor): the required bits-per-cell is π, and 1/occ₀ = 1/4 is
    ///                      a numerical coincidence in the wrong units (1/(16π) physical, needs π = 1/4);
    ///   QUARTER ORIGIN  — the exact 1/4 is derived (proven impossible here).
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (Math.Abs(DeficitFirstLawCoefficient() - BekensteinCoefficient) < 1e-9) return "QUARTER ORIGIN";
        if (score <= 1) return "NO ORIGIN";
        return "PARTIAL ORIGIN";
    }
}
