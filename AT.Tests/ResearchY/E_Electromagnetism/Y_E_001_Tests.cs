using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_001 — Electromagnetism Inventory Audit (group E — Electromagnetism).
///
/// QUESTION. What electromagnetic structure is ALREADY derived inside AT? Search charge, current, gauge
/// symmetry, Maxwell equations, vector potential, photon sector and wave equation; classify every occurrence as
/// DERIVED / BOUNDARY / ASSUMED / REFUTED. Output: (1) existing EM primitives, (2) existing EM laws,
/// (3) missing components, (4) the minimal route to Maxwell theory.
///
/// ANSWER: **BOUNDARY**, for one repeated reason — **AT's electromagnetic dynamics is DECLARED but never
/// COMPUTED.**
///
///  (1) DERIVED AND COMPUTED: the U(1) gauge group, the 1 + 3 + 8 = 12 gauge structure (QG161, deterministic,
///      no fitted parameters), topological charge, the link-connection picture, an emergent c = ℓ/τ, and the
///      substrate's own wave equation.
///  (2) THE FINDING — and note that a FIRST DRAFT of this audit got it wrong. It claimed the dynamics was
///      simply absent (no current, no kinetic term, no Lagrangian). **The live scan refuted that draft.** AT has
///      all of them — in `ResearchXH/LagrangianOrigin.cs` (QG244) — **as members returning STRINGS**: the
///      Noether currents, F^a_μν, `L_gauge = −(1/4)F^a F^a`, D_μ, the matter term and the full density
///      `L = −(1/4)F^a F^a + iψ̄γ^μD_μψ − mψ̄ψ`. 'Derived' is asserted by ANDing other audits' booleans.
///  (3) THE PROGRAM ALREADY KNEW: QG242 recorded the gauge dynamics (interaction Lagrangian, vertices,
///      propagators) as **HOSTED/OPEN**; QG243/244 then close it by declaration.
///  (4) STILL ABSENT: the **sourced Maxwell equation ∂_μF^μν = J^ν** (AT reaches only current CONSERVATION
///      ∂_μJ^μ = 0, which is kinematic) and any massless **spin-1** wave equation.
///  (5) TWO CONTRADICTIONS: U(1) has two incompatible origins, and α has two values (137 vs ~100).
///  (6) ONE CLAIM IS NOT EVIDENCE: a seeded RNG with hand-picked thresholds presented as an experiment.
///  (7) AN INSTRUMENT DEFECT, FOUND AND FIXED: the G_027/G_033/G_035 scanners strip per-line and so count
///      multi-line verbatim-string PROSE as executable CODE.
/// </summary>
public class Y_E_001_Tests : ResearchTestBase
{
    public Y_E_001_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_001_EmDynamicsIsDeclaredNotComputed()
    {
        // MECHANICAL PROOF, read live from QG244: every piece of the EM dynamics is a STRING-returning member.
        var asStrings = ElectromagnetismInventoryAudit.EmDynamicsIsStringReturning();
        Assert.Equal(6, asStrings.Length);
        Assert.Contains("LagrangianDensity", asStrings);
        Assert.Contains("GaugeKineticTerm", asStrings);
        Assert.Contains("FieldStrengthForm", asStrings);
        Assert.Contains("ConservedCurrents", asStrings);

        // …and the members that declare the dynamics DERIVED are conjunctions of other audits' booleans.
        var conjunctions = ElectromagnetismInventoryAudit.DerivationsThatAreConjunctions();
        Assert.NotEmpty(conjunctions);
        Assert.Contains("QedLagrangianDerived", conjunctions);

        // Wherefore the statuses: the Lagrangian, the kinetic term, the field strength and the currents are
        // ASSUMED — present as declarations, with zero executable occurrences of their own vocabulary.
        var assumed = ElectromagnetismInventoryAudit.Assumed();
        Assert.Contains(assumed, c => c.Name.Contains("Lagrangian density"));
        Assert.Contains(assumed, c => c.Name.Contains("kinetic term"));
        Assert.Contains(assumed, c => c.Name.Contains("Noether currents"));
        Assert.All(assumed, c => Assert.True(c.DocumentedCount > 0, $"{c.Name} must at least be stated"));

        // Only two things are MISSING outright: the sourced field equation and the spin-1 wave equation.
        var missing = ElectromagnetismInventoryAudit.Missing().Select(c => c.Name).ToArray();
        Assert.Equal(2, missing.Length);
        Assert.Contains(missing, n => n.Contains("Maxwell field equation"));
        Assert.Contains(missing, n => n.Contains("spin-1"));
        Assert.All(ElectromagnetismInventoryAudit.Missing(),
            c => Assert.Equal(0, c.DocumentedCount));
    }

