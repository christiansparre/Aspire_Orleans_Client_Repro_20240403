using System.Diagnostics;
using Orleans.Runtime;
using OrleansAspireTest.Grains.Contracts;

namespace OrleansAspireTest.Grains;

public class TestGrain : IGrainBase, ITestGrain
{
    public TestGrain(IGrainContext grainContext)
    {
        GrainContext = grainContext;
    }

    public Task<string> Ping(string name)
    {
        this.DeactivateOnIdle();
        return Task.FromResult($"Hello {name}, I'm process {Process.GetCurrentProcess().Id} and the time and date is {DateTime.Now:F}");
    }

    public IGrainContext GrainContext { get; }
}