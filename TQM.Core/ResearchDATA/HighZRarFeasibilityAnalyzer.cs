namespace TQM.Core.ResearchDATA;

public sealed record ZPoint(double Z,double HZ,double GDagger_1e10,double DeltaFromZ0,double FracChange,string Regime);
public sealed record MondDiff(double Z,double Tqm,double Mond,double Delta,double Sigma,string Detectable);
public sealed record InstCap(string Instrument,double Zmax,double SigmaPerGal,int NGalaxies,string Timeline,string Notes);
public sealed record SampleNeed(double SigmaTarget,int NRequired,int NAvailable,bool Feasible,string Timeline,string Instrument);
public sealed record SysEffect(string Effect,double BiasMag,string Mitigation,string Severity);
public sealed record TLEntry(double Sigma,int Year,string Instrument,string Milestone);
public sealed record FeasResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,ZPoint[] ZP,MondDiff[] MD,InstCap[] IC,SampleNeed[] SN,SysEffect[] SEf,TLEntry[] TL);