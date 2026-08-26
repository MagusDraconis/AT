namespace AT.Core.ResearchQG;

public sealed record LMeaning(string Aspect,string Definition,string IsMeasurable,string Status);
public sealed record LOrigin(string Candidate,string Mechanism,string DependsOn,string Strength,string Status);
public sealed record LDependency(string DerivedQuantity,string Relation,string IfLUnknown,string Status);
public sealed record LConstraint(string Source,string Constraint,string FixesL,string Status);
public sealed record ParamElim(string Parameter,string Category,string Eliminable,string Consequence,string Status);
public sealed record LResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,LMeaning[] LM,LOrigin[] LO,LDependency[] LD,LConstraint[] LC,ParamElim[] PE);