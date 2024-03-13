namespace helloworld.Models
{
    public class MoneyViewModel
    {
        public List<TestPersonModel> Persons { get; set; }
        // if your variable uses other variables, use getters, to make
        // sure that the code remains easy to maintain and does not get
        // saved into the database.
        public int PersonCount
        {
            get
            {
                return Persons.Count;
            }
        }
        public MoneyViewModel(List<TestPersonModel> persons)
        {
            this.Persons = persons;
        }
    }
}
