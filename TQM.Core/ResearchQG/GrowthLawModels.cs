namespace TQM.Core.ResearchQG;

public sealed record GrowthMech(string Mechanism,string EmergesFrom,string IsDerivable,string Status);
public sealed record GrowthLaw(string Form,string Nt,string Ht,string Predicts,string Naturalness,string Status);
public sealed record NEvol(string Era,string Nt,string Ht,string At,string Wz,string Status);
public sealed record HubEmerge(string Era,string Ht,string Q,string Comparison,string Status);
public sealed record CosmoEra(string Era,string Growth,string H,string Lambda,string Observables,string Status);
public sealed record FutureEvol(string Scenario,string Nt,string Ht,string Fate,string Timeline,string Status);
public sealed record GLResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,GrowthMech[] GM,GrowthLaw[] GL,NEvol[] NE,HubEmerge[] HE,CosmoEra[] CE,FutureEvol[] FE);