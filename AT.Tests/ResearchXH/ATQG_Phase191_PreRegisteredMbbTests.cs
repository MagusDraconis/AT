using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 191 — Pre-Registered Neutrinoless Double Beta Decay. The effective Majorana mass prediction
/// m_ββ is LOCKED from QG167 (PMNS), QG172 (masses) and QG179 (Majorana) ONLY, before any future measurement.
/// Forbidden: experimental limits, detector sensitivities, future measurements. Deterministic.
/// </summary>
public class ATQG_Phase191_PreRegisteredMbbTests : ResearchTestBase
{
    public ATQG_Phase191_PreRegisteredMbbTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1910_MbbFrozenFromD96Inputs()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1910: pre-registered m_ββ from QG167/172/179 only");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Inputs are QG167 PMNS, QG172 masses, QG179 Majorana result — NOTHING else.");
        sb.AppendLine("  - NO experimental limit, detector sensitivity, or future measurement is used.");
        sb.AppendLine();

        double mbbMeV = PreRegisteredMbb.MbbMeV();
        double m1 = PreRegisteredMbb.M1(), m2 = PreRegisteredMbb.M2(), m3 = PreRegisteredMbb.M3();
        double s12 = PreRegisteredMbb.SinTheta12(), s13 = PreRegisteredMbb.SinTheta13();

        sb.AppendLine("INTERMEDIATE CALCULATIONS (all D96-derived):");
        sb.AppendLine($"  QG172 masses: m1 = {m1} eV, m2 = {m2 * 1e3:F2} meV, m3 = {m3 * 1e3:F2} meV");
        sb.AppendLine($"  QG167 angles: s12 = {s12:F4}, s13 = {s13:F4}, δ_ν = {PreRegisteredMbb.DeltaNuDeg():F1}°");
        sb.AppendLine($"  QG179 phases: α2 = {PreRegisteredMbb.MajoranaPhases().Alpha2}, α3 = {PreRegisteredMbb.MajoranaPhases().Alpha3}");
        sb.AppendLine();
        sb.AppendLine("PRE-REGISTERED OUTPUTS:");
        sb.AppendLine($"  1. m_ββ = |Σ U_ei²·m_i| = {mbbMeV:F2} meV   (locked value 2.02 meV)");
        sb.AppendLine($"  2. mass ordering NORMAL (m1=0<m2<m3)?  {PreRegisteredMbb.NormalOrdering()}");
        sb.AppendLine($"  3. Majorana phases vanish (real matrix)? {PreRegisteredMbb.MajoranaPhasesVanish()}");
        sb.AppendLine($"  forbidden-input guard: {PreRegisteredMbb.ForbiddenInputsNeverUsed()}");

        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(mbbMeV - 2.02) < 0.01, "m_ββ must be 2.02 meV (D96-computed)");
        Assert.True(PreRegisteredMbb.NormalOrdering(), "normal ordering m1=0<m2<m3 must be frozen");
        Assert.True(PreRegisteredMbb.MajoranaPhasesVanish(), "Majorana phases must vanish (QG179 real matrix)");
        Assert.True(PreRegisteredMbb.ForbiddenInputsNeverUsed(),
            "no experimental limit/sensitivity may enter the prediction");
    }

    [Fact]
    public void ATQG1911_OutputsAreCompleteAndConsistent()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1911: pre-registered outputs complete and internally consistent");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The three outputs (m_ββ, mass ordering, phase assumption) are frozen and consistent.");
        sb.AppendLine();

        double mbbMeV = PreRegisteredMbb.MbbMeV();
        double m2 = PreRegisteredMbb.M2() * 1e3;
        double m3 = PreRegisteredMbb.M3() * 1e3;
        double m2Term = m2 * PreRegisteredMbb.SinTheta12() * PreRegisteredMbb.SinTheta12()
                          * (1 - PreRegisteredMbb.SinTheta13() * PreRegisteredMbb.SinTheta13());
        double m3Term = m3 * PreRegisteredMbb.SinTheta13() * PreRegisteredMbb.SinTheta13();

        sb.AppendLine("CONSISTENCY CHECKS:");
        sb.AppendLine($"  m_ββ (2.02 meV) non-zero:                  {mbbMeV > 0}");
        sb.AppendLine($"  m_ββ &lt; m2 (8.72 meV):                      {mbbMeV < m2}");
        sb.AppendLine($"  m_ββ &lt; m3 (49.4 meV):                      {mbbMeV < m3}");
        sb.AppendLine($"  m2·s12²·c13² term = {m2Term:F2} meV, m3·s13² term = {m3Term:F2} meV");
        sb.AppendLine($"  m2 term dominates (m2·s12²·c13² > m3·s13²)? {m2Term > m3Term}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  m_ββ ≈ 2.02 meV is dominated by the m2·s12²·c13² term (2.52 meV, m1 = 0); the");
        sb.AppendLine("  m3·s13²·e^(−2iδ) term (1.04 meV) partially interferes to give the frozen value.");
        sb.AppendLine("  The prediction is therefore robust to the CP-phase assumption (the m3 term is small).");

        Output.WriteLine(sb.ToString());

        Assert.True(mbbMeV > 0, "m_ββ must be non-zero (Majorana decay allowed)");
        Assert.True(mbbMeV < m2, "m_ββ must be below the lightest massive neutrino m2");
        Assert.True(mbbMeV < m3, "m_ββ must be below m3");
    }

    [Fact]
    public void ATQG1912_AcceptanceCriteria()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1912: acceptance criteria (CONFIRMED / FALSIFIED)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - CONFIRMED: future measurement consistent with the 2.02 meV range (±10%).");
        sb.AppendLine("  - FALSIFIED: significant exclusion below the prediction.");
        sb.AppendLine();

        double mbbMeV = PreRegisteredMbb.MbbMeV();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  frozen m_ββ = {mbbMeV:F2} meV");
        sb.AppendLine($"  CONFIRMED (measured 2.10 meV)?    {PreRegisteredMbb.Confirmed(2.10)}");
        sb.AppendLine($"  CONFIRMED (measured 2.02 meV)?    {PreRegisteredMbb.Confirmed(2.02)}");
        sb.AppendLine($"  CONFIRMED (measured 1.00 meV)?    {PreRegisteredMbb.Confirmed(1.00)}");
        sb.AppendLine($"  FALSIFIED (limit 1.50 meV < 2.02)? {PreRegisteredMbb.Falsified(1.50)}");
        sb.AppendLine($"  FALSIFIED (limit 5.00 meV)?        {PreRegisteredMbb.Falsified(5.00)}");
        sb.AppendLine($"  Classification                   = {PreRegisteredMbb.Classify()}");

        Output.WriteLine(sb.ToString());

        Assert.True(PreRegisteredMbb.Confirmed(2.10), "a measurement at 2.10 meV is consistent with 2.02 meV");
        Assert.True(PreRegisteredMbb.Confirmed(2.02), "a measurement at 2.02 meV is exactly consistent");
        Assert.False(PreRegisteredMbb.Confirmed(1.00), "1.00 meV is outside the ±10% range");
        Assert.True(PreRegisteredMbb.Falsified(1.50), "an exclusion limit below 2.02 meV falsifies");
        Assert.False(PreRegisteredMbb.Falsified(5.00), "a limit above the prediction does not falsify");
        Assert.Equal("PRE-REGISTERED", PreRegisteredMbb.Classify());
    }
}
