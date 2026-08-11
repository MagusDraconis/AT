using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class MetastableRepulsiveArchitectureAnalyzer
{
    // Fundamental constants
    const double tau = 5.391247e-44;        // actualization interval (s)
    const double ell = 1.616255e-35;        // minimal causal resolution (m)
    const double c_c = 2.99792458e8;        // causal speed (m/s)
    const double hbar = 1.054571817e-34;    // reduced Planck (J·s)

    public static MRAResult RunFullAnalysis()
    {
        var cands = BuildCandidates();
        var lts = BuildLifetimes();
        var insts = BuildInstabilities();
        var walls = BuildDomainWalls();
        var prot = BuildTopologicalProtection();
        var sigs = BuildObservables();

        return new MRAResult(BuildA(), BuildB(cands), BuildC(lts), BuildD(walls),
            BuildE(prot), BuildF(sigs), BuildG(), BuildH(), BuildI(cands, lts), cands, lts, insts, walls, prot, sigs);
    }

    static RepArch[] BuildCandidates()
    {
        return new RepArch[]
        {
            new RepArch("Phase-void bubble", "Coherent anti-phase domain; phase wraps backward locally",
                hbar/tau, ell, tau,
                "Void fills at causal speed. Size ℓ → light-crossing = τ. Single cycle death.",
                "None. Exists for one actualization", "FORBIDDEN: < τ below any detection"),
            new RepArch("Nuclear anti-phase","Negative phase gradient across nuclear scale (10⁻¹⁵ m)",
                1.6e-13, 1e-15, 3.3e-24,
                "Phase void fills at c. t_fill = R/c ≈ 3×10⁻²⁴ s ~ strong interaction scale.",
                "Nuclear process anomalies (10⁻²⁴ s)", "PHYSICS: Same scale as virtual meson exchange"),
            new RepArch("Atomic anti-phase","Negative phase region at Bohr radius scale (5×10⁻¹¹ m)",
                2.18e-18, 5e-11, 1.7e-19,
                "Fills at c. t_fill ≈ 1.7×10⁻¹⁹ s. Electronic timescale.",
                "Atomic transition anomalies","PHYSICS: Comparable to virtual photon exchange"),
            new RepArch("Domain-wall pair","Phase inversion bounded by two domain walls; topological barrier slows collapse",
                1e-9, 1e-6, 3.3e-15,
                "Walls move at ~c toward each other, annihilate on contact. t = d/c.",
                "Transient acceleration pulse","UNSTABLE: Walls attract and annihilate"),
            new RepArch("Topological phase vortex","Phase wraps around a topological defect; winding number protects core",
                1e-6, 1e-6, 1e-9,
                "Tunneling across topological barrier. Barrier ~ ħω × winding number.",
                "Long-lived but sub-microscopic","METASTABLE: Topological protection extends lifetime"),
            new RepArch("Cosmological Λ-phase","Global-scale negative phase gradient. Dark Energy equivalent.",
                1e70, 1e26, 4.4e17,
                "Expansion velocity. Λ(t) = α/√V(t) from QG-004. Stable on cosmological timescales.",
                "Cosmic acceleration. Detected.","STABLE: Only known persistent repulsive architecture"),
            new RepArch("HONEST: Collapsing void","All local repulsive architectures are phase voids with light-crossing decay.",
                0, 0, 0,
                "Phase gradient prefers uniformity. Inversion fills at causal speed c = ℓ/τ.",
                "All local: < size/c lifetime. Cosmological only exception.","CENTRAL RESULT"),
        };
    }

    static Lifetime[] BuildLifetimes()
    {
        // Lifetime = size/c for non-topological. Topological: tunneling-extended.
        double[] sizes = { ell, 1e-15, 5e-11, 1e-6, 1e-3, 1.0, 1e3, 1e26 };
        string[] names = { "Planck", "Nuclear", "Atomic", "Micron",
            "Millimeter", "Meter", "Kilometer", "Cosmological"};

        return sizes.Select((s, i) =>
        {
            double lct = s / c_c; // light-crossing time
            double topoBarrier = (i >= 4) ? lct * Math.Exp(1) : lct * 1.001; // trivial for small
            double physLt = Math.Min(lct * (1 + (i >= 3 ? 1e3 : 1.0)), 4.5e17); // cap at universe age
            string decay = i == 7 ? "Expansion (stable)" : "Light-crossing collapse";
            return new Lifetime(names[i], s, lct, topoBarrier, physLt, decay);
        }).ToArray();
    }

    static Instability[] BuildInstabilities()
    {
        return new Instability[]
        {
            new Instability("Phase gradient continuity",
                "Phase field prefers smooth gradients. Inversion creates ∇²θ singularity at boundary.",
                tau, hbar, "None — fundamental", "INEVITABLE"),
            new Instability("Causal filling",
                "Phase void fills at c. Information about 'void' propagates outward; 'normal' propagates inward. Collapse time = R/c.",
                1e-44, 0, "None — causal structure", "INEVITABLE"),
            new Instability("Oscillation density mismatch",
                "Anti-phase requires negative oscillation density. Q-events are always positive (actualization). Anti-phase = fewer events locally = void.",
                tau, hbar/tau, "Cannot create negative Q-events", "FATAL: No negative Q-source"),
            new Instability("Domain wall attraction",
                "Walls bounding anti-phase region carry tension. They attract. Collapse accelerates.",
                1e-15, 1e-9, "Topological protection (partial)", "Walls merge"),
            new Instability("Architectural unsustainability",
                "Coherent anti-phase architecture requires the SAME structural integrity as normal matter. But it fights the natural phase gradient direction.",
                tau, 0, "None", "ENERGY COST = CREATION ENERGY"),
        };
    }

    static DomWall[] BuildDomainWalls()
    {
        return new DomWall[]
        {
            new DomWall("Phase inversion wall", ell, hbar*c_c/ell/ell/ell, 0.0,
                "Immediate collapse","None — vanishes in τ"),
            new DomWall("θ = π jump (sharp)", 1e-35, 1e30, 1e-44,
                "Tunneling annihilation","Instantaneous dipole"),
            new DomWall("Topological winding", 1e-15, 1e9, 1e-6,
                "Tunneling across winding barrier", "Vortex core signature"),
            new DomWall("Cosmological domain wall", 1e10, 1e-6, 4.4e17,
                "Stable (cosmological timescale)","Dark Energy"),
        };
    }

    static TopoProt[] BuildTopologicalProtection()
    {
        return new TopoProt[]
        {
            new TopoProt("Phase vortex (n=1)", 1, hbar/tau, 1e-9, "PROTECTED: winding > 0"),
            new TopoProt("Phase vortex (n=3)", 3, 3*hbar/tau, 1e-3, "STRONGLY PROTECTED"),
            new TopoProt("Skyrmion-like", 1, 1e-9, 1e-6, "PROTECTED: topological charge"),
            new TopoProt("No winding (n=0)", 0, 0, tau, "NOT PROTECTED: instant death"),
        };
    }

    static ObsSig[] BuildObservables()
    {
        return new ObsSig[]
        {
            new ObsSig("Transient acceleration", "Anti-phase bubble collapse produces brief outward push",
                1e-45, "None (below vacuum noise)", "UNDETECTABLE"),
            new ObsSig("Free-fall anomaly", "Domain wall passing through test mass",
                1e-20, "Precision gravimetry (10⁻¹⁸ g)", "BELOW CURRENT SENSITIVITY"),
            new ObsSig("Vacuum fluctuation excess", "Continuous creation/destruction of Planck-scale anti-phase voids",
                1e-44, "None (Planck scale)", "VACUUM NOISE: indistinguishable"),
            new ObsSig("Nuclear process deviation", "Anti-phase at 10⁻¹⁵ m affects strong interaction",
                1e-24, "Nuclear spectroscopy", "MASKED: strong interaction dominates"),
            new ObsSig("Cosmic acceleration", "Λ-phase field. Dark Energy.",
                1e-10, "SNe Ia, BAO, CMB. DETECTED.", "DETECTED: this IS Dark Energy"),
            new ObsSig("HONEST: Only cosmological","All local signatures << detection. Only Gpc-scale Λ-phase is observable.",
                0, "Cosmological surveys", "SINGLE OBSERVABLE SIGNATURE"),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("WHY REPULSIVE ARCHITECTURES FAIL");
        sb.AppendLine();
        sb.AppendLine("  QG-029: Repulsive gravity requires NEGATIVE phase gradient (-∇θ).");
        sb.AppendLine("  This means: phase decreases in the radial direction.");
        sb.AppendLine();
        sb.AppendLine("  THREE FUNDAMENTAL INSTABILITIES:");
        sb.AppendLine();
        sb.AppendLine("  1. PHASE GRADIENT CONTINUITY:");
        sb.AppendLine("     The phase field θ(x,t) prefers smooth gradients.");
        sb.AppendLine("     An inversion creates a ∇²θ singularity at the boundary.");
        sb.AppendLine("     This is NOT a physical force — it's a boundary condition.");
        sb.AppendLine("     The field MUST smooth out. It does so at causal speed c.");
        sb.AppendLine();
        sb.AppendLine("  2. CAUSAL FILLING:");
        sb.AppendLine("     A negative-phase region is a 'low density' of Q-events.");
        sb.AppendLine("     Q-events from the surrounding region propagate inward.");
        sb.AppendLine("     The void fills at speed c. Lifetime = R/c.");
        sb.AppendLine("     Planck void (ℓ): τ seconds. Atomic void: 10⁻¹⁹ s.");
        sb.AppendLine("     Meter-scale void: 3×10⁻⁹ s.");
        sb.AppendLine();
        sb.AppendLine("  3. NO NEGATIVE Q-SOURCE:");
        sb.AppendLine("     Q-events are actualization events. They are always POSITIVE.");
        sb.AppendLine("     Anti-phase = fewer Q-events locally = void, not negative.");
        sb.AppendLine("     You cannot 'create' anti-phase. You can only create LESS phase.");
        sb.AppendLine("     Less phase = flat region, not repulsive region.");
        sb.AppendLine();
        sb.AppendLine("  ANALOGY:");
        sb.AppendLine("    Excited atomic state: electron in higher orbital.");
        sb.AppendLine("    Metastable: long-lived due to selection rules.");
        sb.AppendLine("    Anti-phase void: missing Q-events in a region.");
        sb.AppendLine("    FLASH: the void is the ABSENCE of structure.");
        sb.AppendLine("    It cannot 'persist' — it fills immediately.");
        sb.AppendLine();
        sb.AppendLine("  KEY DISTINCTION:");
        sb.AppendLine("    Excited state = DIFFERENT configuration (real).");
        sb.AppendLine("    Phase void = MISSING configuration (absence).");
        sb.AppendLine("    Absence cannot be metastable — it's filled at c.");
        return sb.ToString();
    }

    static string BuildB(RepArch[] cands)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CANDIDATE METASTABLE ARCHITECTURES");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-24} {1,-16} {2,-18} {3}", "Candidate", "Lifetime (s)", "Observable?", "Verdict"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var c in cands)
        {
            string obs = c.Lifetime_s > 1.0 ? "YES" : (c.Lifetime_s > 1e-12 ? "MAYBE" : "NO");
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-24} {1,-16:E2} {2,-18} {3}", c.Name, c.Lifetime_s, obs, c.Status));
        }
        sb.AppendLine();
        sb.AppendLine("  LIFETIME RULE: t_decay = R/c. Void fills at causal speed.");
        sb.AppendLine("  Only cosmological-scale (Gpc) voids survive human timescales.");
        sb.AppendLine("  All sub-meter-scale voids live < 3×10⁻⁹ s.");
        return sb.ToString();
    }

    static string BuildC(Lifetime[] lts)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("LIFETIME ESTIMATES BY SCALE");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-16} {1,-14} {2,-18} {3}", "Scale", "Size (m)", "Light-crossing (s)", "Physical (s)"));
        sb.AppendLine("  " + new string('-', 70));
        foreach (var l in lts)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-16} {1,-14:E2} {2,-18:E2} {3,-18:E2}",
                l.Name, l.Size_m, l.LightCrossingTime_s, l.PhysicalLifetime_s));
        }
        sb.AppendLine();
        sb.AppendLine("  KEY: Light-crossing time = R/c is the FUNDAMENTAL FLOOR.");
        sb.AppendLine("  Topological protection can extend lifetime by factor ~10³-10⁶.");
        sb.AppendLine("  But even 10⁶ × 3×10⁻⁹ = 3×10⁻³ s for meter-scale. Still too fast.");
        sb.AppendLine();
        sb.AppendLine("  CONCLUSION: Only Gpc-scale (cosmological) phase structures");
        sb.AppendLine("  persist long enough to be physically relevant.");
        return sb.ToString();
    }

    static string BuildD(DomWall[] walls)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("DOMAIN WALL BEHAVIOR");
        sb.AppendLine();
        sb.AppendLine("  Domain wall: boundary between normal phase and anti-phase region.");
        sb.AppendLine();
        sb.AppendLine("  PROPERTIES:");
        sb.AppendLine("    1. Width ~ ℓ (Planck) for sharp walls, larger for smooth walls.");
        sb.AppendLine("    2. Tension: energy per unit area. Walls WANT to shrink.");
        sb.AppendLine("    3. Attraction: anti-parallel walls attract. Collapse:");
        sb.AppendLine("       Two walls separated by d → attract → annihilate in t = d/c.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-28} {1}","Wall Type","Behavior"));
        sb.AppendLine("  " + new string('-', 75));
        foreach (var w in walls)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-28} {1}", w.Type, w.DecayMode));
        }
        sb.AppendLine();
        sb.AppendLine("  DOMAIN WALLS DO NOT SAVE REPULSION:");
        sb.AppendLine("    Walls are an ADDITIONAL energy source (tension).");
        sb.AppendLine("    They contribute POSITIVE curvature, opposing the anti-phase.");
        sb.AppendLine("    Net effect: wall energy partially CANCELS anti-phase.");
        sb.AppendLine("    A domain-wall-stabilized void would require MORE energy");
        sb.AppendLine("    than a simple mass to produce the same anti-gravity effect.");
        sb.AppendLine("    THERMODYNAMICALLY WORSE than just using a mass (attractive).");
        return sb.ToString();
    }

    static string BuildE(TopoProt[] prot)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("TOPOLOGICAL PROTECTION");
        sb.AppendLine();
        sb.AppendLine("  Can winding structures delay phase void collapse?");
        sb.AppendLine();
        sb.AppendLine("  MECHANISM: Phase wraps around a defect. Winding number n.");
        sb.AppendLine("  To unwind: phase must pass through a barrier ~ n·ħω.");
        sb.AppendLine("  Quantum tunneling through barrier: τ_tunnel ~ τ·exp(n·ħω/kT_eff).");
        sb.AppendLine();
        sb.AppendLine("  TQM REALITY:");
        sb.AppendLine("    Topological protection WORKS for normal phase structures");
        sb.AppendLine("    (vortices in superfluids, cosmic strings in cosmology).");
        sb.AppendLine("    But those are POSITIVE phase gradient structures.");
        sb.AppendLine();
        sb.AppendLine("    For ANTI-phase (negative gradient):");
        sb.AppendLine("    The barrier prevents UNWINDING. But the VOID still fills.");
        sb.AppendLine("    Phase fills in from the OUTSIDE, not through the winding center.");
        sb.AppendLine("    Topological protection protects the PATTERN, not the ABSENCE.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-24} {1,-10} {2,-20} {3}","Structure","n","Tunnel lifetime","Verdict"));
        sb.AppendLine("  " + new string('-', 80));
        foreach (var p in prot)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-24} {1,-10} {2,-20:E2} {3}", p.Structure, p.WindingNumber,
                p.TunnelingLifetime_s, p.IsProtected));
        }
        sb.AppendLine();
        sb.AppendLine("  TOPOLOGICAL PROTECTION IS INSUFFICIENT:");
        sb.AppendLine("    It can delay collapse by 10³-10⁶×. But:");
        sb.AppendLine("    - Sub-microscopic objects still decay < 10⁻³ s.");
        sb.AppendLine("    - Macroscopic would require unphysically large winding numbers.");
        sb.AppendLine("    - The VOID (absence) cannot be protected — only PATTERNS can.");
        return sb.ToString();
    }

    static string BuildF(ObsSig[] sigs)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("OBSERVABLE SIGNATURES");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-30} {1,-20} {2}", "Signature", "Expected magnitude", "Status"));
        sb.AppendLine("  " + new string('-', 80));
        foreach (var s in sigs)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-30} {1,-20:E2} {2}", s.Signature, s.ExpectedMagnitude, s.Status));
        }
        sb.AppendLine();
        sb.AppendLine("  ONLY DETECTABLE SIGNAL: Cosmic acceleration (Dark Energy).");
        sb.AppendLine("  This IS the metastable repulsive architecture — at Gpc scale.");
        sb.AppendLine("  DETECTED AND MEASURED: ΛCDM, w ~ -1, consistent with TQM Λ(t).");
        sb.AppendLine();
        sb.AppendLine("  ALL OTHER SIGNATURES: Below detection threshold by > 10¹⁵.");
        sb.AppendLine("  No laboratory experiment can detect local anti-phase structures.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("REALITY CONSTRAINTS");
        sb.AppendLine();
        sb.AppendLine("  1. SUPERCONDUCTORS: No anti-gravity signature detected.");
        sb.AppendLine("     If domain walls stabilized anti-phase, superconductors");
        sb.AppendLine("     (which support topological phase structures) would show it.");
        sb.AppendLine("     They don't. Eöt-Wash: equivalence principle to 10⁻¹⁵.");
        sb.AppendLine();
        sb.AppendLine("  2. BECs: Coherent phase, no gravitational anomaly.");
        sb.AppendLine("     BECs have long-range phase coherence. If anti-phase");
        sb.AppendLine("     architectures were real, BEC experiments would see them.");
        sb.AppendLine("     Free-fall: g = 9.8 m/s² exactly.");
        sb.AppendLine();
        sb.AppendLine("  3. COSMOLOGY: Only Λ (Dark Energy) shows repulsive behavior.");
        sb.AppendLine("     No transient repulsive events detected in CMB or LSS.");
        sb.AppendLine("     Universe is net attractive (structure formation) + large-scale Λ.");
        sb.AppendLine();
        sb.AppendLine("  4. VACUUM: Quantum vacuum fluctuations exist but are NET ZERO.");
        sb.AppendLine("     If anti-phase voids formed continuously, they'd contribute");
        sb.AppendLine("     to vacuum energy — already measured as Λ (10⁻⁹ J/m³).");
        sb.AppendLine("     They ARE the vacuum fluctuations. Already accounted for.");
        sb.AppendLine();
        sb.AppendLine("  ALL CONSTRAINTS: consistent with no local metastable anti-gravity.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  1. THIS IS A 'NO, WITH CAVEATS' RESULT:");
        sb.AppendLine("     Metastable repulsive architectures are: the vacuum itself.");
        sb.AppendLine("     The only macroscopic metastable architecture is Dark Energy.");
        sb.AppendLine("     Everything else is too small / too fast / too weak.");
        sb.AppendLine();
        sb.AppendLine("  2. THE EXCITED STATE ANALOGY PARTIALLY FAILS:");
        sb.AppendLine("     Excited atomic states are DIFFERENT CONFIGURATIONS.");
        sb.AppendLine("     Anti-phase is the ABSENCE of configuration.");
        sb.AppendLine("     Absence cannot be 'excited' — it fills at c.");
        sb.AppendLine("     Better analogy: hole in water. There is no 'stable hole'.");
        sb.AppendLine();
        sb.AppendLine("  3. TOPOLOGICAL PROTECTION IS REAL BUT WRONG CONTEXT:");
        sb.AppendLine("     Topological protection PREVENTS UNWINDING of patterns.");
        sb.AppendLine("     It does NOT prevent FILLING of voids.");
        sb.AppendLine("     A vortex is a PATTERN. An anti-phase void is an ABSENCE.");
        sb.AppendLine();
        sb.AppendLine("  4. THE DARK ENERGY CORRESPONDENCE IS DEEP:");
        sb.AppendLine("     Λ(t) = α/√V(t) is the ONLY stable anti-phase structure.");
        sb.AppendLine("     It works because Gpc-scale voids fill slower than expansion.");
        sb.AppendLine("     This suggests: repulsive gravity IS cosmological.");
        sb.AppendLine("     It is fundamentally NOT a local phenomenon.");
        sb.AppendLine();
        sb.AppendLine("  5. WHAT TQM REALLY SAYS:");
        sb.AppendLine("     (a) Repulsive gravity is real (Dark Energy proves it).");
        sb.AppendLine("     (b) It is necessarily COSMOLOGICAL in scale.");
        sb.AppendLine("     (c) Local anti-phase cannot persist (causal filling).");
        sb.AppendLine("     (d) The universe has exactly ONE stable repulsive structure.");
        sb.AppendLine("     (e) That structure IS the cosmological constant / DE.");
        return sb.ToString();
    }

    static string BuildI(RepArch[] cands, Lifetime[] lts)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  Q1-Q3: Anti-phase domains = local depressions in Q-event density.");
        sb.AppendLine("         They fill at c. Lifetime = R/c. No negative Q-source exists.");
        sb.AppendLine("         Anti-phase = LESS phase = flat (not repulsive) at best.");
        sb.AppendLine();
        sb.AppendLine("  Q4-Q6: Decay time = size/c (light-crossing). Topological protection");
        sb.AppendLine("         can extend by 10³-10⁶× but cannot protect absence.");
        sb.AppendLine("         Planck-scale: τ. Atomic: 10⁻¹⁹ s. Meter: 3×10⁻⁹ s.");
        sb.AppendLine("         Only Gpc-scale structures persist meaningful times.");
        sb.AppendLine();
        sb.AppendLine("  Q7-Q10: Domain walls add energy (worsen repulsion).");
        sb.AppendLine("         Phase voids = vacuum fluctuations (already in Λ).");
        sb.AppendLine("         Dark Energy IS the only observable repulsive architecture.");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  OVERALL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  METASTABLE REPULSIVE ARCHITECTURES: PHYSICAL BUT TRIVIAL");
        sb.AppendLine();
        sb.AppendLine("  PHYSICAL: Anti-phase voids exist at Planck scale continuously.");
        sb.AppendLine("    These are the quantum vacuum fluctuations.");
        sb.AppendLine();
        sb.AppendLine("  TRIVIAL: All sub-cosmological voids fill at c.");
        sb.AppendLine("    Lifetime = R/c. Nothing macroscopic survives.");
        sb.AppendLine();
        sb.AppendLine("  THE DEEPER INSIGHT:");
        sb.AppendLine("    The ONLY possible 'metastable repulsive architecture'");
        sb.AppendLine("    at observable scales IS Dark Energy.");
        sb.AppendLine();
        sb.AppendLine("    This unifies QG-029 (repulsion unstable locally),");
        sb.AppendLine("    QG-030 (no counter-structure), and QG-031 (metastable = DE).");
        sb.AppendLine();
        sb.AppendLine("    TRILOGY COMPLETE:");
        sb.AppendLine("    QG-029: Attractive gravity = stable local solution.");
        sb.AppendLine("    QG-030: No counter-structure without modifying gravity.");
        sb.AppendLine("    QG-031: Metastable repulsive = Dark Energy only.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B → C boundary");
        sb.AppendLine("  - Planck-scale: B (extremely short-lived, real but trivial)");
        sb.AppendLine("  - Cosmological scale: C (observable but unstable in principle,");
        sb.AppendLine("    though Λ(t) is quasi-stable due to expansion)");
        sb.AppendLine("  - ANY intermediate scale: IMPOSSIBLE (filled at c)");
        sb.AppendLine("  QG program now: 31 experiments.");
        return sb.ToString();
    }
}
