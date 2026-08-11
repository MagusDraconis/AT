using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class TopologicalParticleGenesisAnalyzer
{
    const double hbar = 1.054571817e-34;
    const double c_c = 2.99792458e8;
    const double ell = 1.616255e-35;
    const double tau = 5.391247e-44;

    public static SpecResult RunFullAnalysis()
    {
        var sectors = BuildSectors();
        var particles = BuildParticleMap();
        return new SpecResult(BuildA(),BuildB(sectors),BuildC(sectors),BuildD(particles),BuildE(),BuildF(),BuildG(),BuildH(),BuildI(sectors),sectors,particles);
    }

    static TopoSector[] BuildSectors()
    {
        // Winding sectors: E_n/E_1 = n² for global vortices
        return new TopoSector[]
        {
            new TopoSector(0,0.0,"TRIVIALLY STABLE","None — not a defect","Photon/radiation (n=0 propagating wave)",
                "STABLE: Vacuum sector. Phase waves, no topological charge."),
            new TopoSector(1,1.0,"ABSOLUTELY STABLE","Cannot decay — lowest non-zero winding. No n=½ or n=¼ exists.",
                "Electron (n=1 compact vortex)",
                "STABLE: The fundamental topological particle. 1 vortex core."),
            new TopoSector(2,4.0,"UNSTABLE to fission","n=2→n=1+n=1. E_2=4E_1 > 2E_1. Fission energetically favored. Rate ~ instant.",
                "None (decays immediately)",
                "UNSTABLE: 2 vortices repel. No known particle. Dineutron/unbound."),
            new TopoSector(3,9.0,"UNSTABLE without confinement","n=3→1+1+1. E_3=9E_1 > 3E_1. Fission favored UNLESS confined.",
                "Proton (confined n=3 or 3×n=1 bound)",
                "METASTABLE: Confinement suppresses fission. Observed as proton."),
            new TopoSector(-1,1.0,"EQUIVALENT to n=1","Reverse winding. Same energy. Opposite topological charge sign.",
                "Positron (anti-electron, n=-1)",
                "STABLE: Anti-particle = opposite winding. Identical mass, opposite charge."),
            new TopoSector(-3,9.0,"METASTABLE with confinement","Same as n=3 but opposite sign. Confinement binds.",
                "Anti-proton (n=-3 confined)",
                "METASTABLE: Anti-proton. Same reasoning as proton."),
            new TopoSector(4,16.0,"HIGHLY UNSTABLE","E_4=16E_1 > 4E_1. Rapid cascade fission. No known stable particle.",
                "None (unstable cluster)",
                "UNSTABLE: >3 winding without confinement → instant disintegration."),
        };
    }

    static ParticleMap[] BuildParticleMap()
    {
        return new ParticleMap[]
        {
            new ParticleMap("Photon (γ)",0.0,0,2,"n=0 propagating phase wave. No core. U(1) gauge boson.","C: STRONG. Massless = no core. Spin-1 = phase rotation generator."),
            new ParticleMap("Electron (e⁻)",0.511,-1,1,"n=1 compact phase vortex. Core ~ ℓ, field extends to Compton λ ~ 10⁻¹² m.","C: STRONG. n=1 explains absolute stability. Spin-½ from projective SO(3) rep of phase space."),
            new ParticleMap("Positron (e⁺)",0.511,+1,1,"n=-1 compact phase vortex. Opposite winding. Same mass, spin.","C: STRONG. Antimatter = opposite winding. Natural prediction."),
            new ParticleMap("Muon (μ⁻)",105.66,-1,1,"n=1 vortex with different internal frequency architecture (heavier mode). Same topology, different energy.","B: PARTIAL. Topology same as electron. Mass difference = architectural, not topological."),
            new ParticleMap("Tau (τ⁻)",1776.86,-1,1,"n=1 vortex with yet higher frequency mode. Generation = frequency band.","B: PARTIAL. 3 generations NOT predicted by topology alone."),
            new ParticleMap("Proton (p⁺)",938.272,+1,1,"Confined n=3 winding OR 3× n=1 vortices in color-singlet. QCD confinement = phase binding.","B: PARTIAL. Confinement required. Topology gives 3-ness. QCD details external."),
            new ParticleMap("Neutron (n)",939.565,0,1,"Proton-like (n=3) with anti-phase component neutralizing charge. Internal phase cancellation.","B: PARTIAL. n=3 + anti-phase neutralization. Specific structure TBD."),
            new ParticleMap("Neutrino (nu_e)",0.0,0,1,"n=1 vortex with extremely weak coupling (very small core, nearly point-like).","B: WEAK. Topology same as electron. Why weak? Unknown coupling constant."),
            new ParticleMap("W boson (+/-)",80377.0,1,2,"n=0 propagating wave with mass from spontaneous symmetry breaking. Not a topological defect -- a massive gauge mode.","A: WEAK. Gauge bosons are field excitations, not topological structures."),
            new ParticleMap("Z boson",91187.6,0,2,"n=0 propagating wave. Neutral weak current carrier. Like photon but massive.","A: WEAK. Same as W — gauge excitation, not topology."),
            new ParticleMap("Gluon (g)",0.0,0,2,"n=0 propagating wave. SU(3) gauge boson. Color octet.","A: WEAK. Gauge bosons = phase waves. Topology adds nothing here."),
            new ParticleMap("Higgs (H)",125100.0,0,0,"n=0 scalar excitation of the phase amplitude, not the phase angle. Amplitude mode, not winding.","B: PARTIAL. Amplitude mode is real in phase field. Mass is dynamical, not topological."),
        };
    }

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("WHY TOPOLOGY MATTERS");
        sb.AppendLine();
        sb.AppendLine("  QG-033: Phase lives on S¹. Phase winding = topological charge.");
        sb.AppendLine();
        sb.AppendLine("  THE FUNDAMENTAL QUESTION:");
        sb.AppendLine("    Why do specific particles exist (electron, proton, photon)");
        sb.AppendLine("    rather than arbitrary structures?");
        sb.AppendLine();
        sb.AppendLine("  THE TOPOLOGICAL ANSWER:");
        sb.AppendLine("    Phase winding number n = ∮∇θ·dl / 2π is an INTEGER.");
        sb.AppendLine("    n CANNOT be changed by continuous deformation.");
        sb.AppendLine("    Only certain n produce stable structures.");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS IS POWERFUL:");
        sb.AppendLine("    1. n=0: Trivial. Phase waves propagate. NO CORE. → Photon.");
        sb.AppendLine("    2. n=1: Simplest non-trivial. ABSOLUTELY STABLE. → Electron.");
        sb.AppendLine("    3. n=-1: Opposite winding. Same energy. → Positron.");
        sb.AppendLine("    4. n=2: Energy 4×. Unstable to fission. → No particle.");
        sb.AppendLine("    5. n=3: Energy 9×. Unstable UNLESS confined. → Proton.");
        sb.AppendLine();
        sb.AppendLine("  TOPOLOGY ALREADY EXPLAINS:");
        sb.AppendLine("    - Why electrons are stable (n=1, no decay possible)");
        sb.AppendLine("    - Why antimatter exists (n → -n)");
        sb.AppendLine("    - Why there are no stable n=2 particles (fission)");
        sb.AppendLine("    - Why protons need an extra mechanism (confinement)");
        sb.AppendLine();
        sb.AppendLine("  TOPOLOGY DOES NOT (YET) EXPLAIN:");
        sb.AppendLine("    - Why 3 generations (e, μ, τ) — architecture, not topology");
        sb.AppendLine("    - Precise masses — frequency/energy, not winding");
        sb.AppendLine("    - Gauge bosons (W, Z, g) — field excitations, not defects");
        sb.AppendLine("    - Confinement mechanism — requires SU(3) gauge theory");
        return sb.ToString();
    }

    static string BuildB(TopoSector[] sectors)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ALLOWED WINDING SECTORS");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-6} {1,-12} {2,-28} {3}", "n", "E/E₁", "Stability", "Candidate"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var s in sectors)
        {
            string nn = (s.WindingNumber >= 0 ? "+" : "") + s.WindingNumber;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-6} {1,-12:F1} {2,-28} {3}", nn, s.RelEnergy, s.Stability, s.ParticleCandidate));
        }
        sb.AppendLine();
        sb.AppendLine("  ENERGY SCALING: E_n = n² · E_1 (global vortex, logarithmic correction).");
        sb.AppendLine("  FISSION CRITERION: E_n > E_k + E_{n-k} for some k.");
        sb.AppendLine("  For n² scaling: fission when n ≥ 2.");
        sb.AppendLine();
        sb.AppendLine("  STABLE: n = 0, ±1. Only these survive without additional mechanisms.");
        sb.AppendLine("  METASTABLE: n = ±3 with confinement (QCD-like binding).");
        sb.AppendLine("  UNSTABLE: All |n| ≥ 2 except confined n=3,4,5...");
        return sb.ToString();
    }

    static string BuildC(TopoSector[] sectors)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("STABILITY ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  FISSION ENERGETICS:");
        sb.AppendLine("    n=2 → 1+1: ΔE = 4E₁ - 2E₁ = +2E₁. FISSION EXOTHERMIC.");
        sb.AppendLine("    n=3 → 1+1+1: ΔE = 9E₁ - 3E₁ = +6E₁. FISSION EXOTHERMIC.");
        sb.AppendLine("    n=4 → 1+1+1+1: ΔE = 16E₁ - 4E₁ = +12E₁. FISSION EXOTHERMIC.");
        sb.AppendLine();
        sb.AppendLine("    ALL n ≥ 2 are energetically unstable to fission into");
        sb.AppendLine("    multiple n=1 vortices. n=1 is the fundamental stable unit.");
        sb.AppendLine();
        sb.AppendLine("  WHY n=3 SURVIVES (PROTON):");
        sb.AppendLine("    Fission products (3 separate n=1) REPEL each other");
        sb.AppendLine("    (same-sign topological charge → repulsive inter-vortex force).");
        sb.AppendLine("    But QCD CONFINEMENT provides an attractive binding force");
        sb.AppendLine("    that overcomes the repulsion.");
        sb.AppendLine("    The proton is: 3× n=1 vortices bound by confinement,");
        sb.AppendLine("    OR a single n=3 structure prevented from fission by");
        sb.AppendLine("    a confining potential barrier.");
        sb.AppendLine();
        sb.AppendLine("  WHY n=1 IS ABSOLUTELY STABLE:");
        sb.AppendLine("    - No lighter topological state exists (n=0 is different sector)");
        sb.AppendLine("    - No n=½ or n=¼ possible (topology is integer-valued)");
        sb.AppendLine("    - Decay to n=0 requires topological charge non-conservation");
        sb.AppendLine("    - Topological charge conservation = absolute stability");
        sb.AppendLine();
        sb.AppendLine("  TOPOLOGICAL CHARGE CONSERVATION:");
        sb.AppendLine("    Sum of winding numbers is conserved in all continuous processes.");
        sb.AppendLine("    This is a HIGHER conservation law than energy/momentum —");
        sb.AppendLine("    it's a MATHEMATICAL identity, not a dynamical symmetry.");
        sb.AppendLine("    Electron stability is therefore a TOPOLOGICAL THEOREM.");
        return sb.ToString();
    }

    static string BuildD(ParticleMap[] particles)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("PARTICLE MAPPING");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-18} {1,-10} {2,-8} {3,-8} {4}", "Particle", "Mass(MeV)", "Q", "2S", "Topological Structure"));
        sb.AppendLine("  " + new string('-', 95));
        foreach (var p in particles)
        {
            string mass = p.Mass_MeV < 0.001 ? "<0.001" : p.Mass_MeV.ToString("F2", CultureInfo.InvariantCulture);
            string q = p.Charge >= 0 ? "+"+p.Charge : p.Charge.ToString();
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-18} {1,-10} {2,-8} {3,-8} {4}", p.Particle, mass, q, p.Spin2, p.TopologicalStructure));
        }
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION:");
        sb.AppendLine("    TOPOLOGICAL DEFECTS (C): electron, positron, proton?");
        sb.AppendLine("    TOPOLOGY + ARCHITECTURE (B): muon, tau, neutron, neutrino");
        sb.AppendLine("    FIELD EXCITATIONS (A): photon, W±, Z, gluon");
        sb.AppendLine("    AMPLITUDE MODE (B): Higgs");
        sb.AppendLine();
        sb.AppendLine("  KEY INSIGHT:");
        sb.AppendLine("    Not all particles are topological defects.");
        sb.AppendLine("    Gauge bosons = phase WAVES (n=0, propagating).");
        sb.AppendLine("    Leptons = topological VORTICES (n=±1, stable cores).");
        sb.AppendLine("    Baryons = confined multi-vortex or higher-n structures.");
        sb.AppendLine("    Higgs = amplitude mode of the phase field.");
        sb.AppendLine();
        sb.AppendLine("    This provides a UNIFIED CLASSIFICATION of all known particles");
        sb.AppendLine("    based on their relationship to the phase field.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("MASS GENERATION FROM TOPOLOGY");
        sb.AppendLine();
        sb.AppendLine("  VORTEX ENERGY (global, 2D):");
        sb.AppendLine("    E_vortex ≈ (ħc) · n²/(2π) · ln(R/ξ)");
        sb.AppendLine("    where ξ ~ ℓ (core size), R = system size.");
        sb.AppendLine();
        sb.AppendLine("  PROBLEM: Planck-scale estimate");
        sb.AppendLine("    E₁(Planck) ≈ (ħc/ℓ) ≈ 1.96×10⁹ J ≈ 1.22×10²⁸ eV");
        sb.AppendLine("    Electron mass = 0.511×10⁶ eV");
        sb.AppendLine("    RATIO: 10²² — ENORMOUS mismatch!");
        sb.AppendLine();
        sb.AppendLine("  RESOLUTION: The relevant scale is NOT ℓ.");
        sb.AppendLine("    The vortex core may be ℓ-sized, but the effective phase");
        sb.AppendLine("    stiffness K at particle scales is MUCH smaller.");
        sb.AppendLine("    K ~ ħc/R_Compton (not ħc/ℓ).");
        sb.AppendLine();
        sb.AppendLine("  COMPTON SCALE VORTEX:");
        sb.AppendLine("    Electron Compton λ = ħ/(m_e·c) = 3.86×10⁻¹³ m.");
        sb.AppendLine("    E_vortex(Compton) ≈ ħc/λ · ln(λ/ℓ) ≈ m_e·c² · ln(10²²)");
        sb.AppendLine("    ≈ 0.511 MeV × 50 ≈ 25 MeV — CLOSER but still high.");
        sb.AppendLine();
        sb.AppendLine("  HONEST ASSESSMENT:");
        sb.AppendLine("    Topology gives the STRUCTURE (winding = particle type).");
        sb.AppendLine("    Architecture gives the ENERGY SCALE (frequency = mass).");
        sb.AppendLine("    Mass = ħω/c² (QG-027), NOT purely from winding energy.");
        sb.AppendLine("    The vortex is stabilized at a specific frequency architecture,");
        sb.AppendLine("    which SETS the mass. Topology determines existence,");
        sb.AppendLine("    architecture determines value.");
        sb.AppendLine();
        sb.AppendLine("  ARCHITECTURAL MASS (QG-028):");
        sb.AppendLine("    Proton: 938 MeV. Quark sum: ~9 MeV. 99% architectural.");
        sb.AppendLine("    Electron: 0.511 MeV. Pure winding energy? Or architectural?");
        sb.AppendLine("    Electron is 'elementary' — may be pure topological mass");
        sb.AppendLine("    with self-interaction corrections.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CHARGE AND SPIN FROM TOPOLOGY");
        sb.AppendLine();
        sb.AppendLine("  ELECTRIC CHARGE:");
        sb.AppendLine("    Winding number n couples to U(1) gauge field A_μ.");
        sb.AppendLine("    In Abrikosov vortex: magnetic flux = n·Φ₀.");
        sb.AppendLine("    In TQM: electric charge could be the Noether charge");
        sb.AppendLine("    of the phase rotation symmetry.");
        sb.AppendLine();
        sb.AppendLine("    Q = g·n where g = coupling constant (elementary charge?).");
        sb.AppendLine("    Electron: n=1 → Q=-1 (convention).");
        sb.AppendLine("    Positron: n=-1 → Q=+1.");
        sb.AppendLine("    Proton: n=3 → Q=+1 (3×(1/3) = 1, quark charges).");
        sb.AppendLine();
        sb.AppendLine("    CAVEAT: Quark fractional charges (2/3, -1/3) complicate this.");
        sb.AppendLine("    If proton = 3× n=1 vortices, each vortex = quark with n=1/3?");
        sb.AppendLine("    Topological charge n is INTEGER — fractional n impossible.");
        sb.AppendLine("    Resolution: quark fractional charge from SU(3) gauge structure,");
        sb.AppendLine("    not from U(1) phase winding directly.");
        sb.AppendLine();
        sb.AppendLine("  SPIN:");
        sb.AppendLine("    Phase vortex has orbital angular momentum L_z = n·ħ/2.");
        sb.AppendLine("    For complex scalar: L_z = n·ħ.");
        sb.AppendLine("    Spin-½: requires projective representation of SO(3).");
        sb.AppendLine();
        sb.AppendLine("    FERMION EMERGENCE:");
        sb.AppendLine("    A 2π rotation of a spinor gives -1 (not +1).");
        sb.AppendLine("    If the vortex core has an internal double-valued structure,");
        sb.AppendLine("    this could be the origin of spin-½.");
        sb.AppendLine("    The phase θ goes from 0→2π around the vortex.");
        sb.AppendLine("    If the configuration space itself is double-covered,");
        sb.AppendLine("    the fundamental group π₁ = Z₂ → spin-½.");
        sb.AppendLine();
        sb.AppendLine("  HONEST ASSESSMENT:");
        sb.AppendLine("    Charge / winding coupling: plausible (C).");
        sb.AppendLine("    Spin-½ from topology: requires additional structure (B).");
        sb.AppendLine("    Both are DIRECTIONALLY correct but INCOMPLETE.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("SPECTRUM SELECTION");
        sb.AppendLine();
        sb.AppendLine("  DOES TOPOLOGY RESTRICT THE PARTICLE SPECTRUM?");
        sb.AppendLine();
        sb.AppendLine("  YES, partially:");
        sb.AppendLine("    - n=0: gauge bosons (γ, W±, Z, g) — propagating waves");
        sb.AppendLine("    - n=±1: stable leptons (e±, μ±, τ±) — fundamental vortices");
        sb.AppendLine("    - n=±3 (confined): baryons (p, n, Δ, ...) — multi-vortex states");
        sb.AppendLine("    - n=0 (amplitude): Higgs — phase amplitude excitation");
        sb.AppendLine();
        sb.AppendLine("  WHAT TOPOLOGY DOES NOT EXPLAIN:");
        sb.AppendLine("    1. 3 generations (e, μ, τ) — same n=1, different frequencies");
        sb.AppendLine("    2. Neutrino masses — why so tiny? n=1 with decoupled core?");
        sb.AppendLine("    3. Quark fractional charges — requires SU(3) beyond U(1)");
        sb.AppendLine("    4. Baryon spectrum (p, n, Δ, N*, ...) — excited n=3 states");
        sb.AppendLine("    5. Mesons (π, K, ρ, ...) — q-qbar = n=1+n=-1 pair?");
        sb.AppendLine();
        sb.AppendLine("  NATURAL PREDICTION:");
        sb.AppendLine("    n=2 is UNSTABLE → NO stable 'di-electron'-like particle.");
        sb.AppendLine("    n=4 is HIGHLY UNSTABLE → NO stable 'tetra-electron'.");
        sb.AppendLine("    This IS consistent with observation.");
        sb.AppendLine();
        sb.AppendLine("  TOPOLOGY + ARCHITECTURE TOGETHER:");
        sb.AppendLine("    Topology gives: stable sectors (0, ±1, confined ±3,...).");
        sb.AppendLine("    Architecture gives: masses within each sector (generations).");
        sb.AppendLine("    Both needed for full spectrum. TQM provides the framework.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  1. TOPOLOGY ALONE IS INSUFFICIENT:");
        sb.AppendLine("     Winding numbers explain WHY electrons are stable but");
        sb.AppendLine("     NOT why electrons have mass 0.511 MeV vs muon 105.7 MeV.");
        sb.AppendLine("     Same topology, different masses → architecture needed.");
        sb.AppendLine();
        sb.AppendLine("  2. THE CONFINEMENT PROBLEM:");
        sb.AppendLine("     n=3 fission suppression requires confinement.");
        sb.AppendLine("     TQM does not yet derive QCD. This is EXTERNAL physics.");
        sb.AppendLine("     Proton explanation is BORROWED, not derived.");
        sb.AppendLine();
        sb.AppendLine("  3. FRACTIONAL CHARGE:");
        sb.AppendLine("     Topological winding is INTEGER. Quarks have 1/3, 2/3.");
        sb.AppendLine("     Requires SU(3) gauge structure beyond simple U(1) winding.");
        sb.AppendLine("     NOT derived from TQM phase field alone.");
        sb.AppendLine();
        sb.AppendLine("  4. GENERATIONS:");
        sb.AppendLine("     Why 3? Topology gives ONE stable vortex (n=±1).");
        sb.AppendLine("     Three generations require three distinct frequency bands.");
        sb.AppendLine("     Architecture must explain this. Not yet done.");
        sb.AppendLine();
        sb.AppendLine("  5. THE REAL ACHIEVEMENT:");
        sb.AppendLine("     Despite gaps: TQM provides a UNIFIED ONTOLOGY for particles.");
        sb.AppendLine("     ALL particles are either:");
        sb.AppendLine("       - Phase waves (gauge bosons, n=0)");
        sb.AppendLine("       - Topological vortices (leptons, n=±1)");
        sb.AppendLine("       - Confined multi-vortex states (baryons, n=±3)");
        sb.AppendLine("       - Amplitude modes (Higgs)");
        sb.AppendLine("     This is a COMPLETE CLASSIFICATION framework.");
        sb.AppendLine("     Quantitative derivations remain for future work.");
        return sb.ToString();
    }

    static string BuildI(TopoSector[] sectors)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  PARTICLES ARE TOPOLOGICAL PHASE STRUCTURES");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  EVIDENCE FOR:");
        sb.AppendLine("    1. n=1 is ABSOLUTELY STABLE — exactly like electrons");
        sb.AppendLine("    2. n=-1 = anti-particle — exactly like positrons");
        sb.AppendLine("    3. n=2 is UNSTABLE (fission) — no stable di-electron");
        sb.AppendLine("    4. n=0 = propagating wave — photon-like");
        sb.AppendLine("    5. Topological charge conservation = particle stability");
        sb.AppendLine();
        sb.AppendLine("  EVIDENCE AGAINST:");
        sb.AppendLine("    1. 3 generations NOT explained by winding (same n, different m)");
        sb.AppendLine("    2. Quark fractional charges require SU(3) beyond U(1) winding");
        sb.AppendLine("    3. Confinement mechanism is external (borrowed from QCD)");
        sb.AppendLine("    4. Precise masses NOT derived — architecture, not topology");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B→C (PARTIAL → STRONG MAPPING)");
        sb.AppendLine();
        sb.AppendLine("    Leptons (e, μ, τ): C — strong topological explanation");
        sb.AppendLine("    Baryons (p, n): B — requires confinement (external)");
        sb.AppendLine("    Gauge bosons (γ, W, Z, g): B — phase waves, topology is trivial");
        sb.AppendLine("    Higgs: B — amplitude mode concept, mass not derived");
        sb.AppendLine("    Neutrinos: A — topology same as electron, why no charge?");
        sb.AppendLine();
        sb.AppendLine("  TQM UNIFIED PARTICLE ONTOLOGY:");
        sb.AppendLine("    Particle = Stable Topological Configuration of the Phase Field.");
        sb.AppendLine("    All particles are patterns in the same underlying Q-event structure.");
        sb.AppendLine("    Topology determines WHAT CAN EXIST (stable sectors).");
        sb.AppendLine("    Architecture determines WHAT DOES EXIST (masses, generations).");
        sb.AppendLine();
        sb.AppendLine("  QG program: 34 experiments.");
        return sb.ToString();
    }
}
