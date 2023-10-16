﻿namespace ServiceContracts;

public interface IFinnhubService
{
  public Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
  public Dictionary<string, object>? GetCompanyProfile(string stockSymbol);
}
