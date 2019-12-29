using System.IO;

namespace DnaFormatConverter
{
    public class FormatReaderFactory
    {
        private readonly FileFormat _fileFormat;

        public FormatReaderFactory(FileFormat fileFormat)
        {
            this._fileFormat = fileFormat;
        }

        public IFormatReader Create(StreamReader streamReader)
        {
            switch (_fileFormat)
            {
                case FileFormat.MyHeritageV2: return new MyHeritageV2Reader(streamReader);
                case FileFormat.TwentyThreeAndMeV5:
                default: return new TwentyThreeAndMeV5Reader(streamReader);
            }
        }
    }
}