using System.Globalization;
using AT.Book.Domain;

namespace AT.Book.Services.Calculations;

/// <summary>
/// Flat-ΛCDM kinematics from the derived density fractions: deceleration parameter
/// q₀ = Ωm/2 − ΩΛ and acceleration redshift z_acc = (2ΩΛ/Ωm)^(1/3) − 1.
/// </summary>
public sealed class CosmologyService : ICalculationService
{
    private readonly InformationService _information;

    public string Name => "Cosmology";

    public IReadOnlyList<CalculationResult> Results { get; }

    public CosmologyService(InformationService information)
    {
        _information = information;
        var occ = OccupancyService.OctaveRecord();
        double iocc = InformationService.InformationContent(occ);
        double omegaLambda = iocc / InformationService.LnK;
        double omegaMatter = 1 - omegaLambda;
        double q0 = omegaMatter / 2 - omegaLambda;
        double zacc = Math.Pow(2 * omegaLambda / omegaMatter, 1.0 / 3.0) - 1.0;
        Results =
        [
            new(
                "deceleration",
                "Deceleration Parameter q₀",
                "q₀ = Ωm/2 − ΩΛ",
                [
                    new("Ωm", omegaMatter.ToString("0.0000", CultureInfo.InvariantCulture)),
                    new("ΩΛ", omegaLambda.ToString("0.0000", CultureInfo.InvariantCulture)),
                    new("q₀", q0.ToString("0.0000", CultureInfo.InvariantCulture), "negative ⇒ accelerating"),
                ],
                "q₀ < 0: the derived fractions imply an accelerating present epoch."),
            new(
                "acceleration-redshift",
                "Acceleration Redshift z_acc",
                "z_acc = (2ΩΛ/Ωm)^(1/3) − 1",
                [
                    new("2ΩΛ/Ωm", (2 * omegaLambda / omegaMatter).ToString("0.0000", CultureInfo.InvariantCulture)),
                    new("z_acc", zacc.ToString("0.0000", CultureInfo.InvariantCulture)),
                ],
                "The transition from deceleration to acceleration occurs near z_acc ≈ 0.63."),
        ];
    }
}
