using AT.Book.Domain;

namespace AT.Book.Data;

/// <summary>
/// The book's table of contents: Preface + six parts, each with ordered chapters.
/// This is structural metadata only — every word a reader sees comes from the
/// localized content files (wwwroot/Content/{culture}/).
/// </summary>
public sealed class ChapterRegistry
{
    /// <summary>The Preface as a standalone chapter (PartId = "preface").</summary>
    public Chapter Preface { get; }

    public IReadOnlyList<Part> Parts { get; }

    /// <summary>Every chapter in reading order (Preface first).</summary>
    public IReadOnlyList<Chapter> Chapters { get; }

    private readonly IReadOnlyDictionary<string, Chapter> _byId;

    public ChapterRegistry()
    {
        Preface = new Chapter("preface", "preface", 0, null, "preface");

        Parts =
        [
            new("foundations", "part.foundations", "part.foundations.subtitle",
            [
                new("difference", "foundations", 1, null, "difference", "images/difference-Web.jpg"),
                new("actualization", "foundations", 2, null, "actualization"),
                new("emergence", "foundations", 3, null, "emergence"),
                new("boundaries", "foundations", 4, null, "boundaries"),
            ]),
            new("structure", "part.structure", "part.structure.subtitle",
            [
                new("d96", "structure", 1, "spectrum", "d96"),
                new("occupancy", "structure", 2, "occupancy", "occupancy"),
                new("resonance", "structure", 3, null, "resonance"),
                new("symmetry", "structure", 4, null, "symmetry"),
            ]),
            new("information", "part.information", "part.information.subtitle",
            [
                new("information-content", "information", 1, null, "information"),
                new("iocc", "information", 2, "iocc", "information"),
                new("kl-selection", "information", 3, null, "information"),
            ]),
            new("cosmology", "part.cosmology", "part.cosmology.subtitle",
            [
                new("omega-lambda", "cosmology", 1, "omegalambda", "cosmology"),
                new("omega-matter", "cosmology", 2, null, "cosmology"),
                new("q0", "cosmology", 3, "deceleration", "cosmology"),
                new("zacc", "cosmology", 4, "acceleration-redshift", "cosmology"),
            ]),
            new("physics", "part.physics", "part.physics.subtitle",
            [
                new("families", "physics", 1, null, "physics"),
                new("masses", "physics", 2, null, "physics"),
                new("couplings", "physics", 3, null, "physics"),
                new("planck-scale", "physics", 4, "planck-scale", "physics"),
            ]),
            new("correspondence", "part.correspondence", "part.correspondence.subtitle",
            [
                new("thermodynamics", "correspondence", 1, null, "correspondence"),
                new("quantum-layer", "correspondence", 2, null, "quantum"),
                new("joint-state", "correspondence", 3, "bell-state", "quantum"),
                new("entangling-gate", "correspondence", 4, "d96-rank", "quantum"),
            ]),
        ];

        Chapters = new[] { Preface }.Concat(Parts.SelectMany(p => p.Chapters)).ToArray();
        _byId = Chapters.ToDictionary(c => c.Id, StringComparer.OrdinalIgnoreCase);
    }

    public Chapter? Get(string? id) => id is null ? null : _byId.GetValueOrDefault(id);

    public Chapter? Next(Chapter c)
    {
        var list = Chapters;
        for (int i = 0; i < list.Count; i++)
            if (string.Equals(list[i].Id, c.Id, StringComparison.OrdinalIgnoreCase))
                return i + 1 < list.Count ? list[i + 1] : null;
        return null;
    }

    public Chapter? Previous(Chapter c)
    {
        var list = Chapters;
        for (int i = 0; i < list.Count; i++)
            if (string.Equals(list[i].Id, c.Id, StringComparison.OrdinalIgnoreCase))
                return i > 0 ? list[i - 1] : null;
        return null;
    }
}
