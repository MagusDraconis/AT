namespace AT.Core.ResearchQG;

public sealed record LeveragePt(string Parameter,string Level,string Amplification,string Mechanism,string Accessibility,string Status);
public sealed record SynchAmp(string Domain,string Qevents,string Amplification,string Feasibility,string Status);
public sealed record TopoLever(string Defect,string Sensitivity,string Leverage,string Feasibility,string Status);
public sealed record M2Lever(string Variation,string Effect,string Amplification,string Feasibility,string Status);
public sealed record RLResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,LeveragePt[] LP,SynchAmp[] SA_,TopoLever[] TL,M2Lever[] ML);