namespace AuthenticationService.Services;

public interface IQueueService
{
    Task SendMessageAsync<T>(T serviceBusMessage, string queueName);
}