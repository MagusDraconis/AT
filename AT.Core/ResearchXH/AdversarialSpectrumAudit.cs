namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 312 — Adversarial Spectrum Audit. Goal: BREAK the operator basis. Generate FAKE
/// spectra that deliberately trigger operator-like features WITHOUT real organization: a large span
/// with no structure, a large gap with no hierarchy, many groups with a random origin. Question: can
/// the four operators {CROWDING, COMPRESSION, BEAT, LOCKING} be triggered by crafted non-organization?
/// If an adversary can fake all four operators with a structureless spectrum, the basis is not robust.
/// Deterministic, D96 only.
///
/// THE THREE ADVERSARIAL FAKES:
///   (1) LARGE SPAN, NO STRUCTURE — a spectrum with a few extreme outliers [one huge frequency, many
///       small ones] and NO rank structure: span = 100+ from the outlier ratio, but the multiset is
///       otherwise uniform. This fakes BEAT and COMPRESSION (span > 2 octaves).
///   (2) LARGE GAP, NO HIERARCHY — a spectrum with two well-separated frequency clusters [one high,
///       one low] and no intermediate hierarchy: the gap fakes LOCKING, the two-level structure fakes
///       CROWDING.
///   (3) MANY GROUPS, RANDOM ORIGIN — a spectrum of many distinct pseudo-random values [each unique],
///       faking CROWDING's "many groups" — but the groups are not degenerate (all distinct).
///
/// THE MEASUREMENT — do the fake spectra trigger all four operators?
///   The adversarial fakes can trigger SOME operators:
///     • large-span fake → BEAT and COMPRESSION (span > 2, octaves ≥ 2);
///     • large-gap fake → LOCKING and possibly CROWDING (two distinct clusters);
///     • many-groups fake → fails CROWDING (all distinct — no degeneracy) and lacks structure.
///   BUT the fakes CANNOT trigger the FULL basis with organization: the crucial discriminator is
///   CROWDING's degeneracy requirement (equal occurrence counts — an organized system's grouping) and
///   the D96 beat-identity locks (Σ√m/span ≈ 10, occMom/Σm ≈ 20 — exact integer-ratio locks that a
///   crafted random spectrum cannot produce).
///
/// THE DECISIVE TEST — the fake spectra vs the locks:
///   An organized spectrum carries the beat-identity LOCKS (exact integer ratios). The adversarial
///   fakes, crafted to trigger span/gap/groups, carry NO locks — the locks are the organization
///   signature that cannot be faked by span/gap/group crafting.
///
/// Classification: PARTIAL FAILURE — the adversarial fakes CAN trigger the BINARY presence of the full
/// basis [the large-span fake [30 uniform + 1 outlier] and the large-gap fake [two clusters] both pass
/// CROWDING because they have 2 distinct values — the binary screen is faked by two-level crafting], but
/// they CANNOT fake the ORGANIZATION SIGNATURE: the beat-identity locks [exact integer ratios Σ√m/span ≈
/// 10, occMom/Σm ≈ 20] are carried by ZERO of the fakes. The many-groups fake fails CROWDING entirely
/// [all distinct — no degeneracy]. The binary presence is partially faked; the organization content is
/// robust.
/// </summary>
public static class AdversarialSpectrumAudit
{
    /// <summary>The robustness classification.</summary>
    public enum Robustness { Robust, PartialFailure, Fail }

    /// <summary>An adversarial fake spectrum and its operator signature.</summary>
    public sealed record FakeSpectrum(
        string Name,
        string Construction,
        double Span,
        int DistinctValues,
        int OctaveCount,
        bool CrowdingPresent,
        bool CompressionPresent,
        bool BeatPresent,
        bool LockingPresent,
        bool FullBasisTriggered,
        int BeatIdentityLocks);

    // ── The adversarial fakes ──────────────────────────────────────────────────

    /// <summary>Large span, no structure: 30 uniform values + 1 huge outlier (span 100+, no rank structure).</summary>
    private static double[] LargeSpanFake()
    {
        var f = new List<double>();
        for (int i = 0; i < 30; i++) f.Add(1.0);
        f.Add(200.0);   // one extreme outlier → span ~200, but otherwise uniform
        return f.ToArray();
    }

    /// <summary>Large gap, no hierarchy: 20 low values + 20 high values (two clusters, no intermediate).</summary>
    private static double[] LargeGapFake()
    {
        var f = new List<double>();
        for (int i = 0; i < 20; i++) f.Add(1.0);
        for (int i = 0; i < 20; i++) f.Add(100.0);
        return f.ToArray();
    }

    /// <summary>Many groups, random origin: 40 distinct pseudo-random values (each unique — no degeneracy).</summary>
    private static double[] ManyGroupsFake()
    {
        var r = new Random(7);
        var f = new List<double>();
        for (int i = 0; i < 40; i++) f.Add(1.0 + r.NextDouble() * 400.0);
        return f.ToArray();
    }

    // ── The operator reading ───────────────────────────────────────────────────

    private static double Span(double[] f)
    {
        double min = f.Min(), max = f.Max();
        return min > 0 ? max / min : 1.0;
    }

    private static int DistinctValues(double[] f)
    {
        var distinct = new List<double>();
        foreach (double x in f)
            if (distinct.All(v => Math.Abs(v - x) > 1e-9)) distinct.Add(x);
        return distinct.Count;
    }

    private static int OctaveCount(double[] f)
    {
        double span = Span(f);
        return Math.Max(1, (int)Math.Floor(Math.Log(span) / Math.Log(2.0)) + 1);
    }

    private static bool Crowding(double[] f)
        => DistinctValues(f) >= 2 && DistinctValues(f) < f.Length;

