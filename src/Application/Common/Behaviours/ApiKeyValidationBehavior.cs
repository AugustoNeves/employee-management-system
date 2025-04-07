using Application.Common.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Application.Common.Behaviours
{
    public class ApiKeyValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ApiKeyValidationBehavior<TRequest, TResponse>> _logger;
        private readonly string? _apiKey;

        public ApiKeyValidationBehavior(IHttpContextAccessor httpContextAccessor, ILogger<ApiKeyValidationBehavior<TRequest, TResponse>> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiKey = Environment.GetEnvironmentVariable("X-API-KEY");
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var hasApiKeyAttribute = request?.GetType().GetCustomAttribute<RequireApiKeyAttribute>() != null;

            if (hasApiKeyAttribute)
            {
                var context = _httpContextAccessor.HttpContext;
                if (context == null || !context.Request.Headers.TryGetValue("X-API-KEY", out var extractedApiKey) || extractedApiKey != _apiKey)
                {
                    throw new UnauthorizedAccessException("Invalid API Key");
                }
            }

            return await next();
        }
    }
}
