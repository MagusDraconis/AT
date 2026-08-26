namespace AT.Core.ResearchQG;

public sealed record HiggsScan(double Mass_GeV,double Lambda0,double LambdaFinal,string VacuumStatus,string ArchitectureStatus);
public sealed record HmsMechanism(string Mechanism,string Explanation,string Selects125,string Status);
public sealed record HMSResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,HiggsScan[] Scans,HmsMechanism[] Mechanisms);
