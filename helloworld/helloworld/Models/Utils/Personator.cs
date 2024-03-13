namespace helloworld.Models.Utils
{
	public static class Personator
	{
		private static List<string> firstNames = new List<string>()
		{
			"Seppo",
			"Sami",
			"Miku",
			"TIku",
			"Taku"
		};
		private static List<string> lastNames = new List<string>()
		{
			"Hiltunen",
			"Järvinen",
			"Virolainen",
			"Latvialainen",
			"Karttunen",
		};
		private static List<string> descriptions = new List<string>()
		{
			"Vanha ukko",
			"Hupiukko",
			"Semmonen",
			"Legend",
			"Ihte",
		};

		public static TestPersonModel CreatePerson()
		{
			var rnd = new Random();
			var fname = firstNames[rnd.Next(firstNames.Count - 1)];
			var lname = lastNames[rnd.Next(lastNames.Count - 1)];
			var description = descriptions[rnd.Next(descriptions.Count - 1)];

			return new TestPersonModel()
			{
				name = $"{fname} {lname}",
				description = description,
				age = rnd.Next(10, 80),
			};
		}
	}
}
