# Aspire_Orleans_Client_Repro

This demonstrates a problem with running Orleans clients using Aspire where the `UseOrleansClient()` method tries to configure grain storage because Aspire "injects" `Orleans__GrainStorage__Default` environment variables.

## Notes

### OrleansAspireTest.AppHost

Remove `DOTNET_ASPIRE_CONTAINER_RUNTIME` to use Docker Desktop instead of Podman

### OrleansAspireTest.Client

Add `"USE_ORLEANS_CLIENT_HACK": "sure"` env var to `launchSettings.json` to get around the problem. It will remove the `Orleans__GrainStorage__Default` env vars during startup