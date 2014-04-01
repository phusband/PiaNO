//
//  Copyright © 2014 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using System;

namespace PiaNO
{
    public class PiaHeader
    {
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
    }
}
