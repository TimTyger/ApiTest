using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BusinessCaseStudyService.Utilities
{
    public static class Helpers
    {
        public static Task<string> toSystemDate(string datestring)
        {
            CultureInfo cl = CultureInfo.InvariantCulture;
            try
            {
                string outDate;
                DateTime formatedDate;
                if (DateTime.TryParseExact(datestring, "dd/MM/yyyy hh:mm:ss tt", cl, DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "dd/MM/yyyy HH:mm:ss", cl, DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "dd/MM/yyyy HH:mm:ss tt", cl, DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "dd/MM/yyyy", cl, DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "yyyy/MM/dd", cl, DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "yyyy/dd/MM", cl, DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "dd-MM-yyyy hh:mm:ss tt", cl, DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "dd/MM/yyyy h:mm:ss tt", cl, DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "dd/MM/yyyy H:mm:ss tt", cl, DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "dd-MM-yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "dd-MMM-yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "yyyy-MMM-dd", new CultureInfo("en-GB"), DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "d-M-yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "dd-MM-yy", new CultureInfo("en-GB"), DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else if (DateTime.TryParseExact(datestring, "dd-MM-yyyy hh:mm:ss", new CultureInfo("en-GB"), DateTimeStyles.None, out formatedDate))
                {
                    outDate = formatedDate.ToString("dd-MM-yyyy HH:mm:ss");
                }
                else
                {
                    outDate = datestring;
                }
                return Task.FromResult(outDate);
            }
            catch
            {
                return Task.FromResult(datestring);
            }
        }
        public static string ISO_CONVERT(string code)
        {
            var iso2 = "NG";
            var iso3 = "NGA";
            var res = code.Contains(iso2) || code.Contains(iso3);
            return res ? code.Length is 2 ? iso3 : iso2 : code;
        }

        public static Task<string> GenerateRandom(int val)
        {
            string code = string.Empty;
            try
            {
                var bytes = new byte[sizeof(long)];
                var provider = new RNGCryptoServiceProvider();
                provider.GetBytes(bytes);
                long random = BitConverter.ToInt64(bytes, 0);
                code = random.ToString().Replace("-", "").Substring(0, val);
            }
            catch (Exception)
            {
                throw;
            }
            return Task.FromResult(code);
        }
    }

    public static class ConstMessage
    {
        public const string INVALID_REQ_OBJ = "Invalid request object";
        public const string INVALID_CODE = "Invalid process code";
        public const string SUCCESS = "Success!!";
        public const string INCORRECT_ACCESS = "Incorrect Login Details!!";
        public const string FAIL = "Unable to complete request, please try again!!";
    }


}
