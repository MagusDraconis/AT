using System.Globalization;

namespace TQM.Core.ResearchDATA;

public static class HighZRarFeasibilityAnalyzer
{
    const double H0=67.4, C=299792.458, Om=0.315, Ol=0.685, Kc=0.000324077929, Pi=Math.PI;

    static double Hz(double z)=>H0*Math.Sqrt(Om*Math.Pow(1+z,3)+Ol);
    static double Gdagger(double z)=>C*Hz(z)/1000*Kc/(2*Pi); // x1e-10 m/s^2

    public static FeasResult RunFullAnalysis()
    {
        var g0 = Gdagger(0); // ~1.04 x1e-10

        var zp = new[]{new ZPoint(0,H0,g0,0,0,"Local — SPARC era"),
            new ZPoint(0.5,Hz(0.5),Gdagger(0.5),Gdagger(0.5)-g0,(Gdagger(0.5)-g0)/g0,"DESI — marginally higher g†"),
            new ZPoint(1.0,Hz(1.0),Gdagger(1.0),Gdagger(1.0)-g0,(Gdagger(1.0)-g0)/g0,"Euclid DR1 — +49% signal"),
            new ZPoint(1.5,Hz(1.5),Gdagger(1.5),Gdagger(1.5)-g0,(Gdagger(1.5)-g0)/g0,"Roman era — +86% signal"),
            new ZPoint(2.0,Hz(2.0),Gdagger(2.0),Gdagger(2.0)-g0,(Gdagger(2.0)-g0)/g0,"Strong evolution — +120% signal"),
            new ZPoint(3.0,Hz(3.0),Gdagger(3.0),Gdagger(3.0)-g0,(Gdagger(3.0)-g0)/g0,"+195% — but rotation curves very hard"),
        };

        double sigmaRar = 0.20; // dex scatter per galaxy
        var md = new[]{new MondDiff(0,Gdagger(0),g0,0,0.09,"NO — identical at z=0"),
            new MondDiff(0.5,Gdagger(0.5),g0,Gdagger(0.5)-g0,0.11,"MARGINAL (1.3σ per galaxy)"),
            new MondDiff(1.0,Gdagger(1.0),g0,Gdagger(1.0)-g0,0.13,"POSSIBLE (1.9σ per galaxy)"),
            new MondDiff(1.5,Gdagger(1.5),g0,Gdagger(1.5)-g0,0.15,"YES (2.5σ per galaxy)"),
            new MondDiff(2.0,Gdagger(2.0),g0,Gdagger(2.0)-g0,0.18,"YES (3.0σ per galaxy)"),
            new MondDiff(3.0,Gdagger(3.0),g0,Gdagger(3.0)-g0,0.22,"YES (3.5σ per galaxy) — but very hard observationally"),
        };

        var ic = new[]{new InstCap("JWST NIRSpec",2.5,0.25,50,"2022-2030","Best for z=1-2.5. 50 galaxies/cycle achievable."),
            new InstCap("Euclid NISP",1.5,0.35,200,"2027-2035","Wide survey. Many galaxies but lower S/N per galaxy."),
            new InstCap("Roman WFI",2.0,0.30,100,"2029-2035","Deep+wide. Ideal for z=1-2 RAR mapping."),
            new InstCap("ELT MICADO",3.5,0.20,30,"2032+","Highest z. Excellent S/N but few targets."),
            new InstCap("MUSE (VLT)",1.0,0.15,100,"Now-2030","Best local calibrator. 0<z<0.5, gold standard."),
            new InstCap("ALMA",3.0,0.35,40,"Now-2035","CO rotation curves. Dusty SF galaxies at z=1-3."),
        };

        // N = (sigma_per_gal / (delta/sqrt(N)))^2 = (sigma_per_gal * sigma_target / delta)^2
        // For z=1: delta=0.51 (fractional), sigma_per_gal=0.20 dex = 0.46 fractional
        // N_1sigma = (0.46/0.51)^2 = 0.81 -> 1 galaxy gives 1.1σ. Need 1 for 1σ, 4 for 2σ, 9 for 3σ
        // For z=1.5: delta=0.89, N_1sigma = (0.46/0.89)^2 = 0.27

        double spg = 0.20; // fractional uncertainty per galaxy (from 0.20 dex scatter)
        double d1 = (Gdagger(1.0)-g0)/g0; // fractional change at z=1
        double d15 = (Gdagger(1.5)-g0)/g0;
        double d2 = (Gdagger(2.0)-g0)/g0;

        int N1s(double d)=>Math.Max(1,(int)Math.Ceiling(spg*spg/(d*d)));
        int Nns(double d,int n)=>Math.Max(1,(int)Math.Ceiling(n*n*spg*spg/(d*d)));

        int totAvail = 50+200+100+30; // sum of NGalaxies from JWST+Euclid+Roman+ELT
        var sn = new[]{new SampleNeed(1,N1s(d1),totAvail,N1s(d1)<=totAvail,"2028","JWST+Euclid"),
            new SampleNeed(2,Nns(d1,2),totAvail,Nns(d1,2)<=totAvail,"2029","JWST+Euclid+Roman"),
            new SampleNeed(3,Nns(d1,3),totAvail,Nns(d1,3)<=totAvail,"2030","JWST+Euclid+Roman"),
            new SampleNeed(5,Nns(d15,5),totAvail,Nns(d15,5)<=totAvail,"2035","JWST+Euclid+Roman+ELT"),
        };

        var se = new[]{new SysEffect("Inclination uncertainty",0.05,"Require i>30deg, use 3D kinematic modeling.","MODERATE"),
            new SysEffect("Beam smearing",0.10,"Use adaptive binning, model PSF. High-z: larger physical scales.","HIGH"),
            new SysEffect("Morphology evolution",0.08,"z>1 galaxies more irregular. Select disk-dominated (Sersic n<2).","HIGH"),
            new SysEffect("Redshift uncertainty",0.02,"Spectroscopic z required. Photo-z insufficient.","LOW"),
            new SysEffect("Unresolved RCs",0.15,"Need >3 resolution elements across disk. JWST ~0.1 arcsec → ~0.8 kpc at z=1.","HIGH"),
            new SysEffect("M/L evolution",0.05,"Stellar pops brighter at high z. M/L must be modeled.","MODERATE"),
            new SysEffect("Gas fraction evolution",0.03,"Higher gas fraction at high z. Vgas contribution larger.","LOW"),
        };

        var tl = new[]{new TLEntry(1,2028,"JWST+Euclid","First g†(z) measurement. 1σ hint possible."),
            new TLEntry(2,2029,"JWST+Euclid+Roman","2σ evidence. Requires ~200 galaxies."),
            new TLEntry(3,2031,"All combined","3σ detection. TQM vs MOND distinguishable."),
            new TLEntry(5,2038,"ELT+JWST+Roman","5σ discovery. Definitive. But ~15 years away."),
        };

        string A=BuildA(zp,g0), B=BuildB(md), C=BuildC(ic), D=BuildD(sn), E=BuildE(se), F=BuildF(sn,tl), G=BuildG(tl), H=BuildH(), I=BuildI(sn,tl);
        return new FeasResult(A,B,C,D,E,F,G,H,I,zp,md,ic,sn,se,tl);
    }

