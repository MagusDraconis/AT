using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 31 — derive the TRM propagator from Q-event network dynamics. Tests whether TRM's kernel is a
/// propagation law or a correlation function. Classify: NO RELATION / PARTIAL MATCH / SAME OBJECT.
///
/// Tests: TQMQG310 (native vs TRM effective profile), TQMQG311 (derivability), TQMQG312 (classification).
/// </summary>
public class TQMQG_Phase31_TRMPropagatorOriginTests : ResearchTestBase
{
    public TQMQG_Phase31_TRMPropagatorOriginTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG310: native tick propagation vs TRM kernel ───────────────────────────────

    [Fact]
    public void TQMQG310_NativeVsTrm()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG310: native tick propagator (M_eff=0) vs TRM kernel (M_eff=e^Φ−1)");

        double phi = 0.5;
        double native = TRMPropagatorOrigin.NativeMeff();
        double trm = TRMPropagatorOrigin.TrmMeff(phi);
        double nNative = TRMPropagatorOrigin.NativeIndex();
        bool causal = TRMPropagatorOrigin.TickPropagatesAlongLightCone();
        bool shared = TRMPropagatorOrigin.SharesCausalStructure();

        sb.AppendLine($"native tick propagation: n = {nNative:F4}, M_eff = n−1 = {native:F4}  (massless null)");
        sb.AppendLine($"TRM kernel:              M_eff = e^Φ−1 = {trm:F4}  (refractive/massive)");
        sb.AppendLine($"tick propagates along the light cone: {causal}");
        sb.AppendLine($"shared causal (retarded, light-cone) structure: {shared}");

        bool nativeMassless = native == 0.0 && nNative == 1.0;
        bool trmRefractive = trm > 0.0;

        sb.AppendLine();
        sb.AppendLine($"native propagator is massless/conformal: {nativeMassless}");
        sb.AppendLine($"TRM kernel is refractive (nonzero M_eff): {trmRefractive}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the tick propagates along the generation relation → the light cone (conformal, n=1, M_eff=0).");
        sb.AppendLine("TRM's kernel M_eff = e^Φ−1 is a nonzero refractive profile — the non-conformal (ψ) sector, not the native law.");
        Output.WriteLine(sb.ToString());

        Assert.True(nativeMassless, "native propagation should be massless/conformal");
        Assert.True(trmRefractive, "TRM kernel should be refractive");
        Assert.True(shared, "both should share the causal structure");
    }

    // ── TQMQG311: derivability ─────────────────────────────────────────────────────────

    [Fact]
    public void TQMQG311_Derivability()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG311: is TRM's kernel derivable as a propagation law (not a correlation)?");

        bool derivable = TRMPropagatorOrigin.DerivableAsPropagation();
        bool coincideAtZero = TRMPropagatorOrigin.Coincide(0.0);
        bool coincideAtNonzero = TRMPropagatorOrigin.Coincide(TRMPropagatorOrigin.TrmMeff(0.5));

        sb.AppendLine($"TRM kernel derivable as the NATIVE propagation law: {derivable}");
        sb.AppendLine($"coincide at M_eff = 0 (ψ = 0, conformal): {coincideAtZero}");
        sb.AppendLine($"coincide at M_eff ≠ 0 (ψ ≠ 0): {coincideAtNonzero}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the native tick dynamics give ONLY M_eff = 0 (null geodesics). TRM's nonzero kernel M_eff = e^Φ−1");
        sb.AppendLine("is a propagation law of the ψ-extension, not of the conformal tick network. As a correlation function it is");
        sb.AppendLine("zero-mean jitter (QG30). So TRM's kernel is NEITHER native propagation NOR native correlation — it is the ψ sector.");
        Output.WriteLine(sb.ToString());

        Assert.False(derivable, "TRM kernel should not be derivable from native tick dynamics");
        Assert.True(coincideAtZero, "the two should coincide at M_eff=0");
        Assert.False(coincideAtNonzero, "the two should differ at M_eff != 0");
    }

    // ── TQMQG312: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG312_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG312: NO RELATION / PARTIAL MATCH / SAME OBJECT?");

        sb.AppendLine("CLASSIFICATION: PARTIAL MATCH.");
        sb.AppendLine();
        sb.AppendLine("  • SHARED: the causal (retarded, light-cone) structure — both the native tick propagator and TRM's kernel");
        sb.AppendLine("    propagate influence along the generation relation's light cone.");
        sb.AppendLine("  • DIFFERENT: the refractive content. The native propagator is massless (M_eff = 0, n = 1); TRM's kernel");
        sb.AppendLine("    has M_eff = e^Φ−1 ≠ 0 — the non-conformal (ψ) correction.");
        sb.AppendLine("  • They are the SAME OBJECT only at ψ = 0 (M_eff = 0); otherwise they differ by exactly the ψ correction.");
        sb.AppendLine("  • As a correlation function, TRM's kernel is zero-mean jitter (QG30); as a propagation law it is the ψ sector.");
        sb.AppendLine("    In neither reading is it derivable from the conformal tick network.");
        Output.WriteLine(sb.ToString());

        Assert.True(TRMPropagatorOrigin.SharesCausalStructure());
        Assert.False(TRMPropagatorOrigin.DerivableAsPropagation());
        Assert.True(TRMPropagatorOrigin.Coincide(0.0));
    }
}
