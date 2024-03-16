namespace workorder.Models.Utils
{
    // class for generating random text
    // used for generating random work order descriptions
    public static class TextGenerator
    {
        private static readonly Random rnd = new Random();
        private static readonly List<string> words = new List<string>
        {
            "",
            "j",
            "",
            "j",
            "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
        };
        public static string GenerateRandomText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
