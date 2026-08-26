namespace AT.Core.ResearchQG;

public sealed record FreqLevel(string Structure,double OmegaHz,double RatioToFund,double Energy,string Mechanism,string Status);
public sealed record FreqCascade(int Step,string From,string To,string Ratio,string Mechanism,string Status);
public sealed record FResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,FreqLevel[] FL,FreqCascade[] FC);