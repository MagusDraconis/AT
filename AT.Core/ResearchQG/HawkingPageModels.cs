namespace AT.Core.ResearchQG;

public sealed record HRStep(int Step,string Mechanism,string EmergesFrom,string Status);
public sealed record ThermAnalysis(string Aspect,string AtMechanism,string Prediction,string Status);
public sealed record InfoEncode(string Phase,string Where,string Mechanism,string Accessible,string Status);
public sealed record PagePhase(string Phase,string Time,string Sentropy,string InfoOut,string Mechanism,string Status);
public sealed record EntropyEvo(string Stage,string SBH,string Srad,string Stotal,string Info,string Status);
public sealed record ParaComp(string Approach,string PageCurve,string Firewall,string InfoOutcome,string AtPosition);
public sealed record HPResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,HRStep[] HR,ThermAnalysis[] TA,InfoEncode[] IE,PagePhase[] PP,EntropyEvo[] EE,ParaComp[] PC);