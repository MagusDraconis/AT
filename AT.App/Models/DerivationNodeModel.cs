namespace AT.App.Models;

public sealed class DerivationNodeModel
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string Summary { get; init; }
    public ClassificationKind Classification { get; init; }
    public required string VerificationStatus { get; init; }
    public IReadOnlyList<DerivationNodeModel> Children { get; init; } = [];
}
