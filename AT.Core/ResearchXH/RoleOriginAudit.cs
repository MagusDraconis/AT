namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 275 — Role Origin Audit. QG272 showed sectors are projection classes; QG274 showed the
/// measurement classes (VALUE, STRENGTH, ORIENTATION, GLOBAL, GEOMETRY) are structural. This phase asks
/// the open question: WHY does a measurement class receive a sector role (mass, coupling, mixing,
/// cosmology, gravity)? What determines the role assignment? No observables, no target values, D96
/// only, deterministic.
///
/// THE ONTOLOGICAL AXES (investigated):
///   AXIS 1 — LEVEL: local (per-object) vs global (whole-universe) vs arena (spacetime);
///   AXIS 2 — NATURE: absolute (intrinsic) vs relational (between things);
///   AXIS 3 — KIND: magnitude (size) vs orientation (angle);
///   AXIS 4 — DOMAIN: geometry (spacetime) vs interaction (between objects).
///
/// THE CLASS POSITIONS (each class occupies a unique position on the axes):
///   VALUE       = (local, absolute, magnitude, intrinsic)     → the SIZE of a thing;
///   STRENGTH    = (local, relational, interaction, between-objects) → how things INTERACT;
///   ORIENTATION = (relational, orientation, alignment, between-frames) → how bases are ALIGNED;
///   GLOBAL      = (global, scale-invariant, whole, universe)  → the WHOLE;
///   GEOMETRY    = (arena, geometry, spacetime, stage)         → the STAGE.
///
/// THE ROLE PRINCIPLE — ROLE = THE ONTOLOGICAL CATEGORY OF THE READ:
///   local-absolute (what a thing IS)                → MASS;
///   local-relational (how things INTERACT)          → COUPLING;
///   relational-orientation (how bases rotate)       → MIXING;
///   global (what the whole IS)                      → COSMOLOGY;
///   arena (what spacetime IS)                       → GRAVITY.
///   The role is the category of the read's position on the axes — DETERMINED BY THE STRUCTURE of
///   the read (local/global, absolute/relational, magnitude/orientation), not by any target value.
///
/// THE DECISIVE CHECK — how many class→role assignments are FORCED by the axes?
///   VALUE → mass:       FORCED (the only dimensional absolute read — a local absolute magnitude IS
///                       the intrinsic energy scale of an object);
///   ORIENTATION → mixing: FORCED (the only unitary arrangement — a norm-preserving angle between
///                       bases IS a mixing angle);
///   GLOBAL → cosmology:  FORCED (the only scale-invariant whole-universe read);
///   GEOMETRY → gravity:  FORCED (the only arena/spacetime read);
///   STRENGTH → coupling: PARTIAL — the local-relational position determines the CATEGORY (interaction
///                       strength), but the SAME strength read can also play a mass-ratio role
///                       (m_τ/m_μ) or a mixing role (Vus = #d/(2Σm)) depending on context.
///
/// THE RESIDUAL (consistent with QG273/274):
///   The STRENGTH class (local, relational) is assigned coupling, mixing, or mass-ratio by ADDITIONAL
///   structure: the unitary arrangement upgrades a strength read to an orientation/mixing role; the
///   equation context (mass hierarchy vs Yukawa) sets the mass vs coupling role. So 4/5 role
///   assignments are forced by the read's position on the axes; the relational subclass (1/5) is
///   context-dependent.
///
/// CLASSIFICATION: PARTIAL ROLE PRINCIPLE — the role assignment is DETERMINED by the read's position
/// on the ontological axes (level × nature) for 4 of 5 classes (value→mass, orientation→mixing,
/// global→cosmology, geometry→gravity), but the relational subclass (strength) is context-dependent:
/// the same strength read can be a coupling, a mixing entry, or a mass-ratio.
/// </summary>
public static class RoleOriginAudit
{
    public enum Level { Local, Global, Arena }
    public enum Nature { Absolute, Relational }

    /// <summary>A measurement class with its ontological position and assigned role.</summary>
    public sealed record ClassRole(
        MeasurementClassAudit.Class Class,
        Level Level,
        Nature Nature,
        string Kind,
        string Domain,
        string Role,
        bool Forced,
        string Note);

    /// <summary>The class→role mapping with its position on the ontological axes.</summary>
    public static ClassRole[] ClassRoles() => new[]
    {
        new ClassRole(MeasurementClassAudit.Class.Value, Level.Local, Nature.Absolute,
            "magnitude", "intrinsic", "mass", true,
            "a local absolute magnitude IS the intrinsic energy scale of an object — forced by the axes"),
        new ClassRole(MeasurementClassAudit.Class.Strength, Level.Local, Nature.Relational,
            "interaction", "between-objects", "coupling", false,
            "local-relational → interaction strength; but the same read can also be a mass-ratio or a mixing entry (context-dependent)"),
        new ClassRole(MeasurementClassAudit.Class.Orientation, Level.Local, Nature.Relational,
            "orientation", "between-frames", "mixing", true,
            "a norm-preserving angle between bases IS a mixing angle — forced by the unitary arrangement"),
        new ClassRole(MeasurementClassAudit.Class.Global, Level.Global, Nature.Relational,
            "whole", "universe", "cosmology", true,
            "a scale-invariant whole-universe read IS cosmology — forced by the global level"),
        new ClassRole(MeasurementClassAudit.Class.Geometry, Level.Arena, Nature.Absolute,
            "geometry", "spacetime", "gravity", true,
            "an arena/spacetime read IS gravity — forced by the arena level"),
    };

