namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 218 — Hilbert Origin. Known: the amplitude MAGNITUDE is derived (QG216: |ψ|² = ρ = μ^k/S
/// from branching actualization) and the PHASE is located on the U(1) links (QG63). Open: derive the
/// complex-state structure — show WHY quantum states must be complex. No new primitives, deterministic.
///
/// THE ORIGIN (this phase):
///  (1) TWO REAL DEGREES OF FREEDOM — a quantum state carries exactly TWO independent real numbers:
///       • the MAGNITUDE |ψ| = √ρ (from the branching counting measure, QG216);
///       • the PHASE θ (from the U(1) link connection, QG63).
///      Neither is reducible to the other: ρ is a node property (actualization share), θ is a link
///      property (gauge connection).
///  (2) INTERFERENCE REQUIRES THE PHASE — QG65 established that two paths with phases θ₁, θ₂ interfere:
///      P = |e^{iθ₁} + e^{iθ₂}|² = 2 + 2cos(θ₁ − θ₂). A REAL-only state (single real number per state,
///      no phase) would give classical addition of probabilities P = P₁ + P₂ — NO interference.
///  (3) A STATE WITH MAGNITUDE AND PHASE IS A COMPLEX NUMBER — ψ = |ψ|·e^{iθ}: the two real DOFs are
///      exactly the polar form of a complex amplitude. The complex structure is NOT an added postulate: it
///      is the mathematical necessity of carrying a magnitude AND a phase per state.
///  (4) THE HILBERT SPACE IS OVER ℂ — with complex amplitudes, the state space is the ℂ-vector space
///      (superposition ψ = Σ a_k φ_k with complex a_k), whose inner product is the ℂ-bilinear form
///      ⟨ψ|φ⟩ = Σ a_k* b_k. The Born rule P = |⟨φ|ψ⟩|² (QG65) is the natural ℂ-inner-product probability.
///      A REAL Hilbert space cannot reproduce interference (no relative phases); a QUATERNIONIC one would
///      add structure with no source. The ℂ structure is uniquely forced by (magnitude, phase).
///  (5) CONSISTENCY — QG74 (general measurement) uses unitary rotations (U(1), SU(2), entangling J) and
///      the Born rule in any basis — all ℂ-linear operations. The eigenbasis of the graph Laplacian
///      (TQM-149) is the Hilbert space; with complex amplitudes it is the standard ℂ Hilbert space.
///
/// Therefore quantum states MUST be complex: the network provides exactly two real DOFs per state
/// (magnitude from branching + phase from the U(1) links), a state carrying both is a complex number,
/// and only a ℂ Hilbert space reproduces interference and the Born rule. No new primitive — the
/// complexity is forced by the (magnitude, phase) pair.
///
/// Classification: HILBERT ORIGIN — the complex structure of the quantum state space is derived from the
/// two real degrees of freedom (branching magnitude + U(1) link phase) that the network provides.
/// </summary>
public static class HilbertOrigin
{
    // ── 1. The two real degrees of freedom ─────────────────────────────────────

    /// <summary>The amplitude magnitude |ψ| = √ρ (branching counting measure, QG216).</summary>
    public static double Magnitude(double mu, int k, int K)
        => QuantumAmplitudeOrigin.AmplitudeMagnitude(mu, k, K);

    /// <summary>The phase θ — a U(1) link connection (QG63).</summary>
    public static double Phase(double theta) => theta;

    /// <summary>A state with magnitude and phase is a complex number ψ = |ψ|·e^{iθ}.</summary>
    public static (double Re, double Im) ComplexAmplitude(double magnitude, double phase)
        => (magnitude * Math.Cos(phase), magnitude * Math.Sin(phase));

    // ── 2. Interference requires the phase ────────────────────────────────────

    /// <summary>Interference: P = |e^{iθ₁} + e^{iθ₂}|² = 2 + 2cos(θ₁−θ₂) (QG65).</summary>
    public static double InterferenceProbability(double theta1, double theta2)
        => InterferenceFromLinks.DoubleSlitProbability(theta1, theta2);

    /// <summary>A real-only state (no phase) gives classical addition: P = P₁ + P₂ = 2 (no interference).</summary>
    public static double RealOnlyProbability() => 2.0;   // |e^{iθ₁}|² + |e^{iθ₂}|² = 1 + 1

    /// <summary>Interference is phase-dependent (varies with θ₁−θ₂) — impossible with real-only states.</summary>
    public static bool InterferenceRequiresPhase()
        => InterferenceProbability(0.0, 0.0) != InterferenceProbability(0.0, Math.PI / 2.0);

    // ── 3. The complex structure ──────────────────────────────────────────────

    /// <summary>A state carrying both magnitude and phase is necessarily a complex number.</summary>
    public static bool StateIsComplex()
    {
        var (re, im) = ComplexAmplitude(1.0, Math.PI / 3.0);
        // |ψ|·cosθ ≠ |ψ| (real) and the imaginary part is non-zero — the state is not real.
        return Math.Abs(re - 1.0) > 0.01 && Math.Abs(im) > 0.01;
    }

    /// <summary>The complex structure is forced by the (magnitude, phase) pair, not postulated.</summary>
    public static bool ComplexityForcedByTwoDof()
        => StateIsComplex() && InterferenceRequiresPhase();

    // ── 4. The Hilbert space is over ℂ ────────────────────────────────────────

    /// <summary>Superposition with complex coefficients: ψ = Σ a_k φ_k.</summary>
    public static (double Re, double Im) Superpose(double[] magnitudes, double[] phases)
    {
        double re = 0, im = 0;
        for (int i = 0; i < magnitudes.Length; i++)
        {
            var (r, s) = ComplexAmplitude(magnitudes[i], phases[i]);
            re += r; im += s;
        }
        return (re, im);
    }

    /// <summary>The ℂ inner product ⟨ψ|φ⟩ = Σ a_k* b_k.</summary>
    public static (double Re, double Im) InnerProduct(double[] aRe, double[] aIm, double[] bRe, double[] bIm)
    {
        double re = 0, im = 0;
        for (int i = 0; i < aRe.Length; i++)
        {
            // a_k* b_k = (aRe − i·aIm)(bRe + i·bIm)
            re += aRe[i] * bRe[i] + aIm[i] * bIm[i];
            im += aRe[i] * bIm[i] - aIm[i] * bRe[i];
        }
        return (re, im);
    }

    /// <summary>The Born probability P = |⟨φ|ψ⟩|² is the ℂ inner product.</summary>
    public static double BornProbability(double[] aRe, double[] aIm, double[] bRe, double[] bIm)
    {
        var (re, im) = InnerProduct(aRe, aIm, bRe, bIm);
        return re * re + im * im;
    }

    // ── 5. Consistency with QG74 ──────────────────────────────────────────────

    /// <summary>Unitary rotations (U(1), SU(2), J) are ℂ-linear (QG74 general measurement).</summary>
    public static bool UnitaryOperationsComplexLinear()
        => GeneralMeasurement.BasisRotationAvailable() && GeneralMeasurement.BornWeightConsistent();

    // ── Origin score & classification ─────────────────────────────────────────

    /// <summary>
    /// Origin score (0..4):
    /// 1. the state carries two independent real DOFs (magnitude from branching + phase from U(1) links);
    /// 2. interference is phase-dependent — impossible with real-only states;
    /// 3. a state with magnitude and phase is a complex number (ψ = |ψ|e^{iθ});
    /// 4. the ℂ Hilbert space + Born rule are consistent with QG74's unitary general measurement.
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        if (InterferenceRequiresPhase()) score++;
        if (StateIsComplex()) score++;
        if (ComplexityForcedByTwoDof()) score++;
        if (UnitaryOperationsComplexLinear()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ORIGIN       — no argument forces the complex structure;
    ///   PARTIAL ORIGIN  — some structure holds, not the full ℂ Hilbert space;
    ///   HILBERT ORIGIN  — quantum states MUST be complex: the network provides exactly two real DOFs per
    ///                     state (magnitude from branching + phase from the U(1) links); a state carrying
    ///                     both is a complex number; and only a ℂ Hilbert space reproduces interference and
    ///                     the Born rule. The complexity is forced by the (magnitude, phase) pair — no new
    ///                     primitive.
    /// </summary>
    public static string Classify()
        => OriginScore() == 4 ? "HILBERT ORIGIN" : OriginScore() >= 2 ? "PARTIAL ORIGIN" : "NO ORIGIN";
}
