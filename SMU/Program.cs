using CommandLine;
using SMU.CommandLine;
using SMU.Helpers;
using System;

namespace SMU
{

    public class Program
    {
        public static AppConfig Config { get; private set; }

        static void Main(string[] args)
        {
            Config = AppConfig.LoadConfig();
            Parser.Default.ParseArguments(args, AppGlobals.GetCommandLineTypes()).WithParsed(ProcessArguments);
        }

        private static void ProcessArguments(object opts)
        {
            if (opts is ICLOpts)
            {
                ((ICLOpts)opts).ProcessCommand();
            }
        }
    }
}
