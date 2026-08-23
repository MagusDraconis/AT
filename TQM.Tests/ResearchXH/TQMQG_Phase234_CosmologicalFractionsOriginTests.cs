using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 234 — Cosmological Density Fractions Origin. Derive Ω_Λ and Ω_m from the counting measure:
/// vacuum actualization fraction, deficit-matter fraction, critical branching balance, attractor
/// equilibrium. No new primitives, deterministic. Closes QG233's last open parameters.
/// </summary>
public class TQMQG_Phase234_CosmologicalFractionsOriginTests : ResearchTestBase
{
    public TQMQG_Phase234_CosmologicalFractionsOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2340_OctaveRecordInformation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2340: the realized octave record [4,4,87] and its information content");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The actualization record is the D96 octave spectrum [4,4,87] (95 modes, QG210).");
        sb.AppendLine("  - Its information content relative to uniform is I_occ = KL(p‖uniform) (QG228).");
        sb.AppendLine();

        var occ = CosmologicalFractionsOrigin.OctaveOccupancies();
        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Octave occupancies: [{string.Join(", ", occ)}] (total {occ.Sum()})");
        sb.AppendLine($"  Octave (family) count K = {CosmologicalFractionsOrigin.OctaveCount()}");
        sb.AppendLine($"  I_occ = KL(p‖uniform) = {CosmologicalFractionsOrigin.RecordInformation():F6} nats");
        sb.AppendLine($"  Max information ln K = {CosmologicalFractionsOrigin.MaxInformation(3):F6} nats");
        sb.AppendLine($"  Record from the attractor (3 octaves, 95 modes)? {CosmologicalFractionsOrigin.RecordFromAttractor()}");
        sb.AppendLine($"  Max entropy from octave count? {CosmologicalFractionsOrigin.MaxEntropyFromOctaveCount()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The realized record's information density is I_occ = 0.7513 nats over the maximum");
        sb.AppendLine("    possible ln 3 = 1.0986 nats — a well-defined fraction of the information capacity.");
        sb.AppendLine("  - Both the record and the maximum are derived from the D96 attractor geometry.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(3, CosmologicalFractionsOrigin.OctaveCount());
        Assert.Equal(95, occ.Sum());
        Assert.True(CosmologicalFractionsOrigin.RecordInformation() > 0.0, "the record must carry information");
        Assert.True(CosmologicalFractionsOrigin.RecordFromAttractor(), "the record must come from the attractor");
        Assert.True(CosmologicalFractionsOrigin.MaxEntropyFromOctaveCount(), "the max entropy must come from the octave count");
    }

    [Fact]
    public void TQMQG2341_TheFractions()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2341: Ω_Λ = I_occ/ln K and Ω_m = 1 − Ω_Λ");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Ω_Λ is the vacuum actualization fraction = I_occ/ln K (the residual information");
        sb.AppendLine("    density as a fraction of the maximum).");
        sb.AppendLine("  - Ω_m = 1 − Ω_Λ (the deficit matter is the complement in the single-scale R universe).");
        sb.AppendLine();

        double oL = CosmologicalFractionsOrigin.VacuumFraction();
        double om = CosmologicalFractionsOrigin.MatterFraction();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Ω_Λ = I_occ/ln K = {oL:F6}");
        sb.AppendLine($"      observed 0.6847 → dev {CosmologicalFractionsOrigin.VacuumDeviation():P2}");
        sb.AppendLine($"  Ω_m = 1 − Ω_Λ = {om:F6}");
        sb.AppendLine($"      observed 0.3153 → dev {CosmologicalFractionsOrigin.MatterDeviation():P2}");
        sb.AppendLine($"  Ω_Λ + Ω_m = {oL + om:F10} (flatness identity)");
        sb.AppendLine($"  Vacuum fraction bounded in (0,1)? {CosmologicalFractionsOrigin.VacuumFractionBounded()}");
        sb.AppendLine($"  Flatness identity holds? {CosmologicalFractionsOrigin.FlatnessIdentity()}");
        sb.AppendLine($"  No imports (no Planck-fit / ΛCDM inputs)? {CosmologicalFractionsOrigin.NoImports()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Ω_Λ = 0.6839 (dev 0.12% from Planck) and Ω_m = 0.3161 (dev 0.26%) are derived from");
        sb.AppendLine("    the counting measure's octave-record information — no fitted values.");
        sb.AppendLine("  - The flatness identity Ω_Λ + Ω_m = 1 is the single-scale R structure (Λ ~ ρ̄, QG230).");

        Output.WriteLine(sb.ToString());

        Assert.True(CosmologicalFractionsOrigin.VacuumFractionBounded(), "Ω_Λ must be in (0,1)");
        Assert.True(CosmologicalFractionsOrigin.VacuumFractionMatches(), "Ω_Λ must match the observed value");
        Assert.True(CosmologicalFractionsOrigin.MatterFractionMatches(), "Ω_m must match the observed value");
        Assert.True(CosmologicalFractionsOrigin.FlatnessIdentity(), "Ω_Λ + Ω_m = 1 must hold exactly");
    }

    [Fact]
    public void TQMQG2342_ClassificationFractionOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2342: classification — FRACTION ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The full chain: octave record → I_occ (QG228) → Ω_Λ = I_occ/ln K → Ω_m = 1 − Ω_Λ.");
        sb.AppendLine("  - The observed values are comparison anchors, never inputs.");
        sb.AppendLine();

        int score = CosmologicalFractionsOrigin.OriginScore();
        string classification = CosmologicalFractionsOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Ω_Λ = {CosmologicalFractionsOrigin.VacuumFraction():F6}  (observed 0.6847)");
        sb.AppendLine($"  Ω_m = {CosmologicalFractionsOrigin.MatterFraction():F6}  (observed 0.3153)");
        sb.AppendLine($"  Origin score (max 5) = {score}");
        sb.AppendLine($"    +1 record + information derived ({CosmologicalFractionsOrigin.RecordInformation() > 0.0})");
        sb.AppendLine($"    +1 max entropy from octave count ({CosmologicalFractionsOrigin.MaxEntropyFromOctaveCount()})");
        sb.AppendLine($"    +1 Ω_Λ matches ({CosmologicalFractionsOrigin.VacuumFractionMatches()})");
        sb.AppendLine($"    +1 Ω_m matches ({CosmologicalFractionsOrigin.MatterFractionMatches()})");
        sb.AppendLine($"    +1 flatness + no imports ({CosmologicalFractionsOrigin.FlatnessIdentity()})");
        sb.AppendLine($"  Full chain holds? {CosmologicalFractionsOrigin.FractionChainHolds()}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Ω_Λ = I_occ/ln K: the information-density fraction of the D96 octave record");
        sb.AppendLine("    [4,4,87] (0.7513/1.0986 nats = 0.6839, dev 0.12%);");
        sb.AppendLine("  - Ω_m = 1 − Ω_Λ = 0.3161 (dev 0.26%), fixed by the single-scale flatness identity;");
        sb.AppendLine("  - No Planck-fit values, no ΛCDM inputs, no observationally tuned fractions.");
        sb.AppendLine($"  ⇒ {classification} — this closes QG233's last open parameters (Ω_Λ and Ω_m).");

        Output.WriteLine(sb.ToString());

        Assert.Equal("FRACTION ORIGIN", classification);
        Assert.Equal(5, score);
        Assert.True(CosmologicalFractionsOrigin.FractionChainHolds(), "the full derivation chain must hold");
    }
}
