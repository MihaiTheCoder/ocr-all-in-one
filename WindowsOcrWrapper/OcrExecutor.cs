using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;

namespace WindowsOcrWrapper.WinOcrResults
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
        public OcrExecutor()
        {
            var taskScheduler = new LimitedConcurrencyLevelTaskScheduler(1);
            factory = new TaskFactory(taskScheduler);
            powershell = PowerShell.Create();
            var psScript = GetResourceText("Get-Text-Win-OCR.ps1");
            powershell.AddScript(psScript, false);
            powershell.Invoke();
        }

        private string GetResourceText(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var manifestName = assembly.GetManifestResourceNames().Single(man => man.EndsWith(fileName));
            using (Stream stream = assembly.GetManifestResourceStream(manifestName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
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
