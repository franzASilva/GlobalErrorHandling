namespace GlobalErrorHandling.API.Endpoints;

public static class RegisterEndpointsExtension
{
    public static void RegisterEndpoints(this WebApplication? app)
    {
        app?.RegisterDummyEndpoints();
    }
}
