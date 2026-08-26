namespace AT.Core.ResearchQG;

public sealed record FlavorRelation(string Name,string Precision,string Sector,string Status,string Type);
public sealed record Exceptionalism(string Hypothesis,string Evidence,string Status,string Verdict);
public sealed record FEResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,FlavorRelation[] Relations,Exceptionalism[] Hypotheses);
