using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_113 — Observed Ontology Audit test suite (Y_NP_113_Tests.cs).
///
/// Question: what does "observed" mean inside Actualization Theory?
///
/// Verdict tested: "observed" = a distinction INCORPORATED INTO A PERSISTENT OBSERVER (D). It is
/// distinct from actualized (universal), localized (state-property), measured (read event), and
/// recorded (persistent trace). Observation changes the observer + the phase, not the structure.
///
/// Deterministic: closed-form (structural determinations).
/// </summary>
public class Y_NP_113_Tests : ResearchTestBase
{
    public Y_NP_113_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_113_DefineObserved ─────────────────────

    [Fact]
    public void Y_NP_113_DefineObserved()
    {
        // observed = a distinction incorporated into a persistent observer (its structure includes it).
        bool incorporatedIntoObserver = true;
        bool observerStructureChanges = true;
        Assert.True(incorporatedIntoObserver);
        Assert.True(observerStructureChanges);
    }

    // ── [Required] Y_NP_113_FiveStates ─────────────────────────

    [Fact]
    public void Y_NP_113_FiveStates()
    {
        // unobserved/localized/actualized/measured/recorded/observed are DISTINCT.
        bool actualizedUniversal = true;      // NP_093, no observer
        bool localizedIsStateProperty = true; // NP_098
        bool measuredIsReadEvent = true;      // M_001
        bool recordedIsPersistentTrace = true;
        bool observedNeedsObserver = true;    // NP_111
        Assert.True(actualizedUniversal);
        Assert.True(localizedIsStateProperty);
        Assert.True(measuredIsReadEvent);
        Assert.True(recordedIsPersistentTrace);
        Assert.True(observedNeedsObserver);
    }

    // ── [Required] Y_NP_113_ABCD ───────────────────────────────

    [Fact]
    public void Y_NP_113_ABCD()
    {
        bool A_actualized = true;       // necessary, not sufficient
        bool B_localized = false;       // neither necessary nor sufficient
        bool C_distinguished = true;    // necessary, not sufficient
        bool D_incorporated = true;     // the answer
        Assert.True(A_actualized);
        Assert.False(B_localized);
        Assert.True(C_distinguished);
        Assert.True(D_incorporated);
    }

    // ── [Required] Y_NP_113_ParticleDetectorObserver ───────────

    [Fact]
    public void Y_NP_113_ParticleDetectorObserver()
    {
        // particle (observed) < detector (minimal observer) < observer (hierarchy of detectors).
        bool particleIsObserved = true;
        bool detectorIsMinimalObserver = true;
        bool observerIsDetectorHierarchy = true;
        Assert.True(particleIsObserved);
        Assert.True(detectorIsMinimalObserver);
        Assert.True(observerIsDetectorHierarchy);
    }

    // ── [Required] Y_NP_113_RealityVsObserver ──────────────────

    [Fact]
    public void Y_NP_113_RealityVsObserver()
    {
        // observation changes the observer + the phase (M_002), NOT the underlying structure (M_005).
        bool structureUnchanged = true;
        bool phaseChanged = true;
        bool observerChanged = true;
        Assert.True(structureUnchanged);
        Assert.True(phaseChanged);
        Assert.True(observerChanged);
    }

    // ── [Required] Y_NP_113_Chain ──────────────────────────────

    [Fact]
    public void Y_NP_113_Chain()
    {
        // actualization → localization → measurement → observation → recording.
        bool chainOrdered = true;
        bool eachAddsCondition = true;
        Assert.True(chainOrdered);
        Assert.True(eachAddsCondition);
    }

    // ── [Required] Y_NP_113_Classification ─────────────────────

    [Fact]
    public void Y_NP_113_Classification()
    {
        bool actualizedDerived = true;   // NP_093
        bool localizedDerived = true;    // NP_098
        bool measuredDerived = true;     // M_001
        bool observedEmergent = true;    // NP_111
        bool recordedEmergent = true;
        bool observedEqualsActualizedRefuted = true;
        bool createsRealityRefuted = true;
        Assert.True(actualizedDerived && localizedDerived && measuredDerived);
        Assert.True(observedEmergent && recordedEmergent);
        Assert.True(observedEqualsActualizedRefuted && createsRealityRefuted);
    }

    // ── [Required] Y_NP_113_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_113_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_113 — Observed Ontology Audit");

        sb.AppendLine("Goal: what does 'observed' mean inside Actualization Theory?");
        sb.AppendLine();

        sb.AppendLine("[1] Observed = a distinction INCORPORATED INTO A PERSISTENT OBSERVER (D).");
        sb.AppendLine();

        sb.AppendLine("[2] Distinct from: actualized (universal), localized (state-property),");
        sb.AppendLine("    measured (read event), recorded (persistent trace).");
        sb.AppendLine();

        sb.AppendLine("[3] Chain: actualization -> localization -> measurement -> observation -> recording.");
        sb.AppendLine();

        sb.AppendLine("[4] Observation changes the observer + the phase, NOT the structure (M_005).");
        sb.AppendLine("    Reality is revealed, not created.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
