using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ServiceContracts;

namespace StocksApp.Services;

public class FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    : IFinnhubService
{
    public Dictionary<string, object>? GetCompanyProfile(string stockSymbol)
    {
        var endpoint = "stock/profile2";
        return SendHttpRequest(endpoint, stockSymbol).GetAwaiter().GetResult();
    }

    public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
    {
        var endpoint = "quote";
        return await SendHttpRequest(endpoint, stockSymbol);
    }

    private async Task<Dictionary<string, object>?> SendHttpRequest(string endpoint, string stockSymbol)
    {
        using var httpClient = httpClientFactory.CreateClient();
        var token = configuration.GetSection("FinnhubToken").Value;
        var requestUri = $"https://finnhub.io/api/v1/{endpoint}?symbol={stockSymbol}&token={token}";

        var httpRequestMessage = new HttpRequestMessage
        {
            RequestUri = new Uri(requestUri),
            Method = HttpMethod.Get
        };

        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        var response = await httpResponseMessage.Content.ReadAsStringAsync();

        var responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

        if (responseDictionary == null)
            throw new InvalidOperationException("No response from server");

        return responseDictionary.TryGetValue("error", out var value)
            ? throw new InvalidOperationException(Convert.ToString(value))
            : responseDictionary;
    }
}