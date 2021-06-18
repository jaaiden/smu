using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMU.CommandLine
{
    [Verb("config", HelpText = "Access the configuration options for SMU.")]
    public class ConfigOptions : ICLOpts
    {
        public enum ConfigCommand
        {
            get,
            set,
            list
        }

        /// <summary>
        /// Command for the action to take, either get or set.
        /// </summary> 
        [Value(0, MetaName = "command", HelpText = "Command for the action to take, can be get, set, or list.")]
        public ConfigCommand ConfigAction { get; set; }

        /// <summary>
        /// The name of the configuration variable to get or set.
        /// </summary>
        [Value(1, MetaName = "property", HelpText = "The name of the configuration property to get or set.")]
        public string ConfigProperty { get; set; }

        /// <summary>
        /// If the command is 'set', then this is the value to set the variable to.
        /// </summary>
        [Value(2, MetaName = "value", HelpText = "If the config command is 'set', then this is the value to set the variable to.")]
        public string ConfigValue { get; set; }

        public void ProcessCommand()
        {
            switch (ConfigAction)
            {
                case ConfigCommand.get:
                    if (string.IsNullOrEmpty(ConfigProperty))
                    {
                        Console.WriteLine("You must provide a property to get.");
                        Console.WriteLine("Run 'smu config list' to see all available properties.");
                        break;
                    }
                    if (!Program.Config.HasConfigValue(ConfigProperty))
                    {
                        Console.WriteLine($"The property '{ConfigProperty}' does not exist.");
                        Console.WriteLine("Run 'smu config list' to see all available properties.");
                        break;
                    }
                    Console.WriteLine($"'{ConfigProperty}' is currently set to: '{Program.Config.GetConfigValue(ConfigProperty)}'.");
                    Console.WriteLine($"Run 'smu config set {ConfigProperty} [value]' to change it.");
                    break;

                case ConfigCommand.set:
                    if (!Program.Config.HasConfigValue(ConfigProperty))
                    {
                        Console.WriteLine($"The property '{ConfigProperty}' does not exist.");
                        Console.WriteLine("Run 'smu config list' to see all available properties.");
                        break;
                    }
                    if (ConfigValue == null)
                    {
                        Console.WriteLine($"A value is required when setting a configuration option.");
                        break;
                    }
                    Program.Config.SetConfigValue(ConfigProperty, ConfigValue);
                    break;

                case ConfigCommand.list:
                    Console.WriteLine("\tName:     \tDescription:");
                    foreach (System.Reflection.PropertyInfo pi in Program.Config.GetAllProperties())
                    {
                        Console.WriteLine($"\t{pi.Name, -10}\t{Program.Config.GetConfigValueDescription(pi.Name)}");
                    }    
                    break;

                default:
                    Console.WriteLine($"{ConfigAction} is not a valid command for the 'config' option. Valid options are 'get' and 'set'.");
                    break;

            }
        }
    }
}
