namespace TQM.Core.ResearchQG;

public sealed record RARPoint(double Redshift,double H_kmsMpc,double Gdagger_mps2,double RatioToG0);
public sealed record RPEAResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,RARPoint[] Points,double G0_mps2,double MaxSeparation);
