using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_066_CriticalCouplingUniversality : ResearchTestBase
{
    private const double Lambda = 0.05;
    private const double Beta = 0.5;
    private const int NPerGroup = 50;
    private const int BaseSeed = 660173849;

    private static readonly double[] Ks = {0.01,0.02,0.05,0.1,0.2,0.5,1.0,2.0,5.0,10.0};
    private static readonly string[] LawNames = {"cos","sin","cos²","exp","1/(1+|x|)","cos*exp","sign(cos)","1-|x|/pi"};
    private static readonly double[] Separations = {0.5,2.0};

    public AT_066_CriticalCouplingUniversality(ITestOutputHelper o):base(o){}

    [Fact]
    public void AT_066_Run()
    {
        var orig=Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        try{RunTest();}finally{Thread.CurrentThread.CurrentCulture=orig;}
    }

    private void RunTest()
    {
        var sb=new StringBuilder();
        PrintHeader("AT-066 Critical Coupling Universality");
        sb.AppendLine("AT-066: Is There a Kc Where Coupling Law Becomes Irrelevant?");
        sb.AppendLine();
        Sec(sb,"1. Objective");
        sb.AppendLine("  AT-065: even symmetry guarantees attraction, odd fails at far range.");
        sb.AppendLine("  This tests whether a critical K exists above which ALL laws");
        sb.AppendLine("  produce universal behavior regardless of their detailed form.");
        sb.AppendLine();

        Sec(sb,"2. Setup");
        int total=LawNames.Length*Ks.Length*Separations.Length*2;
        sb.AppendLine($"  {LawNames.Length} laws × {Ks.Length} K × {Separations.Length} sep × 2 seeds = {total} runs");
        sb.AppendLine();

        var bag=new ConcurrentBag<CriticalCouplingAnalyzer.KProfile>();
        var sw=System.Diagnostics.Stopwatch.StartNew();
        Parallel.For(0,total,idx=>{
            int li=idx%LawNames.Length,rem=idx/LawNames.Length;
            int ki=rem%Ks.Length;rem/=Ks.Length;
            int si=rem%Separations.Length;int seedI=rem/Separations.Length;
            bag.Add(CriticalCouplingAnalyzer.RunKProfile(
                LawNames[li],Ks[ki],Separations[si],Beta,Lambda,NPerGroup,
                BaseSeed+idx*7919));
        });
        sw.Stop();
        var profiles=bag.ToList();
        sb.AppendLine($"  Done in {sw.ElapsedMilliseconds}ms.");
        sb.AppendLine();

        var univ=CriticalCouplingAnalyzer.AnalyzeUniversality(profiles);

        // ── Section 3: K-Sweep ───────────────────────────────────────
        Sec(sb,"3. Coupling Strength Sweep");
        sb.AppendLine("  K      │ MeanAttr│ LawStd  │ Converge%│ Sync%");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach(var up in univ.ByK)
            sb.AppendLine($"  {up.K,5:F2} │ {up.MeanAttraction,7:P1} │ {up.LawVariance,7:P1} │ {up.ConvergeFraction,8:P0} │ {up.SyncFraction,6:P0}");
        sb.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        sb.AppendLine($"  Q1: Do laws behave differently at low K?");
        sb.AppendLine($"    Law variance at K=0.01: {univ.ByK.First().LawVariance:P1}");
        sb.AppendLine($"    {(univ.ByK.First().LawVariance>0.1?"YES — Significant law dependence":"NO — Already universal")}");
        sb.AppendLine();

        sb.AppendLine($"  Q2: Does behavior converge at high K?");
        sb.AppendLine($"    Law variance at K=10: {univ.ByK.Last().LawVariance:P1}");
        sb.AppendLine($"    {(univ.ByK.Last().LawVariance<0.1?"YES — Universal at high K":"NO — Still law-dependent")}");
        sb.AppendLine();

        sb.AppendLine($"  Q3: Does a critical Kc exist?");
        sb.AppendLine($"    {(univ.CriticalK>0?$"YES — Kc≈{univ.CriticalK:F2}":"NO — No clear threshold")}");
        sb.AppendLine();

        sb.AppendLine($"  Q4: How rapidly does universality emerge?");
        sb.AppendLine($"    {(univ.PhaseTransition?"SHARPLY — Phase transition detected":"GRADUALLY — Smooth crossover")}");
        sb.AppendLine();

        sb.AppendLine($"  Q5: Is attraction or sync first to become universal?");
        var firstUniversal=univ.ByK.FirstOrDefault(u=>u.LawVariance<0.1);
        if(firstUniversal!=null)sb.AppendLine($"    At K≈{firstUniversal.K:F2}: sync {firstUniversal.SyncFraction:P0}, attract {firstUniversal.ConvergeFraction:P0}");
        sb.AppendLine();

        sb.AppendLine($"  Q6: Do odd and even laws become indistinguishable?");
        sb.AppendLine($"    Law variance at K=10: {univ.ByK.Last().LawVariance:P1}");
        sb.AppendLine($"    {(univ.ByK.Last().LawVariance<0.05?"YES — Indistinguishable":"NO — Still distinct")}");
        sb.AppendLine();

        sb.AppendLine($"  Q7: Phase transition or smooth crossover?");
        sb.AppendLine($"    {(univ.PhaseTransition?"PHASE TRANSITION — Sharp threshold":"SMOOTH CROSSOVER — Gradual change")}");
        sb.AppendLine();

        // ── Per-law at low/high K ────────────────────────────────────
        Sec(sb,"4. Law Behavior at Low vs High K");
        var lowK=profiles.Where(p=>p.K<0.05).ToList();
        var highK=profiles.Where(p=>p.K>5.0).ToList();
        sb.AppendLine("  Law           │ LowK Attr│ HighK Attr│ LowK Sync│ HighK Sync");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach(string law in LawNames)
        {
            var l=lowK.Where(p=>p.LawName==law).ToList();
            var h=highK.Where(p=>p.LawName==law).ToList();
            double la=l.Count>0?l.Average(p=>p.AttractionScore):0;
            double ha=h.Count>0?h.Average(p=>p.AttractionScore):0;
            double ls=l.Count>0?(double)l.Count(p=>p.Synchronizes)/l.Count:0;
            double hs=h.Count>0?(double)h.Count(p=>p.Synchronizes)/h.Count:0;
            sb.AppendLine($"  {law,-13} │ {la,8:P1} │ {ha,8:P1} │ {ls,8:P0} │ {hs,8:P0}");
        }
        sb.AppendLine();

        Sec(sb,"5. Interpretation");
        sb.AppendLine($"  Classification: {univ.Classification}");
        sb.AppendLine($"  Critical K: {(univ.CriticalK>0?$"Kc≈{univ.CriticalK:F2}":"none found")}");
        sb.AppendLine();

        Sec(sb,"6. Conclusion");
        sb.AppendLine($"  C1. {univ.Classification}");
        sb.AppendLine();

        sb.AppendLine(new string('=',100));
        sb.AppendLine("  Experiment AT-066 completed successfully.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb,string t){sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
