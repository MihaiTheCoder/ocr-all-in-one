using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsOcrWrapper
{
    public interface IGenericOcrRunner<T>: IGenericOcrRunner
        where T: IMappableToGenericResponse
    {
        Task<T> GetOcrResultAsync(string inputImage, string inputLanguage = null);
    }
    public interface IGenericOcrRunner
    {
        Task<GenericOcrResponse> GetGenericOcrResultAsync(string inputImage, string inputLanguage=null);

        string Name { get; }
    }
}
