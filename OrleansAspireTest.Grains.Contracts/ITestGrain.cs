namespace OrleansAspireTest.Grains.Contracts;

public interface ITestGrain : IGrainWithStringKey
{
    Task<string> Ping(string name);
}