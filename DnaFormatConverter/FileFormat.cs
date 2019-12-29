namespace DnaFormatConverter
{
    public enum FileFormat
    {
        MyHeritageV2,
        TwentyThreeAndMeV5
    }

    public static class FileFormatExtensions
    {
        public static string ToFriendlyString(this FileFormat fileFormat)
        {
            switch (fileFormat)
            {
                case FileFormat.MyHeritageV2:
                    return "MyHeritage V2 with GSA chip";
                case FileFormat.TwentyThreeAndMeV5:
                    return "23andme V5 with GSA chip";
                default: return "";
            }
        }
    }
}