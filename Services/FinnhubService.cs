using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ServiceContracts;


namespace StocksApp.Services;

public class FinnhubService(
    IHttpClientFactory httpClientFactory, 
    IConfiguration configuration
): IFinnhubService
{
    public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
    {
        using HttpClient httpClient = httpClientFactory.CreateClient();
        string token = configuration.GetSection("FinnhubToken").Value;
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage
        {
            RequestUri = new Uri(
                $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={token}"
            ),
            Method = HttpMethod.Get
        };

        HttpResponseMessage httpResponseMessage = 
            await httpClient.SendAsync(httpRequestMessage);

        Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();

        StreamReader streamReader = new StreamReader(stream);

        string response = await streamReader.ReadToEndAsync();
        Dictionary<string, object>? responseDictionary = 
            JsonSerializer.Deserialize<Dictionary<string, object>>(response);

        if (responseDictionary == null)
            throw new InvalidOperationException("No response from finnhub server");

        return responseDictionary.TryGetValue("error", out var value)
            ? throw new InvalidOperationException(Convert.ToString(value))
            : responseDictionary;
    }
}