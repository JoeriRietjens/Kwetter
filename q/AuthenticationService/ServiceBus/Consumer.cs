using AuthenticationService.Models;
using MassTransit;

namespace AuthenticationService.Consumers;

public class Consumer : IConsumer<User>
{
    private readonly ILogger<Consumer> _logger;

    public Consumer(ILogger<Consumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<User> context)
    {
        _logger.LogInformation(context.Message.Id, context.Message.Email);
        return Task.CompletedTask;
    }
}