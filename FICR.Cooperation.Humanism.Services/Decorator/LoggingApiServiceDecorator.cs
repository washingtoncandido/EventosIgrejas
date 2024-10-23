using FICR.Cooperation.Humanism.Services.Contracts;
using FICR.Cooperation.Humanism.Services.Decorator.Base;
using Microsoft.Extensions.Logging;


namespace FICR.Cooperation.Humanism.Services.Decorator
{
    public class LoggingApiServiceDecorator : ApiServiceDecorator
    {
        private readonly ILogger<LoggingApiServiceDecorator> _logger;

        public LoggingApiServiceDecorator(IApiService decoratedService, ILogger<LoggingApiServiceDecorator> logger)
            : base(decoratedService)
        {
            _logger = logger;
        }

        public override async Task CallApiAsync(string endpoint,  object parameters)
        {
            _logger.LogInformation("Calling API. Endpoint: {Endpoint}, Parameters: {Parameters}", endpoint, parameters);

            await _decoratedService.CallApiAsync(endpoint, parameters);

            _logger.LogInformation("API call completed. Endpoint: {Endpoint}", endpoint);
        }
    }
}
