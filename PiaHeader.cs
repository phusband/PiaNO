using System;

namespace PiaNO
{
    public class PiaHeader
    {
        private const string PIA_HEADER_FORMAT = @"PIAFILEVERSION_{0},{1}VER{2},compress\r\npmzlibcodec\255\255\255\255\255\255\255\000\255\255\255\000";
        private string _rawData;

        public double PIAVersion { get; private set; }
        public short SubVersion { get; private set; }
        public PiaType SubType { get; private set; }

        public PiaHeader(string headerString)
        {
            _rawData = headerString;

            var firstLine = headerString.Split()[0];
            var headerArray = headerString.Split(new char[] { ',', '_'});
            if (headerArray.Length < 4)
                throw new ArgumentOutOfRangeException();

            PIAVersion = Double.Parse(headerArray[1]);

            var typeString = headerArray[2].Substring(0, 3);
            SubType = (PiaType)Enum.Parse(typeof(PiaType), typeString);

            var versionString = headerArray[2].Substring(3).ToUpper().Replace("VER", string.Empty);
            SubVersion = Int16.Parse(versionString);
        }

        public override string ToString()
        {
            return string.Format(PIA_HEADER_FORMAT, PIAVersion, SubType, SubVersion);
        }
    }
}
