namespace restingapi.Models
{
    public class BusinessContainer
    {
        private List<string> BusinessNames = new List<string>
        {
            "Apple",
            "Microsoft",
            "Google",
            "Amazon",
            "Meta",
            "Tesla",
        };
        public List<Business> Businesses { get; set; }

        public Business GetBusiness(string name)
        {
            var business = Businesses.FirstOrDefault(b => b.Name.ToLower() == name.ToLower());
            // if business is null, create a new business with the name
            return business ?? Businesses.First();
        }

        public void UpdateStocks()
        {
            foreach (var business in Businesses)
                business.UpdateStock();
        }

        public BusinessContainer()
        {
            Businesses = new List<Business>();
            foreach (var name in BusinessNames)
                Businesses.Add(new Business(name));
        }
    }
}
