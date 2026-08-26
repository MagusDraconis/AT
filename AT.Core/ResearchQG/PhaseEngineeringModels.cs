namespace AT.Core.ResearchQG;

public sealed record PhEngControl(string Variable,string Controls,string Coupling,string Feasibility,string Status);
public sealed record CoherenceReq(string Scale,string Energy,string Curvature,string Detectable,string Status);
public sealed record GravResp(string Manipulation,string Effect,string Magnitude,string Testable,string Status);
public sealed record PEResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,PhEngControl[] PC,CoherenceReq[] CR,GravResp[] GR);