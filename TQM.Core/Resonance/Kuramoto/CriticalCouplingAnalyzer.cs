using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether a critical coupling strength Kc exists above which
/// all coupling laws produce universal synchronization and attraction.
/// </summary>
public static class CriticalCouplingAnalyzer
{
    public sealed record KProfile(
        string LawName, double K, double SepLambda,
        double AttractionScore, bool Converges, bool Synchronizes,
        double FinalRA, double FinalRB, int Seed);

    public sealed record UniversalityPoint(
        double K, double SepLambda,
        double MeanAttraction, double StdAttraction,
        double ConvergeFraction, double SyncFraction,
        double LawVariance);

    public sealed record UniversalityReport(
        List<KProfile> Profiles,
        List<UniversalityPoint> ByK,
        double CriticalK,
        bool PhaseTransition,
        string Classification);

    // ── Coupling laws ────────────────────────────────────────────────

    private static readonly Dictionary<string, Func<double, double>> Laws = new()
    {
        ["cos"]=Math.Cos, ["sin"]=Math.Sin, ["cos²"]=d=>Math.Cos(d)*Math.Cos(d),
        ["exp"]=d=>Math.Exp(-Math.Abs(d)), ["1/(1+|x|)"]=d=>1.0/(1+Math.Abs(d)),
        ["cos*exp"]=d=>Math.Cos(d)*Math.Exp(-Math.Abs(d)),
        ["sign(cos)"]=d=>Math.Sign(Math.Cos(d)), ["1-|x|/pi"]=d=>1.0-Math.Abs(d)/Math.PI,
    };

    // ── Run ──────────────────────────────────────────────────────────

    public static KProfile RunKProfile(
        string lawName, double kCoupling, double sepLambda, double beta,
        double lambda, int nPerGroup, int seed, int totalIters=1500)
    {
        var fn = Laws[lawName];
        int n = nPerGroup*2;
        var rng = new Random(seed);
        var net = new TemporalNetwork(n);
        double sep = sepLambda*lambda;
        for(int i=0;i<nPerGroup;i++)
            net.AddNode(new TemporalNode(i,rng.NextDouble()*2*Math.PI,1.0)
            {X=Math.Clamp(0.3+(rng.NextDouble()*2-1)*lambda*0.8,0.01,0.99),
             Y=Math.Clamp(0.5+(rng.NextDouble()*2-1)*lambda*0.8,0.01,0.99)});
        for(int i=0;i<nPerGroup;i++)
            net.AddNode(new TemporalNode(nPerGroup+i,rng.NextDouble()*2*Math.PI,1.0)
            {X=Math.Clamp(0.3+sep+(rng.NextDouble()*2-1)*lambda*0.8,0.01,0.99),
             Y=Math.Clamp(0.5+(rng.NextDouble()*2-1)*lambda*0.8,0.01,0.99)});
        net.Matrix.FillSpatialCoupling(net.Nodes, kCoupling, lambda, normalize:false);
        double initSep=GroupSep(net,nPerGroup);

        for(int iter=0;iter<totalIters;iter++)
        {
            for(int i=0;i<n;i++)
            {
                double sum=0;
                for(int j=0;j<n;j++){if(i==j)continue;sum+=net.Matrix.GetCoupling(i,j)*Math.Sin(net.Nodes[j].Phase-net.Nodes[i].Phase);}
                net.Nodes[i].Phase=TemporalSimulation.NormalizePhase(net.Nodes[i].Phase+0.01*(net.Nodes[i].Frequency+sum));
            }
            double[] nx=new double[n],ny=new double[n];
            for(int i=0;i<n;i++)
            {
                double fx=0,fy=0;
                for(int j=0;j<n;j++)
                {if(i==j)continue;double dx=net.Nodes[j].X-net.Nodes[i].X,dy=net.Nodes[j].Y-net.Nodes[i].Y,d=Math.Sqrt(dx*dx+dy*dy)+1e-10,w=net.Matrix.GetCoupling(i,j);double pd=TemporalSimulation.NormalizePhase(net.Nodes[j].Phase-net.Nodes[i].Phase);if(pd>Math.PI)pd-=2*Math.PI;double f=fn(pd);fx+=w*f*dx/d;fy+=w*f*dy/d;}
                nx[i]=Math.Clamp(net.Nodes[i].X+0.001*fx,0.01,0.99);ny[i]=Math.Clamp(net.Nodes[i].Y+0.001*fy,0.01,0.99);
            }
            for(int i=0;i<n;i++){net.Nodes[i].X=nx[i];net.Nodes[i].Y=ny[i];}
        }
        double fSep=GroupSep(net,nPerGroup);
        double rA=GroupR(net,0,nPerGroup),rB=GroupR(net,nPerGroup,nPerGroup);
        return new KProfile(lawName,kCoupling,sepLambda,
            Math.Clamp((initSep-fSep)/Math.Max(initSep,1e-10),-1,1),fSep<initSep*0.95,rA>0.8&&rB>0.8,rA,rB,seed);
    }

