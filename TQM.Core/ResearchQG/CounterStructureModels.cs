namespace TQM.Core.ResearchQG;

public sealed record CountStruc(string Candidate,string Mechanism,string WhyFails,string Status);
public sealed record CS30Result(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,CountStruc[] CS);