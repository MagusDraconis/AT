using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class FrequencyHierarchyAnalyzer
{
    const double tau = 5.391247e-44, hbar = 1.054571817e-34, c = 299792458;
    const double mp = 1.67262192369e-27, me = 9.1093837015e-31;
    const double Ey = 2.1798741e-18; // Rydberg energy
    const double w0 = 2*Math.PI/tau; // fundamental angular frequency

    public static FResult RunFullAnalysis()
    {
        double wp = w0; // Planck frequency = fundamental
        double w_proton = mp*c*c/hbar;
        double w_electron = me*c*c/hbar;
        double w_Ry = Ey/hbar;
        double w_H21cm = 2*Math.PI*1.42040575177e9; // 21cm hydrogen line
        double w_vib = 2*Math.PI*1e13; // typical molecular vibration
        double w_visible = 2*Math.PI*5e14; // green light
        double w_human = 2*Math.PI*1; // ~1 Hz heartbeat

        var fl = new[]{new FreqLevel("Q-event grain (tau)",w0,1,w0*hbar,"Temporal succession at interval tau.","FUNDAMENTAL CLOCK of reality."),
            new FreqLevel("Proton mass-energy",w_proton,w0/w_proton,mp*c*c,"Defect energy (M^2 and QCD).","PARTICLE — topological defect."),
            new FreqLevel("Electron mass-energy",w_electron,w0/w_electron,me*c*c,"Defect energy (M^2 and QED).","PARTICLE — lighter defect."),
            new FreqLevel("Rydberg (hydrogen)",w_Ry,w0/w_Ry,Ey,"Coulomb binding (alpha).","ATOMIC — first bound state."),
            new FreqLevel("Hydrogen 21cm line",w_H21cm,w0/w_H21cm,hbar*w_H21cm,"Hyperfine splitting.","ATOMIC — spin-flip transition."),
            new FreqLevel("Molecular vibration",w_vib,w0/w_vib,hbar*w_vib,"Chemical bond oscillation.","MOLECULAR — IR spectroscopy."),
            new FreqLevel("Visible light",w_visible,w0/w_visible,hbar*w_visible,"Atomic electron transitions.","PHOTONIC — optical regime."),
            new FreqLevel("Human heartbeat",w_human,w0/w_human,hbar*w_human,"Biological oscillation (metabolism).","LIFE — far from equilibrium."),
        };

        var fc = new[]{new FreqCascade(1,"tau -> Proton","10^-44s -> 10^-24Hz","10^20","M^2 + QCD confinement.","NUCLEAR — defect energy scale."),
            new FreqCascade(2,"Proton -> Electron","10^24Hz -> 10^20Hz","10^4","Yukawa coupling (mass hierarchy).","PARTICLE — mass ratio ~1836."),
            new FreqCascade(3,"Electron -> Atom","10^20Hz -> 10^16Hz","10^4","Coulomb binding (alpha ~ 1/137).","ATOMIC — binding energy."),
            new FreqCascade(4,"Atom -> Molecule","10^16Hz -> 10^13Hz","10^3","Exchange interaction (electron sharing).","MOLECULAR — chemical bond."),
            new FreqCascade(5,"Molecule -> Life","10^13Hz -> 10^0Hz","10^13","Metabolic cycles (far-from-equilibrium).","BIOLOGICAL — emergent rhythm."),
        };

        string A=BuildA(fl),B=BuildB(fc),C=BuildC(),D=BuildD(),E=BuildE(),F=BuildF(),G=BuildG(),H=BuildH(),I=BuildI(fl);
        return new FResult(A,B,C,D,E,F,G,H,I,fl,fc);
    }

    static string BuildA(FreqLevel[] f){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FUNDAMENTAL FREQUENCY");sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  omega_0 = 2*pi/tau = {0:E2} Hz",w0));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  Energy: hbar*omega_0 = {0:E2} J = {1:E2} GeV",w0*hbar,w0*hbar/1.602e-10));
        sb.AppendLine("  This is the CLOCK SPEED of reality — 10^44 ticks per second.");
        sb.AppendLine();
        sb.AppendLine("  Structure                 Frequency [Hz]      Ratio to w0      Energy");
        sb.AppendLine("  ------------------------  ------------------  ---------------  ------------------");
        foreach(var x in f) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-25} {1,-19:E2} {2,-16:E1} {3,-17:E1} J",x.Structure,x.OmegaHz,x.RatioToFund,x.Energy));
        sb.AppendLine();sb.AppendLine("  SPAN: 44 orders of magnitude from Planck to human heartbeat.");
        return sb.ToString();
    }

    static string BuildB(FreqCascade[] f){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FREQUENCY CASCADE");sb.AppendLine();
        sb.AppendLine("  Step  Transition              Ratio       Mechanism");
        sb.AppendLine("  ----  ----------------------  ----------  ---------");
        foreach(var x in f) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  [{0}]   {1,-23} {2,-11} {3}",x.Step,x.From+" -> "+x.To,x.Ratio,x.Mechanism));
        sb.AppendLine();sb.AppendLine("  EACH STEP IS AN EMERGENT ATTRACTOR.");
        sb.AppendLine("  The cascade is governed by coupling constants (alpha, M^2, Yukawa).");
        return sb.ToString();
    }

    static string BuildC()=>"PARTICLES AS FREQUENCIES\n\n  E = hbar*omega. m = E/c^2. Therefore: mass = frequency (times hbar/c^2).\n\n  PROTON:\n    omega_p = m_p*c^2/hbar = 1.42e24 Hz.\n    Ratio to w0: 10^20. Gap from M^2 (nonlinearity scale).\n\n  ELECTRON:\n    omega_e = m_e*c^2/hbar = 7.76e20 Hz.\n    Ratio to proton: 1/1836 (mass hierarchy).\n\n  EVERY PARTICLE HAS A CHARACTERISTIC FREQUENCY.\n  omega_compton = m*c^2/hbar.\n  The Compton frequency IS the particle's identity.\n\n  MASS = FREQUENCY.\n  This is not a metaphor — it's E = hbar*omega.";

    static string BuildD()=>"ATOMS AS RESONANCES\n\n  HYDROGEN GROUND STATE:\n    omega_Ry = E_Ry/hbar = 2.07e16 Hz.\n    This is the frequency of the electron's orbital motion.\n\n  RYDBERG FORMULA:\n    1/lambda = R_inf * (1/n1^2 - 1/n2^2).\n    R_inf = alpha^2 * m_e*c/(2h) = 1.097e7 m^-1.\n\n  IN TQM:\n    The Rydberg constant emerges from:\n    - Electron Compton frequency (omega_e)\n    - Fine-structure constant (alpha ~ 1/137)\n    - Coulomb binding (U(1) gauge from defect moduli)\n\n  ATOMS ARE RESONANT CAVITIES:\n    The Coulomb potential creates a resonant cavity.\n    Only specific frequencies survive (quantization).\n    This IS the origin of atomic spectra.";

    static string BuildE()=>"MULTI-SCALE HIERARCHY\n\n  FREQUENCY DOMAINS:\n\n  Planck:      10^44 Hz    Q-event grain (fundamental clock).\n  GUT:         10^39 Hz    Unification (speculative).\n  Nuclear:     10^24 Hz    Proton mass (QCD confinement).\n  Particle:    10^20 Hz    Electron mass (Yukawa + QED).\n  Atomic:      10^16 Hz    Rydberg (Coulomb binding).\n  Hyperfine:   10^9  Hz    Spin-flip (21 cm line).\n  Molecular:   10^13 Hz    Vibrations (chemical bonds).\n  Optical:     10^15 Hz    Electron transitions.\n  Microwave:   10^10 Hz    Rotations.\n  Radio:       10^8  Hz    NMR, ESR.\n  Audio:       10^3  Hz    Sound.\n  Biological:  10^0  Hz    Heartbeat, neural oscillations.\n\n  ALL TRACE BACK TO tau AND EMERGENT COUPLINGS.\n  A SINGLE HIERARCHY FROM A SINGLE CLOCK.";

    static string BuildF()=>"UNIFIED SPECTRUM\n\n  THE FREQUENCY TREE OF REALITY:\n\n  omega_0 = 2*pi/tau (fundamental clock)\n      |\n      +-- M^2 + QCD -> omega_proton (10^24 Hz)\n      |       |\n      |       +-- Yukawa -> omega_electron (10^20 Hz)\n      |               |\n      |               +-- alpha -> omega_Ry (10^16 Hz)\n      |                       |\n      |                       +-- hyperfine -> omega_21cm (10^9 Hz)\n      |                       |\n      |                       +-- exchange -> omega_vib (10^13 Hz)\n      |                       |\n      |                       +-- optics -> omega_visible (10^15 Hz)\n      |\n      +-- N(t), cosmology -> H_0 (10^-18 Hz)\n\n  ALL FREQUENCIES EMERGE FROM tau + COUPLING CONSTANTS.\n  THE FUNDAMENTAL CLOCK + THE STRUCTURE OF THE INSTRUMENT\n  = THE MUSIC OF REALITY.";

    static string BuildG()=>"HOSTILE REVIEW\n\n1. 'MASS = FREQUENCY' IS STANDARD QM:\n   E = hbar*omega is not TQM-specific. De Broglie (1924)\n   established this. TQM adds nothing new here.\n\n2. COUPLING CONSTANTS ARE IMPORTED:\n   Alpha ~ 1/137 comes from QED. Yukawa couplings from SM.\n   M^2 is unknown. TQM does not predict any of these.\n\n3. THE HIERARCHY IS DESCRIPTIVE, NOT EXPLANATORY:\n   We observe these frequencies. TQM maps them back to tau.\n   But 'maps back' is not 'derives.' The chain omega_0 ->\n   omega_proton depends on unknown M^2.\n\n4. THE VALUE IS IN THE UNIFICATION:\n   TQM shows that ALL frequencies are oscillations of the\n   same Q-event substrate. Same clock, different emergent\n   scales. This IS genuine unification of ontology.\n\n5. BUT THE NUMBERS REMAIN EMPIRICAL:\n   tau, M^2, alpha, Yukawa couplings — all measured, not predicted.\n   TQM unifies the ONTOLOGY of frequency, not its NUMEROLOGY.";

    static string BuildH()=>"REMAINING GAPS\n\n  1. M^2 — determines particle mass scale. Unknown.\n  2. alpha — fine-structure constant. Empirical (1/137).\n  3. Yukawa couplings — fermion mass hierarchy. Empirical.\n  4. tau — fundamental clock. Empirical (via G, hbar, c).\n\n  THE FREQUENCY HIERARCHY IS COHERENT BUT NOT PREDICTIVE:\n    TQM explains WHY everything oscillates (QG-026).\n    It does not explain WHY each frequency has its specific value.\n    The values come from coupling constants — which TQM does not derive.\n\n  THE UNANSWERED QUESTION:\n    Why does M^2 produce proton mass specifically?\n    Why alpha ~ 1/137?\n    These are the NEXT FRONTIER of the TQM program.";

    static string BuildI(FreqLevel[] f){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  Fundamental clock: omega_0 = {0:E2} Hz.",w0));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  Frequency span: {0} orders of magnitude.",Math.Log10(w0/f[^1].OmegaHz)));
        sb.AppendLine("  Hierarchy: tau -> particle -> atomic -> molecular -> life.");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  OVERALL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  ALL PHYSICS IS A FREQUENCY HIERARCHY ROOTED IN tau.");
        sb.AppendLine();
        sb.AppendLine("  FUNDAMENTAL: omega_0 = 2*pi/tau.");
        sb.AppendLine("  EMERGENT: all lower frequencies from couplings + attractors.");
        sb.AppendLine();
        sb.AppendLine("  REALITY = ONE CLOCK + ONE INSTRUMENT (Q-event network).");
        sb.AppendLine("  THE CLOCK: tau (fundamental oscillation).");
        sb.AppendLine("  THE INSTRUMENT: Q-event causal structure + M^2 + gauge couplings.");
        sb.AppendLine("  THE MUSIC: the frequency hierarchy of all matter.");
        sb.AppendLine();
        sb.AppendLine("  MASS = FREQUENCY (E = hbar*omega).");
        sb.AppendLine("  PARTICLES = FREQUENCY ATTRACTORS.");
        sb.AppendLine("  ATOMS = RESONANT FREQUENCY CAVITIES.");
        sb.AppendLine("  MOLECULES = COUPLED FREQUENCY NETWORKS.");
        sb.AppendLine("  LIFE = FAR-FROM-EQUILIBRIUM FREQUENCY CYCLES.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: D — UNIFIED FREQUENCY PICTURE");
        sb.AppendLine("  QG program (QG-001->027, 27 experiments).");
        return sb.ToString();
    }
}
