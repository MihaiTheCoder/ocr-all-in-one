using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Ocr.Wrapper
{
    public class Languages
    {
        public static Dictionary<string, string> GetOcrLanguagesForWin10AndAzure()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("Chinese (Simplified)", "zh-Hans");
            dict.Add("Czech", "cs");
            dict.Add("Danish", "da");
            dict.Add("Dutch", "nl");
            dict.Add("English", "en");
            dict.Add("Finnish", "fi");
            dict.Add("French", "fr");
            dict.Add("German", "de");
            dict.Add("Greek", "el");
            dict.Add("Hungarian", "hu");
            dict.Add("Italian", "it");
            dict.Add("Japanese", "ja");
            dict.Add("Korean", "ko");
            dict.Add("Norwegian", "nb");
            dict.Add("Polish", "pl");
            dict.Add("Portuguese", "pt");
            dict.Add("Romanian", "ro");
            dict.Add("Russian", "ru");
            dict.Add("Serbian (Cyrillic)", "sr-Cyrl");
            dict.Add("Serbian (Latin)", "sr-Latn");
            dict.Add("Slovak", "sk");
            dict.Add("Spanish", "es");
            dict.Add("Swedish", "sw");
            dict.Add("Turkish", "tr");
            return dict;
        }

        public static Dictionary<string, string> GetOcrLanguagesForGoogle()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("Chinese (Simplified)", "zh");
            dict.Add("Czech", "cs");
            dict.Add("Danish", "da");
            dict.Add("Dutch", "nl");
            dict.Add("English", "en");
            dict.Add("Finnish", "fi");
            dict.Add("French", "fr");
            dict.Add("German", "de");
            dict.Add("Greek", "el");
            dict.Add("Hungarian", "hu");
            dict.Add("Italian", "it");
            dict.Add("Japanese", "ja");
            dict.Add("Korean", "ko");
            dict.Add("Norwegian", "no");
            dict.Add("Polish", "pl");
            dict.Add("Portuguese", "pt");
            dict.Add("Romanian", "ro");
            dict.Add("Russian", "ru");
            dict.Add("Serbian (Cyrillic)", "sr");
            dict.Add("Serbian (Latin)", "sr-Latn");
            dict.Add("Slovak", "sk");
            dict.Add("Spanish", "es");
            dict.Add("Swedish", "sv");
            dict.Add("Turkish", "tr");
            return dict;
        }

        public static Dictionary<string, string> GetOcrLanguagesForTesseract()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("Chinese (Simplified)", "chi_sim");
            dict.Add("Czech", "ces");
            dict.Add("Danish", "dan");
            dict.Add("Dutch", "nld");
            dict.Add("English", "eng");
            dict.Add("Finnish", "fin");
            dict.Add("French", "fra");
            dict.Add("German", "deu");
            dict.Add("Greek", "ell");
            dict.Add("Hungarian", "hun");
            dict.Add("Italian", "ita");
            dict.Add("Japanese", "jpn");
            dict.Add("Korean", "kor");
            dict.Add("Norwegian", "nor");
            dict.Add("Polish", "pol");
            dict.Add("Portuguese", "por");
            dict.Add("Romanian", "ron");
            dict.Add("Russian", "rus");
            dict.Add("Serbian (Cyrillic)", "srp");
            dict.Add("Serbian (Latin)", "srp_latn");
            dict.Add("Slovak", "slk");
            dict.Add("Spanish", "spa");
            dict.Add("Swedish", "swe");
            dict.Add("Turkish", "tur");

            return dict;
        }
    }

    public class MultiOcrRunner
    {
        public IGenericOcrRunner[] OcrRunners { get; private set; }

        public MultiOcrRunner(params IGenericOcrRunner[] ocrRunners)
        {
            OcrRunners = ocrRunners;
        }

        public async Task<Dictionary<string, GenericOcrResponse>> RunAllOcrEnginesOnImage(string inputImagePath, Language language = Language.English)
        {
            return await RunAllOcrEnginesOnImage(OcrRunners, inputImagePath, language);
        }

        public static async Task<Dictionary<string, GenericOcrResponse>> RunAllOcrEnginesOnImage(
            IEnumerable<IGenericOcrRunner> ocrEngines,
            string inputImagePath, Language language = Language.English)
        {
            var responses = new Dictionary<string, GenericOcrResponse>();
            foreach (var ocr in ocrEngines)
            {
                var allLanguagesForOcr = new Dictionary<string, string>();
                var languageForOcr = string.Empty;
                if (ocr.Name == "TesseractLowLevelOcrService")
                {
                    allLanguagesForOcr = Ocr.Wrapper.Languages.GetOcrLanguagesForTesseract();
                }
                if (ocr.Name == "AzureLowLevelOcrService" || ocr.Name == "WindowsLowLevelOcrService")
                {
                    allLanguagesForOcr = Ocr.Wrapper.Languages.GetOcrLanguagesForWin10AndAzure();
                }
                if (ocr.Name == "GoogleLowLevelOcrService")
                {
                    allLanguagesForOcr = Ocr.Wrapper.Languages.GetOcrLanguagesForGoogle();
                }
                
                if (allLanguagesForOcr.ContainsKey(language.ToString()))
                    languageForOcr = allLanguagesForOcr[language.ToString()];

                responses[ocr.Name] = await ocr.GetGenericOcrResultAsync(inputImagePath, languageForOcr);
            }
            return responses;
        }

        public IGenericOcrRunner GetOcrByName(string ocrName)
        {
            return OcrRunners.FirstOrDefault(o => o.Name == ocrName);
        }

        public T GetOcrByType<T>() where T: class, IGenericOcrRunner
        {
            return OcrRunners.FirstOrDefault(ocr => ocr is T) as T;
        }
    }
}
