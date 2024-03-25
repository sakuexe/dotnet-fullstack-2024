using System.Text.Json;

namespace restingapi.Models
{
    public class BusinessContainer
    {
        private static string jsonFilename => "businesses.json";
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
            // save the businesses to a json file
            var json = JsonSerializer.Serialize(Businesses);
            File.WriteAllText(jsonFilename, json);
        }

        public BusinessContainer()
        {
            // try reading the businesses information from a json file
            try {
                var json = File.ReadAllText(jsonFilename);
                if (string.IsNullOrWhiteSpace(json)) 
                    throw new JsonException("Empty file");
                Businesses = JsonSerializer.Deserialize<List<Business>>(json) ?? new();
            } catch (FileNotFoundException) {
                Console.WriteLine($"No file {jsonFilename} found, creating new businesses");
                Businesses = new();
            } catch (JsonException e) {
                Console.WriteLine($"Error reading file {jsonFilename}, creating new businesses");
                Console.WriteLine(e.Message);
                Businesses = new();
            } catch (Exception e) {
                Console.WriteLine($"Error reading file {jsonFilename}, creating new businesses");
                Console.WriteLine(e.Message);
                Businesses = new();
            }

            if (Businesses.Count > 0)
                return;

            foreach (var name in BusinessNames)
                Businesses.Add(new Business(name));
        }
    }
}
