namespace AT.Tests.Shared;

/// <summary>
/// Skip-by-default gate for tests that require external FITS data
/// (KMOS3D cubes / COSMOS catalog). The *.fits files are intentionally NOT
/// committed (see Data/FitsData/info.txt). Without the data the catalog
/// pipelines cannot produce meaningful results, so these tests are skipped
/// unless the data is present or the AT_RUN_FITS environment variable is "1".
/// </summary>
public static class FitsDataGate
{
    public const string EnvVar = "AT_RUN_FITS";

    /// <summary>Determines whether the KMOS3D FITS data directory contains cubes.</summary>
    public static bool HasFitsData()
    {
        string fitsDir = LocateDir("Data", "FitsData");
        return Directory.Exists(fitsDir) &&
               Directory.EnumerateFiles(fitsDir, "*.fits").Any();
    }

    /// <summary>Skips the test unless FITS data is present (or AT_RUN_FITS=1).</summary>
    public static void SkipUnlessFitsData()
    {
        bool forced = Environment.GetEnvironmentVariable(EnvVar) == "1";
        Assert.SkipUnless(
            forced || HasFitsData(),
            $"Requires KMOS3D FITS data in Data/FitsData (see info.txt). Set {EnvVar}=1 to force run.");
    }

    private static string LocateDir(params string[] segments)
    {
        string combined = Path.Combine(segments);
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            string candidate = Path.Combine(dir.FullName, combined);
            if (Directory.Exists(candidate)) return candidate;
            dir = dir.Parent;
        }
        return Path.Combine(@"D:\Coding\Test\AT", combined);
    }
}
