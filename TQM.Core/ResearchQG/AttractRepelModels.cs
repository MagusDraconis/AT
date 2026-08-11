namespace TQM.Core.ResearchQG;

public sealed record PhaseSign(string Sign,string Gradient,string Curvature,string Stability,string Status);
public sealed record RepulArch(string Architecture,string PhaseStructure,string Gravity,string Stability,string Status);
public sealed record AR29Result(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,PhaseSign[] PS,RepulArch[] RA);