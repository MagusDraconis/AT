namespace TQM.Core.ResearchQG;

/// <summary>QG-093 exponent scan: test Λ ~ N^e for e ∈ {−1/4, −1/3, −1/2, −2/3, −1} and
/// the rival non-power-law numerology (1/N, 1/log N). The amplitude α must be O(1) — a
/// small/large α means the exponent is not the natural one.</summary>
public sealed record LambdaExponentRow(double Exponent, double LogLambdaPred, double LogLambdaObs,
    double ResidualDex, double Alpha);

public sealed record NumerologyRow(string Model, string LambdaInPlanckUnits, double ResidualDex, string Verdict);

public static class LambdaExponentAudit
{
    public static LambdaExponentRow[] Scan()
    {
        double obs = Math.Log10(CausalSetLambdaModel.ObservedLambdaPlanck());
        double[] exponents = { -0.25, -1.0 / 3.0, -0.5, -2.0 / 3.0, -1.0 };
        return exponents.Select(e =>
        {
            double pred = Math.Log10(CausalSetLambdaModel.LambdaPlanck(e));
            double alpha = Math.Pow(10, obs - pred); // Λ_obs = α·N^e
            return new LambdaExponentRow(e, pred, obs, obs - pred, alpha);
        }).ToArray();
    }

    public static NumerologyRow[] NumerologyComparison()
    {
        double obs = Math.Log10(CausalSetLambdaModel.ObservedLambdaPlanck());
        double logN = Math.Log10(CausalSetLambdaModel.N());
        var rows = new List<NumerologyRow>
        {
            new NumerologyRow("1/√N (causal set)", "N^-1/2",
                Math.Abs(obs - (-0.5 * logN)), Math.Abs(obs - (-0.5 * logN)) < 1.0 ? "O(1) match" : "off"),
            new NumerologyRow("1/N", "N^-1",
                Math.Abs(obs - (-logN)), Math.Abs(obs - (-logN)) < 1.0 ? "O(1) match" : "off by ~1e-122"),
            new NumerologyRow("1/log N", "1/log N",
                Math.Abs(obs - (-1.0 / logN)), Math.Abs(obs - (-1.0 / logN)) < 1.0 ? "O(1) match" : "off"),
            new NumerologyRow("1/N^(1/3)", "N^-1/3",
                Math.Abs(obs - (-logN / 3.0)), Math.Abs(obs - (-logN / 3.0)) < 1.0 ? "O(1) match" : "off"),
        };
        return rows.ToArray();
    }
}