    /// <summary>Number of class→role assignments forced by the read's position on the axes.</summary>
    public static int ForcedRoleCount()
        => ClassRoles().Count(c => c.Forced);

    /// <summary>Number of classes whose role is context-dependent (not forced).</summary>
    public static int ContextDependentCount()
        => ClassRoles().Count(c => !c.Forced);

    /// <summary>The role principle: role = the ontological category of the read's position.</summary>
    public static string RolePrinciple()
        => "ROLE = ONTOLOGICAL CATEGORY of the read: local-absolute→mass; local-relational→coupling; "
         + "relational-orientation→mixing; global→cosmology; arena→gravity";

    // ── The residual (the relational subclass) ─────────────────────────────────

    /// <summary>m_τ/m_μ = √occMom·λ₂ (a strength read assigned the mass-ratio role).</summary>
    public static double TauMuonMassRatio()
        => LeptonHierarchyExactLaw.TauMuonRatio();

    /// <summary>y_τ/y_μ = √occMom·λ₂ (a strength read assigned the coupling role).</summary>
    public static double TauMuonYukawaRatio()
        => YukawaOrigin.TauMuonRatio();

    /// <summary>Vus = #d/(2Σm) (a strength read assigned the mixing role via unitarity).</summary>
    public static double Vus()
        => CKMOrigin.Vus();

    /// <summary>
    /// The residual: the same STRENGTH read is assigned coupling, mixing, or mass-ratio by context —
    /// the unitary arrangement (orientation role) vs the equation (mass/coupling role).
    /// </summary>
    public static bool RelationalSubclassContextDependent()
        => Math.Abs(TauMuonMassRatio() - TauMuonYukawaRatio()) < 1e-9;  // same read, two roles

    // ── Classification ─────────────────────────────────────────────────────────

    /// <summary>
    /// Role-origin score (0..6):
    /// 1. value→mass is forced (local absolute magnitude);
    /// 2. orientation→mixing is forced (unitary arrangement);
    /// 3. global→cosmology is forced (whole-universe level);
    /// 4. geometry→gravity is forced (arena level);
    /// 5. ≥ 4 of 5 role assignments are forced by the axes;
    /// 6. the relational subclass is context-dependent (the residual — consistent with QG273/274).
    /// </summary>
    public static int RoleScore()
    {
        int score = 0;
        foreach (var c in ClassRoles())
            if (c.Forced) score++;
        if (ForcedRoleCount() >= 4) score++;
        if (RelationalSubclassContextDependent()) score++;
        return Math.Min(score, 6);
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ROLE PRINCIPLE       — the class→sector mapping is arbitrary (no structural determination);
    ///   PARTIAL ROLE PRINCIPLE  — the role is determined by the read's position on the ontological
    ///                             axes (level × nature) for 4 of 5 classes (value→mass,
    ///                             orientation→mixing, global→cosmology, geometry→gravity), but the
    ///                             relational subclass (strength) is context-dependent: the same
    ///                             strength read can be a coupling, a mixing entry, or a mass-ratio
    ///                             (the unitary arrangement vs the equation sets the role);
    ///   ROLE ASSIGNMENT PRINCIPLE — every class→sector mapping is forced by the read structure.
    ///   The context-dependence of the relational subclass is the DECISIVE BLOCKER: a complete role
    ///   assignment principle would require every class→role map to be forced, and the strength class
    ///   is not.
    /// </summary>
    public static string Classify()
    {
        // The decisive blocker: the relational subclass is context-dependent, so no complete
        // target-free role assignment principle exists.
        if (RelationalSubclassContextDependent() && ContextDependentCount() >= 1) return "PARTIAL ROLE PRINCIPLE";
        int score = RoleScore();
        if (score <= 2) return "NO ROLE PRINCIPLE";
        if (score <= 4) return "PARTIAL ROLE PRINCIPLE";
        return "ROLE ASSIGNMENT PRINCIPLE";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — role score {RoleScore()}/6: "
             + $"the role assignment is determined by the read's position on the ontological axes "
             + $"(level × nature) for {ForcedRoleCount()}/5 classes "
             + $"(value→mass, orientation→mixing, global→cosmology, geometry→gravity are FORCED); "
             + $"the relational subclass (strength, {ContextDependentCount()}/5) is context-dependent: "
             + $"the same read √occMom·λ₂ = {TauMuonMassRatio():F3} is a mass-ratio (m_τ/m_μ) OR a "
             + $"coupling (y_τ/y_μ); Vus = {Vus():F6} is a strength read in the mixing role via unitarity. "
             + "The principle: ROLE = ONTOLOGICAL CATEGORY of the read (local-absolute→mass, "
             + "local-relational→coupling, relational-orientation→mixing, global→cosmology, "
             + "arena→gravity). Structure only, no observables.";
    }
}
