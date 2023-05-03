using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace WinCleaner
{
    public static class ConfigurationManager
    {
        public const string IncludePath = "include.txt";
        public const string ExcludePath = "exclude.txt";


        public static IEnumerable<string> ReadInclude()
        {
            EnsureFileExistence(IncludePath);
            var lines = File.ReadAllLines(IncludePath);
            return lines;
        }

        public static IEnumerable<string> ReadExclude()
        {
            EnsureFileExistence(ExcludePath);
            var lines = File.ReadAllLines(ExcludePath);
            return lines;
        }

        public static void WriteInclude(IEnumerable<string> lines)
        {
            EnsureFileExistence(IncludePath);
            File.WriteAllLines(IncludePath, lines);
        }

        public static void WriteExclude(IEnumerable<string> lines)
        {
            EnsureFileExistence(ExcludePath);
            File.WriteAllLines(ExcludePath, lines);
        }

        private static void EnsureFileExistence(string path)
        {
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
                using (File.CreateText(path)) ;
            }
        }

        public static void AddInclude(string dir)
        {
            var include = ReadInclude();

            if (include.Contains(dir))
            {
                return;
            }

            var exclude = ReadExclude();
            if (exclude.Contains(dir))
            {
                exclude = exclude.Where(x => x != dir);
            }
            include = include.Append(dir);

            WriteInclude(include);
            WriteExclude(exclude);
        }

        public static void AddExclude(string dir)
        {
            var exclude = ReadExclude();

            if (exclude.Contains(dir))
            {
                return;
            }

            var include = ReadInclude();
            if (include.Contains(dir))
            {
                include = include.Where(x => x != dir);
            }
            exclude = exclude.Append(dir);

            WriteInclude(include);
            WriteExclude(exclude);
        }
    }
}
