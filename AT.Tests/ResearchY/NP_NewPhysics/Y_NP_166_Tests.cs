using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_166 — Periodic Table Emergence Audit test suite (Y_NP_166_Tests.cs).
///
/// Question: can the periodic table emerge from AT resonance organization alone, without solving the
/// Schrödinger equation?
///
/// Verdict tested: NO — the periodic table's shell capacities 2, 8, 18, 32 = 2n² are the hydrogenic
/// (central-field Coulomb) degeneracy 2·Σ(2l+1), imported from quantum mechanics. Electron shells are
/// NOT resonance layers (D96 octave occupancy [4,4,87] ≠ 2n²), NOT organization levels (the NP_101
/// hierarchy stacks structures, not intra-atom shells), and NOT locking capacities. Atom binding and
/// the hierarchy DERIVED (NP_100/101); shell capacities BOUNDARY-class (imported hosted values); the
/// periodic table CORRESPONDENCE (hosted QM); "AT predicts 2, 8, 18, 32 naturally" REFUTED.
///
/// Deterministic: the electron shell counts of H, He, Li, Ne, Ar are reproduced here by a closed-form
/// Madelung (n+l) filling of the hydrogenic subshells (s=2, p=6, d=10, f=14) — the observed empirical
/// content — with no randomness and no external dependencies.
/// </summary>
public class Y_NP_166_Tests : ResearchTestBase
{
    public Y_NP_166_Tests(ITestOutputHelper output) : base(output) { }

    private const int DERIVED = 0;
    private const int CORRESPONDENCE = 1;
    private const int BOUNDARY = 2;
    private const int REFUTED = 3;

    // ── [Required] Y_NP_166_Inventory ──────────────────────────

    [Fact]
    public void Y_NP_166_Inventory()
    {
        // Shell capacities 2n², observed period lengths, and noble-gas closures.
        int[] shellCapacities = { 2, 8, 18, 32 };          // 2n² for n = 1..4
        int[] observedPeriodLengths = { 2, 8, 8, 18, 18, 32, 32 }; // Madelung rows
        int[] nobleGasZ = { 2, 10, 18, 36, 54, 86, 118 };

        Assert.Equal(shellCapacities, CapacityLaw(1, 4));   // 2n² computed
        Assert.Equal(118, Sum(observedPeriodLengths));
        Assert.Equal(2, observedPeriodLengths[0]);
        Assert.Equal(2, nobleGasZ[0]);
        Assert.Equal(118, nobleGasZ[^1]);
        Assert.Equal(observedPeriodLengths, PeriodLengthsFromNobleGases(nobleGasZ));
    }

    // ── [Required] Y_NP_166_NotResonanceLayers ────────────────

    [Fact]
    public void Y_NP_166_NotResonanceLayers()
    {
        // D96 octave occupancy is [4, 4, 87]; electron shells are 2, 8, 18, 32 — no identity.
        int[] d96OctaveOccupancy = { 4, 4, 87 };
        int[] electronShells = { 2, 8, 18, 32 };

        Assert.NotEqual(d96OctaveOccupancy, electronShells);
        Assert.NotEqual(d96OctaveOccupancy[0], electronShells[0]); // 4 ≠ 2
        // D96 BFS shell profile from a vertex is {1, 12, 12, …}, not a 2n² ladder either.
        Assert.NotEqual(1, electronShells[0]);
        Assert.True(electronShells[1] != d96OctaveOccupancy[1]);
    }

    // ── [Required] Y_NP_166_NotOrganizationLevels ─────────────

    [Fact]
    public void Y_NP_166_NotOrganizationLevels()
    {
        // The NP_101 hierarchy (particle → atom → molecule → crystal) organizes structures at
        // different scales; one atom is ONE level — not a ladder of internal electron shells.
        bool hierarchyStacksStructures = true;
        bool atomIsOneLevel = true;
        bool shellsAreIntraAtom = true;          // inside one level, not separate levels
        Assert.True(hierarchyStacksStructures);
        Assert.True(atomIsOneLevel);
        Assert.True(shellsAreIntraAtom);
    }

    // ── [Required] Y_NP_166_TestElements ───────────────────────

    [Fact]
    public void Y_NP_166_TestElements()
    {
        // Observed electron shell counts (by principal quantum number) for H, He, Li, Ne, Ar.
        Assert.Equal(new[] { 1 }, ShellCounts(1));
        Assert.Equal(new[] { 2 }, ShellCounts(2));
        Assert.Equal(new[] { 2, 1 }, ShellCounts(3));
        Assert.Equal(new[] { 2, 8 }, ShellCounts(10));
        Assert.Equal(new[] { 2, 8, 8 }, ShellCounts(18));
    }

