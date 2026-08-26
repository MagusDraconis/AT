namespace AT.Core.ResearchQG;

public sealed record Prediction(string Claim,string Program,string SpecificValue,string TestStatus,string Type);
public sealed record Negative(string Phenomenon,string ProhibitedBy,string Status);
public sealed record Scorecard(string Category,int Count,string Notes);
public sealed record PIResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,Prediction[] Predictions,Negative[] Negatives,Scorecard[] Scorecards);
