using helloworld.Models.Utils;

namespace helloworld.Models
{
    public class Worker
    {
        static List<string> titles = new List<string>()
        {
            "director",
            "security",
            "escort",
            "tma",
            "digger",
            "saw",
            "other",
        };

        public string Title { get; set; }
        public string Name { get; set; }

        // constructor
        public Worker()
        {
            var person = Personator.CreatePerson();
            this.Title = titles[new Random().Next(titles.Count - 1)];
            this.Name = person.name!;
        }
    }
}
