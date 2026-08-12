namespace TQM.Core.ResearchQG;

public sealed record TopoGrav(double WindingN,int WindingSign,double EnergyDensity_Jpm3,double PhaseGradientMagnitude,double CurvatureEffect,string IdenticalTo_n1);
public sealed record AntiMatterGrav(string Particle,string nStr,double Mass_MeV,string GravPrediction,string Experiment,string Status);
public sealed record WSGResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,TopoGrav[] Comparisons,AntiMatterGrav[] AntiMatterPredictions);
