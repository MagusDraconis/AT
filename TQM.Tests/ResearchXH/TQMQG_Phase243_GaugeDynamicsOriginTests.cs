using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 243 — Gauge Dynamics Origin. Derive interaction dynamics from the same D96 structure
/// that gives the symmetry groups. No new primitives, deterministic. Closes QG242's hosted/OPEN
/// dynamics items.
/// </summary>
public class TQMQG_Phase243_GaugeDynamicsOriginTests : ResearchTestBase
{
    public TQMQG_Phase243_GaugeDynamicsOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2430_GeneratorActionAndCouplings()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2430: the interaction IS the generator action; the couplings are D96-derived");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The gauge generators (QG161) act on the spectral modes; a gauge boson is a link");
        sb.AppendLine("    excitation (QG57) exchanged between modes.");
        sb.AppendLine("  - The coupling strengths are D96-normalized (QG162).");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Generator acts on modes (D96 automorphism)? {GaugeDynamicsOrigin.GeneratorActsOnModes()}");
        sb.AppendLine($"  Boson is a link excitation (QG57)? {GaugeDynamicsOrigin.BosonIsLinkExcitation()}");
        sb.AppendLine($"  1/α_em = {GaugeCouplingOrigin.InverseAlphaEm()} (137, QG162)");
        sb.AppendLine($"  e = √(4π/137) = {GaugeDynamicsOrigin.QedCoupling():F4}");
        sb.AppendLine($"  g = √(4π·3/95) = {GaugeDynamicsOrigin.WeakCoupling():F4}");
        sb.AppendLine($"  g_s = √(4π·8/Σ√m) = {GaugeDynamicsOrigin.StrongCoupling():F4}");
        sb.AppendLine($"  Couplings derived (1/α_em = 137, 12 generators)? {GaugeDynamicsOrigin.CouplingsDerived()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The interaction is the generator's action on the modes (lattice-gauge link, QG63/65).");
        sb.AppendLine("  - The couplings are D96-normalized — no imported SM values.");

        Output.WriteLine(sb.ToString());

        Assert.True(GaugeDynamicsOrigin.GeneratorActsOnModes(), "the generators must act on the modes");
        Assert.True(GaugeDynamicsOrigin.BosonIsLinkExcitation(), "a gauge boson must be a link excitation");
        Assert.True(GaugeDynamicsOrigin.CouplingsDerived(), "the couplings must be D96-derived");
    }

    [Fact]
    public void TQMQG2431_InteractionEquations()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2431: the three interaction equations — QED, weak, strong");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Each gauge generator is a conserved Noether current (QG89 symmetry conservation).");
        sb.AppendLine("  - The vertex is the generator matrix element; the equation is the current conservation.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Currents conserved (Noether)? {GaugeDynamicsOrigin.CurrentsConserved()}");
        sb.AppendLine($"  U(1) current conserved (QED)? {GaugeDynamicsOrigin.U1CurrentConserved()}");
        sb.AppendLine($"  SU(2) isospin conserved (algebra closes)? {GaugeDynamicsOrigin.Su2CurrentConserved()}");
        sb.AppendLine($"  SU(3) color conserved (3 families)? {GaugeDynamicsOrigin.Su3CurrentConserved()}");
        sb.AppendLine($"  QED equation derived? {GaugeDynamicsOrigin.QedEquationDerived()}");
        sb.AppendLine($"  Weak equation derived? {GaugeDynamicsOrigin.WeakEquationDerived()}");
        sb.AppendLine($"  Strong equation derived? {GaugeDynamicsOrigin.StrongEquationDerived()}");
        sb.AppendLine($"  All three equations derived? {GaugeDynamicsOrigin.AllEquationsDerived()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - QED: the U(1) phase-covariant conservation ∂_μ J^μ = 0 with e = √(4πα_em).");
        sb.AppendLine("  - Weak: the isospin-current conservation with g = √(4πα_weak).");
        sb.AppendLine("  - Strong: the color-current conservation with g_s = √(4πα_s).");

        Output.WriteLine(sb.ToString());

        Assert.True(GaugeDynamicsOrigin.QedEquationDerived(), "the QED equation must be derived");
        Assert.True(GaugeDynamicsOrigin.WeakEquationDerived(), "the weak equation must be derived");
        Assert.True(GaugeDynamicsOrigin.StrongEquationDerived(), "the strong equation must be derived");
        Assert.True(GaugeDynamicsOrigin.AllEquationsDerived(), "all three equations must be derived");
    }

    [Fact]
    public void TQMQG2432_ClassificationPartialOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2432: classification — PARTIAL ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The interaction structure (vertices, couplings, conservation) is derived from D96;");
        sb.AppendLine("    the explicit Lagrangian/propagator FORM remains hosted.");
        sb.AppendLine();

        int score = GaugeDynamicsOrigin.OriginScore();
        string classification = GaugeDynamicsOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Origin score (max 5) = {score}");
        sb.AppendLine($"    +1 generator action + link boson ({GaugeDynamicsOrigin.GeneratorActsOnModes()})");
        sb.AppendLine($"    +1 couplings derived ({GaugeDynamicsOrigin.CouplingsDerived()})");
        sb.AppendLine($"    +1 QED equation ({GaugeDynamicsOrigin.QedEquationDerived()})");
        sb.AppendLine($"    +1 weak + strong equations ({GaugeDynamicsOrigin.WeakEquationDerived()} / {GaugeDynamicsOrigin.StrongEquationDerived()})");
        sb.AppendLine($"    +1 no imports ({GaugeDynamicsOrigin.NoImports()})");
        sb.AppendLine($"  Lagrangian form hosted? {GaugeDynamicsOrigin.LagrangianFormHosted()}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The interaction DYNAMICS is derived from the D96 generator action: gauge bosons");
        sb.AppendLine("    = link excitations (QG57), vertices = generator matrix elements, couplings = QG162,");
        sb.AppendLine("    currents = Noether charges of the D96 symmetries.");
        sb.AppendLine("  - This substantially closes QG242's OPEN item (interaction vertices): the vertex IS");
        sb.AppendLine("    the generator matrix element.");
        sb.AppendLine("  - The explicit Lagrangian/propagator FORM remains hosted — hence PARTIAL, not DYNAMICS ORIGIN.");
        sb.AppendLine($"  ⇒ {classification} — the gauge dynamics is derived from the same D96 structure as");
        sb.AppendLine("    the symmetry groups; the Lagrangian form is the remaining partial item.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL ORIGIN", classification);
        Assert.Equal(5, score);
        Assert.True(GaugeDynamicsOrigin.AllEquationsDerived(), "all three interaction equations must be derived");
    }
}
