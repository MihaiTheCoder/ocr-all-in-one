using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocr.Wrapper
{
    public interface IHighLevelOcrService<T>: IGenericOcrRunner
        where T: IMappableToGenericResponse
    {
        Task<T> GetOcrResultAsync(string inputImage, string inputLanguage = null, bool runAnywayWithBadLanguage = true);
    }
    public interface IGenericOcrRunner
    {
        Task<GenericOcrResponse> GetGenericOcrResultAsync(string inputImage, string inputLanguage=null, bool runAnywayWithBadLanguage = true);

        string Name { get; }
    }
}
