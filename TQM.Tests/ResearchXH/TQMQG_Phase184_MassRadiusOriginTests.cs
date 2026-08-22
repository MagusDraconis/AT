using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 184 — Mass-radius origin. Known: QG12 (S ∝ Area from boundary counting), QG13
/// (compact-void E ∝ R^d gives T ∝ R — Hawking fails). This phase derives the OBSERVED M ∝ R mass-radius
/// relation from the TRM/D96 counting measure — no new primitives, deterministic.
///
/// Tests: TQMQG1840 (the per-octave deficit gives M ∝ R), TQMQG1841 (QG13's volume assignment was the
/// compact-void assumption; the counting-measure deficit is per-octave), TQMQG1842 (Hawking restored via
/// first law + classification).
/// </summary>
public class TQMQG_Phase184_MassRadiusOriginTests : ResearchTestBase
{
    public TQMQG_Phase184_MassRadiusOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1840_PerOctaveDeficitGivesMassProportionalToRadius()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1840: the per-octave deficit gives M ∝ R (radius-proportional mass)");

        sb.AppendLine("ASSUMPTIONS: the gravitational mass is the FIELD-DEFINED mass GM_eff = −a·R²");
        sb.AppendLine("(the field a = −(1/d)·ρ′/ρ at radius R), NOT the enclosed deficit volume. For the");
        sb.AppendLine("per-octave (log) deficit ρ = ρ̄ − m₀·ln(Rmax/r)/L — the SAME profile that gives flat");
        sb.AppendLine("rotation curves in G4ME — ρ′ = m₀/(r·L), so a ∝ −1/r and GM_eff ∝ R.");
        sb.AppendLine();
        sb.AppendLine("THE PER-OCTAVE (LOG) DEFICIT:");
        sb.AppendLine($"  L = ln(Rmax/r₀) = {MassRadiusOrigin.LogRange():F4}");
        sb.AppendLine($"  ρ′ = m₀/(r·L) = 0.4/(r·{MassRadiusOrigin.LogRange():F4})  →  a ∝ −1/r");
        sb.AppendLine();
        sb.AppendLine("FIELD-DEFINED MASS GM_eff(R) = −a·R²:");
        sb.AppendLine("  " + "R".PadLeft(4) + "  " + "GM_eff (numeric)".PadRight(18) + " " + "GM_eff (linear)".PadRight(18) + " " + "scaling exp".PadRight(12));
        foreach (double R in new[] { 1.0, 2.0, 4.0, 8.0 })
        {
            double num = MassRadiusOrigin.GravitationalMass(R);
            double lin = MassRadiusOrigin.LinearMass(R);
            double exp = MassRadiusOrigin.ScalingExponent(R);
            sb.AppendLine($"  {R,4:F0} {num,-18:F6} {lin,-18:F6} {exp,-12:F4}");
        }
        sb.AppendLine();
        sb.AppendLine("  scaling exponent ~1 ⇒ M ∝ R (radius); a compact void would give ~3 (volume);");
        sb.AppendLine("  a point mass would give ~0.");
        sb.AppendLine($"  M ∝ R: {MassRadiusOrigin.MassScalesWithRadius()}");
        Output.WriteLine(sb.ToString());

