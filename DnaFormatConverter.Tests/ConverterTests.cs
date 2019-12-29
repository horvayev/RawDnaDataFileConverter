using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace DnaFormatConverter.Tests
{
    public class ConverterTests
    {

        [Fact]
        public async Task Convert_ArgumentsValidation()
        {
            Mock<IFormatReader> reader = new Mock<IFormatReader>();
            Mock<IFormatWriter> writer = new Mock<IFormatWriter>();

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await Converter.Convert(null, null));
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await Converter.Convert(reader.Object, null));
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await Converter.Convert(null, writer.Object));
        }
        
        [Fact]
        public async Task Convert_Behaviour()
        {
            Mock<IFormatReader> reader = new Mock<IFormatReader>();
            Mock<IFormatWriter> writer = new Mock<IFormatWriter>();
            await Converter.Convert(reader.Object, writer.Object);
            
            reader.Verify(x => x.Begin(), Times.Once());
            writer.Verify(x => x.Begin(), Times.Once());
            writer.Verify(x => x.WriteAllAsync(It.IsAny<IFormatReader>()), Times.Once());
            writer.Verify(x => x.End(), Times.Once());
        }
    }
}