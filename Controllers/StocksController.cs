using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyFirstDotNetCoreApp.Models;
using StocksApp.Services;

namespace MyFirstDotNetCoreApp.Controllers;

[Route("[controller]")]
public class StocksController(
    FinnhubService finnhubService,
    IOptions<TradingOptions> tradingOptions,
    IConfiguration configuration
) : Controller
{
    private readonly TradingOptions _tradingOptions = tradingOptions.Value;

    [Route("/stocks")]
    public async Task<IActionResult> Index()
    {
        tradingOptions.Value.DefaultStockSymbol ??= "MSFT";
        var responseDictionary = await finnhubService.GetStockPriceQuote(tradingOptions.Value.DefaultStockSymbol);
        var stock = new Stock
        {
            StockSymbol = tradingOptions.Value.DefaultStockSymbol,
            CurrentPrice = Convert.ToDouble(responseDictionary["c"].ToString()),
            HighestPrice = Convert.ToDouble(responseDictionary["h"].ToString()),
            LowestPrice = Convert.ToDouble(responseDictionary["l"].ToString()),
            OpenPrice = Convert.ToDouble(responseDictionary["o"].ToString())
        };
        return View(stock);
    }

    [Route("/stock-trade")]
    public async Task<IActionResult> Trade()
    {
        //reset stock symbol if not exists
        tradingOptions.Value.DefaultStockSymbol ??= "MSFT";
        var companyProfileDictionary =
            finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);
        var stockQuoteDictionary =
            await finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);
        var stockTrade = new StockTrade { StockSymbol = _tradingOptions.DefaultStockSymbol };
        if (companyProfileDictionary != null && stockQuoteDictionary != null)
        {
            if (companyProfileDictionary.TryGetValue("ticker", out var value))
                stockTrade.StockSymbol = Convert.ToString(value);
            if (companyProfileDictionary.TryGetValue("name", out var value1))
                stockTrade.StockName = Convert.ToString(value1);
            if (stockQuoteDictionary.TryGetValue("c", out var value2))
                stockTrade.Price = Convert.ToDouble(value2.ToString() ?? "0");
        }


        ViewBag.FinnhubToken = configuration["FinnhubToken"];
        return View(stockTrade);
    }
}