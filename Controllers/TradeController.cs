using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
using MyFirstDotNetCoreApp.Models;
using Services;

namespace MyFirstDotNetCoreApp.Controllers;

[Route("[controller]")]
public class TradeController(IOptions<TradingOptions> tradingOptions,
        IStocksService stocksService,
        IFinnhubService finnhubService,
        IConfiguration configuration)
    : Controller
{
    private readonly TradingOptions _tradingOptions = tradingOptions.Value;

    [Route("[action]")]
    [Route("~/[controller]")]
    public async Task<IActionResult> Index()
    {
        string finnhubToken = configuration["FinnhubToken"] ?? string.Empty;
        ViewBag.FinnhubToken = finnhubToken;

        if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
            _tradingOptions.DefaultStockSymbol = "MSFT";

        var companyProfileDictionary = finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);
        var stockQuoteDictionary = await finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);

        StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };

        if (companyProfileDictionary != null && stockQuoteDictionary != null)
        {
            stockTrade = new StockTrade
            {
                StockSymbol = companyProfileDictionary["ticker"].ToString(),
                StockName = companyProfileDictionary["name"].ToString(),
                Quantity = _tradingOptions.DefaultOrderQuantity ?? 0,
                Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString())
            };
        }

        return View(stockTrade);
    }

    [Route("[action]")]
    [HttpPost]
    public IActionResult BuyOrder(BuyOrderRequest buyOrderRequest)
    {
        buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

        ModelState.Clear();
        TryValidateModel(buyOrderRequest);

        StockTrade stockTrade = new()
        {
            Price = buyOrderRequest.Price,
            StockName = buyOrderRequest.StockName,
            Quantity = buyOrderRequest.Quantity,
            StockSymbol = buyOrderRequest.StockSymbol
        };
        if (ModelState.IsValid)
        {
            var buyOrderResponse = stocksService.CreateBuyOrder(buyOrderRequest);
            return RedirectToAction(nameof(Order), new { buyOrderResponse });
        }

        ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        return View("Index", stockTrade);
    }

    [Route("[action]")]
    [HttpPost]
    public IActionResult SellOrder(SellOrderRequest sellOrderRequest)
    {
        sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

        ModelState.Clear();
        TryValidateModel(sellOrderRequest);

        if (!ModelState.IsValid)
        {
            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            StockTrade stockTrade = new StockTrade()
            {
                StockName = sellOrderRequest.StockName,
                Quantity = sellOrderRequest.Quantity,
                StockSymbol = sellOrderRequest.StockSymbol
            };
            return View("Index", stockTrade);
        }

        SellOrderResponse sellOrderResponse = stocksService.CreateSellOrder(sellOrderRequest);
        return RedirectToAction(nameof(Order));
    }

    [Route("[action]")]
    public IActionResult Order()
    {
        List<BuyOrderResponse> buyOrderResponses = stocksService.GetBuyOrders();
        List<SellOrderResponse> sellOrderResponses = stocksService.GetSellOrders();

        Orders orders = new Orders()
        {
            BuyOrders = buyOrderResponses,
            SellOrders = sellOrderResponses
        };

        ViewBag.TradingOptions = _tradingOptions;
        return View(orders);
    }
}