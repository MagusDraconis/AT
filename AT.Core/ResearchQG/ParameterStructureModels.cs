namespace AT.Core.ResearchQG;

public sealed record DerivedItem(string Quantity,string Category,string Derivation,string WhyDerivable);
public sealed record ResistantItem(string Quantity,double Value,string Category,string WhyResistant);
public sealed record PSResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,DerivedItem[] Derived,ResistantItem[] Resistant);
