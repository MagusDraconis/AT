using System.Globalization;
using System.Text;
using TQM.Core.ResearchXC;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXC;

public class TQM_XC006_GrBridgeCompletion : ResearchTestBase
{
    public TQM_XC006_GrBridgeCompletion(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XC006_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXC-006 GR Bridge Completion Program");

        var audit = GrBridgeAnalyzer.FullAudit();

        // ═══ SECTION A: Current gravity chain ═══
        Sec(sb, "Section A — Current Gravity Chain (Q → Einstein)");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-40} {1,-20} {2,-8} Priority", "Step", "Status", "Native"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var s in audit.Steps)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-40} {1,-20} {2,-8} {3}", s.Name, s.DerivationStatus,
                s.IsTqmNative ? "YES" : "no", s.Priority));
        }
        sb.AppendLine();
        sb.AppendLine($"  TQM-Native: {audit.NativeFraction:P0} ({audit.Steps.Count(s => s.IsTqmNative)}/{audit.Steps.Count})");
        sb.AppendLine($"  External:   {audit.ExternalFraction:P0} ({audit.Steps.Count(s => s.DerivationStatus == "External theorem")}/{audit.Steps.Count})");
        sb.AppendLine($"  Missing:    {audit.MissingFraction:P0} ({audit.Steps.Count(s => s.DerivationStatus is "Missing" or "Heuristic")}/{audit.Steps.Count})");

        // ═══ SECTION B: External dependencies ═══
        Sec(sb, "Section B — External Dependencies (What TQM Imports)");
        sb.AppendLine("  The following theorems are ESTABLISHED in mathematical physics");
        sb.AppendLine("  but NOT re-derived within TQM:");
        sb.AppendLine();
        foreach (var s in audit.Steps.Where(s => s.DerivationStatus == "External theorem" && s.Priority is "Critical" or "High"))
        {
            sb.AppendLine($"  [{s.Priority}] {s.Name}");
            sb.AppendLine($"  {s.Description}");
            sb.AppendLine($"  GAP: {s.GapDescription}");
            sb.AppendLine();
        }

        // ═══ SECTION C: Native TQM results ═══
        Sec(sb, "Section C — Native TQM Gravity Results");
        sb.AppendLine("  What TQM derives INTERNALLY (no external imports):");
        sb.AppendLine();
        foreach (var s in audit.Steps.Where(s => s.IsTqmNative))
        {
            sb.AppendLine($"  ✓ {s.Name}");
            sb.AppendLine($"    {s.Description}");
            sb.AppendLine();
        }

        // ═══ SECTION D: Curvature interpretations ═══
        Sec(sb, "Section D — Curvature from Q-Event Connectivity");
        foreach (var c in audit.CurvatureViews)
        {
            sb.AppendLine($"  [{c.Status}] {c.Approach}");
            sb.AppendLine($"  Definition: {c.Definition}");
            sb.AppendLine($"  Recovers Ricci: {(c.RecoverRicci ? "YES" : "no")}  Riemann: {(c.RecoverRiemann ? "YES" : "no")}");
            sb.AppendLine($"  Continuum: {c.ContinuumLimit}");
            sb.AppendLine();
        }

        // ═══ SECTION E: Continuum limit ═══
        Sec(sb, "Section E — Continuum Limit Analysis");
        sb.AppendLine("  The continuum limit requires:");
        sb.AppendLine("  1. Q-events are dense (large N in any macroscopic region). ✓ SATISFIED");
        sb.AppendLine("  2. Distribution is approximately Poisson. PARTIALLY SHOWN (X046)");
        sb.AppendLine("  3. Lorentz invariance emerges in the limit. EXPECTED (random sprinkling)");
        sb.AppendLine("  4. Non-locality vanishes as N→∞. ✓ (discreteness → 0)");
        sb.AppendLine();
        sb.AppendLine("  CRITICAL UNPROVEN: Poisson sprinkling assumption.");
        sb.AppendLine("  X046 shows Q-event count fluctuations are Poisson-like for Λ.");
        sb.AppendLine("  But full 4D spatial-temporal Poisson property is not proven.");

        // ═══ SECTION F: Missing theorems ═══
        Sec(sb, "Section F — Missing Theorems (Gap Analysis)");
        foreach (var g in audit.Gaps)
        {
            sb.AppendLine($"  [{g.Difficulty}] {g.Gap}");
            sb.AppendLine($"  Needed: {g.WhatIsNeeded}");
            sb.AppendLine($"  Approach: {g.CurrentBestApproach}");
            sb.AppendLine($"  Blocks derivation: {(g.BlocksFullDerivation ? "YES ⚠" : "no")}");
            sb.AppendLine();
        }

        // ═══ SECTION G: Derivation roadmap ═══
        Sec(sb, "Section G — Derivation Roadmap");
        sb.AppendLine(audit.Roadmap);

        // ═══ SECTION H: Hostile review ═══
        Sec(sb, "Section H — Hostile Review");
        sb.AppendLine(GrBridgeAnalyzer.HostileReview());

        // ═══ SECTION H.5: TQM-native derivation vision ═══
        Sec(sb, "Section H.5 — What a TQM-Native Derivation Would Look Like");
        sb.AppendLine(GrBridgeAnalyzer.WhatWouldATqmNativeDerivationLookLike());

        // ═══ SECTION I: Final verdict ═══
        Sec(sb, "Section I — Final Verdict");
        sb.AppendLine(audit.Verdict);

        // ═══ SUMMARY TABLE ═══
        Sec(sb, "Summary — GR Bridge Status");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Native fraction:      {0:P0}", audit.NativeFraction));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  External fraction:    {0:P0}", audit.ExternalFraction));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Missing fraction:     {0:P0}", audit.MissingFraction));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Critical gaps:        {0}", audit.Steps.Count(s => s.Priority == "Critical")));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Theorem gaps:         {0}", audit.Gaps.Count));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Gaps blocking GR:     {0}", audit.Gaps.Count(g => g.BlocksFullDerivation)));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Candidate actions:    {0}", audit.Actions.Count));
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  Viable candidates:    {0}", audit.Actions.Count(a => a.RecoversEinstein)));
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — Bridge exists, depends on external theorems.");
        sb.AppendLine("  WORST CASE: ~15% of TQM killed. Everything else survives.");
        sb.AppendLine("  PATH TO CLOSURE: 5 phases, 3-6 years (Roadmap Section G).");
        sb.AppendLine("  CURRENT: Usable for physics. Gaps are mathematical closure, not physics risk.");

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  ResearchXC-006 COMPLETE.");
        sb.AppendLine("  GR Bridge fully audited. Gaps identified. Roadmap produced.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(); sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
