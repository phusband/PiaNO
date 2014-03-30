using System;
using System.IO;
using PiaNO.Zip.Compression;
using PiaNO.Zip.Streams;

namespace PiaNO
{
    public abstract class PiaFile : PiaNode
    {
        #region Properties

        public PiaHeader Header { get; private set; }
        public string FileName { get; set; }

        #endregion

        #region Constructors

        protected internal PiaFile() : base() { }
        protected internal PiaFile(string innerData) : base(innerData) { }

        #endregion

        #region Methods

        public void Read(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            if (!File.Exists(fileName))
                throw new FileNotFoundException("Plot file not found", fileName);

            try
            {
                using (var piaStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    Read(piaStream);
                }
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
                    var sr = new StreamReader(zStream, System.Text.Encoding.Default);
                    InnerData = sr.ReadToEnd();
                }

                Deserialize();
                FileName = stream is FileStream
                    ? ((FileStream)stream).Name
                    : string.Empty;

                _setOwnership(this);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Write(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                Write(fileStream);
            }
        }
        public void Write(Stream stream)
        {
            try
            {
                var headerBytes = System.Text.Encoding.Default.GetBytes(Header.ToString());
                stream.Write(headerBytes, 0, headerBytes.Length);

                var piaBytes = System.Text.Encoding.Default.GetBytes(InnerData.ToString());
                byte[] compressedBytes;
                using (var ms = new MemoryStream())
                {
                    var deflateStream = new DeflaterOutputStream(ms, new Deflater(Deflater.DEFAULT_COMPRESSION));
                    deflateStream.Write(piaBytes, 0, piaBytes.Length);
                    deflateStream.Flush();
                    deflateStream.Finish();

                    compressedBytes = ms.ToArray();
                }

                stream.Write(compressedBytes, 0, compressedBytes.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void _setOwnership(PiaNode node)
        {
            foreach (var child in node.ChildNodes)
            {
                if (child.HasChildNodes)
                    _setOwnership(child);
                else
                    child.Owner = this;
            }
        }

        public override string ToString()
        {
            return Path.GetFileName(FileName);
        }

        #endregion
    }
}
