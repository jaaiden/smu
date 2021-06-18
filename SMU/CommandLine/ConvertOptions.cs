using CommandLine;
using SMU.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMU.CommandLine
{
    [Verb("convert", HelpText = "Mod conversion tools.")]
    public class ConvertOptions : ICLOpts
    {
        public enum ConvertCommand
        {
            mod,
            allmods
        }

        [Value(0, MetaName = "command", HelpText = "The conversion command to run.")]
        public ConvertCommand Command { get; set; }

        [Value(1, MetaName = "mod name", HelpText = "The folder name to run the command on. If blank then it will run on the root folder.")]
        public string ModName { get; set; } = "";

        public void ProcessCommand()
        {
            switch (Command)
            {
                case ConvertCommand.mod:
                    ModConverter.AddModInfo(Path.Combine(Program.Config.ModFolder, ModName));
                    break;

                case ConvertCommand.allmods:
                    foreach (string path in Directory.GetDirectories(Program.Config.ModFolder))
                    {
                        ModConverter.AddModInfo(path);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
