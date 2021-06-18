using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SMU.Helpers
{
    public static class AppGlobals
    {
        public static Type[] GetCommandLineTypes()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
        }

        public static List<string> GetModFilesInFolder(string folder, bool addParent = false)
        {
            List<string> files = new List<string>();
            DirectoryInfo di = new DirectoryInfo(folder);

            foreach (FileInfo f in di.GetFiles("*.package"))
            {
                files.Add($"{(addParent ? (f.Directory?.Name + "\\") : "")}{f.Name}");
            }

            foreach (FileInfo f in di.GetFiles("*.ts4script"))
            {
                files.Add($"{(addParent ? (f.Directory?.Name + "\\") : "")}{f.Name}");
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                files.AddRange(GetModFilesInFolder(dir.FullName, true));
            }

            return files;
        }
    }
}
