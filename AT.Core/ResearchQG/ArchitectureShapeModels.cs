namespace AT.Core.ResearchQG;

public sealed record ArchProperty(string Property,string Determination,string Status);
public sealed record ShapeMechanism(string Mechanism,string DeterminesShape,string DerivesSpecifics,string Status);
public sealed record ASOResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,ArchProperty[] Properties,ShapeMechanism[] Mechanisms);
