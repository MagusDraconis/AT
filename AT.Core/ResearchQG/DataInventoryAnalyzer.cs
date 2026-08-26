using System.Globalization;
using nom.tam.fits;
using AT.Core.FitsAnalysis;

namespace AT.Core.ResearchQG;

/// <summary>
/// QG-072: High-z Baryonic Mass Reconstruction Audit. Indexes the Data directory,
/// detects catalogs carrying mass/photometry/redshift columns, and attempts to
/// cross-match KMOS3D targets (by object ID and RA/DEC) against every catalog to
/// determine whether independent baryonic masses exist in the local data folder.
/// </summary>
public static class DataInventoryAnalyzer
{
    // Mass/photometry/redshift column keywords used for catalog detection.
    private static readonly string[] MassKeywords =
    {
        "Mstar", "Mass", "logM", "StellarMass", "BaryonicMass", "SFR",
        "Photometry", "Redshift", "LOGMASS", "L[3.6]", "L36", "MHI",
        "Luminosity", "Ldisk", "Lbul", "SBdisk", "Vgas",
    };

    public static DataInventoryReport Run(string dataDir, string fitsDir, string outDir)
    {
        Directory.CreateDirectory(outDir);

        // 1. File inventory.
        var files = Directory.GetFiles(dataDir, "*", SearchOption.AllDirectories)
            .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
            .Select(f => new FileInventoryEntry(
                Path.GetRelativePath(dataDir, f),
                Path.GetExtension(f).ToLowerInvariant(),
                new FileInfo(f).Length,
                ClassifyType(f)))
            .ToArray();

        string inventoryCsv = Path.Combine(outDir, "FileInventory.csv");
        WriteInventory(inventoryCsv, files);

        // 2/3. Catalog column detection.
        var catalogs = files
            .Where(f => IsCatalog(f.Extension))
            .Select(f => InspectCatalog(Path.Combine(dataDir, f.Filename)))
            .ToArray();

        // 4. KMOS3D targets from FITS primary headers.
        var targets = ReadKmos3DTargets(fitsDir);

        // 5. Cross-match.
        var matches = CrossMatch(targets, dataDir, catalogs);

        // 7. Assessment.
        string assessment = Assess(matches, catalogs);

        return new DataInventoryReport(
            BuildA(files),
            BuildB(catalogs),
            BuildC(matches, targets.Count),
            BuildD(matches, catalogs),
            BuildE(matches, assessment),
            inventoryCsv,
            matches,
            assessment);
    }

    // ---------------------------------------------------------------------
    // KMOS3D targets
    // ---------------------------------------------------------------------

    private static List<(string obj, double ra, double dec)> ReadKmos3DTargets(string fitsDir)
    {
        var targets = new List<(string, double, double)>();
        foreach (string file in Directory.GetFiles(fitsDir, "*.fits"))
        {
            var fits = new Fits(file);
            try
            {
                BasicHDU[] hdus = fits.Read();
                if (hdus.Length == 0) continue;
                Header h = hdus[0].Header;
                string obj = FitsHeaderReport.SafeString(h, "OBJECT");
                if (string.IsNullOrEmpty(obj)) obj = FitsHeaderReport.SafeString(h, "OBJ_TARG");
                double ra = FitsHeaderReport.SafeDouble(h, "RA", double.NaN);
                double dec = FitsHeaderReport.SafeDouble(h, "DEC", double.NaN);
                if (!string.IsNullOrEmpty(obj) && !double.IsNaN(ra) && !double.IsNaN(dec))
                    targets.Add((obj, ra, dec));
            }
            catch { }
            finally { fits.Close(); }
        }
        return targets;
    }

    // ---------------------------------------------------------------------
    // Cross-matching
    // ---------------------------------------------------------------------

    private static MatchResult[] CrossMatch(
        List<(string obj, double ra, double dec)> targets, string dataDir, CatalogInfo[] catalogs)
    {
        var results = new List<MatchResult>();

        foreach (var cat in catalogs)
        {
            // Only catalogs with positional or ID columns can be matched.
            bool hasId = cat.Columns.Any(c => c.ToLowerInvariant() is "objid" or "object" or "name" or "galaxy" or "cid" or "id");
            bool hasRa = cat.Columns.Any(c => c.Equals("ra", StringComparison.OrdinalIgnoreCase) || c.StartsWith("host_ra", StringComparison.OrdinalIgnoreCase));
            bool hasDec = cat.Columns.Any(c => c.Equals("dec", StringComparison.OrdinalIgnoreCase) || c.StartsWith("host_dec", StringComparison.OrdinalIgnoreCase));

            if (hasRa && hasDec)
                results.AddRange(PositionMatch(targets, dataDir, cat));

            if (hasId)
                results.AddRange(IdMatch(targets, dataDir, cat));
        }
        return results.ToArray();
    }

