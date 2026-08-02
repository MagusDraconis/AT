using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Investigates synchronization collapse at extreme coupling strength.
/// Fine K-sweep to detect optimal sync window and overcoupling instability.
/// </summary>
public static class OvercouplingAnalyzer
{
    public sealed record SyncProfile(
        double K, double FinalR, double FinalPhaseVar, double FreqSpread,
        double MeanPhaseOscillation, bool Synchronized, int Seed);

    public sealed record InstabilityReport(
        List<SyncProfile> Profiles,
        double PeakR, double OptimalK,
        double CollapseK, bool HasOptimalWindow,
        string Classification);

    public static SyncProfile RunKProfile(double k, double lambda, int n, int seed, int iters=2000)
    {
        var rng=new Random(seed);
        var net=new TemporalNetwork(n);
        for(int i=0;i<n;i++)
            net.AddNode(new TemporalNode(i,rng.NextDouble()*2*Math.PI,
                0.5+rng.NextDouble()*1.5)
            {X=rng.NextDouble(),Y=rng.NextDouble()});
        net.Matrix.FillSpatialCoupling(net.Nodes,k,lambda,normalize:false);

        double[] lastPhases=new double[n];
        for(int i=0;i<n;i++)lastPhases[i]=net.Nodes[i].Phase;

        for(int iter=0;iter<iters;iter++)
        {
            for(int i=0;i<n;i++)
            {
                double sum=0;
                for(int j=0;j<n;j++){if(i==j)continue;sum+=net.Matrix.GetCoupling(i,j)*Math.Sin(net.Nodes[j].Phase-net.Nodes[i].Phase);}
                net.Nodes[i].Phase=TemporalSimulation.NormalizePhase(net.Nodes[i].Phase+0.01*(net.Nodes[i].Frequency+sum));
            }
        }

        // Final R.
        var m=SynchronizationMetrics.FromNetwork(net,0);
        double r=m.OrderParameterR;
        double pv=m.PhaseVariance;
        double fs=Math.Sqrt(net.Nodes.Average(nd=>{
            double mf=net.Nodes.Average(x=>x.Frequency);
            return (nd.Frequency-mf)*(nd.Frequency-mf);
        }));

        // Phase oscillation: mean phase change in last 10 steps.
        for(int iter=iters-10;iter<iters;iter++)
        {
            for(int i=0;i<n;i++)
            {
                double sum=0;
                for(int j=0;j<n;j++){if(i==j)continue;sum+=net.Matrix.GetCoupling(i,j)*Math.Sin(net.Nodes[j].Phase-net.Nodes[i].Phase);}
                net.Nodes[i].Phase=TemporalSimulation.NormalizePhase(net.Nodes[i].Phase+0.01*(net.Nodes[i].Frequency+sum));
            }
        }
        double osc=0;for(int i=0;i<n;i++)osc+=Math.Abs(net.Nodes[i].Phase-lastPhases[i]);osc/=n;

        return new SyncProfile(k,r,pv,fs,osc,r>0.8,seed);
    }

    public static InstabilityReport Analyze(List<SyncProfile> profiles)
    {
        var ks=profiles.Select(p=>p.K).Distinct().OrderBy(k=>k).ToList();
        var byK=ks.Select(k=>profiles.Where(p=>Math.Abs(p.K-k)<0.001).ToList()).ToList();

        double peakR=profiles.Max(p=>p.FinalR);
        double optK=profiles.OrderByDescending(p=>p.FinalR).First().K;

        // Collapse K: where R drops below 0.5 after peak.
        double collapseK=0;
        bool foundPeak=false;
        foreach(var sub in byK)
        {
            double avgR=sub.Average(p=>p.FinalR);
            if(avgR>0.9)foundPeak=true;
            if(foundPeak&&avgR<0.5){collapseK=sub.First().K;break;}
        }

        bool hasOptimal=peakR>0.9&&collapseK>0&&optK<collapseK;
        string cls=collapseK>0?"C: Overcoupling Phase Transition":
                   hasOptimal?"B: Optimal Window":"A: Monotonic";

        return new InstabilityReport(profiles,peakR,optK,collapseK,hasOptimal,cls);
    }
}
