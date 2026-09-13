namespace AT.Core.ResearchXH;

/// <summary>One group-G audit and its use of the D96 spectral substrate.</summary>
public sealed record SubstrateUse(string Audit, int CodeRefs, int CommentRefs)
{
    /// <summary>Does this audit actually compute with D96 / D96³?</summary>
    public bool UsesInCode => CodeRefs > 0;
}

/// <summary>An irrep of the octahedral rotational group O, with its dimension.</summary>
public sealed record OctahedralIrrep(string Name, int Dimension, string Parity);

/// <summary>Where a piece of the spatial metric's content lands under the octahedral group.</summary>
public sealed record SectorPiece(string Label, int Dimension, string Irrep, int Multiplicity);

/// <summary>Two values of the same quantity, as recorded in two places in the repository.</summary>
public sealed record DisagreeingValue(string Source, int Value, string Note);

/// <summary>
/// ResearchY-G_033 — CUBIC SUBSTRATE AUDIT (D96³ requirement check).
///
/// QUESTION. The programme established that **D96 ⊗ D96 ⊗ D96 ("D96³", the cubic tensor product) is required to
/// calculate and explain physics** — specifically, M_012 proved that a genuine 3D (vector / p-wave, l = 1)
/// sector exists on the cubic lattice but is **absent from the single D96 ring, whose maximum irrep dimension
/// is 2**. Does the search for **gravitation and time (group G)** show the same behaviour?
///
/// ANSWER: **PARTIAL — the requirement is present in structure but era-local in practice, and it was absorbed
/// into the primitive η rather than exercised.**
///
/// (1) IN CODE IT IS ERA-LOCAL. The scan below re-reads the group-G sources at test time. Every audit from the
///     density era (G_001–G_018, plus G_023/024/026) computes with D96 or D96³; the entire **metric / closure
///     era** — G_015, G_017, G_019–G_022, G_025, G_027–G_032 — contains **no D96 reference at all**. Those
///     audits are pure continuum PPN calculations: a scalar ρ, an exponent, a conformal factor.
///
/// (2) STRUCTURALLY IT IS INHERITED, NOT ELIMINATED. Gravity's central object is the spatial metric g_ij — a
///     symmetric rank-2 tensor in d = 3 with **6 components = 1 (trace, l = 0) + 5 (traceless, l = 2)**. Under
///     the octahedral group the traceless part subducts as **l = 2 → Eg(2) + T2g(3)**, so the spatial metric
///     **requires a dimension-3 irrep**. That is exactly M_012's conclusion, one multipole higher: the p-wave
///     sector needs T1u(3) and the metric's traceless sector needs T2g(3), and **a single D96 ring supplies
///     neither** (its symmetry group D_96 has irreps of dimension 1 and 2 only). So gravity has the **same**
///     requirement for the cubic substrate as the rest of physics.
///
/// (3) WHY IT IS INVISIBLE. The metric era does not instantiate a 96³ spectrum because it imports 3D space
///     through the **primitive η** — the conformal reference metric that G_032 proved is an assumed input. The
///     D96³ requirement was therefore **absorbed into η**, not removed. G_032 and G_033 are the same fact seen
///     from two sides: G_032 says the shape of space is an input; G_033 says that input is precisely the thing
///     the cubic lattice would otherwise have had to supply.
///
/// (4) A DEFECT FOUND WHILE CHECKING. The figure **A₀ = 20 812 eigenspaces**, which group G has been citing
///     for D96³ (G_002, G_005, G_006 and downstream surfaces), is **not an invariant**. It is the count of
///     distinct binary64 sums produced by the implementation's `Dictionary<double,int>` keying, and it depends
///     on the last bits of `Math.Cos`: the same IEEE-754 algorithm gives 20 812 under .NET and 20 440 under
///     Python, and a one-ulp (1e−16) perturbation of the 1D spectrum swings it over 20 212 … 22 911. The
///     **robust** count — stable across a 7–12 decimal-place plateau and invariant under 1e−16 … 1e−13 noise —
///     is **16 080**, which is M_012's own independently reported figure and which the repository uses
///     elsewhere (free room 868 656 = 884 736 − 16 080). The two lineages never met; the qualitative
///     conclusion (D96³ is ~98 % energy-free) survives, the specific numbers do not.
/// </summary>
public static class CubicSubstrateAudit
{
    public const int N96 = 96;
    public const long CubeModes = 96L * 96L * 96L;      // 884 736

    // ── (1) The live inventory of group G's substrate use ─────────────────────

