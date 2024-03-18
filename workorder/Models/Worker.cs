using workorder.Models.Utils; // import utils, including the Personator.cs file

namespace workorder.Models
{
    public class Worker
    {
        public enum Title
        {
            ohjaaja,
            järkkä,
            saatto,
            tma,
            kaivo,
            saha,
            muu,
        }

        public enum Status
        {
            vahvistettu,
            odottaa,
            hylatty,
        }

        public Title title { get; set; }
        public Status status { get; set; }
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
            this.title = (Title?)randomTitle ?? Title.muu;
            // random status
            this.status = (Status)rnd.Next(3);
        }
    }
}
