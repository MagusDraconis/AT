namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 48 — can GW observations be reconstructed without the GR tensor interpretation? ψ exists only
/// because of GW polarization data (QG47), so we audit what is DIRECTLY observed vs what is INFERRED. The directly
/// observed quantity is the STRAIN signal h(t) — the differential arm-length change δL/L. Everything above it —
/// polarization decomposition (h_+/h_×/breathing/vector), the GR waveform templates, and the spin-2 assignment —
/// is a MODEL-DEPENDENT inference. Hence spin-2 is RECONSTRUCTED, not directly measured: the tensor interpretation
/// is the conclusion of fitting the strain to a polarization basis under GR model assumptions. No new primitives.
/// </summary>
public static class GWObservationAudit
{
    /// <summary>The four layers of the GW observation, from raw data to interpretation.</summary>
    public static readonly string[] Layers =
    {
        "detector-signal",           // raw strain h(t)
        "polarization-reconstruction", // h_+/h_x/b/x decomposition
        "model-assumptions",         // GR waveform templates
        "spin-assignment",           // spin-2 (tensor) conclusion
    };

    /// <summary>Classification of each layer.</summary>
    public static string Classify(string layer) => layer switch
    {
        "detector-signal" => "DIRECT",               // strain h(t) is directly measured
        "polarization-reconstruction" => "MODEL-DEPENDENT", // needs a polarization basis + multi-detector analysis
        "model-assumptions" => "MODEL-DEPENDENT",    // GR waveform templates
        "spin-assignment" => "MODEL-DEPENDENT",      // spin-2 is inferred
        _ => throw new ArgumentOutOfRangeException(nameof(layer))
    };

    /// <summary>Is spin-2 DIRECTLY measured? No — only the strain signal is direct.</summary>
    public static bool Spin2DirectlyMeasured() => false;

    /// <summary>Is spin-2 RECONSTRUCTED (model-dependent)? Yes.</summary>
    public static bool Spin2Reconstructed() => true;

    /// <summary>The one directly-observed layer.</summary>
    public static string DirectLayer() => "detector-signal";
}
