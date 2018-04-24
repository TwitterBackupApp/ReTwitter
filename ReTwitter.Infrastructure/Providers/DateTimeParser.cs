using System;
using System.Globalization;

namespace ReTwitter.Infrastructure.Providers
{
    public class DateTimeParser : IDateTimeParser
    {
        public DateTime ParseFromTwitter(string dateString)
        {
           var dateTimeParsed = DateTime.ParseExact(dateString, "ddd MMM dd HH:mm:ss K yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

            return dateTimeParsed;
        }
    }
}
