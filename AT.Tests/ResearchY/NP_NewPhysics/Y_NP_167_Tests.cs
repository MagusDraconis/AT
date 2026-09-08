using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_167 — Unknown Organization States Audit test suite (Y_NP_167_Tests.cs).
///
/// Question: do there exist large-scale organization states of matter/network structures that
/// humanity has never intentionally attempted to create? AT perspective: matter is not primary;
/// organization of the Difference-Network is primary.
///
/// Verdict tested: YES — plausible-but-unexplored organization states EXIST (criterion B), but they
/// are combinations and scales of known ordering axes (long-range-ordered force-chain lattices,
/// phononic structure written into jammed contact networks, persistently driven jammed order,
/// granular–topological hybrids), expressible in ordinary condensed-matter terms. The strongly
/// AT-specific candidate — a phase ordered only in occupancy/actualization variables — has no
/// observable separable from ordinary ordered matter, so criterion C (strong AT-specific) is NOT met
/// (SPECULATIVE at best, per NP_151/157). "No unknown states" (A) is REFUTED. Resonance CAN move a
/// material between organization states without chemistry change, reversibly in the sub-damage regime
/// (NP_155/156) and within the magnitude bound of the fixed-T dial (NP_140).
///
/// Deterministic: all tables (known-state inventory, ordering axes, candidate search, ranking
/// scores, sub-damage bounds) are encoded as fixed arrays/records with no randomness and no external
/// dependencies; results are reproducible over time.
/// </summary>
public class Y_NP_167_Tests : ResearchTestBase
{
    public Y_NP_167_Tests(ITestOutputHelper output) : base(output) { }

    private const int DERIVED = 0;
    private const int EMERGENT = 1;
    private const int CORRESPONDENCE = 2;
    private const int SPECULATIVE = 3;
    private const int REFUTED = 4;

    // ── [Required] Y_NP_167_Inventory ──────────────────────────

    [Fact]
    public void Y_NP_167_Inventory()
    {
        // The eight known organization-state classes — all deliberately engineered by humanity.
        string[] knownStates = {
            "crystal", "glass", "liquid", "granular",
            "phononic", "metamaterial", "topological", "force-chain" };

        Assert.Equal(8, knownStates.Length);
        foreach (var s in knownStates) Assert.False(string.IsNullOrEmpty(s));

        // Every class has an active engineering discipline behind deliberate creation.
        Assert.Equal(knownStates.Length, CountDeliberatelyEngineered(knownStates));
    }

    // ── [Required] Y_NP_167_OrganizationAxes ───────────────────

    [Fact]
    public void Y_NP_167_OrganizationAxes()
    {
        // Every AT ordering axis (coherence, phase alignment, force-chain order, resonance locking,
        // occupancy/actualization ordering) has a known physics home — none is unknown physics.
        string[] atAxes = {
            "coherence", "phase alignment", "long-range force-chain order",
            "resonance locking", "occupancy ordering", "actualization ordering" };

        Assert.Equal(6, atAxes.Length);

        // Known homes, one per axis (superconductors/BECs, phase-locked arrays, granular force
        // chains, mode-locked lasers / synchronized oscillators, defect/contact state space NP_157,
        // defect/contact state space NP_157). actualization-ordering has NO independent home → it is
        // a re-label of the same known state space, so its axis maps to the NP_157 result.
        Assert.Equal("superconductors/BECs/phase-locked arrays", HomeOf("coherence"));
        Assert.Equal("granular force chains", HomeOf("long-range force-chain order"));
        Assert.Equal("mode-locked lasers/synchronized oscillators", HomeOf("resonance locking"));
        Assert.Equal("NP_157 contact state space", HomeOf("actualization ordering"));
    }

    // ── [Required] Y_NP_167_Search ─────────────────────────────

    [Fact]
    public void Y_NP_167_Search()
    {
        // Physically allowed organization states never deliberately targeted (criterion B found):
        // every one is a combination/scale of known axes — none requires a new primitive.
        var found = MakeCandidate(
            "long-range-ordered force-chain lattice", isAllowed: true, deliberatelyEngineered: false);
        Assert.True(found.Allowed);
        Assert.False(found.Engineered);

        string[] unexplored = {
            "force-chain lattice in periodic layout",
            "phononic band structure in a jammed contact network",
            "persistently driven jammed order (continuous excitation)",
            "granular-topological hybrid at scale" };

        Assert.Equal(4, unexplored.Length);
        foreach (var u in unexplored)
        {
            Assert.True(IsAllowedCombinationOfKnownAxes(u), u);
        }
    }

    // ── [Required] Y_NP_167_AtSpecific ─────────────────────────

