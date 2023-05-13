using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace WinCleaner
{
    public static class Logger
    {
        public const string LastSessionPath = "last-session.txt";
        public const string ResultPath = "report.txt";
        private static readonly List<Log> _logs = new List<Log>();
        private static long _windowsSizeBefore;
        private static long _windowsSizeAfter;
        private static long _userSizeBefore;
        private static long _userSizeAfter;
        private static long _totalBefore;
        private static long _totalAfter;

        public static long TotalBefore => _totalBefore;
        public static long TotalAfter => _totalAfter;


        public static void CommitBeforeCleaning()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var dir = new DirectoryInfo(path);
            _windowsSizeBefore = dir.GetDirectorySize();

            path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            dir = new DirectoryInfo(path);
            _userSizeBefore = dir.GetDirectorySize();

            var systemDrive = DriveInfo.GetDrives().Where(d => d.Name == @"C:\").FirstOrDefault();
            if (systemDrive is null)
            {
                return;
            }
            _totalBefore = systemDrive.TotalSize - systemDrive.TotalFreeSpace;
        }

        public static void CommitAfterCleaning()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var dir = new DirectoryInfo(path);
            _windowsSizeAfter = dir.GetDirectorySize();

            path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            dir = new DirectoryInfo(path);
            _userSizeAfter = dir.GetDirectorySize();

            var systemDrive = DriveInfo.GetDrives().Where(d => d.Name == @"C:\").FirstOrDefault();
            if (systemDrive is null)
            {
                return;
            }
            _totalAfter = systemDrive.TotalSize - systemDrive.TotalFreeSpace;
        }

        public static void Log(Log log)
        {
            _logs.Add(log);
        }

        public static void Publish()
        {
            var report = CreateReport();

            var folders = GoogleDriveStatisticFolder.ListFiles(DriveResources.StatisticFolderId,
                GoogleDriveStatisticFolder.FolderType);
            var folder = folders.Where(f => f.Name == Environment.MachineName).FirstOrDefault();

            if (folder == default)
            {
                folder = GoogleDriveStatisticFolder.CreateFolder(Environment.MachineName,
                    DriveResources.StatisticFolderId);
            }

            var files = GoogleDriveStatisticFolder.ListFiles(folder.Id, GoogleDriveStatisticFolder.TextType);
            var reportName = DateTime.Now.ToString("dd.MM.yyyy");

            var count = 1;
            var stringCount = "";
            while (files.Where(f => f.Name == reportName + stringCount + ".txt").Any())
            {
                stringCount = $"({count++})";
            }
            reportName += stringCount + ".txt";

            using (var stream = GenerateStreamFromString(report))
            {
                GoogleDriveStatisticFolder.UploadFile(stream, reportName,
                    GoogleDriveStatisticFolder.TextType, folder.Id);
            }
        }

        private static string CreateReport()
        {
            var report = "";

            report += $"Windows folder before: {_windowsSizeBefore} bytes\n";
            report += $"Windows folder after: {_windowsSizeAfter} bytes\n";
            report += $"Reduced by {_windowsSizeBefore - _windowsSizeAfter} bytes " +
                $"({100 - (float)_windowsSizeAfter / _windowsSizeBefore * 100: ##0.##}%)\n\n";

            report += $"User folder before: {_userSizeBefore} bytes\n";
            report += $"User folder after: {_userSizeAfter} bytes\n";
            report += $"Reduced by {_userSizeBefore - _userSizeAfter} bytes " +
                $"({100 - (float)_userSizeAfter / _userSizeBefore * 100: ##0.##}%)\n\n";

            report += $"Total cleared: {_totalBefore - _totalAfter} bytes";

            return report;
        }

        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }


    public class Log
    {
        public string Name { get; set; }
        public long Size { get; set; }
    }
}
