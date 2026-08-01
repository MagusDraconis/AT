namespace TQM.Core.TemporalField;

/// <summary>
/// A 1D temporal field that mediates interaction between oscillators.
///
/// The field evolves via a discrete wave equation with diffusion,
/// damping, and external source injection from coupled oscillators.
///
/// ∂²φ/∂t² = c²∇²φ + D∇²φ − γ ∂φ/∂t + S(x,t)
/// </summary>
public sealed class TemporalField
{
    private readonly TemporalFieldCell[] _cells;
    private readonly double[] _prevPhase; // φ(t−Δt) for wave equation

    public int CellCount { get; }

    /// <summary>
    /// Wave propagation speed c. Higher values → faster signal transmission.
    /// </summary>
    public double PropagationSpeed { get; set; } = 0.3;

    /// <summary>
    /// Diffusion coefficient D. Smooths the field by spreading energy.
    /// </summary>
    public double DiffusionCoefficient { get; set; } = 0.05;

    /// <summary>
    /// Damping coefficient γ. Dissipates field energy over time.
    /// </summary>
    public double DampingCoefficient { get; set; } = 0.005;

    /// <summary>
    /// Conversion factor from accumulated energy to density.
    /// </summary>
    public double EnergyToDensity { get; set; } = 1.0;

    public TemporalField(int cellCount)
    {
        if (cellCount < 2)
            throw new ArgumentOutOfRangeException(nameof(cellCount), "Field must have at least 2 cells.");

        CellCount = cellCount;
        _cells = new TemporalFieldCell[cellCount];
        _prevPhase = new double[cellCount];

        for (int i = 0; i < cellCount; i++)
            _cells[i] = new TemporalFieldCell();
    }

    /// <summary>
    /// Returns the cell at the given index.
    /// </summary>
    public TemporalFieldCell this[int i]
    {
        get
        {
            if (i < 0 || i >= CellCount)
                throw new ArgumentOutOfRangeException(nameof(i));
            return _cells[i];
        }
    }

    /// <summary>
    /// Injects energy from an oscillator at the specified cell position.
    /// This excites both the accumulated energy and the wave phase field.
    /// </summary>
    public void InjectEnergy(int cellIndex, double amount)
    {
        if (cellIndex < 0 || cellIndex >= CellCount)
            return;

        _cells[cellIndex].TemporalEnergy += amount;
        _cells[cellIndex].TemporalPhase += amount; // excite the wave field for propagation
    }

    /// <summary>
    /// Returns the temporal density at the specified cell.
    /// </summary>
    public double GetDensityAt(int cellIndex)
    {
        if (cellIndex < 0 || cellIndex >= CellCount)
            return 0.0;

        return _cells[cellIndex].TemporalDensity;
    }

    /// <summary>
    /// Returns the temporal phase at the specified cell.
    /// </summary>
    public double GetPhaseAt(int cellIndex)
    {
        if (cellIndex < 0 || cellIndex >= CellCount)
            return 0.0;

        return _cells[cellIndex].TemporalPhase;
    }

    /// <summary>
    /// Advances the field by one time step Δt.
    ///
    /// 1. Wave propagation: second-order time derivative + spatial Laplacian on phase.
    /// 2. Energy diffusion: spreads accumulated energy to neighbors.
    /// 3. Damping: dissipates both phase amplitude and accumulated energy.
    /// 4. Density: updated from accumulated energy.
    /// </summary>
    public void Update(double dt = 1.0)
    {
        double c2 = PropagationSpeed * PropagationSpeed;
        int n = CellCount;

        double[] newPhase = new double[n];
        double[] newEnergy = new double[n];

        for (int i = 0; i < n; i++)
        {
            int left = (i - 1 + n) % n;
            int right = (i + 1) % n;

            // ── Phase: wave equation with damping ──
            double phaseLaplacian = _cells[left].TemporalPhase
                                  + _cells[right].TemporalPhase
                                  - 2.0 * _cells[i].TemporalPhase;

            double waveTerm = c2 * phaseLaplacian;
            double dampingTerm = -DampingCoefficient * (_cells[i].TemporalPhase - _prevPhase[i]);

            newPhase[i] = 2.0 * _cells[i].TemporalPhase - _prevPhase[i]
                        + waveTerm
                        + dampingTerm;

            // ── Energy: diffusion + damping (no phase² feedback) ──
            double energyLaplacian = _cells[left].TemporalEnergy
                                   + _cells[right].TemporalEnergy
                                   - 2.0 * _cells[i].TemporalEnergy;

            double energyDiffusion = DiffusionCoefficient * energyLaplacian;
            double energyDamping = -DampingCoefficient * _cells[i].TemporalEnergy;

            newEnergy[i] = _cells[i].TemporalEnergy
                         + energyDiffusion
                         + energyDamping;

            // Clamp energy to non-negative.
            if (newEnergy[i] < 0) newEnergy[i] = 0;
        }

        // Swap state: current → previous, new → current.
        for (int i = 0; i < n; i++)
        {
            _prevPhase[i] = _cells[i].TemporalPhase;
            _cells[i].TemporalPhase = newPhase[i];
            _cells[i].TemporalEnergy = newEnergy[i];
            _cells[i].TemporalDensity = newEnergy[i] * EnergyToDensity;
        }
    }

    /// <summary>
    /// Computes the total energy across all field cells.
    /// </summary>
    public double TotalEnergy()
    {
        double sum = 0.0;
        for (int i = 0; i < CellCount; i++)
            sum += _cells[i].TemporalEnergy;
        return sum;
    }

    /// <summary>
    /// Computes the mean density across all cells.
    /// </summary>
    public double MeanDensity()
    {
        double sum = 0.0;
        for (int i = 0; i < CellCount; i++)
            sum += _cells[i].TemporalDensity;
        return sum / CellCount;
    }

    /// <summary>
    /// Finds the index of the cell with the highest density.
    /// </summary>
    public int PeakDensityCell()
    {
        int peak = 0;
        double max = _cells[0].TemporalDensity;
        for (int i = 1; i < CellCount; i++)
        {
            if (_cells[i].TemporalDensity > max)
            {
                max = _cells[i].TemporalDensity;
                peak = i;
            }
        }
        return peak;
    }

    /// <summary>
    /// Creates a snapshot of the current field state.
    /// </summary>
    public TemporalFieldSnapshot TakeSnapshot(int iteration)
    {
        double[] densityProfile = new double[CellCount];
        for (int i = 0; i < CellCount; i++)
            densityProfile[i] = _cells[i].TemporalDensity;

        return new TemporalFieldSnapshot(
            iteration,
            TotalEnergy(),
            MeanDensity(),
            _cells[PeakDensityCell()].TemporalDensity,
            PeakDensityCell(),
            densityProfile);
    }
}
