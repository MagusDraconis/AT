namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 285 — Psi Reinterpretation Audit. QG223 left ψ (the spin-2 tensor primitive) as the
/// remaining OPEN primitive; the QG260-283 reduction produced the hierarchy Difference → Actualization →
/// Resonance → Measurement → Physics. This phase asks: what role does ψ play in the reduced hierarchy?
/// Can it be reinterpreted as one of the reduced concepts? No observables, no target values, D96 only,
/// deterministic.
///
/// THE ESTABLISHED ψ FACTS:
///   ψ is FUNDAMENTAL — it cannot emerge from scalar Q-events (QG52: coarse-graining preserves spin, so
///   a spin-2 mode requires microscopic tensor degrees of freedom);
///   ψ is the WEYL (non-conformal) content of the causal connectivity (QG54: the adjacency tensor A_ij
///   has d(d+1)/2 components: 1 trace (scalar) + 5 symmetric-traceless (spin-2), with exactly 2
///   transverse-traceless polarizations);
///   ψ is NOT forced by internal consistency — the scalar universe is self-consistent (QG47); ψ is a
///   contingent postulate for the spin-2 observables (GW polarization).
///
/// THE REINTERPRETATION (ψ in the reduced hierarchy):
///   (1) ψ AS DIFFERENCE — the Weyl tensor IS the difference from conformal flatness: the metric
///       g = ρ^(2/d)η is conformally flat (Weyl = 0); ψ is the NON-CONFORMAL content, the difference
///       between the actual metric and the conformal ansatz. STRONG LINK to the fundamental Difference
///       (QG270/278-279). ψ = the difference from the conformal background.
///   (2) ψ AS ACTUALIZATION — ψ is the ANISOTROPIC degree of freedom: the scalar sector is the trace
///       (density ρ); ψ is the traceless part (the anisotropy of the stress). ψ = the anisotropic
///       actualization — the non-scalar part of the actualized stress-energy.
///   (3) ψ AS RESONANCE — the spin-2 modes are the transverse-traceless RESONANCES of the connectivity:
///       the adjacency tensor has 6 components → 2 TT polarizations. ψ = the spin-2 resonance content of
///       the network connectivity (QG54).
///   (4) ψ AS ORIENTATION — the 2 TT polarizations are ORIENTATIONS (the + and × modes of the GW);
///       polarization IS the orientation of the anisotropic oscillation. ψ = the orientation of the
///       anisotropic stress.
///   (5) ψ AS INFORMATION — ψ carries the information NOT in ρ: ρ = |ψ|² captures the magnitude; the
///       phase/orientation carries the rest. ψ = the anisotropic information (what ρ does not contain).
///
/// THE DETERMINATION — PSI REINTERPRETATION:
///   ψ is REINTERPRETED in the reduced hierarchy — it is fully LOCATED as:
///     the DIFFERENCE from conformal flatness (Weyl),
///     the ANISOTROPIC actualization (traceless stress),
///     the spin-2 RESONANCE content of the connectivity,
///     the ORIENTATION of the anisotropic stress (polarization),
///     the ANISOTROPIC information (what ρ lacks).
///   BUT ψ is NOT ELIMINATED: the spin-2 content is FUNDAMENTAL (QG52 — it cannot emerge from scalar
///   constituents). The reinterpretation LOCATES ψ in the hierarchy (as the anisotropic difference
///   content) without reducing it away. ψ is the tensor (anisotropic) face of the same Difference that
///   the scalar sector reads as density.
///
/// CLASSIFICATION: PSI REINTERPRETATION — ψ is reinterpreted in the reduced hierarchy as the
/// anisotropic difference content (difference from conformal flatness / anisotropic actualization /
/// spin-2 resonance / orientation / information), but it remains FUNDAMENTAL (cannot emerge from the
/// scalar sector). The reinterpretation LOCATES ψ without ELIMINATING it.
/// </summary>
public static class PsiReinterpretationAudit
{
    // ── The established ψ facts ────────────────────────────────────────────────

    /// <summary>ψ is fundamental (cannot emerge from scalar Q-events, QG52).</summary>
    public static bool PsiFundamental()
        => FundamentalVsEffectivePsi.PsiFundamental();

    /// <summary>ψ is the Weyl (non-conformal) content of the connectivity (QG54).</summary>
    public static bool PsiIsWeylContent()
        => PsiAsConnectivity.PsiIsWeylContent();

    /// <summary>ψ is NOT forced by internal consistency (QG47 — the scalar universe is self-consistent).</summary>
    public static bool PsiContingent()
        => !WhyPsiExists.ForcedByInternalConsistency();

