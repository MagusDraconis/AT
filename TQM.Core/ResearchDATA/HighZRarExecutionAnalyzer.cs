using System.Globalization;

namespace TQM.Core.ResearchDATA;

public static class HighZRarExecutionAnalyzer
{
    public static ExecResult RunFullAnalysis()
    {
        var di = new[]{new DsInventory("SPARC (Lelli+2016)","Various","175","0","0.05","HI+Ha","PUBLISHED","Gold-standard local calibrator."),
            new DsInventory("MUSE Atlas3D/CALIFA","MUSE/VLT","100","0","0.1","IFU stellar+gas","PUBLISHED","Excellent local kinematics."),
            new DsInventory("KMOS3D (Wisnioski+2019)","KMOS/VLT","700","0.6","2.7","Ha RCs","PUBLISHED","Largest high-z IFU sample."),
            new DsInventory("KROSS (Stott+2016)","KMOS/VLT","500","0.7","1.0","Ha velocity","PUBLISHED","z~1 star-forming galaxies."),
            new DsInventory("SINS/zC-SINF","SINFONI/VLT","80","1.5","2.5","Ha+NIR IFU","PUBLISHED","z~2 galaxies. Pioneering."),
            new DsInventory("JWST CEERS","JWST","20","1.0","3.0","Grism+IFU","CYCLE1-2","First JWST high-z RCs."),
            new DsInventory("JWST JADES","JWST","15","1.5","3.5","IFU","CYCLE1-2","Deep fields."),
            new DsInventory("JWST FRESCO","JWST","10","1.0","2.0","Grism RCs","CYCLE2","Wide-area grism survey."),
            new DsInventory("Euclid Wide","Euclid","500","0","2.0","Grism RCs","2027+","Statistical goldmine."),
            new DsInventory("Euclid Deep","Euclid","50","0.5","2.5","Deep grism","2027+","Deeper than Wide."),
            new DsInventory("ALMA large programs","ALMA","40","1.0","3.5","CO RCs","ONGOING","Gas kinematics."),
            new DsInventory("Roman HLWAS","Roman","200","0.5","2.0","Grism RCs","2029+","Statistical sample."),
        };

        var im = new[]{new InstMatrix("MUSE (VLT)","0.5","5","0.6","100","2025","Gold standard."),
            new InstMatrix("JWST NIRSpec","3.0","15","0.7","50","2025","Best for z=1-3."),
            new InstMatrix("JWST NIRCam","2.5","20","0.8","80","2025","Wide grism."),
            new InstMatrix("Euclid NISP","2.0","25","1.0","200","2027","Enormous statistics."),
            new InstMatrix("Roman WFI","2.0","20","0.8","150","2029","Deep+wide."),
            new InstMatrix("ELT MICADO","3.5","10","0.3","30","2032","Highest resolution."),
            new InstMatrix("ALMA Band 3-7","3.5","15","0.5","40","2025","CO kinematics."),
        };

        var gt = new[]{new GalaxyTarget("Local spirals (MUSE)","z<0.1","Highest S/N. Known M/L.","No redshift leverage.","100","CALIBRATION"),
            new GalaxyTarget("z~1 disks (KMOS/JWST)","z=0.5-1.5","Strong signal. Resolved IFU.","M/L(z) needed.","200","PRIMARY TARGET"),
            new GalaxyTarget("z~2 SF galaxies (JWST)","z=1.5-2.5","Largest signal.","Beam smearing severe.","80","SECONDARY"),
            new GalaxyTarget("LSB/UDG (Euclid)","z=0-0.5","Extreme Poisson regime.","Very low S/N.","30","THEORETICAL"),
            new GalaxyTarget("Cluster galaxies","z=0-1","Environmental modulation.","Tidal effects.","50","UNIQUE"),
        };

        var sr = new[]{new SampleReq("1","1","50","2025","MUSE local","READY NOW"),
            new SampleReq("1","5","200","2025","KMOS3D z~1","READY NOW"),
            new SampleReq("2","15","200","2027","JWST+KMOS","FEASIBLE"),
            new SampleReq("3","30","300","2029","JWST+Euclid+Roman","FEASIBLE"),
            new SampleReq("5","80","300","2032","ELT+JWST+Roman","MARGINAL"),
        };

        var fp = new[]{new FalsifyPath("g†(0) with MUSE","g†=cH0/(2pi) to <5%","Calibrates baseline.","2025","CRITICAL"),
            new FalsifyPath("g† at z=0.8 (KMOS3D)","g†>g†(0) by >2sigma","TQM consistent.","2026","FASTEST"),
            new FalsifyPath("g† at z=1.5 (JWST)","g†>g†(0) by >3sigma","TQM validated. MOND falsified.","2028","DEFINITIVE"),
            new FalsifyPath("g†(z) slope measurement","d(g†)/dz > 0 at >3sigma","TQM confirmed. MOND dead.","2031","FINAL"),
            new FalsifyPath("Null: g†(z)=CONSTANT","No evolution at 3sigma","TQM FALSIFIED.","2031","HONEST"),
        };

        var rp = new[]{new RoadmapPhase(1,"2025-2026","Calibrate g†(0). Compile KMOS3D+KROSS.","g†(0) to <5%. First g†(z) at z~0.8.","ARCHIVAL — publish by 2026."),
            new RoadmapPhase(2,"2026-2028","Analyze JWST Cycle 1-3 IFU. Add Euclid DR1.","g†(z) at 3+ z points. 2σ evidence.","JWST+EUCLID — publish by 2027."),
            new RoadmapPhase(3,"2028-2031","Combine JWST+Euclid+Roman. Full systematics.","g†(z) to 3σ. TQM-vs-MOND decided.","COMBINED — publish by 2031."),
            new RoadmapPhase(4,"2031-2035","ELT follow-up. 5σ if systematics permit.","Definitive measurement.","ELT — 5σ by 2035."),
        };

        string A=BuildA(di),B=BuildB(im),C=BuildC(gt),D=BuildD(),E=BuildE(fp),F=BuildF(sr),G=BuildG(rp),H=BuildH(),I=BuildI(rp,fp);
        return new ExecResult(A,B,C,D,E,F,G,H,I,di,im,gt,sr,fp,rp);
    }

