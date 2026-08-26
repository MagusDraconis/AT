using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 43 — observational uniqueness of ψ. Which observations require the tensor ψ vs a scalar ψ?
/// Classify: SCALAR / PSI / AMBIGUOUS.
///
/// Tests: ATQG430 (classification table), ATQG431 (the PSI case), ATQG432 (uniqueness summary).
/// </summary>
public class ATQG_Phase43_ObservationalUniquenessTests : ResearchTestBase
{
    public ATQG_Phase43_ObservationalUniquenessTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG430: classification table ────────────────────────────────────────────────

    [Fact]
    public void ATQG430_ClassificationTable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG430: SCALAR / PSI / AMBIGUOUS for five observables");

        int scalar = 0, psi = 0, ambiguous = 0;
        foreach (var ob in ObservationalUniqueness.Observables)
        {
            string c = ObservationalUniqueness.Classify(ob);
            double spin = ObservationalUniqueness.Spin(ob);
            sb.AppendLine($"{ob,-18} -> {c,-9} (spin {spin})");
            switch (c)
            {
                case "SCALAR": scalar++; break;
                case "PSI": psi++; break;
                case "AMBIGUOUS": ambiguous++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"SCALAR    : {scalar}");
        sb.AppendLine($"PSI       : {psi}");
        sb.AppendLine($"AMBIGUOUS : {ambiguous}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, scalar);
        Assert.Equal(1, psi);
        Assert.Equal(1, ambiguous);
    }

    // ── ATQG431: the PSI case ─────────────────────────────────────────────────────────

    [Fact]
    public void ATQG431_PsiCase()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG431: only the GW polarization genuinely requires the tensor ψ");

        bool gwNeedsTensor = ObservationalUniqueness.RequiresTensorPsi("gw-polarization");
        bool lensingScalar = ObservationalUniqueness.ScalarPsiSuffices("lensing");
        bool delayScalar = ObservationalUniqueness.ScalarPsiSuffices("shapiro-delay");
        bool ppnScalar = ObservationalUniqueness.ScalarPsiSuffices("ppn-gamma");

        sb.AppendLine($"GW polarization needs the spin-2 tensor ψ: {gwNeedsTensor}");
        sb.AppendLine($"lensing reproducible by a scalar ψ:        {lensingScalar}");
        sb.AppendLine($"Shapiro delay reproducible by a scalar ψ:  {delayScalar}");
        sb.AppendLine($"PPN γ reproducible by a scalar ψ:          {ppnScalar}");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: lensing, Shapiro delay, and PPN γ are each a single SCALAR quantity (spin 0); a 1-d.o.f.");
        sb.AppendLine("non-conformal scalar ψ (γ → ≠ −1) reproduces all three. Only the GW polarization (h_+, h_×) is spin-2.");
        Output.WriteLine(sb.ToString());

        Assert.True(gwNeedsTensor, "GW polarization should need the tensor psi");
        Assert.True(lensingScalar && delayScalar && ppnScalar, "scalar observables should need only a scalar psi");
    }

    // ── ATQG432: uniqueness summary ───────────────────────────────────────────────────

    [Fact]
    public void ATQG432_UniquenessSummary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG432: the observational uniqueness of ψ");

        sb.AppendLine("THE UNIQUE ROLE OF THE TENSOR ψ:");
        sb.AppendLine("  • Exactly ONE observable — the GW polarization (h_+, h_×) — genuinely requires the spin-2 tensor ψ.");
        sb.AppendLine("  • Lensing, Shapiro delay, and PPN γ are spin-0: a scalar non-conformal ψ (1 d.o.f.) suffices for all of them.");
        sb.AppendLine("  • Horizon physics is AMBIGUOUS: the shadow and entropy are scalar, but Hawking T is UNDECIDED (QG25).");
        sb.AppendLine();
        sb.AppendLine("REFINEMENT OF QG40: the tensor ψ is observationally UNIQUE only for gravitational-wave polarization. The");
        sb.AppendLine("conformal-flatness-breaking scalar ψ (a cheaper 1-d.o.f. extension) would already restore lensing, delay, and");
        sb.AppendLine("γ. So ψ's irreducible spin-2 content is demanded by a single, specific observation: GW polarization.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PSI", ObservationalUniqueness.Classify("gw-polarization"));
        Assert.Equal("SCALAR", ObservationalUniqueness.Classify("lensing"));
        Assert.Equal("AMBIGUOUS", ObservationalUniqueness.Classify("horizon-physics"));
        Assert.True(ObservationalUniqueness.RequiresTensorPsi("gw-polarization"));
    }
}
