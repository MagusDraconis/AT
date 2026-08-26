namespace AT.Core.ResearchQG;

public sealed record OscNecessity(string Model,string Outcome,string Fails,string Status);
public sealed record SuccessChain(string Step,string Mechanism,string Inevitable,string Status);
public sealed record OscVerdict(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,OscNecessity[] ON,SuccessChain[] SCH);