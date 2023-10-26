namespace MyFirstDotNetCoreApp.Models;
using ServiceContracts.DTO;

public class Orders
{
    public List<BuyOrderResponse> BuyOrders { get; set; } = new();
    public List<SellOrderResponse> SellOrders { get; set; } = new(); 
}