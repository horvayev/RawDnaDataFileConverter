using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DnaFormatConverter.Tests
{
    public class ConvertTests
    {
        [Fact]
        public async Task ConvertToFile_Throws()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await Convert.ConvertToFile(null, "", FileFormat.MyHeritageV2, FileFormat.TwentyThreeAndMeV5));
            await Assert.ThrowsAsync<ArgumentException>(async () => await Convert.ConvertToFile("", null, FileFormat.MyHeritageV2, FileFormat.TwentyThreeAndMeV5));
        }

        [Fact]
        public async Task ConvertStringToFile_Throws()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await Convert.ConvertStringToFile(null, "sample.txt", FileFormat.MyHeritageV2, FileFormat.TwentyThreeAndMeV5));
            await Assert.ThrowsAsync<ArgumentException>(async () => await Convert.ConvertStringToFile("   ", "sample.txt", FileFormat.MyHeritageV2, FileFormat.TwentyThreeAndMeV5));
        }

        [Fact]
        public async Task ConvertToString_Throws()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await Convert.ConvertToString(null, FileFormat.MyHeritageV2, FileFormat.TwentyThreeAndMeV5));
            await Assert.ThrowsAsync<ArgumentException>(async () => await Convert.ConvertToString("   ", FileFormat.MyHeritageV2, FileFormat.TwentyThreeAndMeV5));
        }
    }
}
