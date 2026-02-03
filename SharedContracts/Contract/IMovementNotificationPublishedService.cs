
namespace SharedContracts.Contract;

public interface IMovementNotificationPublishedService
{
    Task SentNotification(
        string AccountNumber,
        string TypeMovement,
        decimal Amount,
        decimal Balance,
        DateTime Date
    );
}
