namespace TQM.Core.ResearchQG;

public sealed record YukawaOrigin(string Interpretation,string PhysicalMeaning,string DerivesSpectrum,string Status);
public sealed record EliminationTest(string Attempt,string WhyFails,string Verdict);
public sealed record YOOResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,YukawaOrigin[] Origins,EliminationTest[] Eliminations);
