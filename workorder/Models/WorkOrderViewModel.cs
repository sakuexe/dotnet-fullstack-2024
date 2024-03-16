using workorder.Models.Utils;

namespace workorder.Models
{

    // A work order view model with randomized default values
    public class WorkOrderViewModel
    {
        // random work id, made the number a big one to TRY to avoid duplicates
        public int workId { get => new Random().Next(10, 999999); }
        public int referenceNumber { get => new Random().Next(1000, 99999); }
        public string dpContact { get; set; } = Personator.CreateName();
        public string status { get; set; } = "luonnos";
        public CustomerModel customer { get => new CustomerModel(); }
        public long startDate { get; set; }
        public long endDate { get; set; }
        public string? startTimeEstimate { get; set; }
        public string? description { get; set; }
        public string? hiddenDescription { get; set; }
        public string address { get; set; }
        public List<Worker> workers { get; set; } = new List<Worker>();
        // counts of workers on the job
        public int directors
        {
            get => workers.Where(w => w.title == Worker.Title.director).Count();
        }
        public int security
        {
            get => workers.Where(w => w.title == Worker.Title.security).Count();
        }
        public int escorts
        {
            get => workers.Where(w => w.title == Worker.Title.escort).Count();
        }
        public int tmas
        {
            get => workers.Where(w => w.title == Worker.Title.tma).Count();
        }
        public int diggers
        {
            get => workers.Where(w => w.title == Worker.Title.digger).Count();
        }
        public int saws
        {
            get => workers.Where(w => w.title == Worker.Title.saw).Count();
        }
        public int others
        {
            get => workers.Where(w => w.title == Worker.Title.other).Count();
        }

        private string[] _statusOptions = new string[] { 
            "luonnos", 
            "hylatty",
            "laskutettu",
            "odottaa",
            "kuitattu",
        };

        public struct Props
        {
            public string? startTimeEstimate;
            public string? description;
            public string? hiddenDescription;
        }

        // constructor
        public WorkOrderViewModel(Props props = default)
        {
            var rnd = new Random();
            this.status = _statusOptions[rnd.Next(_statusOptions.Length)];
            this.startDate = DateTimeOffset.Now.ToUnixTimeSeconds();
            this.endDate = DateTimeOffset.Now.AddDays(rnd.Next(10)).ToUnixTimeSeconds();
            this.startTimeEstimate = $"{rnd.Next(6, 24)}:00";
            this.address = $"Tietäjäntie {rnd.Next(255)}, 11{rnd.Next(999)} Riihimäki";
            // add random workers to the work
            foreach (var i in Enumerable.Range(0, rnd.Next(1, 10)))
            {
                this.workers.Add(new Worker());
            }
            // optional parameters
            this.startTimeEstimate = props.startTimeEstimate ?? this.startTimeEstimate;
            this.description = props.description;
            this.hiddenDescription = props.hiddenDescription;
        }
    }
}
