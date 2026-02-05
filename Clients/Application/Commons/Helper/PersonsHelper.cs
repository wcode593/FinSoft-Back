using System.Net;
using System.Text.Json;
using Application.IRepository;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using RestSharp;

namespace Application.Commons.Helper;

public class PersonsHelper : IPersonsHelper
{
    private readonly RestClient _client;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly AsyncCircuitBreakerPolicy _circuitBreaker;

    public PersonsHelper(IConfiguration config)
    {
        var baseUrl = "http://localhost:5205";

        _client = new RestClient(new RestClientOptions(baseUrl)
        {
            Timeout = TimeSpan.FromMinutes(1)
        });

        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry)));

        _circuitBreaker = Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(3, TimeSpan.FromSeconds(3));
    }

    public async Task<bool> CedulaExistsAsync(string cedula)
    {
        if (string.IsNullOrWhiteSpace(cedula)) throw new ArgumentNullException(nameof(cedula));
        return await _retryPolicy.ExecuteAsync(() => _circuitBreaker.ExecuteAsync(() => ExecuteAsync(cedula)));
    }

    private async Task<bool> ExecuteAsync(string cedula)
    {
        var request = new RestRequest($"/api/person/by-identification/{cedula}", Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Persons service error: {response.StatusCode} - {response.Content}");
        }

        if (string.IsNullOrWhiteSpace(response.Content) || response.Content == "null" || response.Content == "{}") return false;
        try
        {
            var person = JsonSerializer.Deserialize<object>(response.Content);
            return person != null;
        }
        catch
        {
            return false;
        }
    }
}
