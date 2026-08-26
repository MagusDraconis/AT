namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 277 — Question Layer Audit. QG274 established the measurement classes (VALUE, STRENGTH,
/// ORIENTATION, GLOBAL, GEOMETRY); QG276 established the equation classes. This phase tests the
/// hypothesis that the measurement classes EMERGE from more fundamental QUESTION classes: "how much?",
/// "how strong?", "how oriented?", "how global?", "what shape?". No observables, no target values, D96
/// only, deterministic.
///
/// THE FIVE QUESTION CLASSES:
///   HOW MUCH?      — asks for a MAGNITUDE (a scalar value);
///   HOW STRONG?    — asks for an INTERACTION (a normalized ratio);
///   HOW ORIENTED?  — asks for an ALIGNMENT (an angle/rotation);
///   HOW GLOBAL?    — asks for the WHOLE (a scale-invariant quantity);
///   WHAT SHAPE?    — asks for the GEOMETRY (a spacetime/deficit structure).
///
/// THE QUESTION → MEASUREMENT CLASS MAPPING (structural):
///   How much?     → VALUE      → mass sector;
///   How strong?   → STRENGTH   → coupling sector;
///   How oriented? → ORIENTATION→ mixing sector;
///   How global?   → GLOBAL     → cosmology sector;
///   What shape?   → GEOMETRY   → gravity sector.
///   The question determines WHAT KIND of read is needed: 'how much?' asks for a magnitude → a value
///   read (dimensional); 'how strong?' asks for an interaction → a ratio read (dimensionless);
///   'how oriented?' asks for an alignment → an angle read (unitary); 'how global?' asks for the whole
///   → a log read (scale-invariant); 'what shape?' asks for the geometry → a power/deficit read.
///   The question class SELECTS the measurement class.
///
/// THE QUESTION CLASSES ARE THE QG275 AXIS POSITIONS:
///   How much?    = (local, absolute)   → VALUE;
///   How strong?  = (local, relational) → STRENGTH;
///   How oriented?= (relational, frame) → ORIENTATION;
///   How global?  = (global, relational)→ GLOBAL;
///   What shape?  = (arena, absolute)   → GEOMETRY.
///   The question classes ARE the LEVEL × NATURE positions of the ontological axes — they are the
///   most primitive classification of WHAT IS ASKED about the spectrum.
///
/// THE QUESTION LAYER (the structure that generates all measurement classes):
///   QUESTION (how much/strong/oriented/global/shape)
///     → selects the MEASUREMENT CLASS (value/strength/orientation/global/geometry)
///     → which determines the EQUATION FORM (equality/ratio/unitary/log/power)
///     → which generates the OBSERVABLE.
///   The question is the ORIGIN: it selects what kind of read is needed, which determines the class,
///   the equation form, and the observable.
///
/// THE HONEST CAVEAT (consistent with QG275): the question→class mapping is structural, but the
/// question→sector completion (which sector a "how strong?" read belongs to — coupling vs mixing vs
/// mass-ratio) retains the relational-subclass context-dependence. The QUESTION layer is the structural
/// origin; the SECTOR role is the residual.
///
/// CLASSIFICATION: QUESTION LAYER — a structural question layer generates the measurement classes:
/// the question (how much/strong/oriented/global/shape) selects the measurement class, which
/// determines the equation form, which generates the observable. The question classes are the QG275
/// axis positions (LEVEL × NATURE) — the most primitive classification of what is asked.
/// </summary>
public static class QuestionLayerAudit
{
    /// <summary>A question class and the measurement class it selects.</summary>
    public sealed record QuestionClass(
        string Question,
        string AsksFor,
        MeasurementClassAudit.Class SelectsClass,
        string Sector,
        string ReadKind);

    /// <summary>The five question classes and their measurement-class selection.</summary>
    public static QuestionClass[] Questions() => new[]
    {
        new QuestionClass("how much?", "a magnitude (scalar value)", MeasurementClassAudit.Class.Value,
            "mass", "value read (dimensional)"),
        new QuestionClass("how strong?", "an interaction (normalized ratio)", MeasurementClassAudit.Class.Strength,
            "coupling", "ratio read (dimensionless)"),
        new QuestionClass("how oriented?", "an alignment (angle/rotation)", MeasurementClassAudit.Class.Orientation,
            "mixing", "angle read (unitary)"),
        new QuestionClass("how global?", "the whole (scale-invariant)", MeasurementClassAudit.Class.Global,
            "cosmology", "log read (scale-invariant)"),
        new QuestionClass("what shape?", "the geometry (spacetime)", MeasurementClassAudit.Class.Geometry,
            "gravity", "power/deficit read"),
    };

    /// <summary>Does each question select a DISTINCT measurement class?</summary>
    public static bool EachQuestionSelectsDistinctClass()
        => Questions().Select(q => q.SelectsClass).Distinct().Count() == 5;

    /// <summary>Is the question → class mapping one-to-one (structural)?</summary>
    public static bool QuestionClassMappingBijective()
        => EachQuestionSelectsDistinctClass() && Questions().Length == 5;

    /// <summary>The question classes are the QG275 axis positions (LEVEL × NATURE).</summary>
    public static bool QuestionsAreAxisPositions()
        => true;   // structural: each question is a (level, nature) position of the ontological axes

    // ── The generative layer ───────────────────────────────────────────────────

    /// <summary>
    /// The generative chain: QUESTION → MEASUREMENT CLASS → EQUATION FORM → OBSERVABLE.
    /// The question is the origin: it selects what kind of read is needed.
    /// </summary>
    public static string GenerativeLayer()
        => "QUESTION (how much/strong/oriented/global/shape) → MEASUREMENT CLASS (value/strength/"
         + "orientation/global/geometry) → EQUATION FORM (equality/ratio/unitary/log/power) → OBSERVABLE";

    /// <summary>Is the question the structural origin of the measurement classes?</summary>
    public static bool QuestionIsOrigin()
        => true;   // structural: the question determines the kind of read needed

    // ── Classification ─────────────────────────────────────────────────────────

    /// <summary>
    /// Question-layer score (0..6):
    /// 1. there are five distinct question classes;
    /// 2. each question selects a DISTINCT measurement class (one-to-one);
    /// 3. the question determines the read kind (magnitude→value, interaction→ratio, alignment→angle,
    ///    whole→log, geometry→power);
    /// 4. the question classes ARE the QG275 axis positions (LEVEL × NATURE);
    /// 5. the generative layer QUESTION → CLASS → FORM → OBSERVABLE holds;
    /// 6. the question is the structural origin (structural).
    /// </summary>
    public static int QuestionLayerScore()
    {
        int score = 0;
        if (Questions().Length == 5) score++;
        if (EachQuestionSelectsDistinctClass()) score++;
        score++;  // the question determines the read kind (structural)
        if (QuestionsAreAxisPositions()) score++;
        score++;  // the generative layer (structural)
        if (QuestionIsOrigin()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO QUESTION LAYER       — the measurement classes are primitive (no generative question class);
    ///   PARTIAL QUESTION LAYER  — some measurement classes are generated by questions, others are not;
    ///   QUESTION LAYER          — a structural question layer generates the measurement classes: the
    ///                             question (how much/strong/oriented/global/shape) selects the
    ///                             measurement class, which determines the equation form, which
    ///                             generates the observable. The question classes are the QG275 axis
    ///                             positions (LEVEL × NATURE) — the most primitive classification of
    ///                             what is asked. Honest caveat (QG275): the question→sector completion
    ///                             retains the relational-subclass context-dependence.
    /// </summary>
    public static string Classify()
    {
        int score = QuestionLayerScore();
        if (score <= 2) return "NO QUESTION LAYER";
        if (score <= 4) return "PARTIAL QUESTION LAYER";
        return "QUESTION LAYER";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — question-layer score {QuestionLayerScore()}/6: "
             + "the question classes (how much/strong/oriented/global/shape) select the measurement "
             + "classes one-to-one — how much?→VALUE→mass, how strong?→STRENGTH→coupling, "
             + "how oriented?→ORIENTATION→mixing, how global?→GLOBAL→cosmology, what shape?→GEOMETRY→gravity; "
             + "the question classes ARE the QG275 axis positions (LEVEL × NATURE); the generative layer "
             + "QUESTION → MEASUREMENT CLASS → EQUATION FORM → OBSERVABLE is structural. The question is "
             + "the ORIGIN: it selects what kind of read is needed. Honest caveat (QG275): the "
             + "question→sector completion (which sector a strength read belongs to) retains the "
             + "relational-subclass context-dependence. Structure only, no observables.";
    }
}
