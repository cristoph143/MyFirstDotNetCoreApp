using ServiceContracts.DTO;
using Services;

namespace CRUDTests;

public class CreateSellOrderServiceTest
{
  private readonly IStocksService _stocksService = new StocksService();

  #region CreateSellOrder
  [Fact]
  public void CreateSellOrder_NullSellOrder_ToBeArgumentNullException()
  {
    //Arrange
    SellOrderRequest? sellOrderRequest = null;
    //Act
    Assert.Throws<ArgumentNullException>(() =>
      _stocksService.CreateSellOrder(sellOrderRequest));
  }

  [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
  [InlineData(0)] //passing parameters to the tet method
  public void CreateSellOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint sellOrderQuantity)
  {
    //Arrange
    SellOrderRequest sellOrderRequest = new SellOrderRequest
    {
      StockSymbol = "MSFT",
      StockName = "Microsoft",
      Price = 1,
      Quantity = sellOrderQuantity
    };
    //Act
    Assert.Throws<ArgumentException>(() =>
      _stocksService.CreateSellOrder(sellOrderRequest));
  }


  [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
  [InlineData(100001)] //passing parameters to the tet method
  public void CreateSellOrder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint sellOrderQuantity)
  {
    //Arrange
    SellOrderRequest sellOrderRequest = new SellOrderRequest
    {
      StockSymbol = "MSFT",
      StockName = "Microsoft",
      Price = 1,
      Quantity = sellOrderQuantity
    };
    //Act
    Assert.Throws<ArgumentException>(() =>
      _stocksService.CreateSellOrder(sellOrderRequest));
  }
  [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
  [InlineData(0)] //passing parameters to the tet method
  public void CreateSellOrder_PriceIsLessThanMinimum_ToBeArgumentException(uint sellOrderPrice)
  {
    //Arrange
    SellOrderRequest sellOrderRequest = new SellOrderRequest
    {
      StockSymbol = "MSFT",
      StockName = "Microsoft",
      Price = sellOrderPrice,
      Quantity = 1
    };
    //Act
    Assert.Throws<ArgumentException>(() =>
      _stocksService.CreateSellOrder(sellOrderRequest));
  }


  [Theory] //Use [Theory] instead of [Fact]; so that, you can pass parameters to the test method
  [InlineData(10001)] //passing parameters to the tet method
  public void CreateSellOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(uint sellOrderQuantity)
  {
    //Arrange
    SellOrderRequest sellOrderRequest = new SellOrderRequest
    {
      StockSymbol = "MSFT",
      StockName = "Microsoft",
      Price = 1,
      Quantity = sellOrderQuantity
    };
    //Act
    Assert.Throws<ArgumentException>(() =>
      _stocksService.CreateSellOrder(sellOrderRequest));
  }

  [Fact]
  public void CreateSellOrder_StockSymbolIsNull_ToBeArgumentException()
  {
    //Arrange
    SellOrderRequest sellOrderRequest = new SellOrderRequest
    {
      StockSymbol = null,
      Price = 1,
      Quantity = 1
    };

    //Act
    Assert.Throws<ArgumentException>(() =>
      _stocksService.CreateSellOrder(sellOrderRequest));
  }

  [Fact]
  public void CreateSellOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentException()
  {
    //Arrange
    SellOrderRequest sellOrderRequest = new SellOrderRequest
    {
      StockSymbol = "MSFT",
      StockName = "Microsoft",
      DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31"),
      Price = 1,
      Quantity = 1
    };
    //Act
    Assert.Throws<ArgumentException>(() =>
      _stocksService.CreateSellOrder(sellOrderRequest));
  }


  [Fact]
  public void CreateSellOrder_ValidData_ToBeSuccessful()
  {
    //Arrange
    SellOrderRequest sellOrderRequest = new SellOrderRequest
    {
      StockSymbol = "MSFT",
      StockName = "Microsoft",
      DateAndTimeOfOrder = Convert.ToDateTime("2024-12-31"),
      Price = 1,
      Quantity = 1
    };
    //Act
    SellOrderResponse sellOrderResponseFromCreate = _stocksService.CreateSellOrder(sellOrderRequest);
    //Assert
    Assert.NotEqual(Guid.Empty, sellOrderResponseFromCreate.SellOrderID);
  }
  #endregion
}