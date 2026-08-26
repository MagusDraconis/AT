namespace AT.Core.ResearchQG;

public sealed record TimeLayer(string Layer,int Level,string Time,string EmergesFrom,string Status);
public sealed record TimeComp(string Scenario,string Tau,string ProperT,string CT,string Status);
public sealed record LocalC(string Condition,string L,string TauVal,string Cval,string Invariant);
public sealed record DualTime(string Aspect,string Fundamental,string Emergent,string Relationship,string Status);
public sealed record T18Result(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,TimeLayer[] TL,TimeComp[] TC,LocalC[] LC,DualTime[] DT);