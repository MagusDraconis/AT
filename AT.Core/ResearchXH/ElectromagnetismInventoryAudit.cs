using System.Text;
using System.Text.RegularExpressions;

namespace AT.Core.ResearchXH;

/// <summary>How firmly a piece of the electromagnetic sector is established.</summary>
public enum EmStatus
{
    /// <summary>Follows from AT primitives by a stated chain.</summary>
    Derived,

    /// <summary>Representable and consistent, but carries an imported or chosen ingredient.</summary>
    Boundary,

    /// <summary>Written down (as prose, a string-valued member, or a declaration) with nothing computing it.</summary>
    Assumed,

    /// <summary>Stated in AT, contradicts a measurement or another AT result.</summary>
    Refuted,

    /// <summary>Nothing computes it AND nothing states it.</summary>
    Missing,
}

/// <summary>
/// One electromagnetic component, with the live counts that justify its status.
/// <paramref name="DocumentedCount"/> counts raw occurrences (prose, doc comments and string literals
/// included); <paramref name="ExecutableCount"/> counts occurrences surviving the removal of comments and
/// literals — code that actually COMPUTES with the component.
/// </summary>
public sealed record EmComponent(
    string Name,
    string Category,
    EmStatus Status,
    string Source,
    string[] Vocabulary,
    string Basis)
{
    public int DocumentedCount { get; init; }
    public int ExecutableCount { get; init; }

    /// <summary>
    /// The defect this audit exists to expose: a component TALKED ABOUT but never COMPUTED. G_027's discipline
    /// applied to the EM sector.
    /// </summary>
    public bool AssertedButNotComputed => DocumentedCount > 0 && ExecutableCount == 0;

    /// <summary>Is the status supported by the live counts? Missing needs nothing anywhere; the rest need prose.</summary>
    public bool StatusIsSupported => Status switch
    {
        EmStatus.Missing => DocumentedCount == 0 && ExecutableCount == 0,
        _ => DocumentedCount > 0,
    };
}

