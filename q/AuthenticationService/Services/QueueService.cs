using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace AuthenticationService.Services;

public class QueueService:IQueueService
{
    private readonly IConfiguration _configuration;
    
    public QueueService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMessageAsync<T>(T serviceBusMessage, string queueName)
    {
        var connectionString = _configuration.GetConnectionString("AzureServiceBus");
        
        ServiceBusClient serviceBusClient = new ServiceBusClient(connectionString);
        var sender = serviceBusClient.CreateSender(queueName);

        string messageBody = JsonSerializer.Serialize(serviceBusMessage);
        ServiceBusMessage message = new ServiceBusMessage(messageBody);

        await sender.SendMessageAsync(message);
    }
}