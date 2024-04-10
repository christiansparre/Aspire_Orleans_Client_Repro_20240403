var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage").RunAsEmulator();

var clusteringTable = storage.AddTables("clustering");
var grainStorage = storage.AddBlobs("grainstate");

var orleans = builder.AddOrleans("my-app")
    .WithClustering(clusteringTable)
    .WithGrainStorage("Default", grainStorage);

builder.AddProject<Projects.OrleansAspireTest_Silo>("silo", launchProfileName: "default")
    .WithReference(orleans)
    .WithReplicas(3);

builder.AddProject<Projects.OrleansAspireTest_Client>("client", launchProfileName: "default")
    .WithReference(orleans);

builder.Build().Run();
