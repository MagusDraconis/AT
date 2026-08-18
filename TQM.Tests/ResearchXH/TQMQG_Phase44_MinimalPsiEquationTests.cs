using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 44 — minimal ψ field equation. Determines the simplest dynamics consistent with observed ψ effects.
/// Classify: DERIVED / PREFERRED / POSTULATED.
///
/// Tests: TQMQG440 (the minimal form), TQMQG441 (classification), TQMQG442 (two-layer conclusion).
/// </summary>
public class TQMQG_Phase44_MinimalPsiEquationTests : ResearchTestBase
{
    public TQMQG_Phase44_MinimalPsiEquationTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG440: the minimal form ────────────────────────────────────────────────────

    [Fact]
    public void TQMQG440_MinimalForm()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG440: the massless spin-2 wave equation (Fierz-Pauli)");

        int d = 3;
        double helicities = MinimalPsiEquation.Helicities(d);
        double speed = MinimalPsiEquation.PropagationSpeed();
        bool weakField = MinimalPsiEquation.MatchesWeakFieldGr();

        sb.AppendLine($"□ψ_μν = 0  (massless wave equation, transverse-traceless)");
        sb.AppendLine($"spin-2 helicities at d=3: {helicities}  (h_+, h_×)");
        sb.AppendLine($"propagation speed: {speed}  (= c)");
        sb.AppendLine($"weak-field limit = linearized GR: {weakField}");

        bool minimal = helicities == 2.0 && speed == 1.0 && weakField;

        sb.AppendLine();
        sb.AppendLine($"minimal spin-2 dynamics (2 helicities, light-speed, linearized GR): {minimal}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the simplest ψ dynamics is the massless spin-2 wave equation — second-order, linear, with the");
        sb.AppendLine("two transverse-traceless helicities that reproduce the observed gravitational waves.");
        Output.WriteLine(sb.ToString());

        Assert.True(minimal, "the minimal form should be massless spin-2 with 2 helicities at light speed");
    }

    // ── TQMQG441: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG441_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG441: DERIVED / PREFERRED / POSTULATED?");

        bool derived = MinimalPsiEquation.Derived();
        bool preferred = MinimalPsiEquation.FormIsPreferred();
        bool postulated = MinimalPsiEquation.Postulated();

        sb.AppendLine($"DERIVED from TQM:    {derived}  (ψ is a new primitive)");
        sb.AppendLine($"form is PREFERRED:   {preferred}  (unique massless spin-2)");
        sb.AppendLine($"equation POSTULATED: {postulated}  (new input for the new primitive)");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {MinimalPsiEquation.Classify()}");
        sb.AppendLine();
        sb.AppendLine("The equation is POSTULATED (ψ's dynamics is a new input), but its SPECIFIC form — the massless spin-2 wave");
        sb.AppendLine("equation — is PREFERRED: it is the UNIQUE ghost-free, Lorentz-invariant, massless spin-2 theory.");
        Output.WriteLine(sb.ToString());

        Assert.False(derived, "psi dynamics should not be derivable");
        Assert.True(preferred, "the massless spin-2 form should be preferred");
        Assert.True(postulated, "the equation should be postulated");
    }

    // ── TQMQG442: two-layer conclusion ─────────────────────────────────────────────────

    [Fact]
    public void TQMQG442_TwoLayerConclusion()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG442: the minimal ψ dynamics, in full");

        sb.AppendLine("MINIMAL ψ FIELD EQUATION:");
        sb.AppendLine("  □ψ_μν = 0   (massless wave equation)");
        sb.AppendLine("  ∂^μ ψ_μν = 0   (transverse),   ψ^μ_μ = 0   (traceless)   →  2 helicities");
        sb.AppendLine("  action: S = ∫ ψ_μν □ ψ^μν   (the quadratic/Weyl action)");
        sb.AppendLine();
        sb.AppendLine("TWO-LAYER STATUS:");
        sb.AppendLine("  • PREFERRED (form): the unique ghost-free, Lorentz-invariant, massless spin-2 theory, matching the");
        sb.AppendLine("    observed light-speed two-polarization gravitational waves.");
        sb.AppendLine("  • POSTULATED (status): ψ is a new primitive (QG23/24/37), so its equation of motion is a new input,");
        sb.AppendLine("    not derived from TQM's scalar sector.");
        sb.AppendLine();
        sb.AppendLine("This is the final step of the QG arc: the minimal tensor extension is a massless spin-2 field with the");
        sb.AppendLine("Fierz-Pauli wave equation — one new primitive, one new equation, uniquely fixed by observation.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("POSTULATED", MinimalPsiEquation.Classify());
        Assert.True(MinimalPsiEquation.FormIsPreferred());
        Assert.Equal(2.0, MinimalPsiEquation.Helicities(3));
    }
}
