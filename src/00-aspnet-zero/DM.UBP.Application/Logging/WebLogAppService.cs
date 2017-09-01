using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Abp.Authorization;
using Abp.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using DM.UBP.Authorization;
using DM.UBP.Dto;
using DM.UBP.IO;
using DM.UBP.Logging.Dto;
using DM.UBP.Net.MimeTypes;

namespace DM.UBP.Logging
{
    [AbpAuthorize(AppPermissions.Pages_Administration_Host_Maintenance)]
    public class WebLogAppService : UBPAppServiceBase, IWebLogAppService
    {
        private readonly IAppFolders _appFolders;

        public WebLogAppService(IAppFolders appFolders)
        {
            _appFolders = appFolders;
        }

        public GetLatestWebLogsOutput GetLatestWebLogs()
        {
            var directory = new DirectoryInfo(_appFolders.WebLogsFolder);
            var lastLogFile = directory.GetFiles("*.txt", SearchOption.TopDirectoryOnly)
                                        .OrderByDescending(f => f.LastWriteTime)
                                        .FirstOrDefault();
            
            if (lastLogFile == null)
            {
                return new GetLatestWebLogsOutput();
            }

            var lines = AppFileHelper.ReadLines(lastLogFile.FullName).Reverse().Take(1000).ToList();
            var logLineCount = 0;
            var lineCount = 0;

            foreach (var line in lines)
            {
                if (line.StartsWith("DEBUG") ||
                    line.StartsWith("INFO") ||
                    line.StartsWith("WARN") ||
                    line.StartsWith("ERROR") ||
                    line.StartsWith("FATAL"))
                {
                    logLineCount++;
                }

                lineCount++;

                if (logLineCount == 100)
                {
                    break;
                }
            }

            return new GetLatestWebLogsOutput
            {
                LatesWebLogLines = lines.Take(lineCount).Reverse().ToList()
            };
        }

        public FileDto DownloadWebLogs()
        {
            //Create temporary copy of logs
            var tempLogDirectory = CopyAllLogFilesToTempDirectory();
            var logFiles = new DirectoryInfo(tempLogDirectory).GetFiles("*.txt", SearchOption.TopDirectoryOnly).ToList();

            //Create the zip file
            var zipFileDto = new FileDto("WebSiteLogs.zip", MimeTypeNames.ApplicationZip);
            var outputZipFilePath = Path.Combine(_appFolders.TempFileDownloadFolder, zipFileDto.FileToken);

            using (var outputZipFileStream = File.Create(outputZipFilePath))
            {
                using (var zipStream = new ZipOutputStream(outputZipFileStream))
                {
                    zipStream.IsStreamOwner = true; // Makes the Close also Close the underlying stream

                    foreach (var logFile in logFiles)
                    {
                        var logZipEntry = new ZipEntry(logFile.Name)
                        {
                            DateTime = logFile.LastWriteTime,
                            Size = logFile.Length
                        };

                        zipStream.PutNextEntry(logZipEntry);

                        using (var fs = new FileStream(logFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan))
                        {
                            StreamUtils.Copy(fs, zipStream, new byte[4096]);
                        }

                        zipStream.CloseEntry();
                    }
                }
            }

            //Delete temporary copied logs
            Directory.Delete(tempLogDirectory, true);

            return zipFileDto;
        }

        private string CopyAllLogFilesToTempDirectory()
        {
            var tempDirectoryPath = Path.Combine(_appFolders.TempFileDownloadFolder, Guid.NewGuid().ToString("N").Substring(16));
            DirectoryHelper.CreateIfNotExists(tempDirectoryPath);

            foreach (var file in GetAllLogFiles())
            {
                var destinationFilePath = Path.Combine(tempDirectoryPath, file.Name);
                File.Copy(file.FullName, destinationFilePath, true);
            }

            return tempDirectoryPath;
        }

        private List<FileInfo> GetAllLogFiles()
        {
            var directory = new DirectoryInfo(_appFolders.WebLogsFolder);
            return directory.GetFiles("*.txt", SearchOption.TopDirectoryOnly).ToList();
        }
    }
}