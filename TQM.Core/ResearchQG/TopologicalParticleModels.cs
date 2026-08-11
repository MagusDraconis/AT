namespace TQM.Core.ResearchQG;

public sealed record TopoSector(int WindingNumber,double RelEnergy,string Stability,string DecayChannel,string ParticleCandidate,string Status);
public sealed record ParticleMap(string Particle,double Mass_MeV,int Charge,int Spin2,string TopologicalStructure,string Confidence);
public sealed record SpecResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,TopoSector[] Sectors,ParticleMap[] Particles);
