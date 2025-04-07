using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Infrastructure.Data;

namespace Application.FunctionalTests;

[SetUpFixture]
public partial class Testing
{
    private static ITestDatabase _database = null!;
    private static CustomWebApplicationFactory _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static readonly string API_KEY = @"d34sPT8TJwpMSsWx39uISlcRvFQSXdijl7fbysJsPsUalcC6UT5N1VT9bdishV4V";

    [OneTimeSetUp]
    public async Task RunBeforeAnyTests()
    {
        Environment.SetEnvironmentVariable("X-API-KEY", API_KEY);
               _database = await TestDatabaseFactory.CreateAsync();       

        _factory = new CustomWebApplicationFactory(_database.GetConnection());

        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {

        using var scope = _scopeFactory.CreateScope();

        var accessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        // Set up the HTTP context with API key
        var context = new DefaultHttpContext();
        context.Request.Headers["X-API-KEY"] = API_KEY;
        accessor.HttpContext = context;

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var accessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        // Set up the HTTP context with API key
        var context = new DefaultHttpContext();
        context.Request.Headers["X-API-KEY"] = API_KEY;
        accessor.HttpContext = context;

        await mediator.Send(request);
    }

    public static async Task ResetState()
    {
        try
        {
            await _database.ResetAsync();
        }
        catch (Exception)
        {
        }
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    [OneTimeTearDown]
    public async Task RunAfterAnyTests()
    {
        await _database.DisposeAsync();
        await _factory.DisposeAsync();
    }
}