    private static MatchResult[] PositionMatch(
        List<(string obj, double ra, double dec)> targets, string dataDir, CatalogInfo cat)
    {
        var results = new List<MatchResult>();
        string path = Path.Combine(dataDir, cat.Filename);
        var rows = ReadTable(path, cat);

        // Locate RA/DEC and HOST_RA/HOST_DEC columns.
        int iRa = cat.Columns.ToList().FindIndex(c => c.Equals("RA", StringComparison.OrdinalIgnoreCase));
        int iDec = cat.Columns.ToList().FindIndex(c => c.Equals("DEC", StringComparison.OrdinalIgnoreCase));
        int iHostRa = cat.Columns.ToList().FindIndex(c => c.Equals("HOST_RA", StringComparison.OrdinalIgnoreCase));
        int iHostDec = cat.Columns.ToList().FindIndex(c => c.Equals("HOST_DEC", StringComparison.OrdinalIgnoreCase));
        int iMass = cat.Columns.ToList().FindIndex(c => MassKeywords.Any(k => c.Contains(k, StringComparison.OrdinalIgnoreCase)));
        int iZ = cat.Columns.ToList().FindIndex(c => c.Equals("z", StringComparison.OrdinalIgnoreCase) || c.Equals("zcmb", StringComparison.OrdinalIgnoreCase) || c.Equals("redshift", StringComparison.OrdinalIgnoreCase));
        if (iRa < 0 || iDec < 0) return results.ToArray();

        foreach (var (obj, ra, dec) in targets)
        foreach (var row in rows)
        {
            if (row.Length <= Math.Max(iRa, Math.Max(iDec, Math.Max(iHostRa, iHostDec)))) continue;
            string mass = iMass >= 0 && iMass < row.Length ? row[iMass] : "";
            string z = iZ >= 0 && iZ < row.Length ? row[iZ] : "";

            // 1. Host-galaxy position (if available, i.e. not -999).
            bool hit = false;
            double sep = double.NaN;
            if (iHostRa >= 0 && iHostDec >= 0 &&
                TryDeg(row[iHostRa], out double hra) && TryDeg(row[iHostDec], out double hdec) &&
                hra > -90 && hdec > -90)
            {
                sep = Separation(ra, dec, hra, hdec);
                hit = sep <= 3.0;
            }
            // 2. SN position (fallback: the SN lies within its host galaxy).
            if (!hit && TryDeg(row[iRa], out double sra) && TryDeg(row[iDec], out double sdec))
            {
                sep = Separation(ra, dec, sra, sdec);
                hit = sep <= 3.0;
            }
            if (hit)
                results.Add(new MatchResult(cat.Filename, obj, ra, dec, "position", sep, mass, z, row[0]));
        }
        return results.ToArray();
    }

    private static double Separation(double ra1, double dec1, double ra2, double dec2)
    {
        double dRa = (ra1 - ra2) * Math.Cos(dec1 * Math.PI / 180.0);
        double dDec = dec1 - dec2;
        return 3600.0 * Math.Sqrt(dRa * dRa + dDec * dDec);
    }

    private static MatchResult[] IdMatch(
        List<(string obj, double ra, double dec)> targets, string dataDir, CatalogInfo cat)
    {
        var results = new List<MatchResult>();
        string path = Path.Combine(dataDir, cat.Filename);
        var rows = ReadTable(path, cat);
        var targetIds = targets.Select(t => t.obj).ToHashSet(StringComparer.OrdinalIgnoreCase);

        for (int r = 0; r < rows.Length; r++)
        {
            var row = rows[r];
            for (int c = 0; c < row.Length; c++)
            {
                string cell = row[c].Trim();
                if (targetIds.Contains(cell))
                {
                    var t = targets.First(x => x.obj.Equals(cell, StringComparison.OrdinalIgnoreCase));
                    results.Add(new MatchResult(cat.Filename, cell, t.ra, t.dec, "ID", 0, "", "", string.Join(" ", row)));
                }
            }
        }
        return results.ToArray();
    }

    // ---------------------------------------------------------------------
    // Table parsing
    // ---------------------------------------------------------------------

