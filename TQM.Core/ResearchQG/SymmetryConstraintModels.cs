namespace TQM.Core.ResearchQG;

public sealed record ConstraintClass(string Name,string SymmetryComponent,string StabilityComponent,string Classification,string HybridNote);
public sealed record ReductionTest(string Target,string ViaMechanism,string Result,string Counterexample);
public sealed record SCResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,ConstraintClass[] Classes,ReductionTest[] Reductions);
