using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 13 — horizon thermodynamics. Tests whether a Hawking-like temperature T ∝ 1/R emerges from the
/// first law T = dE/dS, using the counting-measure entropy S ∝ R^(d−1) and the deficit energy E ∝ R^d.
/// Classify: MATCH / PARTIAL MATCH / NO MATCH.
///
/// Tests: ATQG130 (entropy gradient), ATQG131 (first-law temperature contrast), ATQG132 (classification).
/// </summary>
public class ATQG_Phase13_HorizonThermodynamicsTests : ResearchTestBase
{
    public ATQG_Phase13_HorizonThermodynamicsTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG130: entropy and its gradient ───────────────────────────────────────────

    [Fact]
    public void ATQG130_EntropyAndGradient()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG130: S ∝ R^(d−1) and dS/dR ∝ R^(d−2)");

        int d = 3;
        sb.AppendLine($"{"R",6} {"S ∝ R²",10} {"dS/dR ∝ R",12}");
        for (double R = 1.0; R <= 8.0; R *= 2.0)
        {
            sb.AppendLine($"{R,6:F0} {HorizonThermodynamics.Entropy(d, R),10:F0} {HorizonThermodynamics.EntropyGradient(d, R),12:F1}");
        }

        bool entropyArea = Math.Abs(HorizonThermodynamics.Entropy(d, 2.0) / HorizonThermodynamics.Entropy(d, 1.0) - 4.0) < 1e-9;
        bool gradientLinear = Math.Abs(HorizonThermodynamics.EntropyGradient(d, 2.0) / HorizonThermodynamics.EntropyGradient(d, 1.0) - 2.0) < 1e-9;

        sb.AppendLine();
        sb.AppendLine($"S ∝ R² (area, ratio 4): {entropyArea}");
        sb.AppendLine($"dS/dR ∝ R (ratio 2): {gradientLinear}");
        Output.WriteLine(sb.ToString());

        Assert.True(entropyArea, "entropy should scale as area");
        Assert.True(gradientLinear, "entropy gradient should scale linearly");
    }

    // ── ATQG131: first-law temperature — AT gives T ∝ R, Hawking gives T ∝ 1/R ─────

    [Fact]
    public void ATQG131_FirstLawTemperature()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG131: first law T = dE/dS — deficit energy gives T ∝ R, not T ∝ 1/R");

        int d = 3;
        sb.AppendLine($"{"R",6} {"T_deficit ∝ R",14} {"T_hawking ∝ 1/R",16}");
        for (double R = 1.0; R <= 8.0; R *= 2.0)
        {
            double td = HorizonThermodynamics.TemperatureDeficit(d, R);
            double th = HorizonThermodynamics.TemperatureHawking(d, R);
            sb.AppendLine($"{R,6:F0} {td,14:F2} {th,16:F3}");
        }

        // AT (E ∝ R^d, S ∝ R^(d−1)): T ∝ R (GROWS with R). Hawking (E ∝ R): T ∝ 1/R (DECREASES).
        double tdRatio = HorizonThermodynamics.TemperatureDeficit(d, 2.0) / HorizonThermodynamics.TemperatureDeficit(d, 1.0);
        double thRatio = HorizonThermodynamics.TemperatureHawking(d, 2.0) / HorizonThermodynamics.TemperatureHawking(d, 1.0);

        bool deficitTemperatureGrows = Math.Abs(tdRatio - 2.0) < 1e-9;      // T ∝ R → ratio 2
        bool hawkingTemperatureFalls = Math.Abs(thRatio - 0.5) < 1e-9;      // T ∝ 1/R → ratio 0.5

        sb.AppendLine();
        sb.AppendLine($"T_deficit(2R)/T_deficit(R) = {tdRatio:F2} (T ∝ R, grows): {deficitTemperatureGrows}");
        sb.AppendLine($"T_hawking(2R)/T_hawking(R) = {thRatio:F2} (T ∝ 1/R, falls): {hawkingTemperatureFalls}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: with AT's deficit energy E ∝ R^d (volume) and entropy S ∝ R^(d−1) (area), the first law");
        sb.AppendLine("gives T ∝ R — the OPPOSITE of Hawking's T ∝ 1/R. The Hawking scaling requires E ∝ R (Schwarzschild");
        sb.AppendLine("mass linear in radius), which AT's volume-scaled deficit does not provide.");
        Output.WriteLine(sb.ToString());

        Assert.True(deficitTemperatureGrows, "deficit energy should give T ∝ R");
        Assert.True(hawkingTemperatureFalls, "Schwarzschild energy should give T ∝ 1/R");
    }

    // ── ATQG132: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG132_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG132: can a Hawking-like temperature emerge? MATCH / PARTIAL / NO MATCH?");

        sb.AppendLine("CLASSIFICATION: NO MATCH for T ∝ 1/R (AT gives T ∝ R); entropy S ∝ Area is the MATCH (QG12).");
        sb.AppendLine();
        sb.AppendLine("  • The entropy S ∝ R^(d−1) (area) is correct (QG12), and its gradient dS/dR ∝ R^(d−2) is well-defined");
        sb.AppendLine("    (ATQG130).");
        sb.AppendLine("  • The first law T = dE/dS requires the energy E(R). AT's native deficit energy E ∝ R^d (volume) gives");
        sb.AppendLine("    T = d/(d−1)·R — the temperature GROWS with radius, the opposite of Hawking (ATQG131).");
        sb.AppendLine("  • Hawking's T ∝ 1/R requires the Schwarzschild mass relation E ∝ R (mass linear in radius), which is a");
        sb.AppendLine("    GR-specific relation NOT provided by AT's volume-scaled counting (mass = total deficit ∝ R^d).");
        sb.AppendLine("  • ROOT CAUSE: AT's counting measure makes 'mass' a VOLUME quantity (enclosed deficit), whereas");
        sb.AppendLine("    black-hole mass is a SURFACE/radius quantity (M ∝ R). A native T ∝ 1/R would require a holographic");
        sb.AppendLine("    mass definition (mass from horizon area, not enclosed volume) — not yet present.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
