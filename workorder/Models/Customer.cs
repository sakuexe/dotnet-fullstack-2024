using workorder.Models.Utils;

namespace workorder.Models
{
    // A customer class with randomized default values
    public class CustomerModel
    {
        // random customer id, made the number a big one to TRY to avoid duplicates
        public int customerId { get => new Random().Next(10, 999999); }
        public string businessName { get; set; }
        public string contactPerson { get; set; }
        // random business names, to generate a random business name
        private static List<string> _businessNames
        {
            get
            {
                return new List<string>
                {
                    "Kotikatu Oyj",
                    "Rovio Oy",
                    "Canonical Ltd",
                    "Meta Platforms, Inc.",
                    "Taurudesign Oy",
                    "Microsoft Corporation",
                    "Google LLC",
                    "Apple Inc.",
                    "Samsung Electronics Co., Ltd.",
                    "Tencent Holdings Limited",
                    "Sony Group Corporation",
                };
            }
        }
        // constructor
        public CustomerModel()
        {
            this.businessName = _businessNames[new Random().Next(_businessNames.Count)];
            this.contactPerson = Personator.CreateName();
        }

    }
}
