using CommandLine;
using SMU.Data;
using SMU.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMU.CommandLine
{
    [Verb("profile", HelpText = "Profile management commands.")]
    public class ProfileOptions : ICLOpts
    {
        public enum ProfileCommand
        {
            create,
            list,
            edit,
            delete
        }

        [Value(0, MetaName = "command", HelpText = "The profile command to run ['create', 'list', 'edit', 'delete'].")]
        public ProfileCommand Command { get; set; }

        [Value(1, MetaName = "profile name", HelpText = "The name of the profile to run the command with.")]
        public string ProfileName { get; set; }

        public void ProcessCommand()
        {
            switch (Command)
            {
                case ProfileCommand.create:
                    AppGlobals.CreateProfile(ProfileName);
                    break;

                case ProfileCommand.list:
                    Console.WriteLine("Available profiles:");
                    foreach (Profile p in AppGlobals.GetProfiles())
                    {
                        Console.WriteLine("\t" +
                            $"Name:        {p.Name}\n\t" +
                            $"Description: {p.Description ?? "No description available."}\n\t" +
                            $"Mods:        {p.Mods?.Count}\n");
                    }
                    break;
            }
        }
    }
}
