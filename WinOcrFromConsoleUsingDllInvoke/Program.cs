using WinOcrFromConsoleUsingDllInvoke.WinOcr;

namespace WinOcrFromConsoleUsingDllInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            var png = @"C:\Users\mihai.petrutiu\Downloads\clocks\clocks\AAA_BXSP001_060.mxf_clock.png";            
            OcrExecutor ocrExecutor = new OcrExecutor("Get-Text-Functions.ps1");
            var ocrResult = ocrExecutor.GetOcrResultAsync(png).Result;
        }
    }
}