    [Fact]
    public void Y_E_001_TheProgramRecordedItsOwnDynamicsAsOpen()
    {
        // QG242's own admission, read from the repository: the interaction Lagrangian, vertices and propagators
        // were recorded as HOSTED/OPEN before Phase 243/244 declared the dynamics closed.
        Assert.True(ElectromagnetismInventoryAudit.DynamicsWasRecordedOpen(),
            "QG242 must record the gauge dynamics as HOSTED/OPEN — that is the audit's strongest evidence");

        // The distinction the audit turns on: what AT offers as its field equation is CONSERVATION of a current
        // it cannot compute, not the sourced field equation.
        string route = ElectromagnetismInventoryAudit.MinimalRouteToMaxwell();
        Assert.Contains("current CONSERVATION", route);
        Assert.Contains("∂_μF^μν = J^ν", route);
        Assert.Contains("□A_μ = 0", route);
        Assert.Contains("COMPUTE WHAT IS DECLARED", route);
    }

    [Fact]
    public void Y_E_001_KinematicsDerivedDynamicsDeclared()
    {
        var inv = ElectromagnetismInventoryAudit.Inventory();

        Assert.Equal(EmStatus.Derived, inv.Single(c => c.Name.Contains("gauge symmetry")).Status);
        Assert.Equal(EmStatus.Derived, inv.Single(c => c.Name.Contains("1 + 3 + 8")).Status);
        Assert.Equal(EmStatus.Derived, inv.Single(c => c.Name.Contains("substrate wave equation")).Status);
        Assert.Equal(EmStatus.Derived, inv.Single(c => c.Name.Contains("speed of light")).Status);

        // The connection is a real structure but the PROPAGATING potential is only stated.
        Assert.Equal(EmStatus.Boundary, inv.Single(c => c.Name.Contains("vector potential")).Status);
        Assert.Equal(EmStatus.Boundary, inv.Single(c => c.Name.Contains("photon sector")).Status);
        Assert.Equal(EmStatus.Boundary, inv.Single(c => c.Name.Contains("charge quantization")).Status);

        // No curated status may contradict its own live counts.
        Assert.Empty(ElectromagnetismInventoryAudit.UnsupportedClaims());

        // The verdict follows: primitives derived, field equations missing, coupling refuted.
        Assert.Equal("BOUNDARY", ElectromagnetismInventoryAudit.Verdict());
        Assert.True(ElectromagnetismInventoryAudit.Primitives().Any(c => c.Status == EmStatus.Derived));
        Assert.NotEmpty(ElectromagnetismInventoryAudit.Missing());
    }

    [Fact]
    public void Y_E_001_TheTwoContradictionsAreLive()
    {
        var gauge = ElectromagnetismInventoryAudit.DoublyOriginatedGauge();
        Assert.Contains("GaugeSectorOrigin.cs", gauge.First);
        Assert.Contains("GaugeSymmetryAnalyzer.cs", gauge.Second);
        Assert.Contains("vortex", gauge.Second);
        Assert.Contains("G_026", gauge.Why);
        Assert.True(SourceExists("ResearchXH", "GaugeSectorOrigin.cs"));
        Assert.True(SourceExists("Research", "GaugeSymmetryAnalyzer.cs"));

        var alpha = ElectromagnetismInventoryAudit.AlphaDisagreement();
        Assert.Contains("137", alpha.First);
        Assert.Contains("100", alpha.Second);
        Assert.Contains("LARGEST REMAINING FREE PARAMETER", alpha.Why);
        Assert.True(DocumentedHit("95 + 42"), "QG162's 95 + 42 must be present");
        Assert.True(DocumentedHit("not 1/137"), "the X-series refutation of 1/137 must be present");

        Assert.Equal(EmStatus.Refuted,
            ElectromagnetismInventoryAudit.Inventory().Single(c => c.Name.Contains("fine-structure")).Status);
    }

