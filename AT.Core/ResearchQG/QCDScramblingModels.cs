namespace AT.Core.ResearchQG;

public sealed record SectorAngle(string Sector,double ThetaLowDeg,double DeltaFrom45,double ThetaGUTDeg,string Relation);
public sealed record ScramblingTest(string Test,string Method,string Result,string Verdict);
public sealed record QYSResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,SectorAngle[] Sectors,ScramblingTest[] Tests);
