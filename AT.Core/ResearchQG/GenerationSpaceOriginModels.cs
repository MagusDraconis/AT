namespace AT.Core.ResearchQG;

public sealed record GOrigin(string Hypothesis,string Mechanism,string DerivesDim3,string Status);
public sealed record GOResult2(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,GOrigin[] Origins);
