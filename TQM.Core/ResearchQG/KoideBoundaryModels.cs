namespace TQM.Core.ResearchQG;

public sealed record BoundaryProperty(string Property,string Value,string Interpretation);
public sealed record BoundaryHypothesis(string Interpretation,string ExplainsLeptonSpecificity,string Predictive,string Status);
public sealed record KBResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,BoundaryProperty[] Properties,BoundaryHypothesis[] Hypotheses);
