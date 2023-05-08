#define DUMMY
#define DEV

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;


namespace WinCleaner
{
    internal class Program
    {
        private enum RecycleFlags : uint
        {
            SHRB_NOCONFIRMATION = 0x00000001, // Don't ask confirmation
            SHRB_NOPROGRESSUI = 0x00000002, // Don't show any windows dialog
            SHRB_NOSOUND = 0x00000004 // Don't make sound, ninja mode enabled :v
        }
        
        private static readonly IEnumerable<string> _include = ConfigurationManager.ReadInclude();
        private static readonly IEnumerable<string> _exclude = ConfigurationManager.ReadExclude();


        private static void Main()
        {
#if DEV
            Console.Read();
#endif

#if !DUMMY
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
                ExecuteCleanMgr,
                ResetMicrosoftStore,
                ClearRecycleBin,
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

            Logger.Publish();
#endif
            // TODO check mozilla cache
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
                    FileName = "CMD.exe",
                    Arguments = "/C cleanmgr /sagerun:0\nexit\n"
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

        private static void DeleteAllFilesInDir(string path)
        {
            var target = new DirectoryInfo(path);
            if (target.Exists && !_exclude.Contains(target.FullName))
            {
                var files = target.GetFiles();
                foreach (var file in files)
                {
                    try
                    {
                        var log = new Log
                        {
                            Name = file.Name,
                            Size = file.Length
                        };
                        file.Delete();
                        Logger.Log(log);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                try
                {
                    var dirs = target.GetDirectories();
                    foreach (var dir in dirs)
                    {
                        try
                        {
                            var log = new Log
                            {
                                Name = dir.FullName,
                                Size = DirSize(dir)
                            };
                            dir.Delete(true);
                            Logger.Log(log);
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

        private static long DirSize(DirectoryInfo target)
        {
            long size = 0;
            FileInfo[] files = target.GetFiles();
            foreach (FileInfo file in files)
            {
                size += file.Length;
            }
            DirectoryInfo[] dirs = target.GetDirectories();
            foreach (var dir in dirs)
            {
                size += DirSize(dir);
            }
            return size;
        }
    }
}
