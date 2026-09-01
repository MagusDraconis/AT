using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 190 — Pre-Registered 106 GeV Resonance. The prediction is LOCKED before any future data:
/// central mass, uncertainty window, production hierarchy, decay hierarchy — all from D96 geometry / the
/// sector ladder / the octave structure / QG128–QG132 ONLY. Forbidden: ATLAS/CMS excess locations, fitted
/// resonance masses, new scaling constants. Deterministic.
/// </summary>
public class ATQG_Phase190_PreRegistered106GeVTests : ResearchTestBase
{
    public ATQG_Phase190_PreRegistered106GeVTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1900_CentralMassAndWindowFrozenFromD96()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1900: pre-registered central mass and uncertainty window (D96/QG128-132 only)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Inputs are D96 geometry, the sector ladder, the octave structure, and QG128-132 only.");
        sb.AppendLine("  - The Z-anchor electroweak calibration family (QG130/133): observable radius-6 sector on MZ.");
        sb.AppendLine("  - NO ATLAS/CMS excess location, fitted mass, or new scaling constant is used.");
        sb.AppendLine();

        double central = PreRegistered106GeV.CentralMassGeV();
        var (lo, hi) = PreRegistered106GeV.SearchWindowGeV();
        var missing = PreRegistered106GeV.MissingRungsGeV();
        double spacing = PreRegistered106GeV.RungSpacingGeV();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  ladder radii (QG128): {string.Join(", ", PreRegistered106GeV.LadderRadii)}");
        sb.AppendLine($"  Z-anchor scale = MZ/6 = {PreRegistered106GeV.RadiusScaleGeV():F4} GeV/radius");
        sb.AppendLine($"  missing (unobserved) rungs: {string.Join(", ", missing.Select(m => $"{m:F2}"))}");
        sb.AppendLine($"  mean rung spacing = {spacing:F2} GeV; half-spacing = {spacing / 2:F2} GeV");
        sb.AppendLine();
        sb.AppendLine("PRE-REGISTERED OUTPUTS:");
        sb.AppendLine($"  1. CENTRAL MASS = {central:F2} GeV   (lowest missing rung, rung 10, radius 7.0)");
        sb.AppendLine($"  2. UNCERTAINTY WINDOW = {lo:F2} – {hi:F2} GeV   (stated as 99–114 GeV)");
        sb.AppendLine($"  forbidden-input guard: {PreRegistered106GeV.ForbiddenInputsNeverUsed()}");

        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(central - 106.39) < 0.01, "central mass must be 106.39 GeV (D96-computed)");
        Assert.True(lo >= 98.0 && lo <= 99.5, "window low must be ≈99 GeV");
        Assert.True(hi >= 113.0 && hi <= 114.5, "window high must be ≈114 GeV");
        Assert.True(PreRegistered106GeV.ForbiddenInputsNeverUsed(),
            "no ATLAS/CMS excess or fitted mass may enter the prediction");
    }

    [Fact]
    public void ATQG1901_ProductionAndDecayHierarchyFrozen()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1901: pre-registered production and decay hierarchy");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Production hierarchy = the missing-rung masses (ascending), all below LHC13 reach.");
        sb.AppendLine("  - Decay hierarchy = the QG128 emitted-quantum spectrum (unit ×10, top ×1).");
        sb.AppendLine();

        var prod = PreRegistered106GeV.ProductionHierarchyGeV();
        var decay = PreRegistered106GeV.DecayHierarchy();
        double frac = PreRegistered106GeV.UnitQuantumFraction();
        var endpoint = PreRegistered106GeV.CascadeEndpoint();

        sb.AppendLine("PRE-REGISTERED PRODUCTION HIERARCHY (GeV, ascending):");
        foreach (double m in prod) sb.AppendLine($"    {m:F2}");
        sb.AppendLine($"  all within LHC13 (13 TeV)? {PreRegistered106GeV.AllPredictedWithinLhc13()}");
        sb.AppendLine($"  all within FCC-hh (100 TeV)? {PreRegistered106GeV.AllPredictedWithinFcchh()}");
        sb.AppendLine();
        sb.AppendLine("PRE-REGISTERED DECAY HIERARCHY:");
        foreach (var d in decay)
            sb.AppendLine($"    {d.Quantum} quantum: radius drop {d.RadiusDrop:F3} → {d.EnergyGeV:F2} GeV × {d.Multiplicity}");
        sb.AppendLine($"  unit-quantum fraction: {frac:F3}");
        sb.AppendLine($"  cascade endpoint: radius {endpoint.Radius:F0}, {endpoint.Families} families");

        Output.WriteLine(sb.ToString());

        Assert.True(PreRegistered106GeV.AllPredictedWithinLhc13(), "all predicted rungs must be below LHC13");
        Assert.True(PreRegistered106GeV.AllPredictedWithinFcchh(), "all predicted rungs must be below FCC-hh");
        Assert.True(frac > 0.9, "the unit quantum must dominate the decay spectrum (fraction ≥ 0.9)");
        Assert.Equal(3, endpoint.Families);
        Assert.True(prod[0] > 100 && prod[0] < 110, "the lightest predicted resonance is ~106 GeV");
    }

    [Fact]
    public void ATQG1902_AcceptanceCriteria()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1902: acceptance criteria (CONFIRMED / DISFAVORED)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - CONFIRMED: signal within the frozen window (99-114 GeV) with compatible production");
        sb.AppendLine("    pattern and 15-20 GeV decay quanta.");
        sb.AppendLine("  - DISFAVORED: no signal in statistically sensitive searches of the frozen window.");
        sb.AppendLine();

        var (lo, hi) = PreRegistered106GeV.SearchWindowGeV();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  frozen window: {lo:F2} – {hi:F2} GeV");
        sb.AppendLine($"  in-window check (106.4 GeV)?     {PreRegistered106GeV.InPreRegisteredWindow(106.4)}");
        sb.AppendLine($"  in-window check (95 GeV)?        {PreRegistered106GeV.InPreRegisteredWindow(95.0)}");
        sb.AppendLine($"  CONFIRMED (106.4, quantum 15.2)? {PreRegistered106GeV.Confirmed(106.4, 15.2)}");
        sb.AppendLine($"  CONFIRMED (95.0, quantum 15.2)?  {PreRegistered106GeV.Confirmed(95.0, 15.2)}");
        sb.AppendLine($"  DISFAVORED (null, σ<1)?          {PreRegistered106GeV.Disfavored(0.3)}");
        sb.AppendLine($"  Classification                  = {PreRegistered106GeV.Classify()}");

        Output.WriteLine(sb.ToString());

        // A signal at the frozen central mass with the unit quantum is CONFIRMED.
        Assert.True(PreRegistered106GeV.Confirmed(106.4, 15.2), "signal at 106.4 GeV with 15.2 GeV quanta is CONFIRMED");
        // A signal at 95 GeV (below the window) is NOT confirmed by this pre-registration.
        Assert.False(PreRegistered106GeV.Confirmed(95.0, 15.2), "95 GeV is outside the frozen window");
        // A null result (no excess) is DISFAVORED.
        Assert.True(PreRegistered106GeV.Disfavored(0.3), "no excess in the window is DISFAVORED");
        Assert.Equal("PRE-REGISTERED", PreRegistered106GeV.Classify());
    }
}
