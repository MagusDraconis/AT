using System.Text;
using AT.Core.ResearchXH;
using AT.Core.ResearchQG;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_027 — Literal Verdict Audit (group G — Gravity Source).
///
/// GOAL. Reject every result path where a classification depends on a literal. Every verdict must be
/// traceable to computed evidence.
///
/// THE RULE ENFORCED. A path of the form
///     literal boolean (verdict-vocabulary name) -> score / classification / verdict function
/// is REFUTED unless it is adjudicated in <see cref="LiteralVerdictAudit.Registry"/> as SAFE (the literal is
/// gone, or it is an honest constant whose name says so) or BOUNDARY (a documented authored judgement with a
/// cited reason).
///
/// FAILS IF: a literal -> verdict path exists without a computation path AND without adjudication.
///
/// WHY A SOURCE SCAN. G_025 and G_026 fixed seven instances of this class by hand — and it recurred four
/// times in the QG ladder alone, because fixing instances does not prevent a class from recurring. This
/// suite therefore re-reads the source at test time, so a NEW literal-to-verdict path fails the build until
/// it is either computed or explicitly adjudicated. That is what makes the rule enforceable rather than
/// merely observed.
///
/// SCOPE. The scan covers AT.Core (the AT-QG / AT-X derivation layer). AT.Tests contains the assertions
/// that carry these verdicts, and AT.App/AT.Book surface them; both are downstream of the AT.Core objects
/// this audit adjudicates.
/// </summary>
public class Y_G_027_Tests : ResearchTestBase
{
    public Y_G_027_Tests(ITestOutputHelper o) : base(o) { }

