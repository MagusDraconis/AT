using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether coupling symmetry (even vs odd) is the fundamental
/// cause of effective spatial attraction.
/// </summary>
public static class CouplingSymmetryAnalyzer
{
    public sealed record SymmetryProfile(
        string LawName, double EvenWeight, double OddWeight,
        double InitialSep, double FinalSep, double SeparationChange,
        double FinalRA, double FinalRB,
        bool Converges, double AttractionScore, int Seed);

    public sealed record SymmetryReport(
        List<SymmetryProfile> Profiles,
        double EvenAttractionCorrelation,
        double OddAttractionCorrelation,
        bool EvenAlwaysAttracts, bool OddAlwaysFails,
        string Classification);

    // ── Coupling laws ────────────────────────────────────────────────

    private static readonly Dictionary<string, (Func<double, double> fn, double evenW, double oddW)> Laws = new()
    {
        ["E1: cos"]       = (d => Math.Cos(d),                    1.0, 0.0),
        ["E2: cos²"]      = (d => Math.Cos(d) * Math.Cos(d),      1.0, 0.0),
        ["E3: exp(-|x|)"] = (d => Math.Exp(-Math.Abs(d)),         1.0, 0.0),
        ["E4: 1/(1+|x|)"] = (d => 1.0 / (1.0 + Math.Abs(d)),     0.8, 0.0),
        ["O1: sin"]       = (d => Math.Sin(d),                    0.0, 1.0),
        ["O2: sin³"]      = (d => Math.Pow(Math.Sin(d), 3),       0.0, 1.0),
        ["O3: tanh"]      = (d => Math.Tanh(d),                   0.0, 0.7),
        ["M1: cos+sin"]   = (d => Math.Cos(d) + Math.Sin(d),      0.5, 0.5),
        ["M2: cos-sin"]   = (d => Math.Cos(d) - Math.Sin(d),      0.5, 0.5),
        ["M3: 0.5c+0.5s"] = (d => 0.5*Math.Cos(d)+0.5*Math.Sin(d),0.5,0.5),
        ["M4: cos*exp"]   = (d => Math.Cos(d)*Math.Exp(-Math.Abs(d)),0.8,0.0),
    };

    // ── Run ──────────────────────────────────────────────────────────

    public static SymmetryProfile RunSymmetryTest(
        string lawName, double evenW, double oddW, Func<double, double> forceFn,
        double sepLambda, double beta, double k, double lambda, int nPerGroup,
        int seed, int totalIters = 2000)
    {
        int n = nPerGroup * 2;
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);
        double sep = sepLambda * lambda;

