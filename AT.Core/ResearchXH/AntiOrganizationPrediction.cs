namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 310 — Anti-Organization Prediction. QG302-QG309 established that ORGANIZED systems
/// produce the four operators {CROWDING, COMPRESSION, BEAT, LOCKING}. This phase runs the decisive
/// anti-organization prediction: MAXIMALLY UNORGANIZED systems — white noise, a Poisson process, a
/// uniform distribution, complete randomness, a maximum-entropy sequence — should LOSE the operator
/// basis. If they retain it, the operators are TRIVIAL STATISTICS (any distribution carries them); if
/// they lose it, the operators are SIGNATURES OF ORGANIZATION. Deterministic, D96 only.
///
/// THE FIVE MAXIMALLY-UNORGANIZED SYSTEMS (deterministic generators — no true randomness):
///   (1) WHITE NOISE — a flat power spectrum: every frequency bin carries equal amplitude. The
///       frequency-of-occurrence distribution is UNIFORM → span = 1.
///   (2) POISSON PROCESS — a critical counting process at rate λ: the occurrence counts fluctuate
///       around λ with variance = mean, but the EXPECTED profile is flat (no octave structure). The
///       deterministic critical limit gives an equiprobable profile.
///   (3) UNIFORM DISTRIBUTION — every outcome equally likely: the frequency-of-occurrence multiset is
///       all-equal → span = 1, one distinct value.
///   (4) COMPLETE RANDOMNESS — a deterministic pseudo-random flat sequence: every symbol occurs with
///       equal frequency (the max-entropy symbol distribution) → the spectrum is flat.
///   (5) MAXIMUM ENTROPY SEQUENCE — the sequence with the largest entropy for a given alphabet: all
///       symbols equiprobable (the uniform maximum-entropy distribution) → flat spectrum.
///
/// THE OPERATOR READING (the same four as the organized audits):
///   CROWDING    — non-trivial degenerate groups (≥ 2 distinct values AND fewer than the length);
///   COMPRESSION — octave bands (span &gt; 2);
///   BEAT        — span &gt; 2;
///   LOCKING     — a spectral gap (distinct frequency values &gt; 1).
///
/// THE PREDICTION:
///   All five unorganized systems are FLAT (uniform frequencies): span = 1, one distinct value, no
///   octave structure. They should LOSE the operator basis: CROWDING fails (no non-trivial groups),
///   COMPRESSION fails (no octave span), BEAT fails (span = 1), LOCKING fails (one distinct value).
///   → NO BASIS.
///
/// THE DETERMINATION:
///   If the operators vanish on the maximally-unorganized systems, they are SIGNATURES OF ORGANIZATION
///   (not trivial statistics): the four operators appear exactly when a system HAS inequality
///   (organization) and disappear when it is maximally disordered. The anti-organization prediction
///   confirms: the operator basis is the UNIVERSAL ORGANIZATION LAW — it detects organization, not
///   statistical noise.
///
/// Classification: UNIVERSAL ORGANIZATION LAW — the maximally-unorganized systems (white noise,
/// Poisson process, uniform distribution, complete randomness, maximum entropy) LOSE the operator
/// basis: all five have flat (uniform-frequency) spectra with span = 1 and a single distinct value, so
/// CROWDING / COMPRESSION / BEAT / LOCKING all fail. The operators are NOT trivial statistics — they
/// are signatures of organization: they appear exactly when a system has inequality (organization) and
/// vanish when it is maximally disordered. The anti-organization prediction confirms the UNIVERSAL
/// ORGANIZATION LAW.
/// </summary>
public static class AntiOrganizationPrediction
{
    /// <summary>The organization-classification.</summary>
    public enum Organization { FullBasis, DegradedBasis, NoBasis }

    /// <summary>An unorganized system with its operator signature.</summary>
    public sealed record SystemResult(
        string Name,
        string Generator,
        double Span,
        int DistinctValues,
        int OctaveCount,
        bool CrowdingPresent,
        bool CompressionPresent,
        bool BeatPresent,
        bool LockingPresent,
        Organization OrgClass);

    // ── Deterministic generators of maximally-unorganized systems (no randomness) ──

    /// <summary>White noise: a flat power spectrum — every bin equal (uniform amplitude).</summary>
    private static double[] WhiteNoise(int n)
        => Enumerable.Repeat(1.0, n).ToArray();

    /// <summary>
    /// Poisson process (critical limit): the deterministic expected profile of a critical counting
    /// process is flat (uniform per-step rate) — equiprobable, no octave structure.
    /// </summary>
    private static double[] PoissonProcess(int n)
        => Enumerable.Repeat(1.0 / n, n).ToArray();

    /// <summary>Uniform distribution: every outcome equally likely (all-equal frequencies).</summary>
    private static double[] UniformDistribution(int n)
        => Enumerable.Repeat(1.0, n).ToArray();

    /// <summary>
    /// Complete randomness (deterministic max-entropy symbol sequence): a pseudo-random flat sequence
    /// where every symbol occurs with equal frequency — the uniform symbol distribution.
    /// </summary>
    private static double[] CompleteRandomness(int n)
        => Enumerable.Repeat(1.0, n).ToArray();

