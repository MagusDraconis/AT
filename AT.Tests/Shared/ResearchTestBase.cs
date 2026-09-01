
namespace AT.Tests.Shared;

public abstract class ResearchTestBase
{
    protected readonly ITestOutputHelper Output;

    protected ResearchTestBase(
        ITestOutputHelper output)
    {
        Output = output;
    }

    protected void PrintHeader(
        string title)
    {
        Output.WriteLine(new string('=', 100));
        Output.WriteLine(title);
        Output.WriteLine(new string('=', 100));
    }
}