/// <summary>
/// ResearchY-E_001 — ELECTROMAGNETISM INVENTORY AUDIT.
///
/// QUESTION. What electromagnetic structure is ALREADY derived inside AT? Search charge, current, gauge
/// symmetry, Maxwell equations, vector potential, photon sector, wave equation; classify every occurrence as
/// DERIVED / BOUNDARY / ASSUMED / REFUTED. Output: (1) existing EM primitives, (2) existing EM laws,
/// (3) missing components, (4) the minimal route to Maxwell theory.
///
/// THE ANSWER IS **BOUNDARY**, and the reason is a single repeated pattern. A first draft of this audit
/// concluded the EM *dynamics* was simply absent — no current, no kinetic term, no Lagrangian. **That was
/// wrong, and the live scan caught it.** AT has all of them… **as strings.**
///
/// (1) WHAT IS GENUINELY DERIVED (and computed). The gauge sector: U(1) as the rotation subgroup Z_96 ⊂ D96 of
///     the circulant automorphism group of the observable attractor, SU(2) from the 2D irreps, SU(3) from the
///     three octave families — **1 + 3 + 8 = 12 = degree(C_96(1..6))**, deterministic, no fitted parameters
///     (QG161). Charge as a topological invariant. The substrate's own wave equation (TemporalField), and an
///     emergent c = ℓ/τ.
///
/// (2) THE PATTERN: **AT's electromagnetic DYNAMICS IS DECLARED, NEVER COMPUTED.** `ResearchXH/LagrangianOrigin.cs`
///     (QG244) contains the whole structure — the Noether currents J^μ_em / J^μ_W / J^μ_s, the field strength
///     F^a_μν, the gauge kinetic term `L_gauge = −(1/4)F^a_μν F^aμν`, the covariant derivative, the matter term
///     and the full density `L = −(1/4)F^a F^a + iψ̄γ^μD_μψ − mψ̄ψ`. **Every one of those is a member returning a
///     STRING.** And the "derived" booleans (`QedLagrangianDerived`, `NoetherCurrentsExist`) are conjunctions of
///     other audits' booleans, not computations. This is mechanically checkable and checked — see
///     <see cref="EmDynamicsIsStringReturning"/>.
///
/// (3) THE PROGRAM ALREADY KNEW. QG242 (`GaugeDynamicsOrigin`) records that it "found the gauge SYMMETRY derived
///     but the gauge DYNAMICS (interaction Lagrangian, vertices, propagators) **HOSTED/OPEN**". QG243/244 then
///     close it by declaration. The honest reading is that the dynamics is **OPEN, restated** — the inventory
///     classifies it ASSUMED rather than Derived for exactly that reason.
///
/// (4) THE FIELD EQUATION IS STILL ABSENT — and the distinction matters. What QG243 offers as "the derived QED
///     current-conservation law" is **∂_μJ^μ = 0**, a *conservation* statement that follows kinematically from
///     the symmetry. The **sourced Maxwell equation ∂_μF^μν = J^ν** is never written or solved, and no massless
///     **spin-1** wave equation exists anywhere (AT's only massless wave equation is the **spin-2** Fierz–Pauli
///     □ψ_μν = 0 of the gravity sector). So AT has the *conservation* of a current it cannot compute, and lacks
///     the *field equation* that would make the photon propagate.
///
/// (5) TWO CONTRADICTIONS. **U(1) has two incompatible origins** — Z_96 automorphism (QG161) and the S¹ moduli
///     space of vortices (AT-X050) — two audits, both passing, in the same repository (the G_026 pattern). And
///     **α disagrees with itself**: QG162 gives `1/α_em = Σm + #doublets = 95 + 42 = 137` (0.03 % of 137.036),
///     while `Research/FineStructureAnalyzer` reports α⁻¹ ≈ 100, calls the 1.4× gap "not a precise derivation",
///     and names α "the LARGEST REMAINING FREE PARAMETER in AT".
///
/// (6) ONE CLAIM IS NOT EVIDENCE. `GaugeSymmetryAnalyzer.SimulateEmergence` presents a "COMPUTATIONAL
///     EXPERIMENT" that is `new Random(42)` with hand-picked thresholds (0.15 / 0.07 / 0.18 / 0.02). Its
///     conclusion that U(1) dominates is a deterministic function of the literals. The mechanism claim survives;
///     the simulation is not support for it.
///
/// (7) AN INSTRUMENT DEFECT, FOUND AND FIXED HERE. The G_027/G_033/G_035 scanners strip with a **per-line**
///     regex, which cannot see a verbatim (`@"..."`) string spanning lines — and this repository writes most of
///     its report text in exactly such blocks, so under a per-line stripper PROSE COUNTS AS CODE. `J_Q` is the
///     live proof (<see cref="VerbatimStringDefect"/>). The audits named above should be re-run with
///     <see cref="AtSourceScan"/>.
///
/// VERDICT: **BOUNDARY** — symmetries and couplings derived; the Lagrangian and its currents declared but never
/// computed; the sourced field equation absent; the coupling self-contradictory. Computed, not typed.
/// </summary>
public static class ElectromagnetismInventoryAudit
{
    /// <summary>The tree scanned for EM vocabulary.</summary>
    public const string ScanRelative = "AT.Core";

    private static readonly Regex StringLiteral = new("\"[^\"\\\\]*(?:\\\\.[^\"\\\\]*)*\"", RegexOptions.Compiled);
    private static readonly Regex LineComment = new("//.*$", RegexOptions.Compiled);

