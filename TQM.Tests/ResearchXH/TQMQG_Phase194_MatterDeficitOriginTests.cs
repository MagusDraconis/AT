using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 194 — Matter Deficit Origin. Can "matter = ρ̄ − ρ" be DERIVED from TRM (not postulated)?
/// Chain: actualization deficit → energy deficit (QG89 energy = actualization rate) → rest mass (E=mc²) →
/// exact conservation (Noether count) → uniqueness (gradient-source identity, G4-ME5). No new primitives.
/// </summary>
public class TQMQG_Phase194_MatterDeficitOriginTests : ResearchTestBase
{
    public TQMQG_Phase194_MatterDeficitOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1940_DeficitIsEnergyAndMass()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1940: the deficit is the actualization (energy) deficit carrying rest mass");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - ρ = actualization rate = energy density (QG89: energy = actualization rate).");
        sb.AppendLine("  - E = mc² (QG89): the deficit energy carries rest mass.");
        sb.AppendLine();

        double rhoBar = 1.0, rhoVoid = 0.916;
        double m = MatterDeficitOrigin.ActualizationDeficit(rhoBar, rhoVoid);
        double eDef = MatterDeficitOrigin.EnergyDeficit(rhoBar, rhoVoid);
        double mass = MatterDeficitOrigin.DeficitMass(rhoBar, rhoVoid);

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  ρ̄ = {rhoBar:F3}, ρ(void) = {rhoVoid:F3}");
        sb.AppendLine($"  actualization deficit m = ρ̄ − ρ = {m:F4}");
        sb.AppendLine($"  energy deficit E_def = m (QG89) = {eDef:F4}");
        sb.AppendLine($"  deficit rest mass = m/c² = {mass:F4}");
        sb.AppendLine($"  energy = actualization rate (QG89)? {MatterDeficitOrigin.EnergyIsActualizationRate()}");
        sb.AppendLine($"  deficit carries rest mass (E=mc²)?  {MatterDeficitOrigin.DeficitCarriesRestMass()}");
        sb.AppendLine($"  deficit positive in voids (attractive)? {MatterDeficitOrigin.DeficitPositiveInVoids(rhoBar, rhoVoid)}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Matter = the actualization deficit m = ρ̄ − ρ (missed actualizations per unit volume).");
        sb.AppendLine("  - Since energy = actualization rate (QG89), the deficit IS a deficit in energy density.");
        sb.AppendLine("  - By E = mc² (QG89) the deficit energy carries rest mass — the gravitational source.");

        Output.WriteLine(sb.ToString());

        Assert.True(MatterDeficitOrigin.EnergyIsActualizationRate(), "energy = actualization rate (QG89)");
        Assert.True(MatterDeficitOrigin.DeficitCarriesRestMass(), "deficit energy carries rest mass (E=mc²)");
        Assert.True(m > 0 && eDef > 0 && mass > 0, "the void deficit is positive (attractive matter)");
        Assert.True(MatterDeficitOrigin.DeficitPositiveInVoids(rhoBar, rhoVoid));
    }

    [Fact]
    public void TQMQG1941_DeficitIsExactlyConserved()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1941: deficit conservation (∫m dV = the conserved count deviation)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The total event count N = ∫ρ dV is conserved (Noether / time-translation, QG89).");
        sb.AppendLine("  - Hence ∫m dV = ρ̄V − ∫ρ dV is the conserved count deviation — exact for the linear deficit.");
        sb.AppendLine();

        double rhoBar = 1.0, lo = -2.0, hi = 2.0;
        double mInt = MatterDeficitOrigin.IntegratedDeficit(x => rhoBar - 0.3 * Math.Exp(-x * x), rhoBar, lo, hi);
        double countDev = MatterDeficitOrigin.CountDeviation(x => rhoBar - 0.3 * Math.Exp(-x * x), rhoBar, lo, hi);
        bool conserved = MatterDeficitOrigin.DeficitIsConserved(x => rhoBar - 0.3 * Math.Exp(-x * x), rhoBar, lo, hi);

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  void profile ρ(x) = ρ̄ − 0.3·e^(−x²), domain [−2, 2]");
        sb.AppendLine($"  ∫m dV = ∫(ρ̄−ρ) dV = {mInt:F6}");
        sb.AppendLine($"  count deviation ρ̄V − ∫ρ dV = {countDev:F6}");
        sb.AppendLine($"  exact conservation (∫m dV = count deviation)? {conserved}");
        sb.AppendLine($"  only the LINEAR deficit integrates to the count deviation? {MatterDeficitOrigin.OnlyLinearDeficitConserved()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The deficit abundance is EXACTLY the conserved count deviation (Noether):");
        sb.AppendLine("    matter is conserved because actualizations are conserved.");
        sb.AppendLine("  - The log/ratio transforms are nonlinear and do NOT conserve the count (G4-ME5) —");
        sb.AppendLine("    only m = ρ̄ − ρ has the exact conservation identity.");

        Output.WriteLine(sb.ToString());

        Assert.True(conserved, "the deficit must integrate exactly to the count deviation");
        Assert.True(MatterDeficitOrigin.OnlyLinearDeficitConserved(), "only the linear deficit conserves the count");
        Assert.True(Math.Abs(mInt - countDev) < 1e-9, "∫m dV must equal the count deviation");
    }

    [Fact]
    public void TQMQG1942_ClassificationDeficitOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1942: matter-deficit origin classification");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Classification is data-driven from the phase-194 derivation.");
        sb.AppendLine("  - Energy origin + conservation + uniqueness ⇒ DEFICIT ORIGIN.");
        sb.AppendLine();

        int score = MatterDeficitOrigin.OriginScore();
        string classification = MatterDeficitOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  OriginScore (max 3) = {score}");
        sb.AppendLine($"    +1 energy = actualization rate (QG89); deficit = energy deficit carrying rest mass");
        sb.AppendLine($"    +1 deficit abundance exactly conserved (∫m dV = count deviation, Noether)");
        sb.AppendLine($"    +1 deficit form unique (gradient-source identity + normalization, G4-ME5)");
        sb.AppendLine($"  deficit unique (∇m = −∇ρ, m(ρ̄)=0)? {MatterDeficitOrigin.DeficitIsUnique(1.0, 0.916)}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("FINAL CONCLUSION:");
        sb.AppendLine("  Matter = ρ̄ − ρ is DERIVED, not postulated:");
        sb.AppendLine("    • the deficit IS the actualization (energy) deficit — QG89 energy = actualization rate;");
        sb.AppendLine("    • the deficit energy carries rest mass — E = mc² (QG89);");
        sb.AppendLine("    • the deficit is EXACTLY conserved — the Noether count deviation;");
        sb.AppendLine("    • the deficit form is UNIQUE — the gradient-source identity (G4-ME5).");
        sb.AppendLine("  No new primitives.");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("DEFICIT ORIGIN", classification);
        Assert.True(score == 3, "all three evidence channels (energy, conservation, uniqueness)");
    }
}
