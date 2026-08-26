namespace AT.Core.ResearchQG;

public sealed record GrowthStep(int Step,string What,string Mechanism,string Emerges,string Status);
public sealed record ConnectEvo(string Stage,string N,string Links,string EffectiveR,string Description,string Status);
public sealed record DistEmerge(int Step,string Structure,string Derivation,string FromQ,string Status);
public sealed record ScaleStep(int Step,string Relation,string Derivation,string EmergesFrom,string Status);
public sealed record HubbleStep(int Step,string Relation,string Derivation,string Prediction,string Status);
public sealed record CosmoComp(string Framework,string Expansion,string DarkEnergy,string AtComparison);
public sealed record CEExpResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,GrowthStep[] GS,ConnectEvo[] CE,DistEmerge[] DE,ScaleStep[] SS,HubbleStep[] HS,CosmoComp[] CC);