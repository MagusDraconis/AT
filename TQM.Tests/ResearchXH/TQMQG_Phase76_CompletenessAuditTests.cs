using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 76 — completeness audit. Audits six domains against the network description.
/// Classify: DERIVED / COMPATIBLE / UNKNOWN / MISSING.
///
/// Tests: TQMQG760 (classification), TQMQG761 (derived vs compatible), TQMQG762 (remaining gaps).
/// </summary>
public class TQMQG_Phase76_CompletenessAuditTests : ResearchTestBase
{
    public TQMQG_Phase76_CompletenessAuditTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG760: classification ────────────────────────────────────────────────────

    [Fact]
    public void TQMQG760_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG760: audit six domains against the network");

        int derived = 0, compatible = 0, unknown = 0, missing = 0;
        foreach (var d in CompletenessAudit.Domains)
        {
            string c = CompletenessAudit.Classify(d);
            sb.AppendLine($"{d,-18} -> {c}");
            switch (c)
            {
                case "DERIVED": derived++; break;
                case "COMPATIBLE": compatible++; break;
                case "UNKNOWN": unknown++; break;
                case "MISSING": missing++; break;
            }
        }

        sb.AppendLine();
        sb.AppendLine($"DERIVED    : {derived}");
        sb.AppendLine($"COMPATIBLE : {compatible}");
        sb.AppendLine($"UNKNOWN    : {unknown}");
        sb.AppendLine($"MISSING    : {missing}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1, derived);
        Assert.Equal(4, compatible);
        Assert.Equal(1, unknown);
        Assert.Equal(0, missing);
    }

    // ── TQMQG761: derived vs compatible ─────────────────────────────────────────────

    [Fact]
    public void TQMQG761_DerivedVsCompatible()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG761: GR is derived; QM/gauge/fermions/SM are compatible");

        sb.AppendLine("DERIVED:");
        sb.AppendLine("  • GR — the spin-2 ψ reproduces (linearized) GR; its unique non-linear completion is Einstein gravity.");
        sb.AppendLine();
        sb.AppendLine("COMPATIBLE (via the new sectors θ, S, J):");
        sb.AppendLine("  • Quantum Mechanics — superposition/interference (θ), spin (S), entanglement (J);");
        sb.AppendLine("  • Gauge Theory — U(1) via θ (SU(2)/SU(3) additional);");
        sb.AppendLine("  • Fermions — spin-1/2 via S;");
        sb.AppendLine("  • Standard Model — ingredients hosted (SU(3)/generations/Higgs additional).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("DERIVED", CompletenessAudit.Classify("gr"));
        Assert.Equal("COMPATIBLE", CompletenessAudit.Classify("quantum-mechanics"));
        Assert.Equal("COMPATIBLE", CompletenessAudit.Classify("standard-model"));
    }

    // ── TQMQG762: remaining gaps ─────────────────────────────────────────────────────

    [Fact]
    public void TQMQG762_RemainingGaps()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG762: what is still outside the network?");

        foreach (var g in CompletenessAudit.RemainingGaps)
            sb.AppendLine($"  • {g}");

        sb.AppendLine();
        sb.AppendLine("REMAINING GAPS:");
        sb.AppendLine("  1. Standard-Model completeness: SU(3) strong force, the three fermion generations, the Higgs mechanism;");
        sb.AppendLine("  2. Cosmology: inflation, the CMB, Λ (dark energy), and dark matter — only expansion/redshift are derived.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: nothing fundamental is MISSING, but the full Standard Model (SU(3)/generations/Higgs) and");
        sb.AppendLine("cosmology (inflation/CMB/Λ/DM) are additional content beyond the network's derived/compatible core.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(2, CompletenessAudit.RemainingGaps.Length);
        Assert.Equal("UNKNOWN", CompletenessAudit.Classify("cosmology"));
    }
}
