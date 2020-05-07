using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;

namespace WindowsOcrWrapper.WindowsOcr
{
    /// <summary>
    /// Needs to be singleton, as it's the class that makes sure that Ocr is not invoked in parallel.
    /// A powershell instance is started in background, powershell session that is closed when the object is disposed.
    /// If you have multiple instances of OcrExecutor that run in parallel, the windows OCR dll will complain
    /// If you execute the sync method from multiple threads in parallel the powershell will complain because it was not developed to work in a multi threaded fashion.
    /// The Async API can be used from multiple threads, and it works fine as long as you have a single instance of OcrExecutor
    /// </summary>
    public class WindowsOcrExecutor : GenericOcrRunner<WindowsOcrResult>, IDisposable
    {
        public static object lockObj = new object();
        PowerShell powershell;
        internal bool isOcrRunning = false;
        TaskFactory factory;
        private readonly IOcrCache ocrCache;

        public override string Name => nameof(WindowsOcrExecutor);

        public WindowsOcrExecutor(IOcrCache ocrCache): base(ocrCache)
        {
            var taskScheduler = new LimitedConcurrencyLevelTaskScheduler(1);
            factory = new TaskFactory(taskScheduler);
            powershell = PowerShell.Create();
            string psScript = GetResourceText("Get-Text-Win-OCR.ps1");
            powershell.AddScript(psScript, false);
            powershell.Invoke();
            this.ocrCache = ocrCache;
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
        public override Task<WindowsOcrResult> GetOcrResultWithoutCacheAsync(string imagePath, string language=null)
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
        public WindowsOcrResult GetOcrResult(string imagePath)
        {
            powershell.Commands.Clear();
            powershell.AddCommand("Get-Text-OCR").AddParameter("Path", imagePath);
            var result = powershell.Invoke();
            var firstResult = WindowsOcrResult.FromDynamic(result[0] as dynamic);
            return firstResult;
        }

        public void Dispose()
        {
            powershell.Dispose();
        }
    }
}
