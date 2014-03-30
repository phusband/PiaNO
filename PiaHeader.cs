using System;

namespace PiaNO
{
    public class PiaHeader
    {
        private const string PIA_HEADER_FORMAT = @"PIAFILEVERSION_{0},{1}VER{2},compress\r\npmzlibcodec\255\255\255\255\255\255\255\000\255\255\255\000";
        private string _headerData;

        public double PiaFileVersion { get; private set; }
        public short TypeVersion { get; private set; }
        public PiaType PiaType { get; private set; }

        public PiaHeader(string headerString)
        {
            _headerData = headerString;

            var firstLine = headerString.Split()[0];
            var headerArray = headerString.Split(new char[] { ',', '_'});
            if (headerArray.Length < 4)
                throw new ArgumentOutOfRangeException();

            PiaFileVersion = Double.Parse(headerArray[1]);

            var typeString = headerArray[2].Substring(0, 3);
            PiaType = (PiaType)Enum.Parse(typeof(PiaType), typeString);

            var versionString = headerArray[2].Substring(3).ToUpper().Replace("VER", string.Empty);
            TypeVersion = Int16.Parse(versionString);
        }

        public override string ToString()
        {
            return _headerData;
            //var fileversionString = string.Format("{0:0.0}", Math.Truncate(PiaFileVersion * 10) / 10);
            //return string.Format(PIA_HEADER_FORMAT, fileversionString, PiaType, TypeVersion);
        }

        public byte[] ToByteArray()
        {
            var headerString = this.ToString();
            var bytes = new byte[headerString.Length * sizeof(char)];
            System.Buffer.BlockCopy(headerString.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}
