namespace TQM.Core.ResearchQG;

public sealed record GRole(string Aspect,string StandardView,string TqmView,string Status);
public sealed record DerivationPath(string Path,string Expression,string DependsOn,string Derived,string Status);
public sealed record Dimensional(string Combination,string Result,string EmergesFrom,string Status);
public sealed record ConnectG(string Mechanism,string Relation,string Predicts,string Status);
public sealed record GEvolution(string Era,string GValue,string Mechanism,string Observable,string Status);
public sealed record ObsConstraint(string Observation,string Precision,string Constrains,string Status);
public sealed record GResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,GRole[] GR,DerivationPath[] DP,Dimensional[] DM,ConnectG[] CG,GEvolution[] GE,ObsConstraint[] OC);