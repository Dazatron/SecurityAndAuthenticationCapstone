using System.Text.RegularExpressions;
using System.Net;

namespace SecurityAndAuthenticationCapstone.Services
{
    public static class InputSanitizer
    {
        public static string SanitizeUsername(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Remove anything except letters and numbers
            return Regex.Replace(input, "[^a-zA-Z0-9]", "");
        }

        public static string SanitizePassword(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Remove anything except letters and numbers
            return Regex.Replace(input, "[^a-zA-Z0-9]", "");
        }

        public static string SanitizeEmail(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Encode HTML characters to prevent XSS
            return WebUtility.HtmlEncode(input);
        }

        public static string SanitizeRole(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Remove anything except letters and numbers
            return Regex.Replace(input, "[^a-zA-Z0-9]", "");
        }
    }
}