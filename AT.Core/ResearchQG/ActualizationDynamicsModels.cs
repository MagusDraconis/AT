namespace AT.Core.ResearchQG;

public sealed record ActProperty(string Property,string Definition,string Variable,string Dynamics,string Status);
public sealed record ActRegime(string Regime,string Density,string Emergent,string SameProcess,string Status);
public sealed record ActAmpl(string Pathway,string Mechanism,string Amplification,string Feasibility,string Status);
public sealed record AR25Result(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,ActProperty[] AP,ActRegime[] AR,ActAmpl[] AA);