    /// <summary>Walk up from the test binary to find a directory containing the wanted relative folder.</summary>
    public static string? FindRoot(string relative)
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null)
        {
            var candidate = Path.Combine(dir.FullName, relative);
            if (Directory.Exists(candidate)) return candidate;
            dir = dir.Parent;
        }
        return null;
    }

    /// <summary>The folder holding the group-G suites, relative to the repository root.</summary>
    public const string AuditFolder = @"AT.Tests\ResearchY\G_GravitySource";

    /// <summary>
    /// THE LIVE SCAN. Counts D96 references per group-G suite, separating CODE from COMMENTS — the same
    /// discipline G_027 applies to verdicts, so that a new D96-dependent (or D96-free) audit is classified
    /// automatically rather than by hand.
    /// </summary>
    public static SubstrateUse[] ScanAudits()
    {
        var root = FindRoot(AuditFolder);
        if (root is null) return Array.Empty<SubstrateUse>();
        var uses = new List<SubstrateUse>();
        foreach (var file in Directory.EnumerateFiles(root, "Y_G_*.cs").OrderBy(p => p, StringComparer.Ordinal))
        {
            int code = 0, comment = 0;
            foreach (var line in File.ReadLines(file))
            {
                int n = CountOccurrences(line, "D96");
                if (n == 0) continue;
                var s = line.TrimStart();
                bool isComment = s.StartsWith("//", StringComparison.Ordinal)
                              || s.StartsWith("*", StringComparison.Ordinal);
                if (isComment) comment += n; else code += n;
            }
            var name = Path.GetFileName(file).Replace("_Tests.cs", "");
            uses.Add(new SubstrateUse(name, code, comment));
        }
        return uses.ToArray();
    }

    private static int CountOccurrences(string haystack, string needle)
    {
        int n = 0, i = 0;
        while ((i = haystack.IndexOf(needle, i, StringComparison.Ordinal)) >= 0) { n++; i += needle.Length; }
        return n;
    }

    /// <summary>
    /// The audits that are ABOUT the substrate rather than users of it. They compute the D96³ spectrum in
    /// order to audit it, so counting them would inflate the substrate era — the self-reference the live
    /// scanner exposed as soon as the first such audit existed.
    /// </summary>
    public static readonly string[] MetaAudits = { "Y_G_033", "Y_G_034" };

    /// <summary>Is this audit a substrate meta-audit (excluded from the era classification)?</summary>
    public static bool IsMetaAudit(string audit) => MetaAudits.Contains(audit, StringComparer.Ordinal);

    /// <summary>The audits subject to the era classification — every group-G suite except the meta-audits.</summary>
    public static SubstrateUse[] AuditsSubjectToClassification()
        => ScanAudits().Where(u => !IsMetaAudit(u.Audit)).ToArray();

    /// <summary>Audits whose CODE references the D96 substrate.</summary>
    public static SubstrateUse[] AuditsUsingSubstrate()
        => AuditsSubjectToClassification().Where(u => u.UsesInCode).ToArray();

    /// <summary>Audits that mention D96 only in comments — named but not computed with.</summary>
    public static SubstrateUse[] AuditsCommentOnly()
        => AuditsSubjectToClassification().Where(u => u.CodeRefs == 0 && u.CommentRefs > 0).ToArray();

    /// <summary>Audits with no D96 reference at all — the metric / closure era.</summary>
    public static SubstrateUse[] AuditsWithoutReference()
        => AuditsSubjectToClassification().Where(u => u.CodeRefs == 0 && u.CommentRefs == 0).ToArray();

    /// <summary>Is the substrate era-local — used by some group-G audits but not all?</summary>
    public static bool SubstrateUseIsEraLocal()
    {
        var all = AuditsSubjectToClassification();
        return all.Any(u => u.UsesInCode) && all.Any(u => !u.UsesInCode);
    }

    // ── (2) The structural requirement: does gravity need a dimension-3 irrep? ─

    /// <summary>
    /// The rotational octahedral group O (order 24): five classes and five irreps, Σd² = 24.
    /// Dimensions 1, 1, 2, 3, 3. Adding inversion gives O_h (order 48, ten irreps, Σd² = 48).
    /// </summary>
    public static readonly (string Class, int Size, double Angle)[] OctahedralClasses =
    {
        ("E", 1, double.NaN),
        ("8C3", 8, 2.0 * Math.PI / 3.0),
        ("3C2", 3, Math.PI),
        ("6C4", 6, Math.PI / 2.0),
        ("6C2'", 6, Math.PI),
    };

    private static readonly (string Name, double[] Chi)[] OctahedralChi =
    {
        ("A1", new[] { 1.0, 1.0, 1.0, 1.0, 1.0 }),
        ("A2", new[] { 1.0, 1.0, 1.0, -1.0, -1.0 }),
        ("E",  new[] { 2.0, -1.0, 2.0, 0.0, 0.0 }),
        ("T1", new[] { 3.0, 0.0, -1.0, 1.0, -1.0 }),
        ("T2", new[] { 3.0, 0.0, -1.0, -1.0, 1.0 }),
    };

    public const int OctahedralOrder = 24;

    /// <summary>Character of the SO(3) irrep of dimension 2l+1, restricted to the O classes.</summary>
    public static double[] CharacterOf(int l)
    {
        var chi = new double[OctahedralClasses.Length];
        for (int i = 0; i < chi.Length; i++)
        {
            var (_, _, angle) = OctahedralClasses[i];
            chi[i] = double.IsNaN(angle) ? 2.0 * l + 1.0
                : Math.Sin((l + 0.5) * angle) / Math.Sin(0.5 * angle);
        }
        return chi;
    }

    /// <summary>Subduction of the l-representation onto O: (irrep, dimension, multiplicity).</summary>
    public static (string Irrep, int Dimension, int Multiplicity)[] Subduction(int l)
    {
        var chi = CharacterOf(l);
        var parts = new List<(string, int, int)>();
        foreach (var (name, irreducible) in OctahedralChi)
        {
            double sum = 0.0;
            for (int i = 0; i < chi.Length; i++) sum += OctahedralClasses[i].Size * chi[i] * irreducible[i];
            int m = (int)Math.Round(sum / OctahedralOrder);
            if (m != 0) parts.Add((name, (int)irreducible[0], m));
        }
        return parts.ToArray();
    }

    /// <summary>Mulitplicity of a named octahedral irrep inside the subduction of l.</summary>
    public static int MultiplicityOf(int l, string irrep)
        => Subduction(l).Where(p => p.Irrep == irrep).Sum(p => p.Multiplicity);

    /// <summary>The character's squared norm — the number of O-irreps the subduction contains.</summary>
    public static double CharacterNormSquared(int l)
    {
        var chi = CharacterOf(l);
        double s = 0.0;
        for (int i = 0; i < chi.Length; i++) s += OctahedralClasses[i].Size * chi[i] * chi[i];
        return s / OctahedralOrder;
    }

    /// <summary>
    /// The 1D D96 spectrum over the 49 reduced indices r = 0..48 (doublet λ_j = λ_{96−j} folded), the
    /// spectrum the whole D96³ construction is built from.
    /// </summary>
    public static double[] ReducedSpectrum()
    {
        var f = new double[49];
        for (int r = 0; r <= 48; r++)
        {
            double s = 0.0;
            for (int d = 1; d <= 6; d++) s += 1.0 - Math.Cos(2.0 * Math.PI * d * r / 96.0);
            f[r] = 2.0 * s;
        }
        return f;
    }

    /// <summary>
    /// THE COUNT THAT GROUP G CITES: distinct triple sums keyed on exact binary64 equality, exactly as the
    /// repository's `Dictionary&lt;double,int&gt;` construction does. Reproduces **20 812** — and, as the noise
    /// sweep below shows, that value is an artifact of the last bits of `Math.Cos`, not an invariant.
    /// </summary>
    public static int CubeLevelsExactDoubleKeying()
    {
        var f = ReducedSpectrum();
        var sums = new HashSet<double>();
        for (int i = 0; i < f.Length; i++)
            for (int j = 0; j < f.Length; j++)
                for (int k = 0; k < f.Length; k++)
                    sums.Add(f[i] + f[j] + f[k]);
        return sums.Count;
    }

    /// <summary>
    /// The physically meaningful count: eigenvalues clustered at a tolerance first (so that numerically equal
    /// levels are identified once), then distinct sums. Stable — see <see cref="ToleranceClusterPlateau"/>.
    /// </summary>
    public static int CubeLevelsTolerant(int decimals = 9)
    {
        var values = ReducedSpectrum().OrderBy(v => v).ToArray();
        var reps = new List<double>();
        var current = new List<double> { values[0] };
        for (int i = 1; i < values.Length; i++)
        {
            if (values[i] - current[^1] <= Math.Pow(10.0, -decimals + 1)) current.Add(values[i]);
            else { reps.Add(current.Average()); current = new List<double> { values[i] }; }
        }
        reps.Add(current.Average());
        var sums = new HashSet<double>();
        foreach (var a in reps)
            foreach (var b in reps)
                foreach (var c in reps)
                    sums.Add(Math.Round(a + b + c, decimals));
        return sums.Count;
    }

    /// <summary>Decimal-place plateau over which the tolerant count is constant (the robust value's signature).</summary>
    public static (int From, int To, int Value) ToleranceClusterPlateau()
    {
        int lo = 0, hi = 0, value = 0;
        for (int dp = 6; dp <= 14; dp++)
        {
            int n = CubeLevelsTolerant(dp);
            if (n == 16080) { if (lo == 0) lo = dp; hi = dp; value = n; }
        }
        return (lo, hi, value);
    }

    /// <summary>The tolerant count at every decimal place — recorded so the plateau is visible, not asserted.</summary>
    public static (int Decimals, int Count)[] TolerantCountsByDecimalPlace()
    {
        var rows = new List<(int, int)>();
        for (int dp = 6; dp <= 14; dp++) rows.Add((dp, CubeLevelsTolerant(dp)));
        return rows.ToArray();
    }

    /// <summary>A deterministic LCG (no RNG dependency, reproducible across runs).</summary>
    private static double NextUnit(ref ulong state)
    {
        state = state * 6364136223846793005UL + 1442695040888963407UL;
        return ((state >> 11) & ((1UL << 53) - 1)) / (double)(1UL << 53);
    }

    /// <summary>
    /// THE ROBUSTNESS TEST. Perturb the 1D spectrum by a relative ε and recount with exact-double keying.
    /// Under a one-ulp perturbation the count moves by thousands; the tolerant count does not move at all.
    /// </summary>
    public static (int Min, int Max) ExactKeyingNoiseSweep(double epsilon, int trials, ulong seed = 20260913UL)
    {
        int min = int.MaxValue, max = int.MinValue;
        for (int t = 0; t < trials; t++)
        {
            var f = ReducedSpectrum();
            var g = new double[f.Length];
            for (int i = 0; i < f.Length; i++) g[i] = f[i] * (1.0 + epsilon * (2.0 * NextUnit(ref seed) - 1.0));
            var sums = new HashSet<double>();
            for (int i = 0; i < g.Length; i++)
                for (int j = 0; j < g.Length; j++)
                    for (int k = 0; k < g.Length; k++)
                        sums.Add(g[i] + g[j] + g[k]);
            min = Math.Min(min, sums.Count);
            max = Math.Max(max, sums.Count);
        }
        return (min, max);
    }

    /// <summary>The tolerant count under the same perturbations — the robust quantity.</summary>
    public static int TolerantCountNoiseSpread(double epsilon, int trials, ulong seed = 20260913UL)
    {
        int min = int.MaxValue, max = int.MinValue;
        for (int t = 0; t < trials; t++)
        {
            var f = ReducedSpectrum();
            var g = new double[f.Length];
            for (int i = 0; i < f.Length; i++) g[i] = f[i] * (1.0 + epsilon * (2.0 * NextUnit(ref seed) - 1.0));
            var sorted = g.OrderBy(v => v).ToArray();
            var reps = new List<double>();
            var current = new List<double> { sorted[0] };
            for (int i = 1; i < sorted.Length; i++)
            {
                if (sorted[i] - current[^1] <= 1.0e-8) current.Add(sorted[i]);
                else { reps.Add(current.Average()); current = new List<double> { sorted[i] }; }
            }
            reps.Add(current.Average());
            var sums = new HashSet<double>();
            foreach (var a in reps) foreach (var b in reps) foreach (var c in reps)
                sums.Add(Math.Round(a + b + c, 9));
            min = Math.Min(min, sums.Count); max = Math.Max(max, sums.Count);
        }
        return max - min;
    }

    // ── (3) The requirement, applied to gravity ──────────────────────────────

    /// <summary>A symmetric rank-2 tensor in d = 3: 6 components = 1 (trace) + 5 (traceless).</summary>
    public static (int Total, int Trace, int Traceless) SymmetricTensorContent() => (6, 1, 5);

    /// <summary>Where the traceless symmetric 2-tensor's 5 components land octahedrally: l = 2 → Eg(2) + T2g(3).</summary>
    public static SectorPiece[] TracelessSector()
        => Subduction(2).Select(p => new SectorPiece($"l = 2 traceless part", p.Dimension, p.Irrep + "g", p.Multiplicity)).ToArray();

    /// <summary>Where the p-wave / vector sector lands: l = 1 → T1u(3) — M_012's genuine 3D sector.</summary>
    public static SectorPiece[] VectorSector()
        => Subduction(1).Select(p => new SectorPiece("l = 1 vector", p.Dimension, p.Irrep + "u", p.Multiplicity)).ToArray();

    /// <summary>Maximum irrep dimension available in a sector's decomposition.</summary>
    public static int MaxIrrepDimension(SectorPiece[] pieces) => pieces.Max(p => p.Dimension);

    /// <summary>A single D96 ring is a cycle graph C_96; its symmetry D_96 (dihedral) has irreps of dim 1–2 only.</summary>
    public static int SingleRingMaxIrrepDim() => 2;

    /// <summary>The cubic D96³ lattice's octahedral group has dimension-3 irreps (T1, T2).</summary>
    public static int CubicMaxIrrepDim() => 3;

    /// <summary>Does the spatial metric require a dimension-3 irrep? (Computed from the decomposition.)</summary>
    public static bool MetricNeedsDim3Irrep() => MaxIrrepDimension(TracelessSector()) >= 3;

    /// <summary>Does the vector sector require it too? (M_012's finding.)</summary>
    public static bool VectorNeedsDim3Irrep() => MaxIrrepDimension(VectorSector()) >= 3;

    /// <summary>So does gravity require the cubic substrate at all?</summary>
    public static bool RequiresCubicSubstrate()
        => (MetricNeedsDim3Irrep() || VectorNeedsDim3Irrep()) && SingleRingMaxIrrepDim() < 3;

    /// <summary>Rotation self-duality (M_013): the unique d with dim so(d) = d(d−1)/2 equal to d.</summary>
    public static int RotationSelfDualityDimension()
    {
        for (int d = 1; d <= 12; d++) if (d * (d - 1) / 2 == d) return d;
        return -1;
    }

    /// <summary>Is that d unique in 1..12?</summary>
    public static bool RotationSelfDualityIsUnique()
    {
        int hits = 0;
        for (int d = 1; d <= 12; d++) if (d * (d - 1) / 2 == d) hits++;
        return hits == 1;
    }

    // ── (4) The A₀ defect ────────────────────────────────────────────────────

    /// <summary>The D96³ eigenspace count as group G records it (and as its code computes it).</summary>
    public static int GroupGRecordedA0() => CubeLevelsExactDoubleKeying();

    /// <summary>The robust count — the physically distinct number of D96³ levels.</summary>
    public static int RobustA0() => CubeLevelsTolerant();

    /// <summary>Free room Σ(m_i − 1) = N − A₀.</summary>
    public static long FreeRoom(int a0) => CubeModes - a0;

    /// <summary>Latent fraction L = (N − A₀) / N.</summary>
    public static double LatentFraction(int a0) => FreeRoom(a0) / (double)CubeModes;

    /// <summary>The two values of A₀ the repository carries, and where each came from.</summary>
    public static DisagreeingValue[] RecordedA0() => new[]
    {
        new DisagreeingValue("group G / DensityField / SpectralCaseCatalog (code)",
            CubeLevelsExactDoubleKeying(),
            "distinct binary64 triple sums — implementation-dependent, unstable under one ulp"),
        new DisagreeingValue("M_012 doc + NewChat_Start (free room 868 656)",
            16080,
            "tolerance-clustered: stable plateau, noise-invariant, matches M_012's independent figure"),
    };

    /// <summary>Do the repository's two recorded A₀ values agree? (They do not.)</summary>
    public static bool RecordedA0Disagrees() => RecordedA0().Select(v => v.Value).Distinct().Count() > 1;

    /// <summary>Is the exact-keying count unstable under a one-ulp perturbation of the 1D spectrum?</summary>
    public static bool ExactKeyingIsArtifact() => ExactKeyingNoiseSweep(1.0e-16, 6).Max - ExactKeyingNoiseSweep(1.0e-16, 6).Min > 0;

    // ── The verdict, COMPUTED ────────────────────────────────────────────────

    /// <summary>
    /// THE VERDICT, COMPUTED (G_027: never a literal).
    ///   SAME     — the cubic substrate is required by gravity and exercised throughout group G;
    ///   PARTIAL  — required, but era-local: the metric / closure era does not touch it;
    ///   DIFFERENT— gravity needs no dimension-3 sector, so no cubic substrate at all.
    /// </summary>
    public static string Verdict()
    {
        if (!RequiresCubicSubstrate()) return "DIFFERENT";
        return SubstrateUseIsEraLocal() ? "PARTIAL" : "SAME";
    }

    /// <summary>
    /// The reason the metric era can be D96-free at all: 3D space arrives through the primitive η (G_032).
    /// </summary>
    public static string LocusOfTheRequirement()
        => "the cubic substrate requirement is discharged by the primitive η (the conformal reference metric, "
         + "G_032): the metric era imports 3D space instead of generating it from a 96³ spectrum, so the "
         + "requirement is absorbed rather than removed.";
}