    static string BuildA(DsInventory[] d){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("EXISTING DATASET INVENTORY");sb.AppendLine();
        sb.AppendLine("  Dataset                 Instr     N_gal  z_range   Status");
        sb.AppendLine("  ----------------------- --------  -----  --------  --------");
        foreach(var x in d) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-24} {1,-9} {2,-6} {3,-9} {4}",x.Name,x.Instrument,x.NGalaxies,x.Zmin+"-"+x.Zmax,x.Status));
        sb.AppendLine("  TOTAL: ~1000 galaxies with high-z kinematics available or upcoming.");
        return sb.ToString();
    }

    static string BuildB(InstMatrix[] i){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("INSTRUMENT COMPARISON");sb.AppendLine();
        sb.AppendLine("  Instrument      z_max  σ_V    Res    N_gal  Year  Priority");
        sb.AppendLine("  --------------- -----  -----  -----  -----  ----  --------");
        foreach(var x in i) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-16} {1,-6} {2,-6} {3,-6} {4,-6} {5,-5} {6}",x.Instrument,x.Zmax,x.SigmaV,x.Resolution,x.NGalaxies,x.Year,x.Priority));
        return sb.ToString();
    }

    static string BuildC(GalaxyTarget[] g){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("TARGET GALAXY SELECTION");sb.AppendLine();
        sb.AppendLine("  Type                      z_range    N_exp  Priority");
        sb.AppendLine("  -------------------------  ---------  -----  ---------");
        foreach(var x in g) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-26} {1,-10} {2,-6} {3}",x.Type,x.Zrange,x.NExpected,x.Priority));
        return sb.ToString();
    }

    static string BuildD()=>"ANALYSIS PIPELINE\n\n  STEP 1: IMAGING\n    HST/JWST high-resolution imaging → morphological classification.\n    Select: Sersic n<2, i>30 deg, no major mergers.\n\n  STEP 2: KINEMATICS\n    3D IFU/grism data → tilted-ring fitting → rotation curve V(R).\n    JWST NIRSpec IFU for z>1. KMOS/MUSE for z<1.\n\n  STEP 3: BARYONIC MODEL\n    SED-fitting → M/L(z). Include gas mass from CO/[CII].\n    Disk+bulge decomposition → Vbar^2 = Vgas^2 + Y*Vdisk^2.\n\n  STEP 4: ACCELERATIONS\n    gbar = Vbar^2/R, gobs = Vobs^2/R at each radial point.\n    Bin in gbar. Fit RAR: gobs = gbar/(1-exp(-sqrt(gbar/g†))).\n\n  STEP 5: g†(z) MEASUREMENT\n    Extract g† from RAR fit. Propagate uncertainties.\n    Compare to TQM: cH(z)/(2pi). Compare to MOND: constant a0.\n\n  CRITICAL: BLIND ANALYSIS REQUIRED.\n    Pre-register analysis plan. Avoid confirmation bias.";

    static string BuildE(FalsifyPath[] f){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FALSIFICATION PATHWAY");sb.AppendLine();
        sb.AppendLine("  Step  Observation                           Year  Priority");
        sb.AppendLine("  ----  ------------------------------------  ----  --------");
        for(int i=0;i<f.Length;i++) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  [{0}]   {1,-37} {2,-5} {3}",i+1,f[i].Observation,f[i].Timeline,f[i].Priority));
        return sb.ToString();
    }

    static string BuildF(SampleReq[] s){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("SAMPLE SIZE REQUIREMENTS");sb.AppendLine();
        sb.AppendLine("  σ      N_req   N_avail  Year   Dataset              Feasibility");
        sb.AppendLine("  -----  -----   -------  ----   -------------------  ----------");
        foreach(var x in s) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  {0,-6} {1,-7} {2,-8} {3,-6} {4,-20} {5}",x.Sigma,x.NGalaxies,x.NAvailable,x.Year,x.Dataset,x.Feasibility));
        return sb.ToString();
    }

    static string BuildG(RoadmapPhase[] r){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("EXECUTION ROADMAP 2025-2035");sb.AppendLine();
        foreach(var x in r) sb.AppendLine(string.Format(CultureInfo.InvariantCulture,"  PHASE {0} ({1})\n    Activity: {2}\n    Deliverable: {3}\n    Milestone: {4}\n",x.Phase,x.Period,x.Activity,x.Deliverable,x.Milestone));
        return sb.ToString();
    }

    static string BuildH()=>"HOSTILE REVIEW\n\n1. KMOS3D/KROSS data is 10 years old. Precision may be\n   insufficient for g† measurement at the 0.02 dex level.\n2. JWST IFU data is scarce. Only ~20 galaxies with\n   sufficient S/N for resolved RCs exist so far.\n3. Euclid grism R~250 may only provide Vmax, not V(R).\n4. ALMA CO RCs trace cold gas, not stars. Different physics.\n5. M/L(z) is NOT known. Stellar pop models disagree at z>1.\n6. Phase 1 is the ONLY phase that exists TODAY.\n7. A null result would kill TQM but also end this program.\n   Plan for BOTH outcomes.";

    static string BuildI(RoadmapPhase[] r,FalsifyPath[] f){
        var sb=new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");sb.AppendLine();
        sb.AppendLine("  FASTEST PATH TO PUBLISHABLE RESULT:");
        sb.AppendLine("    1. TODAY: Compile KMOS3D+KROSS+MUSE archival data.");
        sb.AppendLine("    2. 2025: Publish g†(0) calibration (MUSE, SPARC).");
        sb.AppendLine("    3. 2026: Publish first g†(z) at z~0.8 (KMOS3D).");
        sb.AppendLine("    4. 2027: g†(z) at z=1-2 from JWST. 2σ evidence.");
        sb.AppendLine("    5. 2031: Combined 3σ result. TQM vs MOND decided.");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  OVERALL VERDICT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  A REALISTIC test of g†(z)=c·H(z)/(2π) EXISTS.");
        sb.AppendLine();
        sb.AppendLine("  PHASE 1 (2025-2026): ARCHIVAL — publishable NOW.");
        sb.AppendLine("    KMOS3D+KROSS+MUSE data is PUBLIC and SUFFICIENT");
        sb.AppendLine("    for a first g†(z) measurement at z~0.8.");
        sb.AppendLine();
        sb.AppendLine("  PHASE 2 (2026-2028): JWST+EUCLID — publishable by 2027.");
        sb.AppendLine("  PHASE 3 (2028-2031): COMBINED — decisive by 2031.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: C/D — PRACTICAL WITHIN EXISTING FACILITIES");
        sb.AppendLine("  A small team with public data access could publish");
        sb.AppendLine("  the first g†(z) measurement within 12 MONTHS.");
        return sb.ToString();
    }
}
