namespace TQM.Core.ResearchQM;

public sealed record ProbSource(string Name,string Mechanism,string EmergesFrom,string Derivation,string Status);
public sealed record AltMeasure(string Law,string Form,string Interference,string Normalization,string Consistency,string Viable);
public sealed record FreqModel(string N,string ExpectedFreq,string PredictedProb,string BornPrediction,string Deviation,string Convergence);
public sealed record ExpConstraint(string Experiment,string Precision,string Ruling,string Reference,string Status);
public sealed record BornCandidate(string Name,string Derivation,string Assumptions,int NAssumptions,string Strength,string Verdict);
public sealed record AssumptionAudit(string Assumption,string Primitive,string Derivable,string WithinTQM,string Status);
public sealed record BornResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,ProbSource[] PS,AltMeasure[] AM,FreqModel[] FM,ExpConstraint[] EC,BornCandidate[] BC,AssumptionAudit[] AA);