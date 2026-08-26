namespace AT.Core.ResearchQG;

public sealed record S1Fact(string WhatS1Gives,string Group,string Relevance,string Verdict);
public sealed record ModeCount(int N,string SymmetryGroup,string KoideAnalog,string Viability,string S1Preference);
public sealed record S13Result(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,S1Fact[] Facts,ModeCount[] Modes);
