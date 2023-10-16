using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ServiceContracts;


namespace StocksApp.Services;

public class FinnhubService(
    IHttpClientFactory httpClientFactory, 
    IConfiguration configuration
): IFinnhubService
{

    private async Task<Dictionary<string, object>?> SendHttpRequest(string stockSymbol)
    {
        using HttpClient httpClient = httpClientFactory.CreateClient();
        string token = configuration.GetSection("FinnhubToken").Value;
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage
        {
            RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={token}"),
            Method = HttpMethod.Get
        };

        HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        string response = await httpResponseMessage.Content.ReadAsStringAsync();

        Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

        if (responseDictionary == null)
            throw new InvalidOperationException("No response from server");

        return responseDictionary.TryGetValue("error", out var value)
            ? throw new InvalidOperationException(Convert.ToString(value))
            : responseDictionary;
    }

    public Dictionary<string, object>? GetCompanyProfile(string stockSymbol) => SendHttpRequest(stockSymbol).GetAwaiter().GetResult();

    public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol) => await SendHttpRequest(stockSymbol);
}