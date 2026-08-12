namespace TQM.Core.ResearchQG;

public sealed record GGeometry(string Candidate,string Dimension,string Symmetry,string ExplainsMixing,string ExplainsKoide,string CPPhase,string Score);
public sealed record GGRResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,GGeometry[] Geometries);
