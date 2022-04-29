using System.Text;

namespace Service
{
    public static class RandomString
    {
        private static readonly Random _random = new Random();
        public static string GenerateOrderCode(int sz, bool lowerCase = false)
        {
            var builder = new StringBuilder(sz);
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  
            for (var i = 0; i < sz; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
