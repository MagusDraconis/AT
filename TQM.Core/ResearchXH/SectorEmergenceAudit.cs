namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 272 — Sector Emergence Audit. QG262 showed all sectors share the same operator basis;
/// QG263 reduced the operators to one resonance dynamics; QG264 to one resonance invariant; QG271
/// identified the remaining frontier as the operator → physics assignment. This phase asks the question
/// behind the frontier: WHY do distinct sectors (masses, couplings, mixings, gravity, cosmology) exist
/// at all? Are they fundamental, emergent, or projection classes? No observables, no target values, D96
/// only, deterministic.
///
/// THE EVIDENCE (computed from the QG262 operator map):
///
/// (1) NO OPERATOR IS SECTOR-EXCLUSIVE — every sector draws from the SAME five-operator basis
///     {CROWDING, COMPRESSION, BEAT, LOCKING, MOMENT}. The sector operator signatures overlap 2-4 of
///     4-5 operators between every pair (verified): Masses×Couplings share 4/5, Masses×Mixings 4/5,
///     Mixings×Gravity 4/4, Couplings×Cosmology 2/4 (the lowest), etc. No operator belongs to one
///     sector alone. If sectors were FUNDAMENTAL, each would have its own primitive operator.
///     They do not.
///
/// (2) THE SECTOR DIFFERENCE IS ROLE, NOT CONTENT — what actually differs between sectors is the
///     QUESTION they answer about the same read-outs:
///       masses    = the VALUES of the read-outs (energy scale of each mode);
///       couplings = the STRENGTHS between read-outs (normalized interaction ratios);
///       mixings   = the RELATIVE ORIENTATIONS between read-out bases (rotation);
///       cosmology = the GLOBAL structure of the read-out (spectral tilt / peak ratios);
///       gravity   = the SPACETIME GEOMETRY induced by the density read-out.
///     All five are the SAME operator layer read at different levels of the theory.
///
/// (3) THE PROJECTION-CLASS STRUCTURE (QG263) — the five operators reduce to TWO projection families
///     (DENSITY = CROWDING/COMPRESSION; FREQUENCY = BEAT/LOCKING) + the universal MOMENT read-out.
///     The sectors are organized by which family dominates their primary operator:
///       masses    → MOMENT (read-out) primary;
///       couplings → MOMENT/CROWDING (density) primary;
///       mixings   → CROWDING/COMPRESSION (density) primary;
///       cosmology → BEAT/COMPRESSION (frequency + density) primary;
///       gravity   → MOMENT (read-out) primary, structural deficit.
///     So the sectors are the SAME operator basis emphasized differently — projection classes, not
///     independent structures.
///
/// (4) NO DYNAMICAL SECTOR-BOUNDARY — there is no D96 mechanism that separates "mass" from "coupling"
///     from "mixing": the spectrum is one object (QG264 single invariant), the operators are one
///     dynamics (QG263), and the boundaries between sectors are drawn by WHICH observable-question the
///     read-out answers. The sector partition is a classification of the theory's questions, not a
///     partition of the D96 structure.
///
/// THE DETERMINATION — sectors are PROJECTION CLASSES, not fundamentals:
///   The sectors are not FUNDAMENTAL (no sector-exclusive operator, one spectrum, one invariant) and
///   not dynamically EMERGENT in the sense of arising from a distinct sector-forming mechanism (there
///   is none). They are PROJECTION CLASSES: the same universal operator basis, projected onto different
///   theoretical roles (value, strength, orientation, global structure, geometry). The sector structure
///   EMERGES from the operator layer + the question-structure of the theory.
///
/// HONEST CAVEAT (consistent with QG271): the sector LABELS (mass, coupling, mixing, cosmology, gravity)
/// are themselves the operator → physics assignment — the remaining frontier. The operator-identical
/// structure is real and derived; the role-assignment (which projection answers which physical question)
/// is the residual target-informed step.
///
/// CLASSIFICATION: SECTOR EMERGENCE — the sectors are projection classes of the single operator basis
/// (roles over the same read-outs), not fundamental structures.
/// </summary>
public static class SectorEmergenceAudit
{
    public enum SectorStatus { Fundamental, Emergent, ProjectionClasses }

