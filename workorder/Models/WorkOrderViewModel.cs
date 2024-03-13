using workorder.Models.Utils;

namespace workorder.Models
{

    // A work order view model with randomized default values
    public class WorkOrderViewModel
    {
        // random work id, made the number a big one to TRY to avoid duplicates
        public int workId { get => new Random().Next(10, 999999); }
        public int referenceNumber { get => new Random().Next(1000, 99999); }
        public string contactPerson { get; set; } = Personator.CreateName();
        public CustomerModel customer { get => new CustomerModel(); }
        public long startDate { get; set; }
        public long endDate { get; set; }
        public string? startTimeEstimate { get; set; }
        public string? description { get; set; }
        public string? hiddenDescription { get; set; }
        public string address { get; set; }
        public List<Worker> workers { get; set; } = new List<Worker>();
        // counts of workers on the job
        public int _directors
        {
            get => this.workers.Where(w => w.title == Worker.Title.director).Count();
        }
        public int _security
        {
            get => this.workers.Where(w => w.title == Worker.Title.security).Count();
        }
        public int _escorts
        {
            get => this.workers.Where(w => w.title == Worker.Title.escort).Count();
        }
        public int _tmas
        {
            get => this.workers.Where(w => w.title == Worker.Title.tma).Count();
        }
        public int _diggers
        {
            get => this.workers.Where(w => w.title == Worker.Title.digger).Count();
        }
        public int _saws
        {
            get => this.workers.Where(w => w.title == Worker.Title.saw).Count();
        }
        public int _others
        {
            get => this.workers.Where(w => w.title == Worker.Title.other).Count();
        }

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
            this.startDate = DateTimeOffset.Now.ToUnixTimeSeconds();
            this.endDate = DateTimeOffset.Now.AddDays(rnd.Next(10)).ToUnixTimeSeconds();
            this.address = $"Tietäjäntie {rnd.Next(255)}, 11{rnd.Next(999)} Riihimäki";
            // optional parameters
            this.startTimeEstimate = props.startTimeEstimate;
            this.description = props.description;
            this.hiddenDescription = props.hiddenDescription;
        }
    }
}
