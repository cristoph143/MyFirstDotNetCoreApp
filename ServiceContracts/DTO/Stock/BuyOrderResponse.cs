using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO;

public class BuyOrderResponse
{
  public Guid BuyOrderId { get; set; }
  public string? StockSymbol { get; set; }
  [Required(ErrorMessage = "Stock Name can't be null or empty")]
  public string StockName { get; set; }
  public DateTime DateAndTimeOfOrder { get; set; }
  public uint Quantity { get; set; }
  public double Price { get; set; }
  public double TradeAmount { get; set; }
  public override bool Equals(object? obj)
  {
    if (obj is not BuyOrderResponse response)
      return false;

    const double tolerance = 0.001;

    return 
      BuyOrderId == response.BuyOrderId &&
      StockSymbol == response.StockSymbol &&
      StockName == response.StockName &&
      DateAndTimeOfOrder == response.DateAndTimeOfOrder &&
      Quantity == response.Quantity &&
      Math.Abs(Price - response.Price) < tolerance;
  }
  public override int GetHashCode() => StockSymbol.GetHashCode();
  public override string ToString() => 
    $"Buy Order ID: {BuyOrderId}, " +
    $"Stock Symbol: {StockSymbol}, " +
    $"Stock Name: {StockName}, " +
    $"Date and Time of Buy Order: {DateAndTimeOfOrder:dd MMM yyyy hh:mm ss tt}, " +
    $"Quantity: {Quantity}, Buy Price: {Price}, " +
    $"Trade Amount: {TradeAmount}";
}

public static class BuyOrderExtensions
{
  public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder) =>
    new()
    {
      BuyOrderId = buyOrder.BuyOrderID,
      StockSymbol = buyOrder.StockSymbol,
      StockName = buyOrder.StockName,
      Price = buyOrder.Price,
      DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
      Quantity = buyOrder.Quantity,
      TradeAmount = buyOrder.Price * buyOrder.Quantity
    };
}