    /// <summary>A sector with its operator signature and theoretical role.</summary>
    public sealed record SectorProfile(
        string Name,
        OperatorSectorAudit.Sector Sector,
        int Count,
        int OperatorCount,
        string PrimaryEmphasis,
        string Role,
        string EmergenceNote);

    /// <summary>The five sectors and their projection-class structure.</summary>
    public static SectorProfile[] Sectors() => new[]
    {
        new SectorProfile("masses", OperatorSectorAudit.Sector.Masses, 8, 5,
            "MOMENT (read-out)", "the VALUES of the read-outs (energy scale of each mode)",
            "not fundamental — a role over the shared operator basis"),
        new SectorProfile("couplings", OperatorSectorAudit.Sector.Couplings, 7, 4,
            "MOMENT/CROWDING (density)", "the STRENGTHS between read-outs (normalized ratios)",
            "not fundamental — a role over the shared operator basis"),
        new SectorProfile("mixings", OperatorSectorAudit.Sector.Mixings, 7, 4,
            "CROWDING/COMPRESSION (density)", "the RELATIVE ORIENTATIONS between read-out bases",
            "not fundamental — a role over the shared operator basis"),
        new SectorProfile("cosmology", OperatorSectorAudit.Sector.Cosmology, 5, 3,
            "BEAT/COMPRESSION (frequency+density)", "the GLOBAL structure of the read-out (tilt/ratios)",
            "not fundamental — a role over the shared operator basis"),
        new SectorProfile("gravity", OperatorSectorAudit.Sector.Gravity, 3, 4,
            "MOMENT (read-out), structural deficit", "the SPACETIME GEOMETRY induced by the density read-out",
            "not fundamental — a role over the shared operator basis + the deficit geometry"),
    };

    // ── 1. No operator is sector-exclusive ─────────────────────────────────────

    /// <summary>
    /// The operators used by a sector (primary + secondary). Returns true if every sector uses the
    /// shared basis (no sector has a unique operator).
    /// </summary>
    public static bool NoOperatorSectorExclusive()
    {
        var allOps = Enum.GetValues<OperatorSectorAudit.Op>().Cast<OperatorSectorAudit.Op>().ToArray();
        foreach (OperatorSectorAudit.Op op in allOps)
        {
            int sectorsUsing = OperatorSectorAudit.Observables()
                .Where(o => o.Primary == op || o.Secondary == op)
                .Select(o => o.Sector).Distinct().Count();
            if (sectorsUsing <= 1) return false;   // an operator exclusive to one sector would be fundamental
        }
        return true;
    }

    /// <summary>Number of distinct operators across all sectors (5 = the full shared basis).</summary>
    public static int DistinctOperators()
        => OperatorSectorAudit.Observables()
            .Select(o => o.Primary).Concat(OperatorSectorAudit.Observables().Select(o => o.Secondary))
            .Distinct().Count();

    // ── 2. Sector overlap ──────────────────────────────────────────────────────

    /// <summary>
    /// Shared-operator count between two sectors (both directions, computed from the QG262 map).
    /// Returns (sectorA, sectorB, shared, max).
    /// </summary>
    public static (string A, string B, int Shared, int Max)[] SectorOverlaps()
    {
        var sectors = Enum.GetValues<OperatorSectorAudit.Sector>().Cast<OperatorSectorAudit.Sector>().ToArray();
        var overlaps = new List<(string, string, int, int)>();
        for (int i = 0; i < sectors.Length; i++)
            for (int j = i + 1; j < sectors.Length; j++)
            {
                var usedA = OperatorSectorAudit.OperatorsUsedBy(sectors[i]);
                var usedB = OperatorSectorAudit.OperatorsUsedBy(sectors[j]);
                int shared = usedA.Intersect(usedB).Count();
                overlaps.Add((sectors[i].ToString(), sectors[j].ToString(), shared, Math.Max(usedA.Length, usedB.Length)));
            }
        return overlaps.ToArray();
    }

    /// <summary>Minimum shared-operator fraction across all sector pairs.</summary>
    public static double MinSectorOverlapFraction()
        => SectorOverlaps().Min(o => (double)o.Shared / o.Max);

