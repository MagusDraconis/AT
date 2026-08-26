namespace AT.Core.ResearchQG;

public sealed record NearbyValue(double Q,double Cos2Theta,double ThetaDeg,string Viability,string Note);
public sealed record Distinction(string Interpretation,string How2over3Appears,string Distinguished,string Status);
public sealed record WTNResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,NearbyValue[] Nearby,Distinction[] Distinctions);
