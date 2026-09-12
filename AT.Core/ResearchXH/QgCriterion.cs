namespace AT.Core.ResearchXH;

/// <summary>
/// The status of one quantum-gravity closure criterion.
/// <see cref="None"/> = the criterion fails; <see cref="Partial"/> = evidence exists for part of the
/// criterion only (explicit partial credit); <see cref="Full"/> = the criterion holds.
/// </summary>
public enum QgStatus
{
    None = 0,
    Partial = 1,
    Full = 2,
}

/// <summary>
/// A quantum-gravity closure criterion as a WHOLE VALUE: its status, plus the evidence it cites.
///
/// WHY THIS TYPE EXISTS (ResearchY-G_025 / G_026). The QG closure ladder — QG215 (PARTIAL QG), QG219
/// (EFFECTIVE QG), QG221 (NEAR-COMPLETE QG), QG223 (COMPLETE QG) — was originally written as bare
/// <c>bool</c> literals:
///
///     public static bool IsQuantumMechanicsDerived() => true;
///
/// so the entire escalation from PARTIAL to COMPLETE was produced by editing a literal, and each audit's
/// sub-scores were a SEPARATE set of hard-coded <c>1.0</c>s that did not even read the criteria. The
/// consequence was that the classification could not move when the criteria changed, and the tests
/// re-asserted the literals (<c>Assert.True(IsQuantumMechanicsDerived())</c>), so no assertion in the
/// suite could ever fail. Four successive audits with the same method names ended up asserting
/// mutually contradictory verdicts, all passing.
///
/// A criterion is therefore no longer a bare boolean:
///   * the status is explicit, so partial credit is a DECLARED choice rather than a typed number;
///   * <see cref="Basis"/> is mandatory, so every claim names the phase audit carrying its evidence;
///   * <see cref="Score"/> is DERIVED from the status, so a score cannot drift away from its criterion;
///   * <see cref="Holds"/> is the only way a criterion counts as met.
///
/// This does not make an authored research verdict "computed" — it makes it IMPOSSIBLE TO MISTAKE for a
/// computation, and it makes the score ladder a function of the criteria rather than of six literals.
/// </summary>
/// <param name="Name">The criterion, phrased as a question answered by the audit.</param>
/// <param name="Status">None / Partial / Full.</param>
/// <param name="Basis">The phase audit(s) that carry the evidence for this status. Must not be empty.</param>
public sealed record QgCriterion(string Name, QgStatus Status, string Basis)
{
    /// <summary>The criterion's contribution to the closure score: Full = 1.0, Partial = 0.5, None = 0.0.</summary>
    public double Score => Status switch
    {
        QgStatus.Full => 1.0,
        QgStatus.Partial => 0.5,
        _ => 0.0,
    };

    /// <summary>Does the criterion hold outright? (Only Full counts as met.)</summary>
    public bool Holds => Status == QgStatus.Full;

    /// <summary>Is the criterion's evidence cited? Every criterion must name its basis.</summary>
    public bool BasisCited => !string.IsNullOrWhiteSpace(Basis);
}

/// <summary>
/// Helpers for the QG closure audits. These derive the closure ladder from the criteria tables so that
/// the ladder, the total score and the classification are all functions of the criteria — and so that a
/// criterion's basis is never missing.
/// </summary>
public static class QgClosure
{
    /// <summary>Total closure score (0..N) — the sum of the DERIVED per-criterion scores.</summary>
    public static double TotalScore(params QgCriterion[] criteria) => criteria.Sum(c => c.Score);

    /// <summary>
    /// QG classification by total score, on the six-criterion scale:
    ///   &lt; 3.0  → PARTIAL QG;  3.0 – 4.5 → EFFECTIVE QG;  5.0 – 5.5 → NEAR-COMPLETE QG;  6.0 → COMPLETE QG.
    /// </summary>
    public static string Classify(double totalScore)
    {
        if (totalScore >= 6.0) return "COMPLETE QG";
        if (totalScore >= 5.0) return "NEAR-COMPLETE QG";
        if (totalScore >= 3.0) return "EFFECTIVE QG";
        return "PARTIAL QG";
    }

    /// <summary>Do all criteria cite a basis? A criterion without a cited basis is an unbacked claim.</summary>
    public static bool AllBasesCited(params QgCriterion[] criteria) => criteria.All(c => c.BasisCited);

    /// <summary>The criteria that do NOT cite a basis (should always be empty).</summary>
    public static string[] UnbackedCriteria(params QgCriterion[] criteria)
        => criteria.Where(c => !c.BasisCited).Select(c => c.Name).ToArray();

    /// <summary>
    /// Does the declared classification follow from the criteria alone? Re-derives the total from the
    /// criteria and compares — this can fail, so it is a real check rather than a restatement.
    /// </summary>
    public static bool ClassificationFollowsFromCriteria(string declared, params QgCriterion[] criteria)
        => declared == Classify(TotalScore(criteria));

    /// <summary>The criteria, labelled with their status and score, for reporting.</summary>
    public static (string Criterion, string Status, double Score, string Basis)[] Table(
        params QgCriterion[] criteria)
        => criteria.Select(c => (c.Name, c.Status.ToString(), c.Score, c.Basis)).ToArray();
}