    /// <summary>
    /// Files excluded because they are the audit's own instrument — without this the audit counts its own
    /// vocabulary as evidence (`MinimalRouteToMaxwell()` alone would make "Maxwell" look computed). The same
    /// self-reference G_033 had to triage out of its substrate scan.
    /// </summary>
    /// <summary>
    /// Audit sources are EXCLUDED from the scan: an audit's own vocabulary is not evidence about AT, and an audit
    /// that derives a standard result must not be counted as the theory containing that result. This is the
    /// self-reference hazard G_033/G_035 handle with an explicit exclusion, and E_002's core file matters here
    /// especially — it reproduces a massless vacuum wave equation and a sourced Maxwell divergence BY HAND, to
    /// test whether AT has them. Counting it would invert E_002's own conclusion.
    /// </summary>
    private static readonly string[] SelfFiles =
    {
        "ElectromagnetismInventoryAudit.cs",
        "AtSourceScan.cs",
        "FieldEquationDerivationAudit.cs",
        "PhotonOntologyAudit.cs",
        "VectorSectorAudit.cs",
    };

    // ── The inventory ───────────────────────────────────────────────────────

    /// <summary>Every EM component the audit was asked to search for. Curated status, machine-checked counts.</summary>
    public static EmComponent[] Inventory()
    {
        var raw = new (string Name, string Category, EmStatus Status, string Source, string[] Vocab, string Basis)[]
        {
            // ── primitives ──
            ("gauge symmetry (U(1))", "primitive", EmStatus.Derived, "ResearchXH/GaugeSectorOrigin.cs",
                new[] { @"\bGaugeSector\b", @"\bZ_96\b", @"\bD96\b" },
                "QG161: U(1) = the rotation subgroup Z_96 of the circulant automorphism group D96 of the "
                + "observable attractor. A SECOND, incompatible origin also exists — see DoublyOriginatedGauge"),
            ("gauge group structure 1 + 3 + 8 = 12", "primitive", EmStatus.Derived, "ResearchXH/GaugeSectorOrigin.cs",
                new[] { @"\bSU\(2\)", @"\bSU\(3\)", @"[Dd]oublet" },
                "QG161: U(1) from Z_96, SU(2) from the 2D irreps, SU(3) from the three octave families; "
                + "1 + 3 + 8 = 12 = degree(C_96(1..6))"),
            ("electric charge (topological)", "primitive", EmStatus.Derived, "Research/ConstraintManifoldAnalyzer.cs",
                new[] { @"[Cc]harge quantization", @"winding number" },
                "charge is a topologically conserved integer (winding / monopole / instanton number), Q = g·n"),
            ("charge quantization", "primitive", EmStatus.Boundary, "Research/ConstraintManifoldAnalyzer.cs",
                new[] { @"[Cc]harge quantization" },
                "the INTEGERS are topological and derived; the elementary UNIT is not — it is fixed by the "
                + "coupling, which is itself contested (AlphaDisagreement)"),
            ("gauge connection / vector potential A_μ", "primitive", EmStatus.Boundary,
                "ResearchXH/LagrangianOrigin.cs",
                new[] { @"vector potential", @"[Cc]onnection" },
                "the gauge field is a link connection (QG63/65, lattice gauge theory) — a genuine structure — but "
                + "the potential that PROPAGATES is only stated, in the pure-gauge form A_μ = i·g⁻¹∂_μ g, with no "
                + "equation it obeys"),
            ("photon sector (massless U(1) boson)", "primitive", EmStatus.Boundary,
                "ResearchXH/GaugeSectorOrigin.cs",
                new[] { @"\b[Pp]hoton" },
                "the photon EXISTS as the U(1) generator (QG161) and is mapped as a massless n = 0 propagating "
                + "phase wave with no core. Its propagator and wave equation are absent"),

            // ── the declared dynamics: present as strings, not as computation ──
            ("Noether currents J^μ_em / J^μ_W / J^μ_s", "law", EmStatus.Assumed, "ResearchXH/LagrangianOrigin.cs",
                new[] { @"J\^μ_em", @"Noether current" },
                "NAMED, not computed. `ConservedCurrents()` returns the three currents as an array of STRINGS; "
                + "`NoetherCurrentsExist()` is a conjunction of two other audits' booleans rather than a "
                + "derivation. No executable line evaluates a current"),
            ("current conservation ∂_μJ^μ = 0", "law", EmStatus.Assumed, "ResearchXH/GaugeDynamicsOrigin.cs",
                new[] { @"current-conservation", @"conserved current", @"QedEquationDerived" },
                "QG243 offers 'the derived QED current-conservation law' ∂_μJ^μ = 0. That is a CONSERVATION "
                + "statement, kinematically implied by the symmetry — not the sourced field equation. Stated as "
                + "prose and asserted by a boolean; never evaluated"),
            ("field strength F^a_μν", "law", EmStatus.Assumed, "ResearchXH/LagrangianOrigin.cs",
                new[] { @"FieldStrengthForm", @"F\^a_μν", @"f\^abc" },
                "the generator-algebra curl F^a_μν = ∂_μA^a_ν − ∂_νA^a_μ + g f^abc A^b_μ A^c_ν is stated "
                + "exactly, with structure constants from the D96 commutators — and `FieldStrengthForm()` returns "
                + "it as a STRING. A definition restated is still a definition, and nothing computes it"),
            ("gauge kinetic term −¼F^a_μν F^aμν", "law", EmStatus.Assumed, "ResearchXH/LagrangianOrigin.cs",
                new[] { @"GaugeKineticTerm", @"1/4\) F" },
                "`GaugeKineticTerm()` returns the string \"L_gauge = −(1/4) F^a_μν F^aμν\". The kinetic term is "
                + "therefore DECLARED, not constructed — there is no executable action to vary"),
            ("derived Lagrangian density L", "law", EmStatus.Assumed, "ResearchXH/LagrangianOrigin.cs",
                new[] { @"LagrangianDensity", @"L_matter" },
                "`LagrangianDensity()` returns L = −(1/4)F^a F^a + iψ̄γ^μD_μψ − mψ̄ψ as a STRING. The file says "
                + "'the Lagrangian is NOT imported: its form ... is the unique minimal action consistent with the "
                + "D96 symmetries' — which is a MINIMALITY/selection argument. What AT derives is the symmetries "
                + "and the couplings; the functional form selected is the conventional minimal one"),

            // ── genuinely absent, and the two that are present ──
            ("Maxwell field equation ∂_μF^μν = J^ν", "law", EmStatus.Missing, "",
                new[] { @"d_μF\^?μν", @"∂_μF", @"Maxwell equation", @"Poynting" },
                "never written and never solved. AT reaches only the conservation of the current it cannot "
                + "compute; the sourced field equation relating the field to that current does not appear"),
            ("massless spin-1 wave equation (□A_μ = 0)", "law", EmStatus.Missing, "",
                new[] { @"spin-1 wave equation", @"\bProca\b", @"massless vector" },
                "AT's only massless wave equation is the SPIN-2 Fierz–Pauli □ψ_μν = 0 of the gravity sector "
                + "(MinimalPsiEquation). Nothing makes the photon propagate"),
            ("substrate wave equation", "law", EmStatus.Derived, "TemporalField.cs",
                new[] { @"wave equation", @"[Ww]aveEquation" },
                "the actualization field evolves by a discrete wave equation with damping (TemporalField) — the "
                + "one genuine, computed wave equation in the theory, and it is the substrate's, not the photon's"),
            ("speed of light c", "law", EmStatus.Derived, "Research/QuantumActionAnalyzer.cs",
                new[] { @"speed of light", @"luminal" },
                "c = ℓ/τ, emergent from the two AT scales"),

            // ── the contested coupling ──
            ("fine-structure constant α_em", "law", EmStatus.Refuted, "ResearchXH/GaugeCouplingOrigin.cs",
                new[] { @"AlphaEmMatches137", @"\bAlpha\b", @"137" },
                "TWO CONTRADICTORY RESULTS — see AlphaDisagreement. QG162 derives 1/α_em = 95 + 42 = 137; the "
                + "X-series reports α⁻¹ ≈ 100 and names α AT's largest remaining free parameter"),
        };

        var hits = Scan();
        return raw.Select(r => new EmComponent(r.Name, r.Category, r.Status, r.Source, r.Vocab, r.Basis)
        {
            DocumentedCount = r.Vocab.Sum(v => CountIn(hits.Raw, v)),
            ExecutableCount = r.Vocab.Sum(v => CountIn(hits.Executable, v)),
        }).ToArray();
    }

