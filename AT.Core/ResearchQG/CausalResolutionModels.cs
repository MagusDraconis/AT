namespace AT.Core.ResearchQG;

public sealed record LimitConsequence(string Aspect,string LZero,string Fails,string Severity,string Status);
public sealed record Distinguish(string Criterion,string LZero,string LNonZero,string Conclusion,string Status);
public sealed record DensityCheck(string Regime,string Density,string Problem,string Status);
public sealed record EntropyForce(string Relation,string LZero,string LPlanck,string ForcesL,string Status);
public sealed record StabilityCheck(string Structure,string LZero,string LNonZero,string Conclusion,string Status);
public sealed record NecessityClass(string Level,string Condition,string Forces,string Conclusion,string Status);
public sealed record ResResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,LimitConsequence[] LC,Distinguish[] DS,DensityCheck[] DC,EntropyForce[] EF,StabilityCheck[] SC_,NecessityClass[] NC);