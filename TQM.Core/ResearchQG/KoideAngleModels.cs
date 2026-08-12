namespace TQM.Core.ResearchQG;

public sealed record AngleScan(double ThetaDeg,double Q,double SingletFrac,double DoubletFrac,string Note);
public sealed record CoincidenceEstimate(double FractionAt1eMinus2,double ScaledTo1eMinus5,double LookElsewhere,string Assessment);
public sealed record KAResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,AngleScan[] Scan,CoincidenceEstimate Coincidence);
