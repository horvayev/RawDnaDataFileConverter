using System;
using System.IO;

namespace DnaFormatConverter
{
    public class FormatWriterFactory
    {
        private readonly FileFormat _fileFormat;
        public FormatWriterFactory(FileFormat fileFormat)
        {        
            _fileFormat = fileFormat;
        }

        public IFormatWriter Create(StreamWriter streamWriter, FileFormat sourceFormat)
        {
            switch (_fileFormat)
            {
                case FileFormat.MyHeritageV2: return new MyHeritageV2Writer(streamWriter, sourceFormat);
                case FileFormat.TwentyThreeAndMeV5: 
                default: return new TwentyThreeAndMeV5Writer(streamWriter, sourceFormat);
            }
        }
    }
}