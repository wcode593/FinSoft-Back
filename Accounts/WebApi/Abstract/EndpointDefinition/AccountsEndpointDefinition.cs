using Application.Accounts.Commands;
using Application.Accounts.Queries;
using Application.IRepositories;
using MediatR;
using WebApi.Abstract;
using WebApi.DTOs;

namespace MinimalAPI.Abstractions.EndpointDefinitions;

public class AccountEndpointDefinition : IEndpointDefinitionAccount
{
    public void RegisterAccountEndpoints(WebApplication app)
    {
        var url_base = "/api/accounts";
        var movements = app.MapGroup(url_base);
        movements.MapGet("/", GetAccounts).WithName("GetAccounts");
        movements.MapPost("/open", OpenAccount).WithName("OpenAccount");
        movements.MapPost("/deposit", DepositAccount).WithName("DepositAccount");
        movements.MapPost("/withdraw", WithdrawAccount).WithName("WithdrawAccount");
        movements.MapPost("/loan", LoanAccount).WithName("LoanAccount");
        movements.MapPost("/transfer", TransferAccount).WithName("TransferAccount");
        movements.MapGet("/{accountIdOrNumber}", GetAccountByIdOrNumber).WithName("GetAccountByIdOrNumber");
    }

    private static async Task<IResult> GetAccounts(IMediator mediator)
    {
        var command = new GetAllAccountsCommand();
        var result = await mediator.Send(command);
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> OpenAccount(IMediator mediator, OpenAccountRequestDto requestDto)
    {
        var command = new OpenAccountCommand(requestDto.Person, requestDto.AccountType);
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    private static async Task<IResult> DepositAccount(IMediator mediator, DepositRequestDto requestDto)
    {
        var command = new DepositCommand(requestDto.AccountIdOrNumber, requestDto.Amount);
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    private static async Task<IResult> WithdrawAccount(IMediator mediator, WithdrawRequestDto request)
    {
        var command = new WithdrawCommand(request.AccountIdOrNumber, request.Amount);
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    private static async Task<IResult> LoanAccount(IMediator mediator, LoanRequestDto request)
    {
        var command = new LoanCommand(request.AccountId, request.Amount);
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    private static async Task<IResult> TransferAccount(IMediator mediator, TransferRequestDto request)
    {
        var command = new TransferCommand(request.FromAccountIdOrNumber, request.ToAccountIdOrNumber, request.Amount);
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetAccountByIdOrNumber(IAccountRepository repo, string accountIdOrNumber)
    {
        var account = await repo.GetAccountByNumber(accountIdOrNumber);
        return account != null ? Results.Ok(account) : Results.NotFound();
    }


}
