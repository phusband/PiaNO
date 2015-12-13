//
//  Copyright © 2014 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using PiaNO.Serialization;
using System;
using System.IO;

namespace PiaNO
{
    public abstract class PiaFile : PiaNode
    {
        #region Properties

        public PiaHeader Header { get; internal set; }
        public string FileName { get; set; }

        #endregion

        #region Constructors

        protected internal PiaFile() : base() { }
        protected PiaFile(string filePath) : base()
        {
            Read(filePath);
        }

        #endregion

        #region Methods

        public void Read()
        {
            if (string.IsNullOrEmpty(this.FileName))
                throw new ArgumentNullException("FileName");

            Read(this.FileName);
        }
        public void Read(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            if (!File.Exists(fileName))
                throw new FileNotFoundException("Plot file not found", fileName);

            try
            {
                FileName = Path.GetFileName(fileName);
                using (var inStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    PiaNodeSerializer.DeserializeFile(inStream, this);
                    inStream.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Write()
        {
            if (string.IsNullOrEmpty(this.FileName))
                throw new ArgumentNullException("FileName");

            Write(this.FileName);
        }
        public void Write(string fileName)
        {
            try
            {
                using (var outStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    PiaNodeSerializer.Serialize(outStream, this);
                    outStream.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override string ToString()
        {
            return Path.GetFileName(FileName);
        }

        #endregion
    }
}