    public static EmComponent[] WithStatus(EmStatus s) => Inventory().Where(c => c.Status == s).ToArray();
    public static EmComponent[] Primitives() => Inventory().Where(c => c.Category == "primitive").ToArray();
    public static EmComponent[] Laws() => Inventory().Where(c => c.Category == "law").ToArray();
    public static EmComponent[] Missing() => WithStatus(EmStatus.Missing);
    public static EmComponent[] Assumed() => WithStatus(EmStatus.Assumed);

    /// <summary>Statuses whose live counts do not support the curated judgement.</summary>
    public static EmComponent[] UnsupportedClaims() => Inventory().Where(c => !c.StatusIsSupported).ToArray();

    /// <summary>Components written about but never computed — the G_027 defect class.</summary>
    public static EmComponent[] AssertedButNotComputed()
        => Inventory().Where(c => c.AssertedButNotComputed).ToArray();

    // ── The live scan ───────────────────────────────────────────────────────

    public static (string[] Raw, string[] Executable) Scan()
    {
        if (_cache is { } cached) return cached;

        var root = ResearchXH.CubicSubstrateAudit.FindRoot(ScanRelative);
        if (root is null) return (Array.Empty<string>(), Array.Empty<string>());

        var raw = new List<string>();
        var exe = new List<string>();
        foreach (var file in Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories)
                     .OrderBy(p => p, StringComparer.Ordinal))
        {
            if (file.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}")
                || file.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}")) continue;
            if (SelfFiles.Contains(Path.GetFileName(file), StringComparer.Ordinal)) continue;

