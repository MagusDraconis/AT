using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 192 — Pre-Registered Sector-Ladder Spectrum. The full 12-rung ladder spectrum is LOCKED from
/// the ladder structure, the attractor spectrum, and D96 geometry (QG121–QG132) ONLY. Forbidden: collider
/// bumps, resonance catalogs, fitted energies. Deterministic.
/// </summary>
public class ATQG_Phase192_PreRegisteredLadderSpectrumTests : ResearchTestBase
{
    public ATQG_Phase192_PreRegisteredLadderSpectrumTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1920_FrozenLadderSpectrum()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1920: frozen 12-rung ladder spectrum (D96/QG121-132 only)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Inputs are the ladder structure, attractor spectrum, and D96 geometry (QG121-132).");
        sb.AppendLine("  - Z-anchor calibration: scale = MZ/6 = 15.198 GeV per radius unit (QG130).");
        sb.AppendLine("  - NO collider bump, resonance catalog, or fitted energy is used.");
        sb.AppendLine();

        var spec = PreRegisteredLadderSpectrum.FrozenSpectrum();
        var pred = PreRegisteredLadderSpectrum.PredictedResonancesGeV();

        sb.AppendLine("FROZEN LADDER SPECTRUM (Rung | Energy | Predicted?):");
        foreach (var f in spec)
            sb.AppendLine($"  {f.Rung,2} | {f.EnergyGeV,7:F2} GeV | predicted = {f.Predicted}");
        sb.AppendLine();
        sb.AppendLine("PREDICTED RESONANCES (ascending): " + string.Join(", ", pred.Select(p => $"{p:F2}")));
        sb.AppendLine($"  forbidden-input guard: {PreRegisteredLadderSpectrum.ForbiddenInputsNeverUsed()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(12, spec.Length);
        Assert.Equal(9, pred.Length);
        Assert.True(Math.Abs(pred[0] - 106.39) < 0.01, "primary predicted rung is 106.39 GeV");
        Assert.True(Math.Abs(pred[^1] - 263.43) < 0.01, "highest predicted rung is 263.43 GeV");
        Assert.True(PreRegisteredLadderSpectrum.ForbiddenInputsNeverUsed(),
            "no collider bump/catalog/fitted energy may enter");
    }

    [Fact]
    public void ATQG1921_MultiplicitiesWidthsProduction()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1921: multiplicities, widths, production ordering");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Multiplicities from the QG128 emitted-quantum spectrum (unit x10, top x1).");
        sb.AppendLine("  - Width scale = unit-quantum energy (15.20 GeV) — the metastable decay step (QG125).");
        sb.AppendLine("  - Production ordering by rung mass ascending; all below LHC13/FCC-hh (QG130).");
        sb.AppendLine();

        var mult = PreRegisteredLadderSpectrum.Multiplicities();
        double width = PreRegisteredLadderSpectrum.WidthScaleGeV();
        double frac = PreRegisteredLadderSpectrum.UnitQuantumFraction();
        var prod = PreRegisteredLadderSpectrum.ProductionOrderingGeV();

        sb.AppendLine("MULTIPLICITIES:");
        foreach (var m in mult)
            sb.AppendLine($"  {m.Quantum,-4} quantum: Δradius {m.RadiusDrop:F3} → {m.EnergyGeV:F2} GeV × {m.Multiplicity}");
        sb.AppendLine($"  unit-quantum fraction: {frac:F3}");
        sb.AppendLine();
        sb.AppendLine($"WIDTH SCALE (metastable decay step): {width:F2} GeV");
        sb.AppendLine($"ALL below LHC13 (13 TeV)? {PreRegisteredLadderSpectrum.AllBelowLhc13()}");
        sb.AppendLine($"ALL below FCC-hh (100 TeV)? {PreRegisteredLadderSpectrum.AllBelowFcchh()}");
        sb.AppendLine();
        sb.AppendLine("PRODUCTION ORDERING (ascending): " + string.Join(" → ", prod.Select(p => $"{p:F2}")));

        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(width - 15.20) < 0.01, "width scale is the 15.20 GeV unit quantum");
        Assert.True(frac > 0.9, "unit quantum dominates (fraction ≥ 0.9)");
        Assert.True(PreRegisteredLadderSpectrum.AllBelowLhc13(), "all rungs below LHC13");
        Assert.True(PreRegisteredLadderSpectrum.AllBelowFcchh(), "all rungs below FCC-hh");
    }

    [Fact]
    public void ATQG1922_OutputTableAndAcceptance()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1922: required output table and acceptance criteria");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - CONFIRMED: a new resonance matches a frozen rung energy (within 5%).");
        sb.AppendLine("  - FALSIFIED: sensitive searches exclude a frozen rung (limit below rung energy).");
        sb.AppendLine();

        var table = PreRegisteredLadderSpectrum.OutputTable();
        sb.AppendLine("REQUIRED OUTPUT TABLE (Rung | Energy | Expected visibility | Expected channel):");
        foreach (var t in table)
            sb.AppendLine($"  {t.Rung,2} | {t.EnergyGeV,7:F2} GeV | {t.Visibility,-42} | {t.Channel}");
        sb.AppendLine();
        sb.AppendLine("ACCEPTANCE CHECKS:");
        sb.AppendLine($"  CONFIRMED (new resonance at 106.4 GeV)?  {PreRegisteredLadderSpectrum.Confirmed(106.4)}");
        sb.AppendLine($"  CONFIRMED (new resonance at 136.8 GeV)?  {PreRegisteredLadderSpectrum.Confirmed(136.8)}");
        sb.AppendLine($"  CONFIRMED (95 GeV)?                      {PreRegisteredLadderSpectrum.Confirmed(95.0)}");
        sb.AppendLine($"  FALSIFIED (rung 106.39, limit 100 GeV)?  {PreRegisteredLadderSpectrum.Falsified(106.39, 100.0)}");
        sb.AppendLine($"  Classification                         = {PreRegisteredLadderSpectrum.Classify()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(12, table.Length);
        Assert.Equal(9, table.Count(t => t.Visibility.Contains("searchable")));
        Assert.True(PreRegisteredLadderSpectrum.Confirmed(106.4), "resonance at 106.4 GeV matches frozen rung");
        Assert.True(PreRegisteredLadderSpectrum.Confirmed(136.8), "resonance at 136.8 GeV matches frozen rung");
        Assert.False(PreRegisteredLadderSpectrum.Confirmed(95.0), "95 GeV is not a frozen rung");
        Assert.True(PreRegisteredLadderSpectrum.Falsified(106.39, 100.0), "limit below a frozen rung falsifies");
        Assert.Equal("PRE-REGISTERED", PreRegisteredLadderSpectrum.Classify());
    }
}
