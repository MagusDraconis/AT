namespace AT.Book.Domain;

/// <summary>
/// The single abstract contract shared by every theory object: a chapter, definition,
/// theorem, derivation, primitive, boundary, or audit. Everything in AT.Book is a
/// theory object with an id, a layer, a classification, and a dependency list.
/// </summary>
public interface ITheoryObject
{
    string Id { get; }
    string Title { get; }
    string Summary { get; }
    TheoryLayer Layer { get; }
    TheoryClassification Classification { get; }
    IReadOnlyList<string> Dependencies { get; }
}
