using System.Globalization;

namespace AT.Core.ResearchQG;

public static class NegativePhaseAnalyzer
{
    public static NPAResult RunFullAnalysis()
    {
        var structures = BuildStructures();
        return new NPAResult(BuildA(),BuildB(structures),BuildC(structures),BuildD(),BuildE(),BuildF(structures),structures);
    }

    static PhaseStructure[] BuildStructures()
    {
        return new PhaseStructure[]
        {
            new PhaseStructure("θ→-θ (sign flip)","θ → -θ","Phase angle sign reversal. Equivalent to 2π-θ modulo 2π. Same point on phase circle.",
                "TRIVIAL: angle on S¹ is the same.","NO EFFECT: ∇θ → -∇θ cancels globally.","A: COORDINATE CONVENTION. θ and -θ parametrize the same circle S¹."),
            new PhaseStructure("Δθ=π (anti-phase)","θ₂=θ₁+π","Two oscillators π out of phase. Superposition: cos(ωt)+cos(ωt+π)=0. Total destructive interference.",
                "UNSTABLE as domain: walls attract and annihilate. STABLE as pairwise interference pattern.","LOCAL: superposition cancels phase gradient → flat region. No net gravity.","B: REAL PHYSICAL CONFIGURATION. Anti-phase = destructive interference. Used in noise cancellation."),
            new PhaseStructure("Anti-phase domain","Δθ=π across boundary","Extended region with phase shifted by π relative to background. Domain wall at boundary with tension.",
                "UNSTABLE: walls attract → collapse at c. Lifetime=R/c (QG-031). Topological protection can extend.","LOCALLY REPULSIVE at walls (∇²θ singularity). Net: walls contribute positive curvature.","B→C: PHYSICALLY REAL but UNSTABLE. Cosmos: domain walls from early universe phase transitions."),
            new PhaseStructure("Phase inversion (gradient)","∇θ → -∇θ","All spatial phase derivatives change sign. Affects gravity (QG-022: gravity~∇θ).",
                "UNSTABLE (QG-029): phase voids fill at c. Negative ∇θ is the repulsive gravity candidate.","REPULSIVE: -∇θ → negative effective curvature. Dark Energy at Gpc scale.","B: PHYSICALLY MEANINGFUL. This IS the repulsive sector. Unstable locally, stable cosmologically."),
            new PhaseStructure("Phase vortex (n=1)","∮∇θ·dl = 2π","Phase wraps by 2π around a line defect. Winding number = 1. Core: phase undefined (zero amplitude).",
                "STABLE: topological protection. Cannot be unwound by continuous deformation. Tunneling decay only.","LOCAL: vortex core = negative phase gradient ring. Attractive in far field.","C: STABLE ARCHITECTURE. Real in superfluids, superconductors, cosmic strings."),
            new PhaseStructure("Phase vortex (n>1)","∮∇θ·dl = 2πn","Multiple winding. Higher topological charge. Energy ~ n² → unstable to splitting into n=1 vortices.",
                "METASTABLE: higher energy than n=1 array. Decays by fission.","MULTI-CORE: n separate phase gradient rings.","C: METASTABLE ARCHITECTURE. Exists but decays to lower-n states."),
            new PhaseStructure("Particle as winding","∮∇θ·dl = 2π (confined)","Particle = compact region with internal phase winding. Winding number = topological charge = particle identity.",
                "STABLE: topological protection + architectural binding. This IS what particles ARE in AT (QG-027/028).","ATTRACTIVE: net positive ∇θ in far field. Mass = winding energy.","C→D: SPECULATIVE BUT COMPELLING. Particle = trapped phase winding. Explains stability and identity."),
            new PhaseStructure("Skyrmion (3D winding)","π₃(S²)=Z","3D topological texture. Phase wraps around internal S². Topological charge = baryon number candidate.",
                "STABLE: homotopy protection. π₃(S²)=Z → integer conserved charge.","ATTRACTIVE: net positive phase gradient.","C: STABLE ARCHITECTURE. Candidate for baryons (Skyrme model, 1961). AT natural host."),
            new PhaseStructure("Phase void (absence)","θ → undefined","Region where phase is undefined (no Q-events). NOT anti-phase — it's NO phase. Zero oscillation amplitude.",
                "INSTANT DEATH: fills at c (QG-031). Void = nothing → filled by surrounding phase.","NONE: void has no phase → no gradient → flat. REPULSIVE at boundary briefly.","A/B: NOT A STRUCTURE. Absence, not configuration. Fills at c. Category error to call it 'anti-phase'."),
            new PhaseStructure("HONEST: Phase sign is free","θ ∈ S¹","Phase is a CIRCLE. S¹ has no preferred origin. θ and -θ are the same manifold. The sign is a coordinate choice.",
                "STABLE: S¹ topology is immutable.","NONE: sign flip is a global symmetry. No observable difference.","A: COORDINATE. Phase sign = coordinate on S¹. Anti-phase = REAL (Δθ=π). Winding = REAL (topology)."),
        };
    }

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE PHASE CIRCLE: S¹ GEOMETRY");
        sb.AppendLine();
        sb.AppendLine("  Phase θ is an ANGLE — a point on the circle S¹ = [0, 2π) with");
        sb.AppendLine("  identification θ ≡ θ + 2π.");
        sb.AppendLine();
        sb.AppendLine("  S¹ HAS NO PREFERRED ORIGIN:");
        sb.AppendLine("    Any point can be labeled θ = 0. The choice is arbitrary.");
        sb.AppendLine("    θ → -θ maps S¹ to itself (a reflection).");
        sb.AppendLine("    This is a COORDINATE TRANSFORMATION, not new physics.");
        sb.AppendLine();
        sb.AppendLine("  WHAT 'NEGATIVE PHASE' ACTUALLY MEANS:");
        sb.AppendLine();
        sb.AppendLine("    1. Sign flip of the angle: θ → -θ ≡ 2π-θ (mod 2π).");
        sb.AppendLine("       Same point on S¹. TRIVIAL. Coordinate convention.");
        sb.AppendLine();
        sb.AppendLine("    2. Phase inversion of the gradient: ∇θ → -∇θ.");
        sb.AppendLine("       Flips the DIRECTION of spatial phase change.");
        sb.AppendLine("       THIS IS PHYSICALLY MEANINGFUL — opposite geometry.");
        sb.AppendLine();
        sb.AppendLine("    3. Anti-phase: Δθ = π between two oscillators.");
        sb.AppendLine("       Relative phase shift. Destructive interference.");
        sb.AppendLine("       REAL physical configuration with observable effects.");
        sb.AppendLine();
        sb.AppendLine("    4. Phase winding: ∮∇θ·dl = 2πn.");
        sb.AppendLine("       Topological charge. Integer-valued. Protected.");
        sb.AppendLine("       STABLE ARCHITECTURE. This is the 'real negative sector'.");
        sb.AppendLine();
        sb.AppendLine("  KEY DISTINCTION:");
        sb.AppendLine("    θ → -θ (sign flip): coordinate choice on S¹.");
        sb.AppendLine("    ∇θ → -∇θ (gradient inversion): anti-gravity sector.");
        sb.AppendLine("    Δθ = π (anti-phase): destructive interference.");
        sb.AppendLine("    ∮∇θ·dl = 2πn (winding): topological architecture.");
        sb.AppendLine();
        sb.AppendLine("  The S¹ phase circle is the TRUE degree of freedom.");
        sb.AppendLine("  Unlike ℓ and τ (magnitudes, QG-032), phase is CIRCULAR.");
        sb.AppendLine("  'Negative' on a circle is just the other side of the circle.");
        return sb.ToString();
    }

    static string BuildB(PhaseStructure[] structures)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ANTI-PHASE ARCHITECTURES");
        sb.AppendLine();
        sb.AppendLine("  Anti-phase (Δθ = π) is the most fundamental phase relationship");
        sb.AppendLine("  after in-phase (Δθ = 0).");
        sb.AppendLine();
        sb.AppendLine("  PHYSICAL REALITY:");
        sb.AppendLine("    Two oscillators with Δθ = π cancel in superposition.");
        sb.AppendLine("    cos(ωt) + cos(ωt+π) = cos(ωt) - cos(ωt) = 0.");
        sb.AppendLine("    This is DESTRUCTIVE INTERFERENCE — physically real.");
        sb.AppendLine();
        sb.AppendLine("  APPLICATIONS:");
        sb.AppendLine("    - Noise-canceling headphones (anti-phase sound)");
        sb.AppendLine("    - Double-slit dark fringes (path difference = λ/2)");
        sb.AppendLine("    - Antireflection coatings (thin film interference)");
        sb.AppendLine("    - Quantum eraser experiments");
        sb.AppendLine();
        sb.AppendLine("  GRAVITATIONAL CONSEQUENCE:");
        sb.AppendLine("    Anti-phase superposition cancels phase gradient locally.");
        sb.AppendLine("    ∇θ₁ + ∇θ₂ = ∇θ - ∇θ = 0 → flat spacetime.");
        sb.AppendLine("    This is NOT repulsive — it's NEUTRAL (no gravity).");
        sb.AppendLine();
        sb.AppendLine("  DOMAINS:");
        sb.AppendLine("    Extended anti-phase regions bounded by walls collapse at c.");
        sb.AppendLine("    Domain walls carry positive tension → positive curvature.");
        sb.AppendLine("    Net effect: wall energy partially cancels anti-phase.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Anti-phase is physically REAL. Pairwise interference");
        sb.AppendLine("    is stable. Extended anti-phase DOMAINS are unstable.");
        return sb.ToString();
    }

    static string BuildC(PhaseStructure[] structures)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("PHASE WINDING AND TOPOLOGICAL ARCHITECTURES");
        sb.AppendLine();
        sb.AppendLine("  Phase winding: ∮∇θ·dl = 2πn where n ∈ Z.");
        sb.AppendLine();
        sb.AppendLine("  TOPOLOGICAL PROTECTION:");
        sb.AppendLine("    Winding number n cannot be changed by continuous deformation.");
        sb.AppendLine("    To change n: must pass through θ=undefined (zero amplitude).");
        sb.AppendLine("    This is a TOPOLOGICAL BARRIER — energetic, not logical.");
        sb.AppendLine();
        sb.AppendLine("  VORTEX STABILITY (n=1):");
        sb.AppendLine("    Energy: E ~ ln(R/ξ) per unit length (2D).");
        sb.AppendLine("    Core size ξ ~ ℓ (Planck, in AT).");
        sb.AppendLine("    Single vortex is QUANTUM STABLE (lowest non-zero winding).");
        sb.AppendLine();
        sb.AppendLine("  WHY n=1 IS THE FUNDAMENTAL ARCHITECTURE:");
        sb.AppendLine("    - Lowest non-zero topological charge");
        sb.AppendLine("    - Cannot decay (topological protection)");
        sb.AppendLine("    - Cannot split (no n=½ or n=¼ possible)");
        sb.AppendLine("    - n>1 vortices CAN split into n× n=1 vortices");
        sb.AppendLine();
        sb.AppendLine("  PARTICLES AS PHASE WINDING:");
        sb.AppendLine("    Electron: could be n=1 phase vortex confined to compact region.");
        sb.AppendLine("    Proton: n=3 winding (3 quarks) or skyrmion (π₃(S²)=Z).");
        sb.AppendLine("    Photon: n=0 winding (pure propagating phase wave).");
        sb.AppendLine();
        sb.AppendLine("    This unifies:");
        sb.AppendLine("    - Particle identity = winding number");
        sb.AppendLine("    - Particle stability = topological protection");
        sb.AppendLine("    - Particle mass = winding energy (E ~ n²)");
        sb.AppendLine("    - Particle charge = coupling of winding to gauge fields");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Phase winding is THE stable architecture in AT.");
        sb.AppendLine("    It replaces 'particle' with 'topologically protected phase structure.'");
        sb.AppendLine("    Classification: C — STABLE ARCHITECTURE.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("REPULSIVE GEOMETRY FROM PHASE INVERSION");
        sb.AppendLine();
        sb.AppendLine("  QG-022: Gravity emerges from phase gradients.");
        sb.AppendLine("  QG-029: ∇θ → +∇θ = attraction, ∇θ → -∇θ = repulsion.");
        sb.AppendLine();
        sb.AppendLine("  PHASE INVERSION OF THE GRADIENT:");
        sb.AppendLine("    Normal matter: Q-events create positive phase density.");
        sb.AppendLine("    Phase decreases with distance: ∂θ/∂r < 0.");
        sb.AppendLine("    Curvature: positive → attractive gravity.");
        sb.AppendLine();
        sb.AppendLine("    Anti-phase region: phase SHIFTED by π.");
        sb.AppendLine("    But the GRADIENT can have same sign (∂θ/∂r still < 0).");
        sb.AppendLine("    Anti-phase ≠ inverted gradient!");
        sb.AppendLine();
        sb.AppendLine("    Inverted gradient: ∂θ/∂r > 0 (phase INCREASES outward).");
        sb.AppendLine("    This requires a negative Q-event source (fewer events inside).");
        sb.AppendLine("    This IS a phase void (QG-031). Fills at c.");
        sb.AppendLine();
        sb.AppendLine("  CRITICAL DISTINCTION:");
        sb.AppendLine("    Anti-phase (Δθ=π): interference effect. Cancels gradient LOCALLY.");
        sb.AppendLine("    Inverted gradient (∇θ→-∇θ): repulsive gravity. Void effect.");
        sb.AppendLine("    Anti-phase = superposition cancel. ∇θ inverted = source inversion.");
        sb.AppendLine();
        sb.AppendLine("  ONLY STABLE REPULSIVE: Λ(t) = α/√V(t) at cosmological scale.");
        sb.AppendLine("    Expansion provides the 'void' that generates negative effective ∇θ.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Phase inversion of gradient IS the repulsive sector.");
        sb.AppendLine("    Anti-phase is NOT the same thing — it's interference, not repulsion.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("GLOBAL STRUCTURE: THE COMPLETE PHASE SECTOR");
        sb.AppendLine();
        sb.AppendLine("  AT phase structures form a hierarchy:");
        sb.AppendLine();
        sb.AppendLine("  LEVEL 0: TRIVIAL");
        sb.AppendLine("    θ → -θ: coordinate flip on S¹. No physics. Classification A.");
        sb.AppendLine();
        sb.AppendLine("  LEVEL 1: INTERFERENCE (REAL, UNSTABLE AS DOMAINS)");
        sb.AppendLine("    Δθ = π: anti-phase between two sources. Destructive interference.");
        sb.AppendLine("    Stable as PAIRWISE relation, unstable as extended DOMAINS.");
        sb.AppendLine("    Classification B.");
        sb.AppendLine();
        sb.AppendLine("  LEVEL 2: GRADIENT INVERSION (REAL, UNSTABLE LOCALLY)");
        sb.AppendLine("    ∇θ → -∇θ: inverted phase gradient → repulsive effective curvature.");
        sb.AppendLine("    Local: unstable (voids fill at c). Cosmological: stable (DE).");
        sb.AppendLine("    Classification B (local), C (cosmological).");
        sb.AppendLine();
        sb.AppendLine("  LEVEL 3: TOPOLOGICAL WINDING (REAL, STABLE)");
        sb.AppendLine("    ∮∇θ·dl = 2πn: phase winding with integer topological charge.");
        sb.AppendLine("    Vortices, skyrmions. Protected against decay.");
        sb.AppendLine("    Classification C: STABLE ARCHITECTURE.");
        sb.AppendLine();
        sb.AppendLine("  LEVEL 4: PARTICLES AS WINDING (SPECULATIVE, COMPELLING)");
        sb.AppendLine("    Compact confined phase winding structures.");
        sb.AppendLine("    Particle identity = winding number.");
        sb.AppendLine("    Classification C→D: STABLE + FUNDAMENTAL.");
        sb.AppendLine();
        sb.AppendLine("  THE REAL 'NEGATIVE SECTOR' OF AT:");
        sb.AppendLine("    Not ℓ<0 or τ<0 (category errors, QG-032).");
        sb.AppendLine("    Not 'anti-gravity' (unstable, QG-029→031).");
        sb.AppendLine("    It is PHASE WINDING — the topological classification");
        sb.AppendLine("    of all possible stable frequency architectures.");
        return sb.ToString();
    }

    static string BuildF(PhaseStructure[] structures)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-28} {1,-16} {2}","Phase Structure","Classification","Stability"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var s in structures)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-28} {1,-16} {2}", s.Name, s.Classification.Split(':')[0].Trim(), s.Stability));
        }
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  OVERALL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: Negative phase (θ→-θ) = coordinate convention on S¹. (A)");
        sb.AppendLine("  Q2: Anti-phase (Δθ=π) = real destructive interference. (B)");
        sb.AppendLine("  Q3: Anti-phase domains = unstable (walls collapse). (B)");
        sb.AppendLine("  Q4: Phase gradient inversion = repulsive gravity. (B→C)");
        sb.AppendLine("  Q5: Domain walls form between opposite phase regions. (B)");
        sb.AppendLine("  Q6: Topological defects = phase winding, not phase inversion. (C)");
        sb.AppendLine("  Q7: Anti-phase alters superposition, not frequency. (B)");
        sb.AppendLine("  Q8: Particles as phase-winding structures: COMPELLING. (C→D)");
        sb.AppendLine("  Q9: Anti-phase domains influence geometry via wall tension. (B)");
        sb.AppendLine("  Q10: YES — Phase winding is the true 'negative sector' of AT.");
        sb.AppendLine();
        sb.AppendLine("  THE REAL NEGATIVE DEGREE OF FREEDOM:");
        sb.AppendLine("    Not ℓ<0 (category error, QG-032).");
        sb.AppendLine("    Not τ<0 (logical contradiction, QG-032).");
        sb.AppendLine("    Not local anti-gravity (unstable, QG-029→031).");
        sb.AppendLine();
        sb.AppendLine("    It is PHASE STRUCTURE on S¹:");
        sb.AppendLine("    - Sign flips: coordinate convention");
        sb.AppendLine("    - Anti-phase (Δθ=π): destructive interference");
        sb.AppendLine("    - Phase winding (∮∇θ·dl=2πn): TOPOLOGICAL ARCHITECTURE");
        sb.AppendLine();
        sb.AppendLine("    The circle S¹ has no 'negative' — it has OPPOSITE POINTS,");
        sb.AppendLine("    WINDING NUMBERS, and TOPOLOGICAL SECTORS.");
        sb.AppendLine("    This is mathematically richer than a sign.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION:");
        sb.AppendLine("    A: θ→-θ (coordinate), Δθ=π interference (real, unstable domains)");
        sb.AppendLine("    B: ∇θ→-∇θ (gradient inversion, repulsive sector)");
        sb.AppendLine("    C: Phase winding, topological defects, vortex architectures");
        sb.AppendLine("    C→D: Particles as compact winding structures");
        sb.AppendLine("  QG program: 33 experiments.");
        return sb.ToString();
    }
}
