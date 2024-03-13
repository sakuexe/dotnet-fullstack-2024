using System.ComponentModel;
using System.Runtime.InteropServices;
using helloworld.Models.Utils;

namespace helloworld.Models
{
	public class CustomerModel
	{
		public int CustomerId { get; set; }
		public string BusinessName { get; set; }
		public string Contact { get; set; }
		private static List<string> Businesses
		{
			get => new List<string>()
			{
				"Kotikatu Oy",
				"Rovio Oy",
				"Canonical Ltd",
				"Meta Platforms Inc.",
				"Microsoft Corporation",
			};
		}

		// constructor
		public CustomerModel()
		{
			this.Contact = Personator.CreatePerson().name!;
			this.BusinessName = Businesses[new Random().Next(Businesses.Count - 1)];
		}
	}

	public class WorkOrderModel
	{
		public int WorkId { get; set; }
		public string ContactPerson { get; set; }
		public CustomerModel Customer { get; set; }
		public long StartDate { get; set; }
		public string? StartEstimate { get; set; }
		public long? EndDate { get; set; }
		public string? Description { get; set; }
		public string? DescriptionSecret { get; set; }
		public string Reference { get; set; }
		public string Address { get; set; }
		public int Directors
		{
			get => this.Workers.Where(worker => worker.Title == "director").Count();
		}
		public int Security
		{
			get => this.Workers.Where(worker => worker.Title == "security").Count();
		}
		public int Escorts
		{
			get => this.Workers.Where(worker => worker.Title == "escort").Count();
		}
		public int Tmas
		{
			get => this.Workers.Where(worker => worker.Title == "tma").Count();
		}
		public int Diggers
		{
			get => this.Workers.Where(worker => worker.Title == "digger").Count();
		}
		public int Saws
		{
			get => this.Workers.Where(worker => worker.Title == "saw").Count();
		}
		public int Others
		{
			get => this.Workers.Where(worker => worker.Title == "other").Count();
		}
		public List<Worker> Workers { get; set; }

		// constructor
		public WorkOrderModel()
		{
			var person1 = Personator.CreatePerson();
			var person2 = Personator.CreatePerson();
			this.ContactPerson = person1.name!;
			this.Customer = new CustomerModel();
			this.StartDate = DateTimeOffset.Now.ToUnixTimeSeconds();
			this.Reference = person2.name!;
			this.Address = "Kotikatu 1, 12345 Kotikaupunki";
			this.Workers = new List<Worker>{
				new(),
				new(),
				new(),
				new(),
				new(),
				new(),
				new(),
			};
		}
	}
}
