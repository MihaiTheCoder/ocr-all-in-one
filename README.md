# WindowsOcrFromConsole
WinOcrFromConsoleUsingDllInvoke is a self hosted ASP.NET Web API that does OCR on ValuesController POST. After starting the server, we also do a mock test using HttpClient. The main code for doing the OCR is in  WindowsOcrWrapper

WinOcrFromConsoleUsingDllInvoke was tested on Windows 10 and Windows Server 2016 Datacenter version on Azure and it works

On WindowsServer 2016 core long term support the OCR dll is not there, so I didn't try it
