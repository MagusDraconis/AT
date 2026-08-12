namespace TQM.Core.ResearchQG;

public sealed record Manifold(string Name,string Type,string GeneratingMechanism,string EmergenceLevel,string UnifiedBy);
public sealed record CMResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,Manifold[] Manifolds);
