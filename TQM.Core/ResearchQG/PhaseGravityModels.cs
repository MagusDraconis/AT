namespace TQM.Core.ResearchQG;

public sealed record PhGravLink(string Aspect,string Mechanism,string From,string Status);
public sealed record PhGradient(string Regime,string PhaseField,string Metric,string Curvature,string Status);
public sealed record OscDensity(string Density,string CausalEffect,string MetricEffect,string GravityEffect,string Status);
public sealed record MassPh(string Aspect,string TqmView,string StandardView,string Extra,string Status);
public sealed record GR22Result(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,PhGravLink[] PG,PhGradient[] Pg,OscDensity[] OD,MassPh[] MP);