namespace restingapi.Models;

class MyStonksViewModel
{
    public BusinessContainer Businesses { get; set; }
    public UserHoldings UserHoldings { get; set; }
    public string GetStockValue(string businessName)
    {
        var business = Businesses.GetBusiness(businessName);
        var valueCents = business.StockValues.Last().Value;
        // return the value in dollars and with two decimal places
        return $"{((decimal)valueCents / 100):0.00}";
    }
    public string GetHoldingsValue(string businessName) {
        var userStonks = UserHoldings.Stonks.Find(s => s.BusinessName == businessName);
        var stonksValue = userStonks.PurchasePriceCents * userStonks.Quantity;
        return $"{((decimal)stonksValue / 100):0.00}";
    }

    public MyStonksViewModel(string username)
    {
        UserHoldings = new UserHoldings(username);
        Businesses = new BusinessContainer();
    }
}