    [Fact]
    public void Y_E_001_LiteralEvidenceIsFlaggedNotCounted()
    {
        Assert.True(ElectromagnetismInventoryAudit.LiteralEvidenceConfirmed());

        var (where, defect, why) = ElectromagnetismInventoryAudit.LiteralEvidence();
        Assert.Contains("SimulateEmergence", where);
        Assert.Contains("Random(42)", defect);
        Assert.Contains("0.15", defect);
        Assert.Contains("not ab initio", why);

        // The mechanism claim is NOT refuted — only the simulation's evidential force is.
        Assert.Equal(EmStatus.Derived,
            ElectromagnetismInventoryAudit.Inventory().Single(c => c.Name.Contains("gauge symmetry")).Status);
    }

    [Fact]
    public void Y_E_001_ProseIsNotComputation_AndTheInstrumentProvesIt()
    {
        // AT's only current-like object is a proto-matter condensate current J_Q — stated in prose with a
        // continuity equation and a Fick/drift form, never computed, and never coupled to A_μ.
        var jq = ElectromagnetismInventoryAudit.AssertedButNotComputed();
        Assert.Contains(jq, c => c.Name.Contains("Noether currents"));

        // THE INSTRUMENT DEFECT, demonstrated live: a per-line strip counts J_Q as executable code because it
        // sits inside a multi-line verbatim string; the full-file state machine counts zero.
        var (perLine, fullFile) = ElectromagnetismInventoryAudit.VerbatimStringDefect();
        Assert.True(perLine >= 1, $"a per-line strip must miscount J_Q as code (got {perLine})");
        Assert.Equal(0, fullFile);

        // The stripper behaves correctly on each construct it must handle.
        Assert.Equal("", AtSourceScan.StripLiteralsAndComments("\"// not a comment\"").Trim());
        Assert.Equal("", AtSourceScan.StripLiteralsAndComments("@\"line1\nline2\"").Trim());
        Assert.Equal("", AtSourceScan.StripLiteralsAndComments("\"\"\"raw\nstring\"\"\"").Trim());
        Assert.Equal("", AtSourceScan.StripLiteralsAndComments("'c'").Trim());
        Assert.Equal("", AtSourceScan.StripLiteralsAndComments("/* block */").Trim());
        Assert.Equal("code", AtSourceScan.StripLiteralsAndComments("code // tail").Trim());
        Assert.Equal(4, AtSourceScan.StripLiteralsAndComments("a\n@\"x\ny\"\nb").Split('\n').Length);
    }

    [Fact]
    public void Y_E_001_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("Y_E_001 — Electromagnetism Inventory Audit: what EM structure already exists in AT?");

        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. Search terms are those given: charge, current, gauge symmetry, Maxwell equations,");
        sb.AppendLine("     vector potential, photon sector, wave equation.");
        sb.AppendLine("  2. A component is COMPUTED only if it survives stripping comments AND string literals");
        sb.AppendLine("     (the G_035 discipline). Everything else is a declaration.");
        sb.AppendLine("  3. DECLARED BUT NOT COMPUTED is a finding, not a contradiction: it is the G_027 class.");
        sb.AppendLine("  4. The audit excludes its own source, so it cannot count its own vocabulary as evidence.");
        sb.AppendLine();

        PrintHeader("THE SCAN");
        var (raw, exe) = ElectromagnetismInventoryAudit.Scan();
        sb.AppendLine($"  AT.Core lines scanned             : {raw.Length}");
        sb.AppendLine($"  after stripping comments+strings  : {exe.Count(l => l.Trim().Length > 0)}");
        sb.AppendLine("  documented = talked about;   executable = actually computed.");
        sb.AppendLine();

