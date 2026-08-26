using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 244 — Lagrangian Origin. Derive explicit field equations and Lagrangian structure from
/// D96. No new primitives, deterministic. Closes QG243's remaining Lagrangian-form partial.
/// </summary>
public class ATQG_Phase244_LagrangianOriginTests : ResearchTestBase
{
    public ATQG_Phase244_LagrangianOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2440_NoetherCurrentsAndGeneratorAlgebra()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2440: the Noether currents and the generator algebra (the Lagrangian inputs)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The D96 symmetries generate conserved Noether currents (QG89).");
        sb.AppendLine("  - The gauge field strength is the generator-algebra curl (structure constants from D96).");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Noether currents exist (QG89/QG243)? {LagrangianOrigin.NoetherCurrentsExist()}");
        sb.AppendLine($"  Conserved currents: {string.Join("; ", LagrangianOrigin.ConservedCurrents())}");
        sb.AppendLine($"  Generator algebra closes (su(2) + su(3))? {LagrangianOrigin.GeneratorAlgebraCloses()}");
        sb.AppendLine($"  Field strength: {LagrangianOrigin.FieldStrengthForm()}");
        sb.AppendLine($"  Gauge kinetic term: {LagrangianOrigin.GaugeKineticTerm()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The matter sources are the Noether currents of the D96 symmetries.");
        sb.AppendLine("  - The field strength is the generator-algebra curl — the structure constants come");
        sb.AppendLine("    from the D96 generator commutators (QG161).");

        Output.WriteLine(sb.ToString());

        Assert.True(LagrangianOrigin.NoetherCurrentsExist(), "the Noether currents must exist");
        Assert.True(LagrangianOrigin.GeneratorAlgebraCloses(), "the generator algebra must close");
    }

    [Fact]
    public void ATQG2441_TheLagrangianDensity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2441: the derived Lagrangian density L = −(1/4)F^aF^a + iψ̄γ^μD_μψ − mψ̄ψ");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The gauge kinetic term from the generator field strength; the matter term from the");
        sb.AppendLine("    covariant generator coupling + the actualization-flow energy (QG89).");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Covariant derivative: {LagrangianOrigin.CovariantDerivative()}");
        sb.AppendLine($"  Matter term: {LagrangianOrigin.MatterTerm()}");
        sb.AppendLine($"  LAGRANGIAN DENSITY: {LagrangianOrigin.LagrangianDensity()}");
        sb.AppendLine();
        sb.AppendLine($"  QED Lagrangian derived (Abelian, e = √(4π/137))? {LagrangianOrigin.QedLagrangianDerived()}");
        sb.AppendLine($"  Weak Lagrangian derived (su(2), g = √(4π·3/Σm))? {LagrangianOrigin.WeakLagrangianDerived()}");
        sb.AppendLine($"  Strong Lagrangian derived (su(3), g_s = √(4π·8/Σ√m))? {LagrangianOrigin.StrongLagrangianDerived()}");
        sb.AppendLine($"  All three Lagrangians derived? {LagrangianOrigin.AllLagrangiansDerived()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The Lagrangian is assembled from the D96 generator field strength + the covariant");
        sb.AppendLine("    generator coupling + the actualization-flow mass term.");
        sb.AppendLine("  - QED/weak/strong follow as the Abelian / su(2) / su(3) cases with D96 couplings.");

        Output.WriteLine(sb.ToString());

        Assert.True(LagrangianOrigin.QedLagrangianDerived(), "the QED Lagrangian must be derived");
        Assert.True(LagrangianOrigin.WeakLagrangianDerived(), "the weak Lagrangian must be derived");
        Assert.True(LagrangianOrigin.StrongLagrangianDerived(), "the strong Lagrangian must be derived");
        Assert.True(LagrangianOrigin.AllLagrangiansDerived(), "all three Lagrangians must be derived");
    }

    [Fact]
    public void ATQG2442_ClassificationLagrangianOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2442: classification — LAGRANGIAN ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The full density is derived from D96; the Higgs/Yukawa sector is the partial item.");
        sb.AppendLine();

        int score = LagrangianOrigin.OriginScore();
        string classification = LagrangianOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Origin score (max 5) = {score}");
        sb.AppendLine($"    +1 Noether currents ({LagrangianOrigin.NoetherCurrentsExist()})");
        sb.AppendLine($"    +1 generator algebra closes ({LagrangianOrigin.GeneratorAlgebraCloses()})");
        sb.AppendLine($"    +1 QED Lagrangian ({LagrangianOrigin.QedLagrangianDerived()})");
        sb.AppendLine($"    +1 weak + strong Lagrangians ({LagrangianOrigin.WeakLagrangianDerived()} / {LagrangianOrigin.StrongLagrangianDerived()})");
        sb.AppendLine($"    +1 no imports ({LagrangianOrigin.NoImports()})");
        sb.AppendLine($"  Higgs/Yukawa sector partial? {LagrangianOrigin.HiggsYukawaPartial()}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The Lagrangian density L = −(1/4)F^aF^a + iψ̄γ^μD_μψ − mψ̄ψ is DERIVED from the");
        sb.AppendLine("    D96 generator algebra (field strength), the generator action (covariant");
        sb.AppendLine("    coupling), and the actualization-flow energy (QG89).");
        sb.AppendLine("  - The QED/weak/strong field equations are its Euler-Lagrange equations with");
        sb.AppendLine("    D96-determined couplings (QG162).");
        sb.AppendLine("  - The Higgs/Yukawa sector (Higgs = collective occupation-density scalar, QG84) is");
        sb.AppendLine("    the partial item — the full Yukawa coupling structure is not re-derived.");
        sb.AppendLine($"  ⇒ {classification} — the explicit field equations and Lagrangian structure follow");
        sb.AppendLine("    from D96; no imported SM Lagrangian.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("LAGRANGIAN ORIGIN", classification);
        Assert.Equal(5, score);
        Assert.True(LagrangianOrigin.NoImports(), "no imported SM Lagrangian may be used");
    }
}