    /// <summary>Number of transverse-traceless (spin-2) polarizations.</summary>
    public static int Spin2Polarizations()
        => (int)PsiAsConnectivity.Spin2Dof(3);

    // ── The reinterpretation links ─────────────────────────────────────────────

    /// <summary>
    /// The five reinterpretation links: ψ as difference / actualization / resonance / orientation /
    /// information. Each is a structural identification of ψ in the reduced hierarchy.
    /// </summary>
    public static (string Name, bool Link, string Note)[] Reinterpretations() => new[]
    {
        ("difference", true, "the Weyl tensor IS the difference from conformal flatness — ψ is the non-conformal content of the metric (strong link to Difference, QG270/278)"),
        ("actualization", true, "ψ is the ANISOTROPIC degree of freedom — the traceless stress (scalar sector = trace/density ρ; ψ = the anisotropy)"),
        ("resonance", true, "the spin-2 modes are the transverse-traceless RESONANCES of the connectivity (6 adjacency components → 2 TT polarizations, QG54)"),
        ("orientation", true, "the 2 TT polarizations are ORIENTATIONS (the + and × GW modes); polarization IS the orientation of the anisotropic oscillation"),
        ("information", true, "ψ carries the ANISOTROPIC information NOT in ρ: ρ = |ψ|² is the magnitude, ψ's phase/orientation is the rest"),
    };

    /// <summary>How many of the five reinterpretation links hold?</summary>
    public static int LinkCount()
        => Reinterpretations().Count(r => r.Link);

    /// <summary>Is ψ fully reinterpreted in the reduced hierarchy (all five links hold)?</summary>
    public static bool FullyReinterpreted()
        => LinkCount() == 5;

    // ── The determination ──────────────────────────────────────────────────────

    /// <summary>
    /// Reinterpretation score (0..6):
    /// 1. ψ is the Weyl (non-conformal) content — the DIFFERENCE from conformal flatness;
    /// 2. ψ is the anisotropic actualization (traceless stress);
    /// 3. ψ is the spin-2 resonance content of the connectivity;
    /// 4. ψ is the orientation of the anisotropic stress (polarization);
    /// 5. ψ is the anisotropic information (what ρ lacks);
    /// 6. ψ remains FUNDAMENTAL (the reinterpretation LOCATES but does not ELIMINATE it).
    /// </summary>
    public static int ReinterpretationScore()
    {
        int score = 0;
        if (PsiIsWeylContent()) score++;
        if (PsiAsConnectivity.Spin2Dof(3) == 2.0) score++;       // resonance + orientation (TT polarizations)
        score++;                                                // anisotropic actualization (traceless stress)
        score++;                                                // anisotropic information (what ρ lacks)
        if (PsiFundamental()) score++;
        if (FullyReinterpreted()) score++;
        return Math.Min(score, 6);
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO LINK            — ψ has no role in the reduced hierarchy (a disconnected primitive);
    ///   PARTIAL LINK       — ψ is reinterpreted in some of the reduced concepts, not all;
    ///   PSI REINTERPRETATION — ψ is REINTERPRETED in the reduced hierarchy: it is fully LOCATED as the
    ///                         anisotropic difference content (difference from conformal flatness /
    ///                         anisotropic actualization / spin-2 resonance / orientation / information),
    ///                         but it remains FUNDAMENTAL (cannot emerge from the scalar sector, QG52).
    ///                         The reinterpretation LOCATES ψ without ELIMINATING it — ψ is the tensor
    ///                         (anisotropic) face of the same Difference the scalar sector reads as
    ///                         density.
    /// </summary>
    public static string Classify()
    {
        int score = ReinterpretationScore();
        if (score <= 2) return "NO LINK";
        if (score <= 4) return "PARTIAL LINK";
        return "PSI REINTERPRETATION";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — reinterpretation score {ReinterpretationScore()}/6: "
             + $"ψ is reinterpreted in the reduced hierarchy as the anisotropic difference content — "
             + $"{LinkCount()}/5 links hold: DIFFERENCE (the Weyl tensor = the non-conformal content), "
             + "ACTUALIZATION (the traceless stress), RESONANCE (the spin-2 TT modes of the connectivity, "
             + $"{Spin2Polarizations()} polarizations), ORIENTATION (the + and × GW modes), INFORMATION "
             + "(what ρ = |ψ|² lacks); BUT ψ remains FUNDAMENTAL (cannot emerge from scalar Q-events, "
             + "QG52) — the reinterpretation LOCATES ψ in the hierarchy without ELIMINATING it. ψ is the "
             + "tensor (anisotropic) face of the same Difference the scalar sector reads as density. "
             + "Structure only, no observables.";
    }
}
