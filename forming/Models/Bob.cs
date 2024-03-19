namespace forming.Models
{
	public class Bob
	{
		public int Age { get; set; }
		public int WillToLive { get; set; }

		public Bob()
		{
			var rnd = new Random();
			Age = rnd.Next(18, 100);
			WillToLive = rnd.Next(1, 100);
		}
	}
}