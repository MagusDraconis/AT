namespace TQM.App.Models;

public sealed record TestSuiteModel(string Name, int Tests, string Result, string Group);

public sealed record TestGroupModel(string Name, IReadOnlyList<TestSuiteModel> Suites);
