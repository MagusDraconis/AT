namespace AT.App.Models;

/// <summary>
/// Derivational status of a major AT claim, from the claim-classification registry
/// (Docs/Research/ATQG_ClaimClassificationRegistry.md).
/// </summary>
public enum ClaimStatus
{
    /// <summary>Mathematically verified consequence of the stated structure; reproducible.</summary>
    Theorem,

    /// <summary>Forced within the stated (possibly scoped) assumptions.</summary>
    Necessity,

    /// <summary>Numerical/dimensional match to observation; no derivation mechanism.</summary>
    Correspondence,

    /// <summary>Value obtained only after multiplication by a measured anchor (v, m_e, SI conversion).</summary>
    Calibration,

    /// <summary>Carried as an external structure/input; explicitly not derived.</summary>
    Hosted,

    /// <summary>Value/status chosen (or tuned) to match a target; includes selection-from-candidates.</summary>
    Fit
}
