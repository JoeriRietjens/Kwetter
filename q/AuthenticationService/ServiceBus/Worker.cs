using AuthenticationService.Models;
using MassTransit;

namespace AuthenticationService.Consumers;

public class Worker : BackgroundService
{
    readonly IBus _bus;

    public Worker(IBus bus)
    {
        _bus = bus;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bus.Publish(new User
            {
                Email = "hoi"

            }, stoppingToken);
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}