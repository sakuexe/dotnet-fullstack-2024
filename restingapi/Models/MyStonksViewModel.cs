namespace restingapi.Models;

class MyStonksViewModel
{
    public BusinessContainer Businesses { get; set; }
    public UserHoldings UserHoldings { get; set; }
    public string GetStockValue(string businessName)
    {
        // get the current stock value of a business
        var business = Businesses.GetBusiness(businessName);
        var valueCents = business.StockValues.Last().Value;
        // return the value in dollars and with two decimal places
        return $"{((decimal)valueCents / 100):0.00}";
    }
    public string GetPurchaseValue(string businessName)
    {
        // get the purchase value of user's stocks of one business
        var userStonks = UserHoldings.Stonks.Find(s => s.BusinessName == businessName);
        var stonksValue = userStonks.PurchasePriceCents * userStonks.Quantity;
        return $"{((decimal)stonksValue / 100):0.00}";
    }
    public string GetCurrentHoldingValue(string businessName)
    {
        // get the current value of user's stocks of one business
        var currentStockValue = Businesses.GetBusiness(businessName).StockValues.Last().Value;
        var userStocksQuantity = UserHoldings.Stonks.Find(s => s.BusinessName == businessName).Quantity;
        var stonksValue = currentStockValue * userStocksQuantity;
        return $"{((decimal)stonksValue / 100):0.00}";
    }
    public string GetTotal()
    {
        // get the total value of user's stocks
        var total = UserHoldings.Stonks.Sum(s => s.PurchasePriceCents * s.Quantity);
        return $"{((decimal)total / 100):0.00}";
    }
    public string GetStockDelta(string businessName)
    {
        // get the user's growth/loss percentage of one business's stocks
        var business = Businesses.GetBusiness(businessName);
        var currentStockValue = business.StockValues.Last().Value;
        var userStonksValue = UserHoldings.Stonks.Find(s => s.BusinessName == businessName).PurchasePriceCents;
        var percentChange = ((decimal)currentStockValue - (decimal)userStonksValue) / (decimal)userStonksValue * 100;
        var sign = percentChange >= 0 ? "+" : "-";
        return $"{sign} {Math.Abs(percentChange):0.00} %";
    }
    public string GetTotalDelta()
    {
        // get the user's growth/loss percentage of all stocks
        var total = UserHoldings.Stonks.Sum(s => s.PurchasePriceCents * s.Quantity);
        var currentValues = UserHoldings.Stonks.Select(s => Businesses.GetBusiness(s.BusinessName).StockValues.Last().Value * s.Quantity);
        var totalCurrent = currentValues.Sum();
        var percentChange = ((decimal)totalCurrent - (decimal)total) / (decimal)total * 100;
        var sign = percentChange >= 0 ? "+" : "-";
        return $"{sign} {Math.Abs(percentChange):0.00} %";
    }
    public string GetMostStocks()
    {
        // get the business with the most stocks owned by the user
        var mostStocks = UserHoldings.Stonks.OrderByDescending(s => s.Quantity).First();
        return $"{mostStocks.BusinessName} ({mostStocks.Quantity})";
    }

    public MyStonksViewModel(string username)
    {
        UserHoldings = new UserHoldings(username);
        Businesses = new BusinessContainer();
    }
}
