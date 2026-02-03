using System.Net;
using Application.Accounts.DTOs;
using Polly;
using Polly.Retry;
using RestSharp;

namespace Application.Commons.Helpers;

public static class PersonApiHelper
{
    private static readonly string BaseUrl = "http://localhost:5205/api/person";
    private static readonly AsyncRetryPolicy<RestResponse<PersonRequestDto>> RetryPolicy = Policy
        .HandleResult<RestResponse<PersonRequestDto>>(r => r.StatusCode != HttpStatusCode.OK && r.StatusCode != HttpStatusCode.Created)
        .WaitAndRetryAsync(8, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    public static async Task<PersonRequestDto> GetOrCreatePersonAsync(PersonRequestDto personRequest)
    {
        var client = new RestClient(BaseUrl);

        personRequest.Name = personRequest.Name.Trim();
        personRequest.Identification = personRequest.Identification.Trim();

        var request = new RestRequest("get-create", Method.Post)
            .AddJsonBody(personRequest);

        var response = await RetryPolicy.ExecuteAsync(async () => await client.ExecuteAsync<PersonRequestDto>(request));

        if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            return response.Data!;

        throw new Exception($"No se pudo obtener o crear la persona tras varios intentos: {response.Content}");
    }
}
