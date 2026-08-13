using System.Globalization;
using nom.tam.fits;

namespace TQM.Core.FitsAnalysis;

/// <summary>
/// Reads and reports the structure and headers of a FITS file.
/// </summary>
public static class FitsHeaderReport
{
    /// <summary>Extract the per-HDU summary (index, type, name, axes, data type, purpose).</summary>
    public static HduInfo[] BuildHduInfos(BasicHDU[] hdus)
    {
        var list = new List<HduInfo>();
        for (int i = 0; i < hdus.Length; i++)
        {
            BasicHDU hdu = hdus[i];
            Header h = hdu.Header;
            string extName = SafeString(h, "EXTNAME");
            string axes = hdu.Axes != null ? string.Join("x", hdu.Axes) : "n/a";
            string purpose = ClassifyPurpose(i, extName, hdu);
            string dataType = DescribeDataType(hdu);
            string shape = DescribeShape(hdu);
            list.Add(new HduInfo(i, hdu.GetType().Name, extName, axes, hdu.BitPix, dataType, shape, purpose));
        }
        return list.ToArray();
    }

    /// <summary>Dump the primary HDU header into structured key/value/comment entries.</summary>
    public static HeaderEntry[] DumpPrimaryHeader(BasicHDU[] hdus)
    {
        if (hdus.Length == 0) return Array.Empty<HeaderEntry>();
        return DumpHeader(hdus[0].Header);
    }

    /// <summary>Dump any header into structured entries (best-effort).</summary>
    public static HeaderEntry[] DumpHeader(Header h)
    {
        var list = new List<HeaderEntry>();
        try
        {
            for (int i = 0; i < h.NumberOfCards; i++)
            {
                string raw = h.GetCard(i);
                if (string.IsNullOrEmpty(raw)) continue;
                var parsed = ParseCard(raw);
                if (parsed != null) list.Add(parsed);
            }
        }
        catch
        {
            // Fall back to DumpHeader text if card iteration fails.
            try
            {
                var sw = new StringWriter(CultureInfo.InvariantCulture);
                h.DumpHeader(sw);
                foreach (var line in sw.ToString().Split('\n'))
                {
                    var parsed = ParseCard(line);
                    if (parsed != null) list.Add(parsed);
                }
            }
            catch { /* ignore */ }
        }
        return list.ToArray();
    }

    private static HeaderEntry? ParseCard(string raw)
    {
        if (string.IsNullOrEmpty(raw)) return null;
        if (raw.StartsWith("COMMENT", StringComparison.Ordinal))
            return new HeaderEntry("COMMENT", "", raw.Length > 8 ? raw.Substring(8).Trim() : "");
        if (raw.StartsWith("HISTORY", StringComparison.Ordinal))
            return new HeaderEntry("HISTORY", "", raw.Length > 8 ? raw.Substring(8).Trim() : "");
        if (raw.StartsWith("END", StringComparison.Ordinal)) return null;
        // Standard card: KEYWORD = value / comment
        string body = raw;
        int eq = body.IndexOf('=');
        if (eq < 0) return new HeaderEntry(body.Trim(), "", "");
        string key = body.Substring(0, eq).Trim();
        string rest = body.Substring(eq + 1);
        string value;
        string comment = "";
        int slash = rest.IndexOf('/');
        if (slash >= 0)
        {
            value = rest.Substring(0, slash).Trim();
            comment = rest.Substring(slash + 1).Trim();
        }
        else
        {
            value = rest.Trim();
        }
        value = value.Trim('\'').Trim();
        return new HeaderEntry(key, value, comment);
    }

    private static string ClassifyPurpose(int index, string extName, BasicHDU hdu)
    {
        string en = (extName ?? "").ToUpperInvariant();
        bool isImage = hdu is ImageHDU;
        bool isTable = hdu is BinaryTableHDU || hdu is AsciiTableHDU;
        if (index == 0 && isImage && string.IsNullOrEmpty(en)) return "PRIMARY IMAGE (science/calibration)";
        if (en.Contains("ERR") || en.Contains("NOISE") || en.Contains("STAT")) return "NOISE / ERROR cube";
        if (en.Contains("EXP") || en.Contains("EXPTIME")) return "EXPOSURE map";
        if (en.Contains("PSF")) return "PSF image";
        if (en.Contains("VEL")) return "VELOCITY map";
        if (en.Contains("DISP")) return "VELOCITY DISPERSION map";
        if (en.Contains("DATA") || en.Contains("SCI") || en.Contains("FLUX")) return "SCIENCE / FLUX cube";
        if (en.Contains("MASK")) return "MASK / quality";
        if (isImage && index == 0) return "PRIMARY IMAGE (likely science cube)";
        if (isTable) return "TABLE extension";
        return "IMAGE extension";
    }

    private static string DescribeDataType(BasicHDU hdu)
    {
        Data d = hdu.Data;
        if (d is ImageData img)
        {
            object o = img.DataArray;
            if (o == null) return "none";
            return LeafTypeName(o);
        }
        if (d is TableData) return "table";
        return d?.GetType().Name ?? "none";
    }

    private static string LeafTypeName(object node)
    {
        if (node is Array arr)
        {
            if (arr.Length == 0) return arr.GetType().GetElementType()?.Name ?? "unknown";
            return LeafTypeName(arr.GetValue(0)!);
        }
        return node.GetType().Name; // e.g. Single, Double, Int16
    }

    private static string DescribeShape(BasicHDU hdu)
    {
        Data d = hdu.Data;
        if (d is ImageData img && img.DataArray is Array arr)
        {
            return DescribeArrayShape(arr);
        }
        if (d is TableData td) return $"{td.NCols} cols x {td.NRows} rows";
        return "n/a";
    }

    /// <summary>Recursively describe a (possibly jagged) array's dimensions.</summary>
    private static string DescribeArrayShape(Array arr)
    {
        if (arr.Length > 0 && arr.GetValue(0) is Array inner && inner.Rank >= 1)
            return $"{arr.Length}x{DescribeArrayShape(inner)}";
        return arr.Length.ToString();
    }

    public static string SafeString(Header h, string key)
    {
        try { var s = h.GetStringValue(key); return string.IsNullOrWhiteSpace(s) ? "" : s.Trim(); }
        catch { return ""; }
    }

    public static double SafeDouble(Header h, string key, double fallback)
    {
        try { return h.GetDoubleValue(key, fallback); }
        catch { return fallback; }
    }
}
