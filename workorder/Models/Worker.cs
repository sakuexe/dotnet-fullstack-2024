using workorder.Models.Utils; // import utils, including the Personator.cs file

namespace workorder.Models
{
    public class Worker
    {
        public enum Title
        {
            director,
            security,
            escort,
            tma,
            digger,
            saw,
            other,
        }

        public Title title { get; set; }
        public string name { get; set; }

        // constructor
        public Worker(string? name = null, Title? title = null)
        {
            var rnd = new Random();
            var titles = Enum.GetValues(typeof(Title));
            this.name = name ?? Personator.CreateName();

            if (title != null)
            {
                this.title = (Title)title;
                return;
            }

            var randomTitle = titles.GetValue(rnd.Next(titles.Length));
            this.title = (Title?)randomTitle ?? Title.other;
        }
    }
}
