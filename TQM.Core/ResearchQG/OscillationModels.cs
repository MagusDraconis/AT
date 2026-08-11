namespace TQM.Core.ResearchQG;

public sealed record OscRemove(string Removed,string WhatBreaks,string Severity,string Status);
public sealed record PhaseRole(string Aspect,string Mechanism,string Emerges,string Status);
public sealed record ResonStruct(string Structure,string OscillationRole,string Emerges,string Status);
public sealed record OscBridge(string Level,string EmergentEntity,string OscillationRole,string Status);
public sealed record OResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,OscRemove[] OR,PhaseRole[] PR,ResonStruct[] RS,OscBridge[] OB);