using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace SMU.Helpers
{
    public class AppConfig
    {
        private static readonly string FILENAME = "appconfig.json";

        // Config options

        /// <summary>
        /// The target mods folder location. This is usually set to %USERPROFILE%\Documents\Electronic Arts\The Sims 4\Mods.
        /// </summary>
        [Description("The target Mods folder location. This is usually set to %USERPROFILE%\\Documents\\Electronic Arts\\The Sims 4\\Mods.")]
        public string ModFolder { get; set; } = "";



        /// <summary>
        /// Creates a new configuration file for the application.
        /// </summary>
        /// <returns>A newly created <see cref="AppConfig"/> instance.</returns>
        private static AppConfig CreateNewConfig()
        {
            Console.WriteLine($"{FILENAME} does not exist, creating a new one...");
            AppConfig cfg = new AppConfig();
            File.WriteAllText(FILENAME, JsonConvert.SerializeObject(cfg, Formatting.Indented));
            return cfg;
        }

        /// <summary>
        /// Saves the current configuration instance to the config file.
        /// </summary>
        public void SaveConfig()
        {
            File.WriteAllText(FILENAME, JsonConvert.SerializeObject(this));
        }

        /// <summary>
        /// Loads the saved configuration file.
        /// </summary>
        /// <returns>The saved configuration file data, or creates and returns a new one if the file doesn't exist.</returns>
        public static AppConfig LoadConfig()
        {
            if (!File.Exists(FILENAME))
            {
                return CreateNewConfig();
            }
            return JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText(FILENAME));
        }

        /// <summary>
        /// Checks if the specified configuration property exists.
        /// </summary>
        /// <param name="name">The name of the config property to check.</param>
        /// <returns>True if it exists, false if not.</returns>
        public bool HasConfigValue(string name)
        {
            return GetType().GetProperty(name) != null;
        }

        /// <summary>
        /// Sets a configuration value then saves the configuration.
        /// </summary>
        /// <param name="name">The name of the config property to save.</param>
        /// <param name="value">The value to set.</param>
        public void SetConfigValue(string name, object value)
        {
            if (HasConfigValue(name))
            {
                GetType().GetProperty(name)?.SetValue(this, value);
                SaveConfig();
                Console.WriteLine($"Updated config property '{name}' to '{value}'.");
            }
        }

        /// <summary>
        /// Returns the current value of the specified configuration property.
        /// </summary>
        /// <param name="name">The name of the config property to get.</param>
        /// <returns>The currently set value of the config property.</returns>
        public object GetConfigValue(string name)
        {
            return GetType().GetProperty(name)?.GetValue(this);
        }

        /// <summary>
        /// Get all available configuration properties.
        /// </summary>
        /// <returns>Array of configuration properties.</returns>
        public PropertyInfo[] GetAllProperties()
        {
            return GetType().GetProperties();
        }

        /// <summary>
        /// Gets the description of the specified config property, if set.
        /// </summary>
        /// <param name="name">The name of the config property.</param>
        /// <returns>The description of the config property, if set.</returns>
        public string GetConfigValueDescription(string name)
        {
            return GetType().GetProperty(name)?.GetCustomAttribute<DescriptionAttribute>()?.Description;
        }
    }
}
