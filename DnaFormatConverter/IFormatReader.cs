using System;
using System.Threading.Tasks;

namespace DnaFormatConverter
{
    public interface IFormatReader
    {
        Task Begin();
        Task<SNP> ReadNext();
        bool EndOfFile { get;}
    }
}