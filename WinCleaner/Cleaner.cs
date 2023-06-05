using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;


namespace WinCleaner
{
    public static class Cleaner
    {
        private enum RecycleFlags : uint
        {
            SHRB_NOCONFIRMATION = 0x00000001, // Don't ask confirmation
            SHRB_NOPROGRESSUI = 0x00000002, // Don't show any windows dialog
            SHRB_NOSOUND = 0x00000004 // Don't make sound, ninja mode enabled :v
        }

        private static readonly IEnumerable<string> _include = ConfigurationManager.ReadInclude();
        private static readonly IEnumerable<string> _exclude = ConfigurationManager.ReadExclude();


        public static void PerformCleaning()
        {
            var clears = new Action[]
            {
                ClearSoftwareDistributionFolder,
                ClearUserTempFolder,
                ClearSystemTempFolder,
                ClearPrefetchFolder,
                ClearFirefoxCache,
                ClearEdgeCache,
                ClearChromeCache,
                ClearOperaCache,
                ClearDnsCache,
                ResetMicrosoftStore,
                ClearRecycleBin,
                ClearRecentFolder,
                ClearWindowsThumbnailCache,
                RunAdditionalResources,
            };

            foreach (var clear in clears)
            {
                try
                {
                    clear();
                }
                catch { }
            }

            foreach (var item in _include)
            {
                try
                {
                    DeleteAllFilesInDir(item);
                }
                catch { }
            }
        }

        private static void ClearSoftwareDistributionFolder()
        {
            var winFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var softwareDistributionFolder = Path.Combine(winFolder, "SoftwareDistribution");

            DeleteAllFilesInDir(softwareDistributionFolder);
        }

        private static void ClearUserTempFolder()
        {
            var tempFolder = Path.GetTempPath();

            DeleteAllFilesInDir(tempFolder);
        }

        private static void ClearSystemTempFolder()
        {
            var winFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var systemTemp = Path.Combine(winFolder, "Temp");

            DeleteAllFilesInDir(systemTemp);
        }

        private static void ClearPrefetchFolder()
        {
            var winFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var prefetchFolder = Path.Combine(winFolder, "Prefetch");

            DeleteAllFilesInDir(prefetchFolder);
        }

        private static void ClearFirefoxCache()
        {
            var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var cacheFolder = Path.Combine(localFolder, "Mozilla", "Firefox", "Profiles");
            try
            {
                var dirs = new DirectoryInfo(cacheFolder).GetDirectories().SelectMany(d => d.GetDirectories()).Where(d => d.Name == "cache2");
                foreach (var dir in dirs)
                {
                    DeleteAllFilesInDir(dir.FullName);
                }
            }
            catch { }
        }

        private static void ClearChromeCache()
        {
            var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var cacheDir = Path.Combine(localFolder, "Google", "Chrome", "User Data", "Default", "Cache");

            DeleteAllFilesInDir(cacheDir);
        }

        private static void ClearEdgeCache()
        {
            var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var cacheDir = Path.Combine(localFolder, "Microsoft", "Edge", "User Data", "Default", "Cache");

            DeleteAllFilesInDir(cacheDir);
        }

        private static void ClearOperaCache()
        {
            var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var cacheDir = Path.Combine(localFolder, "Opera", "Opera", "Cache");
            DeleteAllFilesInDir(cacheDir);
        }

        private static void ClearDnsCache()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "CMD.exe",
                    Arguments = "/C ipconfig/flushdns\nexit\n"
                }
            };
            process.Start();
            process.WaitForExit();
        }

        private static void ExecuteCleanMgr()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cleanmgr.exe",
                    Arguments = "/verylowdisk"
                }
            };
            process.Start();
            process.WaitForExit();
        }

        private static void ResetMicrosoftStore()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "CMD.exe",
                    Arguments = "/C WSreset.exe\nexit\n"
                }
            };
            process.Start();
            process.WaitForExit();
        }

        private static void ClearRecycleBin()
        {
            if (ConfigurationManager.GetClearRecycleBin())
            {
                SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHRB_NOCONFIRMATION);
            }
        }

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        private static void ClearRecentFolder()
        {
            var recentFolder = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            DeleteAllFilesInDir(recentFolder);
        }

        private static void ClearWindowsThumbnailCache()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path = Path.Combine(path, "Microsoft\\Windows\\Explorer");
            DeleteAllFilesInDir(path);
        }

        private static void RunAdditionalResources()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "additional-resources\\WiseDiskCleanerPortable.exe",
                    Arguments = "-a -ads"
                }
            };
            process.Start();
            process.WaitForExit();
        }

        private static void DeleteAllFilesInDir(string path)
        {
            try
            {
                var target = new DirectoryInfo(path);
                if (target.Exists && !_exclude.Contains(target.FullName))
                {
                    var files = target.GetFiles();
                    foreach (var file in files)
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    try
                    {
                        var dirs = Directory.GetDirectories(path);
                        foreach (var dir in dirs)
                        {
                            try
                            {
                                var dirInfo = new DirectoryInfo(dir);
                                dirInfo.Delete(true);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }
    }


    public static class DirectoryInfoExtensions
    {
        public static long GetDirectorySize(this DirectoryInfo target)
        {
            long result = 0;
            result += target.EnumerateFiles().Sum(f => f.Length);

            var dirs = Directory.GetDirectories(target.FullName);

            if (dirs != null && dirs.Length > 0)
            {
                foreach (var dir in dirs)
                {
                    try
                    {
                        var dirInfo = new DirectoryInfo(dir);
                        result += GetDirectorySize(dirInfo);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return result;
        }
    }
}
