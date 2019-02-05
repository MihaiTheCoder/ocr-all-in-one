using System;
using WindowsOcrWrapper.WinOcrResults;

namespace WinOcrFromConsoleUsingDllInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            var png = @"C:\Users\mihai.petrutiu\Downloads\clocks\clocks\AAA_BXSP001_060.mxf_clock.png";            
            OcrExecutor ocrExecutor = new OcrExecutor();
            var ocrResult = ocrExecutor.GetOcrResultAsync(png).Result;
            Console.WriteLine(ocrResult.Text);
            Console.ReadKey();
        }
    }
}
