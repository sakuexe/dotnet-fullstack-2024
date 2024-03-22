namespace restingapi.Models;

public class UserHoldings
{
    public struct Stonk
    {
        public string BusinessName { get; set; }
        public int PurchasePriceCents { get; set; }
        public int Quantity { get; set; }
    }
    public string Username { get; set; }
    public List<Stonk> Stonks { get; set; }
    private void SortStonks(string type = "quantity")
    {
        // sort the list by quantity
        if (type == "quantity")
            Stonks.Sort((a, b) => b.Quantity.CompareTo(a.Quantity));
        // sort the list by business name
        else if (type == "business")
            Stonks.Sort((a, b) => a.BusinessName.CompareTo(b.BusinessName));
    }

    public void AddStonk(string business, int purchasePrice)
    {
        // get holdings with the same business name
        var existingStonk = Stonks.Find(s => s.BusinessName == business);
        // if the business is not found, add a new one
        if (existingStonk.BusinessName == null)
        {
            Stonks.Add(new Stonk { BusinessName = business, PurchasePriceCents = purchasePrice, Quantity = 1});
            SortStonks();
            return;
        }

        // if the business is found, calculate the average purchase price
        var quantity = existingStonk.Quantity + 1;
        var averagePrice = (existingStonk.PurchasePriceCents * existingStonk.Quantity + purchasePrice) / quantity;
        // and replace the existing business with the new average price
        Stonks.Remove(existingStonk);
        Stonks.Add(new Stonk { BusinessName = business, PurchasePriceCents = averagePrice, Quantity = quantity});
        SortStonks();
    }

    public UserHoldings(string username)
    {
        Username = username;
        Stonks = new List<Stonk>();
    }
}
