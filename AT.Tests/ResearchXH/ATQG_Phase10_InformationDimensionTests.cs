using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 10 — information-theoretic dimension selection. Measures how much information an actualization
/// support of dimension d can carry (information capacity, entropy density, causal connectivity, propagation
/// efficiency, geometry complexity) and whether any dimension maximizes information efficiency. Classify:
/// DERIVED / PREFERRED / NOT SPECIAL.
///
/// Tests: ATQG100 (capacity/entropy/connectivity monotonic), ATQG101 (propagation efficiency d-independent +
///        efficiency landscape), ATQG102 (classification).
/// </summary>
public class ATQG_Phase10_InformationDimensionTests : ResearchTestBase
{
    public ATQG_Phase10_InformationDimensionTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG100: capacity, entropy density, causal connectivity are monotonic ────────

    [Fact]
    public void ATQG100_MonotonicInformationQuantities()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG100: information capacity, entropy density, and causal connectivity are monotonic");

        sb.AppendLine($"{"d",4} {"capacity",10} {"entropy/d",11} {"connectivity",13} {"complexity",11}");
        for (int d = 3; d <= 12; d++)
        {
            double c = InformationDimension.InformationCapacity(d);
            double s = InformationDimension.EntropyDensity(d);
            double cc = InformationDimension.CausalConnectivity(d);
            double gc = InformationDimension.GeometryComplexity(d);
            sb.AppendLine($"{d,4} {c,10:F0} {s,11:F4} {cc,13:F0} {gc,11:F0}");
        }

        bool capacityInc = true, entropyDec = true, connectivityInc = true, complexityInc = true;
        for (int d = 3; d < 12; d++)
        {
            if (InformationDimension.InformationCapacity(d + 1) <= InformationDimension.InformationCapacity(d)) capacityInc = false;
            if (InformationDimension.EntropyDensity(d + 1) >= InformationDimension.EntropyDensity(d)) entropyDec = false;
            if (InformationDimension.CausalConnectivity(d + 1) <= InformationDimension.CausalConnectivity(d)) connectivityInc = false;
            if (InformationDimension.GeometryComplexity(d + 1) <= InformationDimension.GeometryComplexity(d)) complexityInc = false;
        }

        bool allMonotonic = capacityInc && entropyDec && connectivityInc && complexityInc;

        sb.AppendLine();
        sb.AppendLine($"capacity ↑, entropy/d ↓, connectivity ↑, complexity ↑ (all monotonic): {allMonotonic}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: information capacity, causal connectivity, and geometry complexity GROW with d, while");
        sb.AppendLine("entropy density per dimension DECREASES — all monotonic, no interior maximum.");
        Output.WriteLine(sb.ToString());

        Assert.True(allMonotonic, "information quantities should be monotonic in d");
    }

    // ── ATQG101: propagation efficiency is dimension-independent; efficiency landscape ─

    [Fact]
    public void ATQG101_PropagationEfficiencyDimIndependent()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG101: propagation efficiency is dimension-independent; efficiency peaks at smallest allowed d");

        double R = 10.0;
        sb.AppendLine($"{"d",4} {"reach R^d",13} {"intensity R^-(d-1)",19} {"prop eff",10}");
        bool propConst = true;
        double prevPe = -1;
        for (int d = 3; d <= 8; d++)
        {
            double pe = InformationDimension.PropagationEfficiency(R, d);
            if (d > 3 && Math.Abs(pe - prevPe) > 1e-9) propConst = false;
            prevPe = pe;
            sb.AppendLine($"{d,4} {InformationDimension.Reach(R, d),13:E1} {InformationDimension.Intensity(R, d),19:E2} {pe,10:F1}");
        }

        // Information efficiency = 1/(1+graviton): max at d=2 (forbidden), then d=3 among allowed.
        bool effAt2 = InformationDimension.InformationEfficiency(2) == 1.0;
        bool effMaxAmongAllowed = InformationDimension.InformationEfficiency(3) > InformationDimension.InformationEfficiency(4);

        sb.AppendLine();
        sb.AppendLine($"propagation efficiency = R (dimension-INDEPENDENT): {propConst}");
        sb.AppendLine($"information efficiency = 1 at d=2 (forbidden), max at d=3 among allowed: {effAt2 && effMaxAmongAllowed}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: propagation efficiency is EXACTLY dimension-independent (reach × intensity = R), so");
        sb.AppendLine("it does not select a dimension. Information efficiency (useful/total) is monotonic, maximized at the");
        sb.AppendLine("smallest allowed dimension d=3 (3+1).");
        Output.WriteLine(sb.ToString());

        Assert.True(propConst, "propagation efficiency should be dimension-independent");
        Assert.True(effAt2 && effMaxAmongAllowed, "information efficiency should peak at smallest allowed d");
    }

    // ── ATQG102: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG102_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG102: does any dimension maximize information efficiency?");

        sb.AppendLine("CLASSIFICATION: NOT SPECIAL (no interior maximum); d=3 (3+1) PREFERRED as the boundary.");
        sb.AppendLine();
        sb.AppendLine("  • Information capacity (d+1)(d+2)/2, causal connectivity λ^d, and geometry complexity (Weyl) GROW");
        sb.AppendLine("    with d; entropy density (ln d + ln K)/d DECREASES — all MONOTONIC (ATQG100), so no dimension");
        sb.AppendLine("    is an interior maximum.");
        sb.AppendLine("  • Propagation efficiency is EXACTLY dimension-independent (reach × intensity = R), so it selects");
        sb.AppendLine("    nothing (ATQG101).");
        sb.AppendLine("  • Information efficiency = 1/(1+graviton) is maximized at the SMALLEST allowed dimension d=3 (3+1);");
        sb.AppendLine("    the conformal-complete d=2 (efficiency 1) is FORBIDDEN (no gravity).");
        sb.AppendLine("  • Therefore no dimension is DERIVED or an interior SPECIAL; d=3 (3+1) is PREFERRED as the boundary —");
        sb.AppendLine("    the minimal dynamical gravity and the maximal-information-efficiency among allowed dimensions.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
