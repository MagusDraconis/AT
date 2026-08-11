namespace TQM.Core.ResearchQG;

public sealed record TauMeaning(string Aspect,string Definition,string Relation,string Status);
public sealed record TauZero(string Aspect,string Consequence,string Severity,string Status);
public sealed record ContinuousReality(string Claim,string Problem,string Viable,string Status);
public sealed record Becoming(string Aspect,string RequiresTau,string Why);
public sealed record InfoFlowTau(string Aspect,string TauZero,string TauNonZero,string Status);
public sealed record TauDependency(string DependsOn,string Role,string IfTauZero,string Status);
public sealed record TauResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,TauMeaning[] TM,TauZero[] TZ,ContinuousReality[] CR,Becoming[] BG,InfoFlowTau[] IF,TauDependency[] TD);