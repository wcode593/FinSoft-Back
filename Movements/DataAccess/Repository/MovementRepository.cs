using System.Globalization;
using Application.Abstraction;
using DataAccess;
using Domain.DTOs;
using Domain.Models.Movements;
using Microsoft.EntityFrameworkCore;

namespace Movements.DataAccess.Repository;

public class MovementRepository : IMovementRepository
{
    private readonly MovementsDbContext _ctx;

    public MovementRepository(MovementsDbContext ctx) => _ctx = ctx;


    public async Task<MovementCreatedDto> AddMovementAsync(MovementCreatedDto message)
    {
        var movement = new Movement
        {
            Id = Guid.NewGuid(),
            AccountNumber = message.AccountNumber,
            AccountType = message.AccountType,
            MovementType = message.MovementType,
            Amount = message.Amount,
            Balance = message.Balance,
            Date = message.Date
        };

        _ctx.Movements.Add(movement);
        await _ctx.SaveChangesAsync();

        return new MovementCreatedDto
        {
            Id = movement.Id,
            AccountNumber = movement.AccountNumber,
            AccountType = movement.AccountType,
            MovementType = movement.MovementType,
            Amount = movement.Amount,
            Balance = movement.Balance,
            Date = movement.Date,
        };
    }


    public async Task<List<MovementCreatedDto>> GetAccountMovementsAsync(
        string accountNumber,
        string fechaInicioStr,
        string fechaFinStr
    )
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("El número de cuenta es obligatorio.", nameof(accountNumber));

        // Método para parsear y convertir a UTC
        DateTime ParseDateUtc(string dateStr, string paramName) =>
            DateTime.TryParseExact(
                dateStr,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dt
            )
            ? DateTime.SpecifyKind(dt, DateTimeKind.Utc)
            : throw new ArgumentException($"Formato de {paramName} inválido. Debe ser 'yyyy-MM-dd'.", paramName);

        var fechaInicio = ParseDateUtc(fechaInicioStr, nameof(fechaInicioStr));
        var fechaFin = ParseDateUtc(fechaFinStr, nameof(fechaFinStr));

        if (fechaFin < fechaInicio)
            throw new InvalidOperationException("La fecha de fin no puede ser anterior a la fecha de inicio.");

        var movimientos = await _ctx.Movements
            .Where(m => m.AccountNumber == accountNumber &&
                        m.Date >= fechaInicio &&
                        m.Date <= fechaFin)
            .OrderBy(m => m.Date)
            .ToListAsync();

        return movimientos.Select(m => new MovementCreatedDto
        {
            Id = m.Id,
            AccountNumber = m.AccountNumber,
            AccountType = m.AccountType,
            MovementType = m.MovementType,
            Amount = m.Amount,
            Balance = m.Balance,
            Date = DateTime.SpecifyKind(m.Date, DateTimeKind.Utc)
        }).ToList();
    }
}
