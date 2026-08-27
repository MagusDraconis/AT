namespace AT.App.Models;

/// <summary>
/// A claim-status registry entry: a major AT claim, its derivational status, and a short
/// referee-safe explanation. Mirrors Docs/Research/ATQG_ClaimClassificationRegistry.md.
/// </summary>
public sealed record ClaimStatusInfo(string Claim, ClaimStatus Status, string Explanation);
