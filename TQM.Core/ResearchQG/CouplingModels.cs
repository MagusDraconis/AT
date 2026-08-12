namespace TQM.Core.ResearchQG;

public sealed record Coupling(string Name,double Value,string Formula,string TqmInterpretation,string Derived,string Status);
public sealed record NumerAttempt(string Author,string Year,string Formula,string PredictedAlpha,string Verdict);
public sealed record CCResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,Coupling[] Couplings,NumerAttempt[] Numerologies);