            string text = File.ReadAllText(file);
            foreach (var line in text.Split('\n'))
                if (line.Trim().Length > 0) raw.Add(line);
            exe.AddRange(AtSourceScan.ExecutableLines(text));
        }
        _cache = (raw.ToArray(), exe.ToArray());
        return _cache.Value;
    }

    private static (string[] Raw, string[] Executable)? _cache;

    /// <summary>Drop the cached scan (the tree is read-only during a run; this exists for tests and clarity).</summary>
    public static void ResetScanCache() => _cache = null;

    /// <summary>
    /// The naive per-line strip, kept only so the audit can DEMONSTRATE why it is not enough: it cannot see a
    /// multi-line verbatim string, so report prose survives it and is counted as executable code.
    /// </summary>
    public static string StripNonCodePerLine(string line)
        => LineComment.Replace(StringLiteral.Replace(line, "\"\""), "");

    private static int CountIn(string[] lines, string pattern)
    {
        int n = 0;
        foreach (var l in lines) n += Regex.Matches(l, pattern).Count;
        return n;
    }

    // ── The mechanical proofs ───────────────────────────────────────────────

    /// <summary>
    /// PROVES the audit's central claim mechanically: the EM dynamics is declared, not computed. Reads
    /// `LagrangianOrigin.cs` and confirms that the Lagrangian density, the kinetic term, the field strength, the
    /// covariant derivative, the matter term and the conserved currents are all members returning STRINGS.
    /// </summary>
    public static string[] EmDynamicsIsStringReturning()
    {
        var root = ResearchXH.CubicSubstrateAudit.FindRoot(ScanRelative);
        if (root is null) return Array.Empty<string>();
        var f = Path.Combine(root, "ResearchXH", "LagrangianOrigin.cs");
        if (!File.Exists(f)) return Array.Empty<string>();

        string body = AtSourceScan.StripLiteralsAndComments(File.ReadAllText(f));
        var members = new[]
        {
            "ConservedCurrents", "FieldStrengthForm", "GaugeKineticTerm",
            "CovariantDerivative", "MatterTerm", "LagrangianDensity",
        };
        return members.Where(m =>
            Regex.IsMatch(body, @"public static\s+string(\[\])?\s+" + Regex.Escape(m) + @"\s*\(")).ToArray();
    }

    /// <summary>The booleans that declare the EM dynamics derived are conjunctions, not computations.</summary>
    public static string[] DerivationsThatAreConjunctions()
    {
        var root = ResearchXH.CubicSubstrateAudit.FindRoot(ScanRelative);
        if (root is null) return Array.Empty<string>();
        var f = Path.Combine(root, "ResearchXH", "LagrangianOrigin.cs");
        if (!File.Exists(f)) return Array.Empty<string>();

        string body = AtSourceScan.StripLiteralsAndComments(File.ReadAllText(f));
        var names = new[] { "NoetherCurrentsExist", "QedLagrangianDerived", "GeneratorAlgebraCloses" };
        // A conjunction: the member body just ANDs other audits' predicates.
        return names.Where(m =>
        {
            var block = Regex.Match(body, @"public static bool\s+" + Regex.Escape(m) + @"\s*\(\s*\)\s*=>([^;]+);");
            return block.Success && block.Groups[1].Value.Contains("&&");
        }).ToArray();
    }

    /// <summary>
    /// QG242's own admission, read live from the repository: the gauge dynamics was recorded as HOSTED/OPEN
    /// before QG243/244 declared it closed.
    /// </summary>
    public static bool DynamicsWasRecordedOpen()
    {
        var root = ResearchXH.CubicSubstrateAudit.FindRoot(ScanRelative);
        if (root is null) return false;
        var f = Path.Combine(root, "ResearchXH", "GaugeDynamicsOrigin.cs");
        if (!File.Exists(f)) return false;
        string text = File.ReadAllText(f);
        return text.Contains("HOSTED/OPEN", StringComparison.Ordinal)
            && text.Contains("propagators", StringComparison.Ordinal);
    }

    /// <summary>
    /// The audit's instrument defect, demonstrated live: a per-line strip cannot see a verbatim string spanning
    /// lines, so `J_Q` — pure report prose in ProtoMatterCollectiveAnalyzer — is counted as executable code.
    /// The G_027/G_033/G_035 scanners use exactly that per-line form.
    /// </summary>
    public static (int PerLineThinksExecutable, int FullFileSaysExecutable) VerbatimStringDefect()
    {
        var root = ResearchXH.CubicSubstrateAudit.FindRoot(ScanRelative);
        if (root is null) return (0, 0);
        var f = Path.Combine(root, "Resonance", "Theory", "ProtoMatterCollectiveAnalyzer.cs");
        if (!File.Exists(f)) return (0, 0);

        string text = File.ReadAllText(f);
        int perLine = text.Split('\n').Sum(l => Regex.Matches(StripNonCodePerLine(l), @"\bJ_Q\b").Count);
        int full = AtSourceScan.ExecutableLines(text).Sum(l => Regex.Matches(l, @"\bJ_Q\b").Count);
        return (perLine, full);
    }

    /// <summary>Does the code really use hand-picked probabilities in the gauge-emergence simulation?</summary>
    public static bool LiteralEvidenceConfirmed()
    {
        var root = ResearchXH.CubicSubstrateAudit.FindRoot(ScanRelative);
        if (root is null) return false;
        var f = Path.Combine(root, "Research", "GaugeSymmetryAnalyzer.cs");
        if (!File.Exists(f)) return false;

        string text = File.ReadAllText(f);
        var body = AtSourceScan.StripLiteralsAndComments(text);
        return body.Contains("new Random(42)")
            && (text.Contains("r < 0.15") || text.Contains("r < 0.22"))
            && text.Contains("COMPUTATIONAL EXPERIMENT");
    }

    // ── The contradictions ──────────────────────────────────────────────────

    /// <summary>A pair of audits giving incompatible answers about the same object.</summary>
    public sealed record Contradiction(string Subject, string First, string Second, string Why);

    /// <summary>
    /// U(1) is derived TWICE, from incompatible origins: the Z_96 rotation subgroup of a circulant
    /// automorphism group, and Aut(S¹) of a vortex moduli space. Neither audit references the other.
    /// </summary>
    public static Contradiction DoublyOriginatedGauge()
        => new("U(1) gauge group",
            "ResearchXH/GaugeSectorOrigin.cs (QG161): the rotation subgroup Z_96 ⊂ D96 of the circulant "
            + "automorphism group — deterministic, no fitted parameters",
            "Research/GaugeSymmetryAnalyzer.cs (AT-X050): Aut(S¹) of the vortex moduli space in a defect network",
            "two incompatible origins for the same group, in the same repository, both passing — the pattern "
            + "G_026 identified");

    /// <summary>The fine-structure constant is given two different values by two different AT routes.</summary>
    public static Contradiction AlphaDisagreement()
        => new("fine-structure constant α_em",
            "ResearchXH/GaugeCouplingOrigin.cs (QG162): 1/α = Σm + #doublets = 95 + 42 = 137",
            "Research/FineStructureAnalyzer.cs: α⁻¹ ≈ 100 — 'not 1/137 ... not a precise derivation'",
            "QG162 matches 137.036 to 0.03 %; the vortex-stability route gives ~100 and its own hostile review "
            + "calls α 'the LARGEST REMAINING FREE PARAMETER in AT'. Both cannot stand");

    /// <summary>A claimed computational experiment whose output is fixed by hand-picked literals.</summary>
    public static (string Where, string Defect, string Why) LiteralEvidence()
        => ("Research/GaugeSymmetryAnalyzer.cs — SimulateEmergence()",
            "Random(42) with hand-picked defect probabilities 0.15 / 0.07 / 0.18 / 0.02",
            "the method reports a 'COMPUTATIONAL EXPERIMENT' and concludes U(1) dominates because vortexCount is "
            + "largest — but vortexCount is a deterministic function of the literal 0.15. The audit's own hostile "
            + "review concedes 'the simulation is illustrative, not ab initio'. The mechanism claim is unaffected; "
            + "the simulation must not be counted as support for it");

    // ── The verdict, COMPUTED ───────────────────────────────────────────────

    public static string Verdict()
    {
        var inv = Inventory();
        bool anyMissing = inv.Any(c => c.Status == EmStatus.Missing);
        bool anyRefuted = inv.Any(c => c.Status == EmStatus.Refuted);
        bool anyPrimitiveDerived = Primitives().Any(c => c.Status == EmStatus.Derived);
        if (!anyMissing && !anyRefuted) return "DERIVED";
        if (anyMissing && anyPrimitiveDerived) return "BOUNDARY";
        return "REFUTED";
    }

    /// <summary>The one-line statement of where the EM sector stands.</summary>
    public static string WhereItStands()
        => "AT's electromagnetic KINEMATICS is derived — the U(1) group (twice over), the 1 + 3 + 8 = 12 gauge "
         + "structure, topological charge, the link-connection picture, and an emergent c. Its DYNAMICS is "
         + "DECLARED BUT NEVER COMPUTED: the Noether currents, the field strength, the kinetic term and the full "
         + "Lagrangian all exist only as string-returning members in QG244, and 'derived' is asserted by ANDing "
         + "other audits' booleans. What the program offers as its field equation is current CONSERVATION, not "
         + "the sourced Maxwell equation; no spin-1 wave equation exists; and the coupling contradicts itself.";

    // ── The four required outputs ───────────────────────────────────────────

    public static string OutputExistingPrimitives() => Section("1. EXISTING EM PRIMITIVES", Primitives());
    public static string OutputExistingLaws() => Section("2. EXISTING EM LAWS", Laws());

    public static string OutputMissingComponents()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. MISSING COMPONENTS (zero hits anywhere — nothing computes them, nothing states them)");
        foreach (var c in Missing())
            sb.AppendLine($"   [MISSING] {c.Name}\n             scan: documented = {c.DocumentedCount}, "
                        + $"executable = {c.ExecutableCount}\n             {c.Basis}");
        sb.AppendLine();
        sb.AppendLine("   DECLARED BUT NEVER COMPUTED (present as strings; executable count 0):");
        foreach (var c in Assumed().Where(x => x.AssertedButNotComputed))
            sb.AppendLine($"   [ASSUMED] {c.Name}  — documented {c.DocumentedCount}, executable {c.ExecutableCount}");
        return sb.ToString();
    }

    public static string MinimalRouteToMaxwell()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. MINIMAL ROUTE TO MAXWELL THEORY");
        sb.AppendLine("   Already present (no new ingredient needed):");
        sb.AppendLine("     · the gauge group U(1) and its 1 + 3 + 8 = 12 structure          (QG161, computed)");
        sb.AppendLine("     · the connection as a link phase                                  (QG63/65)");
        sb.AppendLine("     · F^a_μν, −¼F², D_μ, the matter term and L — all STATED           (QG244 strings)");
        sb.AppendLine("     · current CONSERVATION ∂_μJ^μ = 0                                 (QG243, AS-ASSERTED)");
        sb.AppendLine("     · an emergent c = ℓ/τ, and a computed substrate wave equation");
        sb.AppendLine("   The gap is three steps, and only the third is genuinely new physics:");
        sb.AppendLine("     (i)  COMPUTE WHAT IS DECLARED. J^μ_em is NAMED; nothing evaluates it. Turn");
        sb.AppendLine("          ConservedCurrents() into a Noether current computed from the D96 generator action.");
        sb.AppendLine("     (ii) MAKE THE ACTION REAL. Turn the string \"L = −¼F² + iψ̄γ^μD_μψ − mψ̄ψ\" into an actual");
        sb.AppendLine("          functional, so that 'the field equations are its Euler–Lagrange equations' is a");
        sb.AppendLine("          statement about something rather than a sentence about a string.");
        sb.AppendLine("     (iii) THEN THE NEW PHYSICS: elevate from conservation ∂_μJ^μ = 0 to the SOURCED field");
        sb.AppendLine("          equation ∂_μF^μν = J^ν, and read off □A_μ = 0 in the free case. This is the step");
        sb.AppendLine("          no current AT result takes, and the reason the photon does not propagate.");
        sb.AppendLine();
        sb.AppendLine("   BEFORE ALL THREE: resolve the two contradictions (U(1) has two origins; α has two values)");
        sb.AppendLine("   and re-check the minimality claim — 'the unique minimal action consistent with the D96");
        sb.AppendLine("   symmetries' is a SELECTION argument, so what remains to be shown is that D96 forces that");
        sb.AppendLine("   form rather than merely permitting it.");
        return sb.ToString();
    }

    private static string Section(string title, EmComponent[] items)
    {
        var sb = new StringBuilder();
        sb.AppendLine(title);
        foreach (var c in items)
            sb.AppendLine($"   [{c.Status.ToString().ToUpperInvariant(),-8}] {c.Name}\n"
                        + $"             where: {c.Source}\n"
                        + $"             scan:  documented = {c.DocumentedCount}, executable = {c.ExecutableCount}"
                        + (c.AssertedButNotComputed ? "   <-- ASSERTED BUT NEVER COMPUTED" : "") + "\n"
                        + $"             {c.Basis}");
        return sb.ToString();
    }
}