        PrintHeader("THE MECHANICAL PROOF — THE DYNAMICS IS A STRING");
        sb.AppendLine("  Members of ResearchXH/LagrangianOrigin.cs (QG244) that return strings:");
        foreach (var m in ElectromagnetismInventoryAudit.EmDynamicsIsStringReturning())
            sb.AppendLine($"    · {m}()  ->  string");
        sb.AppendLine("  Booleans that DECLARE the dynamics derived, by ANDing other audits' predicates:");
        foreach (var m in ElectromagnetismInventoryAudit.DerivationsThatAreConjunctions())
            sb.AppendLine($"    · {m}()  ->  conjunction, not a computation");
        sb.AppendLine($"  QG242 recorded the gauge dynamics as HOSTED/OPEN: "
                      + $"{ElectromagnetismInventoryAudit.DynamicsWasRecordedOpen()}");
        sb.AppendLine();

        sb.AppendLine(ElectromagnetismInventoryAudit.OutputExistingPrimitives());
        sb.AppendLine();
        sb.AppendLine(ElectromagnetismInventoryAudit.OutputExistingLaws());
        sb.AppendLine();
        sb.AppendLine(ElectromagnetismInventoryAudit.OutputMissingComponents());
        sb.AppendLine();

        PrintHeader("ASSERTED BUT NEVER COMPUTED (the G_027 defect class)");
        foreach (var c in ElectromagnetismInventoryAudit.AssertedButNotComputed())
            sb.AppendLine($"  · {c.Name}  — documented {c.DocumentedCount}, executable {c.ExecutableCount}");
        sb.AppendLine();

        PrintHeader("THE TWO CONTRADICTIONS");
        var g = ElectromagnetismInventoryAudit.DoublyOriginatedGauge();
        sb.AppendLine($"  1. {g.Subject} — derived TWICE, incompatibly:");
        sb.AppendLine($"       A: {g.First}");
        sb.AppendLine($"       B: {g.Second}");
        sb.AppendLine($"       {g.Why}");
        var a = ElectromagnetismInventoryAudit.AlphaDisagreement();
        sb.AppendLine($"  2. {a.Subject}:");
        sb.AppendLine($"       A: {a.First}");
        sb.AppendLine($"       B: {a.Second}");
        sb.AppendLine();
        var (where, defect, why) = ElectromagnetismInventoryAudit.LiteralEvidence();
        sb.AppendLine("  3. NOT EVIDENCE — a literal-driven 'experiment':");
        sb.AppendLine($"       {where}");
        sb.AppendLine($"       defect: {defect}");
        sb.AppendLine($"       {why}");
        sb.AppendLine();

        PrintHeader("INSTRUMENT DEFECT FOUND (affects G_027 / G_033 / G_035)");
        var (perLine, full) = ElectromagnetismInventoryAudit.VerbatimStringDefect();
        sb.AppendLine("  Those scanners strip with a PER-LINE regex, which cannot see a verbatim (@\"...\")");
        sb.AppendLine("  string spanning lines — so report prose survives and is counted as executable code.");
        sb.AppendLine($"  Live proof on J_Q (pure prose): per-line strip counts {perLine}, full-file strip counts {full}.");
        sb.AppendLine("  AtSourceScan (stateful, whole-file) is the fix.");
        sb.AppendLine();

        sb.AppendLine(ElectromagnetismInventoryAudit.MinimalRouteToMaxwell());
        sb.AppendLine();

        PrintHeader("VERDICT");
        sb.AppendLine($"  {ElectromagnetismInventoryAudit.Verdict()}");
        sb.AppendLine("  " + ElectromagnetismInventoryAudit.WhereItStands());
        sb.AppendLine();
        sb.AppendLine("  CONCLUSIONS");
        foreach (var c in ElectromagnetismInventoryAudit.Inventory())
            sb.AppendLine($"    {c.Status.ToString().ToUpperInvariant(),-8} {c.Name}");

        Output.WriteLine(sb.ToString());
    }

    private static bool SourceExists(params string[] parts)
    {
        var root = CubicSubstrateAudit.FindRoot("AT.Core");
        if (root is null) return false;
        return File.Exists(Path.Combine(new[] { root }.Concat(parts).ToArray()));
    }

    private static bool DocumentedHit(string text)
    {
        var (raw, _) = ElectromagnetismInventoryAudit.Scan();
        return raw.Any(l => l.Contains(text, StringComparison.Ordinal));
    }
}
