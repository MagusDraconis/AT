namespace AT.Core.ResearchQG;

public sealed record Constraint(string Name,string ActsOn,string Type,string AllowableRegion,string FixedValue,string Status);
public sealed record Layer(string LayerName,string WhatItDetermines,string Mechanism,string Examples);
public sealed record SPBResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,Constraint[] Constraints,Layer[] Layers);
