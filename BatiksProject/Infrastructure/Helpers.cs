namespace BatiksProject.Infrastructure
{
    public static class Helpers
    {
        public static string TrimLongText(string s, int maxLength = 50)
        {
            if (s.Length > maxLength - 3)
            {
                return s.Substring(0, maxLength - 3) + "...";
            }

            return s;
        }
    }
}
