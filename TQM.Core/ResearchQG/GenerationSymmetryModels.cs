namespace TQM.Core.ResearchQG;

public sealed record GenCount(int N,string CKMPhases,string CPViolation,string Baryogenesis,string ObservationStatus,string Verdict);
public sealed record S3Decomp(double SingletMag,double DoubletMag,double TotalMag,double CosTheta,double AngleDeg,string Balanced);
public sealed record GSAResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,GenCount[] GenCounts,S3Decomp Decomp);
