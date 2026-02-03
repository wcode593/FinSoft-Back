namespace SharedContracts.Contract;

public record MovementNotification
(
    string AccountNumber,
    string AccountType,
    string MovementType,
    decimal Amount,
    decimal Balance,
    DateTime Date
);
