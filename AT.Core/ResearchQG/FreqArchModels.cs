namespace AT.Core.ResearchQG;

public sealed record ArchType(string Type,string Frequencies,string PhaseRel,string Stability,string GravityEffect,string Status);
public sealed record SameEnergy(string System,string Architecture,string Mass,string Stability,string Gravity,string Status);
public sealed record PartArch(string Particle,string OmegaHz,string Architecture,string Protection,string Status);
public sealed record FArchResult(string SA,string SB,string SC,string SD,string SEct,string SF,string SG,string SH,string SI,ArchType[] AT,SameEnergy[] SE,PartArch[] PA);