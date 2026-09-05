namespace AT.Book.Domain;

/// <summary>
/// A research audit: a falsifiable claim, its result, status, date, and dependencies.
/// Audits attach to theory objects via the object's <c>AuditIds</c>.
/// </summary>
public sealed record TheoryAudit(
    string Id,
    string Title,
    string Claim,
    string Result,
    AuditStatus Status,
    DateTime Date,
    TheoryLayer Layer,
    TheoryClassification Classification,
    IReadOnlyList<string> Dependencies) : ITheoryObject
{
    public string Summary => Result;
}
