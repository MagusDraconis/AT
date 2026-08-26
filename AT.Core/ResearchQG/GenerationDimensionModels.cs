namespace AT.Core.ResearchQG;

public sealed record GenDimension(int N,int MixingAngles,int CPPhases,string SymmetryGroup,string Baryogenesis,string ObservationalStatus,string Verdict);
public sealed record DimMechanism(string Mechanism,string Selects3,string GivesBound,string Status);
public sealed record GDRResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,GenDimension[] Dimensions,DimMechanism[] Mechanisms);
