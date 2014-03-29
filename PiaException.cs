using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PiaNO
{

#if !NETCF_1_0 && !NETCF_2_0
    [Serializable]
#endif
    public class PiaException : ApplicationException
    {
#if !NETCF_1_0 && !NETCF_2_0

        protected PiaException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
#endif

        public PiaException()
        { }

        public PiaException(string message)
            : base(message) { }

        public PiaException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
