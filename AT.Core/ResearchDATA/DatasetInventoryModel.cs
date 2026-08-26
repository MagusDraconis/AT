namespace AT.Core.ResearchDATA;

public sealed record DsInventory(string Name,string Instrument,string NGalaxies,string Zmin,string Zmax,string Kinematics,string Status,string Notes);
public sealed record InstMatrix(string Instrument,string Zmax,string SigmaV,string Resolution,string NGalaxies,string Year,string Priority);
public sealed record GalaxyTarget(string Type,string Zrange,string Advantage,string Disadvantage,string NExpected,string Priority);
public sealed record SampleReq(string Sigma,string NGalaxies,string NAvailable,string Year,string Dataset,string Feasibility);
public sealed record FalsifyPath(string Observation,string Result,string Implication,string Timeline,string Priority);
public sealed record RoadmapPhase(int Phase,string Period,string Activity,string Deliverable,string Milestone);
public sealed record ExecResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,DsInventory[] DI,InstMatrix[] IM,GalaxyTarget[] GT,SampleReq[] SR,FalsifyPath[] FP,RoadmapPhase[] RP);