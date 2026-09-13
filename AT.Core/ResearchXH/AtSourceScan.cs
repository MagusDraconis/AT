using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// Strips literals and comments from C# source, preserving newlines so line numbers stay aligned.
///
/// WHY THIS EXISTS. The G_027/G_033/G_035 scanners strip with a **per-line** regex, which cannot see a
/// verbatim (`@"..."`), interpolated or raw (`"""..."""`) string that spans lines. This repository builds most
/// of its report text in exactly such blocks, so under a per-line stripper that prose survives and is counted as
/// **executable code** — the opposite of the intended reading, and a direct threat to any scan meant to
/// separate what AT *says* from what AT *computes*. The live example that exposed it: `J_Q = charge current`
/// (ProtoMatterCollectiveAnalyzer) is report prose, yet a per-line strip leaves it looking like code.
///
/// The stripper is stateful and processes the whole file, so literal and comment state carries across lines.
/// A string's *interpolation holes* are stripped along with its text — conservative in the direction of not
/// crediting an expression inside a report string as a computation.
/// </summary>
public static class AtSourceScan
{
    /// <summary>
    /// Replace every literal and comment with spaces, leaving all other characters (and every newline) intact.
    /// </summary>
    public static string StripLiteralsAndComments(string source)
    {
        var sb = new StringBuilder(source.Length);
        int i = 0, n = source.Length;

        while (i < n)
        {
            char c = source[i];

            // ── comments ──
            if (c == '/' && i + 1 < n && source[i + 1] == '/')
            {
                while (i < n && source[i] != '\n') { sb.Append(' '); i++; }
                continue;
            }
            if (c == '/' && i + 1 < n && source[i + 1] == '*')
            {
                sb.Append("  "); i += 2;
                while (i < n && !(source[i] == '*' && i + 1 < n && source[i + 1] == '/'))
                { sb.Append(source[i] == '\n' ? '\n' : ' '); i++; }
                if (i < n) { sb.Append("  "); i += 2; }
                continue;
            }

            // ── verbatim strings: @"...", $@"...", @$"..." (doubled "" is an escaped quote) ──
            bool verbatim = (c == '@' && i + 1 < n && source[i + 1] == '"')
                         || (c == '$' && i + 2 < n && source[i + 1] == '@' && source[i + 2] == '"')
                         || (c == '@' && i + 2 < n && source[i + 1] == '$' && source[i + 2] == '"');
            if (verbatim)
            {
                while (i < n && source[i] != '"') { sb.Append(' '); i++; }   // @ and $ prefixes
                sb.Append(' '); i++;                                        // opening quote
                while (i < n)
                {
                    if (source[i] == '"')
                    {
                        if (i + 1 < n && source[i + 1] == '"') { sb.Append("  "); i += 2; continue; }
                        sb.Append(' '); i++; break;
                    }
                    sb.Append(source[i] == '\n' ? '\n' : ' '); i++;
                }
                continue;
            }

            // ── raw strings: """...""" (and interpolated variants) ──
            if (c == '"' && i + 2 < n && source[i + 1] == '"' && source[i + 2] == '"')
            {
                for (int k = 0; k < 3 && i < n; k++) { sb.Append(' '); i++; }
                while (i < n && !(i + 2 < n && source[i] == '"' && source[i + 1] == '"' && source[i + 2] == '"'))
                { sb.Append(source[i] == '\n' ? '\n' : ' '); i++; }
                for (int k = 0; k < 3 && i < n; k++) { sb.Append(' '); i++; }
                continue;
            }

            // ── ordinary strings and char literals ──
            if (c == '"' || c == '\'')
            {
                char term = c;
                sb.Append(' '); i++;
                while (i < n)
                {
                    if (source[i] == '\\') { sb.Append(' '); i++; if (i < n) { sb.Append(' '); i++; } continue; }
                    if (source[i] == term) { sb.Append(' '); i++; break; }
                    sb.Append(source[i] == '\n' ? '\n' : ' '); i++;
                }
                continue;
            }

            sb.Append(c); i++;
        }
        return sb.ToString();
    }

    /// <summary>Full-file strip, then split — blank lines are dropped (they carry no vocabulary).</summary>
    public static string[] ExecutableLines(string source)
        => StripLiteralsAndComments(source)
            .Split('\n')
            .Where(l => l.Trim().Length > 0)
            .ToArray();
}
