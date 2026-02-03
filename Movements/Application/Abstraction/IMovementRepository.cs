using Domain.DTOs;

namespace Application.Abstraction;

public interface IMovementRepository
{
    Task<List<MovementCreatedDto>> GetAccountMovementsAsync(string accountNumber, string fechaInicioStr, string fechaFinStr);
    Task<MovementCreatedDto> AddMovementAsync(MovementCreatedDto message);
}