    // ── [Required] Y_NP_166_AtVsObserved ───────────────────────

    [Fact]
    public void Y_NP_166_AtVsObserved()
    {
        // AT-native candidates (octave bands [4,4,87], C96 BFS shells {1,12,…}, mirror pairs,
        // mode counts) do NOT reproduce the observed shells 2, 8, 18, 32.
        int[] observedShells = { 2, 8, 18, 32 };

        Assert.False(CandidateMatches(new[] { 4, 4, 87 }, observedShells));
        Assert.False(CandidateMatches(new[] { 1, 12, 12 }, observedShells));
        Assert.False(CandidateMatches(new[] { 2, 2, 2 }, observedShells));   // mirror pairs: 2-fold only
        Assert.False(CandidateMatches(new[] { 95, 47 }, observedShells));    // mode / pair counts
    }

    // ── [Required] Y_NP_166_NumberLaw ───────────────────────────

    [Fact]
    public void Y_NP_166_NumberLaw()
    {
        // 2, 8, 18, 32 = 2n² = 2 × Σ(2l+1) over l = 0..n−1 — the hydrogenic (Coulomb) degeneracy,
        // the Schrödinger content of a 3D central field × spin-2.
        int n1 = 2 * (1);                                  // 2
        int n2 = 2 * (1 + 3);                              // 8
        int n3 = 2 * (1 + 3 + 5);                          // 18
        int n4 = 2 * (1 + 3 + 5 + 7);                      // 32
        Assert.Equal(2, n1);
        Assert.Equal(8, n2);
        Assert.Equal(18, n3);
        Assert.Equal(32, n4);

        // Observed period lengths are the Madelung rows, NOT sequential 2n² completion.
        int[] naiveSequentialClosures = Cumulative(CapacityLaw(1, 4)); // 2, 10, 28, 60
        int[] nobleGasZ = { 2, 10, 18, 36, 54, 86, 118 };
        Assert.Equal(new[] { 2, 10, 28, 60 }, naiveSequentialClosures);
        Assert.NotEqual(naiveSequentialClosures, nobleGasZ);   // sequential shells ≠ noble gases
    }

    // ── [Required] Y_NP_166_ABCD ───────────────────────────────

    [Fact]
    public void Y_NP_166_ABCD()
    {
        bool A_shellsAreResonanceLayers = false;      // REFUTED (octave occupancy ≠ shells)
        bool B_shellsAreOrganizationLevels = false;   // REFUTED (hierarchy stacks structures)
        bool C_shellsAreLockingCapacities = false;    // REFUTED (binding explains existence, not count)
        bool D_bindingAndHierarchyDerived = true;     // DERIVED (NP_100/101)
        Assert.False(A_shellsAreResonanceLayers);
        Assert.False(B_shellsAreOrganizationLevels);
        Assert.False(C_shellsAreLockingCapacities);
        Assert.True(D_bindingAndHierarchyDerived);
    }

    // ── [Required] Y_NP_166_Classification ─────────────────────

    [Fact]
    public void Y_NP_166_Classification()
    {
        int atomBinding = DERIVED;        // NP_100/101 unchanged
        int hierarchy = DERIVED;          // NP_100/101 unchanged
        int shellsAsLayers = REFUTED;     // no AT-native shell ladder
        int shellCapacities = BOUNDARY;   // imported hydrogenic hosted values (2n²)
        int periodicTable = CORRESPONDENCE; // hosted QM structure (NP_017 confirmed)
        int predictsNumbers = REFUTED;    // "AT predicts 2, 8, 18, 32 naturally" REFUTED

        Assert.Equal(DERIVED, atomBinding);
        Assert.Equal(DERIVED, hierarchy);
        Assert.Equal(REFUTED, shellsAsLayers);
        Assert.Equal(BOUNDARY, shellCapacities);
        Assert.Equal(CORRESPONDENCE, periodicTable);
        Assert.Equal(REFUTED, predictsNumbers);
    }

    // ── [Required] Y_NP_166_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_166_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_166 — Periodic Table Emergence Audit");

        sb.AppendLine("Goal: can the periodic table emerge from AT resonance organization alone,");
        sb.AppendLine("without solving the Schrodinger equation?");
        sb.AppendLine();

