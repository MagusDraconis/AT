namespace AT.Core.ResearchQG;

public sealed record RankedPrediction(string Prediction,string Experiment,string Timeline,double FalsificationPower,double Feasibility,double PriorityScore,string Rank);
public sealed record PPRResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,RankedPrediction[] Ranking);
