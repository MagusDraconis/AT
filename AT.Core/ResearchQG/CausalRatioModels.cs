namespace AT.Core.ResearchQG;

public sealed record RatioVar(string Ratio,string Cval,string Physics,string Status);
public sealed record FixedC(string Constraint,string FreeParams,string Degeneracy,string Status);
public sealed record ObsEffect(string Observable,string DependsOn,string RatioChange,string Status);
public sealed record HiddenRatio(string Candidate,string Mechanism,string Discovered,string Status);
public sealed record RResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,RatioVar[] RV,FixedC[] FC,ObsEffect[] OE,HiddenRatio[] HR);