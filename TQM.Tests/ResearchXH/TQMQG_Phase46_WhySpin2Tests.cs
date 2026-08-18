using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 46 — why spin-2? Tests which spin is uniquely selected for the minimal extension.
/// Classify: DERIVED / PREFERRED / POSTULATED.
///
/// Tests: TQMQG460 (spin viability), TQMQG461 (three constraints), TQMQG462 (classification).
/// </summary>
public class TQMQG_Phase46_WhySpin2Tests : ResearchTestBase
{
    public TQMQG_Phase46_WhySpin2Tests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG460: only spin-2 survives all three constraints ─────────────────────────

    [Fact]
    public void TQMQG460_OnlySpin2Survives()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG460: which spin passes polarization + attraction + light bending?");

        for (int s = 0; s <= 2; s++)
        {
            bool pol = WhySpin2.TwoPolarizations(s);
            bool attr = WhySpin2.UniversalAttraction(s);
            bool bend = WhySpin2.CorrectLightBending(s);
            bool viable = WhySpin2.Viable(s);
            sb.AppendLine($"spin {s}: 2-polarizations={pol}  attraction={attr}  light-bending={bend}  -> VIABLE: {viable}");
        }

        int selected = WhySpin2.SelectedSpin();

        sb.AppendLine();
        sb.AppendLine($"selected spin: {selected}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: spin-0 fails polarization (1 helicity) and light bending (couples to trace T); spin-1 fails");
        sb.AppendLine("attraction (repulsive). Only spin-2 passes all three constraints.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(2, selected);
        Assert.False(WhySpin2.Viable(0), "spin-0 should not be viable");
        Assert.False(WhySpin2.Viable(1), "spin-1 should not be viable");
        Assert.True(WhySpin2.Viable(2), "spin-2 should be viable");
    }

    // ── TQMQG461: the three independent constraints ───────────────────────────────────

    [Fact]
    public void TQMQG461_ThreeConstraints()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG461: three independent arguments force spin-2");

        sb.AppendLine("(1) POLARIZATION: 2 observed GW polarizations (h_+, h_×)");
        sb.AppendLine($"    spin-0 has 1 helicity (ruled out): {!WhySpin2.TwoPolarizations(0)}");
        sb.AppendLine();
        sb.AppendLine("(2) ATTRACTION: gravity is universally attractive → even spin");
        sb.AppendLine($"    spin-1 (odd) is repulsive (ruled out): {!WhySpin2.UniversalAttraction(1)}");
        sb.AppendLine();
        sb.AppendLine("(3) LIGHT BENDING: correct deflection needs the full rank-2 stress-energy T_μν");
        sb.AppendLine($"    spin-0 couples to trace T (vanishes for light, ruled out): {!WhySpin2.CorrectLightBending(0)}");
        sb.AppendLine();
        sb.AppendLine("Each constraint independently eliminates one spin; together they leave spin-2 alone.");
        Output.WriteLine(sb.ToString());

        Assert.True(!WhySpin2.TwoPolarizations(0), "spin-0 should fail polarization");
        Assert.True(!WhySpin2.UniversalAttraction(1), "spin-1 should fail attraction");
        Assert.True(!WhySpin2.CorrectLightBending(0), "spin-0 should fail light bending");
    }

    // ── TQMQG462: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG462_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG462: DERIVED / PREFERRED / POSTULATED?");

        sb.AppendLine($"CLASSIFICATION: {WhySpin2.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: ψ is a new primitive (QG23/24); its spin is not derived from TQM's scalar sector.");
        sb.AppendLine("  • NOT A BARE POSTULATE: spin-2 is not an arbitrary choice — it is UNIQUELY selected by three independent");
        sb.AppendLine("    observational constraints (2 polarizations, universal attraction, correct light bending).");
        sb.AppendLine("  • PREFERRED: among spin-0/1/2, only spin-2 satisfies all three; it is the unique viable spin for gravity.");
        sb.AppendLine();
        sb.AppendLine("So the minimal extension is spin-2 because no other spin can reproduce the observed GWs, attraction, and");
        sb.AppendLine("light bending simultaneously.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PREFERRED", WhySpin2.Classify());
        Assert.Equal(2, WhySpin2.SelectedSpin());
    }
}
