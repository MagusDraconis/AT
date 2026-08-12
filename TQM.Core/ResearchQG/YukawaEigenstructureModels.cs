namespace TQM.Core.ResearchQG;

public sealed record EigenProperty(string Property,string Value,string DerivedOrFree,string Status);
public sealed record RandomSpectrum(string Quantity,string Observed,string RandomFrequency,string Exceptional);
public sealed record YEResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,EigenProperty[] Properties,RandomSpectrum[] RandomTests);
