namespace restingapi.Models
{
    public class Business
    {
        public string Name { get; set; }
        public string ShortName => Name.Substring(0, 4).ToUpper();
        public Dictionary<string, int> StockValues { get; set; }
        private int createStockValue() {
            // creates a new random stock value based on the previous value
            // so that the stock value changes feel more realistic

            // when setting the initial stock value, return a random value
            var random = new Random();
            if (StockValues.Count == 0) return random.Next(1000, 10000);

            var latestValue = StockValues.Last().Value;
            var delta = random.Next(-1000, 1000);
            var newValue = latestValue + delta;

            // make sure that the value cannot go to the negatives
            if (newValue < 10) newValue = 100 + random.Next(0, 1000);
            return newValue;
        }
        public string StockDelta()
        {
            var latest = StockValues.Last().Value;
            var previous = StockValues.ElementAtOrDefault(StockValues.Count - 2).Value;
            var percentage = (latest - previous) / (double)previous * 100;
            var sign = percentage >= 0 ? "+" : "-";
            return $"{sign} {Math.Abs(percentage):0.00}%";
        }
        public void UpdateStock()
        {
            var newestDate = DateTime.Parse(StockValues.Last().Key);
            var newDate = newestDate.AddDays(1).ToString("yyyy-MM-dd");
            var value = createStockValue();
            StockValues.Add(newDate, value);
        }

        public Business(string name, int stockAmount = 7)
        {
            Name = name;
            StockValues = new Dictionary<string, int>();
            for (int i = 0; i < stockAmount; i++)
            {
                var date = DateTime.Now.AddDays(-stockAmount + i);
                var valueCents = createStockValue();
                StockValues.Add(date.ToString("yyyy-MM-dd"), valueCents);
            }
        }
    }
}