    private static string[][] ReadTable(string path, CatalogInfo cat)
    {
        var lines = File.ReadAllLines(path);
        var rows = new List<string[]>();
        char[]? delim = cat.Extension == ".csv" ? new[] { ',' }
                      : cat.Extension == ".tab" ? new[] { '\t' }
                      : null;   // whitespace for .dat/.txt
        for (int i = 1; i < lines.Length; i++)   // skip header line
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            if (lines[i].TrimStart().StartsWith("#") || lines[i].TrimStart().StartsWith("-")) continue;
            var parts = delim != null
                ? lines[i].Split(delim, StringSplitOptions.TrimEntries)
                : lines[i].Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2) rows.Add(parts);
        }
        return rows.ToArray();
    }

    private static CatalogInfo InspectCatalog(string path)
    {
        string ext = Path.GetExtension(path).ToLowerInvariant();
        string rel = Path.GetFileName(path);
        string[] columns = Array.Empty<string>();
        string firstLine = "";
        try
        {
            var lines = File.ReadLines(path).Take(40).ToArray();
            if (lines.Length > 0) firstLine = lines[0];

            if (ext == ".mrt")
            {
                // VizieR fixed-width table: search the byte description for mass keywords.
                string text = string.Join(" ", lines);
                columns = MassKeywords
                    .Where(k => text.Contains(k, StringComparison.OrdinalIgnoreCase))
                    .ToArray();
                firstLine = lines.Length > 0 ? lines[0] : "";
            }
            else if (lines.Length > 0)
            {
                char[]? delim = ext == ".csv" ? new[] { ',' } : ext == ".tab" ? new[] { '\t' } : null;
                columns = delim != null
                    ? lines[0].Split(delim, StringSplitOptions.TrimEntries)
                    : lines[0].Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
                // Strip comment marker from the header line.
                if (columns.Length > 0)
                    columns[0] = columns[0].TrimStart('#').Trim();
            }
        }
        catch { }

        var massCols = columns.Where(c => MassKeywords.Any(k => c.Contains(k, StringComparison.OrdinalIgnoreCase))).ToArray();
        string massNote = massCols.Length > 0 ? string.Join(", ", massCols) : "(none)";

        return new CatalogInfo(
            rel,
            ext,
            columns,
            massCols,
            massNote,
            firstLine.Length > 100 ? firstLine.Substring(0, 100) : firstLine);
    }

    // ---------------------------------------------------------------------
    // Assessment
    // ---------------------------------------------------------------------

    private static string Assess(MatchResult[] matches, CatalogInfo[] catalogs)
    {
        bool anyMassMatch = matches.Any(m => !string.IsNullOrWhiteSpace(m.MassValue) && m.MassValue != "7" && m.MassValue != "0");
        if (anyMassMatch) return "B = partial mass estimates available";
        return "A = no independent mass data found";
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(FileInventoryEntry[] files)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Total files indexed: {files.Length}");
        foreach (var g in files.GroupBy(f => f.Type).OrderByDescending(g => g.Count()))
            sb.AppendLine($"  {g.Key,-32} {g.Count()}");
        sb.AppendLine();
        sb.AppendLine("  Files (non-FITS):");
        foreach (var f in files.Where(f => f.Type != "FITS cube"))
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "    {0,-52} {1,8:N0} B   {2}", f.Filename, f.SizeBytes, f.Type));
        return sb.ToString();
    }

    private static string BuildB(CatalogInfo[] catalogs)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Candidate catalogs and their mass/photometry/redshift columns:");
        sb.AppendLine();
        foreach (var c in catalogs)
        {
            sb.AppendLine($"  {c.Filename} ({c.Extension})");
            sb.AppendLine($"    columns: {c.FirstLine}");
            sb.AppendLine($"    mass columns detected: {c.MassNote}");
            sb.AppendLine();
        }
        return sb.ToString();
    }

    private static string BuildC(MatchResult[] matches, int nTargets)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"KMOS3D targets indexed: {nTargets}");
        sb.AppendLine($"Cross-matches found: {matches.Length}");
        sb.AppendLine();
        if (matches.Length == 0)
        {
            sb.AppendLine("  No KMOS3D target (COS4_XXXXX) was matched to any catalog entry");
            sb.AppendLine("  by object ID or by RA/DEC within 3 arcsec.");
        }
        else
        {
            foreach (var m in matches)
                sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                    "  {0,-12} <- {1,-30} ({2}, sep {3:F2}\")  mass={4} z={5}",
                    m.Kmos3DObject, m.Catalog, m.Method, m.SeparationArcsec, m.MassValue, m.Z));
        }
        return sb.ToString();
    }

    private static string BuildD(MatchResult[] matches, CatalogInfo[] catalogs)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Independent mass availability:");
        sb.AppendLine();
        sb.AppendLine("  SPARC (MassModels + Rotmod): baryonic masses for LOCAL (z~0) galaxies only.");
        sb.AppendLine("    -> cannot be matched to COSMOS-field KMOS3D targets.");
        sb.AppendLine("  Pantheon+SH0ES: HOST_LOGMASS (SN host stellar masses) + HOST_RA/HOST_DEC.");
        sb.AppendLine("    -> COSMOS-field SNe present; positional match to KMOS3D targets attempted.");
        sb.AppendLine("  Coma cluster (Chandra + v3344): X-ray temperatures/luminosities, no stellar masses.");
        sb.AppendLine("    -> different field (Coma, z~0.02), no KMOS3D overlap.");
        sb.AppendLine();
        sb.AppendLine($"  Matches with a usable stellar-mass estimate: {matches.Count(m => m.MassValue.Length > 0 && m.MassValue != "7")}");
        return sb.ToString();
    }

    private static string BuildE(MatchResult[] matches, string assessment)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("RAR readiness assessment.");
        sb.AppendLine();
        sb.AppendLine($"  CLASSIFICATION: {assessment}");
        sb.AppendLine();
        sb.AppendLine("  Central question: does the local Data folder contain independent");
        sb.AppendLine("  baryonic masses for the KMOS3D galaxies?");
        sb.AppendLine();
        if (matches.Length == 0)
        {
            sb.AppendLine("  ANSWER: No. The folder contains SPARC (local galaxies), Pantheon");
            sb.AppendLine("  (SNe + host masses) and Coma (X-ray) data, but no COSMOS/CANDELS/");
            sb.AppendLine("  UltraVISTA/3D-HST photometric catalog. The BTFR prior remains the");
            sb.AppendLine("  only baryonic-mass route — and it is circular (QG-071).");
            sb.AppendLine("  External photometric catalogs are still required.");
        }
        else
        {
            sb.AppendLine($"  ANSWER: Partial. {matches.Length} positional match(es) give a stellar");
            sb.AppendLine("  mass (Pantheon HOST_LOGMASS) for KMOS3D targets, but gas masses and");
            sb.AppendLine("  full SED masses remain unavailable locally.");
        }
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // Helpers
    // ---------------------------------------------------------------------

    private static void WriteInventory(string path, FileInventoryEntry[] files)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("filename,extension,size,type");
        foreach (var f in files)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "{0},{1},{2},{3}", f.Filename, f.Extension, f.SizeBytes, f.Type));
        File.WriteAllText(path, sb.ToString());
    }

    private static bool IsCatalog(string ext) =>
        ext is ".csv" or ".txt" or ".dat" or ".mrt" or ".tab";

    private static string ClassifyType(string path)
    {
        string ext = Path.GetExtension(path).ToLowerInvariant();
        if (ext == ".fits") return "FITS cube";
        if (ext == ".csv") return "CSV table";
        if (ext == ".mrt") return "VizieR table";
        if (ext == ".tab") return "tab table";
        if (ext == ".dat") return "data table";
        if (ext == ".txt") return "text";
        if (ext == ".zip") return "archive";
        return "other";
    }

    private static bool TryDeg(string s, out double deg)
    {
        deg = double.NaN;
        if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double v))
        {
            deg = v;
            return true;
        }
        // h:m:s / d:m:s parsing.
        var parts = s.Split(':');
        if (parts.Length == 3 &&
            double.TryParse(parts[0], out double a) &&
            double.TryParse(parts[1], out double b) &&
            double.TryParse(parts[2], out double c))
        {
            double sign = s.TrimStart().StartsWith("-") ? -1 : 1;
            deg = sign * (Math.Abs(a) + b / 60.0 + c / 3600.0);
            return true;
        }
        return false;
    }
}

public sealed record FileInventoryEntry(string Filename, string Extension, long SizeBytes, string Type);

public sealed record CatalogInfo(string Filename, string Extension, string[] Columns,
    string[] MassColumns, string MassNote, string FirstLine);

public sealed record MatchResult(string Catalog, string Kmos3DObject, double Ra, double Dec,
    string Method, double SeparationArcsec, string MassValue, string Z, string Row);

public sealed record DataInventoryReport(
    string SA, string SB, string SC, string SD, string SE,
    string InventoryCsvPath,
    MatchResult[] Matches,
    string AssessmentClass);
