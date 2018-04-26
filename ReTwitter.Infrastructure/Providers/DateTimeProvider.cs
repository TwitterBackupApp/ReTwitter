using System;

namespace ReTwitter.Infrastructure.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public virtual DateTime Now => DateTime.Now;
    }
}
