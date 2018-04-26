using System;

namespace ReTwitter.Infrastructure.Providers
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