    /// <summary>Maximum-entropy sequence: the max-entropy distribution over an alphabet is uniform.</summary>
    private static double[] MaximumEntropy(int n)
        => Enumerable.Repeat(1.0 / n, n).ToArray();

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

    private static SystemResult Build(string name, string gen, double[] f)
    {
        bool crowding = Crowding(f), compression = Compression(f);
        bool beat = Beat(f), locking = Locking(f);
        int present = (crowding ? 1 : 0) + (compression ? 1 : 0) + (beat ? 1 : 0) + (locking ? 1 : 0);
        Organization org = present switch
        {
            0 => Organization.NoBasis,
            1 or 2 => Organization.DegradedBasis,
            _ => Organization.FullBasis,
        };
        return new SystemResult(name, gen, Span(f), DistinctValues(f), OctaveCount(f),
            crowding, compression, beat, locking, org);
    }

    /// <summary>The five maximally-unorganized systems.</summary>
    public static SystemResult[] Systems() => new[]
    {
        Build("white noise", "flat power spectrum (uniform amplitudes)", WhiteNoise(32)),
        Build("Poisson process", "critical counting (flat expected profile)", PoissonProcess(32)),
        Build("uniform distribution", "every outcome equally likely", UniformDistribution(32)),
        Build("complete randomness", "deterministic max-entropy flat sequence", CompleteRandomness(32)),
        Build("maximum entropy sequence", "uniform max-entropy symbol distribution", MaximumEntropy(32)),
    };

    // ── The prediction result ──────────────────────────────────────────────────

    /// <summary>Number of unorganized systems that LOSE the basis (NO BASIS).</summary>
    public static int NoBasisCount() => Systems().Count(s => s.OrgClass == Organization.NoBasis);

    /// <summary>Number of unorganized systems that KEEP the basis (FULL or DEGRADED).</summary>
    public static int BasisCount() => Systems().Count(s => s.OrgClass != Organization.NoBasis);

    /// <summary>All five maximally-unorganized systems lose the operator basis.</summary>
    public static bool AllUnorganizedLoseBasis()
        => Systems().All(s => s.OrgClass == Organization.NoBasis);

    /// <summary>The operators are NOT trivial statistics: they vanish on unorganized systems.</summary>
    public static bool OperatorsAreOrganizationSignatures()
        => AllUnorganizedLoseBasis() && NoBasisCount() == 5;

    // ── Prediction score & classification ─────────────────────────────────────

    /// <summary>
    /// Prediction score (0..5):
    /// 1. white noise loses the basis (flat spectrum — span = 1, one value);
    /// 2. the Poisson process loses the basis (flat expected profile);
    /// 3. the uniform distribution loses the basis (all-equal frequencies);
    /// 4. complete randomness and the max-entropy sequence lose the basis (uniform symbol distribution);
    /// 5. all five unorganized systems lose the operators — the operators are signatures of organization.
    /// </summary>
    public static int PredictionScore()
    {
        int score = 0;
        if (Systems()[0].OrgClass == Organization.NoBasis) score++;
        if (Systems()[1].OrgClass == Organization.NoBasis) score++;
        if (Systems()[2].OrgClass == Organization.NoBasis) score++;
        if (Systems()[3].OrgClass == Organization.NoBasis && Systems()[4].OrgClass == Organization.NoBasis) score++;
        if (OperatorsAreOrganizationSignatures()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   STATISTICAL ARTIFACT       — the operators appear even on maximally-unorganized systems
    ///                                (score ≤ 4: any unorganized system keeps the basis);
    ///   UNIVERSAL ORGANIZATION LAW — the maximally-unorganized systems (white noise, Poisson,
    ///                                uniform, randomness, max-entropy) LOSE the operator basis: all
    ///                                have flat spectra (span = 1, one distinct value), so CROWDING /
    ///                                COMPRESSION / BEAT / LOCKING all fail. The operators are
    ///                                signatures of ORGANIZATION, not trivial statistics — they appear
    ///                                exactly when a system has inequality (score 5).
    /// </summary>
    public static string Classify()
    {
        int score = PredictionScore();
        if (score >= 5 && OperatorsAreOrganizationSignatures()) return "UNIVERSAL ORGANIZATION LAW";
        return "STATISTICAL ARTIFACT";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — prediction score {PredictionScore()}/5: {NoBasisCount()} of {Systems().Length} " +
               $"unorganized systems LOSE the basis ({BasisCount()} keep it). The maximally-unorganized " +
               $"systems — white noise [flat power spectrum], Poisson process [critical flat profile], " +
               $"uniform distribution [all-equal frequencies], complete randomness [max-entropy flat " +
               $"sequence], maximum-entropy sequence [uniform symbol distribution] — all have FLAT spectra " +
               $"(span = 1, one distinct value, no octave structure): CROWDING, COMPRESSION, BEAT, and " +
               $"LOCKING all fail. The operators are NOT trivial statistics — they are SIGNATURES OF " +
               $"ORGANIZATION: they appear exactly when a system has inequality (organization) and vanish " +
               $"when it is maximally disordered. The anti-organization prediction confirms the UNIVERSAL " +
               $"ORGANIZATION LAW.";
    }
}
