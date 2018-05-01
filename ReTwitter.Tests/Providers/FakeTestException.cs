using System;
using System.Runtime.Serialization;

namespace ReTwitter.Tests.Providers
{
    public class FakeTestException : ArgumentException
    {
        public FakeTestException()
        {
        }

        public FakeTestException(string message) : base(message)
        {
        }

        public FakeTestException(string message, string paramName) : base(message, paramName)
        {
        }

        public FakeTestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FakeTestException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
        {
        }

        protected FakeTestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
