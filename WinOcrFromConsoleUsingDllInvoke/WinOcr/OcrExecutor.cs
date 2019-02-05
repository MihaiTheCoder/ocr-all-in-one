using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;

namespace WinOcrFromConsoleUsingDllInvoke.WinOcr
{
    /// <summary>
    /// Needs to be singleton, as it's the class that makes sure that Ocr is not invoked in parallel
    /// </summary>
    public class OcrExecutor : IDisposable
    {
        public static object lockObj = new object();
        PowerShell powershell;
        internal bool isOcrRunning = false;        
        TaskFactory factory;
        public OcrExecutor(string pathToPowershell)
        {
            var taskScheduler = new LimitedConcurrencyLevelTaskScheduler(1);
            factory = new TaskFactory(taskScheduler);
            powershell = PowerShell.Create();
            var psScript = File.ReadAllText(pathToPowershell);
            powershell.AddScript(psScript);
            powershell.Invoke();
        }

        public Task<OcrResult> GetOcrResultAsync(string imagePath)
        {
            return factory.StartNew(() => { return GetOcrResult(imagePath); });
        }

        internal OcrResult GetOcrResult(string imagePath)
        {
            powershell.Commands.Clear();
            powershell.AddCommand("Get-Text-OCR").AddParameter("Path", imagePath);
            var result = powershell.Invoke();
            var firstResult = OcrResult.FromDynamic(result[0] as dynamic);
            return firstResult;
        }

        public void Dispose()
        {
            powershell.Dispose();
        }
    }
}
