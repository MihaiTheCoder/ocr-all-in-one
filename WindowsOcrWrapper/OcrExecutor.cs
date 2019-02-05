using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;
using WindowsOcrWrapper.WinOcrResults;

namespace WindowsOcrWrapper
{
    /// <summary>
    /// Needs to be singleton, as it's the class that makes sure that Ocr is not invoked in parallel.
    /// On dispose, we dispose the powershell session, that we keep alive.
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
            string psScript = GetResourceText("Get-Text-Win-OCR.ps1");
            powershell.AddScript(psScript, false);
            powershell.Invoke();
        }

        /// <summary>
        /// Load the manifest text based on the file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Runs the ocr result on a task created by a factory that has concurrency of 1
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public Task<OcrResult> GetOcrResultAsync(string imagePath)
        {
            return factory.StartNew(() => { return GetOcrResult(imagePath); });
        }

        /// <summary>
        /// Runs the OCR on the specified file
        /// This method is not thread safe. For invokes from multiple threads use the Async method.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <thre
        /// <returns></returns>
        public OcrResult GetOcrResult(string imagePath)
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
