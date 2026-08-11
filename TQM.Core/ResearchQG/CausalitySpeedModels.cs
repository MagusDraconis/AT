namespace TQM.Core.ResearchQG;

public sealed record Cmeaning(string Aspect,string StandardView,string TqmView,string Status);
public sealed record Cinfinite(string Aspect,string Consequence,string Severity,string Status);
public sealed record ActRate(string Mechanism,string Constraint,string Gives,string Status);
public sealed record MinTime(string Candidate,string Mechanism,string Predicts,string Status);
public sealed record LengthEmerge(string Step,string Relation,string From,string Status);
public sealed record PlanckChain(string Level,string Quantity,string Expression,string From);
public sealed record CSResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,Cmeaning[] CM,Cinfinite[] CI,ActRate[] AR,MinTime[] MT,LengthEmerge[] LE,PlanckChain[] PC);