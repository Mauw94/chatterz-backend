namespace Chatterz.HUBS.Interfaces;

public interface INotificationHub
{
    Task SendNotification(string message);
}