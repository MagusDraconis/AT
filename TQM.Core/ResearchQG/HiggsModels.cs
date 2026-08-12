namespace TQM.Core.ResearchQG;

public sealed record MassOrigin(string Source,string Mechanism,string WhatItExplains,string WhatItDoesNot,string Status);
public sealed record HiggsRole(string Interpretation,string HiggsField,string HiggsBoson,string YukawaCouplings,string Compatibility);
public sealed record ParticleMass(string Particle,double Mass_GeV,double Yukawa,string TQMArchitecture,string HiggsRoleHere);
public sealed record HGRResult(string SA,string SB,string SC,string SD,string SE,string SF,string SG,string SH,string SI,MassOrigin[] Origins,HiggsRole[] Roles,ParticleMass[] Particles);
