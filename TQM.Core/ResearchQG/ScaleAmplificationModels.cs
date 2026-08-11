namespace TQM.Core.ResearchQG;

public sealed record SpatialScale(string Structure,double SizeM,double RatioToL,string Status);
public sealed record TemporalScale(string Process,double TimeS,double RatioToTau,string Status);
public sealed record AmplFactor(int Layer,string Transition,string FromScaleM,string ToScaleM,string AmpFactor,string Mechanism);
public sealed record EmergeLayer(int Level,string Structure,string SizeL,string SizeM,string Qevents,string Status);
public sealed record ContValid(string Physics,string BelowScale,string AboveScale,string Breakdown,string Status);
public sealed record Q19Result(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,SpatialScale[] SS,TemporalScale[] TS,AmplFactor[] AF,EmergeLayer[] EL,ContValid[] CV);