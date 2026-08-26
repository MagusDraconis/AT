namespace AT.Core.ResearchQG;

public sealed record MidpointFact(string Observation,string Value,string Interpretation);
public sealed record CrossSystem(string System,string Quantity,string Formula,string MidpointAnalog,string Relevance);
public sealed record KMPResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,MidpointFact[] Facts,CrossSystem[] Analogies);
