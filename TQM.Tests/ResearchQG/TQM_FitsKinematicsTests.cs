using System.Globalization;using System.Text;using TQM.Core.FitsAnalysis;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

public class TQM_FitsKinematicsTests : ResearchTestBase
{
    public TQM_FitsKinematicsTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Fits_Kmos3D_COS4_25850_H_Kinematics()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string path = LocateFitsFile("Data", "FitsData", "COS4_25850_H.fits");
        Assert.True(File.Exists(path), $"FITS file not found: {path}");

        string outDir = Path.Combine(AppContext.BaseDirectory, "kinematics_out");

        var sb = new StringBuilder();
        PrintHeader("Galaxy Kinematics — COS4_25850_H.fits (KMOS3D, z=1.800)");

        KinematicsReport r = FitsKinematicsAnalyzer.Run(path, outDir);

        S(sb, "Section A — H-alpha detection"); sb.AppendLine(r.SA);
        S(sb, "Section B — Velocity-field analysis"); sb.AppendLine(r.SB);
        S(sb, "Section C — Disk-fit quality"); sb.AppendLine(r.SC);
        S(sb, "Section D — Rotation curve"); sb.AppendLine(r.SD);
        S(sb, "Section E — RAR suitability"); sb.AppendLine(r.SE);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  CLASSIFICATION: " + r.Classification);
        sb.AppendLine("  velocity map : " + r.VelocityMapPath);
        sb.AppendLine("  flux map     : " + r.FluxMapPath);
        sb.AppendLine("  rot. curve   : " + r.RotationCurvePath);
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "FITS_COS4_25850_H_Kinematics_Report.txt"), sb.ToString());

        Assert.True(File.Exists(r.VelocityMapPath), "velocity_map.png not written");
        Assert.True(File.Exists(r.FluxMapPath), "flux_map.png not written");
        Assert.True(File.Exists(r.RotationCurvePath), "rotation_curve.csv not written");
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }

    private static string LocateFitsFile(params string[] segments)
    {
        string combined = Path.Combine(segments);
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            string candidate = Path.Combine(dir.FullName, combined);
            if (File.Exists(candidate)) return candidate;
            dir = dir.Parent;
        }
        return Path.Combine(@"D:\Coding\Test\TQM", combined);
    }
}
