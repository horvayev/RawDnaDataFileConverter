using System;
using System.IO;
using System.Threading.Tasks;
using DnaFormatConverter.Exceptions;

namespace DnaFormatConverter
{
    internal class TwentyThreeAndMeV5Reader : IFormatReader
    {
        private StreamReader _streamReader;

        public bool EndOfFile => _streamReader != null ? _streamReader.EndOfStream : false;

        public TwentyThreeAndMeV5Reader(StreamReader streamReader)
        {
            _streamReader = streamReader;
        }

        public async Task Begin()
        {
            // skip comments
            while (!(await _streamReader.ReadLineAsync()).StartsWith("rsid")) ;
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

            string[] parts = line.Split("\t");
            if (parts.Length != 4)
            {
                throw new InvalidFormatException();
            }
            SNP genotype = new SNP
            {
                Rsid = parts[0],
                Chromosome = parts[1],
                Position = parts[2],
                Result = parts[3]
            };
            return genotype;
        }
    }
}