using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PiaNO.Zip
{
    [Serializable]
    public class ZipException : PiaException
    {
        protected ZipException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public ZipException()
        { }

        public ZipException(string message)
            : base(message) { }

        public ZipException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
