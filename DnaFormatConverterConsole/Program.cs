using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using DnaFormatConverter;

namespace DnaFormatConverterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ParserResult<Options> result = Parser.Default.ParseArguments<Options>(args);
            result.WithParsed(options => Run(options).GetAwaiter().GetResult());
            result.WithNotParsed(errors =>
            {
                Console.Error.WriteLine("Invalid inputs");
            });
        }

        private static async Task<int> Run(Options options)
        {
            FileFormat inputFileFormat, outputFileFormat;
            if (!Enum.TryParse(options.InputFormat, true, out inputFileFormat))
            {
                await Console.Error.WriteLineAsync("Unknown --input-format value");
                return 1;
            }
            if (!Enum.TryParse(options.OutputFormat, true, out outputFileFormat))
            {
                await Console.Error.WriteLineAsync("Unknown --output-format value");
                return 1;
            }

            string outputFilePath = $"output-{DateTime.UtcNow.ToFileTimeUtc()}.txt";

            try
            {
                await DnaFormatConverter.Convert.ConvertToFile(options.Input, outputFilePath, inputFileFormat, outputFileFormat);
            }
            catch (System.Exception ex)
            {
                await Console.Error.WriteLineAsync(ex.Message);
                return 1;
            }
            return 0;
        }
    }
}
