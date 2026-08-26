namespace AT.Core.ResearchQG;

public sealed record ScalingResult(string Scaling,string C,string G,string MP,string SBH,string TH,string Status);
public sealed record IndepVar(string Variation,string Cchange,string Gchange,string Viable,string Status);
public sealed record ObsSens(string Observable,string DependsOn,string InvariantUnder,string Status);
public sealed record Degeneracy(string Scaling,string Invariant,string Broken,string Physical,string Status);
public sealed record SResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,ScalingResult[] SR,IndepVar[] IV,ObsSens[] OS,Degeneracy[] DG);