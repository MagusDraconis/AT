namespace AT.Core.ResearchDATA;

public sealed record SysSource(string Name,string Mechanism,double BiasMag,double BiasSign,string ZDependence,string Mitigation,string Severity);
public sealed record BeamModel(double Z,double PsfKpc,double RdiskTyp,double SmearFactor,double BiasOnGdagger,string Impact);
public sealed record InclModel(double SigmaInc,double SigmaV,double SigmaG,int NRequired,string Notes);
public sealed record MorphModel(double Z,string MorphType,double Turbulence,double BiasOnGdagger,string Impact);
public sealed record SelBias(string Effect,string BiasFraction,string Direction,string ZDependence,string Severity);
public sealed record FalsePosResult(double Z,double TrueGdagger,double MeasuredGdagger,double ApparentBias,bool FalsePositive,string Verdict);
public sealed record RobustScore(string Aspect,double Score,double MaxScore,string Assessment);
public sealed record SysResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,SysSource[] SS,BeamModel[] BM,InclModel[] IM,MorphModel[] MM,SelBias[] Sb,FalsePosResult[] FP,RobustScore[] RS);