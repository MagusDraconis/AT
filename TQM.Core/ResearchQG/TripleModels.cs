namespace TQM.Core.ResearchQG;

public sealed record TripleDep(string Parameter,string Dimensions,string Removed,string WhatBreaks,string Status);
public sealed record PairDerive(string Target,string FromPair,string Possible,string Why,string Status);
public sealed record TripleSym(string Symmetry,string Transforms,string Invariant,string Status);
public sealed record TRResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,TripleDep[] TD,PairDerive[] PD,TripleSym[] TS);