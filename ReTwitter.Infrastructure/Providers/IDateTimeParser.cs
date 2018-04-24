using System;

namespace ReTwitter.Infrastructure.Providers
{
    public interface IDateTimeParser
    {
        DateTime ParseFromTwitter(string dateString);
    }
}
