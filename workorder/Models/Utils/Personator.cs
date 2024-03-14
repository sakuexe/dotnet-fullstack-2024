namespace workorder.Models.Utils
{
	// class for generating random workers
	public static class Personator
	{
		private static List<string> firstNames = new List<string>()
		{
			"Seppo",
			"Sami",
			"Miku",
			"Tiku",
			"Taku",
						"Petri",
						"Saku",
						"Wais",
						"Aleksi",
						"Mr.",
		};
		private static List<string> lastNames = new List<string>()
		{
			"Hiltunen",
			"Järvinen",
			"Virolainen",
			"Kuittinen",
						"Maxicola",
			"Karttunen",
						"Atifi",
						"Arch Linux btw",
		};

		// A seperate function for creating names
		// since I generated a lot of workers just for their names
		public static string CreateName()
		{
			var rnd = new Random();
			var fname = firstNames[rnd.Next(firstNames.Count)];
			var lname = lastNames[rnd.Next(lastNames.Count)];
			return $"{fname} {lname}";
		}

		public static Worker CreatePerson()
		{
			return new Worker(name: CreateName());
		}
	}
}
