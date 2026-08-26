namespace AT.Core.ResearchQG;

public sealed record GaugeCandidate(string Group,string AtStructure,string DerivationStatus,string StabilityRank,string MatterSupport,string Verdict);
public sealed record GaugeDeriv(string Group,int Rank,string EmergenceMechanism,string FromAt,string Completeness);
public sealed record GGSResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,GaugeCandidate[] Candidates,GaugeDeriv[] Derivations);
