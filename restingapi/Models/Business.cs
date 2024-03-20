namespace restingapi.Models
{
    public class Business
    {
        private Random random = new Random();
        public string Name { get; set; }
        public string ShortName => Name.Substring(0, 4).ToUpper();
        public Dictionary<string, int> StockValues { get; set; }
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
            var value = random.Next(100, 1000);
            StockValues.Add(newDate, value);
            if (StockValues.Count > 10)
                StockValues.Remove(StockValues.First().Key);
        }

        public Business(string name, int stockAmount = 10)
        {
            Name = name;
            StockValues = new Dictionary<string, int>();
            for (int i = 0; i < stockAmount; i++)
            {
                var date = DateTime.Now.AddDays(-stockAmount + i);
                var value = random.Next(100, 1000);
                StockValues.Add(date.ToString("yyyy-MM-dd"), value);
            }
        }
    }
}
