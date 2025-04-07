using API.Infrastructure;
using Application;
using Infrastructure;
using Infrastructure.Data;
using NSwag;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument((configure, sp) =>
{
    configure.Title = "Employee API";

    configure.AddSecurity("API KEY", new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        In = OpenApiSecurityApiKeyLocation.Header,
        Name = "X-API-KEY",
        Description = "API Key Authentication"
    });

});
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

await InitialiseDatabaseAsync(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseOpenApi();
app.UseSwaggerUi();
app.UseExceptionHandler(options => { });

app.Run();


static async Task<IServiceScope> InitialiseDatabaseAsync(WebApplication app)
{
    var scope = app.Services.CreateScope();

    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

    await initialiser.InitialiseAsync();

    await initialiser.SeedAsync();

    return scope;
}

public partial class Program { }
