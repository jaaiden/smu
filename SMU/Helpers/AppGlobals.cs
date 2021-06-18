using CommandLine;
using Newtonsoft.Json;
using SMU.Data;
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
        private static readonly string ProfilesFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create),
            "SMU",
            "Profiles"
        );

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

        public static bool CreateProfile(string name)
        {
            string profilePath = Path.Combine(ProfilesFolder, $"{name}.json");

            if (!Directory.Exists(ProfilesFolder))
            {
                Directory.CreateDirectory(ProfilesFolder);
            }

            if (!File.Exists(profilePath))
            {
                Profile p = new Profile()
                {
                    Name = name
                };

                File.WriteAllText(profilePath, JsonConvert.SerializeObject(p, Formatting.Indented));
                Console.WriteLine($"Created profile '{name}'.");
                return true;
            }

            Console.WriteLine($"A profile named '{name}' already exists.");
            return false;
        }

        public static List<Profile> GetProfiles()
        {
            List<Profile> profiles = new List<Profile>();

            if (!Directory.Exists(ProfilesFolder))
            {
                Directory.CreateDirectory(ProfilesFolder);
            }

            foreach (string profilePath in Directory.GetFiles(ProfilesFolder))
            {
                profiles.Add(JsonConvert.DeserializeObject<Profile>(File.ReadAllText(profilePath)));
            }

            return profiles;
        }
    }
}