        for (int i = 0; i < nPerGroup; i++)
            network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI, 1.0)
            { X = Math.Clamp(0.3 + (rng.NextDouble()*2-1)*lambda*0.8, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble()*2-1)*lambda*0.8, 0.01, 0.99) });
        for (int i = 0; i < nPerGroup; i++)
            network.AddNode(new TemporalNode(nPerGroup+i, rng.NextDouble()*2*Math.PI, 1.0)
            { X = Math.Clamp(0.3+sep + (rng.NextDouble()*2-1)*lambda*0.8, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble()*2-1)*lambda*0.8, 0.01, 0.99) });

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        double initSep = GroupSep(network, nPerGroup);

        for (int iter = 0; iter < totalIters; iter++)
        {
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                { if (i==j) continue; sum += network.Matrix.GetCoupling(i,j) * Math.Sin(network.Nodes[j].Phase - network.Nodes[i].Phase); }
                network.Nodes[i].Phase = TemporalSimulation.NormalizePhase(network.Nodes[i].Phase + 0.01 * (network.Nodes[i].Frequency + sum));
            }
            double[] nx = new double[n], ny = new double[n];
            for (int i = 0; i < n; i++)
            {
                double fx=0, fy=0;
                for (int j = 0; j < n; j++)
                {
                    if (i==j) continue;
                    double dx=network.Nodes[j].X-network.Nodes[i].X, dy=network.Nodes[j].Y-network.Nodes[i].Y;
                    double d=Math.Sqrt(dx*dx+dy*dy)+1e-10, w=network.Matrix.GetCoupling(i,j);
                    double pd = TemporalSimulation.NormalizePhase(network.Nodes[j].Phase-network.Nodes[i].Phase);
                    if (pd>Math.PI) pd-=2*Math.PI;
                    double f = forceFn(pd);
                    fx += w*f*dx/d; fy += w*f*dy/d;
                }
                nx[i]=Math.Clamp(network.Nodes[i].X+0.001*fx,0.01,0.99);
                ny[i]=Math.Clamp(network.Nodes[i].Y+0.001*fy,0.01,0.99);
            }
            for (int i=0;i<n;i++){network.Nodes[i].X=nx[i];network.Nodes[i].Y=ny[i];}
        }

        double finalSep = GroupSep(network, nPerGroup);
        double rA = GroupR(network, 0, nPerGroup), rB = GroupR(network, nPerGroup, nPerGroup);
        bool conv = finalSep < initSep * 0.95;
        double attrScore = Math.Clamp((initSep-finalSep)/Math.Max(initSep,1e-10),-1,1);

        return new SymmetryProfile(lawName, evenW, oddW, initSep, finalSep,
            finalSep-initSep, rA, rB, conv, attrScore, seed);
    }

    private static double GroupSep(TemporalNetwork n, int np)
    { double ax=0,ay=0,bx=0,by=0; for(int i=0;i<np;i++){ax+=n.Nodes[i].X;ay+=n.Nodes[i].Y;bx+=n.Nodes[i+np].X;by+=n.Nodes[i+np].Y;} ax/=np;ay/=np;bx/=np;by/=np; return Math.Sqrt((ax-bx)*(ax-bx)+(ay-by)*(ay-by)); }

    private static double GroupR(TemporalNetwork n, int s, int c)
    { double ss=0,sc=0; for(int i=s;i<s+c;i++){ss+=Math.Sin(n.Nodes[i].Phase);sc+=Math.Cos(n.Nodes[i].Phase);} return Math.Sqrt(ss*ss+sc*sc)/c; }

    public static SymmetryReport AnalyzeSymmetry(List<SymmetryProfile> profiles)
    {
        var evens = profiles.Select(p => p.EvenWeight).ToList();
        var odds = profiles.Select(p => p.OddWeight).ToList();
        var attrs = profiles.Select(p => p.AttractionScore).ToList();

        double eCorr = Corr(evens, attrs);
        double oCorr = Corr(odds, attrs);

        var evenOnly = profiles.Where(p => p.EvenWeight > 0.8 && p.OddWeight < 0.2).ToList();
        var oddOnly = profiles.Where(p => p.OddWeight > 0.8 && p.EvenWeight < 0.2).ToList();

        bool evenAlways = evenOnly.All(p => p.Converges);
        bool oddAlways = oddOnly.All(p => !p.Converges);

        string cls = evenAlways && oddAlways ? "D: Universal Symmetry Principle" :
                     eCorr > 0.7 ? "C: Symmetry Driven" :
                     eCorr > 0.4 ? "B: Function Specific" : "A: Parameter Driven";

        return new SymmetryReport(profiles, eCorr, oCorr, evenAlways, oddAlways, cls);
    }

    private static double Corr(List<double> x, List<double> y)
    { double mx=x.Average(),my=y.Average(),cov=0,vx=0,vy=0; for(int i=0;i<x.Count;i++){cov+=(x[i]-mx)*(y[i]-my);vx+=(x[i]-mx)*(x[i]-mx);vy+=(y[i]-my)*(y[i]-my);} return cov/Math.Sqrt(Math.Max(vx,1e-15)*Math.Max(vy,1e-15)); }
}
