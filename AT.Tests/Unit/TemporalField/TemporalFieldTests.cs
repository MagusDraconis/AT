using AT.Core.TemporalField;
using TemporalFieldClass = AT.Core.TemporalField.TemporalField;
using TemporalFieldCellClass = AT.Core.TemporalField.TemporalFieldCell;
using TemporalFieldSnapshotClass = AT.Core.TemporalField.TemporalFieldSnapshot;

namespace AT.Tests.Unit.TemporalField;

public class TemporalFieldCellTests
{
    [Fact]
    public void Constructor_Default_AllZeros()
    {
        var cell = new TemporalFieldCellClass();

        Assert.Equal(0.0, cell.TemporalPhase);
        Assert.Equal(0.0, cell.TemporalEnergy);
        Assert.Equal(0.0, cell.TemporalDensity);
    }

    [Fact]
    public void Constructor_WithValues_AssignsCorrectly()
    {
        var cell = new TemporalFieldCellClass(phase: 1.5, energy: 10.0, density: 3.0);

        Assert.Equal(1.5, cell.TemporalPhase);
        Assert.Equal(10.0, cell.TemporalEnergy);
        Assert.Equal(3.0, cell.TemporalDensity);
    }

    [Fact]
    public void Properties_AreMutable()
    {
        var cell = new TemporalFieldCellClass();
        cell.TemporalPhase = 2.0;
        cell.TemporalEnergy = 5.0;
        cell.TemporalDensity = 0.5;

        Assert.Equal(2.0, cell.TemporalPhase);
        Assert.Equal(5.0, cell.TemporalEnergy);
        Assert.Equal(0.5, cell.TemporalDensity);
    }
}

public class TemporalFieldTests
{
    [Fact]
    public void Constructor_WithCellCount_CreatesEmptyField()
    {
        var field = new TemporalFieldClass(10);

        Assert.Equal(10, field.CellCount);
        Assert.Equal(0.0, field.TotalEnergy());
    }

    [Fact]
    public void Constructor_WithInvalidCount_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TemporalFieldClass(1));
        Assert.Throws<ArgumentOutOfRangeException>(() => new TemporalFieldClass(0));
    }

    [Fact]
    public void InjectEnergy_IncreasesCellEnergy()
    {
        var field = new TemporalFieldClass(5);
        field.InjectEnergy(2, 10.0);

        Assert.Equal(10.0, field[2].TemporalEnergy);
    }

    [Fact]
    public void InjectEnergy_OutOfBounds_IsIgnored()
    {
        var field = new TemporalFieldClass(5);
        field.InjectEnergy(-1, 10.0);
        field.InjectEnergy(100, 10.0);

        Assert.Equal(0.0, field.TotalEnergy());
    }

    [Fact]
    public void Update_PropagatesEnergyBetweenCells()
    {
        var field = new TemporalFieldClass(20);
        field.PropagationSpeed = 0.3;
        field.DampingCoefficient = 0.0;
        field.DiffusionCoefficient = 0.05;

        // Inject energy at one location.
        field.InjectEnergy(10, 100.0);
        double initialTotal = field.TotalEnergy();

        // Run several updates to let energy spread.
        for (int i = 0; i < 50; i++)
            field.Update();

        // Neighboring cells should now have non-zero energy/density.
        Assert.True(field[9].TemporalDensity > 0.0 || field[11].TemporalDensity > 0.0,
            "Energy should have propagated to neighboring cells.");
    }

    [Fact]
    public void Damping_ReducesEnergyOverTime()
    {
        var field = new TemporalFieldClass(10);
        field.PropagationSpeed = 0.0;
        field.DiffusionCoefficient = 0.0;
        field.DampingCoefficient = 0.1;

        field.InjectEnergy(5, 100.0);
        double initialEnergy = field.TotalEnergy();

        for (int i = 0; i < 20; i++)
            field.Update();

        Assert.True(field.TotalEnergy() < initialEnergy,
            "Damping should reduce total energy.");
    }

    [Fact]
    public void TakeSnapshot_CapturesFieldState()
    {
        var field = new TemporalFieldClass(5);
        field.InjectEnergy(2, 50.0);
        field.Update();

        var snapshot = field.TakeSnapshot(42);

        Assert.Equal(42, snapshot.Iteration);
        Assert.True(snapshot.TotalEnergy > 0.0);
        Assert.Equal(5, snapshot.DensityProfile.Length);
    }

    [Fact]
    public void PeakDensityCell_ReturnsCorrectIndex()
    {
        var field = new TemporalFieldClass(10);
        field.InjectEnergy(7, 100.0);
        field.InjectEnergy(3, 50.0);

        // After injection but before Update, density comes from energy.
        // Update converts energy to density and propagates.
        field.Update();

        int peak = field.PeakDensityCell();
        // Peak should be near cell 7 (or at it, depending on propagation).
        Assert.True(peak >= 6 && peak <= 8,
            $"Peak should be near cell 7, got {peak}");
    }
}

public class TemporalFieldSnapshotTests
{
    [Fact]
    public void Snapshot_StoresAllProperties()
    {
        double[] profile = { 1.0, 2.0, 3.0 };
        var snap = new TemporalFieldSnapshotClass(10, 100.0, 0.5, 5.0, 2, profile);

        Assert.Equal(10, snap.Iteration);
        Assert.Equal(100.0, snap.TotalEnergy);
        Assert.Equal(0.5, snap.MeanDensity);
        Assert.Equal(5.0, snap.PeakDensity);
        Assert.Equal(2, snap.PeakCellIndex);
        Assert.Equal(3, snap.DensityProfile.Length);
        Assert.Equal(3.0, snap.DensityProfile[2]);
    }

    [Fact]
    public void Snapshot_DensityProfile_IsDefensiveCopy()
    {
        double[] profile = { 1.0, 2.0 };
        var snap = new TemporalFieldSnapshotClass(0, 0, 0, 0, 0, profile);

        profile[0] = 99.0;

        Assert.Equal(1.0, snap.DensityProfile[0]);
    }
}
