namespace AT.Core.ResearchQM;

public sealed record MeasDef(string Aspect,string StandardQM,string AtResolution,string Status);
public sealed record DecoStep(string Step,string Mechanism,string TimeScale,string EmergesFrom,string Status);
public sealed record PointerState(string State,string SelectionMechanism,string Stability,string EmergesFrom,string Status);
public sealed record ClassicalStep(string Step,string Mechanism,string FromQM,string Status);
public sealed record CollapseComp(string Interpretation,string CollapseMechanism,string AdditionalAxioms,string AtEquivalent,string Status);
public sealed record MeasResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,MeasDef[] MD,DecoStep[] DS,PointerState[] PS,ClassicalStep[] CS,CollapseComp[] CC);