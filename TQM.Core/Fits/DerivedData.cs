using System;

namespace TQM.Core.FitsAnalysis;

/// <summary>Persists key derived CSVs into Data/derived/ so they survive
/// bin/ cleanup and can be reused across sessions and audits.</summary>
public static class DerivedData
{
    public static string DerivedDir(string fitsDir) =>
        Path.Combine(Path.GetDirectoryName(fitsDir) ?? ".", "derived");

    public static void Persist(string fitsDir, string outDir, params string[] filenames)
    {
        string dir = DerivedDir(fitsDir);
        Directory.CreateDirectory(dir);
        foreach (var f in filenames)
        {
            string src = Path.Combine(outDir, f);
            if (!File.Exists(src)) continue;
            string dst = Path.Combine(dir, f);
            try { File.Copy(src, dst, overwrite: true); }
            catch { /* best-effort: parallel test runs may collide on the same file */ }
        }
    }

    /// <summary>Copies all files from srcDir into Data/derived/subDir.</summary>
    public static void PersistDirectory(string fitsDir, string srcDir, string subDir)
    {
        if (!Directory.Exists(srcDir)) return;
        string dst = Path.Combine(DerivedDir(fitsDir), subDir);
        Directory.CreateDirectory(dst);
        foreach (var f in Directory.GetFiles(srcDir))
        {
            try { File.Copy(f, Path.Combine(dst, Path.GetFileName(f)), overwrite: true); }
            catch { /* best-effort */ }
        }
    }
}