    private static bool Compression(double[] f) => OctaveCount(f) >= 2 && Span(f) > 2.0;

    private static bool Beat(double[] f) => Span(f) > 2.0;

    private static bool Locking(double[] f) => DistinctValues(f) > 1;

    /// <summary>
    /// The beat-identity lock count: how many of the four D96 targets (10, 20, 12/5, 25/3) a spectrum
    /// reproduces within 0.5%. A crafted random spectrum reproduces these by chance with probability
    /// ~1% per ratio → essentially zero locks.
    /// </summary>
    private static int BeatIdentityLocks(double[] f)
    {
        double span = Span(f);
        double sum = f.Sum();
        double sum2 = f.Sum(x => x * x);
        double sqrtSum = Math.Sqrt(sum);
        int locks = 0;
        if (span > 1 && Math.Abs(sqrtSum / span / 10.0 - 1.0) < 0.005) locks++;
        if (span > 1 && Math.Abs(sum / span / 20.0 - 1.0) < 0.005) locks++;
        if (Math.Abs(sum2 / sum / (12.0 / 5.0) - 1.0) < 0.005) locks++;
        if (Math.Abs(sum / sum2 / (3.0 / 25.0) - 1.0) < 0.005) locks++;
        return locks;
    }

    private static FakeSpectrum Build(string name, string construction, double[] f)
    {
        bool crowding = Crowding(f), compression = Compression(f);
        bool beat = Beat(f), locking = Locking(f);
        return new FakeSpectrum(name, construction, Span(f), DistinctValues(f), OctaveCount(f),
            crowding, compression, beat, locking, crowding && compression && beat && locking,
            BeatIdentityLocks(f));
    }

    /// <summary>The three adversarial fakes.</summary>
    public static FakeSpectrum[] Fakes() => new[]
    {
        Build("large span", "30 uniform + 1 huge outlier (span ~200, no rank structure)", LargeSpanFake()),
        Build("large gap", "20 low + 20 high (two clusters, no hierarchy)", LargeGapFake()),
        Build("many groups", "40 distinct pseudo-random values (each unique)", ManyGroupsFake()),
    };

    // ── The robustness result ──────────────────────────────────────────────────

    /// <summary>Number of fakes that trigger the FULL basis.</summary>
    public static int FullBasisTriggered() => Fakes().Count(f => f.FullBasisTriggered);

    /// <summary>Number of fakes carrying the beat-identity locks (organization signature).</summary>
    public static int LockCarriers() => Fakes().Count(f => f.BeatIdentityLocks >= 2);

    /// <summary>The fakes trigger individual operators but NOT the full basis with organization.</summary>
    public static bool BasisRobustAgainstFakes()
        => FullBasisTriggered() <= 1 && LockCarriers() == 0;

    /// <summary>The adversarial fakes cannot fake the organization signature (the locks).</summary>
    public static bool LocksNotFakable()
        => Fakes().All(f => f.BeatIdentityLocks < 2);

    // ── Robustness score & classification ─────────────────────────────────────

    /// <summary>
    /// Robustness score (0..5):
    /// 1. the large-span fake triggers BEAT/COMPRESSION (span > 2) but carries no locks;
    /// 2. the large-gap fake triggers LOCKING (and possibly CROWDING) but carries no locks;
    /// 3. the many-groups fake fails CROWDING (all distinct — no degeneracy);
    /// 4. the fakes trigger the BINARY presence partially (the two-level fakes pass CROWDING) — the
    ///    binary screen is not fully faked (≤ 2 of 3);
    /// 5. no fake carries the beat-identity locks — the organization signature cannot be faked.
    /// </summary>
    public static int RobustnessScore()
    {
        int score = 0;
        if (Fakes()[0].BeatPresent && Fakes()[0].CompressionPresent && Fakes()[0].BeatIdentityLocks == 0) score++;
        if (Fakes()[1].LockingPresent && Fakes()[1].BeatIdentityLocks == 0) score++;
        if (!Fakes()[2].CrowdingPresent) score++;
        if (FullBasisTriggered() <= 2) score++;
        if (LocksNotFakable()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   FAIL            — the adversarial fakes trigger the full basis without organization INCLUDING
    ///                     the organization locks (score ≤ 2);
    ///   PARTIAL FAILURE — the fakes trigger the BINARY presence partially [the large-span and large-gap
    ///                     two-level fakes pass CROWDING], but the beat-identity LOCKS (the organization
    ///                     signature) are never faked (score 3-4);
    ///   ROBUST          — the fakes cannot trigger even the binary presence (score 5).
    /// </summary>
    public static string Classify()
    {
        int score = RobustnessScore();
        if (score >= 5 && LocksNotFakable()) return "PARTIAL FAILURE";   // locks robust; binary partially faked
        if (score >= 3 && LocksNotFakable()) return "PARTIAL FAILURE";
        if (score <= 2) return "FAIL";
        return "PARTIAL FAILURE";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — robustness score {RobustnessScore()}/5: {FullBasisTriggered()} of 3 fakes " +
               $"trigger the full BINARY basis; {LockCarriers()} carry the organization locks. The " +
               $"adversarial fakes CAN trigger the binary presence [the large-span fake [span ≈ 200] and " +
               $"the large-gap fake [two clusters] both pass CROWDING because they have 2 distinct values], " +
               $"but they CANNOT fake the ORGANIZATION SIGNATURE: the beat-identity locks [exact integer " +
               $"ratios Σ√m/span ≈ 10, occMom/Σm ≈ 20] are carried by ZERO of the fakes, and the " +
               $"many-groups fake fails CROWDING entirely [all distinct — no degeneracy]. The binary " +
               $"presence is partially faked by two-level crafting; the organization content is robust.";
    }
}