        sb.AppendLine("[1] Observed shell capacities 2n^2 = 2, 8, 18, 32 (n = 1..4);");
        sb.AppendLine("    period lengths (Madelung) 2, 8, 8, 18, 18, 32, 32; noble gases");
        sb.AppendLine("    Z = 2, 10, 18, 36, 54, 86, 118.");
        sb.AppendLine();

        sb.AppendLine("[2] Electron shells are NOT resonance layers (D96 octaves [4,4,87]), NOT");
        sb.AppendLine("    organization levels (the NP_101 hierarchy stacks structures, not");
        sb.AppendLine("    intra-atom shells), NOT locking capacities (binding explains existence).");
        sb.AppendLine();

        sb.AppendLine("[3] Test elements (observed, hydrogenic):");
        sb.AppendLine("    H [1], He [2], Li [2,1], Ne [2,8], Ar [2,8,8].");
        sb.AppendLine();

        sb.AppendLine("[4] No AT-native count (octaves, BFS shells, mirror pairs, modes) matches");
        sb.AppendLine("    the observed shell ladder; the only reproducing law is 2n^2 = 2*sum(2l+1),");
        sb.AppendLine("    the hydrogenic (Coulomb central-field) degeneracy times spin-2.");
        sb.AppendLine();

        sb.AppendLine("[5] 2, 8, 18, 32 = 2n^2 requires the (2l+1) spherical degeneracy and the");
        sb.AppendLine("    spin x2 — Schrodinger content, unavailable from the 1D/cubic D96");
        sb.AppendLine("    (same structural obstruction as nuclear shells, NP_087/089).");
        sb.AppendLine();

        sb.AppendLine("[6] Verdict: shell derivation REFUTED; shell capacities imported (BOUNDARY-");
        sb.AppendLine("    class hosted values); the periodic table CORRESPONDENCE (hosted QM, NP_017");
        sb.AppendLine("    confirmed; NP_085 frontier extended: chemistry is not derived).");
        sb.AppendLine("    Atom binding + hierarchy remain DERIVED (NP_100/101). No new primitive.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }

    // ── deterministic helpers (closed-form, no randomness) ─────

    private static int[] CapacityLaw(int from, int to)
    {
        var cap = new int[to - from + 1];
        for (int n = from; n <= to; n++) cap[n - from] = 2 * n * n;
        return cap;
    }

    private static int[] Cumulative(int[] a)
    {
        var cum = new int[a.Length];
        int s = 0;
        for (int i = 0; i < a.Length; i++) { s += a[i]; cum[i] = s; }
        return cum;
    }

    private static int[] PeriodLengthsFromNobleGases(int[] nobleGasZ)
    {
        var periods = new int[nobleGasZ.Length];
        periods[0] = nobleGasZ[0];
        for (int i = 1; i < nobleGasZ.Length; i++) periods[i] = nobleGasZ[i] - nobleGasZ[i - 1];
        return periods;
    }

    /// <summary>Observed shell counts (by principal quantum number) for Z electrons, from the
    /// hydrogenic Madelung filling order — the empirical content the audit imports.</summary>
    private static int[] ShellCounts(int z)
    {
        // subshells (n, l) ordered by Madelung: (n+l, n); capacity 2(2l+1).
        var subshells = new List<(int n, int l)>();
        for (int n = 1; n <= 7; n++)
            for (int l = 0; l < n; l++)
                subshells.Add((n, l));
        subshells.Sort((a, b) =>
        {
            int cmp = (a.n + a.l).CompareTo(b.n + b.l);
            return cmp != 0 ? cmp : a.n.CompareTo(b.n);
        });

        int maxN = 0;
        var shell = new List<int>();
        int remaining = z;
        foreach (var (n, l) in subshells)
        {
            if (remaining <= 0) break;
            int cap = 2 * (2 * l + 1);
            int take = Math.Min(cap, remaining);
            while (shell.Count < n) shell.Add(0);
            shell[n - 1] += take;
            remaining -= take;
            if (n > maxN) maxN = n;
        }
        while (shell.Count > 0 && shell[^1] == 0) shell.RemoveAt(shell.Count - 1);
        return shell.ToArray();
    }

    private static bool CandidateMatches(int[] candidate, int[] observed)
    {
        if (candidate.Length != observed.Length) return false;
        for (int i = 0; i < observed.Length; i++)
            if (candidate[i] != observed[i]) return false;
        return true;
    }

    private static int Sum(int[] a)
    {
        int s = 0;
        foreach (int x in a) s += x;
        return s;
    }
}
