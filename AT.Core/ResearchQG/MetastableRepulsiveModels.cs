namespace AT.Core.ResearchQG;

// Candidate metastable repulsive architecture
public sealed record RepArch(
    string Name, string Mechanism, double FormationEnergy_J, double Size_m,
    double Lifetime_s, string DecayMechanism, string Observable,
    string Status);

// Lifetime estimate for a given architecture
public sealed record Lifetime(
    string Name, double Size_m, double LightCrossingTime_s,
    double TopologicalBarrier_s, double PhysicalLifetime_s, string DominantDecay);

// Instability classification
public sealed record Instability(
    string Cause, string Mechanism, double Timescale_s, double EnergyGap_J,
    string Prevention, string Feasibility);

// Domain wall properties
public sealed record DomWall(
    string Type, double Width_m, double Tension_Jpm, double Stability,
    string DecayMode, string Observable);

// Topological protection
public sealed record TopoProt(
    string Structure, int WindingNumber, double BarrierHeight_J,
    double TunnelingLifetime_s, string IsProtected);

// Observability
public sealed record ObsSig(
    string Signature, string Mechanism, double ExpectedMagnitude,
    string DetectableBy, string Status);

// Result
public sealed record MRAResult(
    string SA, string SB, string SC, string SD, string SE, string SF,
    string SG, string SH, string SI, RepArch[] Candidates, Lifetime[] Lifetimes,
    Instability[] Instabilities, DomWall[] Walls, TopoProt[] Protections,
    ObsSig[] Signatures);
