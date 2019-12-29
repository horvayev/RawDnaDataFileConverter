using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DnaFormatConverter
{
    public static class Converter
    {
        public static async Task Convert(IFormatReader reader, IFormatWriter writer)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            await reader.Begin();
            await writer.Begin();
            await writer.WriteAllAsync(reader);
            await writer.End();
        }
    }
}