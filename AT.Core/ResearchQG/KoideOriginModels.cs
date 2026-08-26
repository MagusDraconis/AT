namespace AT.Core.ResearchQG;

public sealed record InfoMetric(string Quantity,double Value,string Interpretation);
public sealed record KoideOrigin(string Candidate,string Mechanism,string Explains2over3,string Status);
public sealed record KCOResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,InfoMetric[] Metrics,KoideOrigin[] Origins);
