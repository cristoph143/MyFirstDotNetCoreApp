using ServiceContracts.DTO;
using Services;

namespace CRUDTests;

public class GetBuyOrdersServiceTest
{
    private readonly IStocksService _stocksService = new StocksService();

    #region GetBuyOrders
    // The default buy orders list should be empty
    [Fact]
    public void DefaultList_ShouldBeEmpty()
    {
        // Act
        List<BuyOrderResponse> buyOrdersFromGet = _stocksService.GetBuyOrders();
    
        // Assert
        Assert.Empty(buyOrdersFromGet);
    }
  
    [Fact]
    public void WithFewBuyOrders_ShouldBeSuccessful()
    {
        // Arrange
        // Create a list of buy orders with hard-coded data
        BuyOrderRequest buyOrderRequest1 = new BuyOrderRequest
        {
            StockSymbol = "MSFT",
            StockName = "Microsoft",
            Price = 1,
            Quantity = 1,
            DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00")
        };
    
        BuyOrderRequest buyOrderRequest2 = new BuyOrderRequest
        {
            StockSymbol = "MSFT",
            StockName = "Microsoft",
            Price = 1,
            Quantity = 1,
            DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00")
        };
    
        List<BuyOrderRequest> buyOrderRequests = new List<BuyOrderRequest>
        {
            buyOrderRequest1, buyOrderRequest2
        };
    
        List<BuyOrderResponse> buyOrderResponseListFromAdd = new List<BuyOrderResponse>();
    
        foreach (BuyOrderRequest buyOrderRequest in buyOrderRequests)
        {
            BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(buyOrderRequest);
            buyOrderResponseListFromAdd.Add(buyOrderResponse);
        }
    
        // Act
        List<BuyOrderResponse> buyOrdersListFromGet = _stocksService.GetBuyOrders();
    
        // Assert
        foreach (BuyOrderResponse buyOrderResponseFromAdd in buyOrderResponseListFromAdd)
            Assert.Contains(buyOrderResponseFromAdd, buyOrdersListFromGet);
    }
    #endregion
}