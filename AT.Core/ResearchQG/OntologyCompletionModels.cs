namespace AT.Core.ResearchQG;

public sealed record OntologyLayer(string Layer,string Status,string Nature);
public sealed record Residue(string Quantity,string Status,string Nature,string Classification);
public sealed record OAResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,OntologyLayer[] Layers,Residue[] Residues);
