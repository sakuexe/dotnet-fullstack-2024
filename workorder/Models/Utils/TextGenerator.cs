namespace workorder.Models.Utils
{
    // class for generating random text
    // used for generating random work order descriptions
    public static class TextGenerator
    {
        private static readonly Random rnd = new Random();
        public static string filepath = "Models/Utils/kaikkisanat.txt";
        public static string generateWord()
        {
            return File.ReadLines(filepath).Skip(rnd.Next(0, 93086)).Take(1).First();
        }
        public static string GenerateRandomText(int wordCount)
        {
            var text = "";
            /*
             * I benchmarked this and it's faster to read all lines and pick a wordCount amount of lines
             * the original idea was to only read the random lines one by one, not reading all of them
             * you can test this by setting testAlternate to true
             * --- My benchmark results ---
             * 100 calls 100 words each
             * reading all lines: 3ms per call
             * reading a random line one by one: 120ms - 200ms per call
            */
            var lines = File.ReadAllLines(filepath);
            for (int i = 0; i < wordCount; i++)
                text += $"{lines[rnd.Next(0, 93086)]} ";
            return text;
        }
    }
}
