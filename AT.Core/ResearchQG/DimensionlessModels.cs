namespace AT.Core.ResearchQG;

public sealed record DimlessQ(string Quantity,string Expression,string Value,string Independent,string Status);
public sealed record PiAppear(string Context,string Where,int Experiment,string Mechanism,string Coincidence,string Status);
public sealed record TopoStruct(string Structure,string Generates,string Why,string Status);
public sealed record FourierEmerge(string Aspect,string Produces,string Mechanism,string Status);
public sealed record HiddenCon(string Constraint,string Derivation,string Constrains,string Status);
public sealed record DimResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,DimlessQ[] DQ,PiAppear[] PA,TopoStruct[] TS,FourierEmerge[] FE,HiddenCon[] HC);