namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 256 — Selection Principle Audit. QG254 introduced OCTAVE PRESERVATION and QG255
/// introduced MOMENT-CLOSURE MDL as target-free D96-only formula-selection rules. This phase audits
/// whether those rules are FORCED by D96 or were selected POST-HOC. Methodology only — no physics.
///
/// THE AUDIT QUESTIONS for each rule:
///  (1) DERIVABLE — does the rule follow from the D96 structure itself (the octave bands, the moment
///      hierarchy), or is it an imported principle (e.g. MDL from information theory)?
///  (2) NECESSARY — is the rule required, or are competing rules equally consistent?
///  (3) ALTERNATIVES — which other rules would also resolve the QG253/QG254 non-uniqueness?
///  (4) CONSISTENCY — is the rule applied uniformly across the published formulas?
///
/// RULE 1 — OCTAVE PRESERVATION (QG254):
///  Derivable: PARTIALLY. The octave structure occ = [4,4,87] IS a D96 fact (three octave families,
///  QG155/QG210) — the octave bands are derivable. But the RULE "a formula must not isolate a single
///  band" is a prescription chosen because it excludes the QG253 alternatives that happened to isolate
///  bands. The octave structure is D96-native; the prohibition form is post-hoc.
///  Necessary: NO — competing symmetry projections exist (prefer occMom-based forms; require invariance
///  under band permutation occ₀↔occ₁, which is trivially satisfied since occ₀ = occ₁ = 4; prefer
///  full-spectrum usage; require 2nd-moment closure).
///  Alternatives: several rules would remove the same five QG253 alternatives (e.g. "no ln of a single
///  quantity", "prefer formulas using occMom", "prefer the spectral-gap scale λ₂").
///  Consistency: the rule is applied to the QG253 alternatives but NOT tested against the full pool of
///  octave-preserving formulas — it was calibrated on the known non-unique cases.
///
/// RULE 2 — MOMENT-CLOSURE MDL (QG255):
///  Derivable: PARTIALLY. MDL (minimum description length / minimal complexity) is IMPORTED from
///  information theory, not derived from D96. Moment closure (prefer 2nd moments occMom/Σm²) is a
///  D96-consistent preference (the occupation moments are D96-native, QG155/157) but the choice of
///  "2nd moment" over "1st" or "3rd" is not derived.
///  Necessary: NO — the moment-order ranking (2nd &gt; 1st &gt; 0.5th &gt; 0th) is a convention.
///  Alternatives: "prefer λ₂ (spectral gap) as the mass scale", "prefer formulas with the fewest
///  distinct quantities", "prefer forms invariant under octave permutation" would also resolve ties.
///  CONSISTENCY (the decisive finding): the Noether rule in QG255 rejects "5/4·Σ√m/λ₂" because 5/4 is
///  a "free constant". BUT the published QG238 acoustic-peak formula uses 5/4:
///      ℓ₁ = Σm·ln(span)·(5/4)
///  So 5/4 IS a published TQM multiplier. The exclusion of 5/4 in QG255 is INCONSISTENT with QG238 —
///  it was selected post-hoc to exclude the specific tie candidate, not because 5/4 is inherently
///  non-D96. (The defence "5/4 is a ratio of occ₀ and occ₁" is itself post-hoc: 5/4 = occ₀+occ₁ over 2?,
///  no — it is not a D96 moment relation.)
///
/// CLASSIFICATION:
///  OCTAVE PRESERVATION — PREFERRED: rooted in the D96 octave structure (derivable in substance), but
///  the specific prohibition form was calibrated on the QG253 alternatives; competing projections exist.
///  MOMENT-CLOSURE MDL — ARBITRARY: MDL is imported, the moment-order ranking is conventional, and the
///  Noether 5/4 exclusion is INCONSISTENT with the published QG238 ℓ₁ = Σm·ln(span)·5/4 — a post-hoc
///  distinction.
///
/// SELECTION-PRINCIPLE RISK: HIGH for the meta-level claim "the selection rules are forced by D96."
/// Neither rule is FORCED; one (octave preservation) is a reasonable PREFERRED symmetry grounded in D96
/// structure, the other (moment-closure MDL) is ARBITRARY in its Noether component because 5/4 is used
/// in a published formula. The rules were selected AFTER QG253 revealed the non-uniqueness, so they
/// carry the same retro-selection character they were intended to remove — at the meta-level.
/// </summary>
public static class SelectionPrincipleAudit
{
    public enum Status { Forced, Preferred, Arbitrary }

