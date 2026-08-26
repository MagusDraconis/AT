namespace AT.Core.ResearchQM;

public sealed record SubsysDef(string Level,string Criterion,string EmergesFrom,string Status);
public sealed record CompStruct(string Structure,string Dimension,string PreservesAmplitude,string PreservesInterference,string PreservesNorm,string Viable);
public sealed record EntangleStep(string Step,string Mechanism,string Example,string FromQEvents,string Status);
public sealed record BellResult(string Correlation,string ClassicalBound,string QuantumBound,string AtPrediction,string Status);
public sealed record AxiomReduction(string Axiom,string StandardQM,string AtStatus,string DerivedFrom,string Classification);
public sealed record TensorResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,SubsysDef[] SDf,CompStruct[] CS,EntangleStep[] ES,BellResult[] BR,AxiomReduction[] AR);