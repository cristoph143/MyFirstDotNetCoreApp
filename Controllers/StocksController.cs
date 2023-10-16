using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyFirstDotNetCoreApp.Models;
using StocksApp.Services;

namespace MyFirstDotNetCoreApp.Controllers;

[Route("[controller]")]

public class StocksController(FinnhubService finnhubService, IOptions<TradingOptions> tradingOptions)
    : Controller
{
    [Route("/stocks")]
    public async Task<IActionResult> Index()
    {
        tradingOptions.Value.DefaultStockSymbol ??= "MSFT";

        Dictionary<string, object>? responseDictionary = await finnhubService.GetStockPriceQuote(tradingOptions.Value.DefaultStockSymbol);

        Stock stock = new Stock
        {
            StockSymbol = tradingOptions.Value.DefaultStockSymbol,
            CurrentPrice = Convert.ToDouble(responseDictionary["c"].ToString()),
            HighestPrice = Convert.ToDouble(responseDictionary["h"].ToString()),
            LowestPrice = Convert.ToDouble(responseDictionary["l"].ToString()),
            OpenPrice = Convert.ToDouble(responseDictionary["o"].ToString())
        };

        return View(stock);
    }
}