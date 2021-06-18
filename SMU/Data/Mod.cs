using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMU.Data
{
    /// <summary>
    /// The ModInfo class is a metadata collection for a particular mod.
    /// </summary>
    public class Mod
    {
        /// <summary>
        /// The name of the mod.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Description of the mod.
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// The author(s) of the mod.
        /// </summary>
        public string Author { get; set; } = "";

        /// <summary>
        /// The current version of the mod.
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// The author's or mod's website.
        /// </summary>
        public string Website { get; set; } = "";

        /// <summary>
        /// Where to check for mod updates.
        /// </summary>
        public string UpdateURL { get; set; } = "";

        /// <summary>
        /// The name of the mod's root folder.
        /// </summary>
        public string Root { get; set; } = "";

        /// <summary>
        /// The list of files included in the mod, with the root of the path being the mod folder.
        /// </summary>
        public List<string> Files { get; set; } = new List<string>();
    }
}
