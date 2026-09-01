using System.Globalization;using System.Text;using AT.Core.FitsAnalysis;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;

public class AT_FitsAnalyzerTests : ResearchTestBase
{
    public AT_FitsAnalyzerTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Fits_Kmos3D_COS4_25850_H_Analysis()
    {
        FitsDataGate.SkipUnlessFitsData();
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string path = LocateFitsFile("Data", "FitsData", "COS4_25850_H.fits");
        Assert.True(File.Exists(path), $"FITS file not found: {path}");

        var sb = new StringBuilder();
        PrintHeader("FITS Analysis — COS4_25850_H.fits (KMOS3D)");
        sb.AppendLine("File: " + path);
        sb.AppendLine("Size: " + new FileInfo(path).Length + " bytes");

        FitsReport r = FitsAnalyzer.Analyze(path);

        S(sb, "Section A — FITS structure"); sb.AppendLine(r.SA);
        S(sb, "Section B — Header summary"); sb.AppendLine(r.SB);
        S(sb, "Section C — Wavelength analysis"); sb.AppendLine(r.SC);
        S(sb, "Section D — Emission line analysis"); sb.AppendLine(r.SD);
        S(sb, "Section E — Kinematic suitability"); sb.AppendLine(r.SE);
        S(sb, "Section F — QG-070 suitability"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  CLASSIFICATION: " + r.Classification);
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "FITS_COS4_25850_H_Report.txt"), sb.ToString());
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }

    private static string LocateFitsFile(params string[] segments)
    {
        // Try relative to the repo root by walking up from the test output dir.
        string combined = Path.Combine(segments);
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            string candidate = Path.Combine(dir.FullName, combined);
            if (File.Exists(candidate)) return candidate;
            dir = dir.Parent;
        }
        // Fallback: absolute path for this machine.
        return Path.Combine(@"D:\Coding\Test\AT", combined);
    }
}
