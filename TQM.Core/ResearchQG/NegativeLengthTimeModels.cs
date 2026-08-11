namespace TQM.Core.ResearchQG;

public sealed record NegArg(string Domain,string NegativeValue,string Interpretation,string Verdict);
public sealed record NTResult(string SA,string SB,string SC,string SD,string SE,string SF,NegArg[] Arguments);
