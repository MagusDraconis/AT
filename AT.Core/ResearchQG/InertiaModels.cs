namespace AT.Core.ResearchQG;

public sealed record InertiaSource(string Mechanism,string Derivation,double Contribution,string Status);
public sealed record ArchInertia(string Architecture,double TotalEnergy_J,int Complexity,double InertiaFactor,string Explanation);
public sealed record IGResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,InertiaSource[] Sources,ArchInertia[] ArchInertias);
