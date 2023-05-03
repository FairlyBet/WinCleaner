#define DUMMY
#define DEV

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;


namespace WinCleaner
{
    internal class Program
    {
        private static readonly IEnumerable<string> _include = ConfigurationManager.ReadInclude();
        private static readonly IEnumerable<string> _exclude = ConfigurationManager.ReadExclude();


        private static void Main()
        {
#if !DUMMY
#if DEV
            Console.Read();
#endif
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
            Console.Read();
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
#endif
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