        Assert.True(MassRadiusOrigin.MassScalesWithRadius(), "per-octave deficit should give M ∝ R");
        double e1 = MassRadiusOrigin.ScalingExponent(1.0);
        double e2 = MassRadiusOrigin.ScalingExponent(2.0);
        Assert.True(e1 > 0.7 && e1 < 1.1, "scaling exponent should be near 1");
        Assert.True(e2 > 0.7 && e2 < 1.1, "scaling exponent should be near 1");
    }

    [Fact]
    public void TQMQG1841_CompactVoidWasTheVolumeAssumption()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1841: QG13's volume assignment was the compact-void assumption");

        sb.AppendLine("ASSUMPTIONS: QG13 computed T = dE/dS with E ∝ R^d (volume). That assumed a COMPACT");
        sb.AppendLine("VOID — constant deficit inside R, zero outside — whose enclosed deficit volume is");
        sb.AppendLine("M = ρ̄·V(R) ∝ R^d. This is NOT the deficit the counting measure actually produces:");
        sb.AppendLine("the counting measure's deficit is PER-OCTAVE (one deficit unit per octave), the");
        sb.AppendLine("discrete form of the log-deficit (G4ME AnnularDeficit).");
        sb.AppendLine();
        sb.AppendLine("COMPACT VOID (QG13's volume assignment):");
        double t1 = HorizonThermodynamics.TemperatureDeficit(3, 1.0);
        double t2 = HorizonThermodynamics.TemperatureDeficit(3, 2.0);
        sb.AppendLine($"  E ∝ R^d → T = dE/dS = (3/2)·R:  T(R=1)={t1:F4}, T(R=2)={t2:F4}  (T GROWS — anti-Hawking)");
        sb.AppendLine($"  anti-Hawking (T grows with R): {MassRadiusOrigin.CompactVoidIsAntiHawking()}");
        sb.AppendLine();
        sb.AppendLine("THE COUNTING-MEASURE DEFICIT IS PER-OCTAVE:");
        var occ = EffectiveAccessCounts.OctaveOccupancies();
        sb.AppendLine($"  D96 octave bands = [{string.Join(",", occ)}] ({occ.Length} bands)");
        sb.AppendLine("  G4ME AnnularDeficit: constant deficit per octave m(r) = m₀·(K−k)/K");
        sb.AppendLine("  → continuum: log-deficit ρ = ρ̄ − m₀·ln(Rmax/r)/L");
        sb.AppendLine("  → field a ∝ −1/r → GM_eff ∝ R (M ∝ R)");
        sb.AppendLine();
        sb.AppendLine("  the volume (R^d) assignment in QG13 was an ASSUMPTION about the deficit profile;");
        sb.AppendLine("  the counting measure's actual per-octave deficit gives the radius-proportional");
        sb.AppendLine("  mass.");
        Output.WriteLine(sb.ToString());

        Assert.True(MassRadiusOrigin.CompactVoidIsAntiHawking(), "compact void should be anti-Hawking");
        Assert.True(t2 > t1, "compact-void temperature should grow with R");
    }

    [Fact]
    public void TQMQG1842_HawkingRestoredAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1842: Hawking restored via first law, and classification");

        sb.AppendLine("ASSUMPTIONS: with the mass-radius relation M ∝ R (TQMQG1840) and the horizon entropy");
        sb.AppendLine("S ∝ R^(d−1) (QG12 boundary counting), the first law T = dE/dS gives T ∝ 1/R^(d−2),");
        sb.AppendLine("i.e. T ∝ 1/R at d = 3 — Hawking restored with no new primitives.");
        sb.AppendLine();
        sb.AppendLine("FIRST LAW WITH E = GM ∝ R AND S ∝ R^(d−1):");
        sb.AppendLine("  " + "R".PadLeft(4) + "  " + "E∝R".PadRight(8) + " " + "S∝R²".PadRight(8) + " " + "T=dE/dS".PadRight(12) + " " + "T·R".PadRight(8));
        foreach (double R in new[] { 1.0, 2.0, 4.0, 8.0 })
        {
            double T = MassRadiusOrigin.HawkingTemperature(3, R);
            double tr = MassRadiusOrigin.TemperatureRadiusProduct(3, R);
            sb.AppendLine($"  {R,4:F0} {R,-8:F0} {R * R,-8:F0} {T,-12:F6} {tr,-8:F4}");
        }
        sb.AppendLine($"  T·R constant ⇒ T ∝ 1/R (Hawking): {MassRadiusOrigin.HawkingRestored()}");
        sb.AppendLine();
        sb.AppendLine("DEPENDENCY STRUCTURE:");
        sb.AppendLine("  counting measure → per-octave (log) deficit (G4ME flat-rotation-curve profile)");
        sb.AppendLine("    → field a ∝ −1/r → GM_eff = m₀·R/(d·L·ρ̄) ∝ R (M ∝ R)");
        sb.AppendLine("  boundary counting (QG12) → S ∝ R^(d−1) (area)");
        sb.AppendLine("  first law T = dE/dS → T ∝ 1/R (Hawking)");
        sb.AppendLine();
        int score = MassRadiusOrigin.OriginScore();
        string cls = MassRadiusOrigin.Classify();
        sb.AppendLine($"Mass-radius score (0..3): {score}");
        sb.AppendLine($"  +1 per-octave deficit gives M ∝ R: {MassRadiusOrigin.MassScalesWithRadius()}");
        sb.AppendLine($"  +1 compact-void volume assignment is anti-Hawking (explains QG13): {MassRadiusOrigin.CompactVoidIsAntiHawking()}");
        sb.AppendLine($"  +1 T ∝ 1/R with M ∝ R and S ∝ R^(d−1): {MassRadiusOrigin.HawkingRestored()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO ORIGIN rejected: the per-octave deficit reproduces M ∝ R with scaling");
        sb.AppendLine("    exponent ~1 and the linear formula matching the field mass.");
        sb.AppendLine("  • PARTIAL ORIGIN rejected: the mass-radius relation and the first law are");
        sb.AppendLine("    consistent, restoring Hawking T ∝ 1/R with no new primitives.");
        sb.AppendLine("  • MASS-RADIUS ORIGIN accepted: the observed M ∝ R relation EMERGES from the");
        sb.AppendLine("    counting measure — the deficit is per-octave (log, G4ME flat-rotation-curve");
        sb.AppendLine("    profile), giving a ∝ −1/r and GM_eff ∝ R; QG13's E ∝ R^d was the compact-void");
        sb.AppendLine("    assignment, not the counting-measure deficit. With S ∝ R^(d−1) (QG12),");
        sb.AppendLine("    T ∝ 1/R (Hawking) follows with no new primitives.");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 3, "mass-radius score should be maximal");
        Assert.Equal("MASS-RADIUS ORIGIN", cls);
    }
}
