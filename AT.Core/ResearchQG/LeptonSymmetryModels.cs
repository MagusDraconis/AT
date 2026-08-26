namespace AT.Core.ResearchQG;

public sealed record LeptonQuark(string Property,string Leptons,string Quarks,string AtMeaning);
public sealed record HiddenSymmetry(string Candidate,string Mechanism,string Explains45,string ExplainsLeptonSpecificity,string Status);
public sealed record LSSResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,LeptonQuark[] Comparison,HiddenSymmetry[] Symmetries);
