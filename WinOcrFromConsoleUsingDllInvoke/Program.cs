using System;
using System.IO;
using System.Threading.Tasks;
using WindowsOcrWrapper.WinOcrResults;
using System.Linq;

namespace WinOcrFromConsoleUsingDllInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = @"C:\Users\mihai.petrutiu\Downloads\clocks\clocks\";
            string[] files = Directory.GetFiles(dir).Where(f => f.EndsWith("png")).ToArray();
            var png = @"C:\Users\mihai.petrutiu\Downloads\clocks\clocks\AAA_BXSP001_060.mxf_clock.png";

            OcrExecutor ocrExecutor = new OcrExecutor();
            

            Parallel.For(0, files.Length, async (i) => {
                var result = await ocrExecutor.GetOcrResultAsync(files[i]);
                Console.WriteLine(result.Text);
                Console.WriteLine(i);
            });
            Console.ReadKey();
        }
    }
}
