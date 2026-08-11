namespace TQM.Core.ResearchQG;

public sealed record ActDef(string Aspect,string Definition,string Category,string Status);
public sealed record Dependency(string DerivesFrom,string WhatBreaks,string Severity,string Status);
public sealed record RemovalTest(string Removed,string QmBreaks,string GravityBreaks,string CosmoBreaks,string EverythingBreaks,string Status);
public sealed record Deterministic(string Replacement,string QmOutcome,string GravityOutcome,string Viable,string Status);
public sealed record OntoClass(string Level,string Structure,string Reducibility,string Status);
public sealed record BedrockElt(string Element,string Category,string Derivable,string Irreducible,string Status);
public sealed record ActResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,ActDef[] AD,Dependency[] DP,RemovalTest[] RT,Deterministic[] DT,OntoClass[] OC,BedrockElt[] BE);
