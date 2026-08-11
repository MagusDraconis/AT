namespace TQM.Core.ResearchQG;

public sealed record ScaleVar(string Scale,string VeryLarge,string Planck,string VerySmall,string Status);
public sealed record StabCheck(string Structure,string LargeL,string SmallL,string Precision,string Status);
public sealed record SelectMech(string Mechanism,string Selects,string Unique,string HonestAssessment,string Status);
public sealed record InfoDensity(string Bound,string LargeL,string Planck,string SmallL,string Status);
public sealed record FixedPt(string Candidate,string FixedPointValue,string Mechanism,string Plausible,string Status);
public sealed record PResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,ScaleVar[] SV,StabCheck[] STC,SelectMech[] SM,InfoDensity[] ID,FixedPt[] FP);