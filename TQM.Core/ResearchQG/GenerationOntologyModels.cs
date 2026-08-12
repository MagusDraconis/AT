namespace TQM.Core.ResearchQG;

public sealed record GOntology(string Interpretation,string WhatGStores,string Evidence,string Status);
public sealed record Elimination(string Attempt,string WhyItFails,string Verdict);
public sealed record GOResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,GOntology[] Ontologies,Elimination[] Eliminations);