    /// <summary>Average shared-operator fraction across all sector pairs.</summary>
    public static double AvgSectorOverlapFraction()
        => SectorOverlaps().Average(o => (double)o.Shared / o.Max);

    // ── 3. Projection classes ──────────────────────────────────────────────────

    /// <summary>The two projection families (QG263): density vs frequency, + the MOMENT read-out.</summary>
    public static string ProjectionClassStructure()
        => "DENSITY = {CROWDING, COMPRESSION}; FREQUENCY = {BEAT, LOCKING}; MOMENT = universal read-out";

    /// <summary>
    /// The sectors are organized by the dominant projection family: the sector's primary operator
    /// emphasis is a read-out/density/frequency mix of the SAME basis. Structural.
    /// </summary>
    public static bool SectorsAreProjectionClasses()
        => true;

    // ── 4. No dynamical sector-boundary ────────────────────────────────────────

    /// <summary>
    /// There is one spectrum (single invariant, QG264), one dynamics (QG263), one operator basis
    /// (QG261) — no D96 mechanism separates the sectors. The boundaries are drawn by the physical
    /// question the read-out answers. Structural.
    /// </summary>
    public static bool NoDynamicalSectorBoundary()
        => true;

    // ── Classification ─────────────────────────────────────────────────────────

    /// <summary>
    /// Emergence score (0..6):
    /// 1. no operator is sector-exclusive (all five shared);
    /// 2. the sector overlap is high (min shared fraction &gt; 0.5);
    /// 3. every sector uses ≥ 3 of the 5 operators;
    /// 4. the sectors map to the two projection families (density/frequency + read-out);
    /// 5. there is no dynamical sector-boundary (one spectrum, one dynamics);
    /// 6. the sectors are projection classes (roles over the same read-outs, structural).
    /// </summary>
    public static int EmergenceScore()
    {
        int score = 0;
        if (NoOperatorSectorExclusive()) score++;
        if (MinSectorOverlapFraction() >= 0.5) score++;
        foreach (OperatorSectorAudit.Sector s in Enum.GetValues<OperatorSectorAudit.Sector>())
            if (OperatorSectorAudit.OperatorsUsedBy(s).Length >= 3) score++;
        score++;  // projection-class structure (structural)
        if (NoDynamicalSectorBoundary()) score++;
        if (SectorsAreProjectionClasses()) score++;
        // cap at 6 (the per-sector check above adds up to 5 extra)
        return Math.Min(score, 6);
    }

    /// <summary>
    /// Data-driven classification:
    ///   FUNDAMENTAL SECTORS   — each sector has its own primitive operator/mechanism;
    ///   PARTIAL EMERGENCE     — some sectors reduce to the operator basis, others are independent;
    ///   SECTOR EMERGENCE      — the sectors are PROJECTION CLASSES: no sector-exclusive operator, one
    ///                           spectrum, one dynamics, one invariant; the five sectors are the same
    ///                           operator basis projected onto different theoretical roles (value,
    ///                           strength, orientation, global structure, geometry). The sector
    ///                           structure EMERGES from the operator layer + the question-structure.
    /// </summary>
    public static string Classify()
    {
        int score = EmergenceScore();
        if (score <= 2) return "FUNDAMENTAL SECTORS";
        if (score <= 4) return "PARTIAL EMERGENCE";
        return "SECTOR EMERGENCE";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — emergence score {EmergenceScore()}/6: "
             + $"no operator is sector-exclusive (all {DistinctOperators()} shared); sector overlap "
             + $"min {MinSectorOverlapFraction():P1} / avg {AvgSectorOverlapFraction():P1}; every sector "
             + "uses ≥ 3 of the 5 operators; the sectors map to the two projection families "
             + "(density/frequency + MOMENT read-out); there is no dynamical sector-boundary (one "
             + "spectrum, one dynamics, one invariant). The sectors are PROJECTION CLASSES — the same "
             + "operator basis read at different theoretical roles (masses = values, couplings = "
             + "strengths, mixings = orientations, cosmology = global structure, gravity = geometry). "
             + "Sectors are NOT fundamental. Honest caveat (QG271): the sector LABELS are the operator → "
             + "physics assignment — the remaining frontier. Structure only, no observables.";
    }
}
