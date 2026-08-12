namespace TQM.Core.ResearchQG;

public sealed record GenLevel(string Name,double Mass_MeV,double RatioToPrev,string Topology,string FrequencyBand,string Status);
public sealed record GenMechanism(string Mechanism,string Explanation,string DerivesCount,string Status);
public sealed record GSResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,GenLevel[] LeptonLevels,GenMechanism[] Mechanisms);
