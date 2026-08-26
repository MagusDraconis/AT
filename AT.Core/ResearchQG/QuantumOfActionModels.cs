namespace AT.Core.ResearchQG;

public sealed record HMeaning(string Aspect,string Definition,string EmergesFrom,string Status);
public sealed record HbarZero(string Aspect,string Consequence,string Severity,string Status);
public sealed record EventCount(string Approach,string Relation,string Predicts,string Status);
public sealed record PhaseQ(string Aspect,string Mechanism,string Gives,string Status);
public sealed record InfoAction(string Aspect,string Relation,string Constrains,string Status);
public sealed record HResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,HMeaning[] HM,HbarZero[] HZ,EventCount[] EC,PhaseQ[] PQ,InfoAction[] IA);