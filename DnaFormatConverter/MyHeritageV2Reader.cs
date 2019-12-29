using System;
using System.IO;
using System.Threading.Tasks;
using DnaFormatConverter.Exceptions;

namespace DnaFormatConverter
{
    public class MyHeritageV2Reader : IFormatReader
    {
        private const string FILE_FORMAT = "MyHeritage";
        private const string FORMAT = "MHv1.0";
        private const string CHIP = "GSA";

        private readonly StreamReader _streamReader;
        public bool EndOfFile => _streamReader != null ? _streamReader.EndOfStream : false;

        public MyHeritageV2Reader(StreamReader streamReader)
        {
            if (streamReader == null) throw new ArgumentNullException(nameof(streamReader));
            _streamReader = streamReader;
        }

        public async Task Begin()
        {
            string fileFormat = await _streamReader.ReadLineAsync();
            string format = await _streamReader.ReadLineAsync();
            string chip = await _streamReader.ReadLineAsync();

            if (!fileFormat.Contains(FILE_FORMAT)
                || !format.Contains(FORMAT)
                || !chip.Contains(CHIP)) 
                throw new InvalidFormatException();

            // skip comments
            while (true)
            {
                string line = await _streamReader.ReadLineAsync();
                if (line.StartsWith("RSID")) break;
            }
        }

        public async Task<SNP> ReadNext()
        {
            if (_streamReader.EndOfStream)
            {
                return null;
            }
            string line = await _streamReader.ReadLineAsync();
            return Parse(line);
        }

        private SNP Parse(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new InvalidFormatException();
            }

            string[] parts = line.Split(",");
            if (parts.Length != 4)
            {
                throw new InvalidFormatException();
            }
            string toReplace = "\"";
            SNP genotype = new SNP
            {
                Rsid = parts[0].Replace(toReplace, String.Empty),
                Chromosome = parts[1].Replace(toReplace, String.Empty),
                Position = parts[2].Replace(toReplace, String.Empty),
                Result = parts[3].Replace(toReplace, String.Empty)
            };
            return genotype;
        }
    }
}