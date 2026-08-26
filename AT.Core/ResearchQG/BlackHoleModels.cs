namespace AT.Core.ResearchQG;

public sealed record HorizStep(int Step,string Structure,string EmergesFrom,string Mechanism,string Status);
public sealed record EntropyStep(int Step,string Relation,string Derivation,string FromAt,string Status);
public sealed record InfoFlow(string Aspect,string AtMechanism,string Outcome,string Status);
public sealed record EntHorizon(string Aspect,string Mechanism,string Prediction,string Status);
public sealed record HawkStep(int Step,string Mechanism,string EmergesFrom,string Prediction,string Status);
public sealed record ParaResolution(string Approach,string CoreIdea,string AtPosition,string Status);
public sealed record BHResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,HorizStep[] HS,EntropyStep[] ES,InfoFlow[] IF,EntHorizon[] EH,HawkStep[] HaS,ParaResolution[] PR);