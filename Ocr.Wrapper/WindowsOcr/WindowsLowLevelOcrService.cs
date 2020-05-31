using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;
using System.Linq;

namespace Ocr.Wrapper.WindowsOcr
{
    public class WindowsLowLevelOcrService: ILowLevelOcrService<WindowsOcrResult>
    {
        public static object lockObj = new object();
        PowerShell powershell;
        internal bool isOcrRunning = false;
        TaskFactory factory;
        private readonly IOcrCache ocrCache;
        protected internal StringBuilder debugPsOutput = new StringBuilder();


        public string Name => nameof(WindowsLowLevelOcrService);

        public WindowsLowLevelOcrService()
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
        public Task<WindowsOcrResult> GetOcrResultWithoutCacheAsync(string imagePath, string language = null, bool runAnywayWithBadLanguage = true)
        {
            return factory.StartNew(() => { return GetOcrResult(imagePath, language, runAnywayWithBadLanguage); });
        }

        /// <summary>
        /// Runs the OCR on the specified file
        /// This method is not thread safe. For invokes from multiple threads use the Async method.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <thre
        /// <returns></returns>
        public WindowsOcrResult GetOcrResult(
            string imagePath,
            string language,
            bool runAnywayWithBadLanguage = true)
        {

            powershell.Commands.Clear();

            powershell.Streams.Information.DataAdded += InformationHandler;
            powershell.Streams.Verbose.DataAdded += InformationalRecordEventHandler<VerboseRecord>;
            powershell.Streams.Debug.DataAdded += InformationalRecordEventHandler<DebugRecord>;
            powershell.Streams.Warning.DataAdded += InformationalRecordEventHandler<WarningRecord>;


            powershell.AddCommand("Get-Text-OCR")
                      .AddParameter("Path", imagePath)
                      .AddParameter("language", language)
                      .AddParameter("runAnywayWithBadLanguage", runAnywayWithBadLanguage)
                      .AddParameter("Verbose", true);

            var result = powershell.Invoke();

            var debugOutput = debugPsOutput.ToString();

            var firstResult = WindowsOcrResult.FromDynamic(result[0] as dynamic, imagePath, Name);
            return firstResult;
        }

        void InformationalRecordEventHandler<T>(object sender, DataAddedEventArgs e)
        where T : InformationalRecord
        {
            var newRecord = ((PSDataCollection<T>)sender)[e.Index];
            if (!string.IsNullOrEmpty(newRecord.Message))
            {
                debugPsOutput.AppendLine(newRecord.Message);
            }
        }

        void InformationHandler(object sender, DataAddedEventArgs e)
        {
            var newRecord = ((PSDataCollection<InformationRecord>)sender)[e.Index];
            if (newRecord?.MessageData != null)
            {
                debugPsOutput.AppendLine(newRecord.MessageData.ToString());
            }
        }


        public void Dispose()
        {
            powershell.Dispose();
        }
    }
}