    private static double GroupSep(TemporalNetwork n,int np){double ax=0,ay=0,bx=0,by=0;for(int i=0;i<np;i++){ax+=n.Nodes[i].X;ay+=n.Nodes[i].Y;bx+=n.Nodes[i+np].X;by+=n.Nodes[i+np].Y;}ax/=np;ay/=np;bx/=np;by/=np;return Math.Sqrt((ax-bx)*(ax-bx)+(ay-by)*(ay-by));}
    private static double GroupR(TemporalNetwork n,int s,int c){double ss=0,sc=0;for(int i=s;i<s+c;i++){ss+=Math.Sin(n.Nodes[i].Phase);sc+=Math.Cos(n.Nodes[i].Phase);}return Math.Sqrt(ss*ss+sc*sc)/c;}

    public static UniversalityReport AnalyzeUniversality(List<KProfile> profiles)
    {
        var ks = profiles.Select(p=>p.K).Distinct().OrderBy(k=>k).ToList();
        var seps = profiles.Select(p=>p.SepLambda).Distinct().OrderBy(s=>s).ToList();
        var byK = new List<UniversalityPoint>();

        foreach(double k in ks)
        {
            var sub = profiles.Where(p=>Math.Abs(p.K-k)<0.001).ToList();
            double meanA=sub.Average(p=>p.AttractionScore);
            double stdA=Math.Sqrt(sub.Average(p=>(p.AttractionScore-meanA)*(p.AttractionScore-meanA)));
            double convF=(double)sub.Count(p=>p.Converges)/sub.Count;
            double syncF=(double)sub.Count(p=>p.Synchronizes)/sub.Count;
            // Law variance: std of per-law mean attraction.
            double lawVar=0;
            foreach(var law in Laws.Keys)
            {
                var ls = sub.Where(p=>p.LawName==law).ToList();
                if(ls.Count>0){double lm=ls.Average(p=>p.AttractionScore);lawVar+=(lm-meanA)*(lm-meanA);}
            }
            lawVar=Math.Sqrt(lawVar/Math.Max(Laws.Count,1));
            byK.Add(new UniversalityPoint(k,0,meanA,stdA,convF,syncF,lawVar));
        }

        // Critical K: where law variance drops below 0.1.
        double critK=0;
        bool phaseTransition=false;
        foreach(var up in byK.OrderBy(u=>u.K))
        {
            if(up.LawVariance<0.1&&critK==0) critK=up.K;
        }
        // Check if variance drops sharply.
        if(byK.Count>=2)
        {
            var sorted=byK.OrderBy(u=>u.K).ToList();
            double firstVar=sorted[0].LawVariance,lastVar=sorted[^1].LawVariance;
            phaseTransition=lastVar<firstVar*0.3;
        }

        string cls=critK>0&&phaseTransition?$"C: Strong Universality Above Kc≈{critK:F2}":
                   critK>0?$"B: Weak Universality (Kc≈{critK:F2})":"A: No Universality";

        return new UniversalityReport(profiles,byK,critK,phaseTransition,cls);
    }
}
