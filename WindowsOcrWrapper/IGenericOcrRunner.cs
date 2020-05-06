using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsOcrWrapper
{
    public interface IGenericOcrRunner
    {
        Task<GenericOcrResponse> RunAsync(string inputImage, string inputLanguage=null);

        string Name { get; }
    }
}
