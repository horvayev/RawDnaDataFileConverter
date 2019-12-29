using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DnaFormatConverter
{
    public static class Convert
    {
        public static async Task ConvertToFile(string inputFilePath, string outputFilePath, FileFormat inputFileFormat, FileFormat outputFileFormat)
        {
            ThrowIfNullOrWhiteSpace(inputFilePath, nameof(inputFilePath));
            ThrowIfNullOrWhiteSpace(outputFilePath, nameof(outputFilePath));

            using (StreamReader streamReader = new StreamReader(inputFilePath, Encoding.UTF8))
            using (StreamWriter streamWriter = new StreamWriter(outputFilePath, false, Encoding.UTF8))
            {
                await InternalConvert(streamReader, streamWriter, inputFileFormat, outputFileFormat);
            }
        }

        public static async Task ConvertStringToFile(string input, string outputFilePath, FileFormat inputFormat, FileFormat outputFileFormat)
        {
            ThrowIfNullOrWhiteSpace(input, nameof(input));

            using (StreamReader streamReader = new StreamReader(input))
            using (StreamWriter streamWriter = new StreamWriter(outputFilePath, false, Encoding.UTF8))
            {
                await InternalConvert(streamReader, streamWriter, inputFormat, outputFileFormat);
            }
        }

        public static async Task<string> ConvertToString(string input, FileFormat inputFormat, FileFormat outputFormat)
        {
            ThrowIfNullOrWhiteSpace(input, nameof(input));

            using (StreamReader streamReader = new StreamReader(input))
            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamWriter streamWriter = new StreamWriter(memoryStream))
            {
                await InternalConvert(streamReader, streamWriter, inputFormat, outputFormat);
                return Encoding.ASCII.GetString(memoryStream.ToArray());
            }
        }

        private static async Task InternalConvert(StreamReader streamReader, StreamWriter streamWriter, FileFormat inputFormat, FileFormat outputFormat)
        {
            IFormatReader reader = new FormatReaderFactory(inputFormat).Create(streamReader);
            IFormatWriter writer = new FormatWriterFactory(outputFormat).Create(streamWriter, outputFormat);
            await Converter.Convert(reader, writer);
        }

        private static void ThrowIfNullOrWhiteSpace(string str, string name)
        {
            if (str == null) throw new ArgumentNullException(name);
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentException(name);
        }
    }
}