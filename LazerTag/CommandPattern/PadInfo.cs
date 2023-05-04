using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CommandPattern
{
    public class PadInfo
    {
        /// <summary>
        /// property for getting or setting wether a key is down
        /// </summary>
        public bool IsDown { get; set; }

        /// <summary>
        /// property for getting or setting a key
        /// </summary>
        public Buttons Button { get; set; }

        /// <summary>
        /// constructor which takes an argument so we can define a key
        /// </summary>
        /// <param name="button">the button we want as a keybind</param>
        public PadInfo(Buttons button)
        {
            this.Button = button;
        }
    }
}
