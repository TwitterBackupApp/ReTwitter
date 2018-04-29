using System;
using ReTwitter.Infrastructure.Providers;

namespace ReTwitter.Tests.Providers
{
    public class TestDateTimeProvider : DateTimeProvider
    {
        public override DateTime Now => new DateTime(2019, 01, 01, 01, 01, 01);
        public DateTime DeletedOn => new DateTime(2018, 01, 01, 01, 01, 01);
        public DateTime CreatedOn => new DateTime(2016, 01, 01, 01, 01, 01);
        public DateTime ModifiedOn => new DateTime(2017, 01, 01, 01, 01, 01);
    }
}
