using ServiceContracts.DTO;
using Services;

namespace CRUDTests;

public class GetSellOrdersServiceTest
{
  private readonly IStocksService _stocksService = new StocksService();

  #region GetSellOrders
  // The default sell orders list should be empty
  [Fact]
  public void DefaultList_ShouldBeEmpty()
  {
    // Act
    List<SellOrderResponse> sellOrdersFromGet = _stocksService.GetSellOrders();

    // Assert
    Assert.Empty(sellOrdersFromGet);
  }

  [Fact]
  public void WithFewSellOrders_ShouldBeSuccessful()
  {
    // Arrange
    // Create a list of sell orders with hard-coded data
    SellOrderRequest sellOrderRequest1 = new SellOrderRequest
    {
      StockSymbol = "MSFT",
      StockName = "Microsoft",
      Price = 1,
      Quantity = 1,
      DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00")
    };
    SellOrderRequest sellOrderRequest2 = new SellOrderRequest
    {
      StockSymbol = "MSFT",
      StockName = "Microsoft",
      Price = 1,
      Quantity = 1,
      DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00")
    };
    List<SellOrderRequest> sellOrderRequests = new List<SellOrderRequest>
        {
            sellOrderRequest1, sellOrderRequest2
        };
    List<SellOrderResponse> sellOrderResponseListFromAdd = new List<SellOrderResponse>();

    foreach (SellOrderRequest sellOrderRequest in sellOrderRequests)
    {
      SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(sellOrderRequest);
      sellOrderResponseListFromAdd.Add(sellOrderResponse);
    }

    // Act
    List<SellOrderResponse> sellOrdersListFromGet = _stocksService.GetSellOrders();

    // Assert
    foreach (SellOrderResponse sellOrderResponseFromAdd in sellOrderResponseListFromAdd)
      Assert.Contains(sellOrderResponseFromAdd, sellOrdersListFromGet);
  }
  #endregion
}