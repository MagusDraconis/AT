namespace TQM.App.Models;

public sealed record PrimitiveModel(
    string Symbol,
    string Name,
    string Description,
    string Status,
    string Role);
