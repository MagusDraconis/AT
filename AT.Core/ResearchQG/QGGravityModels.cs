namespace AT.Core.ResearchQG;

public sealed record PreGeoLevel(int Level,string Structure,string WhatExists,string WhatEmerges,string Status);
public sealed record EmergenceStep(int Step,string From,string To,string Mechanism,string AtPrimitive,string Status);
public sealed record MetricStep(int Step,string Structure,string Derivation,string FromQ,string Status);
public sealed record GeoStep(int Step,string Structure,string Derivation,string Requires,string Status);
public sealed record GravityPlace(string Aspect,string EmergesAt,string After,string Before,string Status);
public sealed record QGFramework(string Framework,string Approach,string SpacetimeStatus,string GravityStatus,string AtComparison);
public sealed record QGResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,PreGeoLevel[] PG,EmergenceStep[] ES,MetricStep[] MS,GeoStep[] GS,GravityPlace[] GP,QGFramework[] QF);