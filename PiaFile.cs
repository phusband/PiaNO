
using System;
using System.IO;
using PiaNO.Compression.Streams;
using PiaNO.Serialization;

namespace PiaNO
{
    public abstract class PiaFile : PiaNode
    {
        protected  string _rawData;
        public PiaHeader Header { get; private set; }
           
        public void Read(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            if (!File.Exists(fileName))
                throw new FileNotFoundException("Plot file not found", fileName);

            try
            {
                var piaStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                Read(piaStream);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void Read(Stream stream)
        {
            try
            {
                var headerBytes = new Byte[60];
                stream.Read(headerBytes, 0, headerBytes.Length);
                var headerString = System.Text.Encoding.Default.GetString(headerBytes);
                Header = new PiaHeader(headerString);
                
                using (var zStream = new InflaterInputStream(stream))
                {
                    var sr = new StreamReader(zStream);
                    InnerData = sr.ReadToEnd();
                }

                Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
        }

        public void Write(string fileName)
        {

        }
        public void Write(Stream stream)
        {

        }
    }
}