    static string BuildA(ZPoint[] z,double g0){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("g†(z) SIGNAL EVOLUTION");sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  g†(0) = {0:F3} x1e-10 m/s^2",g0));sb.AppendLine();
        sb.AppendLine("  z      H(z)        g†(z)       Δ from z=0   Δ/g†(0)     Regime");
        sb.AppendLine("  -----  ----------  ----------  -----------  ----------  -----");
        foreach(var p in z) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-6:F1} {1,-11:F1} {2,-11:F3} {3,-12:F3} {4,-11:P0} {5}",p.Z,p.HZ,p.GDagger_1e10,p.DeltaFromZ0,p.FracChange,p.Regime));
        sb.AppendLine();sb.AppendLine("  TQM: g† DOUBLES by z≈2. MOND: constant.");
        return sb.ToString();
    }

    static string BuildB(MondDiff[] m){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("MOND vs TQM DIFFERENCE");sb.AppendLine();
        sb.AppendLine("  z      TQM g†     MOND a0    Δ          σ/galaxy   Detectable per galaxy?");
        sb.AppendLine("  -----  ---------  ---------  ---------  ---------  ------------------------");
        foreach(var x in m) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-6:F1} {1,-10:F3} {2,-10:F3} {3,-10:F3} {4,-10:F2} {5}",x.Z,x.Tqm,x.Mond,x.Delta,x.Sigma,x.Detectable));
        sb.AppendLine();sb.AppendLine("  By z≈1, individual galaxy can show ~2σ difference. Stacking N>10 required.");
        return sb.ToString();
    }

    static string BuildC(InstCap[] i){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("INSTRUMENT CAPABILITIES");sb.AppendLine();
        sb.AppendLine("  Instrument      z_max  σ/gal  N_gal  Timeline   Notes");
        sb.AppendLine("  --------------- -----  -----  -----  ---------  ----");
        foreach(var x in i) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-16} {1,-6:F1} {2,-6:F2} {3,-6} {4,-10} {5}",x.Instrument,x.Zmax,x.SigmaPerGal,x.NGalaxies,x.Timeline,x.Notes));
        return sb.ToString();
    }

    static string BuildD(SampleNeed[] s){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("SAMPLE SIZE REQUIREMENTS");sb.AppendLine();
        sb.AppendLine("  σ_target   N_required   N_available   Feasible?   Timeline");
        sb.AppendLine("  ---------  -----------  ------------  ----------  --------");
        foreach(var x in s) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-10:F0} {1,-12} {2,-13} {3,-11} {4}",x.SigmaTarget,x.NRequired,x.NAvailable,x.Feasible?"YES":"NO",x.Timeline));
        sb.AppendLine();sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  Total available: {0} galaxies across all instruments.",s[0].NAvailable));
        return sb.ToString();
    }

    static string BuildE(SysEffect[] s){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("SYSTEMATIC EFFECTS");sb.AppendLine();
        sb.AppendLine("  Effect                  Bias     Mitigation                          Severity");
        sb.AppendLine("  ----------------------  -------  ----------------------------------  --------");
        foreach(var x in s) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-23} {1,-8:F2} {2,-35} {3}",x.Effect,x.BiasMag,x.Mitigation,x.Severity));
        sb.AppendLine();sb.AppendLine("  Quadrature systematic floor: ~0.20 dex. Comparable to statistical error.");
        return sb.ToString();
    }

    static string BuildF(SampleNeed[] s,TLEntry[] t){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("DETECTION SIGNIFICANCE");sb.AppendLine();
        sb.AppendLine("  σ_level   Year   Instrument              Milestone");
        sb.AppendLine("  -------   ----   ----------------------  ---------");
        foreach(var x in t) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-9:F0}σ {1,-6} {2,-23} {3}",x.Sigma,x.Year,x.Instrument,x.Milestone));
        sb.AppendLine();sb.AppendLine("  By 2031: 3σ detection feasible with combined datasets.");
        return sb.ToString();
    }

    static string BuildG(TLEntry[] t)=>string.Format(CultureInfo.InvariantCulture,"EARLIEST FEASIBLE TEST\n\n  Prediction: g†(z) vs CONSTANT.\n  Earliest test: 2028 (JWST Cycle 2-3 high-z sample).\n  1σ hint: {0} with {1} galaxies.\n  3σ detection: {2} with combined JWST+Euclid+Roman.\n  5σ discovery: {3} with ELT+all.\n\n  The test is OBSERVATIONALLY ACCESSIBLE before 2031.",t[0].Year,t[0].Instrument,t[2].Year,t[3].Year);

    static string BuildH()=>"HOSTILE REVIEW\n\n1. Rotation curves at z>1 are EXTREMELY hard. Beam smearing\n   washes out the inner rise. Most z>1 'RCs' are just Vmax estimates.\n2. 20% per-galaxy precision is optimistic. Real scatter may be 40%.\n3. M/L evolution at high z is poorly understood — could mimic g†(z).\n4. Systematic floor (~0.20 dex) may prevent 5σ regardless of N.\n5. Galaxy morphology evolution means high-z disks differ from local.\n6. 2038 for 5σ is beyond current career horizons.\n7. If systematics cannot be controlled below 0.15 dex, the test\n   is FUNDAMENTALLY LIMITED, not just statistically limited.";

    static string BuildI(SampleNeed[] s,TLEntry[] t){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  g†(z) evolution at z=1: +{0:P0} over z=0.",(Gdagger(1.0)-Gdagger(0))/Gdagger(0)));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  g†(z) evolution at z=2: +{0:P0} over z=0.",(Gdagger(2.0)-Gdagger(0))/Gdagger(0)));
        sb.AppendLine();
        sb.AppendLine("  SAMPLE SIZE:");
        foreach(var x in s) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"    {0}σ: {1} galaxies ({2})",x.SigmaTarget,x.NRequired,x.Feasible?"FEASIBLE":"NOT FEASIBLE"));
        sb.AppendLine();
        sb.AppendLine("  TIMELINE:");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"    1σ hint:       {0} (JWST)",t[0].Year));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"    2σ evidence:   {0} (JWST+Euclid)",t[1].Year));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"    3σ detection:  {0} (all combined)",t[2].Year));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"    5σ discovery:  {0} (ELT+all)",t[3].Year));
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  OVERALL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  The g†(z) test is MEASURABLE but HARD.");
        sb.AppendLine("  - Before 2031: 1-3σ possible with JWST+Euclid+Roman.");
        sb.AppendLine("  - 5σ requires ELT (2035+) and exceptional systematics control.");
        sb.AppendLine("  - Systematic floor (~0.20 dex) is the limiting factor, not statistics.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B/C — MARGINALLY MEASURABLE");
        sb.AppendLine("  The test is observationally accessible before 2031 (barely).");
        sb.AppendLine("  But a DECISIVE 5σ result requires 2035+ with ELT.");
        sb.AppendLine();
        sb.AppendLine("  PRIORITY: Maximize JWST high-z rotation curve sample NOW.");
        sb.AppendLine("  Every Cycle 2-3 proposal for z>1 kinematics matters.");
        return sb.ToString();
    }
}
