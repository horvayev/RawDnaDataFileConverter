using System.IO;
using System.Threading.Tasks;

namespace DnaFormatConverter
{
    public interface IFormatWriter
    {
        Task Begin();
        Task End();
        Task WriteNext(SNP genotype);
        Task WriteAllAsync(IFormatReader reader);
    }
}