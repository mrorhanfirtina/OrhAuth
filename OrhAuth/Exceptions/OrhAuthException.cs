using System.Runtime.Serialization;
using System;

namespace OrhAuth.Exceptions
{
    /// <summary>
    /// Represents an exception specific to the OrhAuth authentication framework.
    /// </summary>
    [Serializable]
    public class OrhAuthException : Exception
    {
        public OrhAuthException() { }

        public OrhAuthException(string message) : base(message) { }

        public OrhAuthException(string message, Exception innerException) : base(message, innerException) { }

        protected OrhAuthException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
