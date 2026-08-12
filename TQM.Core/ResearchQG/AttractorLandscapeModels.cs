namespace TQM.Core.ResearchQG;

public sealed record LandscapeProperty(string Property,string Determination,string Status);
public sealed record LandscapeHypothesis(string Hypothesis,string Explains,string Status);
public sealed record ALResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,LandscapeProperty[] Properties,LandscapeHypothesis[] Hypotheses);
