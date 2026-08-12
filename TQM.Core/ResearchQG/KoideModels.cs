namespace TQM.Core.ResearchQG;

public sealed record KoideCheck(double me,double mmu,double mtau,double SumMasses,double SumSqrt,double Ratio,double QDeviation,double AngleDeg,double Cos2Theta);
public sealed record Gen4Test(double m4_MeV,double SumMasses4,double SumSqrt4,double Ratio4,string Verdict);
public sealed record KoideResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,KoideCheck Check,Gen4Test[] Gen4Tests);