    /// <summary>The repository's AT.Core source root, discovered from the test assembly location.</summary>
    private static string CoreRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null)
        {
            var candidate = Path.Combine(dir.FullName, "AT.Core");
            if (Directory.Exists(candidate)) return candidate;
            dir = dir.Parent;
        }
        throw new DirectoryNotFoundException("could not locate the AT.Core source root from " + AppContext.BaseDirectory);
    }

    [Fact]
    public void Y_G_027_NoUnTriagedLiteralToVerdictPath()
    {
        // ── THE ENFORCEMENT ──────────────────────────────────────────────────────
        // Fail if a literal -> verdict path exists without a computation path and without adjudication.
        var root = CoreRoot();
        var untriaged = LiteralVerdictAudit.UnTriaged(root);

        var sb = new StringBuilder();
        foreach (var p in untriaged)
            sb.AppendLine($"    {p.File} :: {p.Type}.{p.Field} = {p.Value}");
        if (untriaged.Count > 0) Output.WriteLine("UNTRIAGED literal-to-verdict paths:\n" + sb);

        Assert.True(untriaged.Count == 0,
            $"{untriaged.Count} un-triaged literal-to-verdict path(s) — each must either be COMPUTED or "
            + $"adjudicated in LiteralVerdictAudit.Registry:\n{sb}");
    }

    [Fact]
    public void Y_G_027_RegistryIsCompleteAndBacked()
    {
        var root = CoreRoot();

        // Stale triage is also a failure: an entry that no longer matches any path means the registry has
        // drifted away from the source it claims to describe.
        var stale = LiteralVerdictAudit.StaleEntries(root);
        Assert.True(stale.Count == 0,
            "stale triage entries (no matching path in the source): "
            + string.Join("; ", stale.Select(s => s.Key)));

        // A REFUTED disposition is not a tolerated state — a refuted path is FIXED.
        Assert.True(LiteralVerdictAudit.RefutedEntries.Count == 0,
            "REFUTED paths must be fixed, not registered: "
            + string.Join("; ", LiteralVerdictAudit.RefutedEntries.Select(r => r.Key)));

        // Every disposition must cite a reason — an unbacked judgement is itself the defect.
        var unbacked = LiteralVerdictAudit.UnbackedEntries;
        Assert.True(unbacked.Count == 0,
            "registry entries without a cited reason: " + string.Join("; ", unbacked.Select(u => u.Key)));

        // The scan must actually be finding the paths the registry describes — otherwise a silent scanner
        // failure would make the audit vacuously green.
        var scanned = LiteralVerdictAudit.Scan(root);
        Assert.True(scanned.Count > 0, "the scanner found no paths at all, so the audit is vacuous");
        Assert.Equal(scanned.Count, LiteralVerdictAudit.Registry.Count);
    }

    [Fact]
    public void Y_G_027_GdaggerEvidenceIsComputed()
    {
        // The one REFUTED path this audit found: DaggerOriginAnalyzer's evidence flags, ratio and score were
        // all literals at every construction site, and `Score` feeds the ranking. The Coincidence row was
        // even internally inconsistent — RatioToA0 typed 1.0 while its own numerator was NaN.
        var dir = Path.Combine(Path.GetTempPath(), "g027_gdagger_" + Guid.NewGuid().ToString("N"));
        try
        {
            var report = GdaggerOriginAnalyzer.Run(dir);
            var mechs = report.Mechanisms;

            Assert.Equal(6, mechs.Length);

            // Deriving the flags must REPRODUCE the previously typed values — the fix changes provenance,
            // not the physics.
            var expectedScore = new Dictionary<string, double>
            {
                ["Coincidence"] = 1.5, ["Mach-like"] = 0.5, ["Cosmic-boundary"] = 0.5,
                ["Causal-horizon"] = 0.5, ["Information/holographic"] = 3.0, ["Time-scale (QG-080)"] = 3.0,
            };
            var expectedTwoPi = new Dictionary<string, bool>
            {
                ["Coincidence"] = false, ["Mach-like"] = false, ["Cosmic-boundary"] = false,
                ["Causal-horizon"] = false, ["Information/holographic"] = true, ["Time-scale (QG-080)"] = true,
            };
            var expectedMatches = new Dictionary<string, bool>
            {
                ["Coincidence"] = false, ["Mach-like"] = false, ["Cosmic-boundary"] = false,
                ["Causal-horizon"] = false, ["Information/holographic"] = true, ["Time-scale (QG-080)"] = true,
            };

            foreach (var m in mechs)
            {
                Assert.Equal(expectedScore[m.Name], m.Score, 12);
                Assert.Equal(expectedTwoPi[m.Name], m.HasExactTwoPi);
                Assert.Equal(expectedMatches[m.Name], m.Matches);

                // The ratio must FOLLOW from its own numerator / a0 — never be typed.
                if (double.IsNaN(m.PredictedGdagger))
                    Assert.True(double.IsNaN(m.RatioToA0), $"{m.Name}: NaN predicted must give a NaN ratio");
                else
                    Assert.False(double.IsNaN(m.RatioToA0), $"{m.Name}: a real predicted value must give a real ratio");

                // Matches must follow from the criterion its own Verdict text states (within 15%).
                if (!double.IsNaN(m.RatioToA0))
                    Assert.Equal(Math.Abs(m.RatioToA0 - 1.0) <= 0.15, m.Matches);
            }

            // The Coincidence row's contradiction is gone: it no longer claims a 1.0 ratio with a NaN input.
            var coin = mechs.Single(m => m.Name == "Coincidence");
            Assert.True(double.IsNaN(coin.PredictedGdagger) && double.IsNaN(coin.RatioToA0),
                "Coincidence: a NaN prediction must not be reported as a ratio of 1.0");
        }
        finally
        {
            if (Directory.Exists(dir)) Directory.Delete(dir, true);
        }
    }

    [Fact]
    public void Y_G_027_ScannerDetectsTheKnownDefectShape()
    {
        // The scanner must actually fire on the shape it exists to catch. A synthetic snippet with the
        // G_025/G_026 fingerprint is written to a temp tree and must be found.
        var tmp = Path.Combine(Path.GetTempPath(), "g027_scan_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tmp);
        try
        {
            File.WriteAllText(Path.Combine(tmp, "Synthetic.cs"), """
                namespace Synthetic;
                public sealed record Judge(string Name, bool Holds, string Note);
                public static class SyntheticAudit
                {
                    public static Judge[] Rows() => new[]
                    {
                        new Judge("r1", Holds: true, "typed in"),
                        new Judge("r2", Holds: false, "typed in"),
                    };
                    public static int Score() => Rows().Count(r => r.Holds);
                    public static string Classify() => Score() >= 2 ? "VERIFIED" : "NOT VERIFIED";
                }
                """);

            var found = LiteralVerdictAudit.Scan(tmp);
            Assert.Single(found);
            Assert.Equal("Judge", found[0].Type);
            Assert.Equal("Holds", found[0].Field);

            // ...and it must NOT fire when the flag is computed instead of typed.
            File.WriteAllText(Path.Combine(tmp, "Synthetic.cs"), """
                namespace Synthetic;
                public sealed record Judge(string Name, bool Holds, string Note);
                public static class SyntheticAudit
                {
                    public static bool ComputedHolds(string n) => n.Length > 3;
                    public static Judge[] Rows() => new[]
                    {
                        new Judge("r1", ComputedHolds("r1"), "derived"),
                    };
                    public static int Score() => Rows().Count(r => r.Holds);
                    public static string Classify() => Score() >= 2 ? "VERIFIED" : "NOT VERIFIED";
                }
                """);

            Assert.Empty(LiteralVerdictAudit.Scan(tmp));
        }
        finally
        {
            if (Directory.Exists(tmp)) Directory.Delete(tmp, true);
        }
    }

    [Fact]
    public void Y_G_027_Run()
    {
        var root = CoreRoot();
        var scanned = LiteralVerdictAudit.Scan(root);

        var sb = new StringBuilder();
        PrintHeader("Y_G_027 — Literal Verdict Audit: every verdict traceable to computed evidence");

        sb.AppendLine("THE RULE");
        sb.AppendLine("  A path  literal boolean (verdict name) -> score / classification / verdict function");
        sb.AppendLine("  is REFUTED unless it is COMPUTED or adjudicated SAFE / BOUNDARY with a cited reason.");
        sb.AppendLine("  FAIL IF: literal -> verdict without a computation path.");
        sb.AppendLine($"  verdict vocabulary: {string.Join(", ", LiteralVerdictAudit.VerdictVocabulary.Take(14))} ...");
        sb.AppendLine($"  consumers:          {string.Join(", ", LiteralVerdictAudit.ConsumerVocabulary)}");
        sb.AppendLine();

        PrintHeader("1. THE ADJUDICATED PATHS");
        foreach (var p in scanned)
        {
            var entry = LiteralVerdictAudit.Registry.FirstOrDefault(r => r.Key == p.Key);
            sb.AppendLine($"  {p.File}");
            sb.AppendLine($"      {p.Type}.{p.Field} = {p.Value}   -> {entry?.Disposition.ToString() ?? "UN-TRIAGED"}");
        }
        sb.AppendLine();

        PrintHeader("2. INVENTORY");
        foreach (var (d, n) in LiteralVerdictAudit.Inventory()) sb.AppendLine($"  {d,-9} {n}");
        sb.AppendLine();
        sb.AppendLine($"  un-triaged : {LiteralVerdictAudit.UnTriaged(root).Count}  (MUST be 0)");
        sb.AppendLine($"  stale      : {LiteralVerdictAudit.StaleEntries(root).Count}  (MUST be 0)");
        sb.AppendLine($"  refuted    : {LiteralVerdictAudit.RefutedEntries.Count}  (MUST be 0 — refuted paths are FIXED)");
        sb.AppendLine($"  unbacked   : {LiteralVerdictAudit.UnbackedEntries.Count}  (MUST be 0)");
        sb.AppendLine();

        PrintHeader("3. THE FIXED PATH (REFUTED -> SAFE)");
        sb.AppendLine("  AT.Core/ResearchQG/GdaggerOriginAnalyzer.cs");
        sb.AppendLine("    before: every evidence flag, the ratio and the score were literals at each site:");
        sb.AppendLine("              new CouplingMechanism(\"Coincidence\", ..., NaN, false, 1.0, true, false, ..., 1.5)");
        sb.AppendLine("            and RatioToA0 == 1.0 with a NaN numerator was INTERNALLY INCONSISTENT");
        sb.AppendLine("    after : HasExactTwoPi = |predicted - cH0/(2pi)|/(cH0/(2pi)) < 1e-6");
        sb.AppendLine("            RatioToA0     = predicted / a0          (NaN propagates honestly)");
        sb.AppendLine("            Matches       = |RatioToA0 - 1| <= 0.15 (the criterion the Verdict text states)");
        sb.AppendLine("            Score         = 3.0 if exact 2pi and matching; else 1.5 if not falsifiable; else 0.5");
        sb.AppendLine("            Verified to reproduce the previously TYPED values for all six mechanisms.");
        sb.AppendLine();

        PrintHeader("4. VERDICT");
        sb.AppendLine("  SAFE      — the adjudicated paths that are computed, or honest constants whose name says so.");
        sb.AppendLine("  BOUNDARY  — authored judgements and input taxonomies with a cited reason. These are NOT");
        sb.AppendLine("              computations, and the audit requires that they not be PRESENTED as such.");
        sb.AppendLine("  REFUTED   — literal -> verdict with computation available. ZERO remain: the one found");
        sb.AppendLine("              (DaggerOriginAnalyzer) is now computed, and the registry forbids a refuted entry.");
        sb.AppendLine();
        sb.AppendLine("  Enforcement: the scan re-reads the source at test time, so a NEW literal-to-verdict path");
        sb.AppendLine("  fails this suite until it is computed or adjudicated. Fixing instances does not prevent a");
        sb.AppendLine("  class recurring — G_026 found the class four times in the QG ladder alone.");
        Output.WriteLine(sb.ToString());
    }
}
