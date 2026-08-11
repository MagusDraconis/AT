namespace TQM.Core.ResearchQG;

public sealed record NoiseVsStruct(string Scenario,string Outcome,string Why,string Status);
public sealed record AttractorMech(string Mechanism,string Creates,string EmergesFrom,string Status);
public sealed record PersistWhy(string Entity,string WhyStable,string Lifetime,string Mechanism,string Status);
public sealed record SelfOrg(string Level,string Structure,string AttractorType,string Status);
public sealed record SPResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,NoiseVsStruct[] NS,AttractorMech[] AM,PersistWhy[] PW,SelfOrg[] SO);