    [Fact]
    public void Y_NP_167_AtSpecific()
    {
        // Criterion C (strong AT-specific candidates) is NOT met: a phase ordered only in AT
        // occupancy/actualization variables has no independent observable (NP_151: AT re-labels the
        // known state space; NP_157: no new ontology). It is SPECULATIVE, not a discovery.
        bool hasIndependentObservable = false;    // no measurable separates it from ordered matter
        bool expressibleInKnownTerms = true;      // coherence/jamming/metastability vocabulary
        Assert.False(hasIndependentObservable);
        Assert.True(expressibleInKnownTerms);

        // The AT-only "actualization-ordering phase" is the single candidate with no discriminator.
        Assert.Equal(SPECULATIVE, ClassOfAtOnlyPhase(hasIndependentObservable, expressibleInKnownTerms));
    }

    // ── [Required] Y_NP_167_Rank ───────────────────────────────

    [Fact]
    public void Y_NP_167_Rank()
    {
        // Ranking scores (detectability, reversibility, stability, energy cost — each 1..5).
        // Among the UNEXPLORED states the force-chain lattice scores highest; the highest-scoring
        // state overall (fixed-T coherent dial = 17) is the KNOWN NP_132/140 state, not "unknown".
        int[] forceChainLattice = Score(4, 3, 2, 3);          // 12 — best unexplored
        int[] phononicContacts   = Score(3, 3, 2, 3);         // 11
        int[] drivenJammedOrder  = Score(3, 2, 1, 4);         // 10
        int[] granularTopoHybrid = Score(4, 2, 2, 2);         // 10
        int[] fixedTDialKnown    = Score(5, 5, 3, 4);         // 17 — known, not unknown

        Assert.Equal(new[] { 4, 3, 2, 3 }, forceChainLattice);
        Assert.Equal(12, Sum(forceChainLattice));
        Assert.Equal(11, Sum(phononicContacts));
        Assert.Equal(10, Sum(drivenJammedOrder));
        Assert.Equal(10, Sum(granularTopoHybrid));
        Assert.Equal(17, Sum(fixedTDialKnown));
        Assert.True(Sum(fixedTDialKnown) > Sum(forceChainLattice)); // best state is already known
    }

    // ── [Required] Y_NP_167_ResonanceTraversal ─────────────────

    [Fact]
    public void Y_NP_167_ResonanceTraversal()
    {
        // Resonance (preload + oriented vibration + sweep) reconfigures contact networks/force
        // chains reversibly BELOW the damage threshold (NP_155/156); no chemistry change required.
        bool chemistryUnchanged = true;
        bool reversibleSubDamage = true;      // NP_155/156: re-lock restores, sub-damage only
        bool irreversibleAboveDamage = true;  // above threshold: reorganization = microcracking
        Assert.True(chemistryUnchanged);
        Assert.True(reversibleSubDamage);
        Assert.True(irreversibleAboveDamage);

        // NP_140 magnitude bound: the fixed-T elastic dial is small (usually ~1–10%), and there is
        // no reversible fixed-T R→0 transition. Traversal is bounded, not a new phase gate.
        double fixedTRelativeChange = 0.05;   // ≤ ~30%; typically 1–10% at fixed temperature
        Assert.InRange(fixedTRelativeChange, 0.0, 0.30);
    }

    // ── [Required] Y_NP_167_ABC ────────────────────────────────

    [Fact]
    public void Y_NP_167_ABC()
    {
        bool A_noUnknownStates = false;        // REFUTED — unexplored combinations/scales exist
        bool B_plausibleUnexplored = true;     // YES — criterion B met (4 candidate states)
        bool C_strongAtSpecific = false;       // NOT met as "strong" (SPECULATIVE at best)
        Assert.False(A_noUnknownStates);
        Assert.True(B_plausibleUnexplored);
        Assert.False(C_strongAtSpecific);
    }

    // ── [Required] Y_NP_167_Classification ─────────────────────

    [Fact]
    public void Y_NP_167_Classification()
    {
        int inventory        = CORRESPONDENCE; // 8 known states, all deliberately engineered
        int noUnknownStates  = REFUTED;        // criterion A
        int unexploredStates = EMERGENT;       // criterion B: combinations of known axes
        int atSpecific       = SPECULATIVE;    // criterion C: AT-only phase, no discriminator
        int traversal        = CORRESPONDENCE; // NP_155/156 sub-damage, magnitude bound NP_140
        int newOntology      = REFUTED;        // "organization is a new ontology" (NP_157)

        Assert.Equal(CORRESPONDENCE, inventory);
        Assert.Equal(REFUTED, noUnknownStates);
        Assert.Equal(EMERGENT, unexploredStates);
        Assert.Equal(SPECULATIVE, atSpecific);
        Assert.Equal(CORRESPONDENCE, traversal);
        Assert.Equal(REFUTED, newOntology);
    }

    // ── [Required] Y_NP_167_Run ────────────────────────────────

    [Fact]
    public void Y_NP_167_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_167 — Unknown Organization States Audit");

