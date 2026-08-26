namespace AT.Core.ResearchQM;

public sealed record QState(string Name,string Structure,string Dimension,string EmergesFrom,string Status);
public sealed record AmplitudeStep(string Step,string Mechanism,string WhyComplex,string Emerges,string Status);
public sealed record InterferenceModel(string Source,string Mechanism,string Produces,string RequiresComplex,string Status);
public sealed record InnerProductStep(string Step,string Definition,string Properties,string FromQEvents,string Status);
public sealed record TensorStep(string Step,string Structure,string Requires,string EmergesFrom,string Status);
public sealed record HilbertStep(int Step,string Structure,string Derivation,string FromQ,string Status);
public sealed record HilbertResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,QState[] QS,AmplitudeStep[] AS,InterferenceModel[] IM,InnerProductStep[] IP,TensorStep[] TS,HilbertStep[] HS);