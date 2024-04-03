var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage").RunAsEmulator();

var clusteringTable = storage.AddTables("clustering");
var grainStorage = storage.AddBlobs("grainstate");

var orleans = builder.AddOrleans("my-app")
    .WithClustering(clusteringTable)
    .WithGrainStorage("Default", grainStorage);

builder.AddProject<Projects.OrleansAspireTest_Silo>("silo")
    .WithReference(orleans)
    .WithReplicas(3);

builder.AddProject<Projects.OrleansAspireTest_Client>("client")
    .WithReference(orleans);

builder.Build().Run();
