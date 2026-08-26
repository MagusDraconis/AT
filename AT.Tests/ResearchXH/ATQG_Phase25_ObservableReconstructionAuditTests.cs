using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 25 — observable reconstruction audit. Separates the OBSERVED EFFECT (its spin) from the GR
/// EXPLANATION (spin-2). Classify each observable: TENSOR REQUIRED / OBSERVABLE AMBIGUITY / UNDECIDED.
///
/// Tests: ATQG250 (spin census), ATQG251 (classification counts), ATQG252 (minimal d.o.f. refinement).
/// </summary>
public class ATQG_Phase25_ObservableReconstructionAuditTests : ResearchTestBase
{
    public ATQG_Phase25_ObservableReconstructionAuditTests(ITestOutputHelper o) : base(o) { }

    private static readonly (string name, string observed, string grExplanation)[] EffectTable =
    {
        ("lensing-deflection",   "a deflection angle",                "geodesic bending in a Weyl ≠ 0 metric"),
        ("time-delay",           "a time shift",                      "gravitational potential + path geometry"),
        ("magnification",        "a magnification factor",            "lensing Jacobian (convergence + shear)"),
        ("horizon-shadow",       "an angular size",                   "photon sphere of a non-conformal horizon"),
        ("hawking-temperature",  "a temperature",                     "surface gravity of a Schwarzschild horizon"),
        ("gw-strain",            "h_+ and h_x (two helicities)",       "transverse-traceless spin-2 perturbation"),
    };

    // ── ATQG250: separate observed effect from GR explanation ────────────────────────

    [Fact]
    public void ATQG250_SeparateEffectFromExplanation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG250: observed effect (spin) vs GR explanation (tensor)");

        int scalarCount = 0, tensorCount = 0;
        foreach (var (name, observed, gr) in EffectTable)
        {
            double spin = ObservableReconstructionAudit.ObservedEffectSpin(name);
            bool tensor = ObservableReconstructionAudit.RequiresTensor(name);
            sb.AppendLine($"{name,-20} measured = {observed,-34} (spin {spin})  GR explains via: {gr}");
            if (tensor) tensorCount++; else scalarCount++;
        }

        sb.AppendLine();
        sb.AppendLine($"scalar observed effects (spin 0): {scalarCount}  —  spin-2 observed effect: {tensorCount}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: five of the six 'failures' measure a SINGLE SCALAR quantity. Only the GW strain");
        sb.AppendLine("(h_+ and h_x, two helicities) is intrinsically spin-2 at the level of the measurement itself.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, scalarCount);
        Assert.Equal(1, tensorCount);
        Assert.True(ObservableReconstructionAudit.RequiresTensor("gw-strain"));
        Assert.False(ObservableReconstructionAudit.RequiresTensor("lensing-deflection"));
    }

    // ── ATQG251: classification counts ────────────────────────────────────────────────

    [Fact]
    public void ATQG251_ClassificationCounts()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG251: which failures are truly tensor requirements?");

        int tensorRequired = 0, ambiguity = 0, undecided = 0;
        foreach (var name in ObservableReconstructionAudit.Observables)
        {
            string c = ObservableReconstructionAudit.Classify(name);
            sb.AppendLine($"{name,-20} -> {c}");
            switch (c)
            {
                case "TENSOR REQUIRED": tensorRequired++; break;
                case "OBSERVABLE AMBIGUITY": ambiguity++; break;
                case "UNDECIDED": undecided++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"TENSOR REQUIRED     : {tensorRequired}   (GW strain — polarization content)");
        sb.AppendLine($"OBSERVABLE AMBIGUITY: {ambiguity}   (lensing, time-delay, magnification, shadow)");
        sb.AppendLine($"UNDECIDED           : {undecided}   (Hawking temperature)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: only the GW detector output is a TRUE tensor requirement. Lensing and its descendants");
        sb.AppendLine("(time-delay, magnification) and the horizon shadow only need a NON-CONFORMAL metric, which a scalar ψ");
        sb.AppendLine("supplies — they are 'tensor required' only under GR's specific observable mapping, not in fact.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1, tensorRequired);
        Assert.Equal(4, ambiguity);
        Assert.Equal(1, undecided);
    }

    // ── ATQG252: minimal d.o.f. refinement ─────────────────────────────────────────────

    [Fact]
    public void ATQG252_MinimalDofRefinement()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG252: refined minimal extension after separating effect from explanation");

        double scalarSet = ObservableReconstructionAudit.ScalarCapableMinimalDof();
        double fullSet = ObservableReconstructionAudit.FullSetMinimalDof();

        sb.AppendLine($"scalar-capable set (lensing + shadow, 4 observables): {scalarSet} d.o.f. (a scalar ψ)");
        sb.AppendLine($"full set (including GW strain):                        {fullSet} d.o.f. (spin-2 graviton)");
        sb.AppendLine();

        bool scalarCheaperThanFull = scalarSet < fullSet;
        bool gwDrivesFullSet = fullSet == 2.0;

        sb.AppendLine($"scalar ψ (1) is cheaper than the graviton (2): {scalarCheaperThanFull}");
        sb.AppendLine($"the full 2-d.o.f. spin-2 is required ONLY by the GW strain: {gwDrivesFullSet}");
        sb.AppendLine();
        sb.AppendLine("REFINEMENT of QG24: the 2-d.o.f. graviton is required specifically by the GW POLARIZATION observable.");
        sb.AppendLine("Lensing, time-delay, magnification, and the shadow would be restored by a 1-d.o.f. scalar ψ alone —");
        sb.AppendLine("the tensor requirement is narrower than 'all three observables'; it is exactly the GW detector output.");
        Output.WriteLine(sb.ToString());

        Assert.True(scalarCheaperThanFull, "scalar ψ should be cheaper than spin-2");
        Assert.Equal(1.0, scalarSet);
        Assert.Equal(2.0, fullSet);
    }
}
