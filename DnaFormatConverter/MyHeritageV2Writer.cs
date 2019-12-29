using System;
using System.IO;
using System.Threading.Tasks;

namespace DnaFormatConverter
{
    public class MyHeritageV2Writer : IFormatWriter
    {
        private static readonly string HEADER_TEMPLATE = @"##fileformat=MyHeritage\r\n##format=MHv1.0\r\n##timestamp={0}\r\n#\r\n# FILE GENERATED FROM {1} USING RAWDNADATAFORMATCONVERTER.\r\nRSID,CHROMOSOME,POSITION,RESULT\r\n";
        
        private StreamWriter _streamWriter;
        private FileFormat _sourceFormat;

        public MyHeritageV2Writer(StreamWriter streamWriter, FileFormat sourceFormat)
        {
            if (streamWriter == null) throw new ArgumentNullException(nameof(streamWriter));
            
            _streamWriter = streamWriter;   
            _sourceFormat = sourceFormat;
        }

        public async Task Begin()
        {            
            string dateTimeString = DateTime.UtcNow.ToString("ddd MMM dd HH:mm:ss yyyy");
            await _streamWriter.WriteLineAsync(string.Format(HEADER_TEMPLATE, dateTimeString, Enum.GetName(typeof(FileFormat), _sourceFormat.ToFriendlyString())));
        }

        public async Task End()
        {
             if (_streamWriter != null)
            {
                await _streamWriter.WriteLineAsync();
                await _streamWriter.FlushAsync();
            }
        }

        public async Task WriteAllAsync(IFormatReader reader)
        {
            while (!reader.EndOfFile)
            {
                SNP genotype = await reader.ReadNext();
                await WriteNext(genotype);
            }
        }

        public async Task WriteNext(SNP genotype)
        {
            if (genotype == null)
            {
                return;
            }

            string line = SerializeGenotype(genotype);
            await _streamWriter.WriteLineAsync(line);
        }

        private string SerializeGenotype(SNP genotype)
        {
            return $"\"{genotype.Rsid}\",\"{genotype.Chromosome}\",\"{genotype.Position}\",\"{genotype.Result}\"";
        }
    }
}