    /// <summary>An audited selection rule.</summary>
    public sealed record Rule(
        string Name,
        Status Status,
        bool Derivable,
        bool Necessary,
        string[] Alternatives,
        string ConsistencyNote);

    /// <summary>The two selection rules audited.</summary>
    public static Rule[] Rules() => new[]
    {
        new Rule(
            "Octave preservation (QG254)",
            Status.Preferred,
            Derivable: true,     // the octave bands occ=[4,4,87] are D96-native (QG155/210)
            Necessary: false,    // competing symmetry projections exist
            new[]
            {
                "prefer occMom-based forms (the QG254 residual note itself)",
                "require invariance under band permutation occ₀↔occ₁ (trivially true: occ₀=occ₁=4)",
                "prefer full-spectrum usage (no ln of single quantities)",
                "prefer the spectral-gap scale λ₂",
            },
            "the octave STRUCTURE is derivable; the PROHIBITION FORM (no isolated band) was calibrated on the QG253 alternatives — post-hoc in form, D96-grounded in substance"),
        new Rule(
            "Moment-closure MDL (QG255)",
            Status.Arbitrary,
            Derivable: false,    // MDL is imported; the moment-order ranking is conventional
            Necessary: false,
            new[]
            {
                "prefer λ₂ (spectral gap) as the mass scale",
                "prefer formulas with the fewest distinct quantities",
                "prefer forms invariant under octave permutation",
                "require 3rd-moment closure instead of 2nd",
            },
            "DECISIVE: the Noether rule rejects 5/4 as a 'free constant', but the PUBLISHED QG238 formula ℓ₁ = Σm·ln(span)·(5/4) uses 5/4 — the exclusion is post-hoc and inconsistent with the published formulas"),
    };

    /// <summary>Status counts.</summary>
    public static IReadOnlyDictionary<Status, int> StatusCounts()
    {
        var dict = Rules().GroupBy(r => r.Status).ToDictionary(g => g.Key, g => g.Count());
        foreach (Status s in Enum.GetValues<Status>())
            if (!dict.ContainsKey(s)) dict[s] = 0;
        return dict;
    }

    /// <summary>
    /// The decisive consistency check: 5/4 is used in a PUBLISHED formula (QG238 ℓ₁ = Σm·ln(span)·5/4),
    /// yet QG255's Noether rule excluded 5/4 as a free constant.
    /// </summary>
    public static bool NoetherInconsistentWithPublished()
        => true;

    /// <summary>The published QG238 formula that uses 5/4.</summary>
    public static string PublishedFiveQuartersFormula()
        => "ℓ₁ = Σm·ln(span)·(5/4)   (QG238 acoustic-peak origin)";

    /// <summary>
    /// Selection-principle risk: the rules are not FORCED. Octave preservation is PREFERRED (D96-
    /// grounded), moment-closure MDL is ARBITRARY (5/4 inconsistency). The rules were selected after
    /// QG253 revealed the non-uniqueness — meta-level retro-selection.
    /// </summary>
    public static string Risk()
    {
        var sc = StatusCounts();
        if (sc[Status.Forced] == 2) return "LOW — both rules forced by D96";
        if (sc[Status.Arbitrary] == 0) return "MEDIUM — rules preferred but not forced";
        return "HIGH — at least one rule is arbitrary (post-hoc selection at the meta-level)";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var sc = StatusCounts();
        return $"Selection-principle risk: {Risk()} — octave preservation {sc[Status.Preferred]}/2 PREFERRED, "
             + $"moment-closure MDL {sc[Status.Arbitrary]}/2 ARBITRARY; 5/4 inconsistency: "
             + $"{NoetherInconsistentWithPublished()} (QG238 ℓ₁ = Σm·ln(span)·5/4)";
    }
}
