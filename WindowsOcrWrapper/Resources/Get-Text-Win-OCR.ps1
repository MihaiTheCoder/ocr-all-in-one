#initially taken from: https://github.com/HumanEquivalentUnit/PowerShell-Misc/blob/03cbe7deaa545a96e5102777877ac929c5b67a62/Get-Win10OcrTextFromImage.ps1
using namespace Windows.Storage
using namespace Windows.Graphics.Imaging
$ErrorActionPreference = "Stop"
# Add the WinRT assembly, and load the appropriate WinRT types
Add-Type -AssemblyName System.Runtime.WindowsRuntime

$null = [Windows.Storage.StorageFile,                Windows.Storage,         ContentType = WindowsRuntime]
$null = [Windows.Media.Ocr.OcrEngine,                Windows.Foundation,      ContentType = WindowsRuntime]
$null = [Windows.Foundation.IAsyncOperation`1,       Windows.Foundation,      ContentType = WindowsRuntime]
$null = [Windows.Graphics.Imaging.SoftwareBitmap,    Windows.Foundation,      ContentType = WindowsRuntime]
$null = [Windows.Storage.Streams.RandomAccessStream, Windows.Storage.Streams, ContentType = WindowsRuntime]
    
    
# [Windows.Media.Ocr.OcrEngine]::AvailableRecognizerLanguages
$ocrEngine = [Windows.Media.Ocr.OcrEngine]::TryCreateFromUserProfileLanguages()
    

# PowerShell doesn't have built-in support for Async operations, 
# but all the WinRT methods are Async.
# This function wraps a way to call those methods, and wait for their results.
$getAwaiterBaseMethod = [WindowsRuntimeSystemExtensions].GetMember('GetAwaiter').
                            Where({
                                    $PSItem.GetParameters()[0].ParameterType.Name -eq 'IAsyncOperation`1'
                                }, 'First')[0]

Function Await {
    param($AsyncTask, $ResultType)

    $getAwaiterBaseMethod.
        MakeGenericMethod($ResultType).
        Invoke($null, @($AsyncTask)).
        GetResult()
}

Function Get-Text-OCR {
	param($Path)

	foreach ($p in $Path)
	{
      
		# From MSDN, the necessary steps to load an image are:
		# Call the OpenAsync method of the StorageFile object to get a random access stream containing the image data.
		# Call the static method BitmapDecoder.CreateAsync to get an instance of the BitmapDecoder class for the specified stream. 
		# Call GetSoftwareBitmapAsync to get a SoftwareBitmap object containing the image.
		#
		# https://docs.microsoft.com/en-us/windows/uwp/audio-video-camera/imaging#save-a-softwarebitmap-to-a-file-with-bitmapencoder

		# .Net method needs a full path, or at least might not have the same relative path root as PowerShell
		$p = $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($p)
        
		$params = @{ 
			AsyncTask  = [StorageFile]::GetFileFromPathAsync($p)
			ResultType = [StorageFile]
		}
		$storageFile = Await @params


		$params = @{ 
			AsyncTask  = $storageFile.OpenAsync([FileAccessMode]::Read)
			ResultType = [Streams.IRandomAccessStream]
		}
		$fileStream = Await @params


		$params = @{
			AsyncTask  = [BitmapDecoder]::CreateAsync($fileStream)
			ResultType = [BitmapDecoder]
		}
		$bitmapDecoder = Await @params


		$params = @{ 
			AsyncTask = $bitmapDecoder.GetSoftwareBitmapAsync()
			ResultType = [SoftwareBitmap]
		}
		$softwareBitmap = Await @params

		# Run the OCR
		Await $ocrEngine.RecognizeAsync($softwareBitmap) ([Windows.Media.Ocr.OcrResult])

	}
}