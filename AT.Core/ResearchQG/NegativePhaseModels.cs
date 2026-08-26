namespace AT.Core.ResearchQG;

public sealed record PhaseStructure(string Name,string PhaseRelation,string Mechanism,string Stability,string GeomEffect,string Classification);
public sealed record NPAResult(string SA,string SB,string SC,string SD,string SE,string SF,PhaseStructure[] Structures);
