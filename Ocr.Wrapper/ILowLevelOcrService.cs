using System.Threading.Tasks;

namespace Ocr.Wrapper
{
    public interface ILowLevelOcrService<T> where T: IMappableToGenericResponse
    {
        string Name { get; }

        Task<T> GetOcrResultWithoutCacheAsync(string filePath, string language = null, bool runAnywayWithBadLanguage = true);
    }
}