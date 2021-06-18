using Newtonsoft.Json;
using SMU.Data;
using SMU.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SMU.Converters
{
    public static class ModConverter
    {
        public static void AddModInfo(string modPath)
        {
            string modInfoFile = Path.Combine(modPath, "smumod.json");

            if (modPath.Equals(Program.Config.ModFolder) || string.IsNullOrEmpty(modPath))
            {
                Console.WriteLine("Cannot add mod metadata file for mods in the root folder.");
                Console.WriteLine("Please move mods into their own folder to create the metadata file.");
                return;
            }

            DirectoryInfo di = new DirectoryInfo(modPath);

            List<string> modFiles = AppGlobals.GetModFilesInFolder(modPath);

            Mod mod = new Mod()
            {
                Name = di.Name,
                Description = di.Name,
                Root = di.Name,
                Files = modFiles
            };

            File.WriteAllText(modInfoFile, JsonConvert.SerializeObject(mod, Formatting.Indented));
            Console.WriteLine($"Added SMU mod metadata file for {di.Name}.");
        }
    }
}
