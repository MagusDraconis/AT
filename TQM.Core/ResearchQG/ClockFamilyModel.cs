namespace TQM.Core.ResearchQG;

/// <summary>QG-083 clock-family model records. A clock family i has rate
/// γ_i(z) = a(z)·(1+ε_i(z)); the atomic clock defines z (ε_A = 0).</summary>
public sealed record ClockFamily(string Name, string Symbol, string Mechanism, double MaxDriftOverZ03);

/// <summary>One experimental bound on the drift between two clock families.</summary>
public sealed record ClockDriftConstraint(string Probe, string ClockPair, string Constraint, string Basis);

/// <summary>Consistency-matrix cell: max allowed |γ_i/γ_j − 1| over z=0→3.</summary>
public sealed record ClockConsistencyCell(string RowClock, string ColClock, double MaxDrift, string Probe);

/// <summary>g† sensitivity of one clock family: g†_i = cH/2π·(1+dε_i/d ln a).</summary>
public sealed record GdaggerSensitivityRow(string ClockFamily, double DriftDex, double DEpsilonDLnA,
    double CorrectionFactor, double GdaggerLocal_m_s2, double GdaggerZ3_m_s2);

/// <summary>Aggregate QG-083 report.</summary>
public sealed record ClockNonUniversalityReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG,
    ClockFamily[] Families, ClockConsistencyCell[] Matrix, GdaggerSensitivityRow[] Sensitivity, string OutDir);
