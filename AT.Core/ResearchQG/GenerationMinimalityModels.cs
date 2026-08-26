namespace AT.Core.ResearchQG;

public sealed record MinimalStructure(string Approach,string WhatItGives,string WhatItLacks,string Verdict);
public sealed record GenDim(int Dim,string Symmetry,string Mixing,string CPViolation,string Status);
public sealed record GSMResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,MinimalStructure[] Approaches,GenDim[] Dimensions);
