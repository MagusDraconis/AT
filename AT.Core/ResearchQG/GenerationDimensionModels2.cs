namespace AT.Core.ResearchQG;

public sealed record DerivationAttempt(string Mechanism,string ProducesN,string Derives3,string Verdict);
public sealed record GDR2Result(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,DerivationAttempt[] Attempts);
