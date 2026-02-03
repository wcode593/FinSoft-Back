using Application.DTOs;
using Polly;
using Polly.Retry;
using RestSharp;
using SharedContracts.Contract;

namespace Application.Commons.Helpers;

public static class MovementApiHelper
{
    private const string localUrl = "http://localhost:5167/api/movements";
    private const string dockerUrl = "http://localhost:5400/api/movements";
    private static readonly string BaseUrl = localUrl ?? dockerUrl;

    private static readonly AsyncRetryPolicy<RestResponse> RetryPolicy = Policy
        .HandleResult<RestResponse>(r => !r.IsSuccessful)
        .WaitAndRetryAsync(5, retry => TimeSpan.FromSeconds(Math.Pow(2, retry)));

    public static async Task SendMovementAsync(MovementNotification movement)
    {
        var client = new RestClient(BaseUrl);

        var request = new RestRequest("/", Method.Post)
            .AddJsonBody(movement);

        var response = await RetryPolicy.ExecuteAsync(() => client.ExecuteAsync(request));

        if (!response.IsSuccessful)
            throw new Exception($"Movement API failed: {response.Content}");
    }
}
