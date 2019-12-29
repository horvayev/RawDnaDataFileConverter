using CommandLine;

namespace DnaFormatConverterConsole
{
    public class Options
    {
        [Option("input-format", Required = true, HelpText = "Set format of input file. Available options: my_heritage_v2, 23_and_me_v5")]
        public string InputFormat { get; set; }

        [Option("output-format", Required = true, HelpText = "Set format of output file. Available options: my_heritage_v2, 23_and_me_v5. Can not be the same as input-format")]
        public string OutputFormat { get; set; }

        [Option("input", Required = true, HelpText = "Set input file")]
        public string Input { get; set; }
    }
}