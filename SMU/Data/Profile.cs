using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMU.Data
{
    /// <summary>
    /// A profile represents a collection of selected mods to use.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// The name of the profile.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the profile.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The list of selected mods in the profile.
        /// </summary>
        public List<Mod> Mods { get; set; } = new List<Mod>();
    }
}
