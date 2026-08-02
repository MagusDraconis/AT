using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_067_OvercouplingInstability : ResearchTestBase
{
    private const double Lambda=0.05;
    private const int N=200;
    private const int BaseSeed=670514839;
    private static readonly double[] Ks={0.001,0.005,0.01,0.02,0.05,0.1,0.2,0.5,1,2,5,10,20,50,100};

    public TQM_067_OvercouplingInstability(ITestOutputHelper o):base(o){}

    [Fact]
    public void TQM_067_Run()
    {
        var orig=Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        try{RunTest();}finally{Thread.CurrentThread.CurrentCulture=orig;}
    }

    private void RunTest()
    {
        var sb=new StringBuilder();
        PrintHeader("TQM-067 Overcoupling Instability");
        sb.AppendLine("TQM-067: Why Does Synchronization Collapse at Extreme K?");
        sb.AppendLine();
        Sec(sb,"1. Objective");
        sb.AppendLine("  TQM-066: sync collapses at K=10 (3%). This maps the full");
        sb.AppendLine("  K-sweep to find the optimal synchronization window.");
        sb.AppendLine();

        Sec(sb,"2. K-Sweep");
        sb.AppendLine($"  K: [{Ks[0]}..{Ks[^1]}], {Ks.Length} levels, 3 seeds, N={N}");
        sb.AppendLine();

        var bag=new ConcurrentBag<OvercouplingAnalyzer.SyncProfile>();
        var sw=System.Diagnostics.Stopwatch.StartNew();
        Parallel.For(0,Ks.Length*3,idx=>{
            int ki=idx/3;bag.Add(OvercouplingAnalyzer.RunKProfile(Ks[ki],Lambda,N,BaseSeed+idx*7919));
        });
        sw.Stop();
        var profiles=bag.ToList();
        sb.AppendLine($"  Done in {sw.ElapsedMilliseconds}ms.");
        sb.AppendLine();

        var inst=OvercouplingAnalyzer.Analyze(profiles);

        Sec(sb,"3. Synchronization vs K");
        sb.AppendLine("  K        │ R       │ PhaseVar│ FreqSpread│ Oscillat│ Sync?");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach(var p in profiles.OrderBy(p=>p.K).ThenBy(p=>p.Seed))
            sb.AppendLine($"  {p.K,7:F3} │ {p.FinalR,6:F4} │ {p.FinalPhaseVar,7:F4} │ {p.FreqSpread,8:F4} │ {p.MeanPhaseOscillation,7:F4} │ {(p.Synchronized?"\u2713":" "),4}");
        sb.AppendLine();

        Sec(sb,"4. Peak & Collapse");
        sb.AppendLine($"  Peak R: {inst.PeakR:F4} at K={inst.OptimalK:F3}");
        sb.AppendLine($"  Collapse K: {(inst.CollapseK>0?$"Kc≈{inst.CollapseK:F3}":"none")}");
        sb.AppendLine($"  Optimal window: {(inst.HasOptimalWindow?"YES":"no")}");
        sb.AppendLine();

        // Average R by K.
        sb.AppendLine("  K        │ Avg R   │ Avg Sync%");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach(double k in Ks){
            var sub=profiles.Where(p=>Math.Abs(p.K-k)<0.001).ToList();
            sb.AppendLine($"  {k,7:F3} │ {sub.Average(p=>p.FinalR),6:F4} │ {100.0*sub.Count(p=>p.Synchronized)/sub.Count,7:F0}%");
        }
        sb.AppendLine();

        sb.AppendLine($"  Q1: Optimal K range?");
        sb.AppendLine($"    {(inst.HasOptimalWindow?$"YES — Peak at K={inst.OptimalK:F3}, R={inst.PeakR:F4}":"NO — No optimal window")}");
        sb.AppendLine();

        sb.AppendLine($"  Q2: Where does sync peak?");
        sb.AppendLine($"    K={inst.OptimalK:F3}, R={inst.PeakR:F4}");
        sb.AppendLine();

        sb.AppendLine($"  Q3: What destabilizes at large K?");
        sb.AppendLine($"    {(inst.CollapseK>0?$"Collapse at K≈{inst.CollapseK:F3}":"No collapse")}");
        sb.AppendLine();

        sb.AppendLine($"  Q4: Does phase chaos emerge?");
        var highK=profiles.Where(p=>p.K>10).ToList();
        bool chaos=highK.Any()&&highK.Average(p=>p.MeanPhaseOscillation)>0.5;
        sb.AppendLine($"    {(chaos?"YES — Large phase oscillations at high K":"NO — Phases stable")}");
        sb.AppendLine();

        sb.AppendLine($"  Q5: Does overcoupling create a new phase?");
        sb.AppendLine($"    {(inst.CollapseK>0?"YES — Disordered phase above Kc":"NO — Same phase at all K")}");
        sb.AppendLine();

        sb.AppendLine($"  Q6: Continuous or abrupt breakdown?");
        var rByK=Ks.Select(k=>profiles.Where(p=>Math.Abs(p.K-k)<0.001).Average(p=>p.FinalR)).ToList();
        double maxDrop=0;for(int i=1;i<rByK.Count;i++)maxDrop=Math.Max(maxDrop,Math.Abs(rByK[i]-rByK[i-1]));
        sb.AppendLine($"    {(maxDrop>0.3?"ABRUPT — Sharp transition":"CONTINUOUS — Smooth degradation")}");
        sb.AppendLine($"    Max R drop between consecutive K: {maxDrop:F4}");
        sb.AppendLine();

        Sec(sb,"5. Interpretation");
        sb.AppendLine($"  Classification: {inst.Classification}");
        sb.AppendLine();

        Sec(sb,"6. Conclusion");
        sb.AppendLine($"  C1. {inst.Classification}");
        sb.AppendLine($"  C2. Optimal K: {inst.OptimalK:F3}");
        sb.AppendLine();

        sb.AppendLine(new string('=',100));
        sb.AppendLine("  Experiment TQM-067 completed successfully.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
    }
    private static void Sec(StringBuilder sb,string t){sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