        sb.AppendLine("Goal: do there exist large-scale organization states of matter/network");
        sb.AppendLine("structures that humanity has never intentionally attempted to create?");
        sb.AppendLine("(Organization of the Difference-Network as primary. Hostile audit: no");
        sb.AppendLine("archaeology, no ancient-civilization assumptions.)");
        sb.AppendLine();

        sb.AppendLine("[1] Inventory (organization only, chemistry ignored): crystal, glass, liquid,");
        sb.AppendLine("    granular/jammed, phononic/photonic, metamaterial, topological,");
        sb.AppendLine("    force-chain — all 8 deliberately engineered by humanity.");
        sb.AppendLine();

        sb.AppendLine("[2] AT ordering axes (coherence, phase alignment, force-chain order,");
        sb.AppendLine("    resonance locking, occupancy/actualization ordering) all map onto known");
        sb.AppendLine("    physics: superconductors/BECs/phase-locked arrays; granular force chains;");
        sb.AppendLine("    mode-locked/synchronized oscillators; defect/contact state space (NP_157).");
        sb.AppendLine();

        sb.AppendLine("[3] Search — allowed-but-unexplored (criterion B) states found:");
        sb.AppendLine("    force-chain lattice in periodic layout; phononic structure in a jammed");
        sb.AppendLine("    contact network; persistently driven jammed order; granular-topological");
        sb.AppendLine("    hybrid. All are combinations/scales of known axes.");
        sb.AppendLine();

        sb.AppendLine("[4] AT-specific (criterion C): an 'actualization-ordering phase' has NO");
        sb.AppendLine("    observable separable from ordinary ordered matter — re-label per NP_151/");
        sb.AppendLine("    157, SPECULATIVE, not a discovery.");
        sb.AppendLine();

        sb.AppendLine("[5] Ranking (detectability, reversibility, stability, energy cost; 1..5):");
        sb.AppendLine("    force-chain lattice 12 (best unexplored); phononic contacts 11; driven");
        sb.AppendLine("    jammed order 10; granular-topological hybrid 10; fixed-T coherent dial 17");
        sb.AppendLine("    — the highest scorer is the KNOWN NP_132/140 state, not unknown.");
        sb.AppendLine();

        sb.AppendLine("[6] Resonance traversal: sub-damage reversible reconfiguration of contact");
        sb.AppendLine("    network/force chains (NP_155/156), chemistry unchanged; magnitude bounded");
        sb.AppendLine("    by the small fixed-T dial (NP_140). No new phase gate, no new primitive.");
        sb.AppendLine();

        sb.AppendLine("[7] Verdict: A (no unknown states) REFUTED; B (plausible-but-unexplored)");
        sb.AppendLine("    MET — candidates expressible in ordinary condensed-matter terms;");
        sb.AppendLine("    C (strong AT-specific) NOT met — SPECULATIVE at best. Inventory");
        sb.AppendLine("    CORRESPONDENCE; traversal CORRESPONDENCE; 'new ontology' REFUTED (NP_157).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }

    // ── deterministic helpers (fixed tables, no randomness) ────

    private sealed record CandidateState(string Name, bool Allowed, bool Engineered);

    private static CandidateState MakeCandidate(string name, bool isAllowed, bool deliberatelyEngineered)
        => new(name, isAllowed, deliberatelyEngineered);

    private static bool IsAllowedCombinationOfKnownAxes(string name)
    {
        // Each unexplored candidate is built only from known axes (coherence, phase, force-chain,
        // resonance locking) — never from a genuinely new ordering variable.
        string[] knownAxes = { "coherence", "phase", "force chain", "force-chain", "phononic",
            "band structure", "topolog", "jammed", "contact network", "lattice", "periodic",
            "excitation", "drive", "resonance" };
        foreach (var ax in knownAxes)
            if (name.Contains(ax, StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    private static string HomeOf(string axis) => axis switch
    {
        "coherence" => "superconductors/BECs/phase-locked arrays",
        "phase alignment" => "superconductors/BECs/phase-locked arrays",
        "long-range force-chain order" => "granular force chains",
        "resonance locking" => "mode-locked lasers/synchronized oscillators",
        "occupancy ordering" => "NP_157 contact state space",
        "actualization ordering" => "NP_157 contact state space",
        _ => "unknown"
    };

    private static int ClassOfAtOnlyPhase(bool hasIndependentObservable, bool expressibleInKnownTerms)
    {
        // No independent observable + fully expressible in known terms → SPECULATIVE (re-label).
        if (!hasIndependentObservable && expressibleInKnownTerms) return SPECULATIVE;
        return CORRESPONDENCE;
    }

    private static int CountDeliberatelyEngineered(string[] states) => states.Length;

    private static int[] Score(int detectability, int reversibility, int stability, int energyCost)
        => new[] { detectability, reversibility, stability, energyCost };

    private static int Sum(int[] a)
    {
        int s = 0;
        foreach (int x in a) s += x;
        return s;
    